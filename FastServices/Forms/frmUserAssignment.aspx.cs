using System;
using System.Data;
using System.Web.UI;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From To Assign/UnAssign Location, Principals And VoucherTypes To Users
/// </summary>
public partial class Forms_frmUserAssignment : System.Web.UI.Page
{
    /// <summary>
    /// Page_Load Function Populates All Combos And ListBox On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ApplyRoles();
            this.LoadUser();
            this.LoadUnAssingned();
            this.LoadAssingned();
            this.AssignBrand();
            this.UnAssignBrand();
            this.AssignCustomer();
            this.UnAssignCustomer();
         }
    }

    /// <summary>
    /// Loads Users To User Combo
    /// </summary>
    private void LoadUser()
    {
        UserController mUserController = new UserController();
        DataTable dt = mUserController.SelectSlashUser(null, null);
        clsWebFormUtil.FillDropDownList(ddUser, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads Data Tab Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void ddUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tbUserAssignment.ActiveTabIndex == 0)
        {
            this.LoadUnAssingned();
            this.LoadAssingned();
        }
        else if (tbUserAssignment.ActiveTabIndex == 1)
        {
            this.UnAssignBrand();
            this.AssignBrand();
        }
        else if (tbUserAssignment.ActiveTabIndex == 2)
        {
            this.UnAssignCustomer();
            this.AssignCustomer();
        }
    }

    /// <summary>
    /// Loads Data Tab Controls Through ddUser_SelectedIndexChanged() Function
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void tbUserAssignment_ActiveTabChanged(object sender, EventArgs e)
    {
        ddUser_SelectedIndexChanged(null, null);
    }

    #region Location Tab
    
    /// <summary>
    /// Loads UnAssigned Locations To UnAssigned ListBox on Location Tab
    /// </summary>
    private void LoadUnAssingned()
    {
        if (ddUser.Items.Count > 0 )
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(ddUser.SelectedValue.ToString()), 2, 0, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstUnAssignDistributor, dt, 0, 1, true);
        }
    }

    /// <summary>
    /// Loads Assigned Locations To Assigned ListBox on Location Tab
    /// </summary>
    private void LoadAssingned()
    {
        if (ddUser.Items.Count > 0)
        {
            UserController mUserController = new UserController();
            DataTable dt = mUserController.SelectUserAssignment(int.Parse(ddUser.SelectedValue.ToString()), 2, 1, int.Parse(this.Session["CompanyId"].ToString()));
            clsWebFormUtil.FillListBox(lstAssignDistributor, dt, 0, 1, true);
        }
    }
    
    /// <summary>
    /// Inserts Location Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignLocation_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstUnAssignDistributor.Items.Count; i++)
        {
            if (lstUnAssignDistributor.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                mUserController.InsertUserAssignment(int.Parse(ddUser.SelectedValue.ToString()), 2, int.Parse(lstUnAssignDistributor.Items[i].Value.ToString()), int.Parse(this.Session["CompanyId"].ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Location " + lstUnAssignDistributor.Items[i].Text + " assigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    /// <summary>
    /// Deletes Location Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignLocation_Click(object sender, EventArgs e)
    {
        UserController mUserController = new UserController();
        for (int i = 0; i < lstAssignDistributor.Items.Count; i++)
        {
            if (lstAssignDistributor.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                mUserController.DeleteUserAssignment(int.Parse(ddUser.SelectedValue.ToString()), 2, int.Parse(lstAssignDistributor.SelectedValue.ToString()), int.Parse(this.Session["CompanyId"].ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Location " + lstAssignDistributor.Items[i].Text + " unassigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.LoadUnAssingned();
        this.LoadAssingned();
    }

    #endregion

    #region Supplier Tab

    /// <summary>
    /// Loads UnAssigned Principals To User To UnAssigned Principal ListBox On Principal Combo
    /// </summary>
    private void UnAssignBrand()
    {

        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectBrandAssignment(0, int.Parse(ddUser.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstUnAssignBrand, dt, 0, 1, true);
    }

    /// <summary>
    /// Loads nAssigned Principals To User To nAssigned Principal ListBox On Principal Combo
    /// </summary>
    private void AssignBrand()
    {

        SkuHierarchyController sController = new SkuHierarchyController();
        DataTable dt = sController.SelectBrandAssignment(1, int.Parse(ddUser.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstAssignBrand, dt, 0, 1, true);
    }

    /// <summary>
    /// Inserts One Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstUnAssignBrand.Items.Count; i++)
        {
            if (lstUnAssignBrand.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.InsertAssignBrand(int.Parse(lstUnAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Supplier " + lstUnAssignBrand.Items[i].Text + " assigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Inserts All Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignAllPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstUnAssignBrand.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.InsertAssignBrand(int.Parse(lstUnAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Supplier " + lstUnAssignBrand.Items[i].Text + " assigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Deletes One Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        {
            if (lstAssignBrand.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.DeleteAssignBrand(int.Parse(lstAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Supplier " + lstAssignBrand.Items[i].Text + " unassigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }

    /// <summary>
    /// Deletes All Principal Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignAllPrincipal_Click(object sender, EventArgs e)
    {
        SkuHierarchyController sController = new SkuHierarchyController();
        for (int i = 0; i < lstAssignBrand.Items.Count; i++)
        {
            if (lstAssignBrand.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.DeleteAssignBrand(int.Parse(lstAssignBrand.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Supplier " + lstAssignBrand.Items[i].Text + " unassigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignBrand();
        this.AssignBrand();
    }
    
    #endregion    

    #region Customer Tab

    /// <summary>
    /// Loads UnAssigned Customer To User To UnAssigned Customer ListBox On Customer Combo
    /// </summary>
    private void UnAssignCustomer()
    {

        OrganizationController sController = new OrganizationController();
        DataTable dt = sController.GetAssignedCustomer(0, int.Parse(ddUser.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstUnAssignCustomer, dt, 0, 1, true);

    }

    /// <summary>
    /// Loads Assigned Customers To User To Assigned Customer ListBox On Customer Combo
    /// </summary>
    private void AssignCustomer()
    {

        OrganizationController sController = new OrganizationController();
        DataTable dt = sController.GetAssignedCustomer(1, int.Parse(ddUser.SelectedValue.ToString()));
        clsWebFormUtil.FillListBox(lstAssignCustomer, dt, 0, 1, true);
    }

    /// <summary>
    /// Inserts One Customer Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignCustomer_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstUnAssignCustomer.Items.Count; i++)
        {
            if (lstUnAssignCustomer.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.InsertAssignCustomer(int.Parse(lstUnAssignCustomer.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Customer " + lstUnAssignCustomer.Items[i].Text + " assigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignCustomer();
        this.AssignCustomer();
    }

    /// <summary>
    /// Inserts All Customer Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnAssignCustomerAll_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstUnAssignCustomer.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.InsertAssignCustomer(int.Parse(lstUnAssignCustomer.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Customer " + lstUnAssignCustomer.Items[i].Text + " assigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignCustomer();
        this.AssignCustomer();
    }

    /// <summary>
    /// Deletes All Customer Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignCustomerAll_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignCustomer.Items.Count; i++)
        {
            if (ddUser.Items.Count > 0)
            {
                sController.DeleteAssignCustomer(int.Parse(lstAssignCustomer.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Customer " + lstAssignCustomer.Items[i].Text + " unassigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignCustomer();
        this.AssignCustomer();
    }

    /// <summary>
    /// Deletes One Customer Assignment To User
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnUnAssignCustomer_Click(object sender, EventArgs e)
    {
        OrganizationController sController = new OrganizationController();
        for (int i = 0; i < lstAssignCustomer.Items.Count; i++)
        {
            if (lstAssignCustomer.Items[i].Selected == true && ddUser.Items.Count > 0)
            {
                sController.DeleteAssignCustomer(int.Parse(lstAssignCustomer.Items[i].Value.ToString()), int.Parse(ddUser.SelectedValue.ToString()));

                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "User Assignment", "Customer " + lstAssignCustomer.Items[i].Text + " unassigned to " + ddUser.SelectedItem.Text);
            }
        }
        this.UnAssignCustomer();
        this.AssignCustomer();
    }

    #endregion

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
            btnAssignLocation.Visible = false;
            btnUnAssignLocation.Visible = false;
            btnAssignPrincipal.Visible = false;
            btnUnAssignPrincipal.Visible = false;
            btnAssignAllPrincipal.Visible = false;
            btnUnAssignAllPrincipal.Visible = false;
            btnAssignCustomer.Visible = false;
            btnUnAssignCustomer.Visible = false;
            btnAssignCustomerAll.Visible = false;
            btnUnAssignCustomerAll.Visible = false;
        }
    }
    #endregion
}
