using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

public partial class Forms_frmDocument : System.Web.UI.Page
{
    SLASHCodesController CodesCtl = new SLASHCodesController();
    DocumentController DocCtl = new DocumentController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ApplyRoles();
            LoadDocuments();
            LoadDocumentCombo();
        }
    }

    protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        hfUpload.Value = "0";
        if (e.CommandName.ToLower() == "del")
        {            
            long id = long.Parse(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)((ImageButton)e.CommandSource).NamingContainer;
            string DocName = gvDocument.DataKeys[row.RowIndex].Value.ToString();

            if (DocCtl.DeleteUploadDocument(id,Convert.ToInt32(Session["UserID"])))
            {
                //Save Transaction in log                
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Delete", Session["UserName2"].ToString(), "Document", DocName + " deleted");

                LoadDocuments();
                ltrlDoc.Text = "<div style='padding:10px; margin:10px; background-color:#ecfaf0;border: Solid 1px #04551d; color:#04551d;	font-size:12px; font-family:Verdana;'><b>Success</b><br />The selected document has been removed successfully.</div>";
            }
        }
        else if (e.CommandName.ToLower() == "view")
        {
            long id = long.Parse(e.CommandArgument.ToString());
            DataTable dtDoc = DocCtl.GetUploadDocument(Constants.LongNullValue, Constants.IntNullValue, id);
            if (dtDoc.Rows.Count > 0)
            {
                string FName = dtDoc.Rows[0]["NAME"].ToString();
                DownLoadFile(Server.MapPath("~/Docs/" + FName));
            }
        }
    }

    protected void gvDocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton btnViewDoc = e.Row.FindControl("btnViewDoc") as ImageButton;
            LinkButton lnkViewDoc = e.Row.FindControl("lnkViewDoc") as LinkButton;

            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnViewDoc);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkViewDoc);

            if (CanEdit(Request.QueryString["LevelID"].ToString()))
            {
                btnViewDoc.Visible = false;
            }

            if (CanDelete(Request.QueryString["LevelID"].ToString()))
            {
                ImageButton btnDel = (ImageButton)e.Row.FindControl("btnDel");
                btnDel.Visible = false;
            }
        }
    }

    private void LoadDocuments()
    {
        gvDocument.DataSource = null;
        gvDocument.DataBind();
        DataTable dtDocs = DocCtl.GetUploadDocument(Constants.LongNullValue, Constants.IntNullValue, Constants.LongNullValue);
        if (dtDocs != null && dtDocs.Rows.Count > 0)
        {
            gvDocument.DataSource = dtDocs;
            gvDocument.DataBind();
            ViewState["docs"] = dtDocs;
        }
    }

    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        if (fuDoc.HasFile)
        {
            string fileName = fuDoc.PostedFile.FileName;
            fuDoc.SaveAs(Server.MapPath("~/Docs/" + fileName));
            if (txtDocTitle.Text.Trim() != "")
            {
                if(DocCtl.InsertUploadDocument(Constants.LongNullValue,Convert.ToInt32(ddlDoc.SelectedValue),Convert.ToInt32(Session["UserID"]),fileName,txtDocTitle.Text))
                {
                    ltrlDoc.Text = Utility.ShowAlert(true, "Document has been uploaded successfully.");
                    txtDocTitle.Text = "";

                    //Save Transaction in log                    
                    Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Insert", Session["UserName2"].ToString(), "Document", fileName + " Uploaded");

                    LoadDocuments();
                }
            }
            else
            {
                ltrlDoc.Text = Utility.ShowAlert(false, "Please enter Title.");
            }
        }
        else
        {
            ltrlDoc.Text = Utility.ShowAlert(false, "Please browse some document.");
        }
        hfUpload.Value = "1";
    }

    private void LoadDocumentCombo()
    {
        DataTable dt = CodesCtl.SelectSlashCodes(Constants.IntNullValue, null, Constants.DocType, null, Constants.IntNullValue, true);
        clsWebFormUtil.FillDropDownList(this.ddlDoc, dt, 0, 2);

        ddlType.Items.Add(new clsListItems("All", "-1"));
        clsWebFormUtil.FillDropDownList(this.ddlType, dt, 0, 2);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DataTable dt;
        if (ViewState["docs"] != null)
        {
            dt = (DataTable)ViewState["docs"];
        }
        else
        {
            dt = DocCtl.GetUploadDocument(Constants.LongNullValue, Constants.IntNullValue, Constants.LongNullValue);
        }

        if (txtName.Text == "")
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                if (ddlType.SelectedValue == "0")
                {
                    gvDocument.DataSource = dt;
                    gvDocument.DataBind();
                }
                else
                {

                    DataTable dti = dt.Clone();
                    DataRow[] dr = dt.Select("TYPE_ID='" + ddlType.SelectedValue + "'");
                    foreach (DataRow drr in dr)
                        dti.ImportRow(drr);

                    gvDocument.DataSource = dti;
                    gvDocument.DataBind();
                }
            }
        }
        else
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dti = dt.Clone();
                DataRow[] dr = dt.Select("NAME LIKE  '%" + txtName.Text + "%'");
                foreach (DataRow drr in dr)
                    dti.ImportRow(drr);

                if (ddlType.SelectedValue != "0")
                {
                    DataTable dtt = dti.Clone();
                    DataRow[] dr1 = dti.Select("TYPE_ID='" + ddlType.SelectedValue + "'");
                    foreach (DataRow drr1 in dr1)
                        dtt.ImportRow(drr1);

                    gvDocument.DataSource = dtt;
                    gvDocument.DataBind();
                }
                else
                {
                    gvDocument.DataSource = dti;
                    gvDocument.DataBind();

                }
            }
        }
    }

    private void DownLoadFile(string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            long sz = fi.Length;
            Response.ClearContent();
            Response.ContentType = MimeType(Path.GetExtension(path));
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename = {0}", System.IO.Path.GetFileName(path)));
            Response.AddHeader("Content-Length", sz.ToString("F0"));
            Response.TransmitFile(path);
            Response.End();
        }
    }

    public static string MimeType(string Extension)
    {
        string mime = "application/octetstream";
        if (string.IsNullOrEmpty(Extension))
            return mime;
        string ext = Extension.ToLower();
        Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
        if (rk != null && rk.GetValue("Content Type") != null)
            mime = rk.GetValue("Content Type").ToString();
        return mime;
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
            btnAddDoc.Visible = false;            
        }

    }
    #endregion
    
}