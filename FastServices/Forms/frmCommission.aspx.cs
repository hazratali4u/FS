using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form To Add, Edit Branch Expense And Salary
/// </summary>
public partial class frmCommission : System.Web.UI.Page
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
            this.LoadMonth();
            ApplyRoles();
            this.LoadDistributor();
            this.LoadAccountHead();            
            this.LoadEmployee();
            this.LoadProductCategory();
            this.LoadGrid();
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
        DataTable dt = mAccountController.GetSalaryPaymentAccountHead(1);
        clsWebFormUtil.FillDropDownList(DrpAccountHead, dt, 0, 4, true);
    }
             
    /// <summary>
    /// Save Or Updates Expenses And Salary
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LedgerController mLController = new LedgerController();
        DataTable dtDetail = new DataTable();
        dtDetail.Columns.Add("CUSTOMER_ID", typeof(long));
        dtDetail.Columns.Add("AMOUNT", typeof(decimal));
        dtDetail.Columns.Add("INVOICE_ID", typeof(long));
        dtDetail.Columns.Add("COMMISSION_TYPE", typeof(int));
        dtDetail.Columns.Add("COMMISSION_VALUE", typeof(decimal));
        dtDetail.Columns.Add("TAX_VALUE", typeof(decimal));

        decimal TotalCommission = 0;

        foreach (GridViewRow gvr in gvPayment.Rows)
        {
            CheckBox cbCommission = (CheckBox)gvr.FindControl("cbCommission");
            if (cbCommission.Checked)
            {
                TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
                TextBox txtCommission = (TextBox)gvr.FindControl("txtCommission");
                TextBox txtNetCommission = (TextBox)gvr.FindControl("txtNetCommission");
                
                DataRow dr = dtDetail.NewRow();
                dr["CUSTOMER_ID"] = gvr.Cells[1].Text;
                dr["AMOUNT"] = txtAmount.Text;
                dr["INVOICE_ID"] = gvr.Cells[0].Text;
                if (txtCommission.Text.Contains("%"))
                {
                    dr["COMMISSION_TYPE"] = "0";
                    dr["COMMISSION_VALUE"] = Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtCommission.Text.Replace("%","").Trim()) / 100;
                }
                else
                {
                    dr["COMMISSION_TYPE"] = "1";
                    dr["COMMISSION_VALUE"] = txtCommission.Text;
                }
                if (gvr.Cells[4].Text == "Cash")
                {
                    dr["TAX_VALUE"] = 0;
                }
                else
                {
                    dr["TAX_VALUE"] = txtTax.Text.Replace("%", "").Trim();
                }
                dtDetail.Rows.Add(dr);
                TotalCommission += Convert.ToDecimal(txtNetCommission.Text);
            }
        }

        if (dtDetail.Rows.Count > 0)
        {
            DateTime DocumentMonth = new DateTime();
            DocumentMonth = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            long CommissionID = Constants.IntNullValue;
             CommissionID = mLController.InsertCommission(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToDateTime(Session["CurrentWorkDate"]), DocumentMonth, Convert.ToInt32(DrpAccountHead.SelectedValue), Convert.ToInt64(ddlEmployee.SelectedValue), Convert.ToDecimal(txtTax.Text.Replace("%","").Trim()), Convert.ToInt32(Session["UserID"]), dtDetail);
            if(CommissionID != Constants.LongNullValue)
            {
                InsertGL(TotalCommission,CommissionID);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Commission saved successfully.')", true); 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.')", true); 
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('No data found')", true); 
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

    private void LoadGrid()
    {
        DataControl dc = new DataControl();
        decimal amount = 0, comm = 0, netcomm = 0;
        DateTime dtFrom = Convert.ToDateTime(rblMonth.SelectedValue);
        DateTime dtTo = Convert.ToDateTime(rblMonth.SelectedValue);

        dtFrom = new DateTime(dtFrom.Year, dtFrom.Month, 1);

        dtTo = new DateTime(dtTo.Year, dtTo.Month, 1);
        dtTo = dtTo.AddMonths(1).AddDays(-1);

        LedgerController LController = new LedgerController();
        gvPayment.DataSource = LController.GetPaymentDataForCommission(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), null, dtFrom, dtTo);
        gvPayment.DataBind();

        DataTable dtCommission = GetCommissionData();
        if (dtCommission.Rows.Count > 0)
        {
            drpDistributor.SelectedValue = dtCommission.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpAccountHead.SelectedValue = dtCommission.Rows[0]["ACCOUNT_HEAD_ID"].ToString();
            ddlEmployee.SelectedValue = dtCommission.Rows[0]["PEOPLE_ID"].ToString();
            txtTax.Text = dtCommission.Rows[0]["TAX"].ToString() + "%";

            DataTable dtCommissionDetail = LController.GetCommissionDetilData(Convert.ToInt64(dtCommission.Rows[0]["COMMISSION_MASTER_ID"]));

            foreach(DataRow dr in dtCommissionDetail.Rows)
            {
                foreach(GridViewRow gvr in gvPayment.Rows)
                {
                    if (gvr.Cells[0].Text ==  dr["INVOICE_ID"].ToString() && gvr.Cells[1].Text == dr["CUSTOMER_ID"].ToString())
                    {
                        CheckBox cbCommission = (CheckBox)gvr.FindControl("cbCommission");
                        TextBox txtCommission = (TextBox)gvr.FindControl("txtCommission");
                        TextBox txtNetCommission = (TextBox)gvr.FindControl("txtNetCommission");
                        cbCommission.Checked = true;

                        if (dr["COMMISSION_TYPE"].ToString() == "0")
                        {
                            txtCommission.Text = (Convert.ToDecimal(dr["COMMISSION_VALUE"]) / Convert.ToDecimal(dr["AMOUNT"]) * 100).ToString() + "%";

                            if (txtTax.Text.Contains("%"))
                            {
                                decimal TotalTax = Convert.ToDecimal(dr["COMMISSION_VALUE"]) * Convert.ToDecimal(txtTax.Text.Replace("%", "").Trim()) / 100;
                                if (gvr.Cells[4].Text == "Cash")
                                {
                                    TotalTax = 0;
                                }
                                txtNetCommission.Text = (Convert.ToDecimal(dr["COMMISSION_VALUE"]) - TotalTax).ToString();
                            }
                            else
                            {
                                if (gvr.Cells[4].Text == "Cash")
                                {
                                    txtNetCommission.Text = Convert.ToDecimal(dr["COMMISSION_VALUE"]).ToString();
                                }
                                else
                                {
                                    txtNetCommission.Text = (Convert.ToDecimal(dr["COMMISSION_VALUE"]) - Convert.ToDecimal(txtTax.Text)).ToString();
                                }
                            }
                        }
                        else
                        {
                            txtCommission.Text = Convert.ToDecimal(dr["COMMISSION_VALUE"]).ToString();
                            if (txtTax.Text.Contains("%"))
                            {
                                decimal TotalTax = Convert.ToDecimal(txtCommission.Text) * Convert.ToDecimal(txtTax.Text.Replace("%", "").Trim()) / 100;
                                if (gvr.Cells[4].Text == "Cash")
                                {
                                    TotalTax = 0;
                                }
                                txtNetCommission.Text = (Convert.ToDecimal(txtCommission.Text) - TotalTax).ToString();
                            }
                            else
                            {
                                if (gvr.Cells[4].Text == "Cash")
                                {
                                    txtNetCommission.Text = Convert.ToDecimal(txtCommission.Text).ToString();
                                }
                                else
                                {
                                    txtNetCommission.Text = (Convert.ToDecimal(txtCommission.Text) - Convert.ToDecimal(txtTax.Text)).ToString();
                                }
                            }
                        }                     
                    }
                }                
            }
        }

        foreach (GridViewRow gvr in gvPayment.Rows)
        {
            TextBox txtAmount = (TextBox)gvr.FindControl("txtAmount");
            TextBox txtCommission = (TextBox)gvr.FindControl("txtCommission");
            TextBox txtNetCommission = (TextBox)gvr.FindControl("txtNetCommission");
            amount += Convert.ToDecimal(dc.chkNull_0(txtAmount.Text));
            //comm += Convert.ToDecimal(dc.chkNull_0(txtCommission.Text));
            netcomm += Convert.ToDecimal(dc.chkNull_0(txtNetCommission.Text));
        }
        txtAmt.Text = amount.ToString();
        //txtComm.Text = comm.ToString();
        txtNetComm.Text = netcomm.ToString();
    }

    private void LoadMonth()
    {
         DateTime dtPreviousMonth = new DateTime(System.DateTime.Now.AddMonths(-1).Year, System.DateTime.Now.AddMonths(-1).Month, 1);
         DateTime dtCurrentMonth = System.DateTime.Now;
                
        rblMonth.Items.Add(new ListItem(dtPreviousMonth.ToString("MMM-yyyy"), dtPreviousMonth.ToString("dd-MMM-yyyy")));
        rblMonth.Items.Add(new ListItem(dtCurrentMonth.ToString("MMM-yyyy"), dtCurrentMonth.ToString("dd-MMM-yyyy")));
        rblMonth.Items[0].Selected = true;
    }

    private DataTable GetCommissionData()
    {
        LedgerController LController = new LedgerController();
        DateTime DocumentMonth = new DateTime();
        DocumentMonth = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
        DataTable dtCommission = LController.GetCommissionData(Convert.ToInt32(drpDistributor.SelectedValue), DocumentMonth, Convert.ToInt32(DrpAccountHead.SelectedValue), Convert.ToInt64(ddlEmployee.SelectedValue));
        return dtCommission;
    }

    protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCommission = e.Row.FindControl("txtCommission") as TextBox;
            TextBox txtNetCommission = e.Row.FindControl("txtNetCommission") as TextBox;
            TextBox txtAmount = e.Row.FindControl("txtAmount") as TextBox;
            CheckBox cbCommission = e.Row.FindControl("cbCommission") as CheckBox;

            txtCommission.Attributes.Add("onchange", "javascript:return CalNetCommission(" + cbCommission.ClientID + ",'" + e.Row.Cells[4].Text + "', '" + txtAmount.ClientID + "', '" + txtCommission.ClientID + "', '" + txtNetCommission.ClientID + "');");
            txtNetCommission.Attributes.Add("onfocus", "javascript:return CalNetCommission('" + cbCommission.ClientID + "'," + e.Row.Cells[4].Text + "', '" + txtAmount.ClientID + "', '" + txtCommission.ClientID + "', '" + txtNetCommission.ClientID + "');");
            cbCommission.Attributes.Add("onchange", "javascript:return CalNetCommission(" + cbCommission.ClientID + ",'" + e.Row.Cells[4].Text + "', '" + txtAmount.ClientID + "', '" + txtCommission.ClientID + "', '" + txtNetCommission.ClientID + "');");

            txtCommission.Attributes.Add("onkeydown ", "javascript:return (event.keyCode!=13);");
            txtNetCommission.Attributes.Add("onkeydown ", "javascript:return (event.keyCode!=13);");
        }
    }

    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    
    protected void DrpAccountHead_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }
    
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadGrid();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    private void InsertGL(decimal Amount, long PaymentID)
    {
        LedgerController mLController = new LedgerController();
        FSCommonLayer.Classes.Configuration.GetAccountHead();
        string MaxDocumentId;

        DataTable dtVoucher = new DataTable();
        dtVoucher.Columns.Add("Account_Head_Id", typeof(long));
        dtVoucher.Columns.Add("Debit", typeof(decimal));
        dtVoucher.Columns.Add("Credit", typeof(decimal));
        dtVoucher.Columns.Add("Remarks", typeof(string));
        dtVoucher.Columns.Add("Principal_Id", typeof(string));
        dtVoucher.Columns.Add("PEOPLE_ID", typeof(long));

        DataRow dr = dtVoucher.NewRow();
        dr["ACCOUNT_HEAD_ID"] = DrpAccountHead.SelectedValue.ToString();
        dr["DEBIT"] = Amount;
        dr["Credit"] = "0";
        dr["REMARKS"] = ddlEmployee.SelectedItem.Text;
        dr["Principal_id"] = "0";
        dr["PEOPLE_ID"] = "0";
        dtVoucher.Rows.Add(dr);

        DataRow dr2 = dtVoucher.NewRow();
        dr2["ACCOUNT_HEAD_ID"] = FSCommonLayer.Classes.Configuration.PeatyCash;
        dr2["DEBIT"] = 0;
        dr2["Credit"] = Amount;
        dr2["REMARKS"] = "Commission for " + ddlEmployee.SelectedItem.Text;
        dr2["Principal_id"] = "0";
        dr2["PEOPLE_ID"] = "0";
        dtVoucher.Rows.Add(dr2);

        MaxDocumentId = mLController.SelectMaxVoucherId(Constants.Journal_Voucher, int.Parse(drpDistributor.SelectedValue.ToString()), ((DateTime)this.Session["CurrentWorkDate"]));

        mLController.Add_Voucher(int.Parse(drpDistributor.SelectedValue.ToString()), 0, MaxDocumentId, Constants.Journal_Voucher,
               ((DateTime)this.Session["CurrentWorkDate"]), Constants.Document_Petty_Cash, null, "Petty Voucher", Constants.DateNullValue, null, dtVoucher, int.Parse(this.Session["UserId"].ToString()), PaymentID.ToString(),Constants.DateNullValue,PaymentID);

    }

    protected void rblMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
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
        }
    }

    #endregion            
}
