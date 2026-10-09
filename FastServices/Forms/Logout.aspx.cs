using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null)
            {
                UserController mController = new UserController();
                mController.InsertUserLogoutTime(Convert.ToInt64(Session["User_Log_ID"]), Convert.ToInt32(Session["UserID"]));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Logout", "Logout DateTime " + System.DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt"));
            }
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
    }
}