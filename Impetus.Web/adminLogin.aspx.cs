using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;
using System.Web.Security;
using EvoPaj.Data;
using EvoPaj.Base;

public partial class adminLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["PostBack"] == "PasswordChanged")
            {
                MessageBox("Login", "alert('Password Changed Successfully. Please login with your new password');");
            }
            else if (Request.QueryString["PostBack"] == "SessionTimeout")
            {
                MessageBox("Login", "alert('Your session timed out. Please login again');");
            }
            else if (Request.QueryString["PostBack"] == "Logout")
            {
                Session["UserObject"] = null;
                MessageBox("Login", "alert('You have successfully logout');");
            }
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string UserName = txtUsername.Text.Trim();
        string Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");

        User userLogin = new UserSystem().GetUserByUserName(UserName, Password);

        if (userLogin != null)
        {

            Institution UserInstitution = new InstitutionDAO().Retrieve(userLogin.UserInstitutionID);
            if (UserInstitution.InstitutionName != System.Configuration.ConfigurationManager.AppSettings.Get("Institution"))
            {
                Response.Redirect("OurSupport.aspx?PostBack=LoginVia");
            }
            else
            {
                if (userLogin.Status == PANE.Framework.Functions.DTO.UserStatus.InActive)
                {
                    MessageBox("Login", "alert('Login Authentication Failed. Your User Account is Not Active. Contact Your Administrator.');");
                    return;
                }
                Session["AdminUser"] = userLogin;               
                Session["Institution"] = UserInstitution;

                txtPassword.Text = "";
                txtUsername.Text = "";
                if (userLogin.FirstLogin)
                {
                    Response.Redirect("ChangeAdminPassword.aspx");
                }
                else
                {
                    userLogin.LastLoginDate = System.DateTime.Now;
                    new UserSystem().UpdateUser(userLogin);
                    Response.Redirect("home.aspx");
                }
            }
        }
        else
            MessageBox("Login", "alert('Login Authentication Failed.');");
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