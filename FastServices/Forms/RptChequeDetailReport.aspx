<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="RptChequeDetailReport.aspx.cs" Inherits="Forms_RptChequeDetailReport" Title="Fast Services: Cheque Detail Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
     <script language="JavaScript" type="text/javascript">
    function ValidateForm()
	{
			
		return true;	  		
	}

    </script>
 <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                        <table>
                            <tr>
                                <td>
                                    <strong><asp:Label id="Label1" runat="server" Width="79px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td>
                                    <asp:DropDownList id="DrpCustomer" runat="server" Width="240px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong><asp:Label id="Label10" runat="server" Width="80px" Text="Status" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td>
                                    <asp:DropDownList id="DrpStatus" runat="server" Width="240px"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong><asp:Label id="Label3" runat="server" Width="75px" Height="13px" Text="From Date"></asp:Label></strong>
                                </td>
                                <td>
                                    <asp:TextBox id="txtStartDate" onkeyup="BlockStartDateKeyPress()" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox> 
                                    <asp:ImageButton id="ibtnStartDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong><asp:Label id="Label4" runat="server" Width="80px" Height="13px" Text="To Date"></asp:Label></strong>
                                </td>
                                <td>
                                    <asp:TextBox id="txtEndDate" onkeyup="BlockEndDateKeyPress()" runat="server" Width="150px" CssClass="txtBox " MaxLength="10"></asp:TextBox> 
                                    <asp:ImageButton id="ibnEndDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif"></asp:ImageButton>
                                    <cc1:CalendarExtender id="CEStartDate" runat="server" TargetControlID="txtStartDate" PopupButtonID="ibtnStartDate" Format="dd-MMM-yyyy">
                                    </cc1:CalendarExtender> 
                                    <cc1:CalendarExtender id="CEEndDate" runat="server" TargetControlID="txtEndDate" PopupButtonID="ibnEndDate" Format="dd-MMM-yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong><asp:Label id="Label2" runat="server" Width="73px" Text="Filter On" CssClass="lblbox"></asp:Label></strong>
                                </td>
                                <td>
                                    <asp:RadioButtonList id="RbReportFilter" runat="server" Width="250px">
                                        <asp:ListItem Selected="True">Recevied Date</asp:ListItem>
                                        <asp:ListItem>Deposit Date</asp:ListItem>
                                        <asp:ListItem>Realized Date</asp:ListItem>
                                        <asp:ListItem>Due Date </asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>

</contenttemplate>
                    </asp:UpdatePanel>
                
        <asp:Button ID="btnViewPDF" runat="server" CssClass="Button"
                Text="View PDF" Width="90" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button"
                Text="View Excel" Width="90" OnClick="btnViewExcel_Click" /></td>
            </tr>
        </table>
           </div>
</asp:Content>
