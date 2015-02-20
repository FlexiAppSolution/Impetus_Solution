using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;
using EvoPaj.Logic;
using EvoPaj.Data;
using EvoPaj.Base;

public partial class ChangeUserPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
        }
    }

    protected void btnChangePassword_Click(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            User theUser = (User)Session["User"];
            string Message = "";
            string loggingPassword = theUser.Password;
            string currentPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtCurrentPassword.Text.Trim(), "SHA1");
            bool ConfirmPassword = new UserSystem().ConfirmPassword(loggingPassword, currentPassword, txtNewPassword.Text, txtConfirmPassword.Text, out Message);
            if (ConfirmPassword)
            {
                theUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtNewPassword.Text.Trim(), "SHA1");
                bool update = new UserSystem().UpdateUser(theUser);
                if (update)
                {
                    theUser.FirstLogin = false;
                    new UserSystem().UpdateUser(theUser);
                    new UserDAO().CommitChanges();
                    MessageBox("Change Password", "alert('Password change was successful');");
                    Response.Redirect("OurSupport.aspx?PostBack=PasswordChanged");
                }
                else
                    MessageBox("Change Password", "alert('Password change was not successful');");

            }
            else
            {
                MessageBox("Change Password", "alert('" + Message + "');");
            }
        }
        else
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
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