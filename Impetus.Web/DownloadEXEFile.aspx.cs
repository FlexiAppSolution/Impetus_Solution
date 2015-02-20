using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;
using EvoPaj.Data;
using System.Text;
using EvoPaj.Logic;

public partial class DownloadEXEFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null)
        {
            Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
        }
        else
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string downloadcontent = GridView1.SelectedRow.Cells[0].Text;
            //Response.Clear();
            //Response.AppendHeader("content-disposition", string.Format("attachment;filename={0}", downloadcontent));
            //Response.Charset = "";
            //Response.ContentType = "application/x-zip-compressed";
         
            HttpContext.Current.Response.Redirect("Executables/" + downloadcontent);
            //using (ZipOutputStream s = new ZipOutputStream(Response.OutputStream))
            //{
            //    s.SetLevel(9); // 0-9, 9 being the highest level of compression
            //    ZipEntry entry = new ZipEntry(downloadcontent);
            //    entry.DateTime = DateTime.Now;
            //    s.PutNextEntry(entry);
            //    byte[] contents = Encoding.Default.GetBytes(downloadcontent);
            //    s.Write(contents, 0, contents.Length);
            //}
            //Response.Flush();
            //Response.End();
        }
        catch (Exception ex)
        {
            MessageBox("Download File", "alert('" + ex.Message + "');");
        }
    }
   
    private void LoadData()
    {
        try
        {
            if (Session["UserInstitution"] == null)
            {
                Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
            }
            else
            {

                License TheLicense = new License();
                TheLicense.TheInstitution.ID = ((Institution)Session["UserInstitution"]).ID;
                IList<License> LicenseList = new LicenseSystem().SelectFile(TheLicense);

                GridView1.DataSource = LicenseList;
                GridView1.DataBind();               
            }
        }
        catch
        {

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

    [System.Web.Services.WebMethod]
    public static string DownloadFile(string ID)
    {
        string result = "";
        long itemId = Convert.ToInt64(ID);
        License lc = new LicenseDAO().Retrieve(itemId);
        try
        {
            //string filePath = HttpContext.Current.Server.MapPath("~/CaseManagement/Executables/");
            //string fileName = lc.FileName;

            //FileInfo FileName = new FileInfo(filePath + fileName);
            //FileStream myFile = new FileStream(filePath + fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //BinaryReader _binaryReader = new BinaryReader(myFile);
            //long startBytes = 0;
            //string lastUpdateTimeStamp = File.GetLastWriteTimeUtc(filePath).ToString("r");
            //string _encodedData = HttpUtility.UrlEncode(fileName, Encoding.UTF8) + lastUpdateTimeStamp;

            //HttpContext.Current.Response.Clear();

            //HttpContext.Current.Response.Buffer = false;

            //HttpContext.Current.Response.AddHeader("Accept-Ranges", "bytes");

            //HttpContext.Current.Response.AppendHeader("ETag", "\"" + _encodedData + "\"");

            //HttpContext.Current.Response.AppendHeader("Last-Modified", lastUpdateTimeStamp);

            //HttpContext.Current.Response.ContentType = "application/octet-stream";

            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName.Name);

            //HttpContext.Current.Response.AddHeader("Content-Length", (FileName.Length - startBytes).ToString());

            //HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");

            //HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;

            //_binaryReader.BaseStream.Seek(startBytes, SeekOrigin.Begin);

            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + lc.FileName);
            HttpContext.Current.Response.TransmitFile(HttpContext.Current.Server.MapPath("Executables/" + lc.FileName));
            HttpContext.Current.Response.End();

            result = "File download was successful.";

        }
        catch
        {
            result = "File download was not successful.";
        }
        return result;
    }
}