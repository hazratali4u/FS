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
            ApplyRoles();
            this.LoadDistributor();
            this.LoadAccountHead();
            this.CreatTable();
            this.LoadVoucherNo();
            this.LoadEmployee();
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
        DataTable dt = mAccountController.GetSalaryPaymentAccountHead(0);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
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
        dtVoucher.Columns.Add("PEOPLE_ID", typeof(long));
        this.Session.Add("dtVoucher", dtVoucher);

    }

    /// <summary>
    /// Loads Voucher Nos To Voucher No Combo
    /// </summary>
    private void LoadVoucherNo()
    {
        LedgerController LController = new LedgerController();
        DataTable  dt  = LController.SelectVoucherNo(((DateTime)this.Session["CurrentWorkDate"]), int.Parse(drpDistributor.SelectedValue.ToString()), Constants.Journal_Voucher, int.Parse(this.Session["UserId"].ToString()),null);
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
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();

        if (GrdOrder.Rows.Count > 0)
        {
            drpDistributor.Enabled = false;
            DrpAccountHead.Enabled = false;
            ddlType.Enabled = false;
            ddlBank.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            DrpAccountHead.Enabled = true;
            ddlType.Enabled = true;
            ddlBank.Enabled = true;
        }
  
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
        ddlEmployee.SelectedItem.Text = GrdOrder.Rows[e.NewEditIndex].Cells[2].Text;
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
            dr["REMARKS"] = ddlEmployee.SelectedItem.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = 61;//Branch Expenses  (FDM EXP)
            dr["PEOPLE_ID"] = ddlEmployee.SelectedValue;
            dtVoucher.Rows.Add(dr);

        }
        else
        {
            DataRow dr = dtVoucher.Rows[int.Parse(lblRowId.Text)];
            dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue.ToString();
            dr["ACCOUNT_NAME"] = DrpAccountHead.SelectedItem.Text;
            dr["DEBIT"] = decimal.Parse(dc.chkNull_0(txtAmount.Text));
            dr["Credit"] = "0";
            dr["REMARKS"] = ddlEmployee.SelectedItem.Text;
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["ACCOUNT_PARENT_ID"] = 61; //Branch Expenses  (FDM EXP)
            dr["PEOPLE_ID"] = ddlEmployee.SelectedValue;
        }
        
        GrdOrder.DataSource = dtVoucher;
        GrdOrder.DataBind();
        txtAmount.Text = "";        
        lblRowId.Text = "-1";
        ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
        
        decimal TotalDebit = 0;

        foreach (DataRow dr in dtVoucher.Rows)
        {
            TotalDebit += decimal.Parse(dr["DEBIT"].ToString());
        }
        txtMainCash.Text = TotalDebit.ToString();

        if (GrdOrder.Rows.Count > 0)
        {
            drpDistributor.Enabled = false;
            DrpAccountHead.Enabled = false;
            ddlType.Enabled = false;
            ddlBank.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            DrpAccountHead.Enabled = true;
            ddlType.Enabled = true;
            ddlBank.Enabled = true;
        }
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
            DataRow dr = dtVoucher.NewRow();
            dr["ACCOUNT_HEAD_ID"] = FSCommonLayer.Classes.Configuration.PeatyCash;
            dr["ACCOUNT_NAME"] = "Peatty Cash";
            dr["DEBIT"] = 0;
            dr["Credit"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
            dr["REMARKS"] = "Salary for " + ddlEmployee.SelectedItem.Text + " for month " + Convert.ToDateTime(this.Session["CurrentWorkDate"]).ToString("MMM, yyyy");
            dr["Principal"] = "0";
            dr["Principal_id"] = "0";
            dr["PEOPLE_ID"] = "0";
            dtVoucher.Rows.Add(dr);


            int AccountHeadID = Convert.ToInt32(ddlBank.SelectedValue);
            if (ddlType.SelectedValue == "1")
            {                
                AccountHeadID = 46;
            }
            
            // Credit Bank Account
            DataRow drBank = dtVoucher.NewRow();            
            drBank["ACCOUNT_HEAD_ID"] = AccountHeadID;
            drBank["ACCOUNT_NAME"] = "Bank";
            drBank["REMARKS"] = "Salary paid for month " + Convert.ToDateTime(this.Session["CurrentWorkDate"]).ToString("MMM, yyyy");
            drBank["DEBIT"] = 0;
            drBank["CREDIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
            drBank["Principal_Id"] = 0;
            drBank["PEOPLE_ID"] = 0;
            dtVoucher.Rows.Add(drBank);

            DataRow drBank2 = dtVoucher.NewRow();
            drBank2["ACCOUNT_NAME"] = "Bank";
            drBank2["ACCOUNT_HEAD_ID"] = 107;
            drBank2["REMARKS"] = "Salary paid for month " + Convert.ToDateTime(this.Session["CurrentWorkDate"]).ToString("MMM, yyyy");
            drBank2["DEBIT"] = decimal.Parse(dc.chkNull_0(txtMainCash.Text));
            drBank2["CREDIT"] = 0;
            drBank2["Principal_Id"] = 0;
            drBank2["PEOPLE_ID"] = 0;
            dtVoucher.Rows.Add(drBank2);

            if (DrpVoucherNo.SelectedValue.ToString() == "0")
            {
                MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()), ((DateTime)this.Session["CurrentWorkDate"]));

                IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
                       ((DateTime)this.Session["CurrentWorkDate"]), Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text,Constants.DateNullValue,Convert.ToInt64(ddlEmployee.SelectedValue));
            }
            else
            {
                MaxDocumentId = DrpVoucherNo.SelectedValue.ToString();

                IsSaveVoucher = mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
                       ((DateTime)this.Session["CurrentWorkDate"]), Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), txtslipNo.Text,Constants.DateNullValue,Convert.ToInt64(ddlEmployee.SelectedValue));
            }

            if (IsSaveVoucher == true)
            {
                if (mLController.InsertSalaryDetail(MaxDocumentId, Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlBank.SelectedValue)))
                {
                    dtVoucher.Rows.Clear();
                    GrdOrder.DataSource = null;
                    GrdOrder.DataBind();
                    txtAmount.Text = "";
                    lblRowId.Text = "-1";
                    ScriptManager.GetCurrent(Page).SetFocus(DrpAccountHead);
                    decimal TotalDebit = 0;
                    foreach (DataRow dr1 in dtVoucher.Rows)
                    {
                        TotalDebit += decimal.Parse(dr1["DEBIT"].ToString());
                    }
                    txtMainCash.Text = TotalDebit.ToString();
                    if (GrdOrder.Rows.Count > 0)
                    {
                        drpDistributor.Enabled = false;
                        DrpAccountHead.Enabled = false;
                        ddlType.Enabled = false;
                        ddlBank.Enabled = false;
                    }
                    else
                    {
                        drpDistributor.Enabled = true;
                        DrpAccountHead.Enabled = true;
                        ddlType.Enabled = true;
                        ddlBank.Enabled = true;
                    }
                    this.LoadVoucherNo();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Salaries saved successfully.')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some Error in Voucher')", true);
                    return;
                }
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
            dtVoucher = LController.SelectVoucherDetail2(int.Parse(drpDistributor.SelectedValue.ToString()), DrpVoucherNo.SelectedValue.ToString(), Constants.Journal_Voucher);
            for (int i = 0; i < dtVoucher.Rows.Count; i++)
            {
                if (Decimal.Parse(dtVoucher.Rows[i]["CREDIT"].ToString()) > 0)
                {
                    dtVoucher.Rows.RemoveAt(i);
                }
            }
            if (dtVoucher.Rows.Count > 0)
            {
                DrpAccountHead.SelectedValue = dtVoucher.Rows[0]["ACCOUNT_HEAD_ID"].ToString();
                txtslipNo.Text = dtVoucher.Rows[0]["Slip_No"].ToString();
            }
            this.Session.Add("dtVoucher", dtVoucher);
            GrdOrder.DataSource = dtVoucher;
            GrdOrder.DataBind();

            DataTable dtSalary = LController.GetSalaryDetail(Constants.LongNullValue, DrpVoucherNo.SelectedValue, Constants.IntNullValue, Constants.IntNullValue);
            if (dtSalary.Rows.Count > 0)
            {
                ddlType.SelectedValue = dtSalary.Rows[0]["TYPE"].ToString();
                ddlBank.SelectedValue = dtSalary.Rows[0]["ACCOUNT_HEAD_ID"].ToString();
            }
        }
        else
        {
            dtVoucher.Rows.Clear();  
            GrdOrder.DataSource = null;
            GrdOrder.DataBind();

        }

        decimal TotalDebit = 0;

        foreach (GridViewRow dr in GrdOrder.Rows)
        {
            TotalDebit += decimal.Parse(dr.Cells[3].Text);
        }
        txtMainCash.Text = TotalDebit.ToString();

        lblRowId.Text = "0";
        this.Session.Add("dtVoucher", dtVoucher);

        if (GrdOrder.Rows.Count > 0)
        {
            drpDistributor.Enabled = false;
            DrpAccountHead.Enabled = false;
            ddlType.Enabled = false;
            ddlBank.Enabled = false;
        }
        else
        {
            drpDistributor.Enabled = true;
            DrpAccountHead.Enabled = true;
            ddlType.Enabled = true;
            ddlBank.Enabled = true;
        }
    }

    private void LoadEmployee()
    {
        PeopleController PplCtl = new PeopleController();
        DataTable dtPeople = new DataTable();

        if (Session["RoleID"].ToString() == "1")
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null,Constants.IntNullValue,0);
        }
        else
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, 1, null, null,Convert.ToInt32(Session["UserID"]),0);
        }

        clsWebFormUtil.FillDropDownList(ddlEmployee, dtPeople, "PEOPLE_ID", "FullName");
    }

    private void LoadBanks()
    {
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        AccountHeadController mAccountController = new AccountHeadController();
        DataTable dt = mAccountController.SelectAccountHead(Constants.AC_AccountHeadId, long.Parse(FSCommonLayer.Classes.Configuration.BankDefaultType));
        clsWebFormUtil.FillDropDownList(ddlBank, dt, 0, 4, true);        
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "0")
        {
            ddlBank.SelectedIndex = 0;
        }
        else
        {
            ddlBank.SelectedIndex = 2;
        }
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