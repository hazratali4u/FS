using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmPeopleDetail : System.Web.UI.Page
{
    PeopleController PplCtl = new PeopleController();
    Distributor_UserController UController = new Distributor_UserController();
    RoleManagementController RoleCtl = new RoleManagementController();
    static int UserId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtTabRights = RoleCtl.GetRoleFormTab(Convert.ToInt32(Request.QueryString["LevelID"]), Convert.ToInt32(Session["RoleID"]));
            this.Session.Add("dtTabRights", dtTabRights);

            ApplyRoles();

            LoadOrganization();
            LoadDistributor();
            this.LoadDepartment();
            CreateFieldDataTable();
            LoadPeople();
            Session["PEOPLE_DETAIL_ID"] = null;
            Session["mode"] = null;

            //FIS
            if (Configuration.Company == 1)
            {
                lblDepartment.Visible = false;
                lblDepartment2.Visible = false;
                Label19.Visible = false;
                ddlDepartment.Visible = false;
                lblDesignation.Visible = false;
                lblDesignation2.Visible = false;
                Label21.Visible = false;
                txtDesignation.Visible = false;
            }
        }
    }

    private void LoadPeople()
    {
        DataTable dtPpl = PplCtl.GetPeople(Convert.ToInt64(Session["PeopleID"]), Constants.LongNullValue, null, null);

        //View Tab
        if (dtPpl.Rows.Count > 0)
        {
            if (dtPpl.Rows[0]["TITLE"].ToString() == "0")
            {
                lblTitle2.Text = "Mr.";
            }
            else if (dtPpl.Rows[0]["TITLE"].ToString() == "1")
            {
                lblTitle2.Text = "Ms.";
            }
            else
            {
                lblTitle2.Text = "Mrs.";
            }
            lblLocation2.Text = dtPpl.Rows[0]["LOCATION"].ToString();
            lblFName2.Text = dtPpl.Rows[0]["FNAME"].ToString();
            lblLName2.Text = dtPpl.Rows[0]["LNAME"].ToString();
            lblDepartment2.Text = dtPpl.Rows[0]["DEPARTMENT"].ToString();
            if (dtPpl.Rows[0]["DOB"].ToString() != "")
            {
                lblDOB2.Text = Convert.ToDateTime(dtPpl.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
            }
            lblAccountNo2.Text = dtPpl.Rows[0]["ACCOUNT_NO"].ToString();
            lblOrganization2.Text = dtPpl.Rows[0]["NAME"].ToString();
            lblOrganizationType2.Text = dtPpl.Rows[0]["TYPE"].ToString();
            lblDesignation2.Text = dtPpl.Rows[0]["SLASH_DESC"].ToString();
            lblEmail2.Text = dtPpl.Rows[0]["EMAIL"].ToString();
            lblPhone2.Text = dtPpl.Rows[0]["PHONE"].ToString();

            gvField.Enabled = false;

            DataTable dtDetail = PplCtl.GetPeopleDetail(Convert.ToInt64(Session["PeopleID"]));
            if (dtDetail != null)
            {
                gvField.DataSource = dtDetail;
                gvField.DataBind();

                gvFieldEdit.DataSource = dtDetail;
                gvFieldEdit.DataBind();
                Session.Add("dtDetail", dtDetail);
            }

            lblTitle2.Visible = true;
            if (dtPpl.Rows[0]["TYPE_ID"].ToString() == "2")
            {
                lblLocation.Visible = true;
                lblLocation2.Visible = true;
            }

            lblFName2.Visible = true;
            lblLName2.Visible = true;
            lblEmail2.Visible = true;
            lblPhone2.Visible = true;
            lblDOB2.Visible = true;
            lblAccountNo2.Visible = true;
            lblOrganizationType2.Visible = true;
            lblOrganization2.Visible = true;
            lblDepartment2.Visible = true;
            lblDesignation2.Visible = true;

            //Edit Tab

            hfPeopleID.Value = Session["PeopleID"].ToString();
            ddlTitle.SelectedValue = dtPpl.Rows[0]["TITLE"].ToString();
            txtFName.Text = dtPpl.Rows[0]["FNAME"].ToString();
            txtLName.Text = dtPpl.Rows[0]["LNAME"].ToString();
            UserId = Convert.ToInt32(dtPpl.Rows[0]["ROLE_ID"]);
            txtDesignation.Text = dtPpl.Rows[0]["DESIGNATION_ID"].ToString();
            ddlOrganizationType.SelectedValue = dtPpl.Rows[0]["TYPE_ID"].ToString();
            if (dtPpl.Rows[0]["TYPE_ID"].ToString() == "2")
            {
                ddlLocation.Visible = true;
                lblLocation.Visible = true;
                lblLocationEdit.Visible = true;
                ddlLocation.SelectedValue = dtPpl.Rows[0]["LOCATION_ID"].ToString();
            }
            this.LoadOrganization();
            if (dtPpl.Rows[0]["DEPARTMENTID"].ToString() != "0")
            {
                ddlDepartment.SelectedValue = dtPpl.Rows[0]["DEPARTMENTID"].ToString();
            }
            cbActive.Checked = Convert.ToBoolean(dtPpl.Rows[0]["IS_ACTIVE"]);
            if (dtPpl.Rows[0]["DOB"].ToString() != "")
            {
                txtDOB.Text = Convert.ToDateTime(dtPpl.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
            }
            txtAccountNo.Text = dtPpl.Rows[0]["ACCOUNT_NO"].ToString();
            txtEmail.Text = dtPpl.Rows[0]["EMAIL"].ToString();
            txtPhone.Text = dtPpl.Rows[0]["PHONE"].ToString();
            ddlOrganization.SelectedValue = dtPpl.Rows[0]["ORGANIZATION_ID"].ToString();
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
    /// Loads Department To Department Combo
    /// </summary>
    private void LoadDepartment()
    {
        SLASHCodesController mController = new SLASHCodesController();
        DataTable m_dt = mController.SelectSlashCodes(Constants.IntNullValue, null, Constants.Department, null, Constants.IntNullValue, true);
        ddlDepartment.DataSource = m_dt;
        ddlDepartment.DataTextField = "SLASH_DESC";
        ddlDepartment.DataValueField = "REF_ID";
        ddlDepartment.DataBind();
    }

    #region Edit Tab

    protected void btnAddField_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        DataTable dtDetail = (DataTable)Session["dtDetail"];
        DataRow dr = dtDetail.NewRow();
        int counter = 0;
        Session["mode"] = null;

        if (Session["PEOPLE_DETAIL_ID"] == null)
        {
            dr["PEOPLE_DETAIL_ID"] = counter;
        }
        else
        {
            dr["PEOPLE_DETAIL_ID"] = ((int)Session["PEOPLE_DETAIL_ID"]) + 1;
        }

        if (hfFieldType.Value == "Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfAddCounter.Value;
            hfAddCounter.Value = (Convert.ToInt32(hfAddCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Phone")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfPhoneCounter.Value;
            hfPhoneCounter.Value = (Convert.ToInt32(hfPhoneCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Cell")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfCellCounter.Value;
            hfCellCounter.Value = (Convert.ToInt32(hfCellCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Fax")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfFaxCounter.Value;
            hfFaxCounter.Value = (Convert.ToInt32(hfFaxCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Email")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfEmailCounter.Value;
            hfEmailCounter.Value = (Convert.ToInt32(hfEmailCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "CNIC")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfCNICCounter.Value;
            hfCNICCounter.Value = (Convert.ToInt32(hfCNICCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Citizenship")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfCitizenshipCounter.Value;
            hfCitizenshipCounter.Value = (Convert.ToInt32(hfCitizenshipCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Passport")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfPassportCounter.Value;
            hfPassportCounter.Value = (Convert.ToInt32(hfPassportCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Emergency Name")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfEmergencyNameCounter.Value;
            hfEmergencyNameCounter.Value = (Convert.ToInt32(hfEmergencyNameCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Relation")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfRelationCounter.Value;
            hfRelationCounter.Value = (Convert.ToInt32(hfRelationCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Phone Phone")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfHomePhoneCounter.Value;
            hfHomePhoneCounter.Value = (Convert.ToInt32(hfHomePhoneCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Work Phone")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfWorkPhoneCounter.Value;
            hfWorkPhoneCounter.Value = (Convert.ToInt32(hfWorkPhoneCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Home Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfHomeAddressCounter.Value;
            hfHomeAddressCounter.Value = (Convert.ToInt32(hfHomeAddressCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Work Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfWorkAddressCounter.Value;
            hfWorkAddressCounter.Value = (Convert.ToInt32(hfWorkAddressCounter.Value) + 1).ToString();
        }
        else
        {
            dr["FIELD_TYPE"] = lblField.Text;
        }

        dr["FIELD_VALUE"] = txtValue.Text;
        dtDetail.Rows.Add(dr);
        txtValue.Text = "";

        dtDetail.DefaultView.Sort = "FIELD_TYPE ASC";
        Session.Add("dtDetail", dtDetail);
        Session.Add("PEOPLE_DETAIL_ID", Convert.ToInt32(dr["PEOPLE_DETAIL_ID"]));
        gvFieldEdit.DataSource = dtDetail;
        gvFieldEdit.DataBind();

    }   

    protected void gvFieldEdit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            DataTable dtDetail = (DataTable)this.Session["dtDetail"];

            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                if (dtDetail.Rows[i]["PEOPLE_DETAIL_ID"].ToString() == e.CommandArgument.ToString())
                {
                    dtDetail.Rows.RemoveAt(i);
                }
            }

            this.Session.Add("dtDetail", dtDetail);
            gvFieldEdit.DataSource = dtDetail;
            gvFieldEdit.DataBind();
        }
    }

    protected void gvFieldEdit_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvFieldEdit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (CanDelete("1"))
            {
                ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;
                btnDelete.Visible = false;
            }
        }
        if (e.Row.RowIndex > -1)
        {
            TextBox txtFIELD_TYPE = e.Row.FindControl("txtVl") as TextBox;
            Label lblFIELD_VALUE = e.Row.FindControl("lblFld2") as Label;
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;

            if (Session["mode"] != null)
            {
                if (Session["mode"].ToString() == "view")
                {
                    txtFIELD_TYPE.Visible = false;
                    lblFIELD_VALUE.Text = txtFIELD_TYPE.Text;
                    lblFIELD_VALUE.Visible = true;
                    btnDelete.Visible = false;
                }
                else if (Session["mode"].ToString() == "edit")
                {
                    TextBox lblFld = e.Row.FindControl("lblFld") as TextBox;
                    if (lblFld.Text.Contains("Address"))
                    {
                        hfAddCounter.Value = (Convert.ToInt32(hfAddCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Phone"))
                    {
                        hfPhoneCounter.Value = (Convert.ToInt32(hfPhoneCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Cell"))
                    {
                        hfCellCounter.Value = (Convert.ToInt32(hfCellCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Fax"))
                    {
                        hfFaxCounter.Value = (Convert.ToInt32(hfFaxCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Email"))
                    {
                        hfEmailCounter.Value = (Convert.ToInt32(hfEmailCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("CNIC"))
                    {
                        hfCNICCounter.Value = (Convert.ToInt32(hfCNICCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Passport"))
                    {
                        hfPassportCounter.Value = (Convert.ToInt32(hfPassportCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Citizenship"))
                    {
                        hfCitizenshipCounter.Value = (Convert.ToInt32(hfCitizenshipCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Emergency Name"))
                    {
                        hfEmergencyNameCounter.Value = (Convert.ToInt32(hfEmergencyNameCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Relation"))
                    {
                        hfRelationCounter.Value = (Convert.ToInt32(hfRelationCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Home Phone"))
                    {
                        hfHomePhoneCounter.Value = (Convert.ToInt32(hfHomePhoneCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Work Phone"))
                    {
                        hfWorkPhoneCounter.Value = (Convert.ToInt32(hfWorkPhoneCounter.Value) + 1).ToString();
                    }
                    else if (lblFld.Text.Contains("Home Address"))
                    {
                        hfHomeAddressCounter.Value = (Convert.ToInt32(hfHomeAddressCounter.Value) + 1).ToString();
                    }
                }
            }

        }
    }

    protected void ddlOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        this.LoadOrganization();
        if (ddlOrganizationType.SelectedValue == "2")
        {
            lblLocation.Visible = true;
            ddlLocation.Visible = true;
            lblLocationEdit.Visible = true;
        }
        else
        {
            lblLocation.Visible = false;
            lblLocationEdit.Visible = false;
            ddlLocation.Visible = false;
        }
    }

    private void LoadOrganization()
    {
        OrganizationController OrgCtl = new OrganizationController();
        DataTable dtOrganization = OrgCtl.GetOrganization(Constants.LongNullValue, Convert.ToInt32(ddlOrganizationType.SelectedValue));
        if (dtOrganization != null)
        {
            clsWebFormUtil.FillDropDownList(ddlOrganization, dtOrganization, 0, 1, true);
        }
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        try
        {            
            DateTime dtDOB = Constants.DateNullValue;
            if (txtDOB.Text.Length > 0)
            {
                dtDOB = Convert.ToDateTime(txtDOB.Text);
            }

            if (txtFName.Text.Length > 0)
            {
                CreateFieldDataTable();
                DataTable dtDetail = (DataTable)Session["dtDetail"];
                foreach (GridViewRow row in gvFieldEdit.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtFIELD_TYPE = row.FindControl("txtVl") as TextBox;
                        TextBox lblFIELD_VALUE = row.FindControl("lblFld") as TextBox;
                        DataRow dr = dtDetail.NewRow();
                        dr["FIELD_TYPE"] = lblFIELD_VALUE.Text;
                        dr["FIELD_VALUE"] = txtFIELD_TYPE.Text;
                        dtDetail.Rows.Add(dr);
                    }
                }

                if (PplCtl.UpdatePeople(Convert.ToInt64(hfPeopleID.Value), Convert.ToInt64(ddlOrganization.SelectedValue), Convert.ToInt32(ddlTitle.SelectedValue), txtFName.Text, txtLName.Text, dtDOB, Convert.ToInt32(ddlDepartment.SelectedValue), txtEmail.Text, txtPhone.Text, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlLocation.SelectedValue), txtDesignation.Text,cbActive.Checked,txtAccountNo.Text, dtDetail))
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "People", txtFName.Text + " " + txtLName.Text + " updated");


                    ///
                    /// If Fast Services People
                    /// Save it to Distributor_User Table
                    ///                        
                    if (ddlOrganization.SelectedValue == "1")
                    {
                        if (UserId > 0)
                        {
                            UpdateNeonChemicalEmployee(Convert.ToInt32(hfPeopleID.Value));
                        }
                        else
                        {
                            SaveNeonChemcialEmployee(Convert.ToInt32(hfPeopleID.Value));
                        }
                    }
                    else
                    {
                        if (UserId > 0)
                        {
                            DeleteUser(Convert.ToInt32(hfPeopleID.Value));
                        }
                    }

                    ltrlEdit.Text = Utility.ShowAlert(true,"People Updated Successfully.");

                    LoadPeople();
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false,"Some error occured.");
                }
            }
            else
            {
                ltrlEdit.Text = Utility.ShowAlert(false,"Please enter First Name.");
            }
        }
        catch (Exception ex)
        {
            ltrlEdit.Text = Utility.ShowAlert(false, ex.Message);
        }
    }

    private void CreateFieldDataTable()
    {
        DataTable dtDetail = new DataTable();
        dtDetail.Columns.Add("PEOPLE_DETAIL_ID", typeof(long));
        dtDetail.Columns.Add("FIELD_TYPE", typeof(string));
        dtDetail.Columns.Add("FIELD_VALUE", typeof(string));
        this.Session.Add("dtDetail", dtDetail);
    }

    private void SaveNeonChemcialEmployee(int People_ID)
    {
        SETTINGS_TABLE_Controller mAutoCode = new SETTINGS_TABLE_Controller();

        string strcode = "";
        UserId = int.Parse(mAutoCode.GetAutoCustomerCode("EM", 0, Constants.LongNullValue).ToString());

        if (UserId.ToString().Length == 1)
        {
            strcode = "EM" + "000" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 2)
        {
            strcode = "EM" + "00" + UserId.ToString();
        }
        else if (UserId.ToString().Length == 3)
        {
            strcode = "EM" + "0" + UserId.ToString();
        }
        else
        {
            strcode = "EM" + UserId.ToString();
        }

        UController.InsertDistributor_User(int.Parse(this.Session["CompanyId"].ToString()), "", true, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
            int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", "", txtPhone.Text, strcode, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
    }

    private void UpdateNeonChemicalEmployee(int People_ID)
    {
        UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), "", true, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
                    int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", ""
                    , txtPhone.Text, null, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
    }

    private void DeleteUser(int People_ID)
    {
        UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), "", false, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
                    int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", ""
                    , txtPhone.Text, null, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmPeople.aspx?LevelID=144&TopMenu=1");
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
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0'>View People </li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0' style='display:none'>View People </li>";
        }
        if (!CanView("1"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "1";
            }
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1'>Edit People</li>";
            hfEditIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            //if (!CanEdit("1"))
            //{
            //    btnSave.Visible = false;
            //    btnCancel.Visible = false;
            //}
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1' style='display:none'>Edit People</li>";
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