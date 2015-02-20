using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;
using EvoPaj.Base.Entities;

public partial class CaseManagement_Control_ViewLicenseRequest : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            long Id = Convert.ToInt64(Request.QueryString["ID"]);
            
            LicenseGeneration theLicense = new LicenseGenerationDAO().Retrieve(Id);

            lblRequestingUser.Text = theLicense.RequestingUser.FullName;
            lblInstitution.Text = theLicense.RequestingUser.TheInstitution.InstitutionName;
            lblDateRequested.Text = theLicense.DateRequested.ToString();
            lblDuration.Text = string.IsNullOrEmpty(theLicense.LicenseDuration) ? "Not Approved Yet." : theLicense.LicenseDuration;
            lblLicenseUsed.Text = theLicense.NoOfUsedLicense.ToString();
            lblNoOfLicense.Text = theLicense.NoOfLicense.ToString();
            lblQuantityApproved.Text = string.IsNullOrEmpty(theLicense.NoOfLicenseApproved.ToString()) ? "Not Approved Yet." : theLicense.NoOfLicenseApproved.ToString();
            lblStatus.Text = theLicense.Approval.ToString();
        }
        else
            lblMsg.Text = "No License ID, Kindly refresh your browser and try again.";
    }
}