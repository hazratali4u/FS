using System;
using System.Data;
using System.Web.UI;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;

/// <summary>
/// From For Company
/// </summary>
public partial class Forms_frmCompany : System.Web.UI.Page
{
    CompanyController mCompany = new CompanyController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadCompany();
        }
    }

    private void LoadCompany()
    {
        DataTable dt = mCompany.SelectCompany(Constants.IntNullValue, Constants.IntNullValue);
        if (dt.Rows.Count > 0)
        {
            hfCompanyID.Value = dt.Rows[0]["COMPANY_ID"].ToString();
            txtAddress.Text = dt.Rows[0]["ADDRESS1"].ToString();
            txtCompany.Text = dt.Rows[0]["COMPANY_NAME"].ToString();
            txtDisplayName.Text = dt.Rows[0]["WEBSITE"].ToString();
            txtEmail.Text = dt.Rows[0]["EMAIL_ADDRESS"].ToString();
            txtNTN.Text = dt.Rows[0]["NTN_NO"].ToString();
            txtPhone.Text = dt.Rows[0]["PHONE"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtCompany.Text.Length > 0)
        {
            if (mCompany.UpdateCompany(false,Convert.ToInt32(hfCompanyID.Value),txtEmail.Text,txtPhone.Text,txtDisplayName.Text,txtCompany.Text,txtAddress.Text,"",txtNTN.Text))
            {
                //Save Transaction in log
                Utility.InsertTransaction(Server.MapPath(Utility.GetTransactionFilePath()), "Update", Session["UserName2"].ToString(), "Company", "Company updated");

                ltrlCompany.Text = Utility.ShowAlert(true, "Company updated successfully.");
            }
            else
            {
                ltrlCompany.Text = Utility.ShowAlert(false, "Some error occured.");
            }
        }
        else
        {
            ltrlCompany.Text = Utility.ShowAlert(false, "Enter company name.");
        }
    }
}