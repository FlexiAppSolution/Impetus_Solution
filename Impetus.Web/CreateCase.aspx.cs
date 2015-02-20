using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using PANE.Framework.NHibernateManager;
using EvoPaj.Base;
using EvoPaj.Logic;

public partial class CreateCase : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["AdminUser"] == null)
            {
                Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
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
                    Issue TheCase = new Issue();
                    User theUser = (User)Session["AdminUser"];
                    Institution theInstitution = new Institution(theUser.TheInstitution.ID);
                    TheCase.Name = txtName.Text;
                    TheCase.Resolution = txtResolution.Text;
                    TheCase.Description = txtDescription.Text;
                    TheCase.ErrorCode = txtErrorCode.Text;
                    TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.Existing;
                    TheCase.DateLogged = System.DateTime.Now;
                    TheCase.DateResolved = System.DateTime.Now;
                    TheCase.LoggingUser = theUser;
                    TheCase.LoggingInstition = theInstitution;
                    long num = new IncidentNumberSystem().getIncidentNumber();
                    TheCase.CaseNumber = num;

                    bool save = new IssueLogic().SaveCase(TheCase);
                    if (save == true)
                    {
                        MessageBox("Create Case", "alert('Case creation was successful with incident number: " + num.ToString().PadLeft(6, '0') + ".');");
                        txtName.Text = "";
                        txtResolution.Text = "";
                        txtErrorCode.Text = "";
                        txtDescription.Text = "";
                    }
                    else
                    {
                        MessageBox("Create Case", "alert('Case Creation was NOT Successful');");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving case : {0}", ex.Message);
            MessageBox("Create Case", "alert('Case Creation was NOT Successful');");
            ScriptManager.RegisterClientScriptBlock(btnCreate, GetType(), "Create Case", "alert('" + error + "')", true);
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