using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text.Length > 0 && tbFirstName.Text.Length > 0 && tbLastName.Text.Length > 0)
                lblResult.Text = PSUtil.CallGetAdditionalInfo(tbFirstName.Text, tbLastName.Text, tbUsername.Text);
            else
                lblResult.Text = "All fields are required.  Please supply the values.";
        }
    }
}