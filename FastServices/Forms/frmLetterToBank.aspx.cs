using System;
using System.Data;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Web.UI;

public partial class Forms_frmLetterToBank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            this.LoadBanks();
            this.LoadGrid();
        }
    }

    private void LoadBanks()
    {
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(FSCommonLayer.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4, true);
    }

    private void LoadGrid()
    {
        DateTime dtFrom = new DateTime();
        DateTime dtTo = new DateTime();

        DateTime dtFromMonth = DateTime.Parse(txtDate.Text);
        dtFrom = new DateTime(dtFromMonth.Year, dtFromMonth.Month, 1);

        DateTime dtToMonth = DateTime.Parse(txtDate.Text);
        dtTo = new DateTime(dtToMonth.Year, dtToMonth.Month, 1);
        dtTo = dtTo.AddMonths(1).AddDays(-1);

        RptAccountController Acc = new RptAccountController();
        DataTable dtEmployee = Acc.GetEmployeeChequeSalary(dtFrom, dtTo);
        this.Session.Add("dtEmployee", dtEmployee);
        gvEmployee.DataSource = dtEmployee;
        gvEmployee.DataBind();
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }

    private void ShowReport(int p_ReportType)
    {
        DocumentPrintController mController = new DocumentPrintController();
        
        FSBusinessLayer.Reports.CrpLetterToBank CrpReport = new FSBusinessLayer.Reports.CrpLetterToBank();
        DataTable dt = mController.SelectReportTitle(1);

        DataTable dtEmployee = (DataTable)Session["dtEmployee"];

        CrpReport.SetDataSource(dtEmployee);
        CrpReport.Refresh();

        string address = ddlBank.SelectedItem.Text + " ,</br>" + txtAddress1.Text + "</br>" + txtAddress2.Text;
        string para2 = "We request you to kindly Credit salary of our staff to their accounts vide Cheque No. " + txtChequeNo.Text + ". The detail of staff with account details is mentioned below:";
        CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());
        CrpReport.SetParameterValue("RefNo", "Ref #: " + txtRefNo.Text);
        CrpReport.SetParameterValue("Date", "Date: " + txtDate.Text);
        CrpReport.SetParameterValue("Address", address);
        CrpReport.SetParameterValue("From", txtSender.Text);
        CrpReport.SetParameterValue("Para2", para2);
        this.Session.Add("CrpReport", CrpReport);
        this.Session.Add("ReportType", p_ReportType);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);        
    }
}