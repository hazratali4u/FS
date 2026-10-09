using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class Forms_frmTransactionHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.txtMonth.Text = System.DateTime.Now.ToString("MMMM yyyy");
            GetHistory(Server.MapPath("~/LogTransaction/" + System.DateTime.Now.ToString("yyyyMM") + "TransactionLog.xml"));
        }
    }

    private void FillRepeater()
    {
        rTranHistory.DataSource = null;
        rTranHistory.DataBind();
        lblTotalNoOfRecords.Text = "";
        lblCurrentPageNo.Text = "";
        lblTotalNoOfPages.Text = "";
        PagedDataSource PagedResults = new PagedDataSource();
        PagedResults.AllowPaging = true;
        PagedResults.PageSize = 20;

        DataTable dt = (DataTable)Session["dtHistory"];

        PagedResults.DataSource = dt.DefaultView;
        if (PagedResults.Count > 0)
        {
            PagedResults.CurrentPageIndex = CurrentPage;
            linkbtnnext.Enabled = !PagedResults.IsLastPage;
            linkbtnprev.Enabled = !PagedResults.IsFirstPage;
            rTranHistory.DataSource = PagedResults;
            rTranHistory.DataBind();

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

    private void GetHistory(string FileName)
    {
        DataTable dt = FSCommonLayer.Classes.Utility .GetTransaction(FileName);

        DataTable dtCloned = dt.Clone();
        dtCloned.Columns[5].DataType = typeof(DateTime);
        foreach (DataRow row in dt.Rows)
        {
            dtCloned.ImportRow(row);
        }

        if (dtCloned == null)
        {
            ltrlHistory.Text = FSCommonLayer.Classes.Utility.ShowAlert(false, "No Transaction found for selected month.");
            rTranHistory.DataSource = null;
            rTranHistory.DataBind();
            return;
        }
        dtCloned.DefaultView.Sort = "TransactionID DESC";
        this.Session.Add("dtHistory", dtCloned);
        FillRepeater();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ltrlHistory.Text = "";
        string strMonth = Convert.ToDateTime(txtMonth.Text).ToString("yyyyMM");
        GetHistory(Server.MapPath("~/LogTransaction/" + strMonth + "TransactionLog.xml"));        
    }
}