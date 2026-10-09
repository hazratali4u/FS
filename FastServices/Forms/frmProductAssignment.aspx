<%@ page language="C#" masterpagefile="~/Forms/PageMaster.master" autoeventwireup="true"  CodeFile = "frmProductAssignment.aspx.cs" inherits="frmProductAssignment" title="Fast Services: Product Assignment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
    <div id="right_data">
        <table width="100%">
            <tr>
                <td>
                
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 82px; "><asp:Label id="lblmsg" runat="server" Visible="False" Width="114px" ForeColor="Red"></asp:Label></TD><TD>&nbsp; </TD><TD style="HEIGHT: 17px"></TD></TR><TR><TD>
<strong> <asp:Label id="Label1" runat="server" Width="107px" Text="Product" 
        CssClass="lblbox"></asp:Label></strong></TD><TD>
        <asp:DropDownList id="ddlProduct" runat="server" Width="200px" 
            CssClass="DropList" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" 
            AutoPostBack="True"></asp:DropDownList> </TD><TD>
    &nbsp;</TD></TR><TR><TD></TD>
        <td>
            </td>
        <td>
            &nbsp;</td>
    </TR>
    <tr>
        <td colspan="2" style="HEIGHT: 182px">
            <table>
                <tbody>
                    <tr>
                        <td><strong>UnAssigned</strong></td>
                        <td>&nbsp;</td>
                        <td><strong>Assigned</strong></td>
                    </tr>
                    <tr>
                        <td rowspan="4" style="WIDTH: 100px">
                            <asp:ListBox ID="lstUnAssignModule" runat="server" 
                                Height="200px" Width="150px" SelectionMode="Multiple"></asp:ListBox>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnAssign" runat="server" CssClass="Button" Font-Size="8pt" 
                                onclick="btnAssign_Click" Text="&gt;" Width="30px" />
                        </td>
                        <td rowspan="4" style="WIDTH: 102px">
                            <asp:ListBox ID="lstAssignModule" runat="server" 
                                Height="200px" Width="150px" SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnAssignAll" runat="server" CssClass="Button" Font-Size="8pt" 
                                onclick="btnAssignAll_Click" Text="&gt;&gt;" Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnUnAssignAll" runat="server" CssClass="Button" 
                                Font-Size="8pt" onclick="btnUnAssignAll_Click" Text="&lt;&lt;" Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="WIDTH: 75px">
                            <asp:Button ID="btnUnAssign" runat="server" CssClass="Button" Font-Size="8pt" 
                                onclick="btnUnAssign_Click" Text="&lt;" Width="30px" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="1">
                        </td>
                        <td style="WIDTH: 75px;">
                        </td>
                        <td rowspan="1">
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
    </TBODY></TABLE>
</ContentTemplate>
                    </asp:UpdatePanel>
                </td>
              
            </tr>
            <tr>
                <td align="left" width="10%">
                    &nbsp;</td>
            </tr>
           
        </table>
      
       
        </div>
</asp:Content>

