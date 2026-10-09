using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections;
using System.Configuration;

/// <summary>
/// From To Add,View, Edit Enquiry Pricing
/// </summary>
public partial class Forms_frmProductView : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    QuotationController QoutCtl = new QuotationController();
    EnquiryController EnqCtl = new EnquiryController();
    DataTable dtQuotationDetail;


    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadProductCategory();
            CreateQuotationTable();
            this.GetProduct(Constants.IntNullValue);
            FillRepeater();
        }
    }
    
    private void CreateQuotationTable()
    {
        dtQuotationDetail = new DataTable();
        dtQuotationDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtQuotationDetail.Columns.Add("ORGANIZATION_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT", typeof(string));
        dtQuotationDetail.Columns.Add("QUANTITY", typeof(int));
        this.Session.Add("dtQuotationDetail", dtQuotationDetail);
    }

    private void FillRepeater()
    {
        rEnquiryPrice.DataSource = null;
        rEnquiryPrice.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
                
        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rEnquiryPrice.DataSource = PagedResults;
            rEnquiryPrice.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (PagedResults.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }

    public void GetProduct(int p_Category)
    {
        SkuController mSKUController = new SkuController();
        DataTable dtProduct = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, p_Category, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        this.Session.Add("dtProduct", dtProduct);
    }

    protected void rEnquiryPrice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            this.Session.Add("ProductID", Convert.ToInt64(e.CommandArgument));
            this.Session.Add("Mode", "edit");
            Response.Redirect("frmProductDetail.aspx?LevelID=170" + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        }
    }

    protected void rEnquiryPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                LinkButton btnView = (LinkButton)e.Item.FindControl("btnView");
                LinkButton btnProduct = (LinkButton)e.Item.FindControl("btnProduct");
                btnView.Visible = false;
                btnProduct.Enabled = false;
            }            
        }
    }
                
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEnquiryView.aspx?TopMenu=" + Request.QueryString["TopMenu"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
    
    private void LoadQuotationGrid()
    {
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
    }

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadProductCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        ddlType.DataSource = m_dt;
        ddlType.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlType, m_dt, "REF_ID", "SLASH_DESC");
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == Constants.IntNullValue.ToString())
        {
            this.GetProduct(Constants.IntNullValue);
        }
        else
        {
            this.GetProduct(Convert.ToInt32(ddlType.SelectedValue));
        }
        FillRepeater();
    }

    protected void btnCustomerSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        DataView dv = new DataView(dt);

        if (Session["SKU_NAME"] != null)
        {
            if (Session["SKU_NAME"].ToString() == "DESC")
            {
                dv.Sort = "SKU_NAME DESC";
                Session.Add("SKU_NAME", "ASC");
            }
            else
            {
                dv.Sort = "SKU_NAME ASC";
                Session.Add("SKU_NAME", "DESC");
            }
        }
        else
        {
            dv.Sort = "SKU_NAME ASC";
            Session.Add("SKU_NAME", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtProduct", dt);
        FillRepeater();

    }

    protected void btnCategorySor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        DataView dv = new DataView(dt);

        if (Session["Category"] != null)
        {
            if (Session["Category"].ToString() == "DESC")
            {
                dv.Sort = "Category DESC";
                Session.Add("Category", "ASC");
            }
            else
            {
                dv.Sort = "Category ASC";
                Session.Add("Category", "DESC");
            }
        }
        else
        {
            dv.Sort = "Category ASC";
            Session.Add("Category", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtProduct", dt);
        FillRepeater();

    }

    protected void btnProductIDSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        DataView dv = new DataView(dt);

        if (Session["SKU_ID"] != null)
        {
            if (Session["SKU_ID"].ToString() == "DESC")
            {
                dv.Sort = "SKU_ID DESC";
                Session.Add("SKU_ID", "ASC");
            }
            else
            {
                dv.Sort = "SKU_ID ASC";
                Session.Add("SKU_ID", "DESC");
            }
        }
        else
        {
            dv.Sort = "SKU_ID ASC";
            Session.Add("SKU_ID", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtProduct", dt);
        FillRepeater();

    }

    protected void btnAssignedSuppSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        DataView dv = new DataView(dt);

        if (Session["NoOfSupplier"] != null)
        {
            if (Session["NoOfSupplier"].ToString() == "DESC")
            {
                dv.Sort = "NoOfSupplier DESC";
                Session.Add("NoOfSupplier", "ASC");
            }
            else
            {
                dv.Sort = "NoOfSupplier ASC";
                Session.Add("NoOfSupplier", "DESC");
            }
        }
        else
        {
            dv.Sort = "NoOfSupplier ASC";
            Session.Add("NoOfSupplier", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtProduct", dt);
        FillRepeater();

    }

    protected void btnDateAddedSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        DataView dv = new DataView(dt);

        if (Session["TIME_STAMP"] != null)
        {
            if (Session["TIME_STAMP"].ToString() == "DESC")
            {
                dv.Sort = "TIME_STAMP DESC";
                Session.Add("TIME_STAMP", "ASC");
            }
            else
            {
                dv.Sort = "TIME_STAMP ASC";
                Session.Add("TIME_STAMP", "DESC");
            }
        }
        else
        {
            dv.Sort = "TIME_STAMP ASC";
            Session.Add("TIME_STAMP", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtProduct", dt);
        FillRepeater();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtProduct"];
        
        DataTable dtFilter = new DataTable();
        dtFilter = dt.Clone();
        DataRow[] foundRows = null;

        if (ddlType.SelectedValue == Constants.IntNullValue.ToString())
        {
            foundRows = dt.Select("SKU_NAME  LIKE  '%" + txtFilter.Text + "%'");
        }             
        else
        {
            foundRows = dt.Select("SKU_NAME  LIKE  '%" + txtFilter.Text + "%' AND Category ='" + ddlType.SelectedItem.Text + "'");
        }

        if (foundRows != null)
        {
            for (int i = 0; i < foundRows.Length; i++)
            {
                dtFilter.ImportRow(foundRows[i]);
            }
            Session.Add("dtProduct", dtFilter);
            FillRepeater();
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

    #endregion
}