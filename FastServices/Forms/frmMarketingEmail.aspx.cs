using System;
using System.Data;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmMarketingEmail : System.Web.UI.Page
{
    MarketingController Market = new MarketingController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplyRoles();
            this.LoadTemplate();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        ltrlTemplate.Text = "";
        if (txtTemplate.Text.Length > 0)
        {
            if (btnSave.Text == "Save")
            {
                if (Market.InsertMarketingTemplate(txtTemplate.Text, txtBody.Value, Convert.ToInt32(Session["UserID"])))
                {
                    this.LoadTemplate();
                    ltrlTemplate.Text = Utility.ShowAlert(true, "Template updated successfull.");
                }
                else
                {
                    ltrlTemplate.Text = Utility.ShowAlert(false, "Some erro occured.");
                }
            }
            else
            {
                if (Market.UpdateMarketingTemplate(Convert.ToInt32(ddlTemplate.SelectedValue), txtTemplate.Text, txtBody.Value, Convert.ToInt32(Session["UserID"])))
                {
                    this.LoadTemplate();
                    ltrlTemplate.Text = Utility.ShowAlert(true, "Template addedd successfull.");
                }
                else
                {
                    ltrlTemplate.Text = Utility.ShowAlert(false, "Some erro occured.");
                }
            }
        }
        else
        {
            ltrlTemplate.Text = Utility.ShowAlert(false, "Please enter Template name.");
        }
    }
    
    private void LoadTemplate()
    {
        DataTable dtTemplate = Market.GetMarketingTemplate(Constants.IntNullValue);
        clsWebFormUtil.FillDropDownList(ddlTemplate, dtTemplate, "MARKETING_TEMPLATE_ID", "TEMPLATE_NAME",true);
        if (dtTemplate.Rows.Count > 0)
        {
            txtTemplate.Visible = false;
            ddlTemplate.Visible = true;
            txtTemplate.Text = dtTemplate.Rows[0]["TEMPLATE_NAME"].ToString();
            txtBody.Value = dtTemplate.Rows[0]["TEMPLATE_BODY"].ToString();
            btnSave.Text = "Update";
            Session.Add("dtTemplate", dtTemplate);
        }
        else
        {
            txtTemplate.Visible = true;
            ddlTemplate.Visible = false;
            btnAddTemplate.Visible = false;
        }
    }

    protected void btnAddTemplate_Click(object sender, EventArgs e)
    {
        txtTemplate.Visible = true;
        ddlTemplate.Visible = false;
        btnAddTemplate.Visible = false;
        txtTemplate.Text = string.Empty;
        txtBody.Value = string.Empty;
        btnSave.Text = "Save";
    }

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        ltrlTemplate.Text = string.Empty;
        if (ddlTemplate.Visible)
        {
            if (txtEmailRange.Text.Length > 0)
            {
                if (txtEmailRange.Text.Contains("-"))
                {
                    string[] strRange = txtEmailRange.Text.Split('-');
                    if (Market.InsertMarketingEmail(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlTemplate.SelectedValue), txtBody.Value, strRange[0].ToString(), strRange[1].ToString()
                        , Convert.ToInt32(Session["UserID"])))
                    {
                        ltrlTemplate.Text = Utility.ShowAlert(true, "Send Email addedd successfull.");
                    }
                    else
                    {
                        ltrlTemplate.Text = Utility.ShowAlert(false, "Some error occured.");
                    }
                }
                else
                {
                    if (Market.InsertMarketingEmail(Convert.ToDateTime(Session["CurrentWorkDate"]), Convert.ToInt32(ddlTemplate.SelectedValue), txtBody.Value, txtEmailRange.Text, txtEmailRange.Text
                        , Convert.ToInt32(Session["UserID"])))
                    {
                        ltrlTemplate.Text = Utility.ShowAlert(true, "Send Email addedd successfull.");
                    }
                    else
                    {
                        ltrlTemplate.Text = Utility.ShowAlert(false, "Some error occured.");
                    }
                }
            }
            else
            {
                ltrlTemplate.Text = Utility.ShowAlert(false, "Please enter email(s) range.");
            }
        }
        else
        {
            ltrlTemplate.Text = Utility.ShowAlert(false, "No template selected.");
        }
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

    private void ApplyRoles()
    {
        if (CanEdit(Request.QueryString["LevelID"].ToString()))
        {
            btnSave.Visible = false;
        }
    }

    #endregion

    protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtTemplate = (DataTable)Session["dtTemplate"];
        if(dtTemplate != null)
        {
            foreach(DataRow dr in dtTemplate.Rows)
            {
                if(dr["MARKETING_TEMPLATE_ID"].ToString() == ddlTemplate.SelectedValue)
                {
                    txtTemplate.Text = dr["TEMPLATE_NAME"].ToString();
                    txtBody.Value = dr["TEMPLATE_BODY"].ToString();
                    btnSave.Text = "Update";
                }
            }
        }
    }
}