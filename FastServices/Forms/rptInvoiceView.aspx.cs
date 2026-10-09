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
public partial class rptInvoiceView : System.Web.UI.Page
{

    /// <summary>
    /// Page_Load Function Populates All Combos On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtTo.Text = System.DateTime.Now.ToString("MMMM yyyy");
            txtFrom.Text = System.DateTime.Now.ToString("MMMM yyyy");
        }
    }

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        RptAccountController AccCtl = new RptAccountController();
        DataTable dtTaxSubmission = null;
        DataSetToExcel dsexcel = new DataSetToExcel();
        string path = FSCommonLayer.Classes.Configuration.GetAppInstallationPath() + "\\TaxReport.xls";

        DateTime dtFrom = System.DateTime.Now;
        DateTime dtTo = System.DateTime.Now;
        int InvoiceType = 0;
        if (cbTax.Checked && cbCash.Checked)
        {
            InvoiceType = 3;
        }
        else if (cbCash.Checked)
        {
            InvoiceType = 1;
        }
        else if (cbTax.Checked)
        {
            InvoiceType = 2;
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "Please select invoice type.");
            return;
        }

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

        dtTaxSubmission = AccCtl.GetInvoiceView(InvoiceType, dtFrom, dtTo).Tables["uspGetInvoiceView"];

        DataSetToExcel.ExportDataTableToExcel(dtTaxSubmission, path, "uspGetInvoiceView");

        System.IO.FileInfo file = new System.IO.FileInfo(path);
        if (file.Exists)
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.WriteFile(file.FullName);
            Response.End();
        }
        else
        {
            Response.Write("This file does not exist.");
        }
    }

    private void ShowReport(int ReprortType)
    {
        ltrlInvoice.Text = string.Empty;
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController AccCtl = new RptAccountController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(Session["DISTRIBUTOR_ID"].ToString()));

        DateTime dtFrom = System.DateTime.Now;
        DateTime dtTo = System.DateTime.Now;
        int InvoiceType = 0;
        if(cbTax.Checked && cbCash.Checked)
        {
            InvoiceType = 3;
        }
        else if(cbCash.Checked)
        {
            InvoiceType = 1;
        }
        else if (cbTax.Checked)
        {
            InvoiceType = 2;
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "Please select invoice type.");
            return;
        }
        
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

        ds = AccCtl.GetInvoiceView(InvoiceType,dtFrom, dtTo);

        FSBusinessLayer.Reports.CrpInvoiceView CrpReport = new FSBusinessLayer.Reports.CrpInvoiceView();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("FromMonth", txtFrom.Text);
        CrpReport.SetParameterValue("ToMonth", txtTo.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

        this.Session.Add("ReportType", ReprortType);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}