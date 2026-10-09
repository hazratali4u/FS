using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form For Petty Expense Summary Report
/// </summary>
public partial class Forms_RptPrincipalWiseExp : System.Web.UI.Page
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
            this.LoadAccountParent();
            this.LoadAccountHead();
            FSCommonLayer.Classes.Configuration.SystemCurrentDateTime = (DateTime)this.Session["CurrentWorkDate"];
            txtFromDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
            txtToDate.Text = FSCommonLayer.Classes.Configuration.SystemCurrentDateTime.ToString("dd-MMM-yyyy");
        }
    }    

    /// <summary>
    /// Loads Locations To Location Combo
    /// </summary>
    private void LoadDistributor()
    {
        DrpLocation.Items.Clear();
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        DrpLocation.Items.Add(new ListItem("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.DrpLocation, dt, 0, 2);

        
        
    }

    /// <summary>
    /// Loads Parent Account Heads To Parent Account Combo
    /// </summary>
    private void LoadAccountParent()
    {
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_DetailTypeId, long.Parse(FSCommonLayer.Classes.Configuration.ExpensDefaultType));
        clsWebFormUtil.FillDropDownList(DrpMasterHead, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads Account Heaeds To Account Combo
    /// </summary>
    private void LoadAccountHead()
    {
        if (DrpMasterHead.Items.Count > 0)
        {
            AccountHeadController mAccountController = new AccountHeadController();
            if (rblType.SelectedValue == "0")
            {
                DataTable dt = mAccountController.SelectAccountHead2(Constants.AC_AccountHeadId, long.Parse(DrpMasterHead.SelectedValue.ToString()),0);
                clsWebFormUtil.FillListBox(LstAccountHead, dt, 0, 4, true);
            }
            else
            {
                DataTable dt = mAccountController.GetAssignAccountHead(0, 47);
                clsWebFormUtil.FillListBox(LstAccountHead, dt, 0, 4, true);
            }
        }
    }

    /// <summary>
    /// Loads Account Heads
    /// </summary>
    protected void DrpMasterHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadAccountHead();
    }

    /// <summary>
    /// Shows Petty Expense Summary in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        ShowReport(0);
    }
    
    /// <summary>
    /// Shows Petty Expense Summary in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExcell_Click(object sender, EventArgs e)
    {
        ShowReport(1);
    }
    
    /// <summary>
    /// Shows Petty Expense Summary Report Either in Excel or PDF
    /// </summary>
    /// <param name="p_ReportType"></param>
    private void ShowReport(int p_ReportType)
    {
        try
        {
            FSBusinessLayer.Classes.DocumentPrintController DPrint = new FSBusinessLayer.Classes.DocumentPrintController();
            RptAccountController RptAccountCtl = new RptAccountController();

            DateTime parsed_date_fromdate = DateTime.Parse(this.txtFromDate.Text);
            DateTime parsed_date_todate = DateTime.Parse(this.txtToDate.Text);
            string FromDate = parsed_date_fromdate.ToShortDateString();
            string ToDate = parsed_date_todate.ToShortDateString();
            string Catagories_IDs = null;

            FSBusinessLayer.Reports.CrpPrincipalWiseExp CrpReport = new FSBusinessLayer.Reports.CrpPrincipalWiseExp();

            DataSet ds = null;

            for (int i = 0; i < this.LstAccountHead.Items.Count; i++)
            {
                if (this.LstAccountHead.Items[i].Selected == true)
                {
                    Catagories_IDs += this.LstAccountHead.Items[i].Value.ToString() + ",";
                }
            }

            ds = RptAccountCtl.PrincipalWiseSale(Constants.IntNullValue, Convert.ToInt32(this.DrpLocation.SelectedValue.ToString()), Convert.ToDateTime(FromDate + " 00:00:00"), Convert.ToDateTime(ToDate + " 23:59:59"), Catagories_IDs, Constants.IntNullValue);
            DataTable dt = DPrint.SelectReportTitle(int.Parse(DrpLocation.SelectedValue.ToString()));

            CrpReport.SetDataSource(ds);
            CrpReport.Refresh();

            CrpReport.SetParameterValue("Principal", "");
            CrpReport.SetParameterValue("Distributor", this.DrpLocation.SelectedItem.Text.ToString());
            CrpReport.SetParameterValue("From_date", this.txtFromDate.Text);
            CrpReport.SetParameterValue("To_Date", this.txtToDate.Text);

            if (rblType.SelectedValue == "0")
            {
                CrpReport.SetParameterValue("ReportTitle", "Petty Expense Summary");
                CrpReport.SetParameterValue("AccountHead", DrpMasterHead.SelectedItem.Text);
            }
            else
            {
                CrpReport.SetParameterValue("ReportTitle", "Payment Summary");
                CrpReport.SetParameterValue("AccountHead", "Purchases (Principal Wise)");
            }
            
            CrpReport.SetParameterValue("CompanyName", dt.Rows[0]["COMPANY_NAME"].ToString());

            this.Session.Add("CrpReport", CrpReport);
            this.Session.Add("ReportType", p_ReportType);
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

    protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChbAllAccountHead.Checked = false;
        this.LoadAccountHead();
    }
}
