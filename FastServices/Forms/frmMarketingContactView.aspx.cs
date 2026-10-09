using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Collections;
using System.Configuration;

/// <summary>
/// From To Add,View, Edit Enquiry Pricing
/// </summary>
public partial class Forms_frmMarketingContactView : System.Web.UI.Page
{
    MarketingController MarketingCtl = new MarketingController();

    
    /// <summary>
    /// Page_Load Function Populates All Combos And Grid On The Page
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CurrentPage = 0;
            ltlAddContact.Text = "<span style='font - size:x - large;'><a href = 'frmMarketingContactAdd.aspx?LevelType=" + Request.QueryString["LevelType"].ToString() + "&TopID=" + Request.QueryString["TopID"].ToString() + "&LevelID=" + Request.QueryString["LevelID"].ToString() + "' style = 'text-decoration:none;color:white;'> + </a></ span >";
            if(!CanView(Request.QueryString["LevelID"].ToString()))
            {
                GetContact(null);
                FillRepeater();                
            }
            else
            {
                pnlContactView.Visible = false;
            }
        }
    }
    
    private void FillRepeater()
    {
        rContact.DataSource = null;
        rContact.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";        
        DataTable dt = new DataTable();
        dt = (DataTable)Session["dtContact"];

        if (dt.Rows.Count > 0)
        {            
            rContact.DataSource = dt;
            rContact.DataBind();

            linkbtnnext.Visible = true;
            linkbtnprev.Visible = true;
            lblTotalNoOfPages.Visible = true;
            lblCurrentPageNo.Visible = true;
            lblTotalNoOfRecords.Visible = true;
            lblDummy.Visible = true;
            lblOf.Visible = true;
            lblTotalNoOfRecords.Text = dt.Rows[0]["TotalRows"].ToString();
            lblCurrentPageNo.Text = (CurrentPage + 1).ToString();
            lblTotalNoOfPages.Text = Convert.ToString(Math.Ceiling(Convert.ToDecimal(dt.Rows[0]["TotalRows"]) / Convert.ToDecimal(20)));

            if (lblCurrentPageNo.Text == lblTotalNoOfPages.Text)
            {
                linkbtnnext.Enabled = false;
            }
            else
            {
                linkbtnnext.Enabled = true;
            }
            if (lblCurrentPageNo.Text == "1")
            {
                linkbtnprev.Enabled = false;
            }
            else
            {
                linkbtnprev.Enabled = true;
            }

        }
        else if (dt.Rows.Count == 0)
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
        if (txtSearch.Text.Trim().Length > 0)
        {
            GetContact(txtSearch.Text);
        }
        else
        {
            GetContact(null);
        }
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        if(txtSearch.Text.Trim().Length > 0)
        {
            GetContact(txtSearch.Text);
        }
        else
        {
            GetContact(null);
        }        
        FillRepeater();
    }

    public void GetContact(string SearchText)
    {
        DataTable dtContact = MarketingCtl.GetMarketingContact(Constants.LongNullValue, CurrentPage, 20, SearchText);
        Session.Add("dtContact", dtContact);
    }

    protected void rContact_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {
        txtSearch.Text = string.Empty;
        if (e.CommandName == "del")
        {
            if (MarketingCtl.UpdateMarketingContact(Convert.ToInt64(e.CommandArgument), null, null, null, null, null, Convert.ToInt32(Session["UserID"]), true))
            {
                ltrlContact.Text = Utility.ShowAlert(true, "Contact deleted successfully.");
                GetContact(null);
                FillRepeater();
            }
            else
            {
                ltrlContact.Text = Utility.ShowAlert(false, "Contact not deleted. Some erro occured.");
            }
        }
        else if (e.CommandName == "edt")
        {

        }
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

    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        if(txtSearch.Text.Trim().Length>0)
        {
            GetContact(txtSearch.Text);
            FillRepeater();
        }
        else
        {
            GetContact(txtSearch.Text);
            FillRepeater();
        }
    }
}