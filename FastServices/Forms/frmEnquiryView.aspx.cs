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
public partial class Forms_frmQutationSupplier : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    QuotationController QoutCtl = new QuotationController();
    EnquiryController EnqCtl = new EnquiryController();
    SLASHCodesController CodesCtl = new SLASHCodesController();

    string strBody = string.Empty;
    string strSMTP = ConfigurationManager.AppSettings["SMTP"].ToString();
    string strSMTPPassword = ConfigurationManager.AppSettings["AccountPassword"].ToString();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(!CanView(Request.QueryString["LevelID"].ToString()))
            {
                this.LoadStatus();
                this.LoadCategory();

                if (Session["RoleID"].ToString() == "1")
                {
                    this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlCategory.SelectedValue));
                }
                else
                {
                    this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlCategory.SelectedValue));
                }
                FillRepeater();                
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
        DataTable dt = view.ToTable(true, "ENQUIREY_MASTER_ID", "CUSTOMER","NoOfProducts","DOCUMENT_DATE","Status","Type","LAST_UPDATE","Category");



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
        DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(p_ENQUIREY_MASTER_ID, p_DISTRIBUTOR_ID, p_USER_ID, p_TYPE_ID,Constants.IntNullValue,Convert.ToInt32(rblCustomerType.SelectedValue));
        this.Session.Add("dtEnqPrice", dtEnqPrice);
        return dtEnqPrice;                        
    }

    protected void rEnquiryPrice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            HiddenField hfStatusID = (HiddenField)e.Item.FindControl("hfStatusID");
            this.Session.Add("EnquiryMasterID", Convert.ToInt64(e.CommandArgument));
            this.Session.Add("Mode", "edit");
            this.Session.Add("EnqStatus", hfStatusID.Value);
            Response.Redirect("frmEnquiryDetail.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=" + Request.QueryString["LevelType"].ToString());
        }
        else if (e.CommandName == "del")
        {
            if (EnqCtl.UpdateEnquiry(Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InActive,false))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Enquiry", "Enquiry No " + e.CommandArgument.ToString() + " deleted");                
                DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(dllEnquiryStatus.SelectedValue),Convert.ToInt32(rblCustomerType.SelectedValue));
                this.Session.Add("dtEnqPrice", dtEnqPrice);                
                FillRepeater();
                ltrlEnquiry.Text = Utility.ShowAlert(true, "Enquiry deleted successfully.");
            }
        }

        else if (e.CommandName == "restore")
        {
            if (EnqCtl.UpdateEnquiry(Convert.ToInt64(e.CommandArgument), Constants.IntNullValue, Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.Modified, true))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Restore", Session["UserName2"].ToString(), "Enquiry", "Enquiry No " + e.CommandArgument.ToString() + " restored.");
                DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(dllEnquiryStatus.SelectedValue),Convert.ToInt32(rblCustomerType.SelectedValue));
                this.Session.Add("dtEnqPrice", dtEnqPrice);
                FillRepeater();

                ltrlEnquiry.Text = Utility.ShowAlert(true, "Enquiry restored successfully.");
            }
        }
    }

    protected void rEnquiryPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hfStatusID = (HiddenField)e.Item.FindControl("hfStatusID");
            LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
            LinkButton btnRestore = (LinkButton)e.Item.FindControl("btnRestore");

            DataRowView drv = (DataRowView)e.Item.DataItem;

            string enqid = drv["ENQUIREY_MASTER_ID"].ToString();

            btnDelete.Attributes.Add("onclick", "return confirm('Are you sure? you are going to delete this Enquiry: " + enqid + "')");
            btnRestore.Attributes.Add("onclick", "return confirm('Are you sure? you are going to restore this Enquiry: " + enqid + "')");

            if (hfStatusID.Value == "Inactive")
            {
                btnDelete.Visible = false;
                btnRestore.Visible = true;
            }
        }
    }
            
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEnquiryView.aspx?TopMenu=" + Request.QueryString["TopMenu"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
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
        DataTable dt = new DataTable();

        if (Session["RoleID"].ToString() == "1")
        {
            dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(ddlCategory.SelectedValue));
        }
        else
        {
            dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlCategory.SelectedValue));
        }

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
        dllEnquiryStatus.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.dllEnquiryStatus, m_dt, "REF_ID", "SLASH_DESC");

    }

    protected void dllEnquiryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dllEnquiryStatus.SelectedValue == "663")
        {
            DataTable dtEnqPrice = new DataTable();
            if (Session["RoleID"].ToString() == "1")
            {
                dtEnqPrice = EnqPriceCtl.GetEnquiryView(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, Convert.ToInt32(dllEnquiryStatus.SelectedValue),Convert.ToInt32(rblCustomerType.SelectedValue));
            }
            else
            {
                dtEnqPrice = EnqPriceCtl.GetEnquiryView(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, Convert.ToInt32(dllEnquiryStatus.SelectedValue), Convert.ToInt32(rblCustomerType.SelectedValue));
            }
             
            this.Session.Add("dtEnqPrice", dtEnqPrice);            
        }
        else
        {
            DataTable dt = new DataTable();
            if (Session["RoleID"].ToString() == "1")
            {
                dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
            }
            else
            {
                dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.IntNullValue);
            }
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
        }

        FillRepeater();
    }

    protected void btnFilterDate_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Session["RoleId"].ToString() == "1")
        {
            dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue);
        }
        else
        {
            dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), Constants.IntNullValue);
        }
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

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        ddlCategory.DataSource = m_dt;
        ddlCategory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlCategory, m_dt, "REF_ID", "SLASH_DESC");        
    }

    protected void rblCustomerType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dllEnquiryStatus_SelectedIndexChanged(null, null);
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