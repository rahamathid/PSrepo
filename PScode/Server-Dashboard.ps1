Function Get-DiskSpace {
    [cmdletBinding()]
    Param (
        [Parameter(Mandatory=$True,
                   ValuefromPipeline=$True,
                   ValuefrompipelineByPropertyName=$True)]
        [String[]]$ComputerNames
    )
    foreach ($ComputerName in $ComputerNames){
        $Disks = Get-WmiObject win32_logicaldisk -ComputerName $ComputerName
        foreach ($Disk in $Disks) {
            $Prop = @{
                    "Hostname"=$ComputerName;
                    "DevideID"= $Disk.DeviceID;
                    "DriveType"=$Disk.DriveType;
                    "FreeSpce"="{0:N2}" -f ($Disk.Freespace/1GB);
                    "TotalSize"= "{0:N2}" -f ($Disk.Size/1GB)
            }

            $objDisk = New-Object -TypeName PsObject -Property $Prop
            Write-Output $objDisk
        }
    } 
}

Function Get-ServerStatus {
    Param (
        $ComputerName
    )

    $Ret = $False

    If (Test-Connection -ComputerName $ComputerName -Count 2 -Quiet) {
        Try {
            $WMIStatus = Get-WmiObject Win32_Bios -ComputerName $ComputerName -ErrorAction stop
            $Ret = $True
        }
        Catch {
            $Ret = $False
        }
    }
    Return $Ret
}

"localhost","home-pc" |Get-DiskSpace