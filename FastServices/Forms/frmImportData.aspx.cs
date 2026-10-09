using System;
using System.Data;
using System.Web.UI;
using FSCommonLayer.Classes;
using FSBusinessLayer.Classes;
using System.IO;

/// <summary>
/// Form To Import Route, Market,  Customer, SKU And SKU Price Data From Text Files
/// </summary>
public partial class Forms_frmImportData : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
    }

    /// <summary>
    /// Imports File Data To Database
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="e">EventArgs</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region FileValidation
        if (txtFile.PostedFile.ContentLength == 0)
        {
            lblErrorMessage.Text = "Please select a file and then upload";
            return;
        }
        else if (txtFile.PostedFile.ContentType != "text/plain")
        {
            lblErrorMessage.Text = "Only text file are supported";
            return;
        }
        #endregion

        try
        {
            if (!Directory.Exists(Constants.fldOtherDataFolder))
            {
                Directory.CreateDirectory(Constants.fldOtherDataFolder);
            }

            string path = System.IO.Path.GetFullPath(txtFile.PostedFile.FileName);
            string filename = path.Substring(path.LastIndexOf('\\'), path.Length - path.LastIndexOf('\\'));
            txtFile.PostedFile.SaveAs(Constants.fldOtherDataFolder + filename);
            path = Constants.fldOtherDataFolder + filename;
            int index = 0;
            this.lblErrorMessage.Text = "";
            DistributorAreaController DistAreaCntl = new DistributorAreaController();
            if(DistAreaCntl.ImportMarketingContact(path, int.Parse(this.Session["UserId"].ToString())))
            {
                this.lblErrorMessage.Text = "Contacts imported successfully.";
            }
            else
            {
                this.lblErrorMessage.Text = "Some error occured.";
            }

        }
        catch (Exception excp)
        {
            lblErrorMessage.Text = excp.ToString();
            return;
        }
    }
}
