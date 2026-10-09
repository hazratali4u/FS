using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_Level : System.Web.UI.Page
{
    RoleManagementController ObjRole = new RoleManagementController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["LevelID"] != null)
            {
                if (Request.QueryString["LevelID"].ToString() == "29")
                {
                    Response.Redirect("frmEnquiryView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopMenu=1");
                }                
                else if (Request.QueryString["LevelID"].ToString() == "5")
                {
                    Response.Redirect("frmProductView.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopMenu=1");
                }
                else if (Request.QueryString["LevelID"].ToString() == "145")
                {
                    Response.Redirect("frmOrganization.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopMenu=1&LevelType=1");
                }
                else if (Request.QueryString["LevelID"].ToString() == "144")
                {
                    Response.Redirect("frmPeople.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&TopMenu=1&LevelType=1");
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
            LoadMainGrid();
        }
    }

    private void LoadMainGrid()
    {
        int RoleID = (int)Session["RoleID"];
        DataTable dtMenu = ObjRole.SelectRoleWiseModule(RoleID, 3, Convert.ToInt32(Request.QueryString["LevelID"]));
        gvMain.DataSource = dtMenu;
        gvMain.DataBind();
    }    
}