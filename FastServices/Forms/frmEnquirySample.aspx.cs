using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmEnquirySample : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    OrganizationController OrgCtl = new OrganizationController();
    DataControl dc = new DataControl();
    private static int RowNo;
    DataTable dtEnquiryDetail;

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
            this.LoadDistributor();
            this.LoadCustomer();
            this.LoadCity();
            this.LoadUOM();
            this.LoadSKUDetail();
            this.CreatTable();
            this.GetDocumentNo();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");
            txtLeadTime.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        dtEnquiryDetail = new DataTable();
        dtEnquiryDetail.Columns.Add("SKU_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SKU_Name", typeof(string));
        dtEnquiryDetail.Columns.Add("Quantity", typeof(int));
        dtEnquiryDetail.Columns.Add("UOM", typeof(string));
        dtEnquiryDetail.Columns.Add("UOM_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("PaymentMode", typeof(string));
        dtEnquiryDetail.Columns.Add("Shipment", typeof(string));
        dtEnquiryDetail.Columns.Add("ShipmentMode", typeof(string));
        dtEnquiryDetail.Columns.Add("DelLocation", typeof(string));
        dtEnquiryDetail.Columns.Add("LEAD_TIME", typeof(string));
        dtEnquiryDetail.Columns.Add("PAYMENT_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SHIPMENT_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SHIPMENTMODE_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("DELIVERYLOCATION_ID", typeof(int));

        this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);

    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNo()
    {
        drpDocumentNo.Items.Clear();
        EnquiryController mEnq = new EnquiryController();
        DataTable dt = mEnq.GetEnquiryMaster(Convert.ToInt32(ddlLocation.SelectedValue),Constants.LongNullValue,Convert.ToInt32(Session["UserId"]),Constants.EnquiryCustomer);
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
            ddlLocation.Enabled = true;
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            LoadGird();
            this.ClearAll();
        }
        else
        {
            ddlLocation.Enabled = false;
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
    /// Loads Cities To City Comb
    /// </summary>
    private void LoadCity()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, 949, null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        ddlDelivery.Items.Add(new clsListItems("--Select--", "-1"));
        clsWebFormUtil.FillDropDownList(this.ddlDelivery, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads UOMs To UOM Comb
    /// </summary>
    private void LoadUOM()
    {
        SLASHCodesController CodesCtl = new SLASHCodesController();
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.UOM, null, 1, true);
        ddlUOM.Items.Add(new clsListItems("--Select--", "-1"));
        clsWebFormUtil.FillDropDownList(this.ddlUOM, m_dt, 0, 2);
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        EnquiryController mEnq = new EnquiryController();
        DataTable dt = mEnq.GetEnquiryMaster(Constants.IntNullValue, long.Parse(drpDocumentNo.SelectedValue.ToString()),Constants.IntNullValue,Constants.EnquiryCustomer);
        if (dt.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();

            dtEnquiryDetail = mEnq.GetEnquiryDetail(long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            LoadGird();
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGird()
    {
        int TotalValue = 0;
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        GrdPurchase.DataSource = dtEnquiryDetail;
        GrdPurchase.DataBind();
        foreach (DataRow dr in dtEnquiryDetail.Rows)
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
        ddlDelivery.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[11].Text;
        ddlUOM.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[12].Text;
        txtLeadTime.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[5].Text;
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
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        if (dtEnquiryDetail.Rows.Count > 0)
        {
            dtEnquiryDetail.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.LoadGird();
        }
    }

    /// <summary>
    /// Loads Product Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, 0, 2, true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);


    }
    
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        if (foundRows.Length > 0 && int.Parse(dc.chkNull_0(txtQuantity.Text)) > 0)
        {
            if (btnSave.Text == "Add Product")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtEnquiryDetail.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["UOM"] = ddlUOM.SelectedItem.Text;
                    dr["UOM_ID"] = Convert.ToInt32(ddlUOM.SelectedValue);
                    dr["PaymentMode"] = 0;
                    dr["PAYMENT_ID"] = 0;

                    dr["Shipment"] = 0;
                    dr["SHIPMENT_ID"] = 0;

                    dr["ShipmentMode"] = 0;
                    dr["SHIPMENTMODE_ID"] = 0;

                    dr["DelLocation"] = ddlDelivery.SelectedItem.Text;
                    dr["DELIVERYLOCATION_ID"] = Convert.ToInt32(ddlDelivery.SelectedValue);

                    dr["LEAD_TIME"] = txtLeadTime.Text;

                    dtEnquiryDetail.Rows.Add(dr);
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
                DataRow dr = dtEnquiryDetail.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["Quantity"] = int.Parse(txtQuantity.Text);
                dr["UOM"] = ddlUOM.SelectedItem.Text;
                dr["UOM_ID"] = Convert.ToInt32(ddlUOM.SelectedValue);

                dr["PaymentMode"] = 0;
                dr["PAYMENT_ID"] = 0;

                dr["Shipment"] = 0;
                dr["SHIPMENT_ID"] = 0;

                dr["ShipmentMode"] = 0;
                dr["SHIPMENTMODE_ID"] = 0;

                dr["DelLocation"] = ddlDelivery.SelectedItem.Text;
                dr["DELIVERYLOCATION_ID"] = Convert.ToInt32(ddlDelivery.SelectedValue);

                dr["LEAD_TIME"] = txtLeadTime.Text;
            }

            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.ClearAll();
            this.LoadGird();
            ScriptManager.GetCurrent(Page).SetFocus(txtskuName);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Wrong Product or Quantity Selected');", true);

        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        if (dtEnquiryDetail.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(ddlLocation.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (SaveEnquiry(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
                    dtEnquiryDetail.Rows.Clear();
                    this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
                    this.LoadGird();
                    this.GetDocumentNo();
                    this.ClearAll();
                    txtRemarks.Text = "";
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
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        DataRow[] foundRows = dtEnquiryDetail.Select("SKU_ID  = '" + hfSKUID.Value + "'");
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
        ddlLocation.Enabled = true;

    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool SaveEnquiry(DateTime MWorkDate)
    {
        EnquiryController mController = new EnquiryController();
        DataTable dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        
        if (drpDocumentNo.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            mController.InsertEnquiry(Constants.EnquiryCustomer, Convert.ToInt32(ddlLocation.SelectedValue),MWorkDate,Convert.ToInt32(DrpCustomer.SelectedValue),txtRemarks.Text, Convert.ToInt32(this.Session["UserId"]),Constants.LongNullValue,dtEnquiryDetail);
            return true;
        }
        else
        {
            bool mResult = mController.UpdateEnquiry(Constants.EnquiryCustomer, int.Parse(drpDocumentNo.SelectedValue.ToString()), Convert.ToInt32(ddlLocation.SelectedValue), MWorkDate, Convert.ToInt32(DrpCustomer.SelectedValue), txtRemarks.Text, Convert.ToInt32(this.Session["UserId"]),Constants.Modified,Constants.LongNullValue, dtEnquiryDetail);
            return mResult;
        }
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