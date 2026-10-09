<%@ Page Title="Fast Services: Designations" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDesignation.aspx.cs" Inherits="Forms_frmDesignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
<asp:UpdatePanel ID="upDesignation" runat="server">
<ContentTemplate>
    <table width="100%">
        <tr>
            <td colspan="3">
                <asp:Literal ID="ltrlDesignation" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td style="width:5%"><strong>Designation</strong></td>
            <td width="1%"></td>
            <td width="94%"><asp:TextBox ID="txtDesignation" runat="server" Width="200px"></asp:TextBox> </td>
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
                    <asp:GridView id="gvDesignation" runat="server" Width="450px" ForeColor="SteelBlue" 
                         OnPageIndexChanging="gvDesignation_PageIndexChanging" 
                        HorizontalAlign="Center" BorderColor="White" BackColor="White" 
                        AutoGenerateColumns="False" OnRowEditing="gvDesignation_RowEditing" 
                        OnRowDeleting="gvDesignation_RowDeleting" EnableModelValidation="True" >
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="REF_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>
<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SLASH_DESC" HeaderText="Designation">
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

