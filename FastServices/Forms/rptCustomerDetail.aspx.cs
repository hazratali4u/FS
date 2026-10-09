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
public partial class Forms_rptCustomerDetail : System.Web.UI.Page
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
            this.LoadProductCategory();
        }
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

    protected void btnViewPDF_Click(object sender, EventArgs e)
    {
        DocumentPrintController DPrint = new DocumentPrintController();
        RptAccountController AccCtl = new RptAccountController();
        DataSet ds = null;        
        ds = AccCtl.GetCustomerDetail(Convert.ToInt32(ddlCategory.SelectedValue),1);

        FSBusinessLayer.Reports.CrpCustomerDetail CrpReport = new FSBusinessLayer.Reports.CrpCustomerDetail();
        CrpReport.SetDataSource(ds);
        CrpReport.Refresh();
        
        this.Session.Add("ReportType", 0);
        this.Session.Add("CrpReport", CrpReport);
        string url = "'Default.aspx'";
        string script = "<script language='JavaScript' type='text/javascript'> window.open(" + url + ",\"Link\",\"toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,resizable=1,width=800,height=600,left=10,top=10\");</script>";
        Type cstype = this.GetType();
        ClientScriptManager cs = Page.ClientScript;
        cs.RegisterStartupScript(cstype, "OpenWindow", script);
    }

    protected void btnViewExcel_Click(object sender, EventArgs e)
    {
        RptAccountController AccCtl = new RptAccountController();
        DataTable dtTaxSubmission = null;
        DataSetToExcel dsexcel = new DataSetToExcel();
        string path = FSCommonLayer.Classes.Configuration.GetAppInstallationPath() + "\\CustomerDetail.xls";
        dtTaxSubmission = AccCtl.GetCustomerDetail(Convert.ToInt32(ddlCategory.SelectedValue), 1).Tables["uspGetCustomerDetail"];
        DataSetToExcel.ExportDataTableToExcel(dtTaxSubmission, path, "uspGetCustomerDetail");
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
}