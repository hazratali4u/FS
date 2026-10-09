using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From To Add,View, Edit Enquiry Pricing
/// </summary>
public partial class Forms_frmEnquiryPricing : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
    EnquiryController EnqCtl = new EnquiryController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadCurrency();
            this.LoadStatus();
            this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
            FillRepeater();
        }
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvMain.Rows.Count > 0)
            {
                bool flag = false;
                foreach (GridViewRow gvr in gvMain.Rows)
                {
                    GridView gvSupplier = gvr.FindControl("gvSupplier") as GridView;

                    foreach (GridViewRow gvrSup in gvSupplier.Rows)
                    {
                        CheckBox cbIsQuotation = gvrSup.FindControl("cbIsQuotation") as CheckBox;
                        decimal Rate = 0;
                        decimal RateCustomer = 0;
                        TextBox txtRate = gvrSup.FindControl("txtRate") as TextBox;
                        TextBox txtRateCustomer = gvrSup.FindControl("txtRateCustomer") as TextBox;
                        TextBox txtRemarks = gvrSup.FindControl("txtRemarks") as TextBox;
                        DropDownList ddlCurrency = gvrSup.FindControl("ddlCurrency") as DropDownList;

                        if (txtRate.Text.Length > 0)
                        {
                            Rate = Convert.ToDecimal(txtRate.Text);
                        }

                        if (txtRateCustomer.Text.Length > 0)
                        {
                            RateCustomer = Convert.ToDecimal(txtRateCustomer.Text);
                        }

                        flag = EnqPriceCtl.InsertEnquiryPrice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(gvr.Cells[0].Text), Convert.ToInt32(gvrSup.Cells[0].Text), Rate, RateCustomer, "", txtRemarks.Text, cbIsQuotation.Checked, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlCurrency.SelectedValue));
                    }
                }

                if (flag)
                {
                    EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.QuotationPrepared, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Enquiry Updated Successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error Occured.')", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
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
        DataTable dt = view.ToTable(true, "ENQUIREY_MASTER_ID", "CUSTOMER", "NoOfProducts", "DOCUMENT_DATE", "Status", "Type", "LAST_UPDATE");



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

    protected void rEnquiryPrice_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            EditEnquiry(Convert.ToInt64(e.CommandArgument));
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

    private void EditEnquiry(long p_EnquiryID)
    {
        EnquiryController EnqCtl = new EnquiryController();
        DataTable dtEnquiry = EnqCtl.GetEnquiryMaster(Constants.IntNullValue, p_EnquiryID, Constants.IntNullValue,Constants.EnquirySupplier);
        hfEnquiryID.Value = p_EnquiryID.ToString();
        if (dtEnquiry.Rows.Count > 0)
        {
            DataTable dtPriceEnq = this.GetEnquiry(p_EnquiryID, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
            
            gvMain.DataSource = dtPriceEnq;
            gvMain.DataBind();

            pnlAddEnquiry.Visible = true;
            pnEnquiryList.Visible = false;
        }
    }

    protected void rEnquiryPrice_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnView = e.Item.FindControl("btnView") as LinkButton;
            LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
            LinkButton btnCustomer = e.Item.FindControl("btnCustomer") as LinkButton;

            if (CanEdit(Request.QueryString["LevelId"].ToString()))
            {
                btnView.Visible = false;
                btnCustomer.Enabled = false;
            }

            if (CanDelete(Request.QueryString["LevelId"].ToString()))
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmEnquiryPricing.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }
    
    /// <summary>
    /// Loads Assigned Supplier To Product To Assigned Supplier Combo
    /// </summary>
    private DataTable GetAssignSupplier(int ProductID)
    {
        DataTable dt = EnqPriceCtl.GetProductSupplier(ProductID, Convert.ToInt64(hfEnquiryID.Value));

        if (dt.Rows.Count > 0)
        {
            lblEnquiry.Text = dt.Rows[0]["ENQUIREY_MASTER_ID"].ToString();
            lblCustomer.Text = dt.Rows[0]["CustomerName"].ToString();
        }
        return dt;
    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvSupplier = (GridView)e.Row.FindControl("gvSupplier");
            {
                int a = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dtSuppler = GetAssignSupplier(a);
                if (dtSuppler != null)
                {
                    gvSupplier.DataSource = dtSuppler;
                    gvSupplier.DataBind();
                }
            }
        }
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtCurrency = (DataTable)Session["dtCurrency"];
            DropDownList ddlCurrency = e.Row.FindControl("ddlCurrency") as DropDownList;
            clsWebFormUtil.FillDropDownList(ddlCurrency, dtCurrency, 0, 2);

            CheckBox cbIsQuotation = e.Row.FindControl("cbIsQuotation") as CheckBox;

            if (cbIsQuotation.Checked)
            {
                ddlCurrency.SelectedValue = e.Row.Cells[8].Text;
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
                Session.Add("Customer", "DESC");
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
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);

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
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
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
        DataTable dt = this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
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

    public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, int p_USER_ID, int p_TYPE_ID)
    {
        DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(p_ENQUIREY_MASTER_ID, p_DISTRIBUTOR_ID, p_USER_ID, p_TYPE_ID,Constants.IntNullValue,0);
        this.Session.Add("dtEnqPrice", dtEnqPrice);
        return dtEnqPrice;
    }

    /// <summary>
    /// Loads Currency To Currency Comb
    /// </summary>
    private void LoadCurrency()
    {
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.Currency, null, 1, true);
        this.Session.Add("dtCurrency", m_dt);
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

    #endregion
}