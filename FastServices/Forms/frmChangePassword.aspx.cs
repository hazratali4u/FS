using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System;
using System.Data;

/// <summary>
/// Form To Change User Password
/// </summary>
public partial class Forms_fmChangePassword : System.Web.UI.Page
{
    UserController UserInfo = new UserController();
    DataTable dtLogin_ID = new DataTable();

    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dtLogin_ID = UserInfo.SelectSlashUser(Convert.ToInt32(Session["PEOPLE_ID"].ToString()));
            txtSMTP.Text = dtLogin_ID.Rows[0]["SMTP_ADDRESS"].ToString();
            txtEmailAccount.Text = dtLogin_ID.Rows[0]["EMAIL_ACCOUNT"].ToString();
        }
    }

    #region Change Password

    /// <summary>
    /// Updates User Password
    /// </summary>
    /// <remarks>
    /// Returns True on Success And False on Failure
    /// </remarks>
    /// <returns>True on Success And False on Failure</returns>
    protected bool UpdatePassword()
    {
        try
        {
            dtLogin_ID = UserInfo.SelectSlashUser(Convert.ToInt32(Session["PEOPLE_ID"].ToString()));
            if (SAMS.Common.Classes.Cryptography.Encrypt(this.txtCurrentPassword.Text, "b0tin@74") == dtLogin_ID.Rows[0]["PASSWORD"].ToString())
            {
                UserInfo.UpdateUser(Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(Session["CompanyId"]), dtLogin_ID.Rows[0]["LOGIN_ID"].ToString(), SAMS.Common.Classes.Cryptography.Encrypt(this.txtConfirmNewPassword.Text, "b0tin@74"), Constants.IntNullValue, true, Convert.ToInt32(Session["DISTRIBUTOR_ID"].ToString()), null, null, null);
                return true;
            }
            else 
            {
                ltrlPassword.Text = Utility.ShowAlert(false, "Current Password is wrong.");
                return false;
            }
        }
        catch(Exception ex)
        {
            ltrlPassword.Text = Utility.ShowAlert(false, ex.Message);
            return false;
        }
    }

    /// <summary>
    /// Updates User Password Through UpdatePassword() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrlPassword.Text = "";
        bool status = UpdatePassword();
        if (status == true)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Change Password", Session["UserName2"].ToString() + " changed password");

            ltrlPassword.Text = Utility.ShowAlert(true, "Password has changed successfully.");
        }
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.txtCurrentPassword.Text = null;
        this.txtNewPassword.Text = null;
        this.txtConfirmNewPassword.Text = null;
    }

    #endregion

    #region Email Settings

    protected void btnSaveEmail_Click(object sender, EventArgs e)
    {
        ltrlEmail.Text = "";
        bool status = UpdateUserEmailSetting();
        if (status == true)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Email Settings", Session["UserName2"].ToString() + " Email settings updated");

            ltrlEmail.Text = Utility.ShowAlert(true, "Email settings has updated successfully.");
        }
    }

    protected void btnCancelEmail_Click(object sender, EventArgs e)
    {
        this.txtAccountPassword.Text = string.Empty;
        this.txtEmailAccount.Text = string.Empty;
        this.txtSMTP.Text = string.Empty;
    }

    /// <summary>
    /// Updates User Settings
    /// </summary>
    /// <remarks>
    /// Returns True on Success And False on Failure
    /// </remarks>
    /// <returns>True on Success And False on Failure</returns>
    protected bool UpdateUserEmailSetting()
    {
        try
        {
            if (this.txtAccountPassword.Text.Length > 0)
            {
                UserInfo.UpdateUser(Convert.ToInt32(Session["UserID"].ToString()), Convert.ToInt32(Session["CompanyId"]), null, null, Constants.IntNullValue, true, Convert.ToInt32(Session["DISTRIBUTOR_ID"].ToString()), txtEmailAccount.Text, this.txtAccountPassword.Text, txtSMTP.Text);
                return true;
            }
            else
            {
                ltrlEmail.Text = Utility.ShowAlert(false, "Enter Account Password.");
                return false;
            }
        }
        catch (Exception ex)
        {
            ltrlEmail.Text = Utility.ShowAlert(false, ex.Message);
            return false;
        }
    }
    #endregion
    
}
