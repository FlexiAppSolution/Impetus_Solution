using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["AdminUser"] != null)
        {
            User LoggedInUser = (User)Session["AdminUser"];
            string institution = ((Institution)Session["Institution"]).InstitutionName;
            lbluser.Text = institution + "[" + LoggedInUser.LastName + " " + LoggedInUser.FirstName + "]";
        }
        else
        {
            Response.Redirect("adminlogin.aspx?PostBack=SessionTimeout");
        }  
    }
}
