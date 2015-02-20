using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;
using EvoPaj.Data;
using EvoPaj.Base;
using System.Web.Security;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
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
            TheUser.TheRole = new RoleDAO().Retrieve(3);
            TheUser.UserName = txtUsername.Text;
            string password = txtPassword.Text;
            TheUser.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
            TheUser.FirstLogin = true;
            TheUser.Status = PANE.Framework.Functions.DTO.UserStatus.Active;
            TheUser.UserInstitutionID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());
            TheUser.FirstLogin = false;

            string msg = "";
            bool save = new UserSystem().SaveUser(TheUser, out msg);

            Institution toretrieve = new Institution() { ID = TheUser.TheInstitution.ID };
            Institution myinst = new InstitutionSystem().GetInstitutionByID(toretrieve).FirstOrDefault();

            if (save)
            {
                string themsg = string.Format("Registration was successful. Your instituion code is {0}. Kindly keep this in order to be able to login successfully.", TheUser.TheInstitution.Code);
                MessageBox("Register", "alert('" + themsg + "');");
                txtLastName.Text = "";
                txtFirstName.Text = "";
                txtEmployeeNum.Text = "";
                txtPhoneNum.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";
                txtUsername.Text = "";
                txtPassword.Text = "";

                Email logEmail = new Email();
                logEmail.ToAddress = System.Configuration.ConfigurationManager.AppSettings["PajunoEmail"];
                logEmail.Title = "Impetus Web User Creation<br/>";
                logEmail.Body = "A user has been successfully created on Impetus Web Platform with the following details:<br/>";
                logEmail.Body += "<b>Username:</b> " + TheUser.UserName + "<br/>";
                if (myinst != null)
                {
                    logEmail.Body += "<b>Institution Code:</b> " + myinst.Code + "<br/>";
                }
                else
                {
                    logEmail.Body += "<b>Institution Code:</b> " + " " + "<br/>";
                }


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
            }
            else
            {
                if (!string.IsNullOrEmpty(msg))
                    MessageBox("Register", "alert('" + msg + "');");
                else
                    MessageBox("Register", "alert('Registration was not successful. kindly try again');");
            }

        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving case : {0}", ex.Message);
            MessageBox("Create User", "alert('Registration was NOT Successful');");
            //ScriptManager.RegisterClientScriptBlock(btnCreate, GetType(), "Create User", "alert('" + error + "')", true);
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