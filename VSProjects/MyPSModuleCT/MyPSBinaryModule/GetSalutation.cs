using System;
using System.Text;
using System.Management.Automation; //Powershell assembly
using System.Collections.Generic;

namespace MyPSBinaryModule
{

    /// <summary>
    /// Get-Salutation cmdlet 
    /// </summary>
    /*

    Declare an attribute that identifies the derived class as a cmdlet.

    Windows PowerShell verbs are assigned to a group based on their most common use.The groups are designed to make the
    verbs easy to find and compare, not to restrict their use.You can use any approved verb for any type of command.

    Each Windows PowerShell verb is assigned to one of the following groups.
    Common: Define generic actions that can apply to almost any cmdlet, such as Add.
    Communications: Define actions that apply to communications, such as Connect.
    Data: Define actions that apply to data handling, such as Backup.
    Diagnostic: Define actions that apply to diagnostics, such as Debug.
    Lifecycle: Define actions that apply to the lifecycle of a cmdlet, such as Complete.
    Security: Define actions that apply to security, such as Revoke.

    Other: Define other types of actions.
    */
    [Cmdlet(VerbsCommon.Get, "Salutation")]
    public class GetSalutation : Cmdlet
    {
        private string[] nameCollection;

        /*Define public properties that are decorated with attributes that 
        identify the public properties as cmdlet parameters.*/
        /// <summary>
        /// Name - takes a collection of Name values
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true,
            ValueFromPipeline = true, Position = 0, HelpMessage = "Name to get salutation for.")]
        [Alias("Person", "Name")]
        public string[] Name
        {
            get { return nameCollection; }
            set { nameCollection = value; }
        }


        /*
        Input Processing Methods

        The Cmdlet class provides the following virtual methods that are used to process records. 
        All the derived cmdlet classes must override one or more of the first three methods: 

        */

        /// <summary>
        /// BeginProcessing: Used to provide optional one-time, pre-processing functionality for the cmdlet.
        /// </summary>
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
        }

        /// <summary>
        /// ProcessRecord: Used to provide record-by-record processing functionality for the cmdlet. 
        /// The ProcessRecord method might be called any number of times, or not at all, depending on 
        /// the input of the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            foreach (string name in nameCollection)
            {
                WriteVerbose("Creating Salutation for " + name);
                string salutation = "Hello, " + name;
                WriteObject(salutation);
            }

        }

        /// <summary>
        /// EndProcessing: Used to provide optional one-time, post-processing 
        /// functionality for the cmdlet.
        /// </summary>
        protected override void EndProcessing()
        {
            base.EndProcessing();
        }

        /// <summary>
        /// StopProcessing: Used to stop processing when the user stops the cmdlet asynchronously 
        /// (for example, by pressing CTRL+C).
        /// </summary>
        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

       
    }



   


}
