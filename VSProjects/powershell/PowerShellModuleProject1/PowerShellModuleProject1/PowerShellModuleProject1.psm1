<#
	My Function
#>
function Get-Function {
	Get-WmiObject Win32_services
	Write-Host "Hello"
}
Get-Function