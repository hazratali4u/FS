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
public partial class Forms_rptSalesReport : System.Web.UI.Page
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
            this.LoadCustomer();
            this.LoadProductCategory();
            rblReportType_SelectedIndexChanged(null, null);
        }
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
        this.ddlSubType.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlSubType, m_dt, "REF_ID", "SLASH_DESC");
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ltrlReport.Text = "";
        Session.Add("LevelType", Request.QueryString["LevelType"].ToString());
        Session.Add("TopID", Request.QueryString["TopID"].ToString());
        Session.Add("LevelID", Request.QueryString["LevelID"].ToString());

        if (rblReportType.SelectedIndex == 0)
        {
            GenerateSalesReport(0, ddlSubType.SelectedItem.Text + " Month Wise Sales Report",0);
        }
        else
        {
            GenerateSalesReport(1, ddlSubType.SelectedItem.Text + " Year Wise Sales Report",0);
        }
    }
      
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        ltrlReport.Text = "";
        
        if (rblReportType.SelectedIndex == 0)
        {
            GenerateSalesReport(0, ddlSubType.SelectedItem.Text + " Month Wise Sales Report", 1);
        }
        else
        {
            GenerateSalesReport(1, ddlSubType.SelectedItem.Text + " Year Wise Sales Report", 1);
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

    #region Sales Rport

    private void GenerateSalesReport(int REPORT_TYPE, string ReportTitle, int Formate)
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

        if (PaymentMode.Length > 0)
        {
            PaymentMode = PaymentMode.Substring(0, PaymentMode.Length - 1);
            PaymentMode2 = PaymentMode2.Substring(0, PaymentMode2.Length - 1);
        }
        
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

        DataTable dtReport = ReportCtl.GetMonthlyReport(dtFrom, Convert.ToDateTime(dtTo.ToShortDateString() + " 23:59:59"), Convert.ToInt32(ddlCustomer.SelectedValue), CategoryID, REPORT_TYPE, PaymentMode);
        
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
            this.Session.Add("ReportType", Formate);
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