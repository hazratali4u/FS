using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Reports
/// </summary>
public partial class Forms_frmReport : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    RoleManagementController RoleCtl = new RoleManagementController();
    ReportController ReportCtl = new ReportController();

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlFromYear.SelectedValue = System.DateTime.Now.Year.ToString();
            ddlToYear.SelectedValue = System.DateTime.Now.Year.ToString();
            txtTo.Text = System.DateTime.Now.ToString("MMMM yyyy");
            txtFrom.Text = System.DateTime.Now.ToString("MMMM yyyy");
            this.LoadReport();
            this.LoadCustomer();
            this.LoadProductCategory();
        }
    }

    private void LoadReport()
    {
        //this.ddlReportType.Items.Add(new clsListItems("Monthly Recurring", "0"));
        this.ddlReportType.Items.Add(new clsListItems("Recurring", "1"));
        this.ddlReportType.Items.Add(new clsListItems("Pending Payments", "2"));
        this.ddlReportType.Items.Add(new clsListItems("Sale Reports", "3"));

        this.ddlRecurringType.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        this.ddlRecurringType.Items.Add(new clsListItems("Monthly","1"));
        this.ddlRecurringType.Items.Add(new clsListItems("Quaterly","3"));
        this.ddlRecurringType.Items.Add(new clsListItems("Semi-Yearly","4"));
        this.ddlRecurringType.Items.Add(new clsListItems("Yearly","2"));
        this.ddlRecurringType.Items.Add(new clsListItems("Date Wise","5"));
    }

    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
        this.ddlCustomer.Items.Add(new clsListItems("All",Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCustomer, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadProductCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        this.ddlCategory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlCategory, m_dt, "REF_ID", "SLASH_DESC");
    }
        
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ltrlReport.Text = "";
        Session.Add("LevelType", Request.QueryString["LevelType"].ToString());
        Session.Add("TopID", Request.QueryString["TopID"].ToString());
        Session.Add("LevelID", Request.QueryString["LevelID"].ToString());
        gvPendingPayment.Visible = false;
        gvNotification.Visible = false;
        if (ddlReportType.SelectedIndex == 0)
        {
            gvPendingPayment.Visible = false;
            gvNotification.Visible = true;
            GenerateCurrentNotificationReport();
        }
        else if (ddlReportType.SelectedIndex == 1)
        {
            gvNotification.Visible = false;
            gvPendingPayment.Visible = true;
            GeneratePendingPaymentReport();
        }
        else if (ddlReportType.SelectedIndex == 2)
        {
            gvPendingPayment.Visible = false;
            gvNotification.Visible = false;

            if (rblReportType.SelectedIndex == 0)
            {
                GenerateSalesReport(0, ddlSubType.SelectedItem.Text + " Month Wise Sales Report");
            }
            else
            {
                GenerateSalesReport(1, ddlSubType.SelectedItem.Text + " Year Wise Sales Report");
            }
        }
    }
      
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition","attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/excel";
        StringBuilder sb = new StringBuilder();
               

        for (int k = 0; k < gvNotification.Columns.Count; k++)
        {
            //add separator
            sb.Append(gvNotification.Columns[k].HeaderText + ',');
        }
        sb.Append("\r\n");

        for (int i = 0; i < gvNotification.Rows.Count; i++)
        {

            for (int k = 0; k < gvNotification.Columns.Count - 2; k++)
            {
                sb.Append(gvNotification.Rows[i].Cells[k].Text + ',');

            }

            sb.Append("\r\n");
        }

        Response.Output.Write(sb.ToString());

        Response.Flush();

        Response.End();
    }

    protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPendingPayment.Visible = false;
        gvNotification.Visible = false;
        ddlRecurringType.Visible = false;
        lblRecurringType.Visible = false;
        if (ddlReportType.SelectedIndex == 0)
        {
            ddlSubType.Visible = false;
            lblSubType.Visible = false;
            rblReportType.Visible = false;
            lblPaymentMode.Visible = false;
            cbPaymentMode.Visible = false;
            ddlCategory.Visible = true;

            ddlRecurringType.Visible = true;
            lblRecurringType.Visible = true;
        }       
        if (ddlReportType.SelectedIndex == 12)
        {
            ddlCategory.Visible = false;
            lblCategory.Visible = false;
            ddlSubType.Visible = false;
            lblSubType.Visible = false;
            rblReportType.Visible = false;
            lblPaymentMode.Visible = false;
            cbPaymentMode.Visible = false;
        }
        else if (ddlReportType.SelectedIndex == 2)
        {
            lblFromMonth.Visible = true;
            lblToMonth.Visible = true;
            txtFrom.Visible = true;
            txtTo.Visible = true;

            ddlCategory.Visible = false;
            lblCategory.Visible = false;

            lblFromYear.Visible = false;
            lblToYear.Visible = false;
            ddlFromYear.Visible = false;
            ddlToYear.Visible = false;
            ddlSubType.Visible = true;
            lblSubType.Visible = true;
            rblReportType.Visible = true;
            lblPaymentMode.Visible = true;
            cbPaymentMode.Visible = true;
        }                
    }

    protected void rblReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblReportType.SelectedIndex == 0)
        {
            lblFromMonth.Visible = true;
            lblToMonth.Visible = true;
            txtFrom.Visible = true;
            txtTo.Visible = true;

            lblFromYear.Visible = false;
            lblToYear.Visible = false;
            ddlFromYear.Visible = false;
            ddlToYear.Visible = false;
        }
        else
        {
            lblFromMonth.Visible = false;
            lblToMonth.Visible = false;
            txtFrom.Visible = false;
            txtTo.Visible = false;

            lblFromYear.Visible = true;
            lblToYear.Visible = true;
            ddlFromYear.Visible = true;
            ddlToYear.Visible = true;
        }
    }

    #region Notification

    private void GenerateCurrentNotificationReport()
    {
        DataTable dtReportReport = ReportCtl.GetCurrentNotificationReport(Convert.ToInt32(ddlCustomer.SelectedValue), Convert.ToInt32(ddlRecurringType.SelectedValue),Convert.ToInt32(ddlCategory.SelectedValue));
        gvNotification.DataSource = dtReportReport;
        gvNotification.DataBind();
        Session.Add("dtReport", dtReportReport);
        if (dtReportReport.Rows.Count > 0)
        {
            btnExcel.Visible = true;
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }
    
    #endregion

    #region Pending Payment

    private void GeneratePendingPaymentReport()
    {
        DataTable dtReportReport = ReportCtl.GetPendingPaymentReport(Convert.ToInt32(ddlCustomer.SelectedValue),Convert.ToInt32(ddlCategory.SelectedValue));
        Session.Add("dtReport", dtReportReport);
        gvPendingPayment.DataSource = dtReportReport;
        gvPendingPayment.DataBind();
        
        if (dtReportReport.Rows.Count > 0)
        {
            btnExcel.Visible = true;
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }

    #endregion

    #region Sales Rport
        
    private void GenerateSalesReport(int REPORT_TYPE, string ReportTitle)
    {
        DateTime dtFrom = System.DateTime.Now;
        DateTime dtTo = System.DateTime.Now;
        string PaymentMode = string.Empty;
        string PaymentMode2 = string.Empty;

        foreach (ListItem item in cbPaymentMode.Items)
        {
            if (item.Selected)
            {
                PaymentMode += item.Value + ",";
                PaymentMode2 += item.Text + ",";
            }
        }

        PaymentMode = PaymentMode.Substring(0, PaymentMode.Length - 1);
        PaymentMode2 = PaymentMode2.Substring(0, PaymentMode2.Length - 1);
        
        if (rblReportType.SelectedIndex == 0)
        {

            if (txtFrom.Text.Length > 0)
            {
                DateTime dtFromMonth = DateTime.Parse(txtFrom.Text);
                dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);
            }

            if (txtTo.Text.Length > 0)
            {
                DateTime dtToMonth = DateTime.Parse(txtTo.Text);
                dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
                dtTo = dtTo.AddMonths(1).AddDays(-1);

            }
        }
        else if (rblReportType.SelectedIndex == 1)
        {
            dtFrom = new DateTime(Convert.ToInt32(this.ddlFromYear.SelectedValue), 1, 1);
            dtTo = new DateTime(Convert.ToInt32(this.ddlToYear.SelectedValue), 12, 31);
        }

        DocumentPrintController DPrint = new DocumentPrintController();

        DataTable dtCompany = DPrint.SelectReportTitle(Constants.IntNullValue);
        int CategoryID = Constants.IntNullValue;
        if (ddlSubType.SelectedValue != "0")
        {
            CategoryID = Convert.ToInt32(ddlSubType.SelectedValue);
        }



        DataTable dtReport = ReportCtl.GetMonthlyReport(dtFrom, dtTo, Convert.ToInt32(ddlCustomer.SelectedValue), CategoryID, REPORT_TYPE, PaymentMode);
        
        if (dtReport.Rows.Count > 0)
        {
            btnExcel.Visible = true;
            FSBusinessLayer.Reports.CrpSalesReport CrpReport = new FSBusinessLayer.Reports.CrpSalesReport();

            CrpReport.SetDataSource(dtReport);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FromDate", dtFrom);
            CrpReport.SetParameterValue("ToDate", dtTo);
            CrpReport.SetParameterValue("Customer", ddlCustomer.SelectedItem.Text);
            CrpReport.SetParameterValue("ReportTitle", ReportTitle);
            CrpReport.SetParameterValue("CompanyName", dtCompany.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("PaymentMode", PaymentMode2);

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"_self\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }

    #endregion           
    
}