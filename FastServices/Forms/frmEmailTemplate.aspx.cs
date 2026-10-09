using System;
using System.Data;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmEmailTemplate : System.Web.UI.Page
{
    QuotationController QtnCtl = new QuotationController();
    SLASHCodesController CodesCtl = new SLASHCodesController();
    private static int m_EmailTemplate_ID;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplyRoles();

            if (Request.QueryString["mode"] != null)
            {
                pnlAdd.Visible = true;
                pnlView.Visible = false;                
            }
            else
            {
                LoadData(Constants.IntNullValue);
                FillRepeater();
            }

            this.LoadCategory();
        }
    }
    
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrlTemplate.Text = "";
        if (txtBody.Value.Length > 0)
        {
            if (m_EmailTemplate_ID != Constants.IntNullValue)
            {
                if (QtnCtl.UpdateEmailTemplate(m_EmailTemplate_ID, Convert.ToInt32(ddlTemplate.SelectedValue), txtSubject.Text, txtBody.Value, txtSignature.Value, txtTerms.Value, Convert.ToInt32(Session["UserId"]), true))
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Email Template", "Template updated for " + ddlTemplate.SelectedItem.Text);

                    ltrlTemplate.Text = Utility.ShowAlert(true, "Quotation Template Updated Successfully.");
                }
                else
                {
                    ltrlTemplate.Text = Utility.ShowAlert(false, "Some error occured");
                }
            }
            else
            {
                m_EmailTemplate_ID = QtnCtl.InsertEmailTemplate(Convert.ToInt32(ddlTemplate.SelectedValue),1, txtSubject.Text, txtBody.Value, txtSignature.Value, txtTerms.Value, Convert.ToInt32(Session["UserId"]));
                if (m_EmailTemplate_ID > Constants.IntNullValue)
                {
                    //Save Transaction in log
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Email Template", "Added for " + ddlTemplate.SelectedItem.Text);

                    ltrlTemplate.Text = Utility.ShowAlert(true, "Quotation Template Added Successfully.");
                }
                else
                {
                    ltrlTemplate.Text = Utility.ShowAlert(false, "Some error occured");
                }
            }
        }
        else
        {
            ltrlTemplate.Text = Utility.ShowAlert(false, "Please enter Quotation Body.");
        }
    }

    private void LoadData(int CategoryID)
    {
        ltrlTemplate.Text = "";
        DataTable dt = QtnCtl.GetEmailTemplate(Constants.IntNullValue, 1,CategoryID);
        this.Session.Add("dtTemplate", dt);        
    }
    
    private void LoadCategory()
    {
        DataTable m_dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.ProductCat, null, Constants.IntNullValue, true);
        clsWebFormUtil.FillDropDownList(this.ddlTemplate, m_dt, "REF_ID", "SLASH_DESC");
    }

    #region View Template

    protected void btnNewTemplate_Click(object sender, EventArgs e)
    {
        m_EmailTemplate_ID = Constants.IntNullValue;
        Response.Redirect("frmEmailTemplate.aspx?LevelID=" + Request.QueryString["LevelID"].ToString() + "&LevelType=2&mode=new");        
    }

    protected void rTemplate_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        ltrlView.Text = string.Empty;
        if (e.CommandName == "edit")
        {
            Session["mode"] = "edit";
            m_EmailTemplate_ID = Constants.IntNullValue;
            EditTemplate(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "del")
        {
            if (QtnCtl.UpdateEmailTemplate((Convert.ToInt32(e.CommandArgument)),0,null,null,null,null, Convert.ToInt32(Session["UserID"]),false))
            {
                HiddenField hfQuotation = (HiddenField)e.Item.FindControl("hfQuotation");
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Email Template", "Template for " + hfQuotation.Value + " deleted");

                ltrlView.Text = Utility.ShowAlert(true, "Template Deleted successfully.");
                LoadData(Constants.IntNullValue);
                FillRepeater();
            }
            else
            {
                ltrlView.Text = Utility.ShowAlert(false,"Some error occured");
            }
        }
    }

    private void EditTemplate(int p_TemplateID)
    {
        DataTable dt = (DataTable)Session["dtTemplate"];
        
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["EMAIL_TEMPLATE_ID"].ToString() == p_TemplateID.ToString())
                {
                    txtSubject.Text = dr["Subject"].ToString();
                    txtBody.Value = dr["Body"].ToString();
                    if (dr["SIGNATURE"] != null)
                    {
                        txtSignature.Value = dr["SIGNATURE"].ToString();
                    }
                    txtTerms.Value = dr["TERMS"].ToString();
                    ddlTemplate.SelectedValue = dr["CATEGORY_ID"].ToString();
                    m_EmailTemplate_ID = Convert.ToInt32(dr["EMAIL_TEMPLATE_ID"]);
                    break;
                }
            }            
        }
        else
        {
            m_EmailTemplate_ID = Constants.IntNullValue;
            txtSubject.Text = "";
            txtBody.Value = "";
            txtSignature.Value = "";
            txtTerms.Value = "";
        }

        btnSave.Text = "Update";

        pnlAdd.Visible = true;
        pnlView.Visible = false;

    }

    protected void rTemplate_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            LinkButton btnDelete = e.Item.FindControl("btnDelete") as LinkButton;
            LinkButton btnView = e.Item.FindControl("btnView") as LinkButton;
            LinkButton btnTemplateName = e.Item.FindControl("btnTemplateName") as LinkButton;
            
            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                btnView.Visible = false;
                btnTemplateName.Enabled = false;
            }

            if (CanDelete(Request.QueryString["LevelID"].ToString()))
            {
                btnDelete.Visible = false;
            }
        }
    }

    private void FillRepeater()
    {
        rTemplate.DataSource = null;
        rTemplate.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtTemplate"];

        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rTemplate.DataSource = PagedResults;
            rTemplate.DataBind();

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
            btnSave.Visible = false;
            btnNewTemplate.Visible = false;
        }
    }

    #endregion
}