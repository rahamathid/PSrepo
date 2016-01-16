using MyPSBinaryModule;
using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace MyConsoleAppPS
{
    class Program
    {
        static void Main(string[] args)
        {
            callGetProcess();
            //callGetAdditionalInfo();
            //callAdditionalInfo2();
            //callGetPersonInfo("Noemi", "Veneracion", "noemi616");

            Console.ReadLine();
        }

        static void callGetPersonInfo(string fName, string lName, string uName)
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
            ps.AddParameter("Employee", true);

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (PSObject result in ps.Invoke())
            {
                sb.Append(result.ToString()).Append("<br/>");
            } // End foreach.

            Console.WriteLine(sb.ToString());

        }

        static void callAdditionalInfo2()
        {
            GetAdditionalInfo ga = new GetAdditionalInfo();
            ga.Country = "Philippines";            

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (string result in ga.Invoke())
            {
                Console.WriteLine(result.ToString());
            } // End foreach.

        }


        static void callGetAdditionalInfo()
        {
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
            ps.AddCommand("Get-AdditionalInfoPS");
            ps.AddParameter("Country", "Philippines");

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (PSObject result in ps.Invoke())
            {
                Console.WriteLine(result.ToString());
            } // End foreach.
            

        }

        static void callGetProcess()
        {
            // Call the PowerShell.Create() method to create an 
            // empty pipeline.
            PowerShell ps = PowerShell.Create();

            // Call the PowerShell.AddCommand(string) method to add 
            // the Get-Process cmdlet to the pipeline. Do 
            // not include spaces before or after the cmdlet name 
            // because that will cause the command to fail.
            ps.AddCommand("Get-Process");

            Console.WriteLine("Process                 Id");
            Console.WriteLine("----------------------------");

            // Call the PowerShell.Invoke() method to run the 
            // commands of the pipeline.
            foreach (PSObject result in ps.Invoke())
            {
                Console.WriteLine(
                        "{0,-24}{1}",
                        result.Members["ProcessName"].Value,
                        result.Members["Id"].Value);
            } // End foreach.
        }
    }
}
