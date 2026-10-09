<%@ Page Title="Fast Services: Departments" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDepartment.aspx.cs" Inherits="Forms_frmDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
<asp:UpdatePanel ID="upDepartment" runat="server">
<ContentTemplate>
    <table width="100%">
        <tr>
            <td colspan="3">
                <asp:Literal ID="ltrlDepartment" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="width:5%"><strong>Department</strong></td>
            <td width="1%"></td>
            <td width="94%"><asp:TextBox ID="txtDepartment" runat="server" Width="200px"></asp:TextBox> </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" 
                    onclick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <asp:Panel id="Panel1" runat="server" HorizontalAlign="Left" Width="480px" Height="300px" ScrollBars="Vertical" >
                    <asp:GridView id="gvDepartment" runat="server" Width="450px" ForeColor="SteelBlue" 
                         OnPageIndexChanging="gvDepartment_PageIndexChanging" 
                        HorizontalAlign="Center" BorderColor="White" BackColor="White" AutoGenerateColumns="False" OnRowEditing="gvDepartment_RowEditing" 
                        OnRowDeleting="gvDepartment_RowDeleting" EnableModelValidation="True">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="REF_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SLASH_DESC" HeaderText="Department">
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
</ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>

