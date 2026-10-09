using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit, Delete SKU
/// </summary>
public partial class Forms_frmSKUData : System.Web.UI.Page
{

    SkuController mController = new SkuController();
    DataControl dc = new DataControl();
    SkuHierarchyController mHer_Controller = new SkuHierarchyController();
    OrganizationController OrgCtl = new OrganizationController();
    private DataTable m_dt, m_SKUDt;
    private static int m_sku_id;

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplyRoles();
            this.LoadProductCategory();
            Populate_drpSKUCategory();
            LoadData();
            LoadGrid();
        }
    }

    /// <summary>
    /// Loads Categories To Category Combo
    /// </summary>
    private void Populate_drpSKUCategory()
    {
        //if (ddskudivision.Items.Count > 0)
        //{
        //    if (ddskudivision.Items.Count   > 0)
        //    {
        //        m_dt = mHer_Controller.SelectSkuHierarchy(Constants.SKUCategory, Constants.IntNullValue, int.Parse(ddskudivision.SelectedValue.ToString()), null, null, true, int.Parse(this.Session["CompanyId"].ToString()));
        //        clsWebFormUtil.FillDropDownList(this.ddskucategory, m_dt, 0, 3, true);
        //    }
        //}
        //else
        //{
        //    ddskucategory.Items.Clear();   
        //}
    }

    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        SkuController mSKUController = new SkuController();
        m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, int.Parse(ddskucategory.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.Session.Add("m_SKUDt", m_SKUDt);
    }

    /// <summary>
    /// Loads Data From Session To Grid
    /// </summary>
    private void LoadGrid()
    {
        m_SKUDt = (DataTable)this.Session["m_SKUDt"];

        switch (ddSearchType.SelectedIndex)
        {

            case 1:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                m_SKUDt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;            
            default:
                m_SKUDt.DefaultView.RowFilter = "SKU_NAME" + " like '%" + "" + "%'";
                break;
        }
        grdSKUData.DataSource = m_SKUDt.DefaultView;
        grdSKUData.DataBind();
    }

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadProductCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        ddskucategory.DataSource = m_dt;
        clsWebFormUtil.FillDropDownList(ddskucategory, m_dt, "REF_ID", "SLASH_DESC");
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ltrlProduct.Text = "";
        this.Populate_drpSKUCategory();
        ddskucategory.SelectedValue = grdSKUData.Rows[e.NewEditIndex].Cells[1].Text;
        m_sku_id = int.Parse(grdSKUData.Rows[e.NewEditIndex].Cells[2].Text);
        txtCode.Text = grdSKUData.Rows[e.NewEditIndex].Cells[4].Text.Replace("&nbsp;", "");
        txtskuname.Text = grdSKUData.Rows[e.NewEditIndex].Cells[5].Text;
        btnSave.Text = "Update";
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void grdSKUData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ltrlProduct.Text = "";
        bool IsExemted = true;
        string result = mController.UpdateSKUS(IsExemted, false, Constants.CharNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
        Constants.DecimalNullValue, Constants.DecimalNullValue, Constants.ShortNullValue, int.Parse(grdSKUData.Rows[e.RowIndex].Cells[2].Text), null, null, null, null, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

        if (result == "Record Updated")
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Product", grdSKUData.Rows[e.RowIndex].Cells[5].Text + " deleted");

            this.LoadData();
            this.LoadGrid();
        }
    }

    /// <summary>
    /// Sets PageIndex Of SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewPageEventArgs</param>
    protected void grdSKUData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ltrlProduct.Text = "";
        this.grdSKUData.PageIndex = e.NewPageIndex;
        this.LoadGrid();
    }

    protected void grdSKUData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                e.Row.Cells[6].Enabled = false;
            }

            if (CanDelete(Request.QueryString["LevelID"].ToString()))
            {
                e.Row.Cells[7].Enabled = false;
            }
        }
    }

    /// <summary>
    /// Save Or Updates an SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool IsExemted = true;

        if (txtskuname.Text.Length <= 0)
        {
            ltrlProduct.Text = Utility.ShowAlert(false,"Must Enter Product Name.");
            return;
        }
        if (btnSave.Text == "Save")
        {
            mController.InsertSKUS(IsExemted, true, 'E', 1, 1, int.Parse(ddskucategory.SelectedValue.ToString()),
            1, 1, 0, 0, 1, txtCode.Text, txtskuname.Text, null, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Product", txtskuname.Text + " added");

            ltrlProduct.Text = Utility.ShowAlert(true, "Product added successfully.");
            this.CLearAll();
        }
        else if (btnSave.Text == "Update")
        {
            mController.UpdateSKUS(IsExemted, true, 'E', 1, 1, int.Parse(ddskucategory.SelectedValue.ToString()),
                1, 1, 0, 0, 1, m_sku_id, txtCode.Text, txtskuname.Text, null, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", txtskuname.Text + " added");

            this.CLearAll();
        }
    }

    /// <summary>
    /// Filters SKU From SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ltrlProduct.Text = "";
        this.LoadGrid();
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void CLearAll()
    {
        txtskuname.Text = "";
        btnSave.Text = "Save";
        this.LoadData();
        this.LoadGrid();
    }
    
    /// <summary>
    /// Loads Brands To Brand Combo And SKUS To SKU Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddskucategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        ltrlProduct.Text = "";
        this.LoadData();
        this.LoadGrid();
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
        }
    }
    #endregion
}
