using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;

public partial class CaseManagement_Control_ViewCase : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            long Id = Convert.ToInt64(Request.QueryString["ID"]);

            Issue theCase = new IssueDAO().Retrieve(Id);
            lblIncidentNumber.Text = theCase.CaseNumber.ToString().PadLeft(6,'0');
            lblIssue.Text = theCase.Name;
            lblInstitution.Text = theCase.LoggingInstition.InstitutionName;
            lblErrorCode.Text = theCase.ErrorCode;
            lblLoggedBy.Text = theCase.LoggingUser.FullName;
            lblDateLogged.Text = theCase.DateLogged.Value.ToString();
            lblDescription.Text = theCase.Description;
            lblResolution.Text = string.IsNullOrEmpty(theCase.Resolution) ? "Not Resolved Yet" : theCase.Resolution;
        }
        else
            lblMsg.Text = "No Case ID, Kindly refresh your browser and try again.";
    }
}