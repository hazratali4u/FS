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
    DataTable dtQuotationDetail;
    DataTable dtEmailCc;

    string strBody = string.Empty;
    string strSubject = string.Empty;
    string strSMTP = ConfigurationManager.AppSettings["SMTP"].ToString();
    string strSMTPPassword = ConfigurationManager.AppSettings["AccountPassword"].ToString();
    string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailAccount"].ToString();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CreateQuotationTable();
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
                ArrayList alSupplierIDs = SupplierIDs();
                dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
                dtEmailCc = (DataTable)Session["dtEmailCc"];

                if (alSupplierIDs.Count > 0)
                {
                    if (dtEmailCc.Rows.Count > 0)
                    {
                        bool flag = false;

                        for (int i = 0; i < alSupplierIDs.Count; i++)
                        {
                            DataTable dtQuotation = new DataTable();
                            dtQuotation = dtQuotationDetail.Clone();
                            DataRow[] foundRows = dtQuotationDetail.Select("ORGANIZATION_ID  = '" + alSupplierIDs[i].ToString() + "'");
                            if (foundRows.Length > 0)
                            {
                                for (int j = 0; j < foundRows.Length; j++)
                                {
                                    dtQuotation.ImportRow(foundRows[j]);
                                }
                            }

                            DataTable dtEmail = new DataTable();
                            dtEmail = dtEmailCc.Clone();
                            DataRow[] foundRows2 = dtEmailCc.Select("ORGANIZATION_ID  = '" + alSupplierIDs[i].ToString() + "'");
                            if (foundRows2.Length > 0)
                            {
                                for (int k = 0; k < foundRows2.Length; k++)
                                {
                                    dtEmail.ImportRow(foundRows2[k]);
                                }
                            }

                            DataView viewTo = new DataView(dtEmail);
                            DataTable dtTo = viewTo.ToTable(true, "ENQUIREY_DETAIL_ID", "ORGANIZATION_ID", "PEOPLE_ID", "PRODUCT_ID", "EMAIL");

                            DataView viewCc = new DataView(dtQuotation);
                            DataTable dtCc = viewCc.ToTable(true, "EmailCc", "ORGANIZATION_ID", "PRODUCT_ID");

                            string[] strEmailcc = dtCc.Rows[0]["EmailCc"].ToString().Split(',');

                            if (SendEmail(dtTo, strEmailcc))
                            {
                                flag = QoutCtl.InsertQuotation(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtQuotation);

                                foreach (DataRow drTo in dtTo.Rows)
                                {
                                    flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(drTo["PRODUCT_ID"]), Constants.ReceipientSupplier, Convert.ToInt64(drTo["PEOPLE_ID"]), drTo["EMAIL"].ToString(), Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));
                                }

                                foreach (DataRow drcc in dtCc.Rows)
                                {
                                    flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(drcc["PRODUCT_ID"]), Constants.ReceipientSupplier, Constants.LongNullValue, drcc["EmailCc"].ToString(), Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                                }

                                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.EmailSent, true);
                            }
                        }
                        if (flag)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Email sent successfully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error Occured. Email not sent.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + "" + " No Email ID Selected. ');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No Supplier Selected.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert(' " + ex.Message + "');", true);
        }
    }

    private bool SendEmail(DataTable dtEmailTo, string[] EmailCc)
    {
        bool flag = false;

        if (Session["EmailAccount"] != null)
        {
            if (Session["EmailAccount"].ToString() != "")
            {
                strEmailFrom = Session["EmailAccount"].ToString();
                strSMTPPassword = Session["AccountPassword"].ToString();
                strSMTP = Session["SMTP"].ToString();
            }
        }

        if (dtEmailTo.Rows.Count > 0)
        {
            DataTable dtEmailTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0,1);

            if (dtEmailTemplate.Rows.Count > 0)
            {
                strSubject = dtEmailTemplate.Rows[0]["SUBJECT"].ToString();
                strBody = dtEmailTemplate.Rows[0]["BODY"].ToString();
            }

            if (Utility.SendEmail(strEmailFrom, dtEmailTo, EmailCc, strSMTP, strSMTPPassword, strSubject, strBody))
            {
                flag = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('  " + "" + " While sending email some error occured.');", true);
            }
        }

        return flag;
    }

    private void CreateQuotationTable()
    {
        dtQuotationDetail = new DataTable();
        dtQuotationDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtQuotationDetail.Columns.Add("ORGANIZATION_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT", typeof(string));
        dtQuotationDetail.Columns.Add("QUANTITY", typeof(int));
        dtQuotationDetail.Columns.Add("EmailCc", typeof(string));
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

    public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID ,int p_DISTRIBUTOR_ID,int p_USER_ID)
    {
        DataTable dtEnqPrice = EnqPriceCtl.GetEnquiry(p_ENQUIREY_MASTER_ID, p_DISTRIBUTOR_ID, p_USER_ID,Constants.EnquirySupplier);
        this.Session.Add("dtEnqPrice", dtEnqPrice);
        return dtEnqPrice;                        
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
        Response.Redirect("frmQutationSupplier.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString());
    }

    /// <summary>
    /// Loads Assigned Supplier To Product To Assigned Supplier Combo
    /// </summary>
    private DataTable GetAssignSupplier(int ProductID)
    {
        DataTable dt = EnqPriceCtl.GetProductSupplier(ProductID,Convert.ToInt64(hfEnquiryID.Value));
        
        if (dt.Rows.Count > 0)
        {
            lblEnquiry.Text = dt.Rows[0]["ENQUIREY_MASTER_ID"].ToString();
            lblCustomer.Text = dt.Rows[0]["CustomerName"].ToString();
            hfgvEnquiryRows.Value = gvMain.Rows.Count.ToString();
            hfCustomerID.Value = dt.Rows[0]["CUSTOMER_ID"].ToString();
        }
        return dt;
    }

    private void LoadQuotationGrid()
    {
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
    }

    private ArrayList SupplierIDs()
    {
        ArrayList alSupplierIDs = new ArrayList();
        CreateQuotationTable();
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
        CreateEmailCcTable();
        dtEmailCc = (DataTable)Session["dtEmailCc"];
        foreach (GridViewRow gvr in gvMain.Rows)
        {
            GridView gvSupplier = (GridView)gvr.FindControl("gvSupplier");
            foreach (GridViewRow gvrSupp in gvSupplier.Rows)
            {
                CheckBox cbQuotation = gvrSupp.FindControl("cbQuotation") as CheckBox;
                TextBox txtEmailCc = gvrSupp.FindControl("txtEmailCc") as TextBox;
                Label lblSKU = gvr.FindControl("lblSKU") as Label;
                if (cbQuotation.Checked)
                {
                    DataRow dr = dtQuotationDetail.NewRow();
                    dr["ENQUIREY_DETAIL_ID"] = gvrSupp.Cells[0].Text;
                    dr["ORGANIZATION_ID"] = gvrSupp.Cells[1].Text;
                    dr["PRODUCT_ID"] = gvr.Cells[0].Text;
                    dr["PRODUCT"] = lblSKU.Text;
                    dr["QUANTITY"] = 0;
                    dr["EmailCc"] = txtEmailCc.Text;
                    dtQuotationDetail.Rows.Add(dr);

                    if (!alSupplierIDs.Contains(gvrSupp.Cells[1].Text))
                    {
                        alSupplierIDs.Add(gvrSupp.Cells[1].Text);
                    }

                    CheckBoxList cblEmailID = gvrSupp.FindControl("cblEmailID") as CheckBoxList;

                    for (int i = 0; i < cblEmailID.Items.Count; i++)
                    {
                        if (cblEmailID.Items[i].Selected)
                        {
                            if (cblEmailID.Items[i].Value != "")
                            {
                                DataRow drCc = dtEmailCc.NewRow();
                                drCc["ENQUIREY_DETAIL_ID"] = hfEnquiryID.Value;
                                drCc["ORGANIZATION_ID"] = gvrSupp.Cells[1].Text;
                                string[] str = cblEmailID.Items[i].Value.Split(',');
                                drCc["EMAIL"] = str[1].ToString();
                                drCc["PEOPLE_ID"] = str[0].ToString();
                                drCc["PRODUCT_ID"] = gvr.Cells[0].Text;
                                dtEmailCc.Rows.Add(drCc);
                            }
                        }
                    }
                }
            }
        }
        Session["dtQuotationDetail"] = dtQuotationDetail;
        return alSupplierIDs;
    }

    private void CreateEmailCcTable()
    {
        dtEmailCc = new DataTable();
        dtEmailCc.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtEmailCc.Columns.Add("ORGANIZATION_ID", typeof(int));
        dtEmailCc.Columns.Add("PEOPLE_ID", typeof(int));
        dtEmailCc.Columns.Add("PRODUCT_ID", typeof(int));
        dtEmailCc.Columns.Add("EMAIL", typeof(string));
        this.Session.Add("dtEmailCc", dtEmailCc);
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

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList cblEmailID = e.Row.FindControl("cblEmailID") as CheckBoxList;
            int SupplierID = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
            clsWebFormUtil.FillListBox(cblEmailID, GetEmailID(SupplierID), "EMAIL2", "EmailName", true);

            DataTable dt = EnqCtl.GetEmailReciepant(Convert.ToInt64(hfEnquiryID.Value), int.Parse(e.Row.Cells[6].Text));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < cblEmailID.Items.Count; i++)
                    {
                        if (dr["RECEIPIENT_EMAIL"].ToString() == cblEmailID.Items[i].Value.Split(',')[1].ToString())
                        {
                            cblEmailID.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
    }

    private DataTable GetEmailID(int SupplierID)
    {
        PeopleController PplCtl = new PeopleController();
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, SupplierID, null, null);
        return dtPeople;
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