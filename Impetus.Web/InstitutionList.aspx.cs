using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Logic;
using EvoPaj.Base;
using System.Text;

public partial class InstitutionList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminlogin.aspx?PostBack=SessionTimeout");
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
    private void LoadData()
    {
        EvoPaj.Base.Institution TheInstituion = new Institution();
        TheInstituion.InstitutionName = txtInstitutionName.Text;
        IList<Institution> InstitutionList = new InstitutionSystem().SelectInstitution(TheInstituion);
        if (InstitutionList.Count != 0)
        {
            GridView1.DataSource = InstitutionList;
            GridView1.DataBind();
        }
        else
        {
            MessageBox("View Institution", "alert('No Records Found');");
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
            MessageBox("View Institution", "alert('" + ex.Message + "');");            
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }
}