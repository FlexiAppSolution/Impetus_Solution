using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Web.Security;

public partial class OurSupport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PostBack"] == "LoginVia")
        {
            MessageBox("Login", "alert('Kindly login via this portal.');");
        }
        else if (Request.QueryString["PostBack"] == "Logout")
        {
            Session["user"] = null;            
        }
        else if (Request.QueryString["PostBack"] == "PasswordChanged")
        {
            MessageBox("Login", "alert('Password Changed Successfully. Please login with your new password');");
        }
        else if (Request.QueryString["PostBack"] == "SessionTimeout")
        {
            MessageBox("Login", "alert('Your session timed out. Please login again');");
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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string institutionCode = txtInstitutionCode.Text;        
        if (string.IsNullOrEmpty(institutionCode))
            MessageBox("Login", "alert('Kindly enter you institution code to login.');");
        else
        {            
            Institution theInstitution = new InstitutionSystem().GetInstitutionByCode(Convert.ToInt32(institutionCode));
            if (theInstitution == null)
                MessageBox("Login", "alert('" + string.Format("Institution with code {} does not exist.", institutionCode) + "');");
            else if (theInstitution.Status == PANE.Framework.Functions.DTO.Status.InActive)
                MessageBox("Login", "alert('Your institution is not active. Kindly contact your administrator');");
            else
            {               
                string UserName = txtUsername.Text.Trim();
                string Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text.Trim(), "SHA1");                
                User userLogin = new UserSystem().GetUserByUserName(UserName, Password);                
                if (userLogin != null)
                {
                    Institution UserInstitution = new InstitutionDAO().Retrieve(userLogin.UserInstitutionID);
                    
                    if (!UserInstitution.Code.ToString().Equals(institutionCode))
                    {                       
                        MessageBox("Login", "alert('Login Authentication Failed. Wrong Institution Code.');");
                        return;
                    }
                    if (userLogin.Status == PANE.Framework.Functions.DTO.UserStatus.InActive)
                    {                       
                        MessageBox("Login", "alert('Login Authentication Failed. Your User Account is Not Active. Contact Your Administrator.');");
                        return;
                    }
                    Session["User"] = userLogin;
                    Session["UserInstitution"] = theInstitution;

                    txtPassword.Text = "";
                    txtUsername.Text = "";                    
                    if (userLogin.FirstLogin)
                    {                       
                        Response.Redirect("ChangeUserPassword.aspx");
                    }
                    else
                    {                      
                        userLogin.LastLoginDate = System.DateTime.Now;
                        new UserSystem().UpdateUser(userLogin);
                        Response.Redirect("regularhome.aspx");
                    }
                }
                else
                    MessageBox("Login", "alert('Login Authentication Failed.');");
            }
        }
    }
}