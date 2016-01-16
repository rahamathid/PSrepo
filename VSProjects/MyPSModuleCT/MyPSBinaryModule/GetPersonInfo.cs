using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MyPSBinaryModule
{
    /// <summary>
    /// Get-PersonInfo cmdlet 
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
    [Cmdlet(VerbsCommon.Get, "PersonInfo")]
    public class GetPersonInfo : Cmdlet, IDynamicParameters
    {
        private string userName;
        private string firstName;
        private string lastName;


        /*Define public properties that are decorated with attributes that 
        identify the public properties as cmdlet parameter sets.
        */
        /// <summary>
        /// Username - takes a unique user name
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Test01",
        ValueFromPipeline = true, Position = 0, HelpMessage = "A Person's User Name")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ParameterSetName = "Test02")]
        [Alias("ParamUserName")]
        [ValidateNotNullOrEmpty]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// FirstName - take a person's first name
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "Test01",
            ValueFromPipeline = true, Position = 1, HelpMessage = "A Person's First Name")]
        [Alias("ParamFirstName")]
        [ValidateNotNullOrEmpty]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        /// <summary>
        /// LastName - take the person's last name
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = false, ParameterSetName = "Test01",
            ValueFromPipeline = true, Position = 2, HelpMessage = "A Person's Last Name")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "Test02")]
        [Alias("ParamLastName")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        /*Defines a switch parameter that accepts a boolean value
        To be used for enabling Dynamic Parameters */
        /// <summary>
        /// Employee - determines if the customer is an FTE or not
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "Test01")]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = false, ParameterSetName = "Test02")]
        [Alias("FTE")]
        public SwitchParameter Employee
        {
            get { return employee; }
            set { employee = value; }
        }
        private Boolean employee;

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

            //Displaying parameter sets
            string person = new StringBuilder(firstName).Append(" ").Append(lastName).ToString().TrimEnd().TrimStart();
            WriteVerbose("Displaying data for " + person);
            WriteObject("UserName: " + userName);
            WriteObject("FirsName: " + firstName);
            WriteObject("LastName: " + lastName);

            //Will be displayed if dynamic parameters are defined
            if (employee)
            {
                WriteObject("Department:" + context.Department);
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

        //Get Dynamic parameters
        private SendGreetingCommandDynamicParameters context;
        public object GetDynamicParameters()
        {
            if (employee)
            {
                context = new SendGreetingCommandDynamicParameters();
                return context;
            }
            return null;

        }
    }

    //Define a class that will hold
    //the dynamic parameter
    public class SendGreetingCommandDynamicParameters
    {
        [Parameter]
        [ValidateSet("Marketing", "Sales", "Development")]
        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        private string department;
    }


}
