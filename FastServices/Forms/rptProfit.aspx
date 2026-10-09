<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptProfit.aspx.cs"
    Inherits="Forms_rptProfit" MasterPageFile="~/Forms/PageMaster.master"
    Title="Fast Services: Category Wise Profit Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table>
                                <tbody>                                    
                                    <tr>
                                        <td style="width: 1px; height: 1px" align="left">
                                        </td>
                                        <td style="width: 29px; height: 1px" align="left">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="48px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 1px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 1px" align="left">
                                            <asp:DropDownList ID="DrpLocation" runat="server" Width="210px" CssClass="DropList"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 1px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px;" align="left">
                                        </td>
                                        <td style="width: 29px; height: 25px;" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Category" Width="61px"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px;" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:DropDownList ID="ddlCategory" runat="server" Width="210px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Text="From Date"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtBox" MaxLength="10" Width="153px"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromCalc" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" />
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 29px; height: 25px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="57px" Text="To  Date" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                        <td style="width: 203px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="153px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgBntFromCalc" TargetControlID="txtFromDate">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" EnableViewState="False"
                                Format="dd-MMM-yyyy" PopupButtonID="ImgToDate" TargetControlID="txtToDate">
                            </cc1:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
