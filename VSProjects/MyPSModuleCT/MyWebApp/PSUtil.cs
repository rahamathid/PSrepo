using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp
{
    public class PSUtil
    {
        public static string CallGetAdditionalInfo(string fName, string lName, string uName)
        {
            StringBuilder sb = new StringBuilder();

            InitialSessionState initial = InitialSessionState.CreateDefault();
            initial.ImportPSModule(new string[] { @"C:\Program Files\WindowsPowerShell\Modules\MyPSBinaryModule.dll" });
            Runspace runspace = RunspaceFactory.CreateRunspace(initial);
            runspace.Open();

            // Call the PowerShell.Create() method to create an 
            // empty pipeline.
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;

            // Call the PowerShell.AddCommand(string) method to add 
            // the Get-AdditionalInfo cmdlet to the pipeline. Do 
            // not include spaces before or after the cmdlet name 
            // because that will cause the command to fail.
            ps.AddCommand("Get-PersonInfo");
            ps.AddParameter("UserName", uName);
            ps.AddParameter("FirstName", fName);
            ps.AddParameter("LastName", lName);
            ps.AddParameter("Employee", false);
            

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (PSObject result in ps.Invoke())
            {
                sb.Append(result.ToString()).Append("<br/>");                
            } // End foreach.

            return sb.ToString();

        }
    }
}
