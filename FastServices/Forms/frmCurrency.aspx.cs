using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmCurrency : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_Currency_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplyRoles();
            this.LoadGrid();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCurrency.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.Currency, txtCurrency.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Currency", txtCurrency.Text + " added");

                    ltrlCurrency.Text = Utility.ShowAlert(true, "Currency inserted successfully.");
                }
                else
                {
                    ltrlCurrency.Text = Utility.ShowAlert(false, msg);
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_Currency_ID, "", Constants.Currency, txtCurrency.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Currency", txtCurrency.Text + " updated");
                    ltrlCurrency.Text = Utility.ShowAlert(true, "Currency updated successfully.");
                }
                else
                {
                    ltrlCurrency.Text = Utility.ShowAlert(false, msg);
                }
            }

            this.LoadGrid();
            txtCurrency.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            ltrlCurrency.Text = Utility.ShowAlert(false, "Please enter Currency.");
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvCurrency_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_Currency_ID = int.Parse(gvCurrency.Rows[e.NewEditIndex].Cells[0].Text);
        txtCurrency.Text = gvCurrency.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvCurrency_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvCurrency.Rows[e.RowIndex].Cells[0].Text) , "", Constants.Currency, txtCurrency.Text, 1, false, DateTime.Now);
        if (result == "Record Updated")
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Currency", gvCurrency.Rows[e.RowIndex].Cells[1].Text + " deleted");
            ltrlCurrency.Text = Utility.ShowAlert(true, "Currency deleted successfully.");
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvCurrency.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.Currency, null, Constants.IntNullValue, true);
        gvCurrency.DataSource = dt;
        gvCurrency.DataBind();
    }

    #region Role Rights

    private bool CanEdit(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_EDIT"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private bool CanDelete(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_DELETE"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private void ApplyRoles()
    {
        if (CanEdit(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvCurrency.Columns[2].Visible = false;
        }

        if (CanDelete(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvCurrency.Columns[3].Visible = false;
        }
    }
    #endregion
}