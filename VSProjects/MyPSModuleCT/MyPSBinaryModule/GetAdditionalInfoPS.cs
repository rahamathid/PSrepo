using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MyPSBinaryModule
{
    [Cmdlet(VerbsCommon.Get, "AdditionalInfoPS")]
    public class GetAdditionalInfoPS : PSCmdlet
    {
        private string country;
        private Dictionary<string, string> countryDictionary = new Dictionary<string, string>();

        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true,
        ValueFromPipeline = true, Position = 0, HelpMessage = "A Person's Country of Origin")]
        [Alias("ParamCountry")]
        //[ValidateSet("Philippines, Singapore")]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        protected override void BeginProcessing()
        {
            countryDictionary.Add("Philippines", "PH");
            countryDictionary.Add("Singapore", "SG");
        }

        protected override void ProcessRecord()
        {
            WriteVerbose("Identifying country code...");


            try
            {
                WriteObject("Country Code:" + countryDictionary[country]);
                WriteObject("Country:" + country);
            }
            catch (KeyNotFoundException kex)
            {
                var errRec = new ErrorRecord(kex, "1234", ErrorCategory.ObjectNotFound, this);
                this.ThrowTerminatingError(errRec);
 
            }

            catch (Exception ex)
            {
                var errRec = new ErrorRecord(ex, "3456", ErrorCategory.NotSpecified, this);
                this.ThrowTerminatingError(errRec);

            }
            finally
            {
                ;
            }


        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
        }
    }
}
