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
public partial class Forms_rptPendingPayments : System.Web.UI.Page
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
        this.ddlCustomer.Items.Add(new clsListItems("All", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCustomer, m_dt, 0, 1);
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
        GeneratePendingPaymentReport();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        Response.Charset = "";
        Response.ContentType = "application/excel";
        StringBuilder sb = new StringBuilder();


        for (int k = 0; k < gvPendingPayment.Columns.Count; k++)
        {
            //add separator
            sb.Append(gvPendingPayment.Columns[k].HeaderText + ',');
        }
        sb.Append("\r\n");

        for (int i = 0; i < gvPendingPayment.Rows.Count; i++)
        {

            for (int k = 0; k < gvPendingPayment.Columns.Count - 2; k++)
            {
                sb.Append(gvPendingPayment.Rows[i].Cells[k].Text + ',');

            }

            sb.Append("\r\n");
        }

        Response.Output.Write(sb.ToString());

        Response.Flush();

        Response.End();
    }

    private void GeneratePendingPaymentReport()
    {
        DataTable dtReportReport = ReportCtl.GetPendingPaymentReport(Convert.ToInt32(ddlCustomer.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue));
        Session.Add("dtReport", dtReportReport);
        gvPendingPayment.DataSource = dtReportReport;
        gvPendingPayment.DataBind();

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