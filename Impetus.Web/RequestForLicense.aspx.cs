using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Logic;
using EvoPaj.Data;
using EvoPaj.Base;
using EvoPaj.Base.Utility;
using EvoPaj.Base.Entities;
using System.Text;

public partial class RequestForLicense : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
        }
    }
    private string EncloseOnDOMReadyEvent(string str)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("function r(f){/in/.test(document.readyState)?setTimeout('r('+f+')',9):f()} r(function(){")
            .Append(str)
            .Append("});");
        return sb.ToString();
    }

    public void MessageBox(string key, string script)
    {
        string enclosed = EncloseOnDOMReadyEvent(script);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), key, enclosed, true);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            int usedLicense = 0;
            int approvedLicense = 0;
            if (Session["User"] != null)
            {
                LicenseType TheLicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), Convert.ToString(DropDownLicensetype.SelectedValue));
                IList<LicenseGeneration> licenseGeneration = new LicenseGenerationSystem().GetBUser(((User)Session["User"]).ID, TheLicenseType);
                if (licenseGeneration != null && licenseGeneration.Count > 0)
                {
                    usedLicense = licenseGeneration.Sum(x => x.NoOfUsedLicense);
                    approvedLicense = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfLicenseApproved));
                }
                if (usedLicense >= approvedLicense && usedLicense != 0)
                {
                    LicenseGeneration theLicenseToGenerate = new LicenseGeneration();
                    theLicenseToGenerate.NoOfLicense = Convert.ToInt32(txtLicenseNo.Text);
                    theLicenseToGenerate.NoOfLicenseApproved = "0";
                    theLicenseToGenerate.NoOfUsedLicense = 0;
                    theLicenseToGenerate.TheLicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), Convert.ToString(DropDownLicensetype.SelectedValue));
                    theLicenseToGenerate.DateRequested = DateTime.Now;
                    theLicenseToGenerate.RequestingUser = (User)Session["User"];
                    theLicenseToGenerate.TheInstitution = ((User)Session["User"]).TheInstitution;
                    //send email to pajuno            
                    Email logEmail = new Email();

                    logEmail.ToAddress = System.Configuration.ConfigurationManager.AppSettings["PajunoEmail"];
                    logEmail.Title = string.Format("License Request From {0} ", theLicenseToGenerate.RequestingUser.FullName);
                    logEmail.Body = string.Format("A license request from {0} by {1}:<br/>", theLicenseToGenerate.RequestingUser.TheInstitution.Name, theLicenseToGenerate.RequestingUser.UserName);
                    logEmail.Body += "<b>Institution Name:</b>" + theLicenseToGenerate.RequestingUser.TheInstitution.Name + "<br/>";
                    logEmail.Body += "<b>Institution Code:</b>" + theLicenseToGenerate.RequestingUser.TheInstitution.Code + "<br/>";
                    logEmail.Body += "<b>No of License Code:</b>" + theLicenseToGenerate.NoOfLicense + "<br/>";
                    logEmail.Body += "<b>License Type:</b>" + theLicenseToGenerate.TheLicenseType.ToString() + "<br/>";
                    logEmail.Comments = "";
                    new EmailSystem().LogEmail(logEmail);

                    bool saved = new LicenseGenerationSystem().SaveLicenseGeneration(theLicenseToGenerate);
                    if (saved)
                    {
                        new LicenseGenerationDAO().CommitChanges();
                        txtLicenseNo.Text = "";
                        DropDownLicensetype.SelectedValue = "0";
                    }

                    string msg = "Your Request has been Logged for Approval";
                    MessageBox("Login", "alert('" + msg + "');");

                }
                else
                {
                    txtLicenseNo.Text = "";
                    DropDownLicensetype.SelectedValue = "0";
                    string msg = "You have not exhausted your license";
                    MessageBox("Login", "alert('" + msg + "');");
                }
            }
            else
            {
                Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
            }
        }
        catch (Exception ex)
        {
            MessageBox("Login", "alert('" + ex.Message + "');"); ;
        }

    }
}