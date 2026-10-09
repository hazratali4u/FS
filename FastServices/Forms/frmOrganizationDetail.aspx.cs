using System;
using System.Data;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmOrganizationDetail : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();
    QuotationController QtnCtl = new QuotationController();
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

            this.LoadCountry();
            this.LoadCity();
            
            //View Edit Organization 
            CreateFieldDataTable();
            LoadData();            

            //Contacts
            CreateFieldDataTableConcat();
            this.LoadDepartment();
            if (Session["OrganizationID"].ToString() == "1")
            {
                this.LoadDistributor();
            }
            LoadPeople(Convert.ToInt64(Session["OrganizationID"]), null, null);
            FillRepeater();

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

    private void LoadData()
    {
        DataTable dtOrg = OrgCtl.GetOrganization(Convert.ToInt64(Session["OrganizationID"]), Constants.IntNullValue);

        if (dtOrg.Rows.Count > 0)
        {
            if (dtOrg.Rows[0]["ORGANIZATION_ID"].ToString() == "1")
            {
                lblOrgType.Visible = false;
                lblType.Visible = false;
                lblType2.Visible = false;
                ddlType.Visible = false;
            }
            txtName.Text = dtOrg.Rows[0]["NAME"].ToString();
            lblName2.Text = dtOrg.Rows[0]["NAME"].ToString();
            if (dtOrg.Rows[0]["TYPE_ID"].ToString() == "0")
            {
                ddlType.SelectedValue = "0";
            }
            else if (dtOrg.Rows[0]["TYPE_ID"].ToString() == "2")
            {
                ddlLocation.Visible = true;
                lblLocation.Visible = true;
            }
            else
            {
                ddlType.SelectedValue = dtOrg.Rows[0]["TYPE_ID"].ToString();
            }


            lblType2.Text = dtOrg.Rows[0]["TYPE"].ToString();
            ddlCountry.SelectedValue = dtOrg.Rows[0]["CountryID"].ToString();
            lblCountry2.Text = dtOrg.Rows[0]["CountryName"].ToString();
            this.LoadCity();
            ddlCity.SelectedValue = dtOrg.Rows[0]["CityID"].ToString();
            lblCity2.Text = dtOrg.Rows[0]["CityName"].ToString();

            DataTable dtDetail = OrgCtl.GetOrganizationDetail(Convert.ToInt64(Session["OrganizationID"]));
            if (dtDetail != null)
            {
                gvField.DataSource = dtDetail;
                gvField.DataBind();

                Session.Add("mode", "Update");
                gvFieldEdit.DataSource = dtDetail;
                gvFieldEdit.DataBind();
                Session.Add("dtDetail", dtDetail);
            }

        }
    }

    #region Edit Organization

    protected void gvField_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        if (e.CommandName == "del")
        {
            DataTable dtDetail = (DataTable)this.Session["dtDetail"];

            for (int i = 0; i < dtDetail.Rows.Count; i++)
            {
                if (dtDetail.Rows[i]["ORGANIZATION_DETAIL_ID"].ToString() == e.CommandArgument.ToString())
                {
                    dtDetail.Rows.RemoveAt(i);
                }
            }

            this.Session.Add("dtDetail", dtDetail);
            gvFieldEdit.DataSource = dtDetail;
            gvFieldEdit.DataBind();
        }
    }

    protected void gvField_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvField_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (CanDelete("1"))
            {
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("btnDelete");
                btnDelete.Visible = false;
            }
        }
        if (e.Row.RowIndex > -1)
        {
            if (Session["mode"] != null)
            {
                TextBox lblFld = e.Row.FindControl("lblFld") as TextBox;
                if (lblFld.Text.Contains("Office Address"))
                {
                    hfAddOfficeCounter.Value = (Convert.ToInt32(hfAddOfficeCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Factory Address"))
                {
                    hfAddFactoryCounter.Value = (Convert.ToInt32(hfAddFactoryCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Email"))
                {
                    hfEmailCounter.Value = (Convert.ToInt32(hfEmailCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Office Phone"))
                {
                    hfPhoneOfficeCounter.Value = (Convert.ToInt32(hfPhoneOfficeCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Factory Phone"))
                {
                    hfPhoneFactoryCounter.Value = (Convert.ToInt32(hfPhoneFactoryCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Office Fax"))
                {
                    hfFaxOfficeCounter.Value = (Convert.ToInt32(hfFaxOfficeCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Factory Fax"))
                {
                    hfFaxFactoryCounter.Value = (Convert.ToInt32(hfFaxFactoryCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Website"))
                {
                    hfWebsiteCounter.Value = (Convert.ToInt32(hfWebsiteCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Facsimile"))
                {
                    hfFacsimileCounter.Value = (Convert.ToInt32(hfFacsimileCounter.Value) + 1).ToString();
                }

                else if (lblFld.Text.Contains("UAN"))
                {
                    hfUANCounter.Value = (Convert.ToInt32(hfUANCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("NTN"))
                {
                    hfNTNCounter.Value = (Convert.ToInt32(hfNTNCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("GST"))
                {
                    hfGSTCounter.Value = (Convert.ToInt32(hfGSTCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Bank Account"))
                {
                    hfBankAccountCounter.Value = (Convert.ToInt32(hfBankAccountCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Branch Code"))
                {
                    hfBranchCodeCounter.Value = (Convert.ToInt32(hfBranchCodeCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Branch Address"))
                {
                    hfBranchAddressCounter.Value = (Convert.ToInt32(hfBranchAddressCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Swift Code"))
                {
                    hfSwiftCodeCounter.Value = (Convert.ToInt32(hfSwiftCodeCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("IBAN"))
                {
                    hfIBANCounter.Value = (Convert.ToInt32(hfIBANCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("RTGS"))
                {
                    hfRTGSCounter.Value = (Convert.ToInt32(hfRTGSCounter.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("PABX Cell"))
                {
                    hfOthersCounter.Value = (Convert.ToInt32(hfOthersCounter.Value) + 1).ToString();
                }
            }
        }
    }

    /// <summary>
    /// Loads Cities To City Comb
    /// </summary>
    private void LoadCity()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, int.Parse(ddlCountry.SelectedValue.ToString()), null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCity, dt, 0, 4, true);
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        this.LoadCity();
    }

    private void LoadCountry()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCountry, dt, 0, 4, true);
    }

    protected void btnAddField_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfEditIndex.Value;
        DataTable dtDetail = (DataTable)Session["dtDetail"];
        DataRow dr = dtDetail.NewRow();
        int counter = 0;
        Session["mode"] = null;

        if (Session["ORGANIZATION_DETAIL_ID"] == null)
        {
            dr["ORGANIZATION_DETAIL_ID"] = counter;
        }
        else
        {
            dr["ORGANIZATION_DETAIL_ID"] = ((int)Session["ORGANIZATION_DETAIL_ID"]) + 1;
        }

        if (hfFieldType.Value == "Office Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfAddOfficeCounter.Value;
            hfAddOfficeCounter.Value = (Convert.ToInt32(hfAddOfficeCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Factory Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfAddFactoryCounter.Value;
            hfAddFactoryCounter.Value = (Convert.ToInt32(hfAddFactoryCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Email")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfEmailCounter.Value;
            hfEmailCounter.Value = (Convert.ToInt32(hfEmailCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Office Phone")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfPhoneOfficeCounter.Value;
            hfPhoneOfficeCounter.Value = (Convert.ToInt32(hfPhoneOfficeCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Factory Phone")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfPhoneFactoryCounter.Value;
            hfPhoneFactoryCounter.Value = (Convert.ToInt32(hfPhoneFactoryCounter.Value) + 1).ToString();
        }

        else if (hfFieldType.Value == "Office Fax")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfFaxOfficeCounter.Value;
            hfFaxOfficeCounter.Value = (Convert.ToInt32(hfFaxOfficeCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Factory Fax")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfFaxFactoryCounter.Value;
            hfFaxFactoryCounter.Value = (Convert.ToInt32(hfFaxFactoryCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Website")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfWebsiteCounter.Value;
            hfWebsiteCounter.Value = (Convert.ToInt32(hfWebsiteCounter.Value) + 1).ToString();
        }
        
        else if (hfFieldType.Value == "Facsimile")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfFacsimileCounter.Value;
            hfFacsimileCounter.Value = (Convert.ToInt32(hfFacsimileCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "UAN")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfUANCounter.Value;
            hfUANCounter.Value = (Convert.ToInt32(hfUANCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "NTN")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfNTNCounter.Value;
            hfNTNCounter.Value = (Convert.ToInt32(hfNTNCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "GST")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfGSTCounter.Value;
            hfGSTCounter.Value = (Convert.ToInt32(hfGSTCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Bank Account")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfBankAccountCounter.Value;
            hfBankAccountCounter.Value = (Convert.ToInt32(hfBankAccountCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "Branch Code")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfBranchCodeCounter.Value;
            hfBranchCodeCounter.Value = (Convert.ToInt32(hfBranchCodeCounter.Value) + 1).ToString();
        }

        else if (hfFieldType.Value == "Branch Address")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfBranchAddressCounter.Value;
            hfBranchAddressCounter.Value = (Convert.ToInt32(hfBranchAddressCounter.Value) + 1).ToString();
        }

        else if (hfFieldType.Value == "Swift Code")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfSwiftCodeCounter.Value;
            hfSwiftCodeCounter.Value = (Convert.ToInt32(hfSwiftCodeCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "IBAN")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfIBANCounter.Value;
            hfIBANCounter.Value = (Convert.ToInt32(hfIBANCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "RTGS")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfRTGSCounter.Value;
            hfRTGSCounter.Value = (Convert.ToInt32(hfRTGSCounter.Value) + 1).ToString();
        }
        else if (hfFieldType.Value == "PABX Cell")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfOthersCounter.Value;
            hfOthersCounter.Value = (Convert.ToInt32(hfOthersCounter.Value) + 1).ToString();
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
        Session.Add("ORGANIZATION_DETAIL_ID", Convert.ToInt32(dr["ORGANIZATION_DETAIL_ID"]));
        gvFieldEdit.DataSource = dtDetail;
        gvFieldEdit.DataBind();

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
            hfTabIndex.Value = hfEditIndex.Value;
            ltrlEdit.Text = "";
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
                if (OrgCtl.UpdateOrganization(Convert.ToInt64(Session["OrganizationID"]), txtName.Text, Convert.ToInt32(ddlType.SelectedValue), Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(Session["UserID"]), dtDetail))
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Organization", txtName.Text + " updated");

                    ltrlEdit.Text = Utility.ShowAlert(true,"Organization Updates Successfully.");
                    LoadData();
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false,"Some error occured.");
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
        dtDetail.Columns.Add("ORGANIZATION_DETAIL_ID", typeof(long));
        dtDetail.Columns.Add("FIELD_TYPE", typeof(string));
        dtDetail.Columns.Add("FIELD_VALUE", typeof(string));
        this.Session.Add("dtDetail", dtDetail);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmOrganization.aspx?LevelID=145&TopMenu=1");
    }

    #endregion

    #region Contacts
        
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfContactIndex.Value;
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtPeople"];
        DataTable dtFilter = new DataTable();
        dtFilter = dt.Clone();
        DataRow[] foundRows = null;
        if (ddlFilterFor.SelectedValue == "-1")
        {
            LoadPeople(Convert.ToInt64(Session["OrganizationID"]), null, null);
            FillRepeater();
        }
        else
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

    protected void gvFieldContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfTabIndex.Value = hfContactIndex.Value;
        if (e.CommandName == "del")
        {
            DataTable dtDetailContact = (DataTable)this.Session["dtDetailContact"];

            for (int i = 0; i < dtDetailContact.Rows.Count; i++)
            {
                if (dtDetailContact.Rows[i]["PEOPLE_DETAIL_ID"].ToString() == e.CommandArgument.ToString())
                {
                    dtDetailContact.Rows.RemoveAt(i);
                }
            }

            this.Session.Add("dtDetailContact", dtDetailContact);
            gvFieldContact.DataSource = dtDetailContact;
            gvFieldContact.DataBind();
        }
    }

    protected void gvFieldContact_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hfTabIndex.Value = hfContactIndex.Value;
    }

    protected void gvFieldContact_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            TextBox txtFIELD_TYPE = e.Row.FindControl("txtVl") as TextBox;
            Label lblFIELD_VALUE = e.Row.FindControl("lblFld2") as Label;
            ImageButton btnDelete = e.Row.FindControl("btnDelete") as ImageButton;

            if (Session["modeOrg"] != null)
            {
                TextBox lblFld = e.Row.FindControl("lblFld") as TextBox;
                if (lblFld.Text.Contains("Address"))
                {
                    hfAddCounterOrganization.Value = (Convert.ToInt32(hfAddCounterOrganization.Value) + 1).ToString();
                }
                else if (lblFld.Text.Contains("Phone"))
                {
                    hfPhoneCounterOrganization.Value = (Convert.ToInt32(hfPhoneCounterOrganization.Value) + 1).ToString();
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

    protected void btnAddFieldOrganization_Click(object sender, EventArgs e)
    {
        hfTabIndex.Value = hfContactIndex.Value;
        DataTable dtDetailContact = (DataTable)Session["dtDetailContact"];
        DataRow dr = dtDetailContact.NewRow();
        int counter = 0;
        Session["modeOrg"] = null;

        if (Session["PEOPLE_DETAIL_ID"] == null)
        {
            dr["PEOPLE_DETAIL_ID"] = counter;
        }
        else
        {
            dr["PEOPLE_DETAIL_ID"] = ((int)Session["PEOPLE_DETAIL_ID"]) + 1;
        }

        if (hfFieldTypeOrganization.Value == "Address")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfAddCounterOrganization.Value;
            hfAddCounterOrganization.Value = (Convert.ToInt32(hfAddCounterOrganization.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Phone")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfPhoneCounterOrganization.Value;
            hfPhoneCounterOrganization.Value = (Convert.ToInt32(hfPhoneCounterOrganization.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Cell")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfCellCounter.Value;
            hfCellCounter.Value = (Convert.ToInt32(hfCellCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Fax")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfFaxCounter.Value;
            hfFaxCounter.Value = (Convert.ToInt32(hfFaxCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Email")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfEmailCounter.Value;
            hfEmailCounter.Value = (Convert.ToInt32(hfEmailCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "CNIC")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfCNICCounter.Value;
            hfCNICCounter.Value = (Convert.ToInt32(hfCNICCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Citizenship")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfCitizenshipCounter.Value;
            hfCitizenshipCounter.Value = (Convert.ToInt32(hfCitizenshipCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Passport")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfPassportCounter.Value;
            hfPassportCounter.Value = (Convert.ToInt32(hfPassportCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Emergency Name")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfEmergencyNameCounter.Value;
            hfEmergencyNameCounter.Value = (Convert.ToInt32(hfEmergencyNameCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Relation")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfRelationCounter.Value;
            hfRelationCounter.Value = (Convert.ToInt32(hfRelationCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Phone Phone")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfHomePhoneCounter.Value;
            hfHomePhoneCounter.Value = (Convert.ToInt32(hfHomePhoneCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Work Phone")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfWorkPhoneCounter.Value;
            hfWorkPhoneCounter.Value = (Convert.ToInt32(hfWorkPhoneCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Home Address")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfHomeAddressCounter.Value;
            hfHomeAddressCounter.Value = (Convert.ToInt32(hfHomeAddressCounter.Value) + 1).ToString();
        }
        else if (hfFieldTypeOrganization.Value == "Work Address")
        {
            dr["FIELD_TYPE"] = hfFieldTypeOrganization.Value + " " + hfWorkAddressCounter.Value;
            hfWorkAddressCounter.Value = (Convert.ToInt32(hfWorkAddressCounter.Value) + 1).ToString();
        }
        else
        {
            dr["FIELD_TYPE"] = lblFieldOrganization.Text;
        }
        dr["FIELD_VALUE"] = txtValueOrganization.Text;
        dtDetailContact.Rows.Add(dr);
        txtValueOrganization.Text = "";

        dtDetailContact.DefaultView.Sort = "FIELD_TYPE ASC";
        Session.Add("dtDetailContact", dtDetailContact);
        Session.Add("PEOPLE_DETAIL_ID", Convert.ToInt32(dr["PEOPLE_DETAIL_ID"]));
        gvFieldContact.DataSource = dtDetailContact;
        gvFieldContact.DataBind();

    }   

    public void LoadPeople(long p_OrganizationID, string p_FNAME, string p_LNAME)
    {
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, p_OrganizationID, p_FNAME, p_LNAME);

        if (dtPeople != null)
        {
            this.Session.Add("dtPeople", dtPeople);
        }

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

    protected void rPeople_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnEdit = e.Item.FindControl("btnEdit") as LinkButton;
            LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;

            if (CanEdit("2"))
            {
                btnEdit.Visible = false;
            }
            if (CanDelete("2"))
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void rPeople_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        hfTabIndex.Value = hfContactIndex.Value;
        ltrlAddContact.Text = "";
        hfTabIndex.Value = "2";
        if (e.CommandName == "edit")
        {
            Session.Add("modeOrg", "Update");
            DataTable dtPpl = PplCtl.GetPeople(Convert.ToInt64(e.CommandArgument), Constants.LongNullValue, null, null);
            hfPeopleID.Value = e.CommandArgument.ToString();
            if (dtPpl.Rows.Count > 0)
            {
                ddlTitle.SelectedValue = dtPpl.Rows[0]["TITLE"].ToString();
                if (dtPpl.Rows[0]["TYPE_ID"].ToString() == "2")
                {
                    ddlLocation.SelectedValue = dtPpl.Rows[0]["LOCATION_ID"].ToString();
                    ddlLocation.Visible = true;
                    lblLocation.Visible = true;
                }
                cbActive.Checked = Convert.ToBoolean(dtPpl.Rows[0]["IS_ACTIVE"]);
                txtFName.Text = dtPpl.Rows[0]["FNAME"].ToString();
                txtLName.Text = dtPpl.Rows[0]["LNAME"].ToString();
                UserId = Convert.ToInt32(dtPpl.Rows[0]["ROLE_ID"]);
                txtDesignation.Text = dtPpl.Rows[0]["DESIGNATION_ID"].ToString();
                if (dtPpl.Rows[0]["DEPARTMENTID"].ToString() != "0")
                {
                    ddlDepartment.SelectedValue = dtPpl.Rows[0]["DEPARTMENTID"].ToString();
                }
                if (dtPpl.Rows[0]["DOB"].ToString() != "")
                {
                    txtDOB.Text = Convert.ToDateTime(dtPpl.Rows[0]["DOB"]).ToString("MM/dd/yyyy");
                }
                txtAccountNo.Text = dtPpl.Rows[0]["ACCOUNT_NO"].ToString();
                txtEmail.Text = dtPpl.Rows[0]["EMAIL"].ToString();
                txtPhone.Text = dtPpl.Rows[0]["PHONE"].ToString();
                
                DataTable dtDetailContact = PplCtl.GetPeopleDetail(Convert.ToInt64(e.CommandArgument));
                if (dtDetailContact != null)
                {
                    gvFieldContact.DataSource = dtDetailContact;
                    gvFieldContact.DataBind();
                    Session.Add("dtDetailContact", dtDetailContact);
                }

                btnSaveContact.Text = "Update";
            }
        }
        else if (e.CommandName == "del")
        {
            if (PplCtl.DeletePeople(Convert.ToInt64(e.CommandArgument)))
            {
                LoadPeople(Convert.ToInt64(Session["OrganizationID"]), null, null);
                FillRepeater();
                ltrlAddContact.Text = Utility.ShowAlert(true,"Contact Deleted Successfylly.");
            }
            else
            {
                ltrlAddContact.Text = Utility.ShowAlert(false,"Some error occured.");
            }
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

    private void CreateFieldDataTableConcat()
    {
        DataTable dtDetailContact = new DataTable();
        dtDetailContact.Columns.Add("PEOPLE_DETAIL_ID", typeof(long));
        dtDetailContact.Columns.Add("FIELD_TYPE", typeof(string));
        dtDetailContact.Columns.Add("FIELD_VALUE", typeof(string));
        this.Session.Add("dtDetailContact", dtDetailContact);
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveContact_Click(object sender, EventArgs e)
    {
        try
        {
            hfTabIndex.Value = hfContactIndex.Value;
            ltrlAddContact.Text = "";
            
            int LocationID = Constants.IntNullValue;
            if (Session["OrganizationID"].ToString() == "1")
            {
                LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            }

            DateTime dtDOB = Constants.DateNullValue;
            if (txtDOB.Text.Length > 0)
            {
                dtDOB = Convert.ToDateTime(txtDOB.Text);
            }

            if (txtFName.Text.Length > 0)
            {
                CreateFieldDataTableConcat();
                DataTable dtDetailContact = (DataTable)Session["dtDetailContact"];
                foreach (GridViewRow row in gvFieldContact.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtFIELD_TYPE = row.FindControl("txtVl") as TextBox;
                        TextBox lblFIELD_VALUE = row.FindControl("lblFld") as TextBox;
                        DataRow dr = dtDetailContact.NewRow();
                        dr["FIELD_TYPE"] = lblFIELD_VALUE.Text;
                        dr["FIELD_VALUE"] = txtFIELD_TYPE.Text;
                        dtDetailContact.Rows.Add(dr);
                    }
                }

                if (btnSaveContact.Text == "Save")
                {
                    long People_ID = Constants.LongNullValue;
                    
                    People_ID = PplCtl.InsertPeople(Convert.ToInt64(Session["OrganizationID"]), Convert.ToInt32(ddlTitle.SelectedValue), txtFName.Text, txtLName.Text, dtDOB, Convert.ToInt32(ddlDepartment.SelectedValue), txtEmail.Text, txtPhone.Text, Convert.ToInt32(Session["UserID"]), LocationID, txtDesignation.Text,txtAccountNo.Text, dtDetailContact);

                    if (People_ID > 0)
                    {
                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "People", "Added for " + txtName.Text);


                        ///
                        /// If Fast Services People
                        /// Save it to Distributor_User Table
                        ///                        
                        if (Session["OrganizationID"].ToString() == "1")
                        {
                            SaveNeonChemcialEmployee((int)People_ID);
                        }

                        LoadPeople(Convert.ToInt64(Session["OrganizationID"]), null, null);
                        FillRepeater();
                        ltrlAddContact.Text = Utility.ShowAlert(true,"Contact Added Successfylly.");
                        ClearContactFields();
                    }
                    else
                    {
                        ltrlAddContact.Text = Utility.ShowAlert(false,"Some error occured.");
                    }
                }
                else
                {
                    if (PplCtl.UpdatePeople(Convert.ToInt64(hfPeopleID.Value), Convert.ToInt64(Session["OrganizationID"]), Convert.ToInt32(ddlTitle.SelectedValue), txtFName.Text, txtLName.Text, dtDOB, Convert.ToInt32(ddlDepartment.SelectedValue), txtEmail.Text, txtPhone.Text, Convert.ToInt32(Session["UserID"]), LocationID, txtDesignation.Text,cbActive.Checked,txtAccountNo.Text, dtDetailContact))
                    {

                        //Save Transaction in log
                        Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "People", txtFName.Text + " " + txtLName.Text + " updated");

                        ///
                        /// If Fast Services People
                        /// Save it to Distributor_User Table
                        ///                        
                        if (Session["OrganizationID"].ToString() == "1")
                        {
                            UpdateFastEmployee(Convert.ToInt32(hfPeopleID.Value));
                        }
                        else
                        {
                            DeleteUser(Convert.ToInt32(hfPeopleID.Value));
                        }

                        LoadPeople(Convert.ToInt64(Session["OrganizationID"]), null, null);
                        FillRepeater();
                        ltrlAddContact.Text = Utility.ShowAlert(true,"Contact Updated Successfully.");
                        ClearContactFields();
                    }
                    else
                    {
                        ltrlAddContact.Text = Utility.ShowAlert(false, "Some error occured.");
                    }
                }
                
            }
            else
            {
                ltrlAddContact.Text = Utility.ShowAlert(false,"Please enter First Name.");
            }
        }
        catch (Exception ex)
        {
            ltrlAddContact.Text = Utility.ShowAlert(false,ex.Message);
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

        UController.InsertDistributor_User(int.Parse(this.Session["CompanyId"].ToString()), "", true, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
            int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", "", txtPhone.Text, strcode, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
    }

    private void UpdateFastEmployee(int People_ID)
    {
        UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), "", true, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
                    int.Parse(ddlLocation.SelectedValue.ToString()), People_ID, txtEmail.Text, "", "", "", ""
                    , txtPhone.Text, null, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
    }

    private void DeleteUser(int People_ID)
    {
        UController.UpdateDistributor_User(UserId, int.Parse(this.Session["CompanyId"].ToString()), "", false, System.DateTime.Now, System.DateTime.Now, Constants.IntNullValue,
                    Constants.IntNullValue, People_ID, txtEmail.Text, "", "", "", ""
                    , txtPhone.Text, null, txtFName.Text + " " + txtLName.Text, txtPhone.Text);
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

    private void ClearContactFields()
    {
        gvFieldContact.DataSource = null;
        gvFieldContact.DataBind();
        CreateFieldDataTableConcat();
        ddlTitle.SelectedIndex = 0;
        txtFName.Text = "";
        txtLName.Text = "";
        txtEmail.Text = "";
        txtPhone.Text = "";
        txtDOB.Text = "";
        txtAccountNo.Text = "";
        ddlDepartment.SelectedIndex = 0;
        ddlDepartment.SelectedIndex = 0;
        txtDesignation.Text = "IT Manager";
        btnSaveContact.Text = "Save";
    }
    #endregion
    
    #region Role Rights

    private void ApplyRoles()
    {
        int TabIndexes = 0;
        hfTabIndex.Value = "0";
        ltrlTabs.Text = "<ul class='TabbedPanelsTabGroup'>";
        if (!CanView("0"))
        {
            ActiveTabIndex.Value = "0";
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0'>View Organization </li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0' style='display:none'>View Organization </li>";
        }
        if (!CanView("1"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "1";
            }
            hfEditIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1'>Edit Organization</li>";
            //if (!CanEdit("1"))
            //{
            //    btnSave.Visible = false;
            //    btnCancel.Visible = false;
            //}
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1' style='display:none'>Edit Organization</li>";
        }
        if (!CanView("2"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "2";
            }
            //if (!CanEdit("2"))
            //{
            //    btnSaveContact.Visible = false;
            //    btnCancelContact.Visible = false;
            //}
            hfContactIndex.Value = TabIndexes.ToString();
            TabIndexes += 1;
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'>Contacts</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'  style='display:none'>Contacts</li>";
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