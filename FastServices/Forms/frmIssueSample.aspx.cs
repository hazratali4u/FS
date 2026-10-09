using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmIssueSampe : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    OrganizationController OrgCtl = new OrganizationController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable PurchaseSKU;

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ApplyRoles();
            this.LoadPrincipal();
            this.LoadDistributor();
            this.LoadCustomer();
            this.LoadBatchNo();
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
        }
    }
    
    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        PurchaseSKU = new DataTable();
        PurchaseSKU.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKU.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKU.Columns.Add("PRINCIPAL_ID", typeof(int));
        PurchaseSKU.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKU.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKU.Columns.Add("Quantity", typeof(int));
        this.Session.Add("PurchaseSKU", PurchaseSKU);  
    
    }
        
    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(2, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
        drpDocumentNo.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNo, dt, 0, 0);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTable();
            drpDistributor.Enabled = true;
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
        }
        else
        {
            drpDistributor.Enabled = false;
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }

    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
        clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2,true);
    }
    
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomer.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            ddlSupplier.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            
            PurchaseSKU = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
        }
    }
        
    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        GrdPurchase.DataSource = PurchaseSKU;
        GrdPurchase.DataBind();
        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());

        }
        txtTotalQuantity.Text = TotalValue.ToString(); 
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        txtskuName.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[1].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;
        hfBatchNo.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        //ddlSupplier.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[4].Text;
        txtQuantity.Focus();
        btnSave.Text = "Update Product";
        this.LoadBatchComb();

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            PurchaseSKU.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.LoadGird();
        }
    }

    /// <summary>
    /// Loads Product Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        DataTable Dtsku_Price = PController.SelectDataPrice(Convert.ToInt32(ddlSupplier.SelectedValue), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
        clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, "SKU_ID", "SKU_NAME", true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }

    /// <summary>
    /// Loads Batch Nos
    /// </summary>
    private void LoadBatchNo()
    {
        DataTable dtBatchNo = PController.GetBatchNo();
        clsWebFormUtil.FillListBox(this.lbBatch, dtBatchNo, 0, 1);
    }
        
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (hfBatchNo.Value.Length > 0)
        {
            DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
            PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + hfSKUID.Value + "'");
            if (foundRows.Length > 0 && int.Parse(dc.chkNull_0(txtQuantity.Text)) > 0)
            {
                if (btnSave.Text == "Add Product")
                {
                    if (CheckDublicateSKU())
                    {
                        DataRow dr = PurchaseSKU.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["PRINCIPAL_ID"] = Convert.ToInt32(ddlSupplier.SelectedValue);
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["BATCH_NO"] = hfBatchNo.Value;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                        PurchaseSKU.Rows.Add(dr);
                    }
                    else
                    {
                        this.LoadBatchComb();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + "" + " Already Exists ');", true);
                        txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                        return;
                    }
                }
                else if (btnSave.Text == "Update Product")
                {
                    DataRow dr = PurchaseSKU.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["PRINCIPAL_ID"] = foundRows[0]["PRINCIPAL_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = hfBatchNo.Value;
                    dr["Quantity"] = int.Parse(txtQuantity.Text);

                }
                this.Session.Add("PurchaseSKU", PurchaseSKU);
                this.ClearAll();
                this.LoadGird();
                ScriptManager.GetCurrent(Page).SetFocus(txtskuName);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Product or Quantity Selected');", true);

            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + "" + " No Batch No Selected ');", true);
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        if (PurchaseSKU.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchase(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
                    PurchaseSKU.Rows.Clear();
                    this.Session.Add("PurchaseSKU", PurchaseSKU);
                    this.LoadGird();
                    this.GetDocumentNo();
                    this.ClearAll();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Location');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' At least one Product must enter');", true);
        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.CreatTable();
        this.LoadGird();
        this.ClearAll();
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + hfSKUID.Value + "' AND BATCH_NO ='" + hfBatchNo.Value + "'" + " AND PRINCIPAL_ID = '" + ddlSupplier.SelectedValue + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtskuName.Text = "";
        txtQuantity.Text = "";
        btnSave.Text = "Add Product";
        drpDistributor.Enabled = true;
    }

    protected void DrpBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (DrpBatch.SelectedValue == "All")
        //{
        //    LoadSKUDetail();
        //}
        //else
        //{

        //}
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate)
    {
        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];

        foreach (DataRow dr in PurchaseSKU.Rows)
        {
            PhaysicalStockController mController2 = new PhaysicalStockController();
            DataTable dtstock = mController2.SelectSKUClosingStock(int.Parse(drpDistributor.SelectedValue), Convert.ToInt32(dr["PRINCIPAL_ID"]), int.Parse(dr["SKU_ID"].ToString()), dr["BATCH_NO"].ToString(), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            
            if (dtstock.Rows.Count > 0)
            {
                if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["Quantity"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + dr["SKU_Name"].ToString() + " Current Stock is " + dtstock.Rows[0][0].ToString() + "');", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' + " + dr["SKU_Name"].ToString() + "No Stock Found');", true);
                return false;
            }
        }

        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), "", Constants.Document_Invoice
            , MWorkDate, int.Parse(DrpCustomer.SelectedValue.ToString()), Convert.ToInt32(ddlSupplier.SelectedValue), 0, false, dtPurchaseDetail, 0, null, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(ddlSupplier.SelectedValue));
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), "", Constants.Document_Invoice
            , MWorkDate, int.Parse(DrpCustomer.SelectedValue.ToString()), Convert.ToInt32(ddlSupplier.SelectedValue), 0, false, dtPurchaseDetail, 0, null, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(ddlSupplier.SelectedValue));
            return mResult;
        }
    }

    private void LoadBatchComb()
    {
        ddlBatch.Items.Clear();
        for (int i = 0; i < lbBatch.Items.Count; i++)
        {
            if (lbBatch.Items[i].Value == hfSKUID.Value)
            {
                ListItem li = new ListItem();
                li.Value = lbBatch.Items[i].Value;
                li.Text = lbBatch.Items[i].Text;
                ddlBatch.Items.Add(li);
            }
        }
    }

    protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
    }

    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 1);
        clsWebFormUtil.FillDropDownList(this.ddlSupplier, m_dt, 0, 1);
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

    private void ApplyRoles()
    {
        if (CanEdit(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnSaveDocument.Visible = false;
        }
    }
    #endregion
}