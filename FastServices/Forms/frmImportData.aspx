<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmImportData.aspx.cs" Inherits="Forms_frmImportData" Title="Fast Services: Import Contacts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td style="width:50%">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Width="175px"></asp:Label>
                </td>
                <td style="width:50%">

                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <asp:FileUpload ID="txtFile" runat="server" Width="287px" />
                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Go" CssClass="Button"
                                        ValidationGroup="vg"/>
                </td>
                <td style="width: 50%"></td>
            </tr>            
        </table>
    </div>
</asp:Content>
