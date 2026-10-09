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
public partial class Forms_frmRecurrsiveInvoices : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    RoleManagementController RoleCtl = new RoleManagementController();
    ReportController ReportCtl = new ReportController();
    QuotationController QoutCtl = new QuotationController();
    PeopleController PplCtl = new PeopleController();
    EnquiryController EnqCtl = new EnquiryController();
    UserController UserCtl = new UserController();

    string strBody = string.Empty;
    string strSubject = string.Empty;
    string strSignature = string.Empty;
    string strTerms = string.Empty;
    string strSMTP = "";
    string strSMTPPassword = "";
    string strEmailFrom = "";
    string strPort = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"].ToString();

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
            this.GetInvoices();
            this.SetTableSorter();
            this.LoadEmailFrom();
            this.GetInvoiceTemplate();
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

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadProductCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        clsWebFormUtil.FillDropDownList(ddlCategory, m_dt, "REF_ID", "SLASH_DESC");
    }

    protected void GetInvoices()
    {
        ltrlReport.Text = "";
        
        DataTable dtReportReport = ReportCtl.GetRecurringInvoices(Convert.ToInt32(ddlCustomer.SelectedValue),Convert.ToInt32(ddlCategory.SelectedValue));
        gvRecurringInvoices.DataSource = dtReportReport;
        gvRecurringInvoices.DataBind();
        Session.Add("dtReport", dtReportReport);
        if (dtReportReport.Rows.Count < 1)
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }

    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInvoices();
        this.SetTableSorter();
    }

    protected void gvRecurringInvoices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            TextBox txtInvoiceDate = (TextBox)e.Row.FindControl("txtInvoiceDate");
            CheckBox cbSelect = (CheckBox)e.Row.Cells[4].FindControl("cbSelect");
            CheckBox cbSelectAll = (CheckBox)this.gvRecurringInvoices.HeaderRow.FindControl("cbSelectAll");
            cbSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", cbSelectAll.ClientID);

            DropDownList ddlEmailTo = (DropDownList)e.Row.FindControl("ddlEmailTo");
            DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, Convert.ToInt64(e.Row.Cells[2].Text), null, null);
            clsWebFormUtil.FillDropDownList(ddlEmailTo, dtPeople, "EMAIL2", "EmailName", true);

            DateTime now = DateTime.Now;
            DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
            txtInvoiceDate.Text = startOfMonth.ToString("dd-MMM-yyyy");

        }
    }

    protected void btnGenerateInvoice_Click(object sender, EventArgs e)
    {
        if (gvRecurringInvoices.Rows.Count > 0)
        {
            string InvoiceID = string.Empty;
            bool flag = true;
            int count = 0;            
            foreach (GridViewRow gvr in gvRecurringInvoices.Rows)
            {               
                CheckBox cbSelect = (CheckBox)gvr.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    count++;
                    long INVOICE_MASTER_ID = Convert.ToInt64(gvr.Cells[0].Text);
                    long ENQUIRY_MASTER_ID = Convert.ToInt64(gvr.Cells[1].Text);
                    long PeopleID = 0;                    
                    DropDownList ddlEmailTo = (DropDownList)gvr.FindControl("ddlEmailTo");
                    TextBox txtInvoiceDate = (TextBox)gvr.FindControl("txtInvoiceDate");

                    if(ddlEmailTo.Items.Count>0)
                    {

                        PeopleID = Convert.ToInt64(ddlEmailTo.SelectedValue.Split(',')[0]);
                    }
                    DateTime InvoiceDate = System.DateTime.Now;
                    try
                    {
                        InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
                    }
                    catch (Exception ex)
                    {
                        InvoiceDate = System.DateTime.Now;
                    }

                    string InvoiceFileName = gvr.Cells[5].Text + "_" + ENQUIRY_MASTER_ID.ToString() + "_" + (INVOICE_MASTER_ID + 1).ToString() + "_Invoice.pdf";
                    InvoiceID = QoutCtl.GenerateInvoice(INVOICE_MASTER_ID, InvoiceFileName, InvoiceDate, Convert.ToInt32(Session["UserID"]));
                    
                    if (Convert.ToInt64(InvoiceID) > 0)
                    {
                        CreateInvoiceFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName , Convert.ToInt64(InvoiceID), ENQUIRY_MASTER_ID, PeopleID);
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                    
                }
            }
            if (count == 0)
            {
                ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No Invoice selected.");
                return;
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Invoice(s) generated successfully.');", true);
                GetInvoices();
                this.SetTableSorter();
            }
            else
            {
                ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "Some erro occured.");
            }
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }

    private bool CreateInvoiceFile(string filePath, long InvoiceID, long EnquiryID, long PeopleID)
    {
        bool flag = false;

        try
        {
            DataTable dtReports = QoutCtl.GetInvoiceData(Convert.ToInt64(EnquiryID), InvoiceID, true);
            if (dtReports.Rows.Count > 0)
            {
                DataTable dtOriginator = PplCtl.GetPeople(PeopleID, Constants.LongNullValue, null, null);                
                
                CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                CrpReport = new FSBusinessLayer.Reports.CrpInvoice();

                CrpReport.SetDataSource(dtReports);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("Customer", dtOriginator.Rows[0]["FullName"].ToString());
                CrpReport.SetParameterValue("Email", dtOriginator.Rows[0]["EMAIL"].ToString());
                CrpReport.SetParameterValue("CustomerCompany", dtOriginator.Rows[0]["NAME"].ToString());
                DataTable dtTemplate = (DataTable)Session["dtTemplate"];
                CrpReport.SetParameterValue("Terms", dtTemplate.Rows[0]["TERMS"].ToString());

                CrpReport.SetDatabaseLogon(FSCommonLayer.Classes.Configuration.ServerUserName, FSCommonLayer.Classes.Configuration.ServerUserPwd, FSCommonLayer.Classes.Configuration.DatabseServer, FSCommonLayer.Classes.Configuration.DatabaseName);
                CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath);
                flag = true;
            }

        }
        catch (Exception ex)
        {
            flag = false;
            ltrlReport.Text = Utility.ShowAlert(false, ex.Message);
        }

        return flag;
    }

    private void GetInvoiceTemplate()
    {
        DataTable dtTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0, 727);
        DataTable dtEmailTemplateInvoice = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 1, 727);
        this.Session.Add("dtTemplate", dtTemplate);
        this.Session.Add("dtEmailTemplateInvoice", dtEmailTemplateInvoice);        
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetInvoices();
        this.SetTableSorter();
    }

    private void LoadEmailFrom()
    {
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null);        
        cblInvoiceCc.AddItems(dtPeople, "Email2", "EmailName");
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        if (gvRecurringInvoices.Rows.Count > 0)
        {
            int count = 0;
            bool flag = false;

            string EmailCc = string.Empty;
            CheckBoxList cblCc = cblInvoiceCc.GetAllItems();

            foreach (ListItem li in cblCc.Items)
            {
                if (li.Selected)
                {
                    EmailCc += "," + li.Value.Split(',')[1].ToString();
                }
            }

            string[] strEmailCc = EmailCc.Split(',');

            foreach (GridViewRow gvr in gvRecurringInvoices.Rows)
            {
                CheckBox cbSelect = (CheckBox)gvr.FindControl("cbSelect");
                long ENQUIRY_MASTER_ID = Convert.ToInt64(gvr.Cells[1].Text);
                if (cbSelect.Checked)
                {
                    count++;
                    string strEmailTo = string.Empty;
                    DropDownList ddlEmailTo = (DropDownList)gvr.FindControl("ddlEmailTo");


                    strEmailTo = ddlEmailTo.SelectedValue.Split(',')[1].ToString();

                    if (SetEmailIDs())
                    {
                        if (strEmailCc[0].ToString() != "" || strEmailTo != string.Empty || strEmailCc.Length > 1)
                        {
                            if (SendEmailInvoice(strEmailTo, strEmailCc, AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + gvr.Cells[3].Text))
                            {
                                flag = true;
                                QoutCtl.InsertQuotationReceipient(ENQUIRY_MASTER_ID, 1, Constants.ReceipientCustomer, Convert.ToInt64(gvr.Cells[2].Text), strEmailTo, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));

                                if (strEmailCc[0].ToString() != "" || strEmailCc.Length > 1)
                                {
                                    foreach (string strCc in strEmailCc)
                                    {
                                        QoutCtl.InsertQuotationReceipient(ENQUIRY_MASTER_ID, 1, Constants.ReceipientCustomer, Constants.LongNullValue, strCc, Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                                    }
                                }

                                EnqCtl.UpdateEnquiry(ENQUIRY_MASTER_ID, Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSent, true);

                                //Save Transaction in log
                                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Email", "Invoice for enquiry no " + ENQUIRY_MASTER_ID.ToString() + " sent");
                                
                            }
                            else
                            {
                                ltrlReport.Text = Utility.ShowAlert(false, "While sending email some error occured.");
                                break;
                            }
                        }                        
                    }
                }
            }

            if (count == 0)
            {
                ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No Invoice selected.");
                return;
            }
            if (flag && count > 0)
            {
                ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(true, "Invoice(s) sent successfully.");
            }
            else
            {
                ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "Some erro occured.");
            }
        }
        else
        {
            ltrlReport.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No reocrd found for selected crieteria.");
        }
    }

    private bool SendEmailInvoice(string p_EmailTo, string[] p_EmailCc, string p_Attachment)
    {
        bool flag = false;

        DataTable dtEmailTemplateInvoice = (DataTable)Session["dtEmailTemplateInvoice"];

        strSubject = "Invoice From FastServices";
        strBody += "Invoice From FastServices";

        if (dtEmailTemplateInvoice.Rows.Count > 0)
        {
            foreach (DataRow dr in dtEmailTemplateInvoice.Rows)
            {
                if (dr["EMAIL_TEMPLATE_ID"].ToString() == "10")
                {
                    strSubject = dr["SUBJECT"].ToString();
                    strBody += dr["BODY"].ToString();
                    strBody += "<br>";
                    strBody += dr["SIGNATURE"].ToString();

                    break;
                }
            }
        }

        int PortNo = 0;
        if (strPort != null)
        {
            if (strPort != "")
            {
                PortNo = Convert.ToInt32(strPort);
            }
        }
        if (Utility.SendEmail(strEmailFrom, p_EmailTo, p_EmailCc, strSMTP, strSMTPPassword, strSubject, strBody, p_Attachment, PortNo))
        {
            flag = true;
        }
        return flag;
    }

    private bool SetEmailIDs()
    {
        bool flag = true;
        if (Session["EmailAccount"] != null)
        {
            if (Session["EmailAccount"].ToString() != "")
            {
                strEmailFrom = Session["EmailAccount"].ToString();
                strSMTPPassword = Session["AccountPassword"].ToString();
                strSMTP = Session["SMTP"].ToString();
            }
        }
        return flag;
    }

    /// <summary>
    /// Sets Grids Columns For JQury Sorting
    /// </summary>
    private void SetTableSorter()
    {
        if (gvRecurringInvoices.Rows.Count > 1)
        {
            gvRecurringInvoices.UseAccessibleHeader = true;
            gvRecurringInvoices.HeaderRow.TableSection = TableRowSection.TableHeader;
        }        
    }
}