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
public partial class Forms_rptRecurringInvoice : System.Web.UI.Page
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
            this.LoadReportType();
            this.LoadCustomer();
            this.LoadProductCategory();
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

    private void LoadReportType()
    {
        this.ddlRecurringType.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        this.ddlRecurringType.Items.Add(new clsListItems("Monthly", "1"));
        this.ddlRecurringType.Items.Add(new clsListItems("Quaterly", "3"));
        this.ddlRecurringType.Items.Add(new clsListItems("Semi-Yearly", "4"));
        this.ddlRecurringType.Items.Add(new clsListItems("Yearly", "2"));
        this.ddlRecurringType.Items.Add(new clsListItems("Date Wise", "5"));
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

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        ltrlReport.Text = "";
        Session.Add("LevelType", Request.QueryString["LevelType"].ToString());
        Session.Add("TopID", Request.QueryString["TopID"].ToString());
        Session.Add("LevelID", Request.QueryString["LevelID"].ToString());        
        GenerateCurrentNotificationReport();
    }
      
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition","attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/excel";
        StringBuilder sb = new StringBuilder();
               

        for (int k = 0; k < gvNotification.Columns.Count; k++)
        {
            //add separator
            sb.Append(gvNotification.Columns[k].HeaderText + ',');
        }
        sb.Append("\r\n");

        for (int i = 0; i < gvNotification.Rows.Count; i++)
        {

            for (int k = 0; k < gvNotification.Columns.Count - 2; k++)
            {
                sb.Append(gvNotification.Rows[i].Cells[k].Text + ',');

            }

            sb.Append("\r\n");
        }

        Response.Output.Write(sb.ToString());

        Response.Flush();

        Response.End();
    }

    private void GenerateCurrentNotificationReport()
    {
        DataTable dtReportReport = ReportCtl.GetCurrentNotificationReport(Convert.ToInt32(ddlCustomer.SelectedValue), Convert.ToInt32(ddlRecurringType.SelectedValue),Convert.ToInt32(ddlCategory.SelectedValue));
        gvNotification.DataSource = dtReportReport;
        gvNotification.DataBind();
        Session.Add("dtReport", dtReportReport);
        if (dtReportReport.Rows.Count > 0)
        {
            btnExcel.Visible = true;
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }
}