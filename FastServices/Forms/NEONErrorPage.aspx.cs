using System;
using System.Web.UI;

public partial class Forms_NEONErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.Session["UserID"] == null)
            {
                this.Session.Clear();
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}
