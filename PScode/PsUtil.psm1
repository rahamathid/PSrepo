Function Get-DiskSpace {
    [cmdletBinding()]
    Param (
        [Parameter(Mandatory=$True,
                   ValuefromPipeline=$True,
                   ValuefrompipelineByPropertyName=$True)]
        [String[]]$ComputerNames
    )
    foreach ($ComputerName in $ComputerNames){
        $Disks = Get-WmiObject win32_logicaldisk -ComputerName $ComputerName | Where-Object {$_.DriveType -eq '3'}
        foreach ($Disk in $Disks) {
            $Prop = [ordered]@{
                    "Hostname"=$ComputerName;
                    "DevideID"= $Disk.DeviceID;
                    "DriveType"=$Disk.DriveType;
                    "FreeSpce(GB)"="{0:N2}" -f ($Disk.Freespace/1GB);
                    "TotalSize(GB)"= "{0:N2}" -f ($Disk.Size/1GB)
            }

            $objDisk = New-Object -TypeName PsObject -Property $Prop
            Write-Output $objDisk
        }
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
