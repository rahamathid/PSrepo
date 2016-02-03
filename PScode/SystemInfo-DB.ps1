$ConnectionString = "Server=Home-PC\SQLExpress;Database=SystemInfo;Trusted_Connection=True;"

Function Get-DBInfo {
    Get-MOLDatabaseData -connectionString $ConnectionString -isSQLServer -query "SELECT hostname FROM Info"
}

Function Set-DBInfo {
    param (
    [Parameter(Mandatory=$True,
                   ValuefromPipeline=$True,
                   ValuefrompipelineByPropertyName=$True)]
        [Object]$InputObject
    )
    Write-Debug $InputObject
    $Query = @"
        UPDATE Info SET
            Hostname = '$($InputObject.Hostname)',
            Domain = '$($InputObject.Domain)',
            Manufacturer = '$($InputObject.Manufacturer)',
            PrimaryOwnerName = '$($InputObject.PrimaryOwnerName)',
            TotalPhysicalMemory = $($InputObject.'TotalPhysicalMemory(GB)')
            WHERE Hostname = '$($InputObject.Hostname)'
"@
    Write-Debug $Query
    Invoke-MOLDatabaseQuery -connectionString $ConnectionString -isSQLServer -query $Query
}

function Get-MOLDatabaseData {
    [CmdletBinding()]
    param (
        [string]$connectionString,
        [string]$query,
        [switch]$isSQLServer
    )
    if ($isSQLServer) {
        Write-Verbose 'in SQL Server mode'
        $connection = New-Object -TypeName `
            System.Data.SqlClient.SqlConnection
    } else {
        Write-Verbose 'in OleDB mode'
        $connection = New-Object -TypeName `
            System.Data.OleDb.OleDbConnection
    }
    $connection.ConnectionString = $connectionString
    $command = $connection.CreateCommand()
    $command.CommandText = $query
    if ($isSQLServer) {
        $adapter = New-Object -TypeName `
        System.Data.SqlClient.SqlDataAdapter $command
    } else {
        $adapter = New-Object -TypeName `
        System.Data.OleDb.OleDbDataAdapter $command
    }
    $dataset = New-Object -TypeName System.Data.DataSet
    $adapter.Fill($dataset) | Out-Null
    $dataset.Tables[0]
    $connection.close()
}

function Invoke-MOLDatabaseQuery {
    [CmdletBinding(SupportsShouldProcess=$True,
                   ConfirmImpact='Low')]
    param (
        [string]$connectionString,
        [string]$query,
        [switch]$isSQLServer
    )
    if ($isSQLServer) {
        Write-Verbose 'in SQL Server mode'
        $connection = New-Object -TypeName `
            System.Data.SqlClient.SqlConnection
    } else {
        Write-Verbose 'in OleDB mode'
        $connection = New-Object -TypeName `
            System.Data.OleDb.OleDbConnection
    }
    $connection.ConnectionString = $connectionString
    $command = $connection.CreateCommand()
    $command.CommandText = $query
    if ($pscmdlet.shouldprocess($query)) {
        $connection.Open()
        $command.ExecuteNonQuery() | Out-Null
        $connection.close()
    }
}

Function Get-OSDetails {
    [cmdletBinding()]
    Param (
        [Parameter(Mandatory=$True,
                   ValuefromPipeline=$True,
                   ValuefrompipelineByPropertyName=$True)]
        [String[]]$ComputerNames
    )
    foreach ($ComputerName in $ComputerNames){
        $OS = Get-WmiObject win32_ComputerSystem -ComputerName $ComputerName
        $Prop = [ordered]@{
                "Hostname"=$ComputerName;
                "Domain"= $OS.Domain
                "Manufacturer"=$OS.Manufacturer;
                "PrimaryOwnerName"=$OS.PrimaryOwnerName;
                "TotalPhysicalMemory(GB)"="{0:N1}" -f ($OS.TotalPhysicalMemory / 1GB)
        }
        $objDisk = New-Object -TypeName PsObject -Property $Prop
        Write-Output $objDisk
    } 
}

(Get-DBInfo).Hostname | Get-OSDetails | Set-DBInfo

#Set-DBInfo -InputObject $InputObject -Debug