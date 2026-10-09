using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections;
using System.Configuration;

public partial class Forms_frmProductDetail : System.Web.UI.Page
{
    RoleManagementController RoleCtl = new RoleManagementController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtTabRights = RoleCtl.GetRoleFormTab(Convert.ToInt32(Request.QueryString["LevelID"]), Convert.ToInt32(Session["RoleID"]));
            this.Session.Add("dtTabRights", dtTabRights);

            ApplyRoles();

            this.LoadProductCategory();

            LoadData(Convert.ToInt32(Session["ProductID"]));
            this.UnAssignSupplier();
            this.AssignSupplier();
        }
    }

    private void LoadData(int p_ProductID)
    {
        SkuController mSKUController = new SkuController();
        DataTable m_SKUDt = mSKUController.SelectSkuData(p_ProductID, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(ddlProduct, m_SKUDt, "SKU_ID", "SKU_NAME");
        if (m_SKUDt.Rows.Count > 0)
        {
            lblCategory.Text = m_SKUDt.Rows[0]["Category"].ToString();
            lblPrductName.Text = m_SKUDt.Rows[0]["SKU_NAME"].ToString();
            lblProductCode.Text = m_SKUDt.Rows[0]["SKU_Code"].ToString();
            ddskucategory.SelectedValue = m_SKUDt.Rows[0]["CATEGORY_ID"].ToString();
            txtskuname.Text = m_SKUDt.Rows[0]["SKU_NAME"].ToString();
            txtCode.Text = m_SKUDt.Rows[0]["SKU_Code"].ToString();
            ChIsActive.Checked = bool.Parse(m_SKUDt.Rows[0]["ISACTIVE"].ToString());
            ddlProduct.SelectedValue = m_SKUDt.Rows[0]["SKU_ID"].ToString();
            ddlProduct.Enabled = false;
        }
    }

    /// <summary>
    /// Save Or Updates an SKU
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool IsExemted = true;
        SkuController mController = new SkuController();
        ltrlEdit.Text = "";
        hfTabIndex.Value = hfEditIndex.Value;
        if (txtskuname.Text.Length <= 0)
        {
            ltrlEdit.Text = Utility.ShowAlert(false,"Must Enter Product Name.");
            return;
        }
        if (btnSave.Text == "Save")
        {
            string msg = mController.UpdateSKUS(IsExemted, ChIsActive.Checked, 'E', 1, 1, int.Parse(ddskucategory.SelectedValue.ToString()), 1, 1, 0, 0, 1, Convert.ToInt32(Session["ProductID"]), txtCode.Text, txtskuname.Text, null, "", int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));

            if (msg == "Record Updated")
            {
                ltrlEdit.Text = Utility.ShowAlert(true,"Product updated successfully.");
                
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", txtskuname.Text + " updated");

                LoadData(Convert.ToInt32(Session["ProductID"]));
            }
            else
            {
                ltrlEdit.Text = Utility.ShowAlert(false,"Some error occured. Product not updated.");
            }
        }
    }

    /// <summary>
    /// Loads Product Categories To Category Combo
    /// </summary>
    private void LoadProductCategory()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        ddskucategory.DataSource = m_dt;
        clsWebFormUtil.FillDropDownList(ddskucategory, m_dt, "REF_ID", "SLASH_DESC");
    }

    #region Product Assignment

    /// <summary>
    /// Assigns Product To Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfSupplierIndex.Value;
        OrganizationController sController = new OrganizationController();
        int count = 0;
        for (int i = 0; i < lstUnAssignModule.Items.Count; i++)
        {
            if (lstUnAssignModule.Items[i].Selected == true && ddlProduct.Items.Count > 0)
            {
                sController.InsertProductAssign(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstUnAssignModule.Items[i].Value.ToString()));
                count += 1;
            }
        }

        if (count > 0)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", ddlProduct.SelectedItem.Text + " Assigned to supplier");

            this.UnAssignSupplier();
            this.AssignSupplier();
        }
    }

    /// <summary>
    /// UnAssigns Product To Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssign_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfSupplierIndex.Value;
        int count = 0;
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignModule.Items.Count; i++)
        {
            if (lstAssignModule.Items[i].Selected == true && ddlProduct.Items.Count > 0)
            {
                sController.DeleteProductAssignment(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstAssignModule.Items[i].Value.ToString()));
                count += 1;
            }
        }

        if (count > 0)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", ddlProduct.SelectedItem.Text + " Unassigned to supplier");

            this.UnAssignSupplier();
            this.AssignSupplier();
        }
    }

    /// <summary>
    /// Assigns Product To All Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAll_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfSupplierIndex.Value;
        int count = 0;
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstUnAssignModule.Items.Count; i++)
        {
            if (ddlProduct.Items.Count > 0)
            {
                sController.InsertProductAssign(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstUnAssignModule.Items[i].Value.ToString()));
                count += 1;
            }
        }

        if (count > 0)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", ddlProduct.SelectedItem.Text + " Assigned to supplier");

            this.UnAssignSupplier();
            this.AssignSupplier();
        }
    }

    /// <summary>
    /// UnAssigns Product To All Suppliers
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAll_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfSupplierIndex.Value;
        int count = 0;
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignModule.Items.Count; i++)
        {
            if (ddlProduct.Items.Count > 0)
            {
                sController.DeleteProductAssignment(int.Parse(ddlProduct.SelectedValue.ToString()), int.Parse(lstAssignModule.Items[i].Value.ToString()));
                count += 1;
            }
        }

        if (count > 0)
        {
            //Save Transaction in log
            Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Product", ddlProduct.SelectedItem.Text + " Unassigned to supplier");

            this.UnAssignSupplier();
            this.AssignSupplier();
        }
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

    #endregion

    #region Role Rights

    private void ApplyRoles()
    {
        int TabIndexes = 0;
        ltrlTabs.Text = "<ul class='TabbedPanelsTabGroup'>";
        if (!CanView("0"))
        {
            ActiveTabIndex.Value = "0";
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0'>View Product </li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0' style='display:none'>View Product </li>";
        }
        if (!CanView("1"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "1";
            }
            hfEditIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1'>Edit Product</li>";
            //if (!CanEdit("1"))
            //{
            //    btnSave.Visible = false;
            //}
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1' style='display:none'>Edit Product</li>";
        }
        if (!CanView("2"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "2";
            }
            //if (!CanEdit("2"))
            //{
            //    btnAssign.Visible = false;
            //    btnAssignAll.Visible = false;
            //    btnUnAssign.Visible = false;
            //    btnUnAssignAll.Visible = false;
            //}
            hfSupplierIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'>Assign Product to Supplier</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'  style='display:none'>Assign Product to Supplier</li>";
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

    #endregion
}