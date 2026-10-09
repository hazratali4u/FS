using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// Form To Post Vouchers
/// </summary>
public partial class Forms_frmApproveSample : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    QuotationController QtnCtl = new QuotationController();
    PurchaseController mController = new PurchaseController();
    EnquiryController EnqCtl = new EnquiryController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grids On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ApplyRoles();
            this.LoadDistributor();
            this.LoadCustomer();
            LoadApproveProduct();
        }
    }

    /// <summary>
    /// Loads Locations To Location Comob
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2);
    }

    /// <summary>
    /// Loads Principals To Principal Combo
    /// </summary>
    private void LoadCustomer()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
        clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Vouchers To Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (GrdQuotation.Rows.Count > 0)
        {
            bool flag = false;
            foreach (GridViewRow gvr in GrdQuotation.Rows)
            {
                int IsApprove = 0;
                CheckBox cbApproved = gvr.FindControl("cbApproved") as CheckBox;
                if (cbApproved.Checked)
                {
                    IsApprove = 1;
                }
                flag = EnqCtl.InsertCustomerProductApproval(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue), Convert.ToInt32(gvr.Cells[0].Text), Convert.ToInt32(gvr.Cells[1].Text), Convert.ToInt32(Session["UserID"]), IsApprove);
            }
            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Selected Product Approved.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Some error occured.');", true);
            }
        }
    }
    
    protected void DrpCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadApproveProduct();
    }

    private void LoadApproveProduct()
    {
        DataTable dtQuotation = QtnCtl.GetApprovalProductsNew(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomer.SelectedValue),Constants.LongNullValue);
        GrdQuotation.DataSource = dtQuotation;
        GrdQuotation.DataBind();

        hfGrdQuotationRows.Value = GrdQuotation.Rows.Count.ToString();
    }
    
    protected void GrdQuotation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow 
            && (e.Row.RowState == DataControlRowState.Normal 
            || e.Row.RowState == DataControlRowState.Alternate))
        {
            //Approve All
            CheckBox cbApproved = (CheckBox)e.Row.Cells[2].FindControl("cbApproved");
            CheckBox cbApproveAll = (CheckBox)this.GrdQuotation.HeaderRow.FindControl("cbApproveAll");
            cbApproved.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", cbApproveAll.ClientID);
        }
    }
    
    protected void drpDistributor_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadApproveProduct();
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
            btnGenerate.Visible = false;
        }
    }
    #endregion
}
