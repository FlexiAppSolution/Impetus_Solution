using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Logic;
using System.Text;

public partial class GetServiceCode : System.Web.UI.Page
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
            data = new LicenseSystem().GetRightCode(txtWrongCode.Text);
            txtRightCode.Text = data.rightCode;
        }
        catch (Exception ex)
        {
            MessageBox("Get Service Code", "alert('" + ex.Message + "');");
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