using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSBusinessLayer.Classes;
using FSCommonLayer.Classes;
using System.Data;

public partial class Forms_frmMenu : System.Web.UI.Page
{
    MenuController MenuCtl = new MenuController();
    RoleManagementController ObjRole = new RoleManagementController();
    private static int MenuID = Constants.IntNullValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadModules();
            LoadParentModuel();
            LoadLeftList();
        }
    }

    #region Top Menu

    protected void ibUp_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstTopMenu.Items.Count; i++)
        {
            if (lstTopMenu.Items[i].Selected)//identify the selected item
            {
                //swap with the top item(move up)
                if (i > 0 && !lstTopMenu.Items[i - 1].Selected)
                {
                    ListItem bottom = lstTopMenu.Items[i];
                    lstTopMenu.Items.Remove(bottom);
                    lstTopMenu.Items.Insert(i - 1, bottom);
                    lstTopMenu.Items[i - 1].Selected = true;
                }
            }
        }

    }

    protected void ibDown_Click(object sender, ImageClickEventArgs e)
    {
        int startindex = lstTopMenu.Items.Count - 1;
        for (int i = startindex; i > -1; i--)
        {
            if (lstTopMenu.Items[i].Selected)//identify the selected item
            {
                //swap with the lower item(move down)
                if (i < startindex && !lstTopMenu.Items[i + 1].Selected)
                {
                    ListItem bottom = lstTopMenu.Items[i];
                    lstTopMenu.Items.Remove(bottom);
                    lstTopMenu.Items.Insert(i + 1, bottom);
                    lstTopMenu.Items[i + 1].Selected = true;
                }
            }
        }
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (btnAdd.Text == "Save")
        {
            AddTopMenu();
            LoadModules();
        }
        else
        {
            EditTopMenu();
            LoadModules();
        }
    }

    private void AddTopMenu()
    {
        if (MenuCtl.InsertMenu(txtMenu.Text, 1, 12, null))
        {
            txtMenu.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu added successfully.');window.location.href = 'frmMenu.aspx'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
        }
    }

    private void EditTopMenu()
    {
        if (MenuCtl.UpdateMenu(MenuID, txtMenu.Text, Constants.IntNullValue, Constants.IntNullValue, Constants.IntNullValue))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu sorted successfully.');window.location.href = 'frmMenu.aspx'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
        }
    }

    private void LoadParentModuel()
    {
        ddlParent.Items.Clear();
        if (ddlTop.Items.Count > 0)
        {
            DataTable dtParent = MenuCtl.GetMenu(Constants.IntNullValue, Convert.ToInt32(ddlTop.SelectedValue), 13);
            ddlParent.Items.Add(new ListItem("No Parent", "0"));
            clsWebFormUtil.FillDropDownList(this.ddlParent, dtParent, "MENU_ID", "MENU_DESCRIPTION");
        }

    }

    private void LoadModules()
    {       
        DataTable dtParent = ObjRole.SelectModuleNames();
        clsWebFormUtil.FillDropDownList(this.ddlLink, dtParent, "MODULE_KEY", "MODULE_DESCRIPTION",true);

        DataTable dtTopMenu = MenuCtl.GetMenu(Constants.IntNullValue, Constants.IntNullValue, 12);
        clsWebFormUtil.FillListBox(this.lstTopMenu, dtTopMenu, "MENU_ID", "MENU_DESCRIPTION",true);
        clsWebFormUtil.FillDropDownList(this.ddlTop, dtTopMenu, "MENU_ID", "MENU_DESCRIPTION",true);
    }

    protected void btnUpdateTopMenu_Click(object sender, EventArgs e)
    {
        if (lstTopMenu.Items.Count > 0)
        {
            MenuController MenuCtl = new MenuController();
            for (int i = 0; i < lstTopMenu.Items.Count; i++)
            {
                if (MenuCtl.UpdateMenu(Convert.ToInt32(lstTopMenu.Items[i].Value), lstTopMenu.Items[i].Text, Constants.IntNullValue, Constants.IntNullValue, i))
                {
                    LoadModules();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu sorted successfully.');window.location.href = 'frmMenu.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No item found.');", true);
        }
    }

    protected void ibEdit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtMenu.Text = lstTopMenu.SelectedItem.Text;
            MenuID = Convert.ToInt32(lstTopMenu.SelectedItem.Value);
            btnAdd.Text = "Update";
        }
        catch (Exception ex)
        {

        }
    }

    protected void ibDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MenuID = Convert.ToInt32(lstTopMenu.SelectedItem.Value);

            DataTable dtDelete = MenuCtl.DeleteMenuLevel(MenuID);
            if (dtDelete.Rows[0]["Message"].ToString() != "NO")
            {
                LoadModules();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu deleted successfully.');window.location.href = 'frmMenu.aspx'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Can not be deleted sub record found.');", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlTop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlParent.SelectedValue) > 0)
        {
            lblLink.Visible = true;
            ddlLink.Visible = true;            
        }
        else
        {
            lblLink.Visible = false;
            ddlLink.Visible = false;         
        }
        LoadParentModuel();
        LoadLeftList();
    }

    protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlParent.SelectedValue) > 0)
        {
            lblLink.Visible = true;
            ddlLink.Visible = true;
            LoadLeftList();
        }
        else
        {
            lblLink.Visible = false;
            ddlLink.Visible = false;
            LoadLeftList();
        }
    }

    #endregion

    #region Left Menu

    protected void ibUp2_click(object sender, EventArgs e)
    {

        for (int i = 0; i < lstLeft.Items.Count; i++)
        {
            if (lstLeft.Items[i].Selected)//identify the selected item
            {
                //swap with the top item(move up)
                if (i > 0 && !lstLeft.Items[i - 1].Selected)
                {
                    ListItem bottom = lstLeft.Items[i];
                    lstLeft.Items.Remove(bottom);
                    lstLeft.Items.Insert(i - 1, bottom);
                    lstLeft.Items[i - 1].Selected = true;
                }
            }
        }

    }

    protected void ibDown2_Click(object sender, ImageClickEventArgs e)
    {
        int startindex = lstLeft.Items.Count - 1;
        for (int i = startindex; i > -1; i--)
        {
            if (lstLeft.Items[i].Selected)//identify the selected item
            {
                //swap with the lower item(move down)
                if (i < startindex && !lstLeft.Items[i + 1].Selected)
                {
                    ListItem bottom = lstLeft.Items[i];
                    lstLeft.Items.Remove(bottom);
                    lstLeft.Items.Insert(i + 1, bottom);
                    lstLeft.Items[i + 1].Selected = true;
                }

            }
        }
    }
    
    protected void btnAdd2_Click(object sender, EventArgs e)
    {
        if (btnAdd2.Text == "Save")
        {
            AddLefMenu();
            LoadModules();
            LoadParentModuel();
            LoadLeftList();
        }
        else
        {
            EditLeftMenu();
            LoadModules();
            LoadParentModuel();
            LoadLeftList();            
        }
    }

    private void AddLefMenu()
    {
        bool flag = false;
        if (ddlParent.SelectedValue == "0")
        {
            flag = MenuCtl.InsertMenu(txtMenu2.Text, Convert.ToInt32(ddlTop.SelectedValue), 13, null);
        }
        else
        {
            flag = MenuCtl.InsertMenu(txtMenu2.Text, Convert.ToInt32(ddlParent.SelectedValue), 14, ddlLink.SelectedValue);
        }

        if (flag)
        {
            txtMenu2.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu added successfully.');'", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
        }

    }

    private void EditLeftMenu()
    {
        bool flag = false;
        if (ddlParent.SelectedValue == "0")
        {
            flag = MenuCtl.UpdateMenu(MenuID, txtMenu2.Text, Convert.ToInt32(ddlTop.SelectedValue), 13, Constants.IntNullValue);
        }
        else
        {
            flag = MenuCtl.UpdateMenu(MenuID, txtMenu2.Text, Convert.ToInt32(ddlParent.SelectedValue), 14, Constants.IntNullValue,ddlLink.SelectedValue);
        }

        if (flag)
        {
            txtMenu2.Text = "";
            btnAdd2.Text = "Save";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu updated successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
        }
    }
       
    protected void ibDelete2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            MenuID = Convert.ToInt32(lstLeft.SelectedItem.Value);

            DataTable dtDelete = MenuCtl.DeleteMenuLevel(MenuID);
            if (dtDelete.Rows[0]["Message"].ToString() != "NO")
            {
                txtMenu2.Text = "";
                btnAdd2.Text = "Save";
                LoadModules();
                LoadParentModuel();
                LoadLeftList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu deleted successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Can not be deleted sub record found.');", true);
            }
        }
        catch (Exception ex)
        {

        }
    }
    
    protected void ibEdit2_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtMenu2.Text = lstLeft.SelectedItem.Text;            
            MenuID = Convert.ToInt32(lstLeft.SelectedItem.Value);
            btnAdd2.Text = "Update";
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnUpdateLeftMenu_Click(object sender, EventArgs e)
    {        
        if (lstLeft.Items.Count > 0)
        {
            bool flag = false;
            for (int i = 0; i < lstLeft.Items.Count; i++)
            {
                flag = MenuCtl.UpdateMenu(Convert.ToInt32(lstLeft.Items[i].Value), lstLeft.Items[i].Text, Constants.IntNullValue, Constants.IntNullValue, i);
                
            }

            if (flag)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Menu sorted successfully.');", true);
                LoadModules();
                LoadParentModuel();
                LoadLeftList();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('Some error occured.');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "javascript:alert('No item found.');", true);
        }
    }

    private void LoadLeftList()
    {
        if (ddlParent.SelectedValue == "0")
        {
            DataTable dtLeftMenu = MenuCtl.GetMenu(Constants.IntNullValue, Convert.ToInt32(ddlTop.SelectedValue), 13);
            clsWebFormUtil.FillListBox(this.lstLeft, dtLeftMenu, "MENU_ID", "MENU_DESCRIPTION",true);
        }
        else
        {
            if (ddlParent.Items.Count > 0)
            {
                DataTable dtLeftMenu = MenuCtl.GetMenu(Constants.IntNullValue, Convert.ToInt32(ddlParent.SelectedValue), 14);
                clsWebFormUtil.FillListBox(this.lstLeft, dtLeftMenu, "MENU_ID", "MENU_DESCRIPTION", true);
            }
        }
    }

    #endregion

}