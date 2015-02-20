using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;
using EvoPaj.Logic;
using System.Text;

public partial class CaseManagement_Control_EditInstitution : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                long Id = Convert.ToInt64(Request.QueryString["ID"]);


                Institution theInstitution = new InstitutionDAO().Retrieve(Id);
                txtName.Text = theInstitution.InstitutionName;
                lblCode.Text = theInstitution.Code.ToString();
                txtAddress.Text = theInstitution.Address;
                txtWebsite.Text = theInstitution.Website;
                if (theInstitution.Status == PANE.Framework.Functions.DTO.Status.InActive)
                    comboStatus.SelectedValue = "1";
                else
                    comboStatus.SelectedValue = "2";
                txtContactName.Text = theInstitution.ContactName;
                txtPhone.Text = theInstitution.ContactPhoneNumber;
                txtEmail.Text = theInstitution.ContactEmail;
            }
            else
                lblMsg.Text = "No Institution ID, Kindly refresh your browser and try again.";
        }
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Institution TheInstitution = new Institution();
            TheInstitution.ID = Convert.ToInt64(Request.QueryString["ID"]);
            TheInstitution.InstitutionName = txtName.Text;
            TheInstitution.Address = txtAddress.Text;
            TheInstitution.Website = txtWebsite.Text;
            TheInstitution.ContactName = txtContactName.Text;
            TheInstitution.ContactPhoneNumber = txtPhone.Text;
            TheInstitution.ContactEmail = txtEmail.Text;
            TheInstitution.Code = Convert.ToInt16(lblCode.Text);
            if (comboStatus.SelectedValue == "1")
                TheInstitution.Status = PANE.Framework.Functions.DTO.Status.InActive;
            else if (comboStatus.SelectedValue == "2")
                TheInstitution.Status = PANE.Framework.Functions.DTO.Status.Active;

            bool update = new InstitutionSystem().UpdateInstitution(TheInstitution);
            new InstitutionDAO().CommitChanges();
            if (update == true)
            {
                string msg = "Institution Update was Successful";
                MessageBox("Update Institution", "alert('" + msg + "');");               
            }
            else
            {
                MessageBox("Update Institution", "alert('Institution Update was NOT Successful');");
            }
            //}
        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving case : {0}", ex.Message);
            MessageBox("Update Institution", "alert('Institution Update was NOT Successful');");
            ScriptManager.RegisterClientScriptBlock(btnUpdate, GetType(), "Update Institution", "alert('" + error + "')", true);
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