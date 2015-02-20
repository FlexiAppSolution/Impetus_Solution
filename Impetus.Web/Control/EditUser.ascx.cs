using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Data;
using EvoPaj.Base;
using EvoPaj.Logic;

public partial class CaseManagement_Control_EditUser : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                long Id = Convert.ToInt64(Request.QueryString["ID"]);

                User theUser= new UserDAO().Retrieve(Id);

                txtLastName.Text = theUser.LastName;
                txtFirstName.Text = theUser.FirstName;
                txtEmployeeNum.Text = theUser.EmployeeNumber;
                txtPhoneNum.Text = theUser.PhoneNumber;
                txtEmail.Text = theUser.Email;
                txtAddress.Text = theUser.Address;
                comboInstitution.SelectedValue = theUser.TheInstitution.ID.ToString();
                comboRole.SelectedValue = theUser.TheRole.ID.ToString();
                if (theUser.Status == PANE.Framework.Functions.DTO.UserStatus.InActive)
                    comboStatus.SelectedValue = "1";
                else
                    comboStatus.SelectedValue = "2";
                txtUser.Text = theUser.UserName;               
            }
            else
                MessageBox("Update User", "alert('No User ID, Kindly refresh your browser and try again.');");
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            EvoPaj.Base.User TheUser = new UserDAO().Retrieve(Convert.ToInt64(Request.QueryString["ID"]));            
            TheUser.LastName = txtLastName.Text;
            TheUser.FirstName = txtFirstName.Text;
            TheUser.EmployeeNumber = txtEmployeeNum.Text;
            TheUser.PhoneNumber = txtPhoneNum.Text;
            TheUser.Email = txtEmail.Text;
            TheUser.Address = txtAddress.Text;
            TheUser.TheInstitution = new Institution(Convert.ToInt64(comboInstitution.SelectedValue.ToString()));
            TheUser.TheRole = new Role(Convert.ToInt64(comboRole.SelectedValue.ToString()));
            //TheUser.Password = txtPass.Text;
            //TheUser.FirstLogin = Convert.ToBoolean(txtFirstLogin.Text);
            if (comboStatus.SelectedValue == "1")
                TheUser.Status = PANE.Framework.Functions.DTO.UserStatus.InActive;
            else if (comboStatus.SelectedValue == "2")
                TheUser.Status = PANE.Framework.Functions.DTO.UserStatus.Active;
            TheUser.UserName = txtUser.Text;
            bool updated = new UserSystem().UpdateUser(TheUser);
            new UserDAO().CommitChanges();
            if (updated)
            {
                MessageBox("Update User", "alert('User Update Was Successful');");               
            }
            else
            {
                MessageBox("Update User", "alert('User Update Was Not Successful');");
            }
        }
        catch (Exception ex)
        {
            MessageBox("Update User", "alert('" + ex.Message + "');");
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