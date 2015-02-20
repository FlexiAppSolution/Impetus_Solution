using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EvoPaj.Base;

public partial class RegularSite : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] != null)
        {
            User LoggedInUser = (User)Session["User"];
            string institution = ((Institution)Session["UserInstitution"]).InstitutionName;
            lbluser.Text = institution + "[" + LoggedInUser.LastName + " " + LoggedInUser.FirstName + "]";
        }
        else
        {
            Response.Redirect("OurSupport.aspx?PostBack=SessionTimeout");
        }  
    }
}
