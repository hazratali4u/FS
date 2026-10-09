using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using FSCommonLayer.Classes;
using FSBusinessLayer.Classes;
using FSBusinessLayer.Reports;

/// <summary>
/// From to Assign, UnAssign Forms To Users
/// </summary>
public partial class frmProductAssignment : System.Web.UI.Page
{
    RoleManagementController mController = new RoleManagementController();
    DataControl dc = new DataControl();

    /// <summary>
    /// Page_Load Function Populates All Combos and ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            ApplyRoles();
            this.LoadData();
            this.UnAssignSupplier();
            this.AssignSupplier();
        }
    }

    /// <summary>
    /// Loads SKU Data To Session
    /// </summary>
    private void LoadData()
    {
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlProduct, m_SKUDt, 0, 2);
    }
        
    /// <summary>
    /// Assigns Product To Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstUnAssignModule.Items.Count; i++)
        {
            if (lstUnAssignModule.Items[i].Selected == true && ddlProduct.Items.Count > 0)
            {
                sController.InsertProductAssign(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstUnAssignModule.Items[i].Value.ToString()));
            }
        }
        this.UnAssignSupplier();
        this.AssignSupplier();
    }
    
    /// <summary>
    /// UnAssigns Product To Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignModule.Items.Count; i++)
        {
            if (lstAssignModule.Items[i].Selected == true && ddlProduct.Items.Count > 0)
            {
                sController.DeleteProductAssignment(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstAssignModule.Items[i].Value.ToString()));

            }
        }
        this.UnAssignSupplier();
        this.AssignSupplier();
    }

    /// <summary>
    /// Loads Assign UnAssign Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.UnAssignSupplier();
        this.AssignSupplier();
    }

    /// <summary>
    /// Assigns Product To All Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstUnAssignModule.Items.Count; i++)
        {
            if (ddlProduct.Items.Count > 0)
            {
                sController.InsertProductAssign(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstUnAssignModule.Items[i].Value.ToString()));

            }
        }
        this.UnAssignSupplier();
        this.AssignSupplier();
    }

    /// <summary>
    /// UnAssigns Product To All Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignModule.Items.Count; i++)
        {
            if (ddlProduct.Items.Count > 0)
            {
                sController.DeleteProductAssignment(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstAssignModule.Items[i].Value.ToString()));
            }
        }
        this.UnAssignSupplier();
        this.AssignSupplier();
    }

    /// <summary>
    /// Loads UnAssigned Supplier To User To UnAssigned Supplier ListBox On Customer Combo
    /// </summary>
    private void UnAssignSupplier()
    {

        OrganizationController sController = new OrganizationController();
        DataTable dt = sController.GetAssignedSupplier(0, int.Parse(ddlProduct.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstUnAssignModule, dt, 0, 1, true);

    }

    /// <summary>
    /// Loads Assigned Supplier To User To Assigned Supplier ListBox
    /// </summary>
    private void AssignSupplier()
    {
        OrganizationController sController = new OrganizationController();
        DataTable dt = sController.GetAssignedSupplier(1, int.Parse(ddlProduct.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstAssignModule, dt, 0, 1, true);
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
            btnAssign.Visible = false;
            btnAssignAll.Visible = false;
            btnUnAssign.Visible = false;
            btnUnAssignAll.Visible = false;
        }
    }
    #endregion
}
