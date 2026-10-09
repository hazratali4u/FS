using System;
using System.Data;

public partial class Forms_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["Message"] != null)
            {
                lblMessage.Text = Session["Message"].ToString();
                Session["Message"] = null;
            }

            if (Request.QueryString["LevelID"] != null)
            {
                if (Request.QueryString["LevelID"].ToString() == "29")
                {
                    if (IsTopMenuAssign("158"))
                    {
                        Response.Redirect("frmEnquiryView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopMenu=1");
                    }
                }                
                else if (Request.QueryString["LevelID"].ToString() == "5")
                {
                    if (IsTopMenuAssign("5"))
                    {
                        Response.Redirect("frmProductView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopMenu=1");
                    }
                }
                else if (Request.QueryString["LevelID"].ToString() == "6")
                {
                    Response.Redirect("frmCustomer.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopMenu=1&LevelType=1");
                }
                else if (Request.QueryString["LevelID"].ToString() == "28")
                {
                    Response.Redirect("frmSupplier.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopMenu=1&LevelType=1");
                }
            }
        }
    }

    private bool IsTopMenuAssign(string mModuleID)
    {
        DataTable dtMenu = (DataTable)Session["dtMenu"];
        bool flag = false;
        if (dtMenu != null)
        {
            foreach (DataRow dr in dtMenu.Rows)
            {
                if (dr["MID"].ToString() == mModuleID)
                {
                    flag = true;
                    break;
                }
            }
        }
        return flag;
    }
}