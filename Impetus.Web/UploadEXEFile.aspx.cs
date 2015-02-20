using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Logic;
using EvoPaj.Base;

public partial class UploadEXEFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?PostBack=SessionTimeout");
        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        try
        {
            License TheLicense = new License();
            string fileName = ""; bool saveFile = false;
            string FilePath = System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"];
            
            if (this.FileUpload1.HasFile)
            {
                fileName = FileUpload1.FileName;
                
                FileUpload1.SaveAs(FilePath + fileName);
                saveFile = true;
            }
            else
            {
                MessageBox("Upload File Exe", "alert('Please Upload the File');");
            }
            if (saveFile)
            {
                TheLicense.TheInstitution = new InstitutionSystem().RetrieveByID(Convert.ToInt64(comboInstitution.SelectedValue.ToString()));
                TheLicense.FileName = fileName;
                TheLicense.FilePath = "Executables";
                bool saveLicense = new LicenseSystem().SaveLicense(TheLicense);
                if (saveLicense)
                {
                    MessageBox("Create License", "alert('FILE Upload was Successful');");
                    comboInstitution.SelectedValue = "0";
                }
                else
                    MessageBox("Create License", "alert('FILE Upload was NOT Successful');");
            }
        }
        catch (Exception ex)
        {
            string error = string.Format("An error occured when saving license : {0}", ex.Message);
            MessageBox("Create License", "alert('License Upload was NOT Successful');");
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