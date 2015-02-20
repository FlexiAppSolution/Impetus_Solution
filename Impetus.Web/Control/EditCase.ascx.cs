using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Text;
using EvoPaj.Logic;

public partial class CaseManagement_Control_EditCase : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                long Id = Convert.ToInt64(Request.QueryString["ID"]);

                Issue theCase = new IssueDAO().Retrieve(Id);
                lblIncidentNumber.Text = theCase.CaseNumber.ToString().PadLeft(6, '0');
                txtName.Text = theCase.Name;
                txtErrorCode.Text = theCase.ErrorCode;
                txtDescription.Text = theCase.Description;
                txtResolution.Text = theCase.Resolution;
            }
            else
                lblMsg.Text = "No Case ID, Kindly refresh your browser and try again.";
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Issue theCase = new IssueLogic().RetrieveByID(Convert.ToInt64(Request.QueryString["ID"]));
        if (theCase != null)
        {
            theCase.Resolution = txtResolution.Text;
            theCase.TheCaseType = EvoPaj.Base.Utility.CaseType.Existing;
            theCase.DateResolved = System.DateTime.Now;
            bool res = new IssueLogic().UpdateCase(theCase);
            if (res == true)
            {
                new IssueDAO().CommitChanges();
                MessageBox("Log Case", "alert('Case updated successfully');");
                Email logEmail = new Email();
                string websiteurl = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"];
                logEmail.ToAddress = theCase.LoggingUser.Email;
                logEmail.Title = "Impetus Web Case Resolution";
                logEmail.Body = "A case logged by your Institution on Impetus Web Solution has been resolved. Details:<br/>";
                logEmail.Body += "<b>Case Number:</b>" + theCase.CaseNumber.ToString().PadLeft(6, '0') +"<br/>";
                logEmail.Body += "<b>Issue:</b>" + theCase.Name + "<br/>";
                logEmail.Body += "<b>Description:</b>" + theCase.Description + "<br/>";
                logEmail.Body += "<b>Resolution:</b>" + theCase.Resolution + "<br/>";
                logEmail.Comments = "With Impetus Web Solution, you get instant resolution to any printer issue.";
                new EmailSystem().LogEmail(logEmail);
            }
            else
                MessageBox("Log Case", "alert('Case was not updated successfully');");
        }
    }
    public void MessageBox(string key, string script)
    {
        string enclosed = ProcessMsg(script);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), key, enclosed, true);
    }

    private string ProcessMsg(string str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("function r(f){/in/.test(document.readyState)?setTimeout('r('+f+')',9):f()} r(function(){")
            .Append(str)
            .Append("});");
        return sb.ToString();
    }
}