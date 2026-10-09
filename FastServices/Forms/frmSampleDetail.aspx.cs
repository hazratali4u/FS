using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections;
using System.Configuration;
using System.Text;

public partial class Forms_frmSampleDetail : System.Web.UI.Page
{
    EnquiryController EnqCtl = new EnquiryController();
    EnquiryPriceController EnqPriceCtl = new EnquiryPriceController();
    QuotationController QoutCtl = new QuotationController();
    OrganizationController OrgCtl = new OrganizationController();
    RoleManagementController RoleCtl = new RoleManagementController();
    DataTable dtQuotationDetail;
    DataTable dtEnquiryDetail;
    DataTable dtEmailCc;
    private static int RowNo;
    DataControl dc = new DataControl();
    DataTable PurchaseSKUIssue;

    string strBody = string.Empty;
    string strSubject = string.Empty;
    string strEmailFrom = ConfigurationManager.AppSettings["EmailAccount"].ToString();
    string strSMTP = ConfigurationManager.AppSettings["SMTP"].ToString();
    string strSMTPPassword = ConfigurationManager.AppSettings["AccountPassword"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtTabRights = RoleCtl.GetRoleFormTab(Convert.ToInt32(Request.QueryString["LevelID"]), Convert.ToInt32(Session["RoleID"]));
            this.Session.Add("dtTabRights", dtTabRights);

            ApplyRoles();


            //Populate Edit Tab
            DataTable dtEnquiry = EnqCtl.GetEnquiryMaster(Constants.IntNullValue, Convert.ToInt64(Session["EnquiryMasterID"]), Constants.IntNullValue, Constants.EnquiryCustomer);
            hfEnquiryID.Value = Session["EnquiryMasterID"].ToString();
            hfCustomerID.Value = Session["EnquiryMasterID"].ToString();
            lblEnquiry.Text = Session["EnquiryMasterID"].ToString();
            
            if (dtEnquiry.Rows.Count > 0)
            {
                DataTable dtPriceEnq = this.GetEnquiry(Convert.ToInt64(Session["EnquiryMasterID"]), Constants.IntNullValue, Constants.IntNullValue, Constants.EnquiryCustomer);

                gvMain.DataSource = dtPriceEnq;
                gvMain.DataBind();
            }

            //Edit Tab
            lblDocumentNo.Text = Session["EnquiryMasterID"].ToString();
            lblDocumentNoView.Text = Session["EnquiryMasterID"].ToString();
            this.CreatTable();
            this.LoadDistributor();
            this.LoadCustomer();
            this.LoadCity();
            this.LoadUOM();
            this.LoadDocumentDetail();
            this.LoadSKUDetail();     
       
            //Approve Sample
            LoadApproveProduct();

            //Issue Sample
            this.GetDocumentNoIssue();
            this.LoadBatchNoIssue();
            this.CreatTableIssue();
            this.LoadGirdIssue();

            btnSaveEnquiry.Attributes.Add("onclick", "return ValidateForm();");
            btnSaveIssue.Attributes.Add("onclick", "return ValidateFormIssue();");
            txtLeadTime.Text = Convert.ToDateTime(Session["CurrentWorkDate"]).ToString("dd-MMM-yyyy");
        }
    }

    public DataTable GetEnquiry(long p_ENQUIREY_MASTER_ID, int p_DISTRIBUTOR_ID, int p_USER_ID, int p_TYPE_ID)
    {
        DataTable dtEnqPrice = EnqPriceCtl.GetEnquiryView(p_ENQUIREY_MASTER_ID, p_DISTRIBUTOR_ID, p_USER_ID, p_TYPE_ID,Constants.IntNullValue,0);
        this.Session.Add("dtEnqPrice", dtEnqPrice);
        return dtEnqPrice;
    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvSupplier = (GridView)e.Row.FindControl("gvSupplier");
            {
                int a = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dtSuppler = GetAssignSupplier(a);
                if (dtSuppler != null)
                {
                    gvSupplier.DataSource = dtSuppler;
                    gvSupplier.DataBind();
                }
            }
        }
    }

    /// <summary>
    /// Loads Assigned Supplier To Product To Assigned Supplier Combo
    /// </summary>
    private DataTable GetAssignSupplier(int ProductID)
    {
        DataTable dt = EnqPriceCtl.GetProductSupplier(ProductID, Convert.ToInt64(hfEnquiryID.Value));

        return dt;
    }

    protected void gvMainEmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridView gvSupplier = (GridView)e.Row.FindControl("gvSupplier");
            {
                int a = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
                DataTable dtSuppler = GetAssignSupplier(a);
                if (dtSuppler != null)
                {
                    gvSupplier.DataSource = dtSuppler;
                    gvSupplier.DataBind();
                }
            }
        }
    }

    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBoxList cblEmailID = e.Row.FindControl("cblEmailID") as CheckBoxList;
            int SupplierID = int.Parse(((GridView)sender).DataKeys[e.Row.RowIndex].Value.ToString());
            clsWebFormUtil.FillListBox(cblEmailID, GetEmailID(SupplierID), "EMAIL2", "EmailName", true);

            DataTable dt = EnqCtl.GetEmailReciepant(Convert.ToInt64(hfEnquiryID.Value), int.Parse(e.Row.Cells[7].Text));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < cblEmailID.Items.Count; i++)
                    {
                        if (dr["RECEIPIENT_EMAIL"].ToString() == cblEmailID.Items[i].Value.Split(',')[1].ToString())
                        {
                            cblEmailID.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Save Or Updates a Customer
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        ltrlEmail.Text = "";
        if (Session["EmailAccount"] != null)
        {
            if (Session["EmailAccount"].ToString() != "")
            {
                strEmailFrom = Session["EmailAccount"].ToString();
                strSMTPPassword = Session["AccountPassword"].ToString();
                strSMTP = Session["SMTP"].ToString();
            }
        }

        if (strEmailFrom == "" || strSMTP == "" || strSMTPPassword == "")
        {
            ltrlEmail.Text = Utility.ShowAlert(false, "Error in email configuration. Please contact to administrator.");
            return;
        }

        try
        {
            if (gvMain.Rows.Count > 0)
            {
                ArrayList alSupplierIDs = SupplierIDs();
                dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
                dtEmailCc = (DataTable)Session["dtEmailCc"];

                if (alSupplierIDs.Count > 0)
                {
                    if (dtEmailCc.Rows.Count > 0)
                    {
                        bool flag = false;

                        for (int i = 0; i < alSupplierIDs.Count; i++)
                        {
                            DataTable dtQuotation = new DataTable();
                            dtQuotation = dtQuotationDetail.Clone();
                            DataRow[] foundRows = dtQuotationDetail.Select("ORGANIZATION_ID  = '" + alSupplierIDs[i].ToString() + "'");
                            if (foundRows.Length > 0)
                            {
                                for (int j = 0; j < foundRows.Length; j++)
                                {
                                    dtQuotation.ImportRow(foundRows[j]);
                                }
                            }

                            DataTable dtEmail = new DataTable();
                            dtEmail = dtEmailCc.Clone();
                            DataRow[] foundRows2 = dtEmailCc.Select("ORGANIZATION_ID  = '" + alSupplierIDs[i].ToString() + "'");
                            if (foundRows2.Length > 0)
                            {
                                for (int k = 0; k < foundRows2.Length; k++)
                                {
                                    dtEmail.ImportRow(foundRows2[k]);
                                }
                            }

                            DataView view = new DataView(dtEmail);
                            DataTable dtTo = view.ToTable(true, "ENQUIREY_DETAIL_ID", "ORGANIZATION_ID", "PEOPLE_ID", "PRODUCT_ID", "EMAIL");

                            DataView viewCc = new DataView(dtQuotation);
                            DataTable dtCc = viewCc.ToTable(true, "EmailCc", "ORGANIZATION_ID", "PRODUCT_ID");

                            string[] strEmailcc = dtCc.Rows[0]["EmailCc"].ToString().Split(',');

                            if (SendEmail(dtTo, strEmailcc, dtQuotation))
                            {
                                flag = QoutCtl.InsertQuotation(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Convert.ToInt32(hfCustomerID.Value), Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(Session["UserID"]), dtQuotation);

                                foreach (DataRow drTo in dtTo.Rows)
                                {
                                    flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(drTo["PRODUCT_ID"]), Constants.ReceipientSupplier, Convert.ToInt64(drTo["PEOPLE_ID"]), drTo["EMAIL"].ToString(), Constants.EmailTypeTo, Convert.ToInt32(Session["UserID"]));
                                }

                                foreach (DataRow drcc in dtCc.Rows)
                                {
                                    flag = QoutCtl.InsertQuotationReceipient(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(drcc["PRODUCT_ID"]), Constants.ReceipientSupplier, Constants.LongNullValue, drcc["EmailCc"].ToString(), Constants.EmailTypeCc, Convert.ToInt32(Session["UserID"]));
                                }

                                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.EmailSent, true);
                            }
                        }
                        if (flag)
                        {
                            ltrlEmail.Text = Utility.ShowAlert(true, "Email sent successfully.");
                        }
                        else
                        {
                            ltrlEmail.Text = Utility.ShowAlert(false, "Some error Occured. Email not sent.");
                        }
                    }
                    else
                    {
                        ltrlEmail.Text = Utility.ShowAlert(false, "No Email ID Selected.");
                    }
                }
                else
                {
                    ltrlEmail.Text = Utility.ShowAlert(false, "No Supplier Selected.");
                }
            }
        }
        catch (Exception ex)
        {
            ltrlEmail.Text = Utility.ShowAlert(false, ex.Message);
        }
    }

    private bool SendEmail(DataTable dtEmailTo, string[] EmailCc, DataTable dtQuotation)
    {
        bool flag = false;
        StringBuilder strProductDetail = new StringBuilder();

        if (dtEmailTo.Rows.Count > 0)
        {
            strProductDetail.Append("<table width='80%' border='1'>");
            strProductDetail.Append("<tr>");
            strProductDetail.Append("<td style='width=50%'>");
            strProductDetail.Append("<strong>PRODUCT</strong>");
            strProductDetail.Append("</td>");
            strProductDetail.Append("<td style='width=50%'>");
            strProductDetail.Append("<strong>Quantity</strong>");
            strProductDetail.Append("</td>");
            strProductDetail.Append("</tr>");
            foreach (DataRow dr in dtQuotation.Rows)
            {
                strProductDetail.Append("<tr>");
                strProductDetail.Append("<td style='width=50%'>");
                strProductDetail.Append(dr["PRODUCT"].ToString());
                strProductDetail.Append("</td>");
                strProductDetail.Append("<td style='width=50%'>");
                strProductDetail.Append(dr["QUANTITY"].ToString());
                strProductDetail.Append("</td>");
                strProductDetail.Append("</tr>");
            }
            strProductDetail.Append("</table>");

            DataTable dtEmailTemplate = QoutCtl.GetEmailTemplate(Constants.IntNullValue, 0,1);

            if (dtEmailTemplate.Rows.Count > 0)
            {
                strSubject = dtEmailTemplate.Rows[0]["SUBJECT"].ToString();

                strBody += "<br>";
                strBody = dtEmailTemplate.Rows[0]["BODY"].ToString();
                strBody += "<br>";
                strBody += strProductDetail.ToString();
                strBody += "<br>";
                strBody += dtEmailTemplate.Rows[0]["SIGNATURE"].ToString();
            }

            if (Utility.SendEmail(strEmailFrom, dtEmailTo, EmailCc, strSMTP, strSMTPPassword, strSubject, strBody))
            {
                flag = true;
            }
            else
            {
                ltrlEmail.Text = Utility.ShowAlert(false, "While sending email some error occured.");
            }
        }

        return flag;
    }

    private DataTable GetEmailID(int SupplierID)
    {
        PeopleController PplCtl = new PeopleController();
        DataTable dtPeople = PplCtl.GetPeople(Constants.LongNullValue, SupplierID, null, null);
        return dtPeople;
    }

    private ArrayList SupplierIDs()
    {
        ArrayList alSupplierIDs = new ArrayList();
        CreateQuotationTable();
        dtQuotationDetail = (DataTable)Session["dtQuotationDetail"];
        CreateEmailCcTable();
        dtEmailCc = (DataTable)Session["dtEmailCc"];
        foreach (GridViewRow gvr in gvMain.Rows)
        {
            GridView gvSupplier = (GridView)gvr.FindControl("gvSupplier");
            foreach (GridViewRow gvrSupp in gvSupplier.Rows)
            {
                CheckBox cbQuotation = gvrSupp.FindControl("cbQuotation") as CheckBox;
                TextBox txtEmailCc = gvrSupp.FindControl("txtEmailCc") as TextBox;
                Label lblSKU = gvr.FindControl("lblSKU") as Label;
                if (cbQuotation.Checked)
                {
                    DataRow dr = dtQuotationDetail.NewRow();
                    dr["ENQUIREY_DETAIL_ID"] = gvrSupp.Cells[0].Text;
                    dr["ORGANIZATION_ID"] = gvrSupp.Cells[1].Text;
                    dr["PRODUCT_ID"] = gvr.Cells[0].Text;
                    dr["PRODUCT"] = lblSKU.Text;
                    dr["QUANTITY"] = gvr.Cells[1].Text;
                    dr["EmailCc"] = txtEmailCc.Text;
                    dtQuotationDetail.Rows.Add(dr);

                    if (!alSupplierIDs.Contains(gvrSupp.Cells[1].Text))
                    {
                        alSupplierIDs.Add(gvrSupp.Cells[1].Text);
                    }

                    CheckBoxList cblEmailID = gvrSupp.FindControl("cblEmailID") as CheckBoxList;

                    for (int i = 0; i < cblEmailID.Items.Count; i++)
                    {
                        if (cblEmailID.Items[i].Selected)
                        {
                            if (cblEmailID.Items[i].Value != "")
                            {
                                DataRow drCc = dtEmailCc.NewRow();
                                drCc["ENQUIREY_DETAIL_ID"] = hfEnquiryID.Value;
                                drCc["ORGANIZATION_ID"] = gvrSupp.Cells[1].Text;
                                string[] str = cblEmailID.Items[i].Value.Split(',');
                                drCc["EMAIL"] = str[1].ToString();
                                drCc["PEOPLE_ID"] = str[0].ToString();
                                drCc["PRODUCT_ID"] = gvr.Cells[0].Text;
                                dtEmailCc.Rows.Add(drCc);
                            }
                        }
                    }


                }
            }
        }
        Session["dtQuotationDetail"] = dtQuotationDetail;
        Session.Add("dtEmailCc", dtEmailCc);
        return alSupplierIDs;
    }

    private void CreateQuotationTable()
    {
        dtQuotationDetail = new DataTable();
        dtQuotationDetail.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtQuotationDetail.Columns.Add("ORGANIZATION_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT_ID", typeof(int));
        dtQuotationDetail.Columns.Add("PRODUCT", typeof(string));
        dtQuotationDetail.Columns.Add("EmailCc", typeof(string));
        dtQuotationDetail.Columns.Add("QUANTITY", typeof(int));
        this.Session.Add("dtQuotationDetail", dtQuotationDetail);
    }

    private void CreateEmailCcTable()
    {
        dtEmailCc = new DataTable();
        dtEmailCc.Columns.Add("ENQUIREY_DETAIL_ID", typeof(long));
        dtEmailCc.Columns.Add("ORGANIZATION_ID", typeof(int));
        dtEmailCc.Columns.Add("PEOPLE_ID", typeof(int));
        dtEmailCc.Columns.Add("PRODUCT_ID", typeof(int));
        dtEmailCc.Columns.Add("EMAIL", typeof(string));
        this.Session.Add("dtEmailCc", dtEmailCc);
    }

    #region Edit Tab

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTable()
    {
        dtEnquiryDetail = new DataTable();
        dtEnquiryDetail.Columns.Add("SKU_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SKU_Name", typeof(string));
        dtEnquiryDetail.Columns.Add("Quantity", typeof(int));
        dtEnquiryDetail.Columns.Add("UOM_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("UOM", typeof(string));
        dtEnquiryDetail.Columns.Add("PaymentMode", typeof(string));
        dtEnquiryDetail.Columns.Add("Shipment", typeof(string));
        dtEnquiryDetail.Columns.Add("ShipmentMode", typeof(string));
        dtEnquiryDetail.Columns.Add("DelLocation", typeof(string));
        dtEnquiryDetail.Columns.Add("LEAD_TIME", typeof(string));
        dtEnquiryDetail.Columns.Add("PAYMENT_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SHIPMENT_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("SHIPMENTMODE_ID", typeof(int));
        dtEnquiryDetail.Columns.Add("DELIVERYLOCATION_ID", typeof(int));

        this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);

    }

    /// <summary>
    /// Loads UOMs To UOM Comb
    /// </summary>
    private void LoadUOM()
    {
        SLASHCodesController CodesCtl = new SLASHCodesController();
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.UOM, null, 1, true);
        ddlUOM.Items.Add(new clsListItems("--Select--", "-1"));
        clsWebFormUtil.FillDropDownList(this.ddlUOM, m_dt, 0, 2);
    }

    /// <summary>
    /// Loads Customer To Customer Comb
    /// </summary>
    private void LoadCustomer()
    {
        DataTable m_dt = OrgCtl.GetOrganization(Constants.LongNullValue, 0);
        clsWebFormUtil.FillDropDownList(this.DrpCustomer, m_dt, 0, 1);
        clsWebFormUtil.FillDropDownList(this.DrpCustomerSample, m_dt, 0, 1);
        clsWebFormUtil.FillDropDownList(this.DrpCustomerIssue, m_dt, 0, 1);
    }

    /// <summary>
    /// Loads Cities To City Comb
    /// </summary>
    private void LoadCity()
    {
        GeoHierarchyController GControler = new GeoHierarchyController();
        DataTable dt = GControler.SelectGeoHierarchy(Constants.IntNullValue, Constants.IntNullValue, 949, null, null, true, Constants.IntNullValue, Constants.Zone, Constants.IntNullValue, Constants.DateNullValue, Constants.DateNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        ddlDelivery.Items.Add(new clsListItems("--Select--", "-1"));
        clsWebFormUtil.FillDropDownList(this.ddlDelivery, dt, 0, 4, true);
    }

    /// <summary>
    /// Loads Locations To Location From Combo
    /// </summary>
    private void LoadDistributor()
    {
        DistributorController DController = new DistributorController();
        DataTable dt = DController.SelectDistributorInfo(Constants.IntNullValue, int.Parse(this.Session["UserId"].ToString()), int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillDropDownList(this.ddlLocation, dt, 0, 2, true);
        clsWebFormUtil.FillDropDownList(this.drpDistributor, dt, 0, 2, true);
        clsWebFormUtil.FillDropDownList(this.drpDistributorIssue, dt, 0, 2, true);
    }

    /// <summary>
    /// Loads Product Prices
    /// </summary>
    private void LoadSKUDetail()
    {
        SkuController mSKUController = new SkuController();
        DataTable Dtsku_Price = mSKUController.SelectSkuInfo(Constants.IntNullValue, 1, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()));
        clsWebFormUtil.FillListBox(lstCode, Dtsku_Price, 0, 2, true);

        this.Session.Add("Dtsku_Price", Dtsku_Price);

        DataTable Dtsku_Price2 = mSKUController.SelectSkuInfoNew(Constants.IntNullValue, 1, Constants.IntNullValue, Constants.IntNullValue, int.Parse(this.Session["CompanyId"].ToString()), Convert.ToInt32(hfEnquiryID.Value));
        clsWebFormUtil.FillListBox(lstCodeIssue, Dtsku_Price2, 0, 2, true);
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadEditGird()
    {
        int TotalValue = 0;
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        gvMainEdit.DataSource = dtEnquiryDetail;
        gvMainEdit.DataBind();

        gvMainView.DataSource = dtEnquiryDetail;
        gvMainView.DataBind();

        foreach (DataRow dr in dtEnquiryDetail.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());

        }
        txtTotalQuantity.Text = TotalValue.ToString();
        txtTotalQty.Text = TotalValue.ToString();
    }

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveEnquiry_Click(object sender, EventArgs e)
    {
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        if (foundRows.Length > 0 && int.Parse(dc.chkNull_0(txtQuantity.Text)) > 0)
        {
            if (btnSaveEnquiry.Text == "Add Product")
            {
                if (CheckDublicateSKU())
                {
                    DataRow dr = dtEnquiryDetail.NewRow();
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantity.Text));
                    dr["UOM"] = ddlUOM.SelectedItem.Text;
                    dr["UOM_ID"] = Convert.ToInt32(ddlUOM.SelectedValue);
                    dr["PaymentMode"] = 0;
                    dr["PAYMENT_ID"] = 0;

                    dr["Shipment"] = 0;
                    dr["SHIPMENT_ID"] = 0;

                    dr["ShipmentMode"] = 0;
                    dr["SHIPMENTMODE_ID"] = 0;

                    dr["DelLocation"] = ddlDelivery.SelectedItem.Text;
                    dr["DELIVERYLOCATION_ID"] = Convert.ToInt32(ddlDelivery.SelectedValue);

                    dr["LEAD_TIME"] = txtLeadTime.Text;

                    dtEnquiryDetail.Rows.Add(dr);
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false, "Already Exists.");
                    txtskuName.Text = foundRows[0]["SKU_NAME"].ToString();
                    return;
                }
            }
            else if (btnSaveEnquiry.Text == "Update Product")
            {
                DataRow dr = dtEnquiryDetail.Rows[RowNo];
                dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                dr["Quantity"] = int.Parse(txtQuantity.Text);
                dr["UOM"] = ddlUOM.SelectedItem.Text;
                dr["UOM_ID"] = Convert.ToInt32(ddlUOM.SelectedValue);
                dr["PaymentMode"] = 0;
                dr["PAYMENT_ID"] = 0;

                dr["Shipment"] = 0;
                dr["SHIPMENT_ID"] = 0;

                dr["ShipmentMode"] = 0;
                dr["SHIPMENTMODE_ID"] = 0;

                dr["DelLocation"] = ddlDelivery.SelectedItem.Text;
                dr["DELIVERYLOCATION_ID"] = Convert.ToInt32(ddlDelivery.SelectedValue);

                dr["LEAD_TIME"] = txtLeadTime.Text;
            }

            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.LoadEditGird();
            this.txtskuName.Text = "";
            this.txtQuantity.Text = "";
            this.btnSaveEnquiry.Text = "Add Product";
            ScriptManager.GetCurrent(Page).SetFocus(txtskuName);
        }
        else
        {
            ltrlEdit.Text = Utility.ShowAlert(false, "Wrong Product or Quantity Selected.");
        }
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKU()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        DataRow[] foundRows = dtEnquiryDetail.Select("SKU_ID  = '" + hfSKUID.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMainEdit_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        txtskuName.Text = gvMainEdit.Rows[e.NewEditIndex].Cells[1].Text;
        txtQuantity.Text = gvMainEdit.Rows[e.NewEditIndex].Cells[2].Text;
        ddlDelivery.SelectedValue = gvMainEdit.Rows[e.NewEditIndex].Cells[11].Text;
        ddlUOM.SelectedValue = gvMainEdit.Rows[e.NewEditIndex].Cells[12].Text;
        txtLeadTime.Text = gvMainEdit.Rows[e.NewEditIndex].Cells[5].Text;
        hfSKUID.Value = gvMainEdit.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantity.Focus();
        btnSaveEnquiry.Text = "Update Product";

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvMainEdit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        if (dtEnquiryDetail.Rows.Count > 0)
        {
            dtEnquiryDetail.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            this.LoadEditGird();
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocument_Click(object sender, EventArgs e)
    {
        dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];

        if (dtEnquiryDetail.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(ddlLocation.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (SaveEnquiry(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    ltrlEdit.Text = Utility.ShowAlert(true, "Enquiry Updated Successfuly.");
                    LoadDocumentDetail();
                }
                else
                {
                    ltrlEdit.Text = Utility.ShowAlert(false, "Some error occured. Enquiry not updated.");
                }
            }
            else
            {
                ltrlEdit.Text = Utility.ShowAlert(false, "Wrong Location");
            }
        }
        else
        {
            ltrlEdit.Text = Utility.ShowAlert(false, "At least one Product must enter");
        }
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool SaveEnquiry(DateTime MWorkDate)
    {
        DataTable dtEnquiryDetail = (DataTable)this.Session["dtEnquiryDetail"];
        bool mResult = EnqCtl.UpdateEnquiry(Constants.EnquiryCustomer, Convert.ToInt32(hfEnquiryID.Value), Convert.ToInt32(ddlLocation.SelectedValue), MWorkDate, Convert.ToInt32(DrpCustomer.SelectedValue), txtRemarks.Text, Convert.ToInt32(this.Session["UserId"]), Constants.Modified,Constants.LongNullValue, dtEnquiryDetail);
        return mResult;
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetail()
    {
        DateTime MWorkDate = System.DateTime.Now;
        EnquiryController mEnq = new EnquiryController();
        DataTable dt = mEnq.GetEnquiryMaster(Constants.IntNullValue, Convert.ToInt64(hfEnquiryID.Value), Constants.IntNullValue, Constants.EnquiryCustomer);
        if (dt.Rows.Count > 0)
        {
            ddlLocation.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomer.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();
            txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();

            drpDistributor.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomerSample.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();

            lblCustomer.Text = DrpCustomerIssue.SelectedItem.Text;

            drpDistributorIssue.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomerIssue.SelectedValue = dt.Rows[0]["CUSTOMER_ID"].ToString();

            lblLocation.Text = ddlLocation.SelectedItem.Text;
            lblCustomerView.Text = DrpCustomer.SelectedItem.Text;
            lblRemarksView.Text = dt.Rows[0]["REMARKS"].ToString();

            dtEnquiryDetail = mEnq.GetEnquiryDetail(long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("dtEnquiryDetail", dtEnquiryDetail);
            LoadEditGird();
        }
    }

    #endregion

    #region Approve Sample

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
                flag = EnqCtl.InsertCustomerProductApproval(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomerSample.SelectedValue), Convert.ToInt32(gvr.Cells[0].Text), Convert.ToInt32(gvr.Cells[1].Text), Convert.ToInt32(Session["UserID"]), IsApprove);
            }
            if (flag)
            {
                EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.IntNullValue, true);
                ltrlApprove.Text = Utility.ShowAlert(true, "Selected Products Approved.");
            }
            else
            {
                ltrlApprove.Text = Utility.ShowAlert(false, "Some error occured.");
            }
        }
    }

    private void LoadApproveProduct()
    {
        DataTable dtQuotation = QoutCtl.GetApprovalProductsNew(Convert.ToInt32(drpDistributor.SelectedValue), Convert.ToInt32(DrpCustomerSample.SelectedValue), Convert.ToInt64(hfEnquiryID.Value));
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

    #endregion

    #region Issue Sample

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void drpDocumentNoIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drpDocumentNoIssue.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            this.CreatTableIssue();
            this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);
            LoadGirdIssue();
            this.ClearAllIssue();
        }
        else
        {
            drpDistributor.Enabled = false;
            this.LoadDocumentDetailIssue();
            this.LoadSKUDetail();
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadDocumentDetailIssue()
    {
        DateTime MWorkDate = System.DateTime.Now;
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(Constants.IntNullValue, Constants.IntNullValue, long.Parse(drpDocumentNoIssue.SelectedValue.ToString()), Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            drpDistributorIssue.SelectedValue = dt.Rows[0]["DISTRIBUTOR_ID"].ToString();
            DrpCustomerIssue.SelectedValue = dt.Rows[0]["SOLD_TO"].ToString();

            PurchaseSKUIssue = mPurchase.SelectPurchaseDetail(Constants.IntNullValue, long.Parse(dt.Rows[0][0].ToString()));
            this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);
            LoadGirdIssue();
        }
    }

    /// <summary>
    /// Creats Datatable For Document
    /// </summary>
    private void CreatTableIssue()
    {
        PurchaseSKUIssue = new DataTable();
        PurchaseSKUIssue.Columns.Add("PURCHASE_DETAIL_ID", typeof(long));
        PurchaseSKUIssue.Columns.Add("SKU_ID", typeof(int));
        PurchaseSKUIssue.Columns.Add("SKU_Name", typeof(string));
        PurchaseSKUIssue.Columns.Add("BATCH_NO", typeof(string));
        PurchaseSKUIssue.Columns.Add("Quantity", typeof(int));
        this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);

    }

    /// <summary>
    /// Gets Document Nos For Purchase, TranferOut, Purchase Return, TranferIn And Damage
    /// </summary>
    private void GetDocumentNoIssue()
    {
        drpDocumentNoIssue.Items.Clear();
        PurchaseController mPurchase = new PurchaseController();
        DataTable dt = mPurchase.SelectPurchaseDocumentNo(2, Constants.IntNullValue, Constants.LongNullValue, int.Parse(this.Session["UserId"].ToString()), Convert.ToInt32(hfEnquiryID.Value));
        drpDocumentNoIssue.Items.Add(new clsListItems("New", Constants.LongNullValue.ToString()));
        clsWebFormUtil.FillDropDownList(this.drpDocumentNoIssue, dt, 0, 0);
    }

    /// <summary>
    /// Clears Form Controls
    /// </summary>
    private void ClearAllIssue()
    {
        txtskuNameIssue.Text = "";
        txtQuantityIssue.Text = "";
        btnSaveIssue.Text = "Issue Sample";
        drpDistributorIssue.Enabled = true;
    }

    /// <summary>
    /// Adds Document Detail To Document Detail Grid
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveIssue_Click(object sender, EventArgs e)
    {
        if (hfBatchNo.Value.Length > 0)
        {
            DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
            PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
            DataRow[] foundRows = Dtsku_Price.Select("SKU_ID  = '" + hfSKUIDIssue.Value + "'");
            if (foundRows.Length > 0 && int.Parse(dc.chkNull_0(txtQuantityIssue.Text)) > 0)
            {
                if (btnSaveIssue.Text == "Issue Sample")
                {
                    if (CheckDublicateSKUIssue())
                    {
                        DataRow dr = PurchaseSKUIssue.NewRow();
                        dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                        dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                        dr["BATCH_NO"] = hfBatchNo.Value;
                        dr["Quantity"] = int.Parse(dc.chkNull_0(txtQuantityIssue.Text));
                        PurchaseSKUIssue.Rows.Add(dr);
                    }
                    else
                    {
                        this.LoadBatchComb();
                        ltrlIssue.Text = Utility.ShowAlert(false, "Already Exists");
                        txtskuNameIssue.Text = foundRows[0]["SKU_NAME"].ToString();
                        return;
                    }
                }
                else if (btnSaveIssue.Text == "Update Sample")
                {
                    DataRow dr = PurchaseSKUIssue.Rows[RowNo];
                    dr["SKU_ID"] = foundRows[0]["SKU_ID"];
                    dr["SKU_Name"] = foundRows[0]["SKU_NAME"];
                    dr["BATCH_NO"] = hfBatchNo.Value;
                    dr["Quantity"] = int.Parse(txtQuantityIssue.Text);
                }
                this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);
                this.ClearAllIssue();
                this.LoadGirdIssue();
                ScriptManager.GetCurrent(Page).SetFocus(txtskuNameIssue);
            }
            else
            {
                ltrlIssue.Text = Utility.ShowAlert(false, "Wrong Product or Quantity Selected");
            }
        }
        else
        {
            ltrlIssue.Text = Utility.ShowAlert(false, "No Batch No Selected");
        }
    }

    private void LoadBatchComb()
    {
        ddlBatch.Items.Clear();
        for (int i = 0; i < lbBatch.Items.Count; i++)
        {
            if (lbBatch.Items[i].Value == hfSKUIDIssue.Value)
            {
                ListItem li = new ListItem();
                li.Value = lbBatch.Items[i].Value;
                li.Text = lbBatch.Items[i].Text;
                ddlBatch.Items.Add(li);
            }
        }
    }

    /// <summary>
    /// Checkes Duplicate SKUS in Document Detail Grid
    /// </summary>
    /// <returns>bool</returns>
    private bool CheckDublicateSKUIssue()
    {
        DataControl dc = new DataControl();
        DataTable Dtsku_Price = (DataTable)this.Session["Dtsku_Price"];
        PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
        DataRow[] foundRows = PurchaseSKUIssue.Select("SKU_ID  = '" + hfSKUIDIssue.Value + "' AND BATCH_NO ='" + hfBatchNo.Value + "'");
        if (foundRows.Length == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Loads Document Detail Data To Document Detail Grid
    /// </summary>
    private void LoadGirdIssue()
    {
        int TotalValue = 0;
        PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
        gvIssue.DataSource = PurchaseSKUIssue;
        gvIssue.DataBind();
        foreach (DataRow dr in PurchaseSKUIssue.Rows)
        {
            TotalValue += int.Parse(dr["Quantity"].ToString());

        }
        txtTotalQuantityIssue.Text = TotalValue.ToString();
    }

    /// <summary>
    /// Sets Dcoment Detail Data For Edit. This Function Runs When An Existing Document Detail Needs To Be Edited
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvIssue_RowEditing(object sender, GridViewEditEventArgs e)
    {
        RowNo = e.NewEditIndex;
        txtskuNameIssue.Text = gvIssue.Rows[e.NewEditIndex].Cells[1].Text;
        txtQuantityIssue.Text = gvIssue.Rows[e.NewEditIndex].Cells[2].Text;
        hfBatchNo.Value = gvIssue.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", "");
        hfSKUIDIssue.Value = gvIssue.Rows[e.NewEditIndex].Cells[0].Text;
        txtQuantityIssue.Focus();
        btnSaveIssue.Text = "Update Sample";
        this.LoadBatchComb();

    }

    /// <summary>
    /// Deletes A Document Detail
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">GridViewEditEventArgs</param>
    protected void gvIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
        if (PurchaseSKUIssue.Rows.Count > 0)
        {
            PurchaseSKUIssue.Rows.RemoveAt(e.RowIndex);
            this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);
            this.LoadGirdIssue();
        }
    }

    /// <summary>
    /// Saves All Document Detail Grid Data
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSaveDocumentIssue_Click(object sender, EventArgs e)
    {
        PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
        if (PurchaseSKUIssue.Rows.Count > 0)
        {
            DistributorController mDayClose = new DistributorController();
            DataTable dt = mDayClose.SelectMaxDayClose(Constants.IntNullValue, int.Parse(drpDistributorIssue.SelectedValue.ToString()));
            if (dt.Rows.Count > 0)
            {
                if (CalculatePurchaseIssue(DateTime.Parse(dt.Rows[0]["CLOSING_DATE"].ToString())))
                {
                    EnqCtl.UpdateEnquiry(Convert.ToInt64(hfEnquiryID.Value), Convert.ToInt32(Session["DISTRIBUTOR_ID"]), Constants.DateNullValue, Constants.IntNullValue, null, Convert.ToInt32(Session["UserID"]), Constants.SampleIssued, true);
                    PurchaseSKUIssue = (DataTable)this.Session["PurchaseSKUIssue"];
                    PurchaseSKUIssue.Rows.Clear();
                    this.Session.Add("PurchaseSKUIssue", PurchaseSKUIssue);
                    this.LoadGirdIssue();
                    this.GetDocumentNoIssue();
                    this.ClearAllIssue();
                }
            }
            else
            {
                ltrlIssue.Text = Utility.ShowAlert(false, "Wrong Location");
            }
        }
        else
        {
            ltrlIssue.Text = Utility.ShowAlert(false, "At least one Product must enter");
        }
    }

    /// <summary>
    /// Save Document Detail Grid Data
    /// </summary>
    /// <param name="MWorkDate">Date</param>
    /// <returns>bool</returns>
    private bool CalculatePurchaseIssue(DateTime MWorkDate)
    {
        PurchaseController mController = new PurchaseController();
        DataTable dtPurchaseDetail = (DataTable)this.Session["PurchaseSKUIssue"];
        int PrincipalID = Constants.IntNullValue;

        foreach (DataRow dr in PurchaseSKUIssue.Rows)
        {
            PhaysicalStockController mController2 = new PhaysicalStockController();
            DataTable dtstock = mController2.SelectSKUClosingStock(int.Parse(drpDistributorIssue.SelectedValue), int.Parse(dr["SKU_ID"].ToString()), hfBatchNo.Value, DateTime.Parse(this.Session["CurrentWorkDate"].ToString()));

            if (dtstock.Rows.Count > 0)
            {
                PrincipalID = Convert.ToInt32(dtstock.Rows[0]["PRINCIPAL_ID"]);

                if (int.Parse(dtstock.Rows[0][0].ToString()) < int.Parse(dr["Quantity"].ToString()))
                {
                    ltrlIssue.Text = Utility.ShowAlert(false, dr["SKU_Name"].ToString() + "Current Stock is " + dtstock.Rows[0][0].ToString());
                    return false;
                }
            }
            else
            {
                ltrlIssue.Text = Utility.ShowAlert(false, dr["SKU_Name"].ToString() + "No Stock Found");
                return false;
            }
        }

        if (drpDocumentNoIssue.SelectedValue.ToString() == Constants.LongNullValue.ToString())
        {
            bool mResult = mController.InsertPurchaseDocument(int.Parse(drpDistributorIssue.SelectedValue.ToString()), "", 2
            , MWorkDate, int.Parse(DrpCustomerIssue.SelectedValue.ToString()), PrincipalID, 0, false, dtPurchaseDetail, Convert.ToInt32(hfEnquiryID.Value), null, int.Parse(this.Session["UserId"].ToString()), PrincipalID);
            return mResult;
        }
        else
        {
            bool mResult = mController.UpdatePurchaseDocument(int.Parse(drpDocumentNoIssue.SelectedValue.ToString()), int.Parse(drpDistributorIssue.SelectedValue.ToString()), "", 2
            , MWorkDate, int.Parse(DrpCustomerIssue.SelectedValue.ToString()), PrincipalID, 0, false, dtPurchaseDetail, Convert.ToInt32(hfEnquiryID.Value), null, int.Parse(this.Session["UserId"].ToString()), PrincipalID);
            return mResult;
        }
    }

    /// <summary>
    /// Resets Form Controls
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnCancelIssue_Click(object sender, EventArgs e)
    {
        this.CreatTableIssue();
        this.LoadGirdIssue();
        this.ClearAllIssue();
    }

    /// <summary>
    /// Loads Batch Nos
    /// </summary>
    private void LoadBatchNoIssue()
    {
        SKUPriceDetailController PController = new SKUPriceDetailController();
        DataTable dtBatchNo = PController.GetBatchNo();
        clsWebFormUtil.FillListBox(this.lbBatch, dtBatchNo, 0, 1);
    }
    #endregion

    #region Role Rights

    private void ApplyRoles()
    {
        ltrlTabs.Text = "<ul class='TabbedPanelsTabGroup'>";
        if (!CanView("0"))
        {
            ActiveTabIndex.Value = "0";
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0'>View Enquiry </li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='0' style='display:none'>View Enquiry </li>";
        }
        if (!CanView("1"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "1";
            }
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1'>Edit Enquiry</li>";
            //if (!CanEdit("1"))
            //{
            //    btnSaveEnquiry.Visible = false;
            //    btnSaveDocument.Visible = false;
            //    gvMainEdit.Columns[6].Visible = false;
            //    gvMainEdit.Columns[7].Visible = false;
            //}
            //if (!CanDelete("1"))
            //{
            //    gvMainEdit.Columns[7].Visible = false;
            //}
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='1' style='display:none'>Edit Enquiry</li>";
        }
        if (!CanView("2"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "2";
            }
            ////if (!CanEdit("2"))
            ////{
            ////    btnAddEmail.Visible = false;
            ////}
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'>Email to Supplier</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='2'  style='display:none'>Email to Supplier</li>";
        }
        if (!CanView("3"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "3";
            }
            //if (!CanEdit("3"))
            //{
            //    btnGenerate.Visible = false;
            //}
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='3'>Approve Sample</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='3' style='display:none'>Approve Sample</li>";
        }
        if (!CanView("4"))
        {
            if (ActiveTabIndex.Value.Length < 1)
            {
                ActiveTabIndex.Value = "4";
            }
            //if (!CanEdit("4"))
            //{
            //    btnSaveIssue.Visible = false;
            //    btnSaveDocumentIssue.Visible = false;
            //    btnCancelIssue.Visible = false;
            //}
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='4'>Issue Sample</li>";
        }
        else
        {
            ltrlTabs.Text += "<li class='TabbedPanelsTab' tabindex='4' style='display:none'>Issue Sample</li>";
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