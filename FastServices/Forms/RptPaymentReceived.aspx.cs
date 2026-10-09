using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using FSBusinessLayer.Reports;

/// <summary>
/// Form For Payment Receiving Report
/// </summary>
public partial class Forms_RptPaymentReceived : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.LoadDistributor();
            this.LoadProductCategory();
            this.LoadCustomer();
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
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
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

    /// <summary>
    /// Shows Payment Receiving in PDF
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewPDF_Click(object sender, EventArgs e)
    {        
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        string CustomerIDs = "";

        foreach (ListItem li in cblCustomer.Items)
        {
            if (li.Selected)
            {
                CustomerIDs += li.Value + ",";                
            }
        }

        CustomerIDs = CustomerIDs.Substring(0, CustomerIDs.Length - 1);

        DataControl dc = new DataControl();
        ds = RptCustomerCtl.SelectPaymentReceived(int.Parse(drpDistributor.SelectedValue.ToString()),Convert.ToInt32(ddlCategory.SelectedValue), CustomerIDs,
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        FSBusinessLayer.Reports.CrpPaymentReceiving CrpReport = new FSBusinessLayer.Reports.CrpPaymentReceiving();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Category", ddlCategory.SelectedItem.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);

        this.Session.Add("ReportType", 0);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    /// <summary>
    /// Shows Payment Receiving in Excel
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnViewExce_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptCustomerController RptCustomerCtl = new RptCustomerController();
        DataSet ds = null;
        DataTable dt = DPrint.SelectReportTitle(int.Parse(drpDistributor.SelectedValue.ToString()));
        string CustomerIDs = "";

        foreach (ListItem li in cblCustomer.Items)
        {
            if (li.Selected)
            {
                CustomerIDs += li.Value + ",";
            }
        }

        CustomerIDs = CustomerIDs.Substring(0, CustomerIDs.Length - 1);

        DataControl dc = new DataControl();
        ds = RptCustomerCtl.SelectPaymentReceived(int.Parse(drpDistributor.SelectedValue.ToString()), Convert.ToInt32(ddlCategory.SelectedValue), CustomerIDs,
            DateTime.Parse(txtStartDate.Text + " 00:00:00"), DateTime.Parse(txtEndDate.Text + " 23:59:59"));

        FSBusinessLayer.Reports.CrpPaymentReceiving CrpReport = new FSBusinessLayer.Reports.CrpPaymentReceiving();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();

        CrpReport.SetParameterValue("From_Date", DateTime.Parse(txtStartDate.Text));
        CrpReport.SetParameterValue("To_Date", DateTime.Parse(txtEndDate.Text));
        CrpReport.SetParameterValue("Category", ddlCategory.SelectedItem.Text);
        CrpReport.SetParameterValue("Location", drpDistributor.SelectedItem.Text);

        this.Session.Add("ReportType", 1);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }
    
    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        OrganizationController OrgCtl = new OrganizationController();
        DataTable m_dt = OrgCtl.GetOrganization(Convert.ToInt32(ddlCategory.SelectedValue), 1);// UserID is used for CategoryID
        clsWebFormUtil.FillListBox(this.cblCustomer, m_dt, 0, 1,true);
        foreach (ListItem li in cblCustomer.Items)
        {
            li.Selected = true;
        }

        cblCustomer.Attributes.Add("onclick", string.Format("javascript:CheckBoxListSelect({0})", cblCustomer.ClientID));
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCustomer();
    }
}
