using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;

public partial class CaseManagement_Control_ViewUser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            long Id = Convert.ToInt64(Request.QueryString["ID"]);

            User theUser = new UserDAO().Retrieve(Id);

            lblFullName.Text = theUser.FullName;
            lblEmployeeNum.Text = theUser.EmployeeNumber;
            lblPhoneNum.Text = theUser.PhoneNumber;
            lblInstitution.Text = theUser.TheInstitution.InstitutionName;
            lblRole.Text = theUser.TheRole.Name;
            lblEmail.Text = theUser.Email;
            lblAddress.Text = theUser.Address;
            lblstatus.Text = theUser.Status.ToString();
        }
        else
            lblMsg.Text = "No User ID, Kindly refresh your browser and try again.";
    }
}