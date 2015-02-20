using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Logic;

public partial class CreateInstitution : System.Web.UI.Page
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
            if (Session["AdminUser"] == null)
            {
                Response.Redirect("adminLogin.aspx?Postback=SessionTimeout");
            }
            else
            {
                Institution TheInstitution = new Institution();
                TheInstitution.InstitutionName = txtName.Text;
                TheInstitution.Address = txtAddress.Text;
                TheInstitution.Website = txtWebsite.Text;
                TheInstitution.ContactName = txtContactName.Text;
                TheInstitution.ContactPhoneNumber = txtPhone.Text;
                TheInstitution.ContactEmail = txtEmail.Text;
                TheInstitution.Status = PANE.Framework.Functions.DTO.Status.InActive;
                Institution lastInstitution = new InstitutionSystem().LastInstitution();
                if (lastInstitution == null)
                    TheInstitution.Code = 100;
                else
                    TheInstitution.Code = lastInstitution.Code + 1;

                //Check if this Instituion exists
                string message = "";
                bool exists = new InstitutionSystem().checkInstitution(TheInstitution, out message);
                if (exists)
                {
                    MessageBox("Create Institution", "alert('" + message + "');");
                }
                else
                {
                    bool save = new InstitutionSystem().SaveInstitution(TheInstitution);
                    if (save == true)
                    {
                        string msg = "Institution Creation was Successful. Institution Code: " + TheInstitution.Code;
                        MessageBox("Create Institution", "alert('" + msg + "');");
                        txtName.Text = "";
                        txtAddress.Text = "";
                        txtWebsite.Text = "";
                        txtContactName.Text = "";
                        txtPhone.Text = "";
                        txtEmail.Text = "";
                        Email logEmail = new Email();
                        string websiteurl = System.Configuration.ConfigurationManager.AppSettings["WebsiteUrl"];
                        logEmail.ToAddress = TheInstitution.ContactEmail;
                        logEmail.Title = "Impetus Institution Creation";
                        logEmail.Body = "You institution has been created with the following details:<br/>";
                        logEmail.Body += "<b>Institution Name:</b>" + TheInstitution.InstitutionName + "<br/>";
                        logEmail.Body += "<b>Institution Code:</b>" + TheInstitution.Code + "<br/>";
                        logEmail.Body += "<b>Website URL:</b>" + websiteurl + "<br/>";
                        
                        new EmailSystem().LogEmail(logEmail);

                    }
                    else
                    {
                        MessageBox("Create Institution", "alert('Institution Creation was NOT Successful');");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving case : {0}", ex.Message);
            MessageBox("Create Institution", "alert('Institution Creation was NOT Successful');");
            ScriptManager.RegisterClientScriptBlock(btnCreate, GetType(), "Create Institution", "alert('" + error + "')", true);
            new PANE.ERRORLOG.Error().LogToFile(new Exception(error));
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