using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Data;
using EvoPaj.Logic;
using EvoPaj.Base;
using System.Web.Security;

public partial class CreateUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?Postback=SessionTimeout");
        }

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            EvoPaj.Base.User TheUser = new User();
            TheUser.LastName = txtLastName.Text;
            TheUser.FirstName = txtFirstName.Text;
            TheUser.EmployeeNumber = txtEmployeeNum.Text;
            TheUser.PhoneNumber = txtPhoneNum.Text;
            TheUser.Email = txtEmail.Text;
            TheUser.Address = txtAddress.Text;
            TheUser.TheInstitution = new InstitutionDAO().Retrieve(Convert.ToInt64(comboInstitution.SelectedValue.ToString()));
            TheUser.TheRole = new RoleDAO().Retrieve(Convert.ToInt64(comboRole.SelectedValue.ToString()));
            TheUser.UserName = txtUsername.Text;
            string password = "password";
            TheUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            TheUser.FirstLogin = true;
            TheUser.Status = PANE.Framework.Functions.DTO.UserStatus.InActive;
            TheUser.UserInstitutionID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());

            string msg = "";
            bool save = new UserSystem().SaveUser(TheUser, out msg);
            new PANE.ERRORLOG.Error().LogToFile(new Exception("user saved"));
            Institution toretrieve = new Institution() { ID = TheUser.TheInstitution.ID };
            Institution myinst = new InstitutionSystem().GetInstitutionByID(toretrieve).FirstOrDefault();
            if (myinst != null)
            {
                new PANE.ERRORLOG.Error().LogToFile(new Exception(string.Format("retrieved institution with id {0} and code {1}", myinst.ID, myinst.Code)));
            }
            if (save)
            {
                MessageBox("Create User", "alert('User Creation was Successful');");
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtEmployeeNum.Text = "";
                txtPhoneNum.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtUsername.Text = "";
                Email logEmail = new Email();
                string websiteurl = System.Configuration.ConfigurationManager.AppSettings["SupportUrl"];
                logEmail.ToAddress = TheUser.Email;
                logEmail.Title = "Impetus Solution User Creation";
                logEmail.Body = "You have been created on Impetus Web Platform with the following details:<br/>";
                logEmail.Body += "<b>Username:</b> " + TheUser.UserName + "<br/>";
                if (myinst != null)
                {
                    logEmail.Body += "<b>Institution Code:</b> " + myinst.Code + "<br/>";
                }
                else
                {
                    logEmail.Body += "<b>Institution Code:</b> " + " " + "<br/>";
                }
                logEmail.Body += "<b>Password:</b> " + password + "<br/>";
                logEmail.Body += "<b>Website URL:</b> " + websiteurl + "<br/>";
                
                new PANE.ERRORLOG.Error().LogToFile(new Exception("before saving email"));
                bool saveemail = new EmailSystem().SaveEmail(logEmail);
                if (saveemail == true)
                {
                    new PANE.ERRORLOG.Error().LogToFile(new Exception("email saved"));
                    try
                    {
                        new UserDAO().CommitChanges();
                    }
                    catch (Exception ex)
                    {
                        new PANE.ERRORLOG.Error().LogToFile(ex);
                        new UserDAO().RollbackChanges();
                    }
                }




                //new EmailSystem().LogEmail(logEmail);                            
            }
            else
            {
                MessageBox("Create User", "alert('" + msg + "');");
            }

        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving case : {0}", ex.Message);
            MessageBox("Create User", "alert('User Creation was NOT Successful');");            
        }
    }

    private static Random random = new Random((int)DateTime.Now.Ticks);
    private string GeneratePassword(int size)
    {
        StringBuilder builder = new StringBuilder();
        char ch;
        for (int i = 0; i < size; i++)
        {
            ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
            builder.Append(ch);
        }

        return builder.ToString();
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