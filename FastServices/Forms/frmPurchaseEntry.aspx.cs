using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmPurchaseEntry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    OrganizationController OrgCtl = new OrganizationController();
    DataControl dc = new DataControl();
    private static int RowNo;
    private static int PrivouseQty, FreePrivousQty;
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
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            FSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
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
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(1, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), 0);
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
            drpPrincipal.Enabled = true;
            drpDistributor.Enabled = true;
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            LoadGird();
            this.ClearAll();
        }
        else
        {
            drpPrincipal.Enabled = false;
            drpDistributor.Enabled = false;
            txtBuiltyNo.Text = "";
            txtDocumentNo.Text = "";
            this.LoadDocumentDetail();
            this.LoadSKUDetail();
        }
    }
    
    /// <summary>
    /// Loads Principals To Principal Comb
    /// </summary>
    private void LoadPrincipal()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 1);
        clsWebFormUtil.FillDropDownList(this.drpPrincipal, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpPrincipal_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadSKUDetail();
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
            drpDistributor.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();
            drpPrincipal.SelectedValue = dt.Rows[0]["SOLD_FROM"].ToString();
            txtDocumentNo.Text = dt.Rows[0][2].ToString();
            txtBuiltyNo.Text = dt.Rows[0]["BUILTY_NO"].ToString();

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

        if (PurchaseSKU.Rows.Count <= 0)
        {
            drpPrincipal.Enabled = true;
        }
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
        PrivouseQty = int.Parse(GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text);
        txtBatchNo.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;","");
        hfSKUID.Value = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Focus();
        btnSave.Text = "Update Product";

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
        if (drpPrincipal.Items.Count > 0)
        {
            DataTable Dtsku_Price = PController.SelectDataPrice(int.Parse(drpPrincipal.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue, 1, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));
            clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, 0, 2, true);
            this.Session.Add("Dtsku_Price", Dtsku_Price);
        }
    }
        
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        drpPrincipal.Enabled = false;
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKU = (DataTable)this.Session["PurchaseSKU"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        if (foundRows.Length > 0)
        {
            if (btnSave.Text == "Add Product")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = PurchaseSKU.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    if (txtBatchNo.Text.Length > 0)
                    {
                        dr["BATCH_NO"] = txtBatchNo.Text;
                    }
                    else
                    {
                        dr["BATCH_NO"] = "N/A";
                    }
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    PurchaseSKU.Rows.Add(dr);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + "" + " Already Exists ');", true);
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSave.Text == "Update Product")
            {
                DataRow dr = PurchaseSKU.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                if (txtBatchNo.Text.Length > 0)
                    {
                        dr["BATCH_NO"] = txtBatchNo.Text;
                    }
                    else
                    {
                        dr["BATCH_NO"] = "N/A";
                    }
                dr["Quantity"] = int.Parse(txtQuantity.Text);
            }
            this.Session.Add("PurchaseSKU", PurchaseSKU);
            this.ClearAll();
            this.LoadGird();
            ScriptManager.GetCurrent(Page).SetFocus(txtskuName);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Product Select');", true);

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
                    txtBuiltyNo.Text = "";
                    txtDocumentNo.Text = "";
                    txtDocumentNo.Text = "";
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
        txtDocumentNo.Text = "";
        txtBuiltyNo.Text = "";
        txtDocumentNo.Text = ""; 
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
        DataRow[] foundRows = PurchaseSKU.Select("SKU_ID  = '" + hfSKUID.Value + "' AND BATCH_NO = '" + txtBatchNo.Text + "'");
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
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchase(DateTime MWorkDate)
    {
        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKU"];

        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, 1
            , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), 0, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNo.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), txtDocumentNo.Text, 1
            , MWorkDate, int.Parse(drpDistributor.SelectedValue.ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()), 0, false, dtPurchaseDetail, 0, txtBuiltyNo.Text, int.Parse(this.Session["UserId"].ToString()), int.Parse(drpPrincipal.SelectedValue.ToString()));
            return mResult;
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
        PrivouseQty = 0;
        FreePrivousQty = 0;
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