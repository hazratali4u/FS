using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Text;

public partial class AppMaster : System.Web.UI.MasterPage
{
    MenuController MenuCtl = new MenuController();
    RoleManagementController ObjRole = new RoleManagementController();
    DataTable dtChild = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Remove("LevelType");
        Session.Remove("TopID");
        Session.Remove("LevelID");

        if (!IsPostBack)
        {
            int SessionAlertCountDown = 0;
            try
            {
                SessionAlertCountDown = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SessionAlertCountDown"]);
            }
            catch (Exception ex)
            {
                SessionAlertCountDown = 0;
            }

            int _displayTimeInMiliSec = (Session.Timeout - SessionAlertCountDown) * 60000;
            if (_displayTimeInMiliSec >= 0)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(),
                    "message",
                    "<script type=\"text/javascript\" language=\"javascript\">Timer('" + _displayTimeInMiliSec + "');</script>",
                    false);
            }

            int RoleID = (int)Session["RoleID"];
            if (RoleID == 1)
            {
                StringBuilder strUser = new StringBuilder();
                strUser.Append("<li><a href='frmChangePassword.aspx'>My Settings</a></li>");
                strUser.Append("<li><a href='frmMenu.aspx'>Arrange Menu</a></li>");
                strUser.Append("<li><a href='Logout.aspx'>Log Out</a></li>");
                ltrlUser.Text = strUser.ToString();
            }
            else
            {
                StringBuilder strUser = new StringBuilder();
                strUser.Append("<li><p>&nbsp;</p></li>");
                strUser.Append("<li><a href='frmChangePassword.aspx'>My Settings</a></li>");
                strUser.Append("<li><a href='Logout.aspx'>Log Out</a></li>");
                ltrlUser.Text = strUser.ToString();
            }
            lblUser.Text = Session["UserName"].ToString();
            lblWorkingDate.Text = ((DateTime)this.Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            ltrlMenu.Text = GetTopMenu();
            LoadLeftMenu(RoleID);
        }
    }
    private void LoadMainMenu(int RoleID, int TypeID, int ModuleID)
    {
        RoleManagementController ObjRole = new RoleManagementController();
        DataTable dtParent = ObjRole.SelectRoleWiseModule(RoleID, TypeID, ModuleID);
        if (dtParent != null)
        {
            gvLeftMenu.DataSource = dtParent;
            gvLeftMenu.DataBind();
        }
    }
    private string GetTopMenu()
    {
        DataTable dtBreadCrumb = MenuCtl.GetMenu(Constants.IntNullValue, Constants.IntNullValue, 12, (int)Session["RoleID"]);
        StringBuilder strBreadCrumb = new StringBuilder();
        if (dtBreadCrumb != null && dtBreadCrumb.Rows.Count > 0)
        {
            foreach (DataRow row in dtBreadCrumb.Rows)
            {
                if (row["FirstLink"].ToString() != "")
                {
                    strBreadCrumb.Append("<a href='" + row["FirstLink"].ToString() + "?LevelType=3&LevelID=" + row["FirstLinkID"] + "&TopID=" + row["MENU_ID"] + "'>" + row["MENU_DESCRIPTION"].ToString() + "</a>");
                }                
            }
        }
        return strBreadCrumb.ToString();
    }
    private void LoadLeftMenu(int RoleID)
    {
        if (Request.QueryString["TopID"] != null)
        {
            DataTable dtParent = ObjRole.SelectRoleWiseModule(RoleID, 3, Convert.ToInt32(Request.QueryString["TopID"]));
            if (dtParent != null)
            {
                gvLeftMenu.DataSource = dtParent;
                gvLeftMenu.DataBind();
            }
        }
    }
    protected void gvLeftMenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvLeftMenuSub = (GridView)e.Row.FindControl("gvLeftMenuSub");
            int ParentID = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
            dtChild = ObjRole.SelectRoleWiseModule(Convert.ToInt32(Session["RoleID"]), 4, ParentID);
            if (dtChild != null)
            {
                gvLeftMenuSub.DataSource = dtChild;
                gvLeftMenuSub.DataBind();
            }
        }
    }
}