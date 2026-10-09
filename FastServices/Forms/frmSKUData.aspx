<%@ page language="C#" masterpagefile="~/Forms/PageMaster.master" autoeventwireup="true" CodeFile = "frmSKUData.aspx.cs" inherits="Forms_frmSKUData" title="Fast Services: Product Information" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
<div >
         <table width="100%">
             <tr>
                 <td >
                     <asp:UpdatePanel id="UpdatePanel1" runat="server">
                         <contenttemplate>
<table width="100%">
    <tbody>
        <tr>
            <td align="left" colspan="4">
                <asp:Literal ID="ltrlProduct" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="width: 100px" align="left">
                <strong><asp:Label id="Label3" runat="server" Width="100px" Text="Category"></asp:Label></strong>
            </td>
            <td align="left">
                <asp:DropDownList id="ddskucategory" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" onselectedindexchanged="ddskucategory_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>    
        <tr>
            <td style="width: 100px; height: 20px;" align=left>
                <strong><asp:Label id="Label1" runat="server" Text="Code"></asp:Label></strong>
            </td>
            <td align="left">
                <asp:TextBox ID="txtCode" runat="server"  Width="192px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; height: 20px;" align=left>
                <strong><asp:Label id="Label7" runat="server" Text="Name"></asp:Label></strong>
            </td>
            <td align="left">
                <asp:TextBox ID="txtskuname" runat="server" CssClass="txtBox " Width="192px"></asp:TextBox>
            </td>
        </tr>                    
        <tr>
        <td style="width: 100px; height: 12px;"></td>
        <td style="height: 12px;" align="left">
            <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" onclick="btnSave_Click" Text="Save" Width="86px" />
        </td>
    </tr>
    </tbody>
</table>
</contenttemplate>
                     </asp:UpdatePanel><asp:UpdateProgress id="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1"><progresstemplate>
<asp:ImageButton id="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"></asp:ImageButton>&nbsp; Loading.... 
</progresstemplate>
                     </asp:UpdateProgress></td>
             </tr>
         </table>
    
     </div>
<div >
        <asp:UpdatePanel id="UpdatePanel2" runat="server">
            <contenttemplate>
<table class="tblhead">
    <tbody>
        <tr>
            <td style="color:White;font-weight:bold; width:110px">
                <asp:Label id="Label10" runat="server" Width="110px" Text="Searching Type"></asp:Label> 
            </td>
            <td style="width: 150px; height: 22px" align="left">
                <asp:DropDownList id="ddSearchType" runat="server" Width="150px" CssClass="DropList">
                    <asp:ListItem Value="0">All Records</asp:ListItem>
                    <asp:ListItem Value="SKU_CODE">Code</asp:ListItem>
                    <asp:ListItem Value="SKU_NAME">Name</asp:ListItem>
                </asp:DropDownList> 
            </td>
            <td style="width: 200px; height: 22px" align="left">
                <asp:TextBox id="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox> 
           </td>
            <td style="width: 50px; height: 22px" align="left">
                <asp:Button id="btnFilter" runat="server" Width="50px" Text="Filter" OnClick="btnFilter_Click"></asp:Button> 
            </td>
        </tr>
    </tbody>
</table>
<br />
<asp:Panel id="Panel1" runat="server" HorizontalAlign="Left" Width="560px" Height="400px" ScrollBars="Vertical" >
                    <asp:GridView id="grdSKUData" runat="server" Width="535px" ForeColor="SteelBlue" 
                        CssClass="gridRow2" OnPageIndexChanging="grdSKUData_PageIndexChanging" 
                        HorizontalAlign="Center" BorderColor="White" BackColor="White" OnRowDataBound = "grdSKUData_RowDataBound"
                        AutoGenerateColumns="False" OnRowEditing="grdSKUData_RowEditing" 
                        OnRowDeleting="grdSKUData_RowDeleting" EnableModelValidation="True">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="Principal_Id">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Category_Id" HeaderText="Category_Id">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Category" HeaderText="Category">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="110px"></ItemStyle>
<HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
    <asp:BoundField DataField="SKU_CODE" HeaderText="Code">
    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px"></ItemStyle>
    </asp:BoundField>
<asp:BoundField DataField="SKU_NAME" HeaderText="Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="300px"></ItemStyle>
<HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>

<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
<HeaderStyle HorizontalAlign="Left" />
</asp:CommandField>
<asp:TemplateField HeaderText="Delete">
<ItemTemplate>
<asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton> 
</ItemTemplate>
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
<HeaderStyle HorizontalAlign="Left" />
</asp:TemplateField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView> 
                </asp:Panel> 
</contenttemplate>
        </asp:UpdatePanel>


    </div>
</div>
</asp:Content>


