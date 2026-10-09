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
public partial class Forms_frmMarketingEmailView : System.Web.UI.Page
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
            if(!CanView(Request.QueryString["LevelID"].ToString()))
            {
                GetEmailLog();
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
        dt = (DataTable)Session["dtEmail"];

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
        GetEmailLog();
        FillRepeater();
    }

    protected void linkbtnnext_Click(object sender, EventArgs e)
    {
        CurrentPage += 1;
        GetEmailLog();
        FillRepeater();
    }

    public void GetEmailLog()
    {
        DataTable dtEmail = MarketingCtl.GetMarketingEmail(Constants.LongNullValue, CurrentPage, 20);
        Session.Add("dtEmail", dtEmail);
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
}