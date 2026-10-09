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
public partial class Forms_rptPayStub : System.Web.UI.Page
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
            LoadEmployee();
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

    private void LoadEmployee()
    {
        PeopleController PplCtl = new PeopleController();
        DataTable dtPeople = new DataTable();

        if (Session["RoleID"].ToString() == "1")
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null, Constants.IntNullValue, 0);
        }
        else
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null, Convert.ToInt32(Session["UserID"]), 0);
        }
        ddlEmployee.Items.Add(new ListItem("All", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dtPeople, "PEOPLE_ID", "FullName");
    }

    /// <summary>
    /// Shows Deposit Slip Detail in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        try
        {
            FSBusinessLayer.Classes.DocumentPrintController DPrint = new FSBusinessLayer.Classes.DocumentPrintController();
            RptAccountController RptAccountCtl = new RptAccountController();
          
            string FromDate = null;
            string ToDate = null;
            FSBusinessLayer.Reports.CrpPaymentSlip CrpReport = new FSBusinessLayer.Reports.CrpPaymentSlip();
            DataSet ds = null;
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();

            ds = RptAccountCtl.GetPaymentSlipData(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 23:59:59"), Convert.ToInt64(this.ddlEmployee.SelectedValue));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor", DrpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtFromDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 0);
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

    /// <summary>
    /// Shows Deposit Slip Detail in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        try
        {
            FSBusinessLayer.Classes.DocumentPrintController DPrint = new FSBusinessLayer.Classes.DocumentPrintController();
            RptAccountController RptAccountCtl = new RptAccountController();

            string FromDate = null;
            string ToDate = null;
            FSBusinessLayer.Reports.CrpPaymentSlip CrpReport = new FSBusinessLayer.Reports.CrpPaymentSlip();
            DataSet ds = null;
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            FromDate = parsed_date_fromdate.ToShortDateString();
            ToDate = parsed_date_todate.ToShortDateString();

            ds = RptAccountCtl.GetPaymentSlipData(int.Parse(DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 23:59:59"), Convert.ToInt64(this.ddlEmployee.SelectedValue));
            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Distributor", DrpLocation.SelectedItem.Text);
            CrpReport.SetParameterValue("FROM_DATE", txtFromDate.Text);
            CrpReport.SetParameterValue("TO_DATE", txtToDate.Text);
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", 1);
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