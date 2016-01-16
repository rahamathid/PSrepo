   
$Servers = gc .\host.txt
"computername|DevideID|Name" | Out-file -Append .\Proc.txt
"ComputerName|BankLabel|Capacity|Speed|DeviceLocator|SerialNumber" | out-file -Append .\Memory.txt

Foreach ($ComputerName in $Servers) {
    If (Test-Connection $ComputerName -Count 2 -Quiet) {
        Write-Host "Working on : $ComputerName"
        Try {
            $Procs= Get-WmiObject Win32_processor -ComputerName $ComputerName -ErrorAction stop
            Foreach ($Proc in $Procs){
                "$computername|$($proc.DevideID)|$($Proc.Name)" | Out-file -Append .\Proc.txt
            }
            $Memorys = Get-WmiObject Win32_PhysicalMemory -ComputerName $ComputerName -ErrorAction stop
            Foreach ($Memory in $Memorys) {
                "$ComputerName|$($Memory.BankLabel)|$($Memory.Capacity)|$($Memory.Speed)|$($Memory.DeviceLocator)|$($Memory.SerialNumber)" | out-file -Append .\Memory.txt
            }

        }
        Catch {
            "$ComputerName : WMI Connection Failure" | Out-File -Append .\error.txt
            Write-host "$ComputerName : WMI Connection Failure"
        }

    }
    Else {
        "$ComputerName :Ping Failed" | Out-File -Append .\Error.txt
        Write-Host "$ComputerName :Ping Failed"
    }
}    
