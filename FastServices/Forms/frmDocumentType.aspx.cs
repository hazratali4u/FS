using System;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI.WebControls;
using System.Data;

public partial class Forms_frmDocumentType : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_DocType_ID;

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
        if (txtDocumentType.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                string msg = CodesCtl.InsertSlashCodes("", Constants.DocType, txtDocumentType.Text, 1, true);
                if (msg == "Record Inserted")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Document Type", txtDocumentType.Text + " added");
                    ltrlDocumentType.Text = Utility.ShowAlert(true, "Document Type inserted successfully.");
                }
                else
                {
                    ltrlDocumentType.Text = Utility.ShowAlert(false, msg);
                }
            }
            else
            {
                string msg = "";
                msg = CodesCtl.UpdateSlashCodes(m_DocType_ID, "", Constants.DocType, txtDocumentType.Text, 1, true, DateTime.Now);
                if (msg == "Record Updated")
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Document Type", txtDocumentType.Text + " updated");
                    ltrlDocumentType.Text = Utility.ShowAlert(true, "Document Type updated successfully.");
                }
                else
                {
                    ltrlDocumentType.Text = Utility.ShowAlert(false, msg);
                }
            }

            this.LoadGrid();
            txtDocumentType.Text = "";
            btnSave.Text = "Save";
        }
        else
        {
            ltrlDocumentType.Text = Utility.ShowAlert(false, "Please enter Document Type.");
        }
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDocumentType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        m_DocType_ID = int.Parse(gvDocumentType.Rows[e.NewEditIndex].Cells[0].Text);
        txtDocumentType.Text = gvDocumentType.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", "");
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvDocumentType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string result = CodesCtl.UpdateSlashCodes(Convert.ToInt32(gvDocumentType.Rows[e.RowIndex].Cells[0].Text), "", Constants.DocType, txtDocumentType.Text, 1, false, DateTime.Now);

        if (result == "Record Updated")
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Document Type", gvDocumentType.Rows[e.RowIndex].Cells[1].Text + " deleted");

            ltrlDocumentType.Text = Utility.ShowAlert(true, "Document Type deleted successfully.");
        }

        this.LoadGrid();
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void gvDocumentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gvDocumentType.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    private void LoadGrid()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.DocType, null, Constants.IntNullValue, true);
        gvDocumentType.DataSource = dt;
        gvDocumentType.DataBind();
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
            gvDocumentType.Columns[2].Visible = false;
        }

        if (CanDelete(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            gvDocumentType.Columns[3].Visible = false;
        }
    }
    #endregion
}