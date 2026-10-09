using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form For Petty Expense Report
/// </summary>
public partial class Forms_RptSalaryStatment : System.Web.UI.Page
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
            this.LoadAccountHead();
            this.LoadEmployee();
            FSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtStartDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
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
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetSalaryPaymentAccountHead(0);
        DrpAccountHead.Items.Add(new ListItem("All",Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4);

        DataTable dtComm = mAccountController.GetSalaryPaymentAccountHead(1);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dtComm, 0, 4);
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
        ddlEmployee.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(ddlEmployee, dtPeople, "PEOPLE_ID", "FullName");
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
        FSBusinessLayer.Reports.CrpPetyCashSummary CrpReport = new FSBusinessLayer.Reports.CrpPetyCashSummary();

        DataSet ds = RptAccountCtl.SelectPetyCashStatment(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), 0,Convert.ToInt32(DrpAccountHead.SelectedValue),Convert.ToInt32(ddlEmployee.SelectedValue));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Principal", "");
        CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
        CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportTitle", "Salary Statement");
        
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 0);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Petty Expense Report in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        DocumentPrintController mController = new DocumentPrintController();
        RptAccountController RptAccountCtl = new RptAccountController();

        DataTable dt = mController.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        FSBusinessLayer.Reports.CrpPetyCashSummary CrpReport = new FSBusinessLayer.Reports.CrpPetyCashSummary();
                
        DataSet ds = RptAccountCtl.SelectPetyCashStatment(int.Parse(drpDistributor.SelectedValue.ToString()), Constants.IntNullValue, DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"), int.Parse(this.Session["UserId"].ToString()), 0, Convert.ToInt32(DrpAccountHead.SelectedValue),Convert.ToInt32(ddlEmployee.SelectedValue));
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("Principal", "");
        CrpReport.SetParameterValue("Distributor", drpDistributor.SelectedItem.Text);
        CrpReport.SetParameterValue("FROM_DATE", txtStartDate.Text);
        CrpReport.SetParameterValue("TO_DATE", txtEndDate.Text);
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("ReportTitle", "Salary Statement");

        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", 1);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
}
