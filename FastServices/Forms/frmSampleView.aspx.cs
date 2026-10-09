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
public partial class Forms_frmSampleView : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    QuotationController QoutCtl = new QuotationController();
    EnquiryController EnqCtl = new EnquiryController();
    SLASHCodesController CodesCtl = new SLASHCodesController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (CanView(Request.QueryString["LevelID"].ToString()))
            {
                this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquiryCustomer);
                FillRepeater();
                this.LoadStatus();
            }
            else
            {
                pnEnquiryList.Visible = false;
            }
        }
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

        DataTable dt2 = new DataTable();
        dt2 = (DataTable)Session["dtEnqPrice"];

        DataView view = new DataView(dt2);
        DataTable dt = view.ToTable(true, "ENQUIREY_MASTER_ID", "CUSTOMER","NoOfProducts","DOCUMENT_DATE","Status","Type","LAST_UPDATE");



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

    public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID ,int p_DISTRIBUTOR_ID,int p_USER_ID,int p_TYPE_ID)
    {
        DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(p_ENQUIREY_MASTER_ID, p_DISTRIBUTOR_ID, p_USER_ID, p_TYPE_ID,Constants.IntNullValue,0);
        this.Session.Add("dtEnqPrice", dtEnqPrice);
        return dtEnqPrice;                        
    }

    protected void rEnquiryPrice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            this.Session.Add("EnquiryMasterID", Convert.ToInt64(e.CommandArgument));
            this.Session.Add("Mode", "edit");
            Response.Redirect("frmSampleDetail.aspx?LevelID=169" + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        }
        else if (e.CommandName == "del")
        {
            if (EnqCtl.UpdateEnquiry(Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, false))
            {
                this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
                FillRepeater();
            }
        }
    }

    protected void rEnquiryPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                LinkButton btnView = (LinkButton)e.Item.FindControl("btnView");
                LinkButton btnCustomer = (LinkButton)e.Item.FindControl("btnCustomer");
                btnView.Visible = false;
                btnCustomer.Enabled = false;
            }

            if (CanDelete(Request.QueryString["LevelID"].ToString()))
            {
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                btnDelete.Visible = false;
            }
        }
    }
    
    protected void btnCustomerSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtEnqPrice"];
        DataView dv = new DataView(dt);

        if (Session["Customer"] != null)
        {
            if (Session["Customer"].ToString() == "DESC")
            {
                dv.Sort = "CUSTOMER DESC";
                Session.Add("Customer", "ASC");
            }
            else
            {
                dv.Sort = "CUSTOMER ASC";
                Session.Add("Customer","DESC");
            }
        }
        else
        {
            dv.Sort = "CUSTOMER ASC";
            Session.Add("Customer", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtEnqPrice", dt);
        FillRepeater();

    }

    protected void btnDateSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtEnqPrice"];
        DataView dv = new DataView(dt);

        if (Session["Date"] != null)
        {
            if (Session["Date"].ToString() == "DESC")
            {
                dv.Sort = "DOCUMENT_DATE DESC";
                Session.Add("Date", "ASC");
            }
            else
            {
                dv.Sort = "DOCUMENT_DATE ASC";
                Session.Add("Date", "DESC");
            }
        }
        else
        {
            dv.Sort = "DOCUMENT_DATE ASC";
            Session.Add("Date", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtEnqPrice", dt);
        FillRepeater();

    }

    protected void btnStatusSort_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtEnqPrice"];
        DataView dv = new DataView(dt);

        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "DESC")
            {
                dv.Sort = "Status DESC";
                Session.Add("Status", "ASC");
            }
            else
            {
                dv.Sort = "Status ASC";
                Session.Add("Status", "DESC");
            }
        }
        else
        {
            dv.Sort = "Status ASC";
            Session.Add("Status", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtEnqPrice", dt);
        FillRepeater();

    }

    protected void btnLastUpdate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtEnqPrice"];
        DataView dv = new DataView(dt);

        if (Session["LAST_UPDATE"] != null)
        {
            if (Session["LAST_UPDATE"].ToString() == "DESC")
            {
                dv.Sort = "LAST_UPDATE DESC";
                Session.Add("LAST_UPDATE", "ASC");
            }
            else
            {
                dv.Sort = "LAST_UPDATE ASC";
                Session.Add("LAST_UPDATE", "DESC");
            }
        }
        else
        {
            dv.Sort = "LAST_UPDATE ASC";
            Session.Add("LAST_UPDATE", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtEnqPrice", dt);
        FillRepeater();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquiryCustomer);

        DataTable dtFilter = new DataTable();
        dtFilter = dt.Clone();
        DataRow[] foundRows = dt.Select("CUSTOMER LIKE '%" + txtCustomer.Text + "%'");
        if (foundRows.Length > 0)
        {
            for (int k = 0; k < foundRows.Length; k++)
            {
                dtFilter.ImportRow(foundRows[k]);
            }
        }

        Session.Add("dtEnqPrice", dtFilter);
        FillRepeater(); 
    }

    private void LoadStatus()
    {
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.EnquiryStatus, null, Constants.IntNullValue, true);
        dllEnquiryStatus.Items.Add(new ListItem("All",Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.dllEnquiryStatus, m_dt, "REF_ID", "SLASH_DESC");
        
    }

    protected void dllEnquiryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquiryCustomer);
        if (dllEnquiryStatus.SelectedValue != Constants.IntNullValue.ToString())
        {
            DataTable dtFilter = new DataTable();
            dtFilter = dt.Clone();
            DataRow[] foundRows = dt.Select("Status ='" + dllEnquiryStatus.SelectedItem.Text + "'");
            if (foundRows.Length > 0)
            {
                for (int k = 0; k < foundRows.Length; k++)
                {
                    dtFilter.ImportRow(foundRows[k]);
                }
            }

            Session.Add("dtEnqPrice", dtFilter);
            
        }
        else
        {
            Session.Add("dtEnqPrice", dt);
        }

        FillRepeater();
    }

    protected void btnFilterDate_Click(object sender, EventArgs e)
    {
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquiryCustomer);
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();
        if (hfDateType.Value == "D")
        {
            if (txtDate.Text.Length > 0)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                String datetime = txtDate.Text.Trim();
                dtFrom = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                dtFrom = dtFrom.AddDays(-1);
                dtTo = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                dtTo = dtTo.AddDays(1);
            }
        }
        else if (hfDateType.Value == "M")
        {
            if (txtMonth.Text.Length > 0)
            {
                DateTime dtFromMonth = DateTime.Parse(txtMonth.Text);
                dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

                DateTime dtToMonth = DateTime.Parse(txtMonth.Text);
                dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
                dtTo = dtTo.AddMonths(1).AddDays(-1);
            }
        }
        else if (hfDateType.Value == "Y")
        {
            dtFrom = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), 1, 1);
            dtTo = new DateTime(Convert.ToInt32(this.ddlYear.SelectedValue), 12, 31);
        }

        DataTable dtFilter = new DataTable();
        dtFilter = dt.Clone();
        DataRow[] foundRows = dt.Select("FILTER_DATE >= '" + dtFrom.ToShortDateString() + "' AND FILTER_DATE <= '" + dtTo.ToShortDateString() + "'");
        if (foundRows.Length > 0)
        {
            for (int k = 0; k < foundRows.Length; k++)
            {
                dtFilter.ImportRow(foundRows[k]);
            }
        }

        Session.Add("dtEnqPrice", dtFilter);
        FillRepeater();
    }

    #region Role Rights

    private bool CanView(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
                {
                    if (Convert.ToBoolean(dr["CAN_VIEW"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

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

    #endregion
}