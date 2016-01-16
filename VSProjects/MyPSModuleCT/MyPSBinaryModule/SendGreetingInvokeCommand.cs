using System;
using System.Diagnostics;
using System.Management.Automation;   // Windows PowerShell assembly.
using Microsoft.PowerShell.Commands;  // Windows PowerShell assembly.

namespace MyPSBinaryModule
{


    namespace SendGreeting
    {
        // Declare the class as a cmdlet and specify an 
        // appropriate verb and noun for the cmdlet name.
        [Cmdlet(VerbsCommunications.Send, "GreetingInvoke")]
        public class SendGreetingInvokeCommand : Cmdlet
        {
            // Declare the parameters for the cmdlet.
            [Parameter(Mandatory = true)]
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            private string name;

            // Override the BeginProcessing method to invoke 
            // the Get-Process cmdlet.
            protected override void BeginProcessing()
            {
                //GetProcessCommand gp = new GetProcessCommand();
                //gp.Name = new string[] { "[a-t]*" };
                //foreach (Process p in gp.Invoke<Process>())
                //{
                //    Console.WriteLine(p.ToString());
                //}

                GetAdditionalInfo ga = new GetAdditionalInfo();
                ga.Country = "Philippines";
                ga.Invoke();
                //foreach (Process p in ga.Invoke())
                //{
                //    Console.WriteLine(p.ToString());
                //}
            }

            // Override the ProcessRecord method to process
            // the supplied user name and write out a 
            // greeting to the user by calling the WriteObject
            // method.
            protected override void ProcessRecord()
            {
                WriteObject("Hello " + name + "!");
            }
        }
    }
}
