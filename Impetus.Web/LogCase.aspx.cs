using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using PANE.Framework.Managed.NHibernateManager;
using EvoPaj.Logic;
using EvoPaj.Base;

public partial class LogCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
        }
    }
    protected void btnLog_Click(object sender, EventArgs e)
    {
        try
        {
            Issue TheCase = new Issue();
            User theUser = (User)Session["User"];
            if (Session["User"] == null)
            {
                Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
            }
            else
            {
                IList<Issue> getIssueByCode = new IssueLogic().GetCaseByErrorCode(txtErrorCode.Text);
                if (getIssueByCode.Count > 0)
                {
                    MessageBox("Create Case", "alert('Case with error code: " + txtErrorCode.Text + ". has been logged already.');");
                }
                else
                {
                    long num = new IncidentNumberSystem().getIncidentNumber();
                    Institution theInstitution = new InstitutionSystem().RetrieveByID(theUser.TheInstitution.ID);
                    TheCase.Name = txtName.Text;
                    TheCase.Description = txtDescription.Text;
                    TheCase.ErrorCode = txtErrorCode.Text;
                    TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.New;
                    TheCase.DateLogged = System.DateTime.Now;
                    TheCase.DateResolved = System.DateTime.Now;
                    TheCase.LoggingUser = theUser;
                    TheCase.LoggingInstition = theInstitution;
                    TheCase.CaseNumber = num;

                    bool save = new IssueLogic().SaveCase(TheCase);
                    if (save == true)
                    {
                        MessageBox("Create Case", "alert('Case Logging was Successful. Incident Number is: " + num.ToString().PadLeft(6, '0') + ". Kindly keep this number for future reference.');");
                        txtName.Text = "";
                        txtErrorCode.Text = "";
                        txtDescription.Text = "";

                        Email logEmail = new Email();
                        string websiteurl = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"];
                        logEmail.ToAddress = System.Configuration.ConfigurationManager.AppSettings["PajunoEmail"];
                        logEmail.Title = "Impetus Web Case Creation";
                        logEmail.Body = "A case has been created on Impetus Web Solution with the following details:<br/>";
                        logEmail.Body += "<b>Incident Number:</b>" + num.ToString().PadLeft(6, '0') + "<br/>";
                        logEmail.Body += "<b>Issue:</b>" + TheCase.Name + "<br/>";
                        logEmail.Body += "<b>Description:</b>" + TheCase.Description + "<br/>";
                        logEmail.Body += "<b>Logged By:</b>" + TheCase.LoggingUser.FullName + "<br/>";
                        logEmail.Body += "<b>Institution:</b>" + TheCase.LoggingUser.TheInstitution.InstitutionName + "<br/>";
                        
                        new EmailSystem().LogEmail(logEmail);
                    }
                    else
                    {
                        MessageBox("Log Case", "alert('Case Logging was NOT Successful');");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when Logging case : {0}", ex.Message);
            MessageBox("Log Case", "alert('Case Logging was NOT Successful');");
            ScriptManager.RegisterClientScriptBlock(btnCreate, GetType(), "Log Case", "alert('" + error + "')", true);
        }
        finally
        {
            NHibernateSessionManager.Instance.CloseSession();
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