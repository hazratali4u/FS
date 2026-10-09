using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections.Generic;

/// <summary>
/// Form To Add, Edit Cheques
/// </summary>
public partial class Forms_frmChequeEntry : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos, ListBox And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DrpStatus.Items.Add(new ListItem("Cash", Constants.CashPayment.ToString()));
            DrpStatus.Items.Add(new ListItem("Online Transfer", Constants.OnlineTransfer.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Received", Constants.Cheque_Pending.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Deposit", Constants.Cheque_Deposit.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Realize", Constants.Cheque_Clear.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Bounce", Constants.Cheque_Bons.ToString()));
            DrpStatus.Items.Add(new ListItem("Cheque Cancel", Constants.Cheque_Cancel.ToString()));
            this.LoadAccountHead();
            this.LoadDistributor();
            this.LoadData();
            this.SelectCreditInvoice();
            this.LoadReceviedCheque();
            btnSave.Attributes.Add("onclick", "return ValidateForm();");

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
    /// Loads Customers To Customer Combo
    /// </summary>
    private void LoadData()
    {
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        OrganizationController OrgCtl = new OrganizationController();
        if (Session["RoleID"].ToString() == "1")
        {
            DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 100);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
        }
        else
        {
            DataTable m_dt = OrgCtl.GetOrganization(Convert.ToInt32(Session["UserID"]), 100);
            clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    private void ClearAll()
    {
        txtChequeNo.Text = "";
        txtAmount.Text = "";
        txtBankName.Text = "";
        txtStartDate.Text = "";
        txtRealDate.Text = "";
        btnSave.Text = "Save";
        txtSlipNo.Text = "";
        txtRemarks.Text = "";

    }
    
    /// <summary>
    /// Loads Cheques To Grid
    /// </summary>
    private void LoadReceviedCheque()
    {
       if (DrpStatus.SelectedValue.ToString() != Constants.Cheque_Clear.ToString())
        {
            ChequeEntryController CController = new ChequeEntryController();
            DataTable dt = CController.SelectChequeEntry(int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), 1, Convert.ToInt32(Session["UserID"]));
            this.Session.Add("dt", dt);
            GrdOrder.DataSource = dt;
            GrdOrder.DataBind();
        }
        else
        {
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();
        }
    }

    /// <summary>
    /// Loads Crdit Invoices To Grid
    /// </summary>
    private void SelectCreditInvoice()
    {
        QuotationController QoutCtl = new QuotationController();
        GrdCredit.DataSource = null;
        GrdCredit.DataBind();
        if (DrpCustomer.Items.Count > 0 )
        {
            DataTable dtCredit = QoutCtl.GetEnquiryInvoice(Constants.LongNullValue, Convert.ToInt32(DrpCustomer.SelectedValue));
            GrdCredit.DataSource = dtCredit;
            GrdCredit.DataBind();
            Session.Add("dtCredit", dtCredit);
            hfData.Value = GetJson(dtCredit);            
        }
    }

    public string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row = null;

        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    
    /// <summary>
    /// Saves Cheque Realization
    /// </summary>
    private void ChequeRealization()
    {
        LedgerController LController = new LedgerController();
        EnquiryController EnqCtl = new EnquiryController();
        DateTime ChequeDate;
        DateTime RealDate = Constants.DateNullValue;
        if (txtStartDate.Text.Length == 10)
        {
            ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

        }
        else
        {
            ChequeDate = DateTime.Now;
        }
        if (txtRealDate.Text.Length == 10)
        {
            RealDate = Convert.ToDateTime(ConvertDate.British_To_American(txtRealDate.Text));
        }

        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Bank_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal OfferAmount = 0;
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                OfferAmount =  decimal.Parse(dr.Cells[4].Text);
                long PaymentID = EnqCtl.InsertEnquiryPayment(Convert.ToInt64(dr.Cells[8].Text), Convert.ToInt64(dr.Cells[0].Text), Convert.ToInt64(dr.Cells[0].Text), dr.Cells[2].Text, 0, txtChequeNo.Text, txtBankName.Text, ChequeDate, txtSlipNo.Text, OfferAmount, RealDate, txtRemarks.Text, Convert.ToInt32(Session["UserID"]));

                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                RealDate, "Cheque Relization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 1,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), PaymentID, PaymentID.ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");

                LController.PostingCash_Bank_Account(Constants.Bank_Voucher, long.Parse(MaxDocumentId), long.Parse(DrpBankAccount.SelectedValue.ToString()), int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                RealDate, "Cheque Relization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 1,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), PaymentID, PaymentID.ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, 18, "");
                PostVoucher(OfferAmount, PaymentID, Constants.Bank_Voucher);                
            }
        }
    }

    private void CashRealization(int PaymentMode)
    {
        LedgerController LController = new LedgerController();
        EnquiryController EnqCtl = new EnquiryController();
        DateTime ChequeDate;
        DateTime RealDate = DateTime.Now.Date;

        int PaymentMode2 = 1;
        if (PaymentMode == Constants.OnlineTransfer)
        {
            PaymentMode2 = 2;
        }

        if (txtStartDate.Text.Length == 10)
        {
            ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));

        }
        else
        {
            ChequeDate = DateTime.Now;
        }

        if (txtRealDate.Text.Length == 10)
        {
            RealDate = Convert.ToDateTime(ConvertDate.British_To_American(txtRealDate.Text));
        }

        string MaxDocumentId = LController.SelectLedgerMaxDocumentId(Constants.Cash_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()));
        decimal OfferAmount = 0;
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {
                OfferAmount = decimal.Parse(dr.Cells[4].Text);
                long PaymentID = EnqCtl.InsertEnquiryPayment(Convert.ToInt64(dr.Cells[8].Text), Convert.ToInt64(dr.Cells[0].Text), Convert.ToInt64(dr.Cells[0].Text), dr.Cells[2].Text, PaymentMode2, txtChequeNo.Text, txtBankName.Text, ChequeDate, txtSlipNo.Text, OfferAmount, System.DateTime.Now, txtRemarks.Text, Convert.ToInt32(Session["UserID"]));

                LController.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(MaxDocumentId), 107, int.Parse(drpDistributor.SelectedValue.ToString()), 0, OfferAmount,
                    RealDate, "Cash Relization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 1,
                    txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), PaymentID, PaymentID.ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "");

                LController.PostingCash_Bank_Account(Constants.Cash_Voucher, long.Parse(MaxDocumentId), 46, int.Parse(drpDistributor.SelectedValue.ToString()), OfferAmount, 0,
                RealDate, "Cash Relization", DateTime.Now, int.Parse(DrpCustomer.SelectedValue.ToString()), 1,
                txtChequeNo.Text, int.Parse(this.Session["UserId"].ToString()), PaymentID, PaymentID.ToString(), Constants.Document_Invoice, txtSlipNo.Text, Constants.DateNullValue, PaymentMode, "");

                PostVoucher(OfferAmount, PaymentID, Constants.Journal_Voucher);
            }
        }
    }

    /// <summary>
    /// Post Cheque Voucher
    /// </summary>
    private void PostVoucher(decimal Amount, long PaymnetID,int VoucherType)
    {
        DateTime RealDate = Constants.DateNullValue;        
        if (txtRealDate.Text.Length == 10)
        {
            RealDate = Convert.ToDateTime(ConvertDate.British_To_American(txtRealDate.Text));
        }

        string strRemarks = "Cheque Realization";     
        int PaymentType = Constants.ChequePayment;
        int AccountHeadID = Convert.ToInt32(DrpBankAccount.SelectedValue);
        if (VoucherType == Constants.Cash_Voucher)
        {
            PaymentType = Constants.CashPayment;
            strRemarks = "Cash Realization";
            AccountHeadID = 46;
        }
        LedgerController mLController = new LedgerController();
        
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
        dr["ACCOUNT_HEAD_ID"] = AccountHeadID;
        dr["REMARKS"] = strRemarks;
        dr["DEBIT"] = Amount;
        dr["CREDIT"] = 0;
        dr["Principal_Id"] = 1;
        dr["PEOPLE_ID"] = 0;
        dtVoucher.Rows.Add(dr);

        DataRow dr2 = dtVoucher.NewRow();
        dr2["Ledger_ID"] = Constants.LongNullValue.ToString();
        dr2["ACCOUNT_HEAD_ID"] = 107;
        dr2["REMARKS"] = strRemarks;
        dr2["DEBIT"] = 0;
        dr2["CREDIT"] = Amount;
        dr2["Principal_Id"] = 1;
        dr2["PEOPLE_ID"] = 0;
        dtVoucher.Rows.Add(dr2);
        
        string MaxDocumentId = mLController.SelectMaxVoucherId(VoucherType, Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]));
        mLController.Add_Voucher(Convert.ToInt32(drpDistributor.SelectedValue), 0, MaxDocumentId, VoucherType, RealDate, PaymentType, DrpCustomer.SelectedItem.Text, strRemarks, Constants.DateNullValue, null, dtVoucher, Convert.ToInt32(Session["UserID"]), null, Constants.DateNullValue, PaymnetID);
    }

    /// <summary>
    /// Loads Account Heads To Account Combo
    /// </summary>
    private void LoadAccountHead()
    {
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(FSCommonLayer.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(DrpBankAccount, dt, 0, 4, true);

        DrpBankAccount.SelectedIndex = 2;
    }

    /// <summary>
    /// Save Or Updates a Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ChequeEntryController CController = new ChequeEntryController();
        DataControl dc = new DataControl();

        DateTime ChequeDate;
        DateTime RealDate = Constants.DateNullValue;

        if (txtStartDate.Text.Length == 10)
        {
            ChequeDate = DateTime.Parse(ConvertDate.British_To_American(txtStartDate.Text));
        }
        else
        {
            ChequeDate = DateTime.Now;
        }

        if (txtRealDate.Text.Length == 10)
        {
            RealDate = DateTime.Parse(ConvertDate.British_To_American(txtRealDate.Text));
        }

        int InvoiceCount = Constants.IntNullValue;
        
        foreach (GridViewRow dr in GrdCredit.Rows)
        {
            CheckBox chRelized = (CheckBox)dr.Cells[0].FindControl("ChbIsAssigned");
            if (chRelized.Checked == true)
            {                
                InvoiceCount++;                
            }
        }

        if (InvoiceCount == Constants.IntNullValue)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Must Select Invoice');", true);
            return;
        }

        if (btnSave.Text == "Save")
        {
            if (DrpStatus.SelectedValue == Constants.Cheque_Pending.ToString())
            {
                HFChqueProcessId.Value = CController.InsertChequeEntry(int.Parse(drpDistributor.SelectedValue.ToString()), 1, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate,
                    DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Constants.DateNullValue, RealDate, decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), DateTime.Now,Convert.ToInt32(Session["UserID"]), txtSlipNo.Text, txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        TextBox txtWHSTax = (TextBox)dr.FindControl("txtWHSTax");
                        TextBox txtWHITax = (TextBox)dr.FindControl("txtWHITax");
                        TextBox txtPayable = (TextBox)dr.FindControl("txtPayable");
                        if (txtWHITax.Text.Length > 0 || txtWHSTax.Text.Length > 0 || txtPayable.Text.Length > 0)
                        {
                            CController.InsertChequeEntry2(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToDecimal(dc.chkNull_0(txtWHITax.Text)), Convert.ToDecimal(dc.chkNull_0(txtWHSTax.Text)), Convert.ToDecimal(dc.chkNull_0(txtPayable.Text)), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                        }
                        CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                    }
                }
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Cheque Entry", DrpStatus.SelectedItem.Text + " added");
            }
            else if (DrpStatus.SelectedValue == Constants.CashPayment.ToString() || DrpStatus.SelectedValue == Constants.OnlineTransfer.ToString())
            {
                this.CashRealization(Convert.ToInt32(DrpStatus.SelectedValue));
                this.LoadData();
                this.SelectCreditInvoice();
            }
        }
        else
        {
            if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Pending)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), int.Parse(drpDistributor.SelectedValue.ToString()), 1, long.Parse(DrpCustomer.SelectedValue.ToString()), txtChequeNo.Text, txtBankName.Text, ChequeDate, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Constants.DateNullValue, RealDate,
                   decimal.Parse(txtAmount.Text), int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["UserID"]), txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));
                CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        TextBox txtWHSTax = (TextBox)dr.FindControl("txtWHSTax");
                        TextBox txtWHITax = (TextBox)dr.FindControl("txtWHITax");
                        TextBox txtPayable = (TextBox)dr.FindControl("txtPayable");
                        CController.InsertChequeEntry2(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToDecimal(dc.chkNull_0(txtWHITax.Text)), Convert.ToDecimal(dc.chkNull_0(txtWHSTax.Text)), Convert.ToDecimal(dc.chkNull_0(txtPayable.Text)), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                        CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                    }
                }
            }
            else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Deposit)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()), Constants.DateNullValue,
                     Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["UserID"]), txtRemarks.Text, int.Parse(DrpBankAccount.SelectedValue.ToString()));                
            }
            else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Bons || int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Cancel)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()),
                    Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["UserID"]), txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));                
            }
            else if (int.Parse(DrpStatus.SelectedValue.ToString()) == Constants.Cheque_Clear)
            {
                CController.UpdateChequeEntry(long.Parse(HFChqueProcessId.Value), Constants.IntNullValue, Constants.IntNullValue, Constants.LongNullValue, null, null, Constants.DateNullValue, Constants.DateNullValue, Constants.DateNullValue, RealDate,
                                                 Constants.DecimalNullValue, int.Parse(DrpStatus.SelectedValue.ToString()), Constants.DateNullValue, txtSlipNo.Text, Convert.ToInt32(Session["UserID"]), txtRemarks.Text, long.Parse(DrpBankAccount.SelectedValue.ToString()));                
                CController.SelectChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), 1);
                foreach (GridViewRow dr in GrdCredit.Rows)
                {
                    CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
                    if (chRelized.Checked == true)
                    {
                        TextBox txtWHSTax = (TextBox)dr.FindControl("txtWHSTax");
                        TextBox txtWHITax = (TextBox)dr.FindControl("txtWHITax");
                        TextBox txtPayable = (TextBox)dr.FindControl("txtPayable");
                        CController.InsertChequeEntry2(Convert.ToInt64(HFChqueProcessId.Value), Convert.ToDecimal(dc.chkNull_0(txtWHITax.Text)), Convert.ToDecimal(dc.chkNull_0(txtWHSTax.Text)), Convert.ToDecimal(dc.chkNull_0(txtPayable.Text)), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                        CController.InsertChequeEntryInvoice(long.Parse(HFChqueProcessId.Value), Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]));
                    }
                }
                this.ChequeRealization();
                this.LoadData();
                this.SelectCreditInvoice();
            }
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Cheque Entry", DrpStatus.SelectedItem.Text + " added");
        }
        this.ClearAll();
        this.LoadReceviedCheque();
    }

    /// <summary>
    /// Cancels Cheque Entry
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearAll();
    }

    /// <summary>
    /// Deletes Cheque
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (DrpStatus.SelectedValue == Constants.CashPayment.ToString())
        {
            EnquiryController EnqCtl = new EnquiryController();
            if (EnqCtl.DeleteEnquiryPayment(Convert.ToInt64(GrdOrder.Rows[e.RowIndex].Cells[0].Text), Convert.ToInt32(Session["UserID"])))
            {
                this.LoadReceviedCheque();
            }
        }
        else
        {
            ChequeEntryController CController = new ChequeEntryController();
            CController.DeleteChequeEntry(long.Parse(GrdOrder.Rows[e.RowIndex].Cells[0].Text));
            this.LoadReceviedCheque();
        }        
    }

    /// <summary>
    /// Loads Credit Invoices
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.SelectCreditInvoice();
    }

    /// <summary>
    /// Loads Routes, Customers, Credit Invoices And Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadData();
        this.SelectCreditInvoice();
        this.LoadReceviedCheque();
    }

    /// <summary>
    /// Sets Cheque Data For Edit. This Function Runs When An Existing Cheque Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (DrpStatus.SelectedValue != Constants.CashPayment.ToString())
        {
            DataControl dc = new DataControl();
            ChequeEntryController Ccontroller = new ChequeEntryController();
            HFChqueProcessId.Value = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;

            this.LoadData();

            DrpCustomer.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[1].Text;

            txtChequeNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
            txtBankName.Text = GrdOrder.Rows[e.NewEditIndex].Cells[4].Text;
            txtStartDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[5].Text;
            txtRealDate.Text = GrdOrder.Rows[e.NewEditIndex].Cells[13].Text;
            txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[8].Text;
            txtSlipNo.Text = GrdOrder.Rows[e.NewEditIndex].Cells[9].Text;
            txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[10].Text;
            DrpBankAccount.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[12].Text;
            btnSave.Text = "Update";
            this.SelectCreditInvoice();

            DataTable dt = Ccontroller.SelectChequeEntry2(long.Parse(HFChqueProcessId.Value));

            decimal TotalAmount = 0;
            decimal TotalWHITax = 0;
            decimal TotalWHSTax = 0;
            decimal TotalTax = 0;

            foreach (GridViewRow dr in GrdCredit.Rows)
            {
                foreach (DataRow dbr in dt.Rows)
                {

                    if (Convert.ToInt64(GrdCredit.DataKeys[dr.RowIndex].Values["INVOICE_MASTER_ID"]) == Convert.ToInt64(dbr["INVOICE_ID"]))
                    {                        
                        CheckBox chRelized = (CheckBox)dr.Cells[1].FindControl("ChbIsAssigned");
                        chRelized.Checked = true;

                        TextBox txtWHITax = (TextBox)dr.FindControl("txtWHITax");
                        TextBox txtWHSTax = (TextBox)dr.FindControl("txtWHSTax");
                        TextBox txtPayable = (TextBox)dr.FindControl("txtPayable");

                        TotalAmount += Convert.ToDecimal(dr.Cells[4].Text);
                        TotalWHITax += Convert.ToDecimal(dbr["WHT"]);
                        txtWHITax.Text = dbr["WHT"].ToString();
                        TotalWHSTax += Convert.ToDecimal(dbr["WHST"]);
                        txtWHSTax.Text = dbr["WHST"].ToString();
                        TotalTax += Convert.ToDecimal(dbr["TAX"]);
                        txtPayable.Text = dbr["TAX"].ToString();
                    }
                }
            }
            txtTotal.Text = TotalAmount.ToString();
            txtTaxIncome.Text = TotalWHITax.ToString();
            txtTaxSales.Text = TotalWHSTax.ToString();
            txtTaxPayable.Text = TotalTax.ToString();
        }
    }

    /// <summary>
    /// Loads Cheques
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal incometax = 0;
        decimal servicetax = 0;
        decimal salestax = 0;

        DataTable dtCredit = (DataTable)Session["dtCredit"];
        foreach (GridViewRow gvr in GrdCredit.Rows)
        {
            CheckBox ChbIsAssigned = (CheckBox)gvr.FindControl("ChbIsAssigned");
            if (ChbIsAssigned.Checked)
            {
                foreach (DataRow dr in dtCredit.Rows)
                {
                    if (dr["INVOICE_MASTER_ID"].ToString() == gvr.Cells[0].Text)
                    {
                        incometax += Convert.ToDecimal(dr["InvoiceTotal"]) * Convert.ToDecimal(dr["INCOME_TAX"]) / 100;

                        decimal exclgst = Convert.ToDecimal(dr["InvoiceTotal2"]) + (Convert.ToDecimal(dr["InvoiceTotal2"]) * Convert.ToDecimal(dr["INCOME_TAX"]) / 100);
                        decimal salestax2 = exclgst * Convert.ToDecimal(dr["SERVICE_TAX"]) / 100;
                        salestax += salestax2;
                        servicetax += salestax2 * 20 / 100;
                    }
                }
            }
        }
        if (btnSave.Text == "Save")
        {
            this.LoadReceviedCheque();
        }

        if (DrpStatus.SelectedValue == Constants.CashPayment.ToString())
        {
            DrpBankAccount.SelectedIndex = 2;
        }
        else
        {
            DrpBankAccount.SelectedIndex = 0;
        }
    }

    /// <summary>
    /// Filters Cheuqe Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)this.Session["dt"];
        switch (ddSearchType.SelectedIndex)
        {
            case 1:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 2:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 3:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            case 4:
                dt.DefaultView.RowFilter = ddSearchType.SelectedValue.ToString() + " like '%" + txtSeach.Text + "%'";
                break;
            default:
                dt.DefaultView.RowFilter = "CHEQUE_NO" + " like '%" + "" + "%'";
                break;
        }
        GrdOrder.DataSource = dt.DefaultView;
        GrdOrder.DataBind();
    }

    protected void GrdCredit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox ChbIsAssigned = (CheckBox)e.Row.FindControl("ChbIsAssigned");
            TextBox txtWHITax = (TextBox)e.Row.FindControl("txtWHITax");
            TextBox txtWHSTax = (TextBox)e.Row.FindControl("txtWHSTax");
            TextBox txtPayable = (TextBox)e.Row.FindControl("txtPayable");
            string strScript = "CalculateWHTax(" + e.Row.Cells[0].Text + "," + ChbIsAssigned.ClientID + "," + txtWHITax.ClientID + "," + txtWHSTax.ClientID + "," + txtPayable.ClientID + ");";
            ChbIsAssigned.Attributes.Add("onchange", strScript);
        }
    }
}