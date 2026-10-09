using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmDepartment : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_Department_ID;

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
        if (txtDepartment.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.Department, txtDepartment.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Department", txtDepartment.Text + " added");

                    ltrlDepartment.Text = Utility.ShowAlert(true, "Department inserted successfully.");
                }
                else
                {
                    ltrlDepartment.Text = Utility.ShowAlert(false, msg);
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_Department_ID, "", Constants.Department, txtDepartment.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Department", txtDepartment.Text + " updated");
                    ltrlDepartment.Text = Utility.ShowAlert(true, "Department updated successfully.");
                }
                else
                {
                    ltrlDepartment.Text = Utility.ShowAlert(false, msg);
                }
            }

            this.LoadGrid();
            txtDepartment.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            ltrlDepartment.Text = Utility.ShowAlert(false, "Please enter Department.");
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDepartment_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_Department_ID = int.Parse(gvDepartment.Rows[e.NewEditIndex].Cells[0].Text);
        txtDepartment.Text = gvDepartment.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDepartment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvDepartment.Rows[e.RowIndex].Cells[0].Text), "", Constants.Department, txtDepartment.Text, 1, false, DateTime.Now);

        if (result == "Record Updated")
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Deleted", Session["UserName2"].ToString(), "Department", gvDepartment.Rows[e.RowIndex].Cells[1].Text + " deleted");
            ltrlDepartment.Text = Utility.ShowAlert(true, "Department deleted successfully.");
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvDepartment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvDepartment.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }
    
    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.Department, null, Constants.IntNullValue, true);
        gvDepartment.DataSource = dt;
        gvDepartment.DataBind();
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
            gvDepartment.Columns[2].Visible = false;
        }

        if (CanDelete(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvDepartment.Columns[3].Visible = false;
        }
    }
    #endregion
}