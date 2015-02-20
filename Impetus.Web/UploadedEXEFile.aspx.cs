using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;
using EvoPaj.Logic;
using System.Text;

public partial class UploadedEXEFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();

        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            License TheLicense = new License();
            TheLicense.TheInstitution.ID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());
            IList<License> LicenseList = new LicenseSystem().SelectFile(TheLicense);

            GridView1.DataSource = LicenseList;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox("Error", "alert('" + ex.Message + "');");
            GridView1.DataSource = null;
            GridView1.DataBind();
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

    [System.Web.Services.WebMethod]
    public static string DeleteItem(string ID)
    {
        string result = "";
        long itemId = Convert.ToInt64(ID);
        License lc = new LicenseDAO().Retrieve(itemId);
        try
        {
            bool toDelete = new LicenseSystem().DeleteLicense(lc);
            if (toDelete == true)
            {
                new LicenseDAO().CommitChanges();
                result = "File was successfully deleted. Refresh your page.";
            }
            else
                result = "File was NOT successfully deleted.";

        }
        catch
        {
            result = "File was NOT successfully deleted.";
        }
        return result;
    }
}