using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Base.Entities;
using EvoPaj.Logic;
using EvoPaj.Base.Utility;

public partial class LicenseInstitutionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
        }
        else
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            MessageBox("Error", "alert('" + ex.Message + "');");           
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

    }

    private void LoadData()
    {
        DateTime? DateFrom, DateTo;

        if (txtDateFrom.Text == "")
            DateFrom = null;
        else
            DateFrom = Convert.ToDateTime(txtDateFrom.Text);
        if (txtDateTo.Text == "")
            DateTo = null;
        else
            DateTo = Convert.ToDateTime(txtDateTo.Text);
        long instionitutionID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());

        LicenseType license = (LicenseType)Enum.Parse(typeof(LicenseType), Convert.ToString(DropDownLicensetype.SelectedValue));

        IList<LicenseGeneration> MyLicenseList = new LicenseGenerationSystem().SearchLicenseGeneration(DateFrom, DateTo, license, instionitutionID, ddlApprovalStatus.SelectedValue.ToString());
        GridView1.DataSource = MyLicenseList;
        GridView1.DataBind();

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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }
}