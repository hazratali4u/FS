using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form For Petty Expense Report
/// </summary>
public partial class Forms_RptPetyCashStatment : System.Web.UI.Page
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
            this.LoadDistributor();

            DateTime dtFrom = Convert.ToDateTime(Session["CurrentWorkDate"]);            
            dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);


            FSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = dtFrom.ToString("dd-MMM-yyyy");
            txtEndDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }
    
    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        drpDistributor.Items.Clear();   
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        if (RbReportType.SelectedIndex == 1)
        {
            drpDistributor.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        }
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }
    
    /// <summary>
    /// Loads Locations
    /// </summary>
    protected void RbReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadDistributor();
    }

    /// <summary>
    /// Shows Petty Expense Report in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();        
        
        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));

        if (RbReportType.SelectedIndex == 0 || RbReportType.SelectedIndex == 2)
        {
            FSBusinessLayer.Reports.CrpPetyCashSummary CrpReport = new FSBusinessLayer.Reports.CrpPetyCashSummary();

            int AccountParentID = 55;//Administrative Expenses
            if (RbReportType.SelectedIndex == 2)
            {
                AccountParentID = 47;//Purchases (Principal Wise)
            }            
            DataSet ds = RptAccountCtl.SelectPetyCashStatment(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), AccountParentID,Constants.IntNullValue,Constants.IntNullValue);
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
            if (RbReportType.SelectedIndex == 0)
            {
                CrpReport.SetParameterValue("ReportTitle", "Petty Expense Statement");
            }
            else
            {
                CrpReport.SetParameterValue("ReportTitle", "Payment Statement");
            }
            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            FSBusinessLayer.Reports.CrpPettyCaahSummery CrpReport = new FSBusinessLayer.Reports.CrpPettyCaahSummery();

            DataSet ds = RptAccountCtl.SelectPetyCashSummary(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
    }

    /// <summary>
    /// Shows Petty Expense Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        RptAccountController RptAccountCtl = new RptAccountController();
        DocumentPrintController mController = new DocumentPrintController();
        FSBusinessLayer.Reports.CrpPetyCashSummary CrpReport = new FSBusinessLayer.Reports.CrpPetyCashSummary();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        
        if (RbReportType.SelectedIndex == 0)
        {
            DataSet ds = RptAccountCtl.SelectPetyCashStatment(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), Constants.IntNullValue,Constants.IntNullValue,Constants.IntNullValue);
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 1);
            string url = "'Default.aspx'";
            string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(cstype, "OpenWindow", script);
        }
        else
        {
            DataSet ds = RptAccountCtl.SelectPetyCashSummary(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
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
}
