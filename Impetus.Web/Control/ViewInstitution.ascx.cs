using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Logic;
using EvoPaj.Data;

public partial class CaseManagement_Control_ViewInstitution : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            long Id = Convert.ToInt64(Request.QueryString["ID"]);

            Institution theInstitution = new InstitutionDAO().Retrieve(Id);

            lblName.Text = theInstitution.InstitutionName;
            lblCode.Text = theInstitution.Code.ToString();
            lblAddress.Text = theInstitution.Address;
            lblWebsite.Text = theInstitution.Website;
            lblStatus.Text = theInstitution.Status.ToString();
            lblContactName.Text = theInstitution.ContactName;
            lblPhone.Text = theInstitution.ContactPhoneNumber;
            lblEmail.Text = theInstitution.ContactEmail;
        }
        else
            lblMsg.Text = "No Institution ID, Kindly refresh your browser and try again.";
    }
}