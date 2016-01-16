<#
	Get-Salutation
#>
function Get-Salutation {
	[CmdletBinding()]
	param(
		[parameter(Mandatory=$true,ValueFromPipeline=$true, 
		Position=0, HelpMessage="Name to get salutation for.")]
		[alias("Person","ParamName")]	
		[string[]] $Name
		
	)
	
	PROCESS {
		 foreach ($n in $Name)
            {
                Write-Verbose("Creating Salutation for " + $n);
                [string] $salutation = "Hello, " + $n;
                Write($salutation);
            }
	}
}



