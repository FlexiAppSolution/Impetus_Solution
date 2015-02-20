using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;
using EvoPaj.Data;
using EvoPaj.Base.Entities;
using EvoPaj.Base.Utility;
using EvoPaj.Base;

public partial class GetLicense : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
        }
    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            EvoPaj.Base.User RequestingUser = (User)Session["User"];
            IList<LicenseGeneration> licenseGeneration = new List<LicenseGeneration>();
            int totalLicenseApproved = 0;
            int totalLicenseUsed = 0;
            LicenseType TheLicenseType = (LicenseType)Enum.Parse(typeof(LicenseType), Convert.ToString(DropDownLicensetype.SelectedValue));
            if (RequestingUser != null)
            {

                licenseGeneration = new LicenseGenerationSystem().GetBUser(RequestingUser.ID, TheLicenseType);
                if (licenseGeneration != null && licenseGeneration.Count > 0)
                {
                    totalLicenseApproved = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfLicenseApproved));
                    totalLicenseUsed = licenseGeneration.Sum(x => Convert.ToInt32(x.NoOfUsedLicense));
                }

            }
            else
            {
                Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
            }
            EvoPaj.Logic.PrintUtilityServiceReference.LicenseData data = new EvoPaj.Logic.PrintUtilityServiceReference.LicenseData();
            if ((totalLicenseApproved - totalLicenseUsed) >= 0 && licenseGeneration != null)
            {
                string licensetype = ((int)TheLicenseType).ToString();
                string duration = Convert.ToString(licenseGeneration.OrderByDescending(x => x.ID).FirstOrDefault().LicenseDuration);
                data = new LicenseSystem().GetLicense(txtToken.Text, licensetype, duration);
                txtLicense.Text = data.LicenseField;

                LicenseGeneration toUpdate = licenseGeneration.OrderByDescending(x => x.ID).FirstOrDefault();
                if (toUpdate != null)
                {
                    toUpdate.NoOfUsedLicense += 1;
                    new LicenseGenerationSystem().UpdateLicenseGeneration(toUpdate);
                    new LicenseGenerationDAO().CommitChanges();
                }
            }
            else
            {
                MessageBox("Get License", "alert('You have not exhausted you current license for this licensetype');");
            }

        }
        catch (Exception ex)
        {
            MessageBox("Get License", "alert('" + ex.Message + "');");
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