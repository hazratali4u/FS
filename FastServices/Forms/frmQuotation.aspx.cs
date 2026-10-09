using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.IO;
using System.Text;

/// <summary>
/// Form To Post Vouchers
/// </summary>
public partial class Forms_frmQuotation : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
    EnquiryController EnqCtl = new EnquiryController();
    QuotationController QoutCtl = new QuotationController();
    PeopleController PplCtl = new PeopleController();
    string EmailCc;

    string strBody = string.Empty;
    string strSubject = string.Empty;
    string strSMTP = ConfigurationManager.AppSettings["SMTP"].ToString();
    string strSMTPPassword = ConfigurationManager.AppSettings["AccountPassword"].ToString();
    string strEmailFrom = System.Configuration.ConfigurationManager.AppSettings["EmailAccount"].ToString();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.LoadStatus();
            this.GetEnquiry(Constants.LongNullValue, Constants.IntNullValue, Constants.IntNullValue, Constants.EnquirySupplier);
            FillRepeater();
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
        hfEnquiryID.Value = p_EnquiryID.ToString();

        DataTable dt = EnqCtl.GetEnquiryMaster(Constants.IntNullValue, Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue, Constants.EnquirySupplier);
        if (dt.Rows.Count > 0)
        {
            hfCustomerID.Value = dt.Rows[0]["CUSTOMER_ID"].ToString();
            this.LoadEmailID();
            ltrlQuotation1.Text = GetQuotation1();
            ltrlQuotation2.Text = GetQuotation2();

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

    private string GetQuotation1()
    {
        StringBuilder strQuotation = new StringBuilder();
        DataTable dt = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value),false);
        Session.Add("dtQuot", dt);

        DataView viewSupplierID = new DataView(dt);
        DataTable dtSupplierID = viewSupplierID.ToTable(true, "SUPPLIER_ID", "Supplier");
        
        strQuotation.Append("<html><body>");
        strQuotation.Append("<table width='100%'>");
        strQuotation.Append("<tr>");

        strQuotation.Append("<td valign='top' style='width:45%;border-style: none solid none none; border-width: thin; border-color: Yellow;'>");
        strQuotation.Append("<span style='color:Yellow;'><u>Head Office</u></span><br>");
        strQuotation.Append("Suite # 03, 4th Floor, Abrar Business Center,<br>");
        strQuotation.Append("25-Main Wahdat Road, Lahore-Pakistan<br>");
        strQuotation.Append("Tel: 0092-42-11111-Fast Services(6366)<br>");
        strQuotation.Append("Fax: 0092-42-37532847, 37502900<br>");
        strQuotation.Append("Email: mail@neonchemicals.com, neonchems@yahoo.com");

        strQuotation.Append("</td>");

        strQuotation.Append("<td valign='top' style='width:30%;'>");
        strQuotation.Append("<span style='color:Yellow;'>&nbsp;<u>Karachi Office</u></span><br>");
        strQuotation.Append("&nbsp;Suite # 908, 9th Floor,");
        strQuotation.Append("&nbsp;Progressive Square, P.E.C.H.S, Block 6,<br>");
        strQuotation.Append("&nbsp;Shahrah-e-Faisal, Karachi-Pakistan<br>");
        strQuotation.Append("&nbsp;Tel: 021-34331187-88<br>");
        strQuotation.Append("&nbsp;Fax: 021-34331189");
        strQuotation.Append("</td>");

        strQuotation.Append("<td valign='top' style='width:25%;'>");
        strQuotation.Append("<img height='120px' src='../images/logo.JPG'>");
        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");

        strQuotation.Append("<tr>");
        strQuotation.Append("<td colspan='3'>");
        strQuotation.Append("&nbsp;");
        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");

        strQuotation.Append("<tr>");
        strQuotation.Append("<td colspan='3'>");
        strQuotation.Append("&nbsp;");
        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");

        strQuotation.Append("<tr>");
        strQuotation.Append("<td colspan='3'>");
        strQuotation.Append("&nbsp;");
        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");

        strQuotation.Append("<tr>");
        strQuotation.Append("<td colspan='3'>");

        if (dt.Rows.Count > 0)
        {
            strQuotation.Append("<table width='100%'>");
            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("Ref #: NC / " + dt.Rows[0]["ENQUIREY_MASTER_ID"].ToString() + "-C" + " / " + System.DateTime.Now.Year);
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("Date: " + System.DateTime.Now.ToString("MMM dd, yyyy"));
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("M/s " + dt.Rows[0]["Customer"].ToString());

            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("<b>SUBJECT: QUOTATION</b>");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("</table>");
        }

        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");


        strQuotation.Append("</table>");
        strQuotation.Append("</body></html>");

        return strQuotation.ToString();
    }

    private string GetQuotation2()
    {
        StringBuilder strQuotation = new StringBuilder();
        DataTable dt = (DataTable)Session["dtQuot"];

        strQuotation.Append("<html><body>");
        strQuotation.Append("<table width='100%'>");

        strQuotation.Append("<tr>");
        strQuotation.Append("<td>");

        if (dt.Rows.Count > 0)
        {
            strQuotation.Append("<table width='100%'>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("Dear Sir,");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("For your kind reference we are pleased to quoute you our best prices on befalf of our valuable <br>");
            strQuotation.Append("principal for your requirement as follows:-");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            foreach (DataRow dr in dt.Rows)
            {
                strQuotation.Append("<tr>");
                strQuotation.Append("<td style='width:5%;'>");
                strQuotation.Append("&nbsp;");
                strQuotation.Append("</td>");
                strQuotation.Append("<td style='width:95%;'>");
                strQuotation.Append("<b>. " + dr["SKU_NAME"].ToString() + " &nbsp;&nbsp;&nbsp;" + dr["QUANTITY"].ToString() + " KG @ $ " + dr["RateCustomer"].ToString() + "/KG</b>");
                strQuotation.Append("</td>");
                strQuotation.Append("</tr>");

                strQuotation.Append("<tr>");
                strQuotation.Append("<td style='width:5%;'>");
                strQuotation.Append("&nbsp;");
                strQuotation.Append("</td>");
                strQuotation.Append("<td style='width:95%;'>");
                strQuotation.Append("<i>(Above product is on " + dr["Payment"].ToString() + " in Advance by " + dr["Shipment"] + " " + dr["CITY_NAME"].ToString() + " Prompt shipment from " + dr["SupplierLocation"].ToString() + "</i>)");
                strQuotation.Append("</td>");
                strQuotation.Append("</tr>");
            }


            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("We hope that you will find our above offer most competitive and looking forward to receive your <br>");
            strQuotation.Append("valued order, which will have our prompt attention.");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("Thanking you and assuring you of our best services and co-operation at all time, we remain.");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("Sincerely Yours,");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("For Fast Services");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td colspan='2'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("<b>" + Session["UserName2"].ToString() + "</b>");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");

            strQuotation.Append("<tr>");
            strQuotation.Append("<td style='width:5%;'>");
            strQuotation.Append("&nbsp;");
            strQuotation.Append("</td>");
            strQuotation.Append("<td style='width:95%;'>");
            strQuotation.Append("For M. Iqbal / Tariq Naeem Jaura");
            strQuotation.Append("</td>");
            strQuotation.Append("</tr>");


            strQuotation.Append("</table>");
        }

        strQuotation.Append("</td>");
        strQuotation.Append("</tr>");


        strQuotation.Append("</table>");
        strQuotation.Append("</body></html>");

        return strQuotation.ToString();
    }

    protected void btnWord_Click(object sender, EventArgs e)
    {
        DataTable dt = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value),false);
        Session.Add("dtQuot", dt);
        SetEmailCc();
        string str = @"<html
                        xmlns:o='urn:schemas-microsoft-com:office:office'
                        xmlns:w='urn:schemas-microsoft-com:office:word'
                        xmlns='http://www.w3.org/TR/REC-html40'>
                        <head><title></title>

                        <!--[if gte mso 9]>
                        <xml>
                        <w:WordDocument>
                        <w:View>Print</w:View>
                        <w:Zoom>100</w:Zoom>
                        <w:DoNotOptimizeForBrowser/>
                        </w:WordDocument>
                        </xml>
                        <![endif]-->";
        str += "<style>";
        str += "@page Section2 {margin:0.1in 0.1in 0in 0.1in;mso-header-margin:0.1in;mso-footer-margin:0.1in;mso-paper-source:0;}";
        str += "div.Section2 {page:Section2;}";
        str += @" p.MsoFooter, li.MsoFooter, div.MsoFooter
                        {
                        margin:0in 0in 0in 0in;
                        margin-bottom:.0001pt;
                        mso-pagination:widow-orphan;
                        tab-stops:center 3.0in right 6.0in;
                        font-size:12.0pt;
                        }";
        str += "</style>";
        str += "</head>";
        str += "<body>";
        str += "<div class=Section2>";

        str += "<table width='100%'>";
        str += "<tr>";

        str += "<td valign='top' style='width:45%;border-style: none solid none none; border-width: thin; border-color: Yellow;'>";
        str += "<span style='color:Yellow;'><u>Head Office</u></span><br>";
        str += "Suite # 03, 4th Floor, Abrar Business Center,<br>";
        str += "25-Main Wahdat Road, Lahore-Pakistan<br>";
        str += "Tel: 0092-42-11111-Fast Services(6366<br>";
        str += "Fax: 0092-42-37532847, 37502900<br>";
        str += "Email: mail@neonchemicals.com, neonchems@yahoo.com";

        str += "</td>";

        str += "<td valign='top' style='width:30%;'>";
        str += "<span style='color:Yellow;'>&nbsp;<u>Karachi Office</u></span><br>";
        str += "&nbsp;Suite # 908, 9th Floor,";
        str += "&nbsp;Progressive Square, P.E.C.H.S, Block 6,<br>";
        str += "&nbsp;Shahrah-e-Faisal, Karachi-Pakistan<br>";
        str += "&nbsp;Tel: 021-34331187-88<br>";
        str += "&nbsp;Fax: 021-34331189";
        str += "</td>";

        str += "<td valign='top' style='width:25%;'>";
        str += "<img height='80px' src='" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "logo.gif'>";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='3'>";
        str += "&nbsp;";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='3'>";
        str += "&nbsp;";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='3'>";
        str += "&nbsp;";
        str += "</td>";
        str += "</tr>";

        str += "<tr>";
        str += "<td colspan='3'>";

        if (dt.Rows.Count > 0)
        {
            str += "<table width='100%'>";
            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "Ref #: NC / " + dt.Rows[0]["ENQUIREY_MASTER_ID"].ToString() + "-C" + " / " + System.DateTime.Now.Year;
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "Date: " + System.DateTime.Now.ToString("MMM dd, yyyy");
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "M/s " + dt.Rows[0]["Customer"].ToString();
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "<b>SUBJECT: QUOTATION</b>";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";



            str += "<tr>";
            str += "<td style='width:5%;'></td>";
            str += "<td>Kind Attn: " + ddlEmailTo.SelectedItem.Text + "</td>";
            str += "</tr>";
            str += "<tr>";
            str += "<td style='width:5%;'></td>";
            str += "<td>Cc: " + Session["strEmailCc"].ToString() + "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "Dear Sir,";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "For your kind reference we are pleased to quoute you our best prices on befalf of our valuable <br>";
            str += "principal for your requirement as follows:-";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            foreach (DataRow dr in dt.Rows)
            {
                str += "<tr>";
                str += "<td style='width:5%;'>";
                str += "&nbsp;";
                str += "</td>";
                str += "<td style='width:95%;'>";
                str += "<b>. " + dr["SKU_NAME"].ToString() + " &nbsp;&nbsp;&nbsp;" + dr["QUANTITY"].ToString() + " KG @ $ " + dr["RateCustomer"].ToString() + "/KG</b>";
                str += "</td>";
                str += "</tr>";

                str += "<tr>";
                str += "<td style='width:5%;'>";
                str += "&nbsp;";
                str += "</td>";
                str += "<td style='width:95%;'>";
                str += "<i>(Above product is on " + dr["Payment"].ToString() + " in Advance by " + dr["Shipment"] + " " + dr["CITY_NAME"].ToString() + " Prompt shipment from " + dr["SupplierLocation"].ToString() + "</i>)";
                str += "</td>";
                str += "</tr>";
            }


            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "We hope that you will find our above offer most competitive and looking forward to receive your <br>";
            str += "valued order, which will have our prompt attention.";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "Thanking you and assuring you of our best services and co-operation at all time, we remain.";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "Sincerely Yours,";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "For Fast Services";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td colspan='2'>";
            str += "&nbsp;";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "<b>" + Session["UserName2"].ToString() + "</b>";
            str += "</td>";
            str += "</tr>";

            str += "<tr>";
            str += "<td style='width:5%;'>";
            str += "&nbsp;";
            str += "</td>";
            str += "<td style='width:95%;'>";
            str += "For M. Iqbal / Tariq Naeem Jaura";
            str += "</td>";
            str += "</tr>";

            str += "</table>";
        }

        str += "</td>";
        str += "</tr>";


        str += "</table>";



        str += "</div>";
        str += @"<div style='mso-element:footer' id=f1>
                        <p class=MsoFooter>
                        
                        </p></div>";
        str += "</body>";
        str += "</html >";

        Response.AppendHeader("Content-Type", "application/msword");
        Response.AppendHeader("Content-disposition", "attachment; filename=" + "Quotation.doc");
        Response.Write(str);
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Docs";
        string fileName = "Quotation.pdf";
        SetEmailCc();

        if (CreateFile(filePath + "\\" + fileName))
        {
            DownLoadFile(filePath, fileName);
        }
    }

    protected void btnSendQuotation_Click(object sender, EventArgs e)
    {
        SetEmailCc();
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Docs" + "\\Quotation.pdf";

        if (CreateFile(filePath))
        {
            EmailCc = Session["EmailCc"].ToString();
            string[] strEmailCc = EmailCc.Split(',');
            if (SendEmail(ddlEmailTo.SelectedValue, strEmailCc, filePath))
            {
                try
                {
                    DataTable dt = (DataTable)Session["dtQuot"];
                    bool flag = false;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DataTable dtQuotation = new DataTable();
                            dtQuotation = dt.Clone();
                            DataRow[] foundRows = dt.Select("SUPPLIER_ID  = '" + dr["SUPPLIER_ID"].ToString() + "'");
                            if (foundRows.Length > 0)
                            {
                                for (int j = 0; j < foundRows.Length; j++)
                                {
                                    dtQuotation.ImportRow(foundRows[j]);
                                }
                            }

                            flag = QoutCtl.InsertQuotation(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtQuotation);
                            flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), ddlEmailTo.SelectedValue, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));
                            flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), "", Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));

                            if (flag)
                            {
                                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSaved, true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Quotation Sent Successfully.')", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error Occured.')", true);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
                }
            }
        }
    }

    private void LoadEmailID()
    {
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, Convert.ToInt64(hfCustomerID.Value), null, null);
        clsWebFormUtil.FillDropDownList(this.ddlEmailTo, dtPeople, "EMAIL", "EmailName");
        clsWebFormUtil.FillListBox(this.cblCc, dtPeople, "EMAIL", "EmailName", true);
        Session.Add("dtByerContact", dtPeople);
    }

    private bool SendEmail(string p_EmailTo, string[] p_EmailCc, string p_Attachment)
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

        DataTable dtEmailTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0,3);

        if (dtEmailTemplate.Rows.Count > 0)
        {
            strSubject = dtEmailTemplate.Rows[0]["SUBJECT"].ToString();
            strBody = dtEmailTemplate.Rows[0]["BODY"].ToString();
        }


        if (Utility.SendEmail(strEmailFrom, p_EmailTo, p_EmailCc, strSMTP, strSMTPPassword, strSubject, strBody, p_Attachment,0))
        {
            flag = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('While sending email some error occured.')", true);
        }

        return flag;
    }

    private bool CreateFile(string filePath)
    {
        bool flag = false;
        try
        {
            DataTable dtReports = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value),false);
            if (dtReports.Rows.Count > 0)
            {
                FSBusinessLayer.Reports.CrpQuotation CrpReport = new FSBusinessLayer.Reports.CrpQuotation();
                CrpReport.SetDataSource(dtReports);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("RefNo", "NC / " + dtReports.Rows[0]["ENQUIREY_MASTER_ID"].ToString() + "-C" + " / " + System.DateTime.Now.Year);
                CrpReport.SetParameterValue("EmailTo", ddlEmailTo.SelectedItem.Text);
                CrpReport.SetParameterValue("EmailCc", Session["strEmailCc"].ToString());
                CrpReport.SetParameterValue("SentBy", Session["UserName2"].ToString());
                CrpReport.SetDatabaseLogon(FSCommonLayer.Classes.Configuration.ServerUserName, FSCommonLayer.Classes.Configuration.ServerUserPwd, FSCommonLayer.Classes.Configuration.DatabseServer, FSCommonLayer.Classes.Configuration.DatabaseName);
                CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath);
            }
            flag = true;
        }
        catch (Exception ex)
        {
            flag = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('" + ex.Message + "')", true);
        }
        return flag;
    }

    private void DownLoadFile(string Path, string FileName)
    {

        // The file name used to save the file to the client's system..
        //string filename = Path.GetFileName(filepath);
        System.IO.Stream stream = null;
        try
        {
            // Open the file into a stream. 
            stream = new FileStream(Path + "\\" + FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            // Total bytes to read: 
            long bytesToRead = stream.Length;
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            // Read the bytes from the stream in small portions. 
            while (bytesToRead > 0)
            {
                // Make sure the client is still connected. 
                if (Response.IsClientConnected)
                {
                    // Read the data into the buffer and write into the 
                    // output stream. 
                    byte[] buffer = new Byte[10000];
                    int length = stream.Read(buffer, 0, 10000);
                    Response.OutputStream.Write(buffer, 0, length);
                    Response.Flush();
                    // We have already read some bytes.. need to read 
                    // only the remaining. 
                    bytesToRead = bytesToRead - length;
                }
                else
                {
                    // Get out of the loop, if user is not connected anymore.. 
                    bytesToRead = -1;
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            // An error occurred.. 
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
    }

    private void SetEmailCc()
    {
        string strEmailCc = "";
        EmailCc = "";
        for (int i = 0; i < cblCc.Items.Count; i++)
        {
            if (cblCc.Items[i].Selected)
            {
                EmailCc += cblCc.Items[i].Value + ",";
                strEmailCc += cblCc.Items[i].Text + ",";
            }
        }

        EmailCc += txtCc2.Text;

        Session.Add("EmailCc", EmailCc);
        Session.Add("strEmailCc", strEmailCc);
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
