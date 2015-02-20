using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base.Entities;
using EvoPaj.Data;
using System.Text;
using EvoPaj.Logic;
using EvoPaj.Base;

public partial class CaseManagement_Control_EditLicenseRequest : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                long Id = Convert.ToInt64(Request.QueryString["ID"]);

                LicenseGeneration theLicense = new LicenseGenerationDAO().Retrieve(Id);
                
                lblRequestingUser.Text = theLicense.RequestingUser.FullName;
                lblInstitution.Text = theLicense.RequestingUser.TheInstitution.InstitutionName;
                lblDateRequested.Text = theLicense.DateRequested.ToString();
                ddlDuration.SelectedValue = string.IsNullOrEmpty(theLicense.LicenseDuration) || theLicense.LicenseDuration == "None" ? "" : theLicense.LicenseDuration;
                lblLicenseUsed.Text = theLicense.NoOfUsedLicense.ToString();
                lblNoOfLicense.Text = theLicense.NoOfLicense.ToString();
                txtQtyApproved.Text = theLicense.NoOfLicenseApproved.ToString();
                lblLicenseType.Text = theLicense.TheLicenseType.ToString();
                lblStatus.Text = theLicense.Approval.ToString();
                if (theLicense.Approval != EvoPaj.Base.Utility.LicenseGenerationApproval.Pending)
                {
                    btnApprove.Visible = false;
                    btnDecline.Visible = false;
                    txtQtyApproved.Enabled = false;
                    ddlDuration.Enabled = false;
                }               
            }
            else
                MessageBox("Update License", "alert('No License ID, Kindly refresh your browser and try again.');");
        }
    }
    protected void btnDecline_Click(object sender, EventArgs e)
    {
        MessageBox("Update License", "alert('" + Decline() + "');");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        MessageBox("Update License", "alert('" + Approve() + "');");
    }
    public string Approve()
    {
        string result = "";
        LicenseGeneration licenseGeneration = new LicenseGenerationSystem().RetrieveByID(Convert.ToInt64(Request.QueryString["ID"]));
        if (licenseGeneration != null)
        {
            if (Convert.ToInt16(txtQtyApproved.Text) > licenseGeneration.NoOfLicense)
                result = "Quantity of License Approved cannot be more than Quantity Requested.";
            else
            {
                licenseGeneration.ApprovingUser = (User)Session["AdminUser"];
                licenseGeneration.Approval = EvoPaj.Base.Utility.LicenseGenerationApproval.Approved;
                licenseGeneration.DateApproved = DateTime.Now;
                licenseGeneration.NoOfLicenseApproved = txtQtyApproved.Text.Trim();
                licenseGeneration.LicenseDuration = ddlDuration.SelectedValue;
                bool res = new LicenseGenerationSystem().UpdateLicenseGeneration(licenseGeneration);
                if (res == true)
                {
                    //new LicenseGenerationDAO().CommitChanges();

                    long Id = Convert.ToInt64(Request.QueryString["ID"]);
                    LicenseGeneration theLicense = new LicenseGenerationDAO().Retrieve(Id);
                    lblRequestingUser.Text = theLicense.RequestingUser.FullName;
                    lblInstitution.Text = theLicense.RequestingUser.TheInstitution.InstitutionName;
                    lblDateRequested.Text = theLicense.DateRequested.ToString();
                    ddlDuration.SelectedValue = string.IsNullOrEmpty(theLicense.LicenseDuration) || theLicense.LicenseDuration == "None" ? "" : theLicense.LicenseDuration;
                    lblLicenseUsed.Text = theLicense.NoOfUsedLicense.ToString();
                    lblNoOfLicense.Text = theLicense.NoOfLicense.ToString();
                    lblLicenseType.Text = theLicense.TheLicenseType.ToString();
                    txtQtyApproved.Text = theLicense.NoOfLicenseApproved.ToString();
                    lblStatus.Text = theLicense.Approval.ToString();
                    if (theLicense.Approval != EvoPaj.Base.Utility.LicenseGenerationApproval.Pending)
                    {
                        btnApprove.Visible = false;
                        btnDecline.Visible = false;
                        txtQtyApproved.Enabled = false;
                        ddlDuration.Enabled = false;
                    }

                    result = "License Request has been approved.";
                }
                else
                    result = "License request was not approved. Kindly try again.";
            }
        }
        return result;
    }
    public string Decline()
    {
        string result = "";
        LicenseGeneration licenseGeneration = new LicenseGenerationSystem().RetrieveByID(Convert.ToInt64(Request.QueryString["ID"]));
        licenseGeneration.ApprovingUser = (User)Session["AdminUser"];
        licenseGeneration.Approval = EvoPaj.Base.Utility.LicenseGenerationApproval.Declined;
        licenseGeneration.DateApproved = DateTime.Now;
        bool res = new LicenseGenerationSystem().UpdateLicenseGeneration(licenseGeneration);
        if (res == true)
        {
           // new LicenseGenerationDAO().CommitChanges();
            long Id = Convert.ToInt64(Request.QueryString["ID"]);
            LicenseGeneration theLicense = new LicenseGenerationDAO().Retrieve(Id);                
            lblRequestingUser.Text = theLicense.RequestingUser.FullName;
            lblInstitution.Text = theLicense.RequestingUser.TheInstitution.InstitutionName;
            lblDateRequested.Text = theLicense.DateRequested.ToString();
            ddlDuration.SelectedValue = string.IsNullOrEmpty(theLicense.LicenseDuration) || theLicense.LicenseDuration == "None" ? "" : theLicense.LicenseDuration;
            lblLicenseUsed.Text = theLicense.NoOfUsedLicense.ToString();
            lblNoOfLicense.Text = theLicense.NoOfLicense.ToString();
            lblLicenseType.Text = theLicense.TheLicenseType.ToString();
            txtQtyApproved.Text = theLicense.NoOfLicenseApproved.ToString();
            lblStatus.Text = theLicense.Approval.ToString();
            if (theLicense.Approval != EvoPaj.Base.Utility.LicenseGenerationApproval.Pending)
            {
                btnApprove.Visible = false;
                btnDecline.Visible = false;
                txtQtyApproved.Enabled = false;
                ddlDuration.Enabled = false;
            } 
    
            result = "License request has been declined.";
        }
        else
            result = "License request was not declined. Kindly try again.";

        return result;
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