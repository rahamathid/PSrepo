$Global:LogFile = "c:\temp\Set-NicCompliance.log"

Function Logging {
    Param ($Message,$Level)
    "$(Get-Date) `t $Level `t $Message" | Out-file -Append $Global:LogFile
    write-host "$(Get-Date) `t $Level `t $Message"
}

Function Get-NicDetails {
    
    Param ($ComputerName=$env:COMPUTERNAME)

    Try {

        $NICs=Get-WmiObject Win32_NetworkAdapterConfiguration -ComputerName $ComputerName |
            where {$_.IPEnabled -eq "True" -and $_.DefaultIPGateway -ne $Null}
        
        Foreach ($NIC in $NICs) {
            If ($NIC.DHCPenabled -eq $True) {
                Logging "DHCP is enalbed on NIC with IP $($NIC.IPAddress.split(" ")[0])." "INFO"
                Logging "No action required, settings are controlled by DHCP server" "INFO"
            }
            Else { 
                 $CurrentDNSServer=($NIC.DNSServerSearchOrder) -join ","
                 #$Global:Dnsservers="202.156.1.16,218.186.2.16,218.186.2.6"
                 #Write-host $Global:DNSServers
                 If ($Global:Dnsservers -eq $CurrentDNSServer) {
                        Logging "Detected DNS settings : $CurrentDNSServer `t IPAddress:$($NIC.IPAddress.split(" ")[0])" "INFO"
                        Logging "Expected DNS Settings : $Global:Dnsservers" "INFO"
                        Logging "No Action required, all settings are in palce" "INFO"
                 }
                 Else {
                    Logging "Detected DNS settings : $CurrentDNSServer `t IPAddress:$($NIC.IPAddress.split(" ")[0])" "INFO"
                    Logging "Expected DNS Settings : $Global:Dnsservers" "INFO"
                    Logging "Settings are not as expected, corrective Action will be taken" "INFO"
                    
                    $Proc=$NIC.SetDNSServerSearchOrder($Global:Dnsservers)
                    
                    If ($Proc.ReturnValue -eq "0" -or $Proc.RetrunValue -eq "1") {
                        Logging "DNS settings applied sucessfully : $($Global:Dnsservers)" "INFO"
                    }
                    Else {
                        Logging "DNS Settings failed to apply" "ERROR"
                    }
                 }
            }             
         }
            
     }
     Catch {
        Logging "Unable to connect to WMI $_" "Error"
     }       
}


Function Get-ServerDetails {
    Param ($ComputerName=$env:COMPUTERNAME)
    #Write-Host $ComputerName
    switch ($ComputerName) {
    
    {$ComputerName -like "*pc*"} { 
        [string[]]$DNSSetting = "1.1.1.1","2.2.2.2","3.3.3.3","4.4.4.4"
        Logging "Server name detected is $ComputerName and must have DNS Settings $DnsSetting" "INFO"
        return $DNSSetting
    }

    {$ComputerName -like "*GB2*"} { 
        [string[]]$DNSSetting = "11.11.11.11","22.22.22.22","33.33.33.33","44.44.44.44"
        Logging "Server name detected is $ComputerName and must have DNS Settings $DnsSetting" "INFO"
        return $DnsSetting
    }

    default {
        Logging "Server name detected is $ComputerName, but no valid DNS settings found" "ERROR"
        Exit 1
    }
    
    }

}

$Global:Dnsservers=Get-ServerDetails
if (Get-NicDetails) {
    Write-host "True returned" 
}
Else {
    write-host "false returned"
}
