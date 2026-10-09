using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmDesignation : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_Designation_ID;

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
        ltrlDesignation.Text = "";
        if (txtDesignation.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.SaleForce, txtDesignation.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Designation", txtDesignation.Text + " added");

                    ltrlDesignation.Text = Utility.ShowAlert(true, "Designation inserted successfully.");
                }
                else
                {
                    ltrlDesignation.Text = Utility.ShowAlert(false,msg);
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_Designation_ID, "", Constants.SaleForce, txtDesignation.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Designation", txtDesignation.Text + " updated");
                    ltrlDesignation.Text = Utility.ShowAlert(true,"Designation updated successfully.");
                }
                else
                {
                    ltrlDesignation.Text = Utility.ShowAlert(false, msg);
                }
            }

            this.LoadGrid();
            txtDesignation.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            ltrlDesignation.Text = Utility.ShowAlert(false, "Please enter Designation.");
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDesignation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_Designation_ID = int.Parse(gvDesignation.Rows[e.NewEditIndex].Cells[0].Text);
        txtDesignation.Text = gvDesignation.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDesignation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ltrlDesignation.Text = "";
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvDesignation.Rows[e.RowIndex].Cells[0].Text), "", Constants.SaleForce, txtDesignation.Text, 1, false, DateTime.Now);
        if (result == "Record Updated")
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Designation", gvDesignation.Rows[e.RowIndex].Cells[1].Text + " deleted");

            ltrlDesignation.Text = Utility.ShowAlert(true, "Designation deleted successfully.");

            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvDesignation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvDesignation.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.SaleForce, null, Constants.IntNullValue, true);
        gvDesignation.DataSource = dt;
        gvDesignation.DataBind();
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
            gvDesignation.Columns[2].Visible = false;
        }

        if (CanDelete(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvDesignation.Columns[3].Visible = false;
        }
    }
    #endregion
    
}