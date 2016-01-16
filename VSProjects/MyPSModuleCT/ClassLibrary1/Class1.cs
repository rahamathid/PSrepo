using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace ClassLibrary1
{
    [Cmdlet(VerbsCommon.Get, "Helloworld")]
    public class GetHelloworld : Cmdlet
    {
        [Parameter(Mandatory =true,HelpMessage ="This is an example for PS DLL",ValueFromPipelineByPropertyName =true,
            Position =0)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            WriteObject("Hello there, " + this.Name);
        }
    }
}
