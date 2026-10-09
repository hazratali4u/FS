using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmPeople : System.Web.UI.Page
{
    PeopleController PplCtl = new PeopleController();
    Distributor_UserController UController = new Distributor_UserController();
    XmlUtil xmlUtl = new XmlUtil();
    static int UserId;
    
        
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                btnNewPeople.Visible = false;
            }

            if (Request.QueryString["mode"] != null)
            {
                pnlAddPeople.Visible = true;
                pnlPeopleList.Visible = false;
            }
            else
            {
                LoadPeople(Constants.LongNullValue,null,null);
                FillRepeater();
            }
            LoadOrganization();
            LoadDistributor();
            this.LoadDepartment();
            CreateFieldDataTable();
            Session["PEOPLE_DETAIL_ID"] = null;
            Session["mode"] = null;
            //FIS
            if (Configuration.Company == 1)
            {
                lblDepartment.Visible = false;
                lblDepartment2.Visible = false;
                ddlDepartment.Visible = false;
                lblDesignation.Visible = false;
                lblDesignation2.Visible = false;
                txtDesignation.Visible = false;
            }
        }
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ltrlPeople.Text = "";
            DateTime dtDOB = Constants.DateNullValue;
            if (txtDOB.Text.Length > 0)
            {
                dtDOB = Convert.ToDateTime(txtDOB.Text);
            }

            if (txtFName.Text.Length > 0)
            {
                CreateFieldDataTable();
                DataTable dtDetail = (DataTable)Session["dtDetail"];
                foreach (GridViewRow row in gvField.Rows)
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

                if (btnSave.Text == "Save")
                {
                    long People_ID = Constants.LongNullValue;
                    
                    People_ID = PplCtl.InsertPeople(Convert.ToInt64(ddlOrganization.SelectedValue), Convert.ToInt32(ddlTitle.SelectedValue), txtFName.Text, txtLName.Text, dtDOB, Convert.ToInt32(ddlDepartment.SelectedValue), txtEmail.Text, txtPhone.Text, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlLocation.SelectedValue),txtDesignation.Text,txtAccountName.Text,dtDetail);

                    if(People_ID > 0)
                    {
                        //Save Transaction in log                        
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "People", "Added for " + ddlOrganization.SelectedItem.Text);

                        ///
                        /// If Fast Services People
                        /// Save it to Distributor_User Table
                        ///                        
                        if (ddlOrganization.SelectedValue == "1")
                        {
                            SaveNeonChemcialEmployee((int) People_ID);
                        }
                        Response.Redirect("frmPeople.aspx?LevelID=144&TopMenu=1");
                    }
                    else
                    {
                        ltrlPeople.Text = Utility.ShowAlert(false,"Some error occured.");
                    }
                }                
            }
            else
            {
                ltrlPeople.Text = Utility.ShowAlert(false,"Please enter First Name.");
            }
        }
        catch (Exception ex)
        {
            ltrlPeople.Text = Utility.ShowAlert(false, ex.Message);
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

    private void FillRepeater()
    {
        rPeople.DataSource = null;
        rPeople.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];

        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rPeople.DataSource = PagedResults;
            rPeople.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows.Count.ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows.Count) / Convert.ToDecimal(20)));

        }
        else if (PagedResults.Count == 0)
        {
            linkbtnnext.Visible = false;
            linkbtnprev.Visible = false;
            lblTotalNoOfPages.Visible = false;
            lblCurrentPageNo.Visible = false;
            lblTotalNoOfRecords.Visible = false;
            lblOf.Visible = false;
            lblDummy.Visible = false;
            lblTotalNoOfRecords.Text = "";
        }
    }

    public int CurrentPage
    {
        get
        {
            object objview = this.ViewState["_CurrentPage"];
            if (objview == null)
                return 0;
            else
                return (int)objview;
        }
        set
        {
            this.ViewState["_CurrentPage"] = value;
        }
    }

    protected void linkbtnprev_Click(object sender, EventArgs e)
    {
        CurrentPage -= 1;
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        FillRepeater();
    }

    public void LoadPeople(long p_OrganizationID,string p_FNAME,string p_LNAME)
    {
        DataTable dtPeople = new DataTable();
        if (Session["RoleID"].ToString() == "1")
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, p_OrganizationID, p_FNAME, p_LNAME);
        }
        else
        {
            dtPeople = PplCtl.GetPeople(Constants.LongNullValue, p_OrganizationID, p_FNAME, p_LNAME, Convert.ToInt32(Session["UserID"]));
        }

        if (dtPeople != null)
        {
            this.Session.Add("dtPeople", dtPeople);
        }

    }

    protected void rPeople_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        ltrlView.Text = "";
        if (e.CommandName == "view")
        {
            this.Session.Add("PeopleID", e.CommandArgument.ToString());
            Response.Redirect("frmPeopleDetail.aspx?LevelType=1&LevelID=172", false);
        }        
        else if (e.CommandName == "edit")
        {
            Session["mode"] = "edit";
            EditPeople(Convert.ToInt64(e.CommandArgument));
        }
        else if (e.CommandName == "del")
        {
            if (PplCtl.DeletePeople(Convert.ToInt64(e.CommandArgument)))
            {
                HiddenField hfFName = (HiddenField)e.Item.FindControl("hfFName");
                HiddenField hfLName = (HiddenField)e.Item.FindControl("hfLName");
                
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "People", hfFName.Value + " " + hfLName.Value + " deleted");

                ltrlView.Text = Utility.ShowAlert(true, "People deleted successfully.");
                LoadPeople(Constants.LongNullValue, null, null);
                FillRepeater();
            }
            else
            {
                ltrlView.Text = Utility.ShowAlert(false,"Some error occured.");
            }
        }
    }

    private void EditPeople(long p_PeopleID)
    {
        DataTable dtPpl = PplCtl.GetPeople(p_PeopleID, Constants.LongNullValue,null,null);
        hfPeopleID.Value = p_PeopleID.ToString();
        if (dtPpl.Rows.Count > 0)
        {
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
                ddlLocation.SelectedValue = dtPpl.Rows[0]["LOCATION_ID"].ToString();
            }
            this.LoadOrganization();
            if (dtPpl.Rows[0]["DEPARTMENTID"].ToString() != "0")
            {
                ddlDepartment.SelectedValue = dtPpl.Rows[0]["DEPARTMENTID"].ToString();
            }
            if (dtPpl.Rows[0]["DOB"].ToString() != "")
            {
                txtDOB.Text = Convert.ToDateTime(dtPpl.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
            }
            txtEmail.Text = dtPpl.Rows[0]["EMAIL"].ToString();
            txtPhone.Text = dtPpl.Rows[0]["PHONE"].ToString();
            ddlOrganization.SelectedValue = dtPpl.Rows[0]["ORGANIZATION_ID"].ToString();
            
            
            DataTable dtDetail = PplCtl.GetPeopleDetail(p_PeopleID);
            if (dtDetail != null)
            {
                gvField.DataSource = dtDetail;
                gvField.DataBind();
                Session.Add("dtDetail", dtDetail);
            }

            btnSave.Text = "Update";

            pnlAddPeople.Visible = true;
            pnlPeopleList.Visible = false;
        }
    }

    protected void rPeople_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
            LinkButton btnView = e.Item.FindControl("btnView") as LinkButton;
            LinkButton btnFName = e.Item.FindControl("btnFName") as LinkButton;
            LinkButton btnLName = e.Item.FindControl("btnLName") as LinkButton;

            //if (!CanEdit(Request.QueryString["LevelID"].ToString()))
            //{
            //    btnView.Visible = false;
            //    btnFName.Enabled = false;
            //    btnLName.Enabled = false;
            //}

            //if (!CanDelete(Request.QueryString["LevelID"].ToString()))
            //{
            //    btnDelete.Visible = false;
            //}
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmPeople.aspx?LevelID=144&TopMenu=1");
    }

    private void LoadOrganization()
    {
        OrganizationController OrgCtl = new OrganizationController();
        DataTable dtOrganization = new DataTable();
        if (Session["RoleID"].ToString() == "1")
        {
            dtOrganization = OrgCtl.GetOrganization(Constants.LongNullValue, Convert.ToInt32(ddlOrganizationType.SelectedValue));
        }
        else
        {
            dtOrganization = OrgCtl.GetOrganization(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlOrganizationType.SelectedValue));
        }
        if (dtOrganization != null)
        {
            clsWebFormUtil.FillDropDownList(ddlOrganization, dtOrganization, 0, 1, true);
        }
    }
    
    protected void btnAddField_Click(object sender, EventArgs e)
    {
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
        gvField.DataSource = dtDetail;
        gvField.DataBind();

    }   

    private void CreateFieldDataTable()
    {
        DataTable dtDetail = new DataTable();
        dtDetail.Columns.Add("PEOPLE_DETAIL_ID", typeof(long));
        dtDetail.Columns.Add("FIELD_TYPE", typeof(string));
        dtDetail.Columns.Add("FIELD_VALUE", typeof(string));
        this.Session.Add("dtDetail", dtDetail);
    }
    
    protected void gvField_RowCommand(object sender, GridViewCommandEventArgs e)
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
            gvField.DataSource = dtDetail;
            gvField.DataBind();
        }
    }

    protected void gvField_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
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

        UController.InsertDistributor_User(int.Parse(this.Session["CompanyId"].ToString()),"" , true, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
            int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", "" , txtPhone.Text, strcode, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
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

    protected void ddlOrganizationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadOrganization();
        if (ddlOrganizationType.SelectedValue == "2")
        {
            lblLocation.Visible = true;
            ddlLocation.Visible = true;
        }
        else
        {
            lblLocation.Visible = false;
            ddlLocation.Visible = false;
        }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        LoadPeople(Constants.LongNullValue, null, null);
        dt = (DataTable)Session["dtPeople"];
        DataTable dtFilter = new DataTable();
        dtFilter = dt.Clone();
        DataRow[] foundRows = null;
        if (ddlFilterFor.SelectedValue == "-1" && ddlFilterType.SelectedValue == "-1")
        {
            LoadPeople(Constants.LongNullValue, null, null);
            FillRepeater();
        }
        else if (ddlFilterType.SelectedValue == "-1")
        {
            if (ddlFilterFor.SelectedValue == "0")
            {
                foundRows = dt.Select("FNAME  LIKE  '%" + txtSearch.Text + "%'");
            }
            else if (ddlFilterFor.SelectedValue == "1")
            {
                foundRows = dt.Select("LNAME  LIKE  '%" + txtSearch.Text + "%'");
            }
            else if (ddlFilterFor.SelectedValue == "2")
            {
                foundRows = dt.Select("SLASH_DESC  LIKE  '%" + txtSearch.Text + "%'");
            }
            else if (ddlFilterFor.SelectedValue == "3")
            {
                foundRows = dt.Select("DEPARTMENT  LIKE  '%" + txtSearch.Text + "%'");
            }
            else if (ddlFilterFor.SelectedValue == "4")
            {
                foundRows = dt.Select("NAME  LIKE  '%" + txtSearch.Text + "%'");
            }
        }
        else if (ddlFilterFor.SelectedValue == "-1")
        {
            foundRows = dt.Select("TYPE_ID ='" + ddlFilterType.SelectedValue + "'");            
        }
        else
        {
            if (ddlFilterFor.SelectedValue == "0")
            {
                foundRows = dt.Select("FNAME  LIKE  '%" + txtSearch.Text + "%' AND TYPE_ID ='" + ddlFilterType.SelectedValue + "'");
            }
            else if (ddlFilterFor.SelectedValue == "1")
            {
                foundRows = dt.Select("LNAME  LIKE  '%" + txtSearch.Text + "%' AND TYPE_ID ='" + ddlFilterType.SelectedValue + "'");
            }
            else if (ddlFilterFor.SelectedValue == "2")
            {
                foundRows = dt.Select("SLASH_DESC  LIKE  '%" + txtSearch.Text + "%' AND TYPE_ID = '" + ddlFilterType.SelectedValue + "'");
            }
            else if (ddlFilterFor.SelectedValue == "3")
            {
                foundRows = dt.Select("DEPARTMENT  LIKE  '%" + txtSearch.Text + "%' AND TYPE_ID = '" + ddlFilterType.SelectedValue + "'");
            }
            else if (ddlFilterFor.SelectedValue == "4")
            {
                foundRows = dt.Select("NAME  LIKE  '%" + txtSearch.Text + "%' AND TYPE_ID = '" + ddlFilterType.SelectedValue + "'");
            }
        }

        if (foundRows != null)
        {
            for (int i = 0; i < foundRows.Length; i++)
            {
                dtFilter.ImportRow(foundRows[i]);
            }
            Session.Add("dtPeople", dtFilter);
            FillRepeater();
        }
    }

    protected void ddlFilterType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        LoadPeople(Constants.LongNullValue, null, null);
        

        if (ddlFilterType.SelectedValue == "-1")
        {
            FillRepeater();
        }
        else
        {
            dt = (DataTable)Session["dtPeople"];
            DataTable dtFilter = new DataTable();
            dtFilter = dt.Clone();
            DataRow[] foundRows = null;
            foundRows = dt.Select("TYPE_ID ='" + ddlFilterType.SelectedValue + "'");

            if (foundRows != null)
            {
                for (int i = 0; i < foundRows.Length; i++)
                {
                    dtFilter.ImportRow(foundRows[i]);
                }
                Session.Add("dtPeople", dtFilter);
                FillRepeater();
            }
        }
    }

    protected void btnFNameSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataView dv = new DataView(dt);

        if (Session["FName"] != null)
        {
            if (Session["FName"].ToString() == "DESC")
            {
                dv.Sort = "FNAME DESC";
                Session.Add("FName", "ASC");
            }
            else
            {
                dv.Sort = "FNAME ASC";
                Session.Add("FName", "DESC");
            }
        }
        else
        {
            dv.Sort = "FNAME ASC";
            Session.Add("FName", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtPeople", dt);
        FillRepeater();

    }

    protected void btnLNameSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataView dv = new DataView(dt);

        if (Session["LName"] != null)
        {
            if (Session["LName"].ToString() == "DESC")
            {
                dv.Sort = "LNAME DESC";
                Session.Add("LName", "ASC");
            }
            else
            {
                dv.Sort = "LNAME ASC";
                Session.Add("LName", "DESC");
            }
        }
        else
        {
            dv.Sort = "LNAME ASC";
            Session.Add("LName", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtPeople", dt);
        FillRepeater();

    }

    protected void btnDesignationSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataView dv = new DataView(dt);

        if (Session["Desig"] != null)
        {
            if (Session["Desig"].ToString() == "DESC")
            {
                dv.Sort = "SLASH_DESC DESC";
                Session.Add("Desig", "ASC");
            }
            else
            {
                dv.Sort = "SLASH_DESC ASC";
                Session.Add("Desig", "DESC");
            }
        }
        else
        {
            dv.Sort = "SLASH_DESC ASC";
            Session.Add("Desig", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtPeople", dt);
        FillRepeater();

    }

    protected void btnDepartmentSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataView dv = new DataView(dt);

        if (Session["Dept"] != null)
        {
            if (Session["Dept"].ToString() == "DESC")
            {
                dv.Sort = "DEPARTMENT DESC";
                Session.Add("Dept", "ASC");
            }
            else
            {
                dv.Sort = "DEPARTMENT ASC";
                Session.Add("Dept", "DESC");
            }
        }
        else
        {
            dv.Sort = "DEPARTMENT ASC";
            Session.Add("Dept", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtPeople", dt);
        FillRepeater();

    }

    protected void btnOrganizationSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataView dv = new DataView(dt);

        if (Session["Org"] != null)
        {
            if (Session["Org"].ToString() == "DESC")
            {
                dv.Sort = "NAME DESC";
                Session.Add("Org", "ASC");
            }
            else
            {
                dv.Sort = "NAME ASC";
                Session.Add("Org", "DESC");
            }
        }
        else
        {
            dv.Sort = "NAME ASC";
            Session.Add("Org", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtPeople", dt);
        FillRepeater();

    }

    protected void btnNewPeople_Click(object sender, EventArgs e)
    {        
        Response.Redirect("frmPeople.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=1&TopMenu=1&mode=new");
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

    private bool CanDelete(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
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