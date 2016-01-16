using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace MyModule
{
    [Cmdlet(VerbsCommon.Get, "Salutation")]
    public class GetSalutation : PSCmdlet
    {
        private string[] nameCollection;

        [Parameter(Mandatory =true, ValueFromPipelineByPropertyName =true, 
            ValueFromPipeline =true, Position =0, HelpMessage ="Name to get salutation for.")]
        [Alias("Person", "FirstName")]
        public string[] Name
        {
            get { return nameCollection; }
            set { nameCollection = value; }
        }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        protected override void ProcessRecord()
        {
            foreach (string name in nameCollection)
            {
                WriteVerbose("Creating Salutation for " + name);
                string salutation = "Hello, " + name;
                WriteObject(salutation);
            }
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}
