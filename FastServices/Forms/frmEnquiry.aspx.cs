using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Purchase, TranferOut, Purchase Return, TranferIn And Damage
/// </summary>
public partial class Forms_frmEnquiry : System.Web.UI.Page
{
    SKUPriceDetailController PController = new SKUPriceDetailController();
    OrganizationController OrgCtl = new OrganizationController();
    PeopleController PplCtl = new PeopleController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
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
            this.LoadOriginator();
            this.LoadCity();
            this.LoadCategory();
            this.LoadSKUDetail();
            this.CreatTable();
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
        dtEnquiryDetail.Columns.Add("DOCUMENT_DATE",typeof(DateTime));
        dtEnquiryDetail.Columns.Add("ENQUIREY_DETAIL_ID2", typeof(long));
        this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);

    }
    
    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        if (Session["RoleID"].ToString() == "1")
        {
            DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
        }
        else
        {
            DataTable m_dt = OrgCtl.GetOrganization(Convert.ToInt32(Session["UserID"]), 0);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
        }
    }

    /// <summary>
    /// Loads Cities To City Comb
    /// </summary>
    private void LoadCity()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, 949, null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        //ddlDelivery.Items.Add(new clsListItems("--Select--", "-1"));
        //clsWebFormUtil.FillDropDownList(this.ddlDelivery, dt, 0, 4, true);
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
    private void LoadGird()
    {
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        GrdPurchase.DataSource = dtEnquiryDetail;
        GrdPurchase.DataBind();       
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdPurchase_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        ddlProduct.SelectedValue = GrdPurchase.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = GrdPurchase.Rows[e.NewEditIndex].Cells[2].Text;        
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

        if(dtEnquiryDetail.Rows.Count <= 0)
        {
            ddlCategotry.Enabled = true;
        }
    }

    /// <summary>
    /// Loads Product Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, Convert.ToInt32(ddlCategotry.SelectedValue), Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlProduct, Dtsku_Price, 0, 2,true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);

    }
    
    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {        
        ltrlEnquiry.Text = "";

        if (ddlProduct.Items.Count > 0 && int.Parse(dc.chkNull_0(txtQuantity.Text)) > 0)
        {
            dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];

            if (btnSave.Text == "Add Product")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtEnquiryDetail.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlProduct.SelectedValue);
                    dr["SKU_Name"] = ddlProduct.SelectedItem.Text;
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["DOCUMENT_DATE"] = System.DateTime.Now.ToShortDateString();
                    dr["ENQUIREY_DETAIL_ID2"] = Constants.LongNullValue;
                    dtEnquiryDetail.Rows.Add(dr);
                }
                else
                {
                    ltrlEnquiry.Text = Utility.ShowAlert(false, "This product already exists");                    
                    return;
                }
            }
            else if (btnSave.Text == "Update Product")
            {
                DataRow dr = dtEnquiryDetail.Rows[RowNo];
                dr["SKU_ID"] = Convert.ToInt32(ddlProduct.SelectedValue);
                dr["SKU_Name"] = ddlProduct.SelectedItem.Text;
                dr["Quantity"] = int.Parse(txtQuantity.Text);
            }

            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.ClearAll();
            this.LoadGird();
            ScriptManager.GetCurrent(Page).SetFocus(txtQuantity);
            ddlCategotry.Enabled = false;
        }
        else
        {
            ltrlEnquiry.Text = Utility.ShowAlert(false, "Wrong Product or Quantity Selected.");
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        if (CustomerCategoryEnquiryExist())
        {
            ModalPopupExtender.Show();
        }
        else
        {
            ltrlEnquiry.Text = "";
            dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
            if (dtEnquiryDetail.Rows.Count > 0)
            {
                DistributorController mDayClose = new DistributorController();
                SaveEnquiry(System.DateTime.Now);
            }
            else
            {
                ltrlEnquiry.Text = Utility.ShowAlert(false, "At least one Product must enter.");
            }
        }
    }

    protected void btnContinue_Click(object sender, EventArgs e)
    {
        ltrlEnquiry.Text = "";
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        if (dtEnquiryDetail.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            SaveEnquiry(System.DateTime.Now);
        }
        else
        {
            ltrlEnquiry.Text = Utility.ShowAlert(false, "At least one Product must enter.");
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
        //FIS
        if (Configuration.Company == 1)
        {
            return true;
        }

        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        
        DataRow[] foundRows = dtEnquiryDetail.Select("SKU_ID  = '" + ddlProduct.SelectedValue + "' AND DOCUMENT_DATE = '" + System.DateTime.Now.ToShortDateString() + "'");
        
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
        txtQuantity.Text = "";
        btnSave.Text = "Add Product";
        ddlLocation.Enabled = true;

    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private void SaveEnquiry(DateTime MWorkDate)
    {        
        long EnquiryMasterID = Constants.LongNullValue;
        EnquiryController mController = new EnquiryController();
        DataTable dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];

        EnquiryMasterID = mController.InsertEnquiry(Convert.ToInt32(ddlCategotry.SelectedValue), Convert.ToInt32(ddlLocation.SelectedValue), MWorkDate, Convert.ToInt32(DrpCustomer.SelectedValue), txtRemarks.Text, Convert.ToInt32(this.Session["UserId"]), Convert.ToInt64(ddlOriginator.SelectedValue), dtEnquiryDetail);

        if (EnquiryMasterID != Constants.LongNullValue)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Enquiry", "Enquiry for " + ddlCategotry.SelectedItem.Text + " added");
            this.Session["dtEnquiryDetail"] = null;
            this.LoadGird();
            this.ClearAll();
            txtRemarks.Text = "";
            Session.Add("IsSaved", "Yes");
            this.Session.Add("EnqStatus", "Active");
            this.Session.Add("EnquiryMasterID", EnquiryMasterID);
            this.Session.Add("Mode", "edit");
            Response.Redirect("frmEnquiryDetail.aspx?LevelID=11&LevelType=3", false);            
        }
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOriginator();
    }

    private void LoadOriginator()
    {
        ddlOriginator.Items.Clear();
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, Convert.ToInt64(DrpCustomer.SelectedValue), null, null);
        ddlOriginator.Items.Add(new clsListItems("---Select---", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlOriginator, dtPeople, "PEOPLE_ID", "FullName");
    }

    private void LoadCategory()
    {
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        clsWebFormUtil.FillDropDownList(this.ddlCategotry, m_dt, "REF_ID", "SLASH_DESC");
    }

    protected void ddlCategotry_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSKUDetail();
    }

    private bool CustomerCategoryEnquiryExist()
    {
        EnquiryPriceController EnqCtl = new EnquiryPriceController();
        DataTable dtEnq = EnqCtl.GetCustomerCategoryEnquiry(Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToInt32(ddlCategotry.SelectedValue), Convert.ToInt64(DrpCustomer.SelectedValue));
        if (dtEnq.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
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