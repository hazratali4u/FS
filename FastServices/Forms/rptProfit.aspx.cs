using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using FSBusinessLayer.Reports;

/// <summary>
/// Form For Deposit Slip Detail Report
/// </summary>
public partial class Forms_rptProfit : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            LoadLocation();
            LoadCategory();
            this.txtFromDate.Text =  System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.txtToDate.Text   =  System.DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    protected void LoadLocation()
    {
        try
        {
            DistributorController DController = new DistributorController();
            DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
            this.DrpLocation.DataSource = dt;
            this.DrpLocation.DataTextField = "DISTRIBUTOR_NAME";
            this.DrpLocation.DataValueField = "DISTRIBUTOR_ID";
            this.DrpLocation.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void LoadCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        this.ddlCategory.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlCategory, m_dt, "REF_ID", "SLASH_DESC");
    }

    /// <summary>
    /// Shows Deposit Slip Detail in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReprt(0);
    }

    /// <summary>
    /// Shows Deposit Slip Detail in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReprt(1);
    }

    private void ShowReprt(int Type)
    {
        try
        {
            FSBusinessLayer.Classes.DocumentPrintController DPrint = new FSBusinessLayer.Classes.DocumentPrintController();
            RptAccountController RptAccountCtl = new RptAccountController();

            string FromDate = null;
            string ToDate = null;
            FSBusinessLayer.Reports.CrpProfit CrpReport = new FSBusinessLayer.Reports.CrpProfit();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srRevenu = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srDirectExpense = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalDecisions.CrystalReports.Engine.ReportDocument srExpense = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalDecisions.CrystalReports.Engine.ReportDocument sbProfit = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            DataSet ds = null;
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();


            ds = RptAccountCtl.GetProfitData(int.Parse(ddlCategory.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 23:59:59"));
            srRevenu = CrpReport.OpenSubreport("srRevenu");
            srDirectExpense = CrpReport.OpenSubreport("srDirectExpense");
            srExpense = CrpReport.OpenSubreport("srExpense");
            sbProfit = CrpReport.OpenSubreport("sbProfit");

            srRevenu.SetDataSource(ds);
            srDirectExpense.SetDataSource(ds);
            srExpense.SetDataSource(ds);
            sbProfit.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor", DrpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtFromDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            CrpReport.SetParameterValue("Category", ddlCategory.SelectedItem.Text);

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", Type);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
}