using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmUOM : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_UOM_ID;

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
        if (txtUOM.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.UOM, txtUOM.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    lblError.Text = "UOM inserted successfully.";
                }
                else
                {
                    lblError.Text = msg;
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_UOM_ID, "", Constants.UOM, txtUOM.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    lblError.Text = "UOM updated successfully.";
                }
                else
                {
                    lblError.Text = msg;
                }
            }

            this.LoadGrid();
            txtUOM.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            lblError.Text = "Please enter UOM";
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvUOM_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_UOM_ID = int.Parse(gvUOM.Rows[e.NewEditIndex].Cells[0].Text);
        txtUOM.Text = gvUOM.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvUOM_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvUOM.Rows[e.RowIndex].Cells[0].Text), "", Constants.UOM, txtUOM.Text, 1, false, DateTime.Now);
        this.LoadGrid();
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvUOM_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvUOM.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null,Constants.UOM, null, Constants.IntNullValue, true);
        gvUOM.DataSource = dt;
        gvUOM.DataBind();
        lblError.Text = "";
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
            gvUOM.Columns[2].Visible = false;
        }

        if (CanDelete(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvUOM.Columns[3].Visible = false;
        }
    }
    #endregion
    
}