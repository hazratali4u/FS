using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Branch Expense And Salary
/// </summary>
public partial class frmExpenseEntry : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            txtDate.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
            ApplyRoles();
            this.LoadDistributor();
            this.LoadAccountHead();
            this.CreatTable();
            this.LoadVoucherNo();
            this.LoadBanks();
            btnAddNew.Attributes.Add("onclick", "return ValidateForm();");
            ScriptManager.GetCurrent(Page).SetFocus(drpDistributor);
            lblRowId.Text = "-1";            
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
    /// Loads Account Heads To AccountHead Combo
    /// </summary>
    private void LoadAccountHead()
    {
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.GetAssignAccountHead(0, 47);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);

        DataTable dt2 = mAccountController.GetAssignAccountHead(0, 601);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt2, 0, 4);
    }
        
    /// <summary>
    /// Creates Datatable For Expense And Salary
    /// </summary>
    private void CreatTable()
    {
        DataTable  dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Account_Code", typeof(string));
        dtVoucher.Columns.Add("Account_Name", typeof(string));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("Principal", typeof(string));
        dtVoucher.Columns.Add("ACCOUNT_PARENT_ID", typeof(string));
        this.Session.Add("dtVoucher", dtVoucher);

    }

    /// <summary>
    /// Loads Voucher Nos To Voucher No Combo
    /// </summary>
    private void LoadVoucherNo()
    {
        LedgerController LController = new LedgerController();
        DataTable  dt  = LController.SelectVoucherNo(Convert.ToDateTime(txtDate.Text), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()),null);
        DrpVoucherNo.Items.Clear();
        DrpVoucherNo.Items.Add(new ListItem("New", "0"));
        clsWebFormUtil.FillDropDownList(DrpVoucherNo, dt, 3, 3);   
    }

    /// <summary>
    /// Deletes Expense And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable  dtVoucher = (DataTable)this.Session["dtVoucher"];
        dtVoucher.Rows.RemoveAt(e.RowIndex);
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        lblRowId.Text = "-1";

        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            if (dr["ACCOUNT_HEAD_ID"].ToString() != "107" && dr["ACCOUNT_HEAD_ID"].ToString() != "136" && Convert.ToDecimal(dr["DEBIT"]) > 0)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            }
        }
        txtMainCash.Text = TotalDebit.ToString();
  
    }

    /// <summary>
    /// Sets Expense And Salary Data For Edit. This Function Runs When An Existing Expense And Salary Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void GrdOrder_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblRowId.Text  = e.NewEditIndex.ToString();
        this.LoadAccountHead(); 
        DrpAccountHead.SelectedValue = GrdOrder.Rows[e.NewEditIndex].Cells[0].Text;
        txtRemarks.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
        txtAmount.Text = GrdOrder.Rows[e.NewEditIndex].Cells[3].Text;
    }

    /// <summary>
    /// Adds Record To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataControl dc = new DataControl();
        DataTable  dtVoucher = (DataTable)this.Session["dtVoucher"];
        if (lblRowId.Text == "-1")
        {
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue.ToString();
            dr["ACCOUNT_NAME"] = DrpAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = txtRemarks.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = 61;//Branch Expenses  (FDM EXP)
            dtVoucher.Rows.Add(dr);

        }
        else
        {
            DataRow dr = dtVoucher.Rows[int.Parse(lblRowId.Text)];
            dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue.ToString();
            dr["ACCOUNT_NAME"] = DrpAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = txtRemarks.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = 61; //Branch Expenses  (FDM EXP)
        }

        DataTable dtVoucher2 = dtVoucher.Clone();
        foreach (DataRow dr in dtVoucher.Rows)
        {
            if (dr["ACCOUNT_HEAD_ID"].ToString() != "107" && dr["ACCOUNT_HEAD_ID"].ToString() != "136" && Convert.ToDecimal(dr["DEBIT"]) > 0)
            {
                DataRow dr2 = dtVoucher2.NewRow();
                dr2["ACCOUNT_HEAD_ID"] = dr["ACCOUNT_HEAD_ID"];
                dr2["ACCOUNT_NAME"] = dr["ACCOUNT_NAME"];
                dr2["DEBIT"] = dr["DEBIT"];
                dr2["Credit"] = dr["Credit"];
                dr2["REMARKS"] = dr["REMARKS"];
                dr2["Principal"] = "0";
                dr2["Principal_id"] = "0";
                dtVoucher2.Rows.Add(dr2);
            }
        }

        GrdOrder.DataSource = dtVoucher2;
        GrdOrder.DataBind();
        txtAmount.Text = "";
        txtRemarks.Text = "";
        lblRowId.Text = "-1";
        ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
        
        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            if (dr["ACCOUNT_HEAD_ID"].ToString() != "107" && dr["ACCOUNT_HEAD_ID"].ToString() != "136" && Convert.ToDecimal(dr["DEBIT"]) > 0)
            {
                TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
            }
        }
        txtMainCash.Text = TotalDebit.ToString();
    }

    /// <summary>
    /// Save Or Updates Expenses And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        DataControl dc = new DataControl();
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];
        bool IsSaveVoucher;

        string MaxDocumentId;

        if (dtVoucher.Rows.Count > 0)
        {
            if (DrpVoucherNo.SelectedValue.ToString() == "0")
            {
                DataRow dr = dtVoucher.NewRow();
                dr["ACCOUNT_HEAD_ID"] = FSCommonLayer.Classes.Configuration.PeatyCash;
                dr["ACCOUNT_NAME"] = "Peatty Cash";
                dr["DEBIT"] = 0;
                dr["Credit"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                dr["REMARKS"] = "Patty Cash Credit";
                dr["Principal"] = "0";
                dr["Principal_id"] = "0";
                dtVoucher.Rows.Add(dr);

                int AccountHeadID = Convert.ToInt32(ddlBank.SelectedValue);

                // Credit Bank Account
                DataRow drBank = dtVoucher.NewRow();
                drBank["ACCOUNT_HEAD_ID"] = AccountHeadID;
                drBank["ACCOUNT_NAME"] = "Bank";
                drBank["REMARKS"] = "Payment to " + DrpAccountHead.SelectedItem.Text;
                drBank["DEBIT"] = 0;
                drBank["CREDIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                dr["Principal"] = "0";
                drBank["Principal_Id"] = 0;
                dtVoucher.Rows.Add(drBank);

                DataRow drBank2 = dtVoucher.NewRow();
                drBank2["ACCOUNT_NAME"] = "Bank";
                drBank2["ACCOUNT_HEAD_ID"] = 107;
                drBank2["REMARKS"] = "Payment to " + DrpAccountHead.SelectedItem.Text;
                drBank2["DEBIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                drBank2["CREDIT"] = 0;
                dr["Principal"] = "0";
                drBank2["Principal_Id"] = 0;
                dtVoucher.Rows.Add(drBank2);
                MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()),Convert.ToDateTime(txtDate.Text));

                IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
                       Convert.ToDateTime(txtDate.Text), Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text);
            }
            else
            {

                foreach (DataRow dr in dtVoucher.Rows)
                {
                    if (dr["ACCOUNT_HEAD_ID"].ToString() == FSCommonLayer.Classes.Configuration.PeatyCash)
                    {
                        DataRow dr2 = dr;
                        dr2["Credit"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                    }

                    if (dr["ACCOUNT_HEAD_ID"].ToString() == "107")
                    {
                        DataRow dr2 = dr;
                        dr2["REMARKS"] = "Payment to " + DrpAccountHead.SelectedItem.Text;
                        dr2["DEBIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                    }

                    if (IsBankIDExists(dr["ACCOUNT_HEAD_ID"].ToString()))
                    {
                        DataRow dr2 = dr;
                        dr2["ACCOUNT_HEAD_ID"] = ddlBank.SelectedValue;
                        dr2["REMARKS"] = "Payment to " + DrpAccountHead.SelectedItem.Text;
                        dr2["CREDIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
                    }
                    
                }

                MaxDocumentId = DrpVoucherNo.SelectedValue.ToString();

                IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
                       Convert.ToDateTime(txtDate.Text), Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text);
            }

            if (IsSaveVoucher == true)
            {
                dtVoucher.Rows.Clear();
                GrdOrder.DataSource = null;
                GrdOrder.DataBind();
                txtAmount.Text = "";
                txtRemarks.Text = "";
                lblRowId.Text = "-1";
                ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);

                decimal TotalDebit = 0;

                foreach (DataRow dr1 in dtVoucher.Rows)
                {
                    if (dr1["ACCOUNT_HEAD_ID"].ToString() != "107" && dr1["ACCOUNT_HEAD_ID"].ToString() != "136" && Convert.ToDecimal(dr1["DEBIT"]) > 0)
                    {
                        TotalDebit += decimal.Parse(dr1["DEBIT"].ToString());
                    }
                }
                txtMainCash.Text = TotalDebit.ToString();
                this.LoadVoucherNo();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error in Voucher')", true);
                return;
            }
        }
        else if (lblRowId.Text == "-1")
        {
            MaxDocumentId = DrpVoucherNo.SelectedValue.ToString();

            IsSaveVoucher = mLController.Delete_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()),MaxDocumentId, Constants.Journal_Voucher);
            Session.Remove("dtVoucher");
            LoadVoucherNo();
        }
    }

    /// <summary>
    /// Loads Expense And Salary Data To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void DrpVoucherNo_SelectedIndexChanged(object sender, EventArgs e)
    {       
        LedgerController LController = new LedgerController();
        DataTable dtVoucher = (DataTable)this.Session["dtVoucher"];

        if (DrpVoucherNo.SelectedValue.ToString() != "0")
        {
            dtVoucher = LController.SelectVoucherDetail(int.Parse(drpDistributor.SelectedValue.ToString()), DrpVoucherNo.SelectedValue.ToString(), Constants.Journal_Voucher);
            DataTable dtVoucher2 = dtVoucher.Clone();
            foreach(DataRow dr in dtVoucher.Rows)
            {
                if (dr["ACCOUNT_HEAD_ID"].ToString() != "107" && dr["ACCOUNT_HEAD_ID"].ToString() != "136" && Convert.ToDecimal(dr["DEBIT"]) > 0)
                {
                    DataRow dr2 = dtVoucher2.NewRow();
                    dr2["ACCOUNT_HEAD_ID"] = dr["ACCOUNT_HEAD_ID"];
                    dr2["ACCOUNT_NAME"] = dr["ACCOUNT_NAME"];
                    dr2["DEBIT"] = dr["DEBIT"];
                    dr2["Credit"] = dr["Credit"];
                    dr2["REMARKS"] = dr["REMARKS"];
                    dr2["Principal"] = "0";
                    dr2["Principal_id"] = "0";
                    dtVoucher2.Rows.Add(dr2);
                }
            }

            this.Session.Add("dtVoucher", dtVoucher2);
            GrdOrder.DataSource = dtVoucher2;
            GrdOrder.DataBind();
        }
        else
        {
            dtVoucher.Rows.Clear();  
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();

        }
        lblRowId.Text = "0";
        this.Session.Add("dtVoucher", dtVoucher);       
    }

    private void LoadBanks()
    {
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(FSCommonLayer.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4, true);
    }

    private bool IsBankIDExists(string p_AccountHeadID)
    {
        bool flag = false;
        foreach (ListItem li in ddlBank.Items)
        {
            if (li.Value == p_AccountHeadID)
            {
                flag = true;
            }
        }

        return flag;
    }
    
    #region Role Rights

    private bool CanEdit(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
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

    private void ApplyRoles()
    {
        if (CanEdit(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
            btnAddNew.Visible = false;

        }
    }
    #endregion
}