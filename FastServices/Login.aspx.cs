using System;
using System.Data;
using System.Web.UI;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Login : System.Web.UI.Page
{
    UserController mController = new UserController();
    RoleManagementController ObjRole = new RoleManagementController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToInt32(Session["UserID"]) > 0)
                {
                    Response.Redirect("Forms/Home.aspx");
                }
            }
            catch (Exception ex)
            {
                txtLogin.Focus();
            }
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        if (txtCity.Text == "Fast Services" || txtCity.Text == "Fast Intelligent Solutions")
        {
            ValidateUser();            
        }
        else
        {
            lblErrorMsg.Text = "Please select Company";
            lblErrorMsg.Visible = true;
        }
    }

    public void ValidateUser()
    {

        if (txtLogin.Text == "" && txtPassword.Text == "")
        {
            return;
        }
        else
        {
            if (txtCity.Text == "Fast Intelligent Solutions")
            {
                FSCommonLayer.Classes.Configuration.Company = 1;
            }
            else
            {
                FSCommonLayer.Classes.Configuration.Company = 0;
            }

            DataTable dt = mController.SelectSlashUser(txtLogin.Text, SAMS.Common.Classes.Cryptography.Encrypt(txtPassword.Text, "b0tin@74"));
            this.Session.Clear();
            if (dt.Rows.Count > 0)
            {
                this.Session.Add("UserID", Convert.ToInt32(dt.Rows[0]["USER_ID"].ToString()));
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                this.Session.Add("DISTRIBUTOR_ID", Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("CompanyId", Convert.ToInt32(dt.Rows[0]["COMPANY_ID"].ToString()));
                this.Session.Add("RoleID", Convert.ToInt32(dt.Rows[0]["ROLE_ID"]));
                LastClosedDay(Convert.ToInt32(dt.Rows[0]["USER_ID"]), Convert.ToInt32(dt.Rows[0]["DISTRIBUTOR_ID"]));
                this.Session.Add("UserName2", dt.Rows[0]["USER_NAME"].ToString());
                this.Session.Add("UserName", dt.Rows[0]["USER_DETAIL"].ToString());
                this.Session.Add("EmailAccount", dt.Rows[0]["EMAIL_ACCOUNT"].ToString());
                this.Session.Add("AccountPassword", dt.Rows[0]["EMAIL_PASSWORD"].ToString());
                this.Session.Add("SMTP", dt.Rows[0]["SMTP_ADDRESS"].ToString());
                this.Session.Add("PEOPLE_ID", dt.Rows[0]["PEOPLE_ID"].ToString());
                long User_Log_ID = mController.InsertUserLoginTime(Convert.ToInt32(dt.Rows[0]["USER_ID"]));
                this.Session.Add("User_Log_ID", User_Log_ID);

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Login", "Login DateTime " + System.DateTime.Now.ToString("dd/MM/yyy hh:mm:ss tt"));

                DataTable dtMenu = new DataTable();
                Session.Add("dtMenu", dtMenu);

                DataTable dtRights = new DataTable();
                this.Session.Add("dtRights", dtRights);

                Response.Redirect("Forms/Home.aspx");
            }
            else
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = "Wrong User Id/Password";
                return;
            }
        }
    }

    private void LastClosedDay(int UserId, int p_Distributor)
    {
        DistributorController mDayClose = new DistributorController();
        DataTable dt = mDayClose.SelectMaxDayClose(UserId, p_Distributor);
        if (dt.Rows.Count > 0)
        {
            this.Session.Add("CurrentWorkDate", DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString()));
        }
        else
        {
            this.Session.Add("CurrentWorkDate", DateTime.Now);
        }
    }
}