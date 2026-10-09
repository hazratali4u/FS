<%@ Page Title="Fast Services: Customer Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptCustomerDetail.aspx.cs" Inherits="Forms_rptCustomerDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">        
      <table width="100%" cellpadding="5" cellspacing="5">
        <tr>
            <td style="width:10%">
                <strong><asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label> </strong>
            </td>
            <td style="width:90%">
                <asp:DropDownList ID="ddlCategory" runat="server" Width="200px"></asp:DropDownList>
            </td>
        </tr>
          <tr>
            <td style="width:10%">
            </td>
            <td style="width:90%">
                <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" CssClass="button" onclick="btnViewPDF_Click" />
                <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="button" onclick="btnViewExcel_Click" />
            </td>
        </tr>                                                             
    </table>            
            
</div>
</asp:Content>

