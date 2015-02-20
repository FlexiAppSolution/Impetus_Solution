using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;

public partial class GetRightCode : System.Web.UI.Page
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
            EvoPaj.Logic.PrintUtilityServiceReference.LicenseData data = new EvoPaj.Logic.PrintUtilityServiceReference.LicenseData();
            data = new LicenseSystem().GetRightCode(txtErrorCode.Text);
            txtRightCode.Text = data.rightCode;
            txtAdminPassword.Text = data.AdminPassword;
        }
        catch (Exception ex)
        {
            MessageBox("Get Right Code", "alert('" + ex.Message + "');");
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