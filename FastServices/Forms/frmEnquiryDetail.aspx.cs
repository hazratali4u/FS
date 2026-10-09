using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;

public partial class Forms_frmEnquiryDetail : System.Web.UI.Page
{
    #region Form Variables

    EnquiryController EnqCtl = new EnquiryController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    QuotationController QoutCtl = new QuotationController();
    OrganizationController OrgCtl = new OrganizationController();
    PeopleController PplCtl = new PeopleController();
    DocumentController DocCtl = new DocumentController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
    IndentController IndentCtl = new IndentController();
    RoleManagementController RoleCtl = new RoleManagementController();
    UserController UserCtl = new UserController();
    LedgerController LedgerCtl = new LedgerController();
    LedgerController LController = new LedgerController();

    DataTable dtQuotationDetail;
    DataTable dtEnquiryDetail;
    DataTable dtProductDetail;
    DataTable dtInvoiceDetail;

    private static int RowNo;
    DataControl dc = new DataControl();
    string EmailCc;
    DataTable dtVoucher;

    string strBody = string.Empty;
    string strSubject = string.Empty;
    string strSignature = string.Empty;
    string strTerms = string.Empty;
    string strSMTP = "";
    string strSMTPPassword = "";
    string strEmailFrom = "";
    string strPort = System.Configuration.ConfigurationManager.AppSettings["SMTPPort"].ToString();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtTabRights = RoleCtl.GetRoleFormTab(Convert.ToInt32(Request.QueryString["LevelID"]), Convert.ToInt32(Session["RoleID"]));
            this.Session.Add("dtTabRights", dtTabRights);

            ApplyRoles();

            hfEnquiryID.Value = Session["EnquiryMasterID"].ToString();

            this.CreateProductDetailTable();
            this.LoadCurrency();
            this.LoadEmailFrom();

            int RoleID = (int)Session["RoleID"];

            if (RoleID == 1)
            {
                lblQuotationFrom.Visible = true;
                ddlEmailFrom.Visible = true;

                ddlEmailFromQuot.Visible = true;
                lblEmailFrom.Visible = true;

                ddlEmailFromInvoice.Visible = true;
                lblEmailFromInvoice.Visible = true;
            }

            //Edit Tab
            lblDocumentNo.Text = Session["EnquiryMasterID"].ToString();
            lblDocumentNoView.Text = Session["EnquiryMasterID"].ToString();
            this.CreatTable();
            this.LoadDistributor();
            this.LoadCustomer();
            this.LoadOriginator();
            this.LoadDocumentDetail();
            btnSaveEnquiry.Attributes.Add("onclick", "return ValidateForm();");

            //Quotation Tab
            this.LoadEmailID();
            this.GetQuotationDetail();
            this.GetEmailFrom();
            //Invoice Tab            
            lblBillTo.Text = ddlOriginator.SelectedItem.Text;
            this.GetInvoiceTemplate();

            //Payment Tab
            //GetEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value));

            //Associated Documents Tab
            this.LoadDocumentCombo();
            this.LoadDocuments();

            if (Session["IsSaved"] != null)
            {
                if (Session["IsSaved"].ToString() == "Yes")
                {
                    hfTabIndex.Value = hfQuotationIndex.Value;
                    Session["IsSaved"] = null;
                }
            }
        }
    }

    #region Edit Tab

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        dtEnquiryDetail = new DataTable();
        dtEnquiryDetail.Columns.Add("SKU_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SKU_Name", typeof(string));
        dtEnquiryDetail.Columns.Add("Quantity", typeof(int));
        dtEnquiryDetail.Columns.Add("DOCUMENT_DATE", typeof(DateTime));
        dtEnquiryDetail.Columns.Add("ENQUIREY_DETAIL_ID2", typeof(long));
        this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);

    }

    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        int UserID = 0;
        UserID = Convert.ToInt32(Session["UserID"]);
        if (UserID > 0)
        {
            if (Session["RoleID"].ToString() == "1")
            {
                DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
            }
            else
            {
                DataTable m_dt = OrgCtl.GetOrganization(UserID, 0);
                clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
            }
        }
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Product Prices
    /// </summary>
    private void LoadSKUDetail(int p_CategoryID)
    {
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, p_CategoryID, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlProductEdit, Dtsku_Price, 0, 2, true);
        this.Session.Add("Dtsku_Price", Dtsku_Price);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadEditGird()
    {
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        gvMainEdit.DataSource = dtEnquiryDetail;
        gvMainEdit.DataBind();

        gvMainView.DataSource = dtEnquiryDetail;
        gvMainView.DataBind();

        //Get Quotation Data
        this.CreateQuotationTable();
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
        dtQuotationDetail = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value), false);

        if (dtQuotationDetail.Rows.Count <= 0)
        {
            foreach (DataRow dr in dtEnquiryDetail.Rows)
            {
                DataRow drQ = dtQuotationDetail.NewRow();
                drQ["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                drQ["SKU_Name"] = dr["SKU_NAME"].ToString();
                drQ["QUANTITY"] = dr["Quantity"].ToString();
                drQ["fnIsEnquiryPaymentReceived"] = false;
                drQ["IS_QUOTATION"] = false;
                drQ["DOCDATE"] = dr["DOCUMENT_DATE"];
                drQ["ENQUIREY_DETAIL_ID"] = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID2"]);
                dtQuotationDetail.Rows.Add(drQ);
            }
        }
        else
        {
            ddlTemplate.SelectedValue = dtQuotationDetail.Rows[0]["TEMPLATE_ID"].ToString();
            dtProductDetail = (DataTable)Session["dtProductDetail"];
            dtProductDetail = QoutCtl.GetQuotationSubDetail(Convert.ToInt64(dtQuotationDetail.Rows[0]["QUOTATION_MASTER_ID"]));
            Session.Add("dtProductDetail", dtProductDetail);

            if (dtEnquiryDetail.Rows.Count > dtQuotationDetail.Rows.Count)
            {
                foreach (DataRow dr in dtEnquiryDetail.Rows)
                {
                    var foundRecord = dtQuotationDetail.Select("SKU_ID = '" + dr["SKU_ID"].ToString() + "' AND ENQUIREY_DETAIL_ID = '" + dr["ENQUIREY_DETAIL_ID2"].ToString() + "'");
                    if (foundRecord.Length == 0)
                    {
                        DataRow drQ = dtQuotationDetail.NewRow();
                        drQ["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                        drQ["SKU_Name"] = dr["SKU_NAME"].ToString();
                        drQ["QUANTITY"] = dr["Quantity"].ToString();
                        drQ["DOCDATE"] = dr["DOCUMENT_DATE"];
                        drQ["ENQUIREY_DETAIL_ID"] = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID2"]);
                        drQ["IS_QUOTATION"] = false;
                        drQ["fnIsEnquiryPaymentReceived"] = false;
                        dtQuotationDetail.Rows.Add(drQ);
                    }

                }
            }

        }

        this.Session.Add("dtQuotationDetail", dtQuotationDetail);
        gvQuotation.DataSource = dtQuotationDetail;
        gvQuotation.DataBind();

        if (strSubject.Length > 0)
        {

            strSubject = strSubject.Substring(0, strSubject.Length - 1);

            if (strSubject.Contains(":"))
            {
                int indx = strSubject.LastIndexOf(":");
                strSubject = strSubject.Remove(indx, 1).Insert(indx, " & ");
                strSubject = strSubject.Replace(":", ", ");
            }

        }

        txtQuantity.Text = "";

        //Get Invoice Data

        DataTable dtEnquiryInvoice = QoutCtl.GetEnquiryInvoice(Convert.ToInt64(hfEnquiryID.Value),Constants.IntNullValue);
        gvAllInvoice.DataSource = dtEnquiryInvoice;
        gvAllInvoice.DataBind();

        long MaxInvoiceNo = Constants.LongNullValue;

        if (dtEnquiryInvoice.Rows.Count > 0)
        {
            MaxInvoiceNo = Convert.ToInt64(dtEnquiryInvoice.Rows[0]["INVOICE_MASTER_ID"]);
            hfMaxInvoiceID.Value = MaxInvoiceNo.ToString();

            if (Session["EnqStatus"].ToString() != "Inactive")
            {
                btnNewInvoice.Visible = true;
                btnNewInvoiceFromQuotion.Visible = true;
            }

            DataTable dtInv = dtEnquiryInvoice.Clone();

            DataRow[] foundRows = dtEnquiryInvoice.Select("IS_ACTIVE='Active'");
            if (foundRows.Length > 0)
            {
                for (int k = 0; k < foundRows.Length; k++)
                {
                    dtInv.ImportRow(foundRows[k]);
                }
            }            

            clsWebFormUtil.FillDropDownList(this.ddlPaymentProduct, dtInv, "INVOICE_MASTER_ID", "REFERENCE_NO", true);

            gvInvoice.Visible = false;
            btnSaveInvoice.Visible = false;
            lblInvoiceDate.Visible = false;
            txtInvoiceDate.Visible = false;
            lblUOM.Visible = false;
            ddlUOM.Visible = false;
            lblCurrency.Visible = false;
            ddlCurrency.Visible = false;
            lblConversion.Visible = false;
            txtConversion.Visible = false;
            lblIncomeTax.Visible = false;
            txtIncomeTax.Visible = false;
            lblServiceTax.Visible = false;
            txtServiceTax.Visible = false;
            lblPONo.Visible = false;
            lblPODate.Visible = false;
            txtPONo.Visible = false;
            txtPODate.Visible = false;
        }

        this.CreateInvoiceTable();
        dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
        dtInvoiceDetail = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), MaxInvoiceNo, false);

        if (dtInvoiceDetail.Rows.Count <= 0)
        {
            foreach (DataRow dr in dtQuotationDetail.Rows)
            {
                if (dr["IS_QUOTATION"].ToString() == "True")
                {
                    DataRow drI = dtInvoiceDetail.NewRow();
                    drI["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                    drI["SKU_Name"] = dr["SKU_NAME"].ToString();
                    drI["QUANTITY"] = dr["Quantity"].ToString();
                    drI["RATE"] = dr["RATE"].ToString();
                    drI["CURRENCY_ID"] = dr["CURRENCY_ID"].ToString();
                    drI["IS_INVOICE"] = false;
                    drI["DOCDATE"] = dr["DOCDATE"];
                    drI["SUBSCRIPTION_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                    drI["EXPIRY_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                    drI["ENQUIREY_DETAIL_ID"] = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
                    drI["fnIsEnquiryPaymentReceived"] = false;
                    dtInvoiceDetail.Rows.Add(drI);
                }
            }
        }
        

        lblInvoiceReference.Text = hfEnquiryID.Value + "-" + System.DateTime.Now.ToString("MM-yy") + "-" + (Convert.ToInt32(gvAllInvoice.Rows.Count + 1).ToString());
        lblInvoiceNo.Text = lblInvoiceReference.Text;
                
        this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
        gvInvoice.DataSource = dtInvoiceDetail;
        gvInvoice.DataBind();

        if (gvInvoice.Rows.Count > 0 && gvInvoice.Visible)
        {
            lblInvoiceDate.Visible = true;
            txtInvoiceDate.Visible = true;
            lblUOM.Visible = true;
            ddlUOM.Visible = true;
            lblCurrency.Visible = true;
            ddlCurrency.Visible = true;
            lblConversion.Visible = true;
            txtConversion.Visible = true;
            lblIncomeTax.Visible = true;
            txtIncomeTax.Visible = true;
            lblServiceTax.Visible = true;
            txtServiceTax.Visible = true;
            lblPONo.Visible = true;
            lblPODate.Visible = true;
            txtPONo.Visible = true;
            txtPODate.Visible = true;
        }
    }

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveEnquiry_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        ltrlEdit.Text = "";
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];

        if (ddlProductEdit.Items.Count > 0 && int.Parse(dc.chkNull_0(txtQuantity.Text)) > 0)
        {
            if (btnSaveEnquiry.Text == "Add Product")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtEnquiryDetail.NewRow();
                    dr["SKU_ID"] = Convert.ToInt32(ddlProductEdit.SelectedValue);
                    dr["SKU_Name"] = ddlProductEdit.SelectedItem.Text;
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["DOCUMENT_DATE"] = System.DateTime.Now.ToShortDateString();
                    dr["ENQUIREY_DETAIL_ID2"] = Constants.LongNullValue;
                    dtEnquiryDetail.Rows.Add(dr);
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false, "This Product Already Exists");
                    return;
                }
            }
            else if (btnSaveEnquiry.Text == "Update Product")
            {
                DataRow dr = dtEnquiryDetail.Rows[RowNo];
                dr["SKU_ID"] = Convert.ToInt32(ddlProductEdit.SelectedValue);
                dr["SKU_Name"] = ddlProductEdit.SelectedItem.Text;
                dr["Quantity"] = int.Parse(txtQuantity.Text);
                btnSaveEnquiry.Text = "Add Product";
            }

            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.LoadEditGird();
            ScriptManager.GetCurrent(Page).SetFocus(txtQuantity);
        }
        else
        {
            ltrlEdit.Text = Utility.ShowAlert(false, "Wrong Product or Quantity Selected");
        }

        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        //FIS
        if (FSCommonLayer.Classes.Configuration.Company == 1)
        {
            return true;
        }
        
        
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        DataRow[] foundRows = dtEnquiryDetail.Select("SKU_ID  = '" + ddlProductEdit.SelectedValue + "' AND DOCUMENT_DATE = '" + System.DateTime.Now.ToShortDateString() + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMainEdit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        RowNo = e.NewEditIndex;
        ddlProductEdit.SelectedValue = gvMainEdit.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Text = gvMainEdit.Rows[e.NewEditIndex].Cells[2].Text;
        txtQuantity.Focus();
        btnSaveEnquiry.Text = "Update Product";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMainEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        if (dtEnquiryDetail.Rows.Count > 0)
        {
            dtEnquiryDetail.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.LoadEditGird();
        }
    }

    protected void gvMainEdit_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;

            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnEdit);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnDelete);
        }
    }

    protected void gvMainEdit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[6].Text == "True")
            {
                e.Row.Cells[4].Text = "";
                e.Row.Cells[5].Text = "";
            }
            if (e.Row.Cells[7].Text == "True")
            {
                e.Row.Cells[5].Text = "";
            }
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        int UserID = 0;
        UserID = Convert.ToInt32(Session["UserID"]);
        if (UserID > 0)
        {
            dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
            if (dtEnquiryDetail.Rows.Count > 0)
            {
                DistributorController mDayClose = new DistributorController();
                if (SaveEnquiry(System.DateTime.Now))
                {
                    EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), Constants.DateNullValue, Constants.IntNullValue, null, UserID, Constants.Modified, true);
                    ltrlEdit.Text = Utility.ShowAlert(true, "Enquiry Updated Successfuly.");
                    LoadDocumentDetail();
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false, "Some error occured. Enquiry not updated.");
                }
            }
            else
            {
                ltrlEdit.Text = Utility.ShowAlert(false, "At least one Product must enter.");
            }

            ltrlInvoice.Text = "";
            ltrlPayment.Text = "";
            ltrlQuotation.Text = "";
            ltrlSendEmail.Text = "";
            ltrlDoc.Text = "";
        }
        else
        {
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("../Login.aspx");
        }
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool SaveEnquiry(DateTime MWorkDate)
    {
        bool flag = false;
        int UserID = 0;
        UserID = Convert.ToInt32(this.Session["UserId"]);
        if (UserID > 0)
        {
            DataTable dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
            flag = EnqCtl.UpdateEnquiry(Constants.IntNullValue, Convert.ToInt32(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), MWorkDate, Convert.ToInt32(DrpCustomer.SelectedValue), txtRemarks.Text, UserID, Constants.Modified, Convert.ToInt64(ddlOriginator.SelectedValue), dtEnquiryDetail);
            //Save Transaction in log
            if (flag)
            {
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Enquiry", "Enquiry No " + hfEnquiryID.Value + " updated");
            }
        }
        return flag;
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        EnquiryController mEnq = new EnquiryController();
        DataTable dt = mEnq.GetEnquiryMaster(Constants.IntNullValue, Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue, Constants.IntNullValue);

        if (dt.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            hfCustomerID.Value = dt.Rows[0]["CUSTOMER_ID"].ToString();
            this.LoadOriginator();
            ddlOriginator.SelectedValue = dt.Rows[0]["PEOPLE_ID"].ToString();
            lblOriginatorView.Text = ddlOriginator.SelectedItem.Text;

            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

            lblLocation.Text = ddlLocation.SelectedItem.Text;
            lblCustomerView.Text = DrpCustomer.SelectedItem.Text;
            lblCustomerMain.Text = DrpCustomer.SelectedItem.Text;
            lblRemarksView.Text = dt.Rows[0]["REMARKS"].ToString();

            lblCategoryView.Text = dt.Rows[0]["SLASH_DESC"].ToString();
            lblCategoryEdit.Text = dt.Rows[0]["SLASH_DESC"].ToString();

            dtEnquiryDetail = mEnq.GetEnquiryDetail(long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);

            this.LoadSKUDetail(Convert.ToInt32(dt.Rows[0]["TYPE_ID"]));
            this.Session.Add("CategoryID", Convert.ToInt32(dt.Rows[0]["TYPE_ID"]));
            this.GetQuotationDetail(Convert.ToInt32(dt.Rows[0]["TYPE_ID"]));

            LoadEditGird();
        }
    }

    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        this.LoadOriginator();
    }

    private void LoadOriginator()
    {
        ddlOriginator.Items.Clear();
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, Convert.ToInt64(DrpCustomer.SelectedValue), null, null);
        ddlOriginator.Items.Add(new clsListItems("---Select---", Constants.IntNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlOriginator, dtPeople, "PEOPLE_ID", "FullName");
        this.Session.Add("dtOriginator", dtPeople);
    }

    #endregion

    #region Quotation Tab

    protected void btnSaveQuotation_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
        strSubject = "";

        if (ddlTemplate.Items.Count > 0)
        {
            foreach (GridViewRow gvr in gvQuotation.Rows)
            {
                DropDownList ddlCurrency = gvr.FindControl("ddlCurrency") as DropDownList;
                TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
                TextBox txtRemarks = gvr.FindControl("txtRemarks") as TextBox;
                TextBox txtQuantity = (TextBox)gvr.FindControl("txtQuantity");
                CheckBox cbQuotation = gvr.FindControl("cbQuotation") as CheckBox;
                decimal Rate = 0;
                int Qty = 0;

                if (cbQuotation.Enabled)
                {
                    foreach (DataRow dr in dtQuotationDetail.Rows)
                    {
                        if (dr["SKU_ID"].ToString() == gvr.Cells[0].Text && dr["ENQUIREY_DETAIL_ID"].ToString() == gvr.Cells[12].Text)
                        {
                            dr["CURRENCY_ID"] = Convert.ToInt32(ddlCurrency.SelectedValue);

                            try
                            {
                                Rate = Convert.ToDecimal(txtRate.Text);
                            }
                            catch (Exception ex)
                            {
                                Rate = 0;
                            }

                            try
                            {
                                Qty = Convert.ToInt32(txtQuantity.Text);
                            }
                            catch (Exception ex)
                            {
                                Qty = 0;
                            }

                            dr["QUANTITY"] = Qty;
                            dr["RATE"] = Rate;
                            dr["DURATION"] = txtRemarks.Text;
                            dr["IS_QUOTATION"] = cbQuotation.Checked;
                            dr["ENQUIREY_DETAIL_ID"] = gvr.Cells[12].Text;

                            if (cbQuotation.Checked)
                            {
                                strSubject += gvr.Cells[3].Text + ":";
                            }

                            break;
                        }

                    }
                }
            }

            dtQuotationDetail.AcceptChanges();

            dtProductDetail = (DataTable)Session["dtProductDetail"];

            bool flag = false;

            if (dtQuotationDetail.Rows.Count > 0)
            {
                int UserID = Convert.ToInt32(Session["UserID"]);
                if (UserID > 0)
                {
                    flag = QoutCtl.InsertQuotation(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToInt32(ddlTemplate.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtQuotationDetail, dtProductDetail);
                    if (flag)
                    {
                        EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.QuotationPrepared, true);

                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Quotation", "Quotation for enquiry no " + hfEnquiryID.Value + " added");

                        ltrlQuotation.Text = Utility.ShowAlert(true, "Quotation Saved Successfully.");
                        this.LoadDocuments();

                    }
                    else
                    {
                        ltrlQuotation.Text = Utility.ShowAlert(false, "Some error Occured.");
                    }
                }
                else
                {
                    Session.Clear();
                    System.Web.Security.FormsAuthentication.SignOut();
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                ltrlQuotation.Text = Utility.ShowAlert(false, "No product selected.");
            }
        }
        else
        {
            ltrlQuotation.Text = Utility.ShowAlert(false, "No Template found for " + lblCategoryEdit.Text);
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnSendQuotation_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;
        ltrlQuotation.Text = "";
        btnSaveQuotation_Click(null, null);
        string QtnFileName = hfEnquiryID.Value + "_" + DateTime.Now.ToString("yyyyMdHHmmss") + "_" + Session["UserName2"].ToString() + "_Quotation.pdf";

        if (CreateFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + QtnFileName))
        {
            if (SetEmailIDs(ltrlQuotation, ddlEmailFrom))
            {
                EmailCc = txtCc2.Text;

                CheckBoxList cblQuotation = cblQuotationCc.GetAllItems();

                foreach (ListItem li in cblQuotation.Items)
                {
                    if (li.Selected)
                    {
                        EmailCc += "," + li.Value.Split(',')[1].ToString();
                    }
                }


                string[] strEmailCc = EmailCc.Split(',');

                string strEmailTo = string.Empty;
                DataTable dtEmailTo = (DataTable) Session["dtEmailTo"];

                foreach (DataRow dr in dtEmailTo.Rows)
                {
                    if (dr["PEOPLE_ID"].ToString() == ddlEmailToQuotation.SelectedValue)
                    {
                        strEmailTo = dr["EMAIL"].ToString();
                        break;
                    }
                }

                if (strEmailCc[0].ToString() != "" || strEmailTo != string.Empty || strEmailCc.Length > 1)
                {
                    if (SendEmail(strEmailTo, strEmailCc, AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + QtnFileName))
                    {
                        QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), strEmailTo, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));

                        if (strEmailCc[0].ToString() != "" || strEmailCc.Length > 1)
                        {
                            foreach (string strCc in strEmailCc)
                            {
                                QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Constants.LongNullValue, strCc, Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                            }
                        }

                        EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.QuotationSent, true);

                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Quotation", "Quotation for enquiry no " + hfEnquiryID.Value + " sent");

                        ltrlQuotation.Text = Utility.ShowAlert(true, "Quotation Sent Successfully.");
                    }
                    else
                    {
                        ltrlQuotation.Text = Utility.ShowAlert(false, "While sending email some error occured.");
                    }
                }
                else
                {
                    ltrlQuotation.Text = Utility.ShowAlert(false, "No email selected.");
                }
            }

            DocCtl.InsertUploadDocument(Convert.ToInt64(Session["EnquiryMasterID"]), 686, Convert.ToInt32(Session["UserID"]), QtnFileName, "Quotation");
            LoadDocuments();

        }
        else
        {
            ltrlQuotation.Text = Utility.ShowAlert(false, "No product selected.");
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnWord_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;

        btnSaveQuotation_Click(null, null);

        DataTable dtTemplate = Session["dtTemplate"] as DataTable;
        DataTable dtPeople = PplCtl.GetPeople(Convert.ToInt64(ddlEmailToQuotation.SelectedValue), Constants.LongNullValue, null, null);
        StringBuilder str = new StringBuilder();
        str.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'>");
        str.Append("<head>");
        str.Append("<!--[if gte mso 9]> <xml> <w:WordDocument> <w:View>Print</w:View> <w:Zoom>100</w:Zoom> <w:DoNotOptimizeForBrowser/> </w:WordDocument> </xml> <![endif]-->");
        str.Append("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"\"text/html; charset=UTF-8\"\">");
        str.Append("<meta name=ProgId content=Word.Document>");
        str.Append("<meta name=Generator content=\"Microsoft Word 9\">");
        str.Append("<meta name=Originator content=\"Microsoft Word 9\">");

        str.Append("<style>");
        str.Append("@page Section2 {margin:0.1in 0.1in 0in 0.1in;mso-header-margin:0.1in;mso-footer-margin:0.1in;mso-paper-source:0;}");
        str.Append("div.Section2 {page:Section2;}");
        str.Append(@" p.MsoFooter, li.MsoFooter, div.MsoFooter");
        str.Append("{");
        str.Append("margin:0in 0in 0in 0in");
        str.Append("margin-bottom:.0001pt");
        str.Append("mso-pagination:widow-orphan");
        str.Append("tab-stops:center 3.0in right 6.0in");
        str.Append("font-size:12.0pt");
        str.Append("}");
        str.Append("</style>");

        str.Append("</head>");

        str.Append("<body>");
        str.Append("<div class=Section2 >");
        str.Append("<table width='100%' cellspacing='0' cellpadding='0'>");
        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        str.Append("<br>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        str.Append("<br>");
        str.Append("<br>");
        str.Append("<br>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        str.Append("<table width='100%' cellspacing='0' cellpadding='0'>");
        str.Append("<tr>");
        str.Append("<td style='width:50%;font-family:Calibri;font-size:10pt;'>");
        str.Append("Kind Attn: " + "<b>" + dtPeople.Rows[0]["FullName"].ToString() + "</b>");
        str.Append("<br>");
        str.Append("<span style='color:Blue'>" + dtPeople.Rows[0]["Email"].ToString() + "</span>");
        str.Append("<br>");
        str.Append(dtPeople.Rows[0]["SLASH_DESC"].ToString());
        str.Append("<br>");
        str.Append(dtPeople.Rows[0]["NAME"].ToString());
        str.Append("</td>");
        str.Append("<td align='right' style='width:50%'>");
        str.Append("<img src='" + System.Configuration.ConfigurationManager.AppSettings["ImagePath"].ToString() + "GoogleLogo.jpg'>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("</table>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");


        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        str.Append("<br>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(dtTemplate.Rows[0]["BODY"].ToString());
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");

        str.Append("<table width='100%' cellspacing='0' cellpadding='0'>");

        str.Append("<tr>");
        str.Append("<td style='width:50%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:White;background-color:Blue'>");
        str.Append("<b>Product</b>");
        str.Append("</td>");
        str.Append("<td align='center' style='width:10%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:White;background-color:Blue'>");
        str.Append("<b>Quantity</b>");
        str.Append("</td>");
        str.Append("<td align='right' style='width:40%;border-style: solid solid solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:White;background-color:Blue'>");
        str.Append("<b>Total Amount (Without Tax)</b>");
        str.Append("</td>");
        str.Append("</tr>");
        foreach (GridViewRow gvr in gvQuotation.Rows)
        {
            DropDownList ddlCurrency = gvr.FindControl("ddlCurrency") as DropDownList;
            TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
            TextBox txtQuantity = gvr.FindControl("txtQuantity") as TextBox;
            TextBox txtRemarks = gvr.FindControl("txtRemarks") as TextBox;
            CheckBox cbQuotation = gvr.FindControl("cbQuotation") as CheckBox;
            if (cbQuotation.Checked)
            {
                str.Append("<tr>");
                str.Append("<td style='width:50%;border-style: none solid solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:Blue'>");
                str.Append(gvr.Cells[3].Text);
                str.Append("<table>");
                str.Append("<tr>");
                str.Append("<td style='font-family:Calibri;font-size:10pt'>");
                dtProductDetail = (DataTable)Session["dtProductDetail"];

                if (dtProductDetail.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProductDetail.Rows)
                    {
                        if (dr["ENQUIREY_DETAIL_ID"].ToString() == gvr.Cells[0].Text)
                        {
                            str.Append(dr["PRODUCT_DETAIL"].ToString());
                        }
                    }
                }
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                str.Append("</td>");
                str.Append("<td align='center' style='width:10%;border-style: none solid solid none; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:Blue'>");
                str.Append(txtQuantity.Text);
                str.Append("</td>");
                str.Append("<td align='right' style='width:40%;border-style: none solid solid none; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;border-color:Blue'>");
                str.Append(ddlCurrency.SelectedItem.Text + " " + Convert.ToDecimal(txtRate.Text).ToString("##,0.00") + " " + txtRemarks.Text);
                str.Append("</td>");
                str.Append("</tr>");
            }
        }

        str.Append("</table>");



        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        str.Append("<br>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(dtTemplate.Rows[0]["SIGNATURE"].ToString());
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");


        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(lblFrom.Text);
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(lblFromDesignation.Text);
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append("Cell No:" + lblFromPhone.Text);
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(lblFromAddress.Text);
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%;font-family:Calibri;font-size:10pt;'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(lblFromEmail.Text);
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");


        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        str.Append("<br><br><br>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("<tr>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("<td style='width:85%'>");
        if (dtTemplate.Rows.Count > 0)
        {
            str.Append(dtTemplate.Rows[0]["TERMS"].ToString());
        }
        str.Append("</td>");
        str.Append("<td style='width:5%'>");
        str.Append("</td>");
        str.Append("</tr>");

        str.Append("</table>");

        str.Append("</div>");
        str.Append("</body>");
        str.Append("</html >");
        Response.AppendHeader("Content-Type", "application/msword");
        Response.ContentEncoding = System.Text.UnicodeEncoding.UTF8;
        Response.Charset = "UTF-8";
        Response.AppendHeader("Content-disposition", "attachment; filename=" + "Proposal for " + ddlTemplate.SelectedItem.Text + "-" + System.DateTime.Now.ToString("dd-MM-yyyy") + ".doc");
        Response.Write(str.ToString());

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnPDF_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;

        btnSaveQuotation_Click(null, null);

        string QtnFileName = hfEnquiryID.Value + "_" + DateTime.Now.ToString("yyyyMdHHmmss") + "_" + Session["UserName2"].ToString() + "_Quotation.pdf";

        if (CreateFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + QtnFileName))
        {
            DownLoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + QtnFileName);
        }
        else
        {
            ltrlQuotation.Text = Utility.ShowAlert(false, "No product selected.");
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    private void GetQuotationDetail(int CategoryID)
    {
        DataTable dt = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0, CategoryID);
        clsWebFormUtil.FillDropDownList(ddlTemplate, dt, "EMAIL_TEMPLATE_ID", "SUBJECT",true);
        this.Session.Add("dtTemplate", dt);

        DataTable dtEmailTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 1, CategoryID);
        clsWebFormUtil.FillDropDownList(ddlEmailTemplate, dtEmailTemplate, "EMAIL_TEMPLATE_ID", "SUBJECT",true);
        clsWebFormUtil.FillDropDownList(ddlQuotationEmailTemplate, dtEmailTemplate, "EMAIL_TEMPLATE_ID", "SUBJECT",true);
        this.Session.Add("dtEmailTemplate", dtEmailTemplate);
    }

    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;
        GetQuotationDetail();
    }

    protected void ddlEmailFrom_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;
        GetEmailFrom();
    }

    private string GetOutationAttachment(string FolderPath)
    {
        string filePath = "";
        string[] QuotationFiles = System.IO.Directory.GetFiles(FolderPath);

        if (QuotationFiles.Length > 0)
        {
            for (int i = QuotationFiles.Length - 1; i >= 0; i--)
            {
                if (QuotationFiles[i].ToString().Contains("_"))
                {
                    if (QuotationFiles[i].ToString().Substring(QuotationFiles[i].IndexOf("Docs") + 5, QuotationFiles[i].IndexOf("_") - QuotationFiles[i].IndexOf("Docs") - 5) == hfEnquiryID.Value)
                    {
                        if (QuotationFiles[i].ToString().Contains("Quotation.pdf"))
                        {
                            filePath = QuotationFiles[i].ToString();
                            break;
                        }
                    }
                }
            }
        }

        return filePath;
    }

    private bool SetEmailIDs(Literal ltrl, DropDownList ddl)
    {
        bool flag = true;
        if (ddl.Visible)
        {
            DataTable dtSmtp = UserCtl.SelectSlashUser(Convert.ToInt32(ddl.SelectedValue));

            if (dtSmtp.Rows.Count > 0)
            {
                strEmailFrom = dtSmtp.Rows[0]["EMAIL_ACCOUNT"].ToString();
                strSMTPPassword = dtSmtp.Rows[0]["EMAIL_PASSWORD"].ToString();
                strSMTP = dtSmtp.Rows[0]["SMTP_ADDRESS"].ToString();
            }
        }
        else
        {
            if (Session["EmailAccount"] != null)
            {
                if (Session["EmailAccount"].ToString() != "")
                {
                    strEmailFrom = Session["EmailAccount"].ToString();
                    strSMTPPassword = Session["AccountPassword"].ToString();
                    strSMTP = Session["SMTP"].ToString();
                }
            }
        }

        if (strEmailFrom == "" || strSMTP == "" || strSMTPPassword == "")
        {
            ltrl.Text = Utility.ShowAlert(false, "Error in email configuration. Please contact to administrator.");
            flag = false;
        }
        return flag;
    }

    private void LoadEmailID()
    {
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, Convert.ToInt64(hfCustomerID.Value), null, null,Constants.IntNullValue,1);
        if (dtPeople.Rows.Count > 0)
        {
            clsWebFormUtil.FillDropDownList(this.ddlEmailTo, dtPeople, "PEOPLE_ID", "EmailName");
            clsWebFormUtil.FillDropDownList(this.ddlEmailToInvoice, dtPeople, "PEOPLE_ID", "EmailName");
            clsWebFormUtil.FillDropDownList(this.ddlEmailToQuotation, dtPeople, "PEOPLE_ID", "EmailName");
            Session.Add("dtEmailTo", dtPeople);
            lblBillCompany.Text = dtPeople.Rows[0]["NAME"].ToString();
            Session.Add("dtByerContact", dtPeople);
            cblQuotationCc2.AddItems(dtPeople, "EMAIL2", "EmailName");
            cblInvoiceCc.AddItems(dtPeople, "EMAIL2", "EmailName");
            cblQuotationCc.AddItems(dtPeople, "EMAIL2", "EmailName");
        }

    }

    private void LoadEmailFrom()
    {
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null,Constants.IntNullValue,1);
        clsWebFormUtil.FillDropDownList(this.ddlEmailFromQuot, dtPeople, "PEOPLE_ID", "EmailName");
        clsWebFormUtil.FillDropDownList(this.ddlEmailFrom, dtPeople, "PEOPLE_ID", "EmailName");
        clsWebFormUtil.FillDropDownList(this.ddlEmailFromInvoice, dtPeople, "PEOPLE_ID", "EmailName");

        ddlEmailFromQuot.SelectedValue = Session["PEOPLE_ID"].ToString();
        ddlEmailFrom.SelectedValue = Session["PEOPLE_ID"].ToString();
        ddlEmailFromInvoice.SelectedValue = Session["PEOPLE_ID"].ToString();
        cblQuotationCc2.AddItems(dtPeople, "EMAIL2", "EmailName");
        cblInvoiceCc.AddItems(dtPeople, "EMAIL2", "EmailName");
        cblQuotationCc.AddItems(dtPeople, "EMAIL2", "EmailName");
    }

    private bool SendEmail(string p_EmailTo, string[] p_EmailCc, string p_Attachment)
    {
        bool flag = false;

        DataTable dtEmailTemplate = (DataTable)Session["dtEmailTemplate"];

        if (dtEmailTemplate.Rows.Count > 0)
        {
            foreach (DataRow dr in dtEmailTemplate.Rows)
            {
                if (dr["EMAIL_TEMPLATE_ID"].ToString() == ddlQuotationEmailTemplate.SelectedValue.ToString())
                {
                    strSubject = dr["SUBJECT"].ToString();
                    strBody = dr["BODY"].ToString();
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

    private bool CreateFile(string filePath)
    {
        bool flag = false;

        try
        {
            DataTable dtReports = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value), true);
            if (dtReports.Rows.Count > 0)
            {                
                DataTable dtPeople = PplCtl.GetPeople(Convert.ToInt64(ddlEmailFrom.SelectedValue), Constants.LongNullValue, null, null);
                DataTable dtOriginator = PplCtl.GetPeople(Convert.ToInt64(ddlEmailToQuotation.SelectedValue), Constants.LongNullValue, null, null);

                DataTable dtTemplate = (DataTable)Session["dtTemplate"];


                strBody = dtTemplate.Rows[0]["BODY"].ToString();
                strSignature = dtTemplate.Rows[0]["SIGNATURE"].ToString();
                strTerms = dtTemplate.Rows[0]["TERMS"].ToString();

                CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                if (Session["CategoryID"].ToString() == "712")
                {
                    if (ddlTemplate.SelectedValue == "12")
                    {
                        CrpReport = new FSBusinessLayer.Reports.CrpQuotationValut();
                    }
                    else
                    {
                        CrpReport = new FSBusinessLayer.Reports.CrpQuotationGoogle();
                    }
                }
                else
                {
                    CrpReport = new FSBusinessLayer.Reports.CrpQuotation();
                }

                CrpReport.SetDataSource(dtReports);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("Customer", dtOriginator.Rows[0]["FullName"].ToString());
                CrpReport.SetParameterValue("CustomerDesignation", dtOriginator.Rows[0]["SLASH_DESC"].ToString());
                CrpReport.SetParameterValue("CustomerCompany", dtOriginator.Rows[0]["NAME"].ToString());
                CrpReport.SetParameterValue("CustomerAddress", dtOriginator.Rows[0]["PeopleAddress"].ToString());
                CrpReport.SetParameterValue("Email", dtOriginator.Rows[0]["EMAIL"].ToString());


                CrpReport.SetParameterValue("Subject", "Proposal for " + strSubject);
                CrpReport.SetParameterValue("Body", ltrlBody.Text);
                CrpReport.SetParameterValue("Signature", ltrlSignature.Text);
                CrpReport.SetParameterValue("Terms", ltrlTerms.Text);                

                if (dtPeople.Rows.Count > 0)
                {
                    CrpReport.SetParameterValue("From", dtPeople.Rows[0]["FullName"].ToString());
                    CrpReport.SetParameterValue("FromDesignation", dtPeople.Rows[0]["SLASH_DESC"].ToString());
                    CrpReport.SetParameterValue("FromPhone", dtPeople.Rows[0]["PHONE"].ToString());
                    CrpReport.SetParameterValue("FromAddress", dtPeople.Rows[0]["PeopleAddress"].ToString());
                    CrpReport.SetParameterValue("FromEmail", dtPeople.Rows[0]["EMAIL"].ToString());

                }
                else
                {
                    CrpReport.SetParameterValue("From", "Administrator");
                    CrpReport.SetParameterValue("FromDesignation", "CEO");
                    CrpReport.SetParameterValue("FromPhone", "+923214402611");
                    CrpReport.SetParameterValue("FromAddress", "10-10 Arfa Technology Park, Lahore");
                    CrpReport.SetParameterValue("FromEmail", "rana@fastservices.pk");
                }
                CrpReport.SetDatabaseLogon(FSCommonLayer.Classes.Configuration.ServerUserName, FSCommonLayer.Classes.Configuration.ServerUserPwd, FSCommonLayer.Classes.Configuration.DatabseServer, FSCommonLayer.Classes.Configuration.DatabaseName);
                CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath);

                flag = true;
            }

        }
        catch (Exception ex)
        {
            flag = false;
            ltrlQuotation.Text = Utility.ShowAlert(false, ex.Message);
        }

        return flag;
    }

    private void CreateQuotationTable()
    {
        dtQuotationDetail = new DataTable();
        dtQuotationDetail.Columns.Add("SKU_ID", typeof(int));
        dtQuotationDetail.Columns.Add("SKU_Name", typeof(string));
        dtQuotationDetail.Columns.Add("CURRENCY_ID", typeof(int));
        dtQuotationDetail.Columns.Add("QUANTITY", typeof(int));
        dtQuotationDetail.Columns.Add("RATE", typeof(decimal));
        dtQuotationDetail.Columns.Add("Amount", typeof(decimal));
        dtQuotationDetail.Columns.Add("DURATION", typeof(string));
        dtQuotationDetail.Columns.Add("CURRENCY", typeof(string));
        dtQuotationDetail.Columns.Add("DOCDATE", typeof(DateTime));
        dtQuotationDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        this.Session.Add("dtQuotationDetail", dtQuotationDetail);
    }

    private void GetQuotationDetail()
    {
        DataTable dt = (DataTable)Session["dtTemplate"];

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EMAIL_TEMPLATE_ID"].ToString() == ddlTemplate.SelectedValue.ToString())
                {
                    ltrlBody.Text = dr["Body"].ToString();
                    if (dr["SIGNATURE"] != null)
                    {
                        ltrlSignature.Text = dr["SIGNATURE"].ToString();
                    }
                    ltrlTerms.Text = dr["TERMS"].ToString();
                    break;
                }
            }
        }
    }

    private void GetEmailFrom()
    {
        DataTable dtPeople = PplCtl.GetPeople(Convert.ToInt64(ddlEmailFrom.SelectedValue), Constants.LongNullValue, null, null);
        this.Session.Add("dtPeople", dtPeople);
        if (dtPeople.Rows.Count > 0)
        {
            lblFrom.Text = dtPeople.Rows[0]["FullName"].ToString();
            lblFromDesignation.Text = dtPeople.Rows[0]["SLASH_DESC"].ToString();
            lblFromPhone.Text = dtPeople.Rows[0]["PHONE"].ToString();
            lblFromAddress.Text = dtPeople.Rows[0]["PeopleAddress"].ToString();
            lblFromEmail.Text = dtPeople.Rows[0]["EMAIL"].ToString();

        }
        else
        {
            lblFrom.Text = "Administrator";
            lblFromDesignation.Text = "CEO";
            lblFromPhone.Text = "+923214402611";
            lblFromAddress.Text = "10-10 Arfa Technology Park, Lahore";
            lblFromEmail.Text = "rana@fastservices.pk";
        }
    }

    protected void gvQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtCurrency = (DataTable)Session["dtCurrency"];

            CheckBox cbQuotation = e.Row.FindControl("cbQuotation") as CheckBox;
            DropDownList ddlCurrency = e.Row.FindControl("ddlCurrency") as DropDownList;
            TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;
            TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            TextBox txtAmount = e.Row.FindControl("txtAmount") as TextBox;
            TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
            LinkButton btnAddDetail = e.Row.FindControl("btnAddDetail") as LinkButton;
            

            txtRate.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtAmount.ClientID + "');");
            txtQuantity.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtAmount.ClientID + "');");


            clsWebFormUtil.FillDropDownList(ddlCurrency, dtCurrency, 0, 2);

            if (cbQuotation.Checked)
            {
                strSubject += e.Row.Cells[3].Text + ":";
                ddlCurrency.SelectedValue = e.Row.Cells[1].Text;
            }

            if (e.Row.Cells[10].Text == "True")
            {
                cbQuotation.Enabled = false;
                ddlCurrency.Enabled = false;
                txtQuantity.Enabled = false;
                txtRate.Enabled = false;
                txtRemarks.Enabled = false;
                btnAddDetail.Enabled = false;
            }
        }
    }

    protected void gvQuotation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;

        if (e.CommandName == "detail")
        {
            dtQuotationDetail = Session["dtQuotationDetail"] as DataTable;
            foreach (DataRow dr in dtQuotationDetail.Rows)
            {
                if (dr["ENQUIREY_DETAIL_ID"].ToString() == e.CommandArgument.ToString())
                {
                    lblProduct.Text = dr["SKU_Name"].ToString();

                    break;
                }
            }
            this.Session.Add("ENQUIREY_DETAIL_ID", e.CommandArgument.ToString());
            BindDetailGrid();
            ModalPopupExtender.Show();
        }
    }

    protected void gvQuotation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            //Approve All
            CheckBox cbQuotation = (CheckBox)e.Row.Cells[2].FindControl("cbQuotation");
            CheckBox cbQuotationAll = (CheckBox)this.gvQuotation.HeaderRow.FindControl("cbQuotationAll");
            cbQuotation.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", cbQuotationAll.ClientID);

            LinkButton btnAddDetail = e.Row.FindControl("btnAddDetail") as LinkButton;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAddDetail);
        }
    }

    private void BindDetailGrid()
    {
        txtDetail.Value = string.Empty;
        dtProductDetail = (DataTable)Session["dtProductDetail"];

        if (dtProductDetail.Rows.Count > 0)
        {
            foreach (DataRow dr in dtProductDetail.Rows)
            {
                if (dr["ENQUIREY_DETAIL_ID"].ToString() == Session["ENQUIREY_DETAIL_ID"].ToString())
                {
                    txtDetail.Value = dr["PRODUCT_DETAIL"].ToString();
                }
            }
        }
    }

    private void CreateProductDetailTable()
    {
        dtProductDetail = new DataTable();
        dtProductDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(int));
        dtProductDetail.Columns.Add("PRODUCT_DETAIL", typeof(string));
        this.Session.Add("dtProductDetail", dtProductDetail);
    }

    protected void btnSaveProductDetail_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfQuotationIndex.Value;

        dtProductDetail = Session["dtProductDetail"] as DataTable;

        for (int i = 0; i < dtProductDetail.Rows.Count; i++)
        {
            if (Session["ENQUIREY_DETAIL_ID"].ToString() == dtProductDetail.Rows[i]["ENQUIREY_DETAIL_ID"].ToString())
            {
                dtProductDetail.Rows.RemoveAt(i);
                this.Session.Add("dtProductDetail", dtProductDetail);
                break;
            }
        }

        dtProductDetail = Session["dtProductDetail"] as DataTable;
        DataRow dr2 = dtProductDetail.NewRow();
        dr2["ENQUIREY_DETAIL_ID"] = Convert.ToInt32(Session["ENQUIREY_DETAIL_ID"]);
        dr2["PRODUCT_DETAIL"] = txtDetail.Value;
        dtProductDetail.Rows.Add(dr2);
        this.Session.Add("dtProductDetail", dtProductDetail);
        ModalPopupExtender.Show();
    }

    /// <summary>
    /// Loads Currency To Currency Comb
    /// </summary>
    private void LoadCurrency()
    {
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.Currency, null, 1, true);
        clsWebFormUtil.FillDropDownList(ddlCurrency, m_dt, "REF_ID", "SLASH_DESC");
        this.Session.Add("dtCurrency", m_dt);
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvProductDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            dtProductDetail = (DataTable)this.Session["dtProductDetail"];
            if (dtProductDetail.Rows.Count > 0)
            {
                for (int i = 0; i < dtProductDetail.Rows.Count; i++)
                {
                    if (dtProductDetail.Rows[i]["ENQUIREY_DETAIL_ID"].ToString() == Session["ENQUIREY_DETAIL_ID"].ToString() && dtProductDetail.Rows[i]["PRODUCT_DETAIL"].ToString() == e.CommandArgument.ToString())
                    {
                        dtProductDetail.Rows.RemoveAt(i);
                        this.Session.Add("dtProductDetail", dtProductDetail);
                        break;
                    }
                }
            }
        }
        BindDetailGrid();
        ModalPopupExtender.Show();
    }

    #endregion

    #region Invoice

    protected void btnSaveInvoice_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
        ltrlInvoice.Text = "";
        dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
        int InvoiceCount = 0;
        if (gvInvoice.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in gvInvoice.Rows)
            {
                TextBox txtRate = gvr.FindControl("txtRate") as TextBox;
                TextBox txtRemarks = gvr.FindControl("txtRemarks") as TextBox;
                TextBox txtQuantity = gvr.FindControl("txtQuantity") as TextBox;
                TextBox txtSubDate = gvr.FindControl("txtSubDate") as TextBox;
                TextBox txtExpDate = gvr.FindControl("txtExpDate") as TextBox;
                CheckBox cbInvoice = gvr.FindControl("cbInvoice") as CheckBox;
                DropDownList ddlPaymentType = gvr.FindControl("ddlPaymentType") as DropDownList;

                decimal Rate = 0;
                int Qty = 0;
                if (cbInvoice.Enabled)
                {
                    foreach (DataRow dr in dtInvoiceDetail.Rows)
                    {
                        if (dr["SKU_ID"].ToString() == gvr.Cells[0].Text && dr["ENQUIREY_DETAIL_ID"].ToString() == gvr.Cells[12].Text)
                        {
                            dr["CURRENCY_ID"] = Convert.ToInt32(ddlCurrency.SelectedValue);

                            try
                            {
                                if (ddlCurrency.SelectedValue == "705")
                                {
                                    Rate = Convert.ToDecimal(txtRate.Text) * Convert.ToDecimal(txtConversion.Text);
                                }
                                else
                                {
                                    Rate = Convert.ToDecimal(txtRate.Text);
                                }
                            }
                            catch (Exception ex)
                            {
                                Rate = 0;
                            }
                            dr["RATE"] = Rate;


                            try
                            {
                                Qty = Convert.ToInt32(txtQuantity.Text);
                            }
                            catch (Exception ex)
                            {
                                Qty = 0;
                            }

                            if (txtSubDate.Text.Length > 0)
                            {
                                dr["SUBSCRIPTION_DATE"] = txtSubDate.Text;
                            }
                            else
                            {
                                dr["SUBSCRIPTION_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                            }

                            if (txtExpDate.Text.Length > 0)
                            {
                                dr["EXPIRY_DATE"] = txtExpDate.Text;
                            }
                            else
                            {
                                dr["EXPIRY_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                            }

                            dr["PAYMENT_TYPE"] = ddlPaymentType.SelectedValue;
                            dr["REMARKS"] = txtRemarks.Text;

                            dr["QUANTITY"] = Qty;
                            dr["IS_INVOICE"] = cbInvoice.Checked;
                            if (cbInvoice.Checked)
                            {
                                InvoiceCount += 1;
                            }
                            dr["ENQUIREY_DETAIL_ID"] = gvr.Cells[12].Text;

                            break;
                        }
                    }
                }
            }

            dtInvoiceDetail.AcceptChanges();

            long InvoiceID = Constants.LongNullValue;

            if (dtInvoiceDetail.Rows.Count > 0 && InvoiceCount > 0)
            {
                DateTime PODate = Constants.DateNullValue;
                if (txtPODate.Text.Length > 0)
                {
                    PODate = Convert.ToDateTime(ConvertDate.British_To_American(txtPODate.Text));
                }
                if (btnSaveInvoice.Text == "Save Invoice")
                {
                    DataTable dtInvoiceNo = QoutCtl.GetMaxInvoiceNo(Convert.ToInt64(hfEnquiryID.Value));
                    string InvoiceFileName = lblCustomerView.Text + "_" + hfEnquiryID.Value + "_" + dtInvoiceNo.Rows[0][0].ToString() + "_Invoice.pdf";

                    InvoiceID = QoutCtl.InsertInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), lblInvoiceReference.Text, Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(ConvertDate.British_To_American(txtInvoiceDate.Text)), Convert.ToDecimal(dc.chkNull_0(txtConversion.Text)), Convert.ToDecimal(dc.chkNull_0(txtIncomeTax.Text)), Convert.ToDecimal(dc.chkNull_0(txtServiceTax.Text)), Convert.ToInt32(ddlUOM.SelectedValue), Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(Session["UserID"]), InvoiceFileName
                        ,txtPONo.Text,PODate, dtInvoiceDetail);

                    hfInvoiceID.Value = InvoiceID.ToString();
                    if (InvoiceID != Constants.LongNullValue)
                    {
                        if (CreateInvoiceFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName, InvoiceID))
                        {
                            int UserID = Convert.ToInt32(Session["UserID"]);
                            if (UserID > 0)
                            {
                                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSaved, true);

                                //Save Transaction in log
                                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " added");

                                DocCtl.InsertUploadDocument(Convert.ToInt64(hfEnquiryID.Value), 730, Convert.ToInt32(Session["UserID"]), InvoiceFileName, "Invoice");

                                ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice Saved Successfully.");
                                LoadEditGird();
                                LoadDocuments();
                            }
                            else
                            {
                                Session.Clear();
                                System.Web.Security.FormsAuthentication.SignOut();
                                Response.Redirect("../Login.aspx");
                            }
                        }
                        else
                        {
                            ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
                        }
                    }
                    else
                    {
                        ltrlInvoice.Text = Utility.ShowAlert(false, "Some error Occured.");
                    }
                }
                else
                {
                    if (QoutCtl.UpdateInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(hfMaxInvoiceID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(ConvertDate.British_To_American(txtInvoiceDate.Text)), Convert.ToDecimal(dc.chkNull_0(txtConversion.Text)), Convert.ToDecimal(dc.chkNull_0(txtIncomeTax.Text)), Convert.ToDecimal(dc.chkNull_0(txtServiceTax.Text)), Convert.ToInt32(ddlUOM.SelectedValue), Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(Session["UserID"]), true
                        ,txtPONo.Text,PODate, dtInvoiceDetail))
                    {
                        if (CreateInvoiceFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + hfPDFFileName.Value, Convert.ToInt64(hfMaxInvoiceID.Value)))
                        {
                            int UserID = Convert.ToInt32(Session["UserID"]);
                            if (UserID > 0)
                            {
                                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSaved, true);
                                //Save Transaction in log
                                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " updated");
                                ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice updated successfully.");
                                LoadEditGird();
                            }
                            else
                            {
                                Session.Clear();
                                System.Web.Security.FormsAuthentication.SignOut();
                                Response.Redirect("../Login.aspx");
                            }
                        }
                        else
                        {
                            ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
                        }
                    }
                }
            }
            else
            {
                ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
            }
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
        }

        ltrlEdit.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnEmailInvoice_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
        ltrlInvoice.Text = "";
        btnSaveInvoice_Click(null, null);

        string InvoiceFileName = hfEnquiryID.Value + "_" + DateTime.Now.ToString("yyyyMdHHmmss") + "_" + Session["UserName2"].ToString() + "_Invoice.pdf";
        if (CreateInvoiceFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName, 0))
        {
            if (SetEmailIDs(ltrlInvoice, ddlEmailFromInvoice))
            {
                EmailCc = txtCc3.Text;
                CheckBoxList cblCc = cblInvoiceCc.GetAllItems();

                foreach (ListItem li in cblCc.Items)
                {
                    if (li.Selected)
                    {
                        EmailCc += "," + li.Value.Split(',')[1].ToString();
                    }
                }

                string[] strEmailCc = EmailCc.Split(',');


                string strEmailTo = string.Empty;
                DataTable dtEmailTo = (DataTable)Session["dtEmailTo"];

                foreach (DataRow dr in dtEmailTo.Rows)
                {
                    if (dr["PEOPLE_ID"].ToString() == ddlEmailToQuotation.SelectedValue)
                    {
                        strEmailTo = dr["EMAIL"].ToString();
                        break;
                    }
                }


                if (strEmailCc[0].ToString() != "" || strEmailTo != string.Empty || strEmailCc.Length > 1)
                {
                    if (SendEmailInvoice(strEmailTo, strEmailCc, AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName))
                    {
                        QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfInvoiceID.Value), 1, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), strEmailTo, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));

                        if (strEmailCc[0].ToString() != "" || strEmailCc.Length > 1)
                        {
                            foreach (string strCc in strEmailCc)
                            {
                                QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfInvoiceID.Value), 1, Constants.ReceipientCustomer, Constants.LongNullValue, strCc, Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                            }
                        }

                        EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSent, true);

                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " sent");

                        ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice Sent Successfully.");
                    }
                    else
                    {
                        ltrlInvoice.Text = Utility.ShowAlert(false, "While sending email some error occured.");
                    }
                }
                else
                {
                    ltrlInvoice.Text = Utility.ShowAlert(false, "No email selected.");
                }
            }
            DocCtl.InsertUploadDocument(Convert.ToInt64(Session["EnquiryMasterID"]), 686, Convert.ToInt32(Session["UserID"]), InvoiceFileName, "Invoice");
            LoadDocuments();

        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
        }

        ltrlEdit.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    private void InvoiceToWord(long InvoiceID)
    {
        DataTable dtReports = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), InvoiceID, true);
        if (dtReports.Rows.Count > 0)
        {
            StringBuilder str = new StringBuilder();
            str.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:word' xmlns='http://www.w3.org/TR/REC-html40'>");
            str.Append("<head>");
            str.Append("<!--[if gte mso 9]> <xml> <w:WordDocument> <w:View>Print</w:View> <w:Zoom>100</w:Zoom> <w:DoNotOptimizeForBrowser/> </w:WordDocument> </xml> <![endif]-->");
            str.Append("<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"\"text/html; charset=UTF-8\"\">");
            str.Append("<meta name=ProgId content=Word.Document>");
            str.Append("<meta name=Generator content=\"Microsoft Word 9\">");
            str.Append("<meta name=Originator content=\"Microsoft Word 9\">");

            str.Append("<style>");
            str.Append("@page Section2 {margin:0.1in 0.1in 0in 0.1in;mso-header-margin:0.1in;mso-footer-margin:0.1in;mso-paper-source:0;}");
            str.Append("div.Section2 {page:Section2;}");
            str.Append(@" p.MsoFooter, li.MsoFooter, div.MsoFooter");
            str.Append("{");
            str.Append("margin:0in 0in 0in 0in");
            str.Append("margin-bottom:.0001pt");
            str.Append("mso-pagination:widow-orphan");
            str.Append("tab-stops:center 3.0in right 6.0in");
            str.Append("font-size:12.0pt");
            str.Append("}");
            str.Append("</style>");

            str.Append("</head>");

            str.Append("<body>");
            str.Append("<div class=Section2>");

            str.Append("<table width='90%'>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%' align='right'>");
            str.Append("&nbsp;<br><br><br>");
            str.Append("</td>");
            str.Append("</tr>");


            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%' align='right'>");
            str.Append("Date: " + Convert.ToDateTime(dtReports.Rows[0]["DOCUMENT_DATE"]).ToString("dd/MM/yyyy"));
            str.Append("<br />" + dtReports.Rows[0]["NTN"].ToString());
            str.Append("<br />" + dtReports.Rows[0]["REFERENCE_NO"].ToString());
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%' align='center'>");
            str.Append("<b><u>INVOICE</u></b>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%'>");
            str.Append("<b>Bill to:</b>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%'>");
            str.Append(lblBillTo.Text);
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%'>");
            str.Append("<b>" + lblBillCompany.Text + "</b>");
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%' align='right'>");
            str.Append("&nbsp;<br>");
            str.Append("</td>");
            str.Append("</tr>");


            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%'>");

            str.Append("<table width='100%' cellspacing='0' cellpadding='0'>");


            str.Append("<tr>");
            str.Append("<td align='center' style='width:5%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
            str.Append("#");
            str.Append("</td>");
            str.Append("<td style='width:50%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
            str.Append("&nbsp;&nbsp;<b>Items Description</b>");
            str.Append("</td>");
            str.Append("<td align='center'  style='width:15%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
            str.Append("<b>Quantity</b>");
            str.Append("</td>");
            str.Append("<td align='center'  style='width:15%;border-style: solid none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
            str.Append("<b>Unit Price</b>");
            str.Append("</td>");
            str.Append("<td align='center'  style='width:15%;border-style: solid solid solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
            str.Append("<b>Line Total</b>");
            str.Append("</td>");
            str.Append("</tr>");

            int rowNow = 0;

            foreach (DataRow dr in dtReports.Rows)
            {
                rowNow++;
                str.Append("<tr>");
                str.Append("<td align='center' style='width:5%;border-style: none none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
                str.Append(rowNow);
                str.Append("</td>");
                str.Append("<td style='width:55%;border-style: none none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
                str.Append("&nbsp;&nbsp;<b>" + dr["SKU_NAME"].ToString() + "</b>");
                str.Append("</td>");
                str.Append("<td align='center'  style='width:15%;border-style: none none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
                str.Append("<b>" + dr["QUANTITY"].ToString() + "</b>");
                str.Append("</td>");
                str.Append("<td align='center'  style='width:15%;border-style: none none solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");
                str.Append("<b>" + dr["RATE"].ToString() + "</b>");
                str.Append("</td>");
                str.Append("<td align='center'  style='width:15%;border-style: none solid solid solid; border-width: thin; border-color: Black;font-family:Calibri;font-size:10pt;'>");

                decimal Total = 0;
                try
                {
                    Total = Convert.ToDecimal(dr["RATE"]) * Convert.ToDecimal(dr["QUANTITY"]);
                }
                catch (Exception)
                {

                }

                str.Append("<b>" + Total + "</b>");
                str.Append("</td>");
                str.Append("</tr>");
            }

            str.Append("</table>");

            str.Append("</td>");
            str.Append("</tr>");

            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%' align='right'>");
            str.Append("&nbsp;<br>");
            str.Append("</td>");
            str.Append("</tr>");


            str.Append("<tr>");
            str.Append("<td style='width:5%'></td>");
            str.Append("<td style='width:95%'>");
            str.Append(ltrlInvoiceTerms.Text);
            str.Append("</td>");
            str.Append("</tr>");

            str.Append("</table>");



            str.Append("</div>");
            str.Append(@"<div style='mso-element:footer' id=f1>");
            str.Append("<p class=MsoFooter>");
            str.Append("</p></div>");
            str.Append("</body>");
            str.Append("</html >");

            Response.AppendHeader("Content-Type", "application/msword");
            Response.AppendHeader("Content-disposition", "attachment; filename=" + "Invoice.doc");
            Response.Write(str);
        }
    }

    protected void btnInvoicePDF_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;

        btnSaveInvoice_Click(null, null);

        string InvoiceFileName = hfEnquiryID.Value + "_" + DateTime.Now.ToString("yyyyMdHHmmss") + "_" + Session["UserName2"].ToString() + "_Invoice.pdf";

        if (CreateInvoiceFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName, 0))
        {
            DownLoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName);
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "No product selected.");
        }

        ltrlEdit.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnNewInvoiceFromQuotion_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
        ltrlInvoice.Text = "";
        gvInvoice.Visible = true;
        btnSaveInvoice.Visible = true;
        lblInvoiceDate.Visible = true;
        txtInvoiceDate.Visible = true;
        lblUOM.Visible = true;
        ddlUOM.Visible = true;
        lblCurrency.Visible = true;
        ddlCurrency.Visible = true;
        lblConversion.Visible = true;
        txtConversion.Visible = true;
        lblIncomeTax.Visible = true;
        txtIncomeTax.Visible = true;
        lblServiceTax.Visible = true;
        txtServiceTax.Visible = true;
        lblPONo.Visible = true;
        lblPODate.Visible = true;
        txtPONo.Visible = true;
        txtPODate.Visible = true;

        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
        dtQuotationDetail = QoutCtl.GetCustomerQuotation(Convert.ToInt64(hfEnquiryID.Value), false);

        this.CreateInvoiceTable();
        dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
        dtInvoiceDetail = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), 0, false);

        foreach (DataRow dr in dtQuotationDetail.Rows)
        {
            if (dr["IS_QUOTATION"].ToString() == "True")
            {
                DataRow drI = dtInvoiceDetail.NewRow();
                drI["SKU_ID"] = Convert.ToInt32(dr["SKU_ID"]);
                drI["SKU_Name"] = dr["SKU_NAME"].ToString();
                drI["QUANTITY"] = dr["Quantity"].ToString();
                drI["RATE"] = dr["RATE"].ToString();
                drI["CURRENCY_ID"] = dr["CURRENCY_ID"].ToString();
                drI["IS_INVOICE"] = false;
                drI["DOCDATE"] = dr["DOCDATE"];
                drI["SUBSCRIPTION_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                drI["EXPIRY_DATE"] = System.DateTime.Now.ToString("dd/MM/yyyy");
                drI["REMARKS"] = "";
                drI["ENQUIREY_DETAIL_ID"] = Convert.ToInt64(dr["ENQUIREY_DETAIL_ID"]);
                drI["fnIsEnquiryPaymentReceived"] = false;
                dtInvoiceDetail.Rows.Add(drI);
            }
        }

        btnSaveInvoice.Text = "Save Invoice";

        this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
        gvInvoice.DataSource = dtInvoiceDetail;
        gvInvoice.DataBind();

        if (gvInvoice.Rows.Count > 0 && gvInvoice.Visible)
        {
            lblInvoiceDate.Visible = true;
            txtInvoiceDate.Visible = true;
            lblUOM.Visible = true;
            ddlUOM.Visible = true;
            lblCurrency.Visible = true;
            ddlCurrency.Visible = true;
            lblConversion.Visible = true;
            txtConversion.Visible = true;
            lblIncomeTax.Visible = true;
            txtIncomeTax.Visible = true;
            lblServiceTax.Visible = true;
            txtServiceTax.Visible = true;
            lblPONo.Visible = true;
            lblPODate.Visible = true;
            txtPONo.Visible = true;
            txtPODate.Visible = true;
        }

        ltrlEdit.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void btnNewInvoice_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
        ltrlInvoice.Text = "";
        DataTable dtInvoiceNo = QoutCtl.GetMaxInvoiceNo(Convert.ToInt64(hfEnquiryID.Value));

        this.CreateInvoiceTable();
        dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
        dtInvoiceDetail = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(dtInvoiceNo.Rows[0][0]), false);

        hfMaxInvoiceID.Value = dtInvoiceNo.Rows[0][0].ToString();
        if (dtInvoiceDetail.Rows.Count > 0)
        {
            gvInvoice.Visible = true;
            btnSaveInvoice.Visible = true;
            lblInvoiceDate.Visible = true;
            txtInvoiceDate.Visible = true;
            lblUOM.Visible = true;
            ddlUOM.Visible = true;
            lblCurrency.Visible = true;
            ddlCurrency.Visible = true;
            lblConversion.Visible = true;
            txtConversion.Visible = true;
            lblIncomeTax.Visible = true;
            txtIncomeTax.Visible = true;
            lblServiceTax.Visible = true;
            txtServiceTax.Visible = true;
            lblPONo.Visible = true;
            lblPODate.Visible = true;
            txtPONo.Visible = true;
            txtPODate.Visible = true;

            hfPDFFileName.Value = dtInvoiceDetail.Rows[0]["PDFFileName"].ToString();

            txtInvoiceDate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["DOCUMENT_DATE"]).ToString("dd/MM/yyyy");
            txtConversion.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["CONVERSION_RATE"]);
            txtIncomeTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["INCOME_TAX"]);
            txtServiceTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["SERVICE_TAX"]);
            ddlUOM.SelectedValue = dtInvoiceDetail.Rows[0]["UOM"].ToString();
            ddlCurrency.SelectedValue = dtInvoiceDetail.Rows[0]["CURRENCY_ID"].ToString();
            txtPONo.Text = dtInvoiceDetail.Rows[0]["PONo"].ToString();
            if (dtInvoiceDetail.Rows[0]["PODate"].ToString() != "")
            {
                txtPODate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["PODate"]).ToString("dd/MM/yyyy");
            }

            btnSaveInvoice.Text = "Save Invoice";

            this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
            gvInvoice.DataSource = dtInvoiceDetail;
            gvInvoice.DataBind();

            if (gvInvoice.Rows.Count > 0 && gvInvoice.Visible)
            {
                lblInvoiceDate.Visible = true;
                txtInvoiceDate.Visible = true;
                lblUOM.Visible = true;
                ddlUOM.Visible = true;
                lblCurrency.Visible = true;
                ddlCurrency.Visible = true;
                lblConversion.Visible = true;
                txtConversion.Visible = true;
                lblIncomeTax.Visible = true;
                txtIncomeTax.Visible = true;
                lblServiceTax.Visible = true;
                txtServiceTax.Visible = true;
                lblPONo.Visible = true;
                lblPODate.Visible = true;
                txtPONo.Visible = true;
                txtPODate.Visible = true;
            }

            DataTable dtEnquiryInvoice = QoutCtl.GetEnquiryInvoice(Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue);
            gvAllInvoice.DataSource = dtEnquiryInvoice;
            gvAllInvoice.DataBind();

            ltrlEdit.Text = "";
            ltrlPayment.Text = "";
            ltrlQuotation.Text = "";
            ltrlSendEmail.Text = "";
            ltrlDoc.Text = "";
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "No Data found.");
        }
    }

    private void CreateInvoiceTable()
    {
        dtInvoiceDetail = new DataTable();
        dtInvoiceDetail.Columns.Add("SKU_ID", typeof(int));
        dtInvoiceDetail.Columns.Add("SKU_Name", typeof(string));
        dtInvoiceDetail.Columns.Add("CURRENCY_ID", typeof(int));
        dtInvoiceDetail.Columns.Add("QUANTITY", typeof(int));
        dtInvoiceDetail.Columns.Add("RATE", typeof(decimal));
        dtInvoiceDetail.Columns.Add("CURRENCY", typeof(string));
        dtInvoiceDetail.Columns.Add("DOCDATE", typeof(DateTime));
        dtInvoiceDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtInvoiceDetail.Columns.Add("PAYMENT_TYPE", typeof(int));
        dtInvoiceDetail.Columns.Add("SUBSCRIPTION_DATE", typeof(DateTime));
        this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
    }

    protected void gvInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtRate = e.Row.FindControl("txtRate") as TextBox;
            TextBox txtQuantity = e.Row.FindControl("txtQuantity") as TextBox;
            TextBox txtTotal = e.Row.FindControl("txtTotal") as TextBox;
            CheckBox cbInvoice = e.Row.FindControl("cbInvoice") as CheckBox;
            DropDownList ddlPaymentType = (DropDownList)e.Row.FindControl("ddlPaymentType");


            txtRate.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtTotal.ClientID + "');");
            txtQuantity.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtTotal.ClientID + "');");
            txtIncomeTax.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtTotal.ClientID + "');");
            txtServiceTax.Attributes.Add("onchange", "javascript:return GetTotal('" + txtRate.ClientID + "', '" + txtQuantity.ClientID + "', '" + txtTotal.ClientID + "');");

            ddlPaymentType.SelectedValue = e.Row.Cells[13].Text;
            if (e.Row.Cells[8].Text == "True")
            {
                //cbInvoice.Enabled = false;
                txtRate.Enabled = false;
                txtQuantity.Enabled = false;
                txtTotal.Enabled = false;
            }

            txtTotal.Text = String.Format("{0:0,0.00}", (Convert.ToDecimal(dc.chkNull_0(txtQuantity.Text)) * Convert.ToDecimal(dc.chkNull_0(txtRate.Text))));

        }
    }

    protected void gvInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
    }

    protected void gvInvoice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow
            && (e.Row.RowState == DataControlRowState.Normal
            || e.Row.RowState == DataControlRowState.Alternate))
        {
            //Select All
            CheckBox cbInvoice = (CheckBox)e.Row.Cells[2].FindControl("cbInvoice");
            CheckBox cbInvoiceAll = (CheckBox)this.gvInvoice.HeaderRow.FindControl("cbInvoiceAll");
            cbInvoice.Attributes["onclick"] = string.Format("javascript:ChildClickInv(this,'{0}');", cbInvoiceAll.ClientID);
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
                if (dr["EMAIL_TEMPLATE_ID"].ToString() == ddlInvoiceEmailFrom.SelectedValue.ToString())
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

    private bool CreateInvoiceFile(string filePath, long InvoiceID)
    {
        bool flag = false;

        try
        {
            DataTable dtReports = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), InvoiceID, true);
            if (dtReports.Rows.Count > 0)
            {
                DataTable dtOriginator = PplCtl.GetPeople(Convert.ToInt64(ddlEmailToInvoice.SelectedValue), Constants.LongNullValue, null, null);
                
                CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (Convert.ToDecimal(dtReports.Rows[0]["SERVICE_TAX"]) > 0)
                {
                    CrpReport = new FSBusinessLayer.Reports.CrpInvoice2();
                }
                else
                {
                    CrpReport = new FSBusinessLayer.Reports.CrpInvoice3();
                }
                CrpReport.SetDataSource(dtReports);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("Customer", dtOriginator.Rows[0]["FullName"].ToString());
                CrpReport.SetParameterValue("Email", dtOriginator.Rows[0]["EMAIL"].ToString());
                CrpReport.SetParameterValue("CustomerCompany", dtOriginator.Rows[0]["NAME"].ToString());
                if (Convert.ToDecimal(dtReports.Rows[0]["SERVICE_TAX"]) > 0)
                {
                    if (dtReports.Rows[0]["TYPE_ID"].ToString() == "710" || dtReports.Rows[0]["TYPE_ID"].ToString() == "763")
                    {
                        CrpReport.SetParameterValue("Title", "Sales Tax Invoice");
                    }
                    else
                    {
                        if (dtReports.Rows[0]["City"].ToString() == "2472")
                        {
                            CrpReport.SetParameterValue("Title", "Sales Tax Invoice (Services - Sindh Revenue Board)");
                        }
                        else
                        {
                            CrpReport.SetParameterValue("Title", "Sales Tax Invoice (Services - Punjab Revenue Authority)");
                        }
                    }
                }
                else
                {
                    CrpReport.SetParameterValue("Terms", ltrlTerms.Text);
                }

                CrpReport.SetDatabaseLogon(FSCommonLayer.Classes.Configuration.ServerUserName, FSCommonLayer.Classes.Configuration.ServerUserPwd, FSCommonLayer.Classes.Configuration.DatabseServer, FSCommonLayer.Classes.Configuration.DatabaseName);
                CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filePath);
                flag = true;
            }

        }
        catch (Exception ex)
        {
            flag = false;
            ltrlInvoice.Text = Utility.ShowAlert(false, ex.Message);
        }

        return flag;
    }

    private void GetInvoiceTemplate()
    {
        DataTable dtTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0, 727);

        DataTable dtEmailTemplateInvoice = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 1, 727);

        clsWebFormUtil.FillDropDownList(ddlInvoiceEmailFrom, dtEmailTemplateInvoice, "EMAIL_TEMPLATE_ID", "SUBJECT");

        this.Session.Add("dtEmailTemplateInvoice", dtEmailTemplateInvoice);


        if (dtTemplate.Rows.Count > 0)
        {
            ltrlInvoiceTerms.Text = dtTemplate.Rows[0]["TERMS"].ToString();
        }
        else
        {
            ltrlInvoiceTerms.Text = "Tersm and conditions";
        }


    }

    private string GetInvoiceAttachment(string FolderPath)
    {
        string filePath = "";
        string[] InvoiceFiles = System.IO.Directory.GetFiles(FolderPath);

        if (InvoiceFiles.Length > 0)
        {
            for (int i = InvoiceFiles.Length - 1; i >= 0; i--)
            {
                if (InvoiceFiles[i].ToString().Contains("_"))
                {
                    if (InvoiceFiles[i].ToString().Substring(InvoiceFiles[i].IndexOf("Docs") + 5, InvoiceFiles[i].IndexOf("_") - InvoiceFiles[i].IndexOf("Docs") - 5) == hfEnquiryID.Value)
                    {
                        if (InvoiceFiles[i].ToString().Contains("Invoice.pdf"))
                        {
                            filePath = InvoiceFiles[i].ToString();
                            break;
                        }
                    }
                }
            }
        }

        return filePath;
    }

    protected void gvAllInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfInvoiceIndex.Value;
        ltrlInvoice.Text = "";

        if (e.CommandName == "email")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

            EmailCc = txtCc3.Text;
            CheckBoxList cblCc = cblInvoiceCc.GetAllItems();

            foreach (ListItem li in cblCc.Items)
            {
                if (li.Selected)
                {
                    EmailCc += "," + li.Value.Split(',')[1].ToString();
                }
            }

            string[] strEmailCc = EmailCc.Split(',');

            string strEmailTo = string.Empty;
            DataTable dtEmailTo = (DataTable)Session["dtEmailTo"];

            foreach (DataRow dr in dtEmailTo.Rows)
            {
                if (dr["PEOPLE_ID"].ToString() == ddlEmailToInvoice.SelectedValue)
                {
                    strEmailTo = dr["EMAIL"].ToString();
                    break;
                }
            }


            if (strEmailCc[0].ToString() != "" || strEmailTo != string.Empty || strEmailCc.Length > 1)
            {
                if (SetEmailIDs(ltrlInvoice, ddlEmailFromInvoice))
                {
                    if (SendEmailInvoice(strEmailTo, strEmailCc, AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + e.CommandArgument.ToString()))
                    {
                        QoutCtl.InsertQuotationReceipient(Convert.ToInt64(row.Cells[0].Text), 1, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), strEmailTo, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));

                        if (strEmailCc[0].ToString() != "" || strEmailCc.Length > 1)
                        {
                            foreach (string strCc in strEmailCc)
                            {
                                QoutCtl.InsertQuotationReceipient(Convert.ToInt64(row.Cells[0].Text), 1, Constants.ReceipientCustomer, Constants.LongNullValue, strCc, Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                            }
                        }

                        EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSent, true);

                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Email", "Invoice for enquiry no " + hfEnquiryID.Value + " sent");

                        ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice Sent Successfully.");
                    }
                    else
                    {
                        ltrlInvoice.Text = Utility.ShowAlert(false, "While sending email some error occured.");
                    }
                }
            }
            else
            {
                ltrlInvoice.Text = Utility.ShowAlert(false, "No email selected.");
            }

            LoadEditGird();
        }
        else if (e.CommandName == "pdf")
        {
            DownLoadFile(AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + e.CommandArgument.ToString());            
        }
        else if (e.CommandName == "pdf2")
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            DataTable dtReports = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(row.Cells[0].Text), true);
            if (dtReports.Rows.Count > 0)
            {
                string InvoiceFileName = lblCustomerView.Text + "_" + hfEnquiryID.Value + "_" + row.Cells[0].Text + "_Invoice.pdf";
                InvoiceFileName = AppDomain.CurrentDomain.BaseDirectory + "\\Docs\\" + InvoiceFileName;
                DataTable dtOriginator = PplCtl.GetPeople(Convert.ToInt64(ddlEmailToInvoice.SelectedValue), Constants.LongNullValue, null, null);

                CrystalDecisions.CrystalReports.Engine.ReportDocument CrpReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                if (Convert.ToDecimal(dtReports.Rows[0]["SERVICE_TAX"]) > 0)
                {
                    CrpReport = new FSBusinessLayer.Reports.CrpInvoice2Simple();
                }
                else
                {
                    CrpReport = new FSBusinessLayer.Reports.CrpInvoice3Simple();
                }
                CrpReport.SetDataSource(dtReports);
                CrpReport.Refresh();
                CrpReport.SetParameterValue("Customer", dtOriginator.Rows[0]["FullName"].ToString());
                CrpReport.SetParameterValue("Email", dtOriginator.Rows[0]["EMAIL"].ToString());
                CrpReport.SetParameterValue("CustomerCompany", dtOriginator.Rows[0]["NAME"].ToString());
                if (Convert.ToDecimal(dtReports.Rows[0]["SERVICE_TAX"]) > 0)
                {
                    if (dtReports.Rows[0]["TYPE_ID"].ToString() == "710" || dtReports.Rows[0]["TYPE_ID"].ToString() == "763")
                    {
                        CrpReport.SetParameterValue("Title", "Sales Tax Invoice");
                    }
                    else
                    {
                        if (dtReports.Rows[0]["City"].ToString() == "2472")
                        {
                            CrpReport.SetParameterValue("Title", "Sales Tax Invoice (Services - Sindh Revenue Board)");
                        }
                        else
                        {
                            CrpReport.SetParameterValue("Title", "Sales Tax Invoice (Services - Punjab Revenue Authority)");
                        }
                    }
                }
                else
                {
                    CrpReport.SetParameterValue("Terms", ltrlTerms.Text);
                }
                CrpReport.SetDatabaseLogon(FSCommonLayer.Classes.Configuration.ServerUserName, FSCommonLayer.Classes.Configuration.ServerUserPwd, FSCommonLayer.Classes.Configuration.DatabseServer, FSCommonLayer.Classes.Configuration.DatabaseName);
                CrpReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,InvoiceFileName);

                DownLoadFile(InvoiceFileName);
            }
        }
        else if (e.CommandName == "word")
        {
            InvoiceToWord(Convert.ToInt64(e.CommandArgument));
        }
        else if (e.CommandName == "change")
        {
            hfTabIndex.Value = hfInvoiceIndex.Value;
            ltrlInvoice.Text = "";
            gvInvoice.Visible = true;
            btnSaveInvoice.Visible = true;
            lblInvoiceDate.Visible = true;
            txtInvoiceDate.Visible = true;
            lblUOM.Visible = true;
            ddlUOM.Visible = true;
            lblCurrency.Visible = true;
            ddlCurrency.Visible = true;
            lblConversion.Visible = true;
            txtConversion.Visible = true;
            lblIncomeTax.Visible = true;
            txtIncomeTax.Visible = true;
            lblServiceTax.Visible = true;
            txtServiceTax.Visible = true;
            lblPONo.Visible = true;
            lblPODate.Visible = true;
            txtPONo.Visible = true;
            txtPODate.Visible = true;

            this.CreateInvoiceTable();
            dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
            dtInvoiceDetail = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), false);

            hfMaxInvoiceID.Value = e.CommandArgument.ToString();
            hfPDFFileName.Value = dtInvoiceDetail.Rows[0]["PDFFileName"].ToString();

            btnSaveInvoice.Text = "Update Invoice";

            this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
            gvInvoice.DataSource = dtInvoiceDetail;
            gvInvoice.DataBind();

            if (dtInvoiceDetail.Rows.Count > 0)
            {
                txtInvoiceDate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["DOCUMENT_DATE"]).ToString("dd/MM/yyyy");
                txtConversion.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["CONVERSION_RATE"]);
                txtIncomeTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["INCOME_TAX"]);
                txtServiceTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["SERVICE_TAX"]);
                ddlUOM.SelectedValue = dtInvoiceDetail.Rows[0]["UOM"].ToString();
                ddlCurrency.SelectedValue = dtInvoiceDetail.Rows[0]["CURRENCY_ID"].ToString();
                txtPONo.Text = dtInvoiceDetail.Rows[0]["PONo"].ToString();
                if (dtInvoiceDetail.Rows[0]["PODate"].ToString() != "")
                {
                    txtPODate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["PODate"]).ToString("dd/MM/yyyy");
                }
            }

            if (gvInvoice.Rows.Count > 0 && gvInvoice.Visible)
            {
                lblInvoiceDate.Visible = true;
                txtInvoiceDate.Visible = true;
                lblUOM.Visible = true;
                ddlUOM.Visible = true;
                lblCurrency.Visible = true;
                ddlCurrency.Visible = true;
                lblConversion.Visible = true;
                txtConversion.Visible = true;
                lblIncomeTax.Visible = true;
                txtIncomeTax.Visible = true;
                lblServiceTax.Visible = true;
                txtServiceTax.Visible = true;
                lblPONo.Visible = true;
                lblPODate.Visible = true;
                txtPONo.Visible = true;
                txtPODate.Visible = true;
            }
        }
        else if (e.CommandName == "Active")
        {
            if (QoutCtl.UpdateInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), false))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " updated");

                ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice updated successfully..");
            }
            else
            {
                ltrlInvoice.Text = Utility.ShowAlert(false, "Some error occured.");
            }

            DataTable dtEnquiryInvoice = QoutCtl.GetEnquiryInvoice(Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue);
            gvAllInvoice.DataSource = dtEnquiryInvoice;
            gvAllInvoice.DataBind();
        }

        else if (e.CommandName == "Inactive")
        {
            if (QoutCtl.UpdateInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), true))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " updated");

                ltrlInvoice.Text = Utility.ShowAlert(true, "Invoice updated successfully..");
            }
            else
            {
                ltrlInvoice.Text = Utility.ShowAlert(false, "Some error occured.");
            }

            DataTable dtEnquiryInvoice = QoutCtl.GetEnquiryInvoice(Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue);
            gvAllInvoice.DataSource = dtEnquiryInvoice;
            gvAllInvoice.DataBind();
        }
        else if (e.CommandName == "duplicate")
        {
            hfTabIndex.Value = hfInvoiceIndex.Value;
            ltrlInvoice.Text = "";
            DataTable dtInvoiceNo = QoutCtl.GetMaxInvoiceNo(Convert.ToInt64(hfEnquiryID.Value));

            this.CreateInvoiceTable();
            dtInvoiceDetail = (DataTable)Session["dtInvoiceDetail"];
            dtInvoiceDetail = QoutCtl.GetInvoiceData(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), false);
            hfMaxInvoiceID.Value = dtInvoiceNo.Rows[0][0].ToString();
            if (dtInvoiceDetail.Rows.Count > 0)
            {
                gvInvoice.Visible = true;
                btnSaveInvoice.Visible = true;
                lblInvoiceDate.Visible = true;
                txtInvoiceDate.Visible = true;
                lblUOM.Visible = true;
                ddlUOM.Visible = true;
                lblCurrency.Visible = true;
                ddlCurrency.Visible = true;
                lblConversion.Visible = true;
                txtConversion.Visible = true;
                lblIncomeTax.Visible = true;
                txtIncomeTax.Visible = true;
                lblServiceTax.Visible = true;
                txtServiceTax.Visible = true;
                lblPONo.Visible = true;
                lblPODate.Visible = true;
                txtPONo.Visible = true;
                txtPODate.Visible = true;

                hfPDFFileName.Value = dtInvoiceDetail.Rows[0]["PDFFileName"].ToString();

                txtInvoiceDate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["DOCUMENT_DATE"]).ToString("dd/MM/yyyy");
                txtConversion.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["CONVERSION_RATE"]);
                txtIncomeTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["INCOME_TAX"]);
                txtServiceTax.Text = String.Format("{0:0,0.00}", dtInvoiceDetail.Rows[0]["SERVICE_TAX"]);
                ddlUOM.SelectedValue = dtInvoiceDetail.Rows[0]["UOM"].ToString();
                ddlCurrency.SelectedValue = dtInvoiceDetail.Rows[0]["CURRENCY_ID"].ToString();
                txtPONo.Text = dtInvoiceDetail.Rows[0]["PONo"].ToString();
                if (dtInvoiceDetail.Rows[0]["PODate"].ToString() != "")
                {
                    txtPODate.Text = Convert.ToDateTime(dtInvoiceDetail.Rows[0]["PODate"]).ToString("dd/MM/yyyy");
                }

                btnSaveInvoice.Text = "Save Invoice";

                this.Session.Add("dtInvoiceDetail", dtInvoiceDetail);
                gvInvoice.DataSource = dtInvoiceDetail;
                gvInvoice.DataBind();

                if (gvInvoice.Rows.Count > 0 && gvInvoice.Visible)
                {
                    lblInvoiceDate.Visible = true;
                    txtInvoiceDate.Visible = true;
                    lblUOM.Visible = true;
                    ddlUOM.Visible = true;
                    lblCurrency.Visible = true;
                    ddlCurrency.Visible = true;
                    lblConversion.Visible = true;
                    txtConversion.Visible = true;
                    lblIncomeTax.Visible = true;
                    txtIncomeTax.Visible = true;
                    lblServiceTax.Visible = true;
                    txtServiceTax.Visible = true;
                    lblPONo.Visible = true;
                    lblPODate.Visible = true;
                    txtPONo.Visible = true;
                    txtPODate.Visible = true;
                }

                DataTable dtEnquiryInvoice = QoutCtl.GetEnquiryInvoice(Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue);
                gvAllInvoice.DataSource = dtEnquiryInvoice;
                gvAllInvoice.DataBind();

                ltrlEdit.Text = "";
                ltrlPayment.Text = "";
                ltrlQuotation.Text = "";
                ltrlSendEmail.Text = "";
                ltrlDoc.Text = "";
            }
            else
            {
                ltrlInvoice.Text = Utility.ShowAlert(false, "No Data found.");
            }
        }

        ltrlEdit.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";

    }

    protected void gvAllInvoice_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            LinkButton btnPDF = e.Row.FindControl("btnPDF") as LinkButton;
            LinkButton btnPDF2 = e.Row.FindControl("btnPDF2") as LinkButton;
            LinkButton btnWord = e.Row.FindControl("btnWord") as LinkButton;
            
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnPDF);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnPDF2);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnWord);            
        }
    }

    protected void gvAllInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnActive = (LinkButton)e.Row.FindControl("btnActive");
            LinkButton btnInActive = (LinkButton)e.Row.FindControl("btnInActive");
            if (e.Row.Cells[8].Text == "False")
            {
                e.Row.Cells[7].Text = "";
                e.Row.BackColor = System.Drawing.Color.BurlyWood;
            }
            if (e.Row.Cells[9].Text == "Active")
            {
                btnInActive.Visible = false;
            }
            else
            {
                btnActive.Visible = false;
            }
        }
    }

    #endregion

    #region Payment

    protected void btnSavePayment_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfPaymentIndex.Value;
        ltrlPayment.Text = "";

        //Validate Inputs
        if (ddlPaymentProduct.Items.Count > 0)
        {
            try
            {
                Convert.ToDecimal(txtTotalAmount.Text);
            }
            catch (Exception ex)
            {
                ltrlPayment.Text = Utility.ShowAlert(false, "Only numeric values allowed in Amount and Commission fileds.");
                return;
            }

            //Set Date Variables
            DateTime dtPaymentDelivery = Constants.DateNullValue;
            DateTime dtChequeDate = Constants.DateNullValue;

            if (txtPaymentDate.Text.Length > 0)
            {
                IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
                String datetime = txtPaymentDate.Text;
                dtPaymentDelivery = DateTime.Parse(datetime, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            }           
            

            if (txtTotalAmount.Text != "")
            {
                long EnquiryPamentID = Constants.LongNullValue;

                if (Convert.ToInt32(hfEnquiryPaymentID.Value) > 0)
                {
                    EnquiryPamentID = Convert.ToInt64(hfEnquiryPaymentID.Value);
                    EnqCtl.UpdateEnquiryPayment(Convert.ToInt64(hfEnquiryPaymentID.Value), Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(ddlPaymentProduct.SelectedValue), Convert.ToInt64(ddlPaymentProduct.SelectedValue), txtPaymentRef.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), null, null, dtChequeDate, null, Convert.ToDecimal(txtTotalAmount.Text), dtPaymentDelivery, txtRemarksPayment.Text, Convert.ToInt32(Session["UserID"]));

                    hfEnquiryPaymentID.Value = "";
                }
                else
                {
                    EnquiryPamentID = EnqCtl.InsertEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(ddlPaymentProduct.SelectedValue), Convert.ToInt64(ddlPaymentProduct.SelectedValue), txtPaymentRef.Text, Convert.ToInt32(ddlPaymentMode.SelectedValue), null, null, dtChequeDate,null, Convert.ToDecimal(txtTotalAmount.Text), dtPaymentDelivery, txtRemarksPayment.Text, Convert.ToInt32(Session["UserID"]));
                }

                if (EnquiryPamentID > 0)
                {
                    //Save in Ledger
                    this.PostSaleVoucher(Convert.ToDecimal(txtTotalAmount.Text),EnquiryPamentID);

                    EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.PaymentReceived, true);

                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Payment", "Payment for enquiry no " + hfEnquiryID.Value + " added");

                    GetEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value));

                    ltrlPayment.Text = Utility.ShowAlert(true, "Enquiry payment saved successfully.");

                    ClearPaymentFields();
                    LoadDocumentDetail();
                }
                else
                {
                    ltrlPayment.Text = Utility.ShowAlert(false, "Some error occured. Payment not saved.");
                }
            }
            else
            {
                ltrlPayment.Text = Utility.ShowAlert(false, "Please enter payment amount and comission.");
            }
        }
        else
        {
            ltrlPayment.Text = Utility.ShowAlert(false, "No Invoice found.");
            return;
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    /// <summary>
    /// Saves Cash Realization
    /// </summary>
    private void PostSaleVoucher(decimal Amount, long PaymnetID)
    {
        LedgerController mLController = new LedgerController();
        string strRemarks = "";
        if (ddlPaymentMode.SelectedIndex == 0)
        {
            strRemarks = "Cash Payment";
        }
        else
        {
            strRemarks = "Online Transfer Payment";
        }
        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("LEDGER_ID", typeof(long));
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("PEOPLE_ID", typeof(long));

        DataRow dr = dtVoucher.NewRow();
        dr["Ledger_ID"] = Constants.LongNullValue.ToString();
        dr["ACCOUNT_HEAD_ID"] = 46;
        dr["REMARKS"] = strRemarks;
        dr["DEBIT"] = Amount;
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 1;
        dr["PEOPLE_ID"] = 0;
        dtVoucher.Rows.Add(dr);

        string MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, Convert.ToInt32(ddlLocation.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));

        mLController.Add_Voucher(Convert.ToInt32(ddlLocation.SelectedValue), 0, MaxDocumentId, Constants.Journal_Voucher, Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.CashPayment, lblCustomerView.Text, strRemarks, Constants.DateNullValue, null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue,PaymnetID);

        
    }

    private void ClearPaymentFields()
    {
        txtPaymentRef.Text = string.Empty;        
        txtTotalAmount.Text = string.Empty;
        txtPaymentDate.Text = string.Empty;
        txtRemarks.Text = string.Empty;
    }

    private void GetEnquiryPayment(long EnquiryID)
    {
        DataTable dtEnqPayment = EnqCtl.GetEnquiryPayment(EnquiryID);
        gvPayment.DataSource = dtEnqPayment;
        gvPayment.DataBind();
    }

    /// <summary>
    /// Sets SKU Data For Edit. This Function Runs When An Existing SKU Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPayment_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hfTabIndex.Value = hfPaymentIndex.Value;
        ddlPaymentProduct.SelectedValue = gvPayment.Rows[e.NewEditIndex].Cells[3].Text;
        hfEnquiryPaymentID.Value = gvPayment.Rows[e.NewEditIndex].Cells[1].Text;
        ddlPaymentMode.SelectedValue = gvPayment.Rows[e.NewEditIndex].Cells[5].Text;        
        txtRemarksPayment.Text = gvPayment.Rows[e.NewEditIndex].Cells[10].Text.Replace("&nbsp;", "");
        txtPaymentRef.Text = gvPayment.Rows[e.NewEditIndex].Cells[13].Text.Replace("&nbsp;", "");
        txtTotalAmount.Text = gvPayment.Rows[e.NewEditIndex].Cells[14].Text;
        txtPaymentDate.Text = gvPayment.Rows[e.NewEditIndex].Cells[15].Text.Replace("&nbsp;", "");        
        
    }

    /// <summary>
    /// Deletes A SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvPayment_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        hfTabIndex.Value = hfPaymentIndex.Value;
        ltrlPayment.Text = "";
        if (EnqCtl.DeleteEnquiryPayment(Convert.ToInt64(gvPayment.Rows[e.RowIndex].Cells[1].Text), Convert.ToInt32(Session["UserID"])))
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Payment", "Payment ID" + hfEnquiryPaymentID.Value + " deleted");

            ltrlPayment.Text = Utility.ShowAlert(true, "Payment deleted successfully.");

            GetEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value));
            LoadDocumentDetail();
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnEdit = e.Row.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Row.FindControl("btnDelete") as LinkButton;
            if (e.Row.Cells[16].Text == "Inactive")
            {   
                btnEdit.Text = "";
                btnDelete.Text = "";
                e.Row.BackColor = System.Drawing.Color.BurlyWood;
            }
            if (e.Row.Cells[5].Text == "0")
            {
                btnEdit.Text = "";
            }
        }        
    }

    protected void gvPayment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfPaymentIndex.Value;
        if (e.CommandName == "Active")
        {
            if (QoutCtl.UpdateInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), false))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " updated");

                ltrlPayment.Text = Utility.ShowAlert(true, "Invoice updated successfully..");
            }
            else
            {
                ltrlPayment.Text = Utility.ShowAlert(false, "Some error occured.");
            }
            LoadEditGird();
            GetEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value));
        }

        else if (e.CommandName == "Inactive")
        {
            if (QoutCtl.UpdateInvoice(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt64(e.CommandArgument), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Constants.IntNullValue, Convert.ToInt32(Session["UserID"]), true))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " updated");

                ltrlPayment.Text = Utility.ShowAlert(true, "Invoice updated successfully..");
            }
            else
            {
                ltrlPayment.Text = Utility.ShowAlert(false, "Some error occured.");
            }
            LoadEditGird();
            GetEnquiryPayment(Convert.ToInt64(hfEnquiryID.Value));
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
        ltrlDoc.Text = "";
    }
    
    /// <summary>
    /// Creates Datatable For Bank Transaction
    /// </summary>
    /// <returns></returns>
    private DataTable CreatVoucherTable()
    {
        DataTable dtLedger = new DataTable();
        dtLedger.Columns.Add("VOUCHER_TYPE_ID", typeof(int));
        dtLedger.Columns.Add("VOUCHER_NO", typeof(long));
        dtLedger.Columns.Add("ACCOUNT_HEAD_ID", typeof(long));
        dtLedger.Columns.Add("Distributor_ID", typeof(int));
        dtLedger.Columns.Add("Debit", typeof(decimal));
        dtLedger.Columns.Add("Credit", typeof(decimal));
        dtLedger.Columns.Add("Ledger_Date", typeof(DateTime));
        dtLedger.Columns.Add("Remarks", typeof(string));
        dtLedger.Columns.Add("TimeStamp", typeof(DateTime));
        dtLedger.Columns.Add("Customer_ID", typeof(int));
        dtLedger.Columns.Add("Principal_ID", typeof(int));
        dtLedger.Columns.Add("Cheque_NO", typeof(string));
        dtLedger.Columns.Add("UserId", typeof(int));
        dtLedger.Columns.Add("Document_ID", typeof(long));
        dtLedger.Columns.Add("Manual_Document_ID", typeof(string));
        dtLedger.Columns.Add("DocumentTypeID", typeof(int));
        dtLedger.Columns.Add("SlipNo", typeof(string));
        dtLedger.Columns.Add("ChequeDate", typeof(DateTime));
        dtLedger.Columns.Add("PaymentMode", typeof(int));
        dtLedger.Columns.Add("PayeesName", typeof(string));
        this.Session.Add("dtLedger", dtLedger);
        return dtLedger;
    }

    #endregion

    #region Associated Documents

    private void LoadDocuments()
    {
        gvDocument.DataSource = null;
        gvDocument.DataBind();
        DataTable dtDocs = DocCtl.GetUploadDocument(Convert.ToInt64(Session["EnquiryMasterID"]), Constants.IntNullValue, Constants.LongNullValue);
        if (dtDocs != null && dtDocs.Rows.Count > 0)
        {
            gvDocument.DataSource = dtDocs;
            gvDocument.DataBind();
            ViewState["docs"] = dtDocs;
        }
    }

    private void LoadDocumentCombo()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.DocType, null, Constants.IntNullValue, true);
        clsWebFormUtil.FillDropDownList(this.ddlDoc, dt, 0, 2);
    }

    private void DownLoadFile(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            long sz = fi.Length;
            Response.ClearContent();
            Response.ContentType = MimeType(Path.GetExtension(path));
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(path)));
            Response.AddHeader("Content-Length", sz.ToString("F0"));
            Response.TransmitFile(path);
            Response.End();
        }
        else
        {
            ltrlInvoice.Text = Utility.ShowAlert(false, "File not found.");
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
    }

    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;
        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = rk.GetValue("Content Type").ToString();
        return mime;
    }

    protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfDocumentIndex.Value;
        if (e.CommandName.ToLower() == "del")
        {
            long id = long.Parse(e.CommandArgument.ToString());
            if (DocCtl.DeleteUploadDocument(id, Convert.ToInt32(Session["UserID"])))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Document", "Document for enquiry no " + hfEnquiryID.Value + " deleted");

                LoadDocuments();
                ltrlDoc.Text = Utility.ShowAlert(true, "The selected document has been removed successfully.");
            }
        }
        else if (e.CommandName.ToLower() == "view")
        {
            long id = long.Parse(e.CommandArgument.ToString());
            DataTable dtDoc = DocCtl.GetUploadDocument(Constants.LongNullValue, Constants.IntNullValue, id);
            if (dtDoc.Rows.Count > 0)
            {
                string FName = dtDoc.Rows[0]["NAME"].ToString();
                DownLoadFile(Server.MapPath("~/Docs/" + FName));
            }
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
    }

    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfDocumentIndex.Value;
        if (fuDoc.HasFile)
        {
            string fileName = fuDoc.PostedFile.FileName;
            fuDoc.SaveAs(Server.MapPath("~/Docs/" + hfEnquiryID.Value + "_" + fileName));
            if (txtDocTitle.Text.Trim() != "")
            {
                if (DocCtl.InsertUploadDocument(Convert.ToInt64(Session["EnquiryMasterID"]), Convert.ToInt32(ddlDoc.SelectedValue), Convert.ToInt32(Session["UserID"]), hfEnquiryID.Value + "_" + fileName, txtDocTitle.Text))
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Document", "Document for enquiry no " + hfEnquiryID.Value + " added");

                    ltrlDoc.Text = Utility.ShowAlert(true, "Document has been uploaded successfully.");
                    txtDocTitle.Text = "";
                    LoadDocuments();
                }
            }
            else
            {
                ltrlDoc.Text = Utility.ShowAlert(false, "Please enter Title.");
            }
        }
        else
        {
            ltrlDoc.Text = Utility.ShowAlert(false, "Please browse some document.");
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlSendEmail.Text = "";
    }

    protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnViewDoc = e.Row.FindControl("btnViewDoc") as ImageButton;
            LinkButton lnkViewDoc = e.Row.FindControl("lnkViewDoc") as LinkButton;
            ImageButton btnDel = (ImageButton)e.Row.FindControl("btnDel");

            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnViewDoc);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkViewDoc);

            if (Session["EnqStatus"].ToString() == "Inactive")
            {                
                btnDel.Visible = false;
            }

            if (Session["EnqStatus"].ToString() == "Inactive")
            {               
                btnViewDoc.Visible = false;
                lnkViewDoc.Enabled = false;
            }
        }
    }

    #endregion

    #region Send Email

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfSendEmailIndex.Value;
        if (fuAttachment.HasFile)
        {
            string fileName = fuAttachment.PostedFile.FileName;
            fuAttachment.SaveAs(Server.MapPath("~/Docs/" + hfEnquiryID.Value + "_" + fileName));
            if (DocCtl.InsertUploadDocument(Convert.ToInt64(Session["EnquiryMasterID"]), Convert.ToInt32(ddlDoc.SelectedValue), Convert.ToInt32(Session["UserID"]), hfEnquiryID.Value + "_" + fileName, ""))
            {
                if (SetEmailIDs(ltrlSendEmail, ddlEmailFromQuot))
                {

                    string filePath = Server.MapPath("~/Docs/" + hfEnquiryID.Value + "_" + fileName);

                    EmailCc = txtCc22.Text;
                    CheckBoxList cblCc = cblQuotationCc2.GetAllItems();

                    foreach (ListItem li in cblCc.Items)
                    {
                        if (li.Selected)
                        {
                            EmailCc += "," + li.Value.Split(',')[1].ToString();
                        }
                    }

                    string[] strEmailCc = EmailCc.Split(',');

                    if (SendEmail(ddlEmailTo.SelectedValue, strEmailCc, filePath))
                    {
                        QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), ddlEmailTo.SelectedValue, Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));

                        if (txtCc22.Text.Length > 0)
                        {
                            QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), 0, Constants.ReceipientCustomer, Convert.ToInt64(hfCustomerID.Value), txtCc22.Text, Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                        }

                        if (ddlType.SelectedIndex == 1)
                        {
                            EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.InvoiceSent, true);

                            //Save Transaction in log
                            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Invoice", "Invoice for enquiry no " + hfEnquiryID.Value + " sent");

                            ltrlSendEmail.Text = Utility.ShowAlert(true, "Invoice Sent Successfully.");
                        }
                        else
                        {
                            EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.QuotationSent, true);

                            //Save Transaction in log
                            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Quotation", "Quotation for enquiry no " + hfEnquiryID.Value + " sent");

                            ltrlSendEmail.Text = Utility.ShowAlert(true, "Quotation Sent Successfully.");
                        }
                    }
                    else
                    {
                        ltrlSendEmail.Text = Utility.ShowAlert(false, "While sending email some error occured.");
                    }
                }
                LoadDocuments();
            }
        }
        else
        {
            ltrlSendEmail.Text = Utility.ShowAlert(false, "Please browse some document.");
        }

        ltrlEdit.Text = "";
        ltrlInvoice.Text = "";
        ltrlPayment.Text = "";
        ltrlQuotation.Text = "";
        ltrlDoc.Text = "";
    }

    #endregion

    #region Role Rights

    private void ApplyRoles()
    {
        int TabIndexes = 0;
        ltrlTabs.Text = "<ul class='TabbedPanelsTabGroup'>";

        if (!CanView("0"))
        {
            ActiveTabIndex.Value = "0";
            hfViewIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0'>View Enquiry </li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0' style='display:none'>View Enquiry </li>";
        }
        if (!CanView("1"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "1";
            }

            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1'>Edit Enquiry</li>";
            hfEditIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                btnSaveEnquiry.Visible = false;
                btnSaveDocument.Visible = false;
                gvMainEdit.Columns[4].Visible = false;
                gvMainEdit.Columns[5].Visible = false;
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                gvMainEdit.Columns[5].Visible = false;
            }
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1' style='display:none'>Edit Enquiry</li>";
        }

        if (!CanView("4"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "2";
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                btnWord.Visible = false;
                btnPDF.Visible = false;
                btnSendQuotation.Visible = false;
                btnSaveQuotation.Visible = false;
            }
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='4'>Quotation</li>";
            hfQuotationIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='4' style='display:none'>Quotation</li>";
        }

        if (!CanView("6"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "6";
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                btnInvoicePDF.Visible = false;
                btnEmailInvoice.Visible = false;
                btnSaveInvoice.Visible = false;
                btnNewInvoiceFromQuotion.Visible = false;
                btnNewInvoice.Visible = false;
                gvAllInvoice.Columns[6].Visible = false;
            }
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='6'>Invoice</li>";
            hfInvoiceIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='6' style='display:none'>Invoice</li>";
        }
        if (CanView("7"))
        {
            FSCommonLayer.Classes.Configuration.GetAccountHead();
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "5";
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                gvPayment.Columns[16].Visible = false;
                btnSavePayment.Visible = false;
            }

            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                gvPayment.Columns[16].Visible = false;
            }
            hfPaymentIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='7'>Payment</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='7' style='display:none'>Payment</li>";
        }
        if (!CanView("8"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "6";
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                btnAddDoc.Visible = false;
            }
            hfDocumentIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='8'>Associated Documents</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='8' style='display:none'>Associated Documents</li>";
        }

        if (!CanView("9"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "7";
            }
            if (Session["EnqStatus"].ToString() == "Inactive")
            {
                btnSendEmail.Visible = false;
            }
            hfSendEmailIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='9'>Send Email</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='9' style='display:none'>Send Email</li>";
        }

        ltrlTabs.Text += "</ul>";

    }

    private bool CanView(string mIndex)
    {
        DataTable dtTabRights = (DataTable)Session["dtTabRights"];
        bool flag = false;
        if (dtTabRights != null)
        {
            foreach (DataRow dr in dtTabRights.Rows)
            {
                if (dr["MODULE_SUB_INDEX"].ToString() == mIndex)
                {
                    if (Convert.ToBoolean(dr["CAN_VIEW"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private bool CanEdit(string mIndex)
    {
        DataTable dtTabRights = (DataTable)Session["dtTabRights"];
        bool flag = false;
        if (dtTabRights != null)
        {
            foreach (DataRow dr in dtTabRights.Rows)
            {
                if (dr["MODULE_SUB_INDEX"].ToString() == mIndex)
                {
                    if (Convert.ToBoolean(dr["CAN_EDIT"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    private bool CanDelete(string mIndex)
    {
        DataTable dtTabRights = (DataTable)Session["dtTabRights"];
        bool flag = false;
        if (dtTabRights != null)
        {
            foreach (DataRow dr in dtTabRights.Rows)
            {
                if (dr["MODULE_SUB_INDEX"].ToString() == mIndex)
                {
                    if (Convert.ToBoolean(dr["CAN_DELETE"]))
                    {
                        flag = true;
                        break;
                    }
                }
            }
        }
        return flag;
    }

    #endregion    
}