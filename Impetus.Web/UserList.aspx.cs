using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Logic;

public partial class UserList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            LoadData();

        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminlogin.aspx?PostBack=SessionTimeout");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            LoadData();
        }
        catch (Exception ex)
        {
            MessageBox("View Institution", "alert('" + ex.Message + "');");
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
    private void LoadData()
    {
        EvoPaj.Base.User TheUser = new User();
        TheUser.TheInstitution.TheInstitutionID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());
        TheUser.LastName = txtLastName.Text;
        try
        {
            IList<User> UserList = new UserSystem().GetUser(TheUser);

            if (UserList != null && UserList.Count > 0)
            {
                GridView1.DataSource = UserList;
                GridView1.DataBind();
            }
            else
            {
                new PANE.ERRORLOG.Error().LogToFile(new Exception("No record")); ;
                MessageBox("Update User", "alert('No Records Found');");
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            new PANE.ERRORLOG.Error().LogToFile(ex);
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadData();
    }
}