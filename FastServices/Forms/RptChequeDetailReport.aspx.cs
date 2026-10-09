using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form For Cheque Detail Report
/// </summary>
public partial class Forms_RptChequeDetailReport : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpStatus.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Received", Constants.Cheque_Pending.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Depsoit", Constants.Cheque_Deposit.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Realize", Constants.Cheque_Clear.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Bounce", Constants.Cheque_Bons.ToString()));
            
            FSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtEndDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            this.LoadCustomer();
        }
    }

    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        OrganizationController OrgCtl = new OrganizationController();
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
        this.DrpCustomer.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
    }

    /// <summary>
    /// Shows Cheque Detail Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
            DocumentPrintController mController = new DocumentPrintController();
            RptCustomerController RptCustomerCtl = new RptCustomerController();
            FSBusinessLayer.Reports.CrpChequeDetailReport CrpReport = new FSBusinessLayer.Reports.CrpChequeDetailReport();
            DataTable dt = mController.SelectReportTitle(1);

            DataSet ds = RptCustomerCtl.SelectCustomerCreditReport(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
                int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), int.Parse(this.Session["UserId"].ToString()), RbReportFilter.SelectedIndex);
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();


            CrpReport.SetParameterValue("from_date", txtStartDate.Text);
            CrpReport.SetParameterValue("To_date", txtEndDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);        
    }
    
    /// <summary>
    /// Shows Cheque Detail Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        FSBusinessLayer.Reports.CrpChequeDetailReport CrpReport = new FSBusinessLayer.Reports.CrpChequeDetailReport();
        DataTable dt = mController.SelectReportTitle(1);

        DataSet ds = RptCustomerCtl.SelectCustomerCreditReport(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue,
            int.Parse(DrpCustomer.SelectedValue.ToString()), Constants.IntNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(txtStartDate.Text), DateTime.Parse(txtEndDate.Text), int.Parse(this.Session["UserId"].ToString()), RbReportFilter.SelectedIndex);
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();


        CrpReport.SetParameterValue("from_date", txtStartDate.Text);
        CrpReport.SetParameterValue("To_date", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);  
    }
}
