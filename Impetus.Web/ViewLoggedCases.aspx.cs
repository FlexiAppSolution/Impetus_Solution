using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EvoPaj.Base;
using EvoPaj.Logic;

public partial class ViewLoggedCases : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUser"] == null)
        {
            Response.Redirect("adminLogin.aspx?Postback=SessionTimeout");
        }
        else
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }
    }

    private void LoadData()
    {
        try
        {
            EvoPaj.Base.Issue TheCase = new Issue();
            long instionitutionID = Convert.ToInt64(comboInstitution.SelectedValue.ToString());

            if (comboCaseType.SelectedValue == "Existing")
                TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.Existing;
            else if (comboCaseType.SelectedValue == "New")
                TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.New;
            else
                TheCase.TheCaseType = EvoPaj.Base.Utility.CaseType.All;

            DateTime? DateFrom, DateTo;

            if (txtDateFrom.Text == "")
                DateFrom = System.DateTime.MinValue;
            else
                DateFrom = Convert.ToDateTime(txtDateFrom.Text);
            if (txtDateTo.Text == "")
                DateTo = System.DateTime.MaxValue;
            else
                DateTo = Convert.ToDateTime(txtDateTo.Text);

            IList<Issue> CaseList = new IssueLogic().SearchCase(instionitutionID, txtIssue.Text, DateFrom, DateTo, TheCase);

            GridView1.DataSource = CaseList;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            MessageBox("Error", "alert('" + ex.Message + "');");
            GridView1.DataSource = null;
            GridView1.DataBind();
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
            MessageBox("Error", "alert('" + ex.Message + "');");            
            GridView1.DataSource = null;
            GridView1.DataBind();
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