using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From To Add, Edit Organization
/// </summary>
public partial class Forms_frmCustomer : System.Web.UI.Page
{
    OrganizationController OrgCtl = new OrganizationController();

    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (CanView(Request.QueryString["LevelID"].ToString()))
            {
                ApplyRoles();
                this.LoadCountry();
                this.LoadCity();

                if (Request.QueryString["mode"] != null)
                {
                    pnlAddOrganization.Visible = true;
                    pnlOrganizationList.Visible = false;
                }
                else
                {
                    LoadOrganization(0);
                    FillRepeater();
                }

                CreateFieldDataTable();
                Session["ORGANIZATION_DETAIL_ID"] = null;
                Session["mode"] = null;
                btnSave.Attributes.Add("onclick", "return ValidateForm()");
            }
            else
            {
                pnlAddOrganization.Visible = false;
                pnlOrganizationList.Visible = false;
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
                long OrganizationID = OrgCtl.InsertOrganization(txtName.Text,0, Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(Session["UserID"]), dtDetail);
                if (OrganizationID != Constants.LongNullValue)
                {
                    this.Session.Add("OrganizationID", OrganizationID);
                    Response.Redirect("frmOrganizationDetail.aspx?TopMenu=1&LevelID=145", false);
                }
                else
                {
                    lblError.Text = "Some error occured";
                }
            }
            else
            {
                if (OrgCtl.UpdateOrganization(Convert.ToInt64(hfOrganizationID.Value), txtName.Text, 0, Convert.ToInt32(ddlCity.SelectedValue), Convert.ToInt32(Session["UserID"]), dtDetail))
                {
                    Response.Redirect("frmCustomer.aspx?LevelID=145&TopMenu=1");
                }
                else
                {
                    lblError.Text = "Some error occured";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void FillRepeater()
    {
        rOrganization.DataSource = null;
        rOrganization.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtOrganization"];

        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rOrganization.DataSource = PagedResults;
            rOrganization.DataBind();

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

    public void LoadOrganization(int p_Type)
    {
        DataTable dtOrganization = OrgCtl.GetOrganization(Constants.LongNullValue, p_Type);

        if (dtOrganization != null)
        {
            this.Session.Add("dtOrganization", dtOrganization);
        }

    }

    protected void rOrganization_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            this.Session.Add("OrganizationID", e.CommandArgument.ToString());
            Response.Redirect("frmOrganizationDetail.aspx?LevelID=171&LevelType=1", false);
        }
        else if (e.CommandName == "del")
        {
            if (OrgCtl.DeleteOrganization(Convert.ToInt64(e.CommandArgument)))
            {
                Response.Redirect("frmOrganization.aspx?LevelID=145&TopMenu=1");
            }
            else
            {
                lblError.Text = "Some error occured";
            }
        }
    }

    protected void rOrganization_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
            LinkButton btnView = e.Item.FindControl("btnView") as LinkButton;
            LinkButton btnOrg = e.Item.FindControl("btnOrg") as LinkButton;
            HiddenField hfOrganization = e.Item.FindControl("hfOrganization") as HiddenField;

            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                btnView.Visible = false;
                btnOrg.Enabled = false;
            }

            if (CanDelete(Request.QueryString["LevelID"].ToString()))
            {
                btnDelete.Visible = false;
            }

            if (hfOrganization.Value == "1")
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmOrganization.aspx?LevelID=145&TopMenu=1");
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

    protected void btnAddField_Click(object sender, EventArgs e)
    {
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
        else if (hfFieldType.Value == "Website")
        {
            dr["FIELD_TYPE"] = hfFieldType.Value + " " + hfWebsiteCounter.Value;
            hfWebsiteCounter.Value = (Convert.ToInt32(hfWebsiteCounter.Value) + 1).ToString();
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
        else if (hfFieldType.Value == "Branch ode")
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
        gvField.DataSource = dtDetail;
        gvField.DataBind();

    }

    private void CreateFieldDataTable()
    {
        DataTable dtDetail = new DataTable();
        dtDetail.Columns.Add("ORGANIZATION_DETAIL_ID", typeof(long));
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
                if (dtDetail.Rows[i]["ORGANIZATION_DETAIL_ID"].ToString() == e.CommandArgument.ToString())
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
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCity();
    }

    private void LoadCountry()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue, null, null, true, Constants.IntNullValue, Constants.Region, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlCountry, dt, 0, 4, true);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dtOrganization = OrgCtl.GetOrganization(Constants.LongNullValue, 0, txtFilter.Text.Trim());

        if (dtOrganization != null)
        {
            this.Session.Add("dtOrganization", dtOrganization);
            FillRepeater();
        }
    }

    protected void btnOrganizationSor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtOrganization"];
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
        Session.Add("dtOrganization", dt);
        FillRepeater();

    }

    protected void btnCountrySor_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtOrganization"];
        DataView dv = new DataView(dt);

        if (Session["Country"] != null)
        {
            if (Session["Country"].ToString() == "DESC")
            {
                dv.Sort = "CountryName DESC";
                Session.Add("Country", "ASC");
            }
            else
            {
                dv.Sort = "CountryName ASC";
                Session.Add("Country", "DESC");
            }
        }
        else
        {
            dv.Sort = "CountryName ASC";
            Session.Add("Country", "DESC");
        }
        dt = dv.ToTable();
        Session.Add("dtOrganization", dt);
        FillRepeater();

    }

    protected void btnNewCustomer_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmCustomer.aspx?LevelID=6&LevelType=1&TopMenu=1&mode=new");
    }

    #region Role Rights

    private bool CanView(string mModuleID)
    {
        DataTable dtRights = (DataTable)Session["dtRights"];
        bool flag = false;
        if (dtRights != null)
        {
            foreach (DataRow dr in dtRights.Rows)
            {
                if (dr["MODULE_ID"].ToString() == mModuleID)
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

    private void ApplyRoles()
    {
        if (CanEdit(Request.QueryString["LevelID"].ToString()))
        {
            btnNewCustomer.Visible = false;
        }
    }
    #endregion
  
}