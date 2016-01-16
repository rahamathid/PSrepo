$service = "wuauserv" 
$Servers = Get-Content -path .\host.txt
foreach ($server in $Servers) {

    write-host "Working on : " $Server
    If (Test-Connection -ComputerName $server -Count 2 -Quiet) {
        Try {
            $stop = (gwmi win32_service -computername $server -filter "name='$service'" -ErrorAction stop).stopservice()
            $result = (gwmi win32_service -computername $server -filter "name='$service'" -ErrorAction stop).ChangeStartMode("Disabled")
            write-host "Disabled service on : " $Server
            "Disabled and stopped service on : $Server" | Out-file -Append .\output.txt
        }
        Catch {
            "Unable to Connect to WMI : $server" | Out-File -Append .\error.txt
            Write-Host "Unable to Connect to WMI : $server"
        }
    
    }
    Else {
        "Unable to ping :  $Server" | out-file -Append .\error.txt
        Write-Host "Unable to ping : " $Server
    }
}

$Server=""

foreach ($Server in $servers) {
    Get-Service -ComputerName $Server -Name $service

}
