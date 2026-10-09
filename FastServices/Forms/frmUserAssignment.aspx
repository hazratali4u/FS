<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmUserAssignment.aspx.cs" Inherits="Forms_frmUserAssignment" Title="Fast Services: User Assignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
    <script language="JavaScript" type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
        function startRequest(sender, e) {
            document.getElementById('<%=btnAssignLocation.ClientID%>').disabled = true;
            document.getElementById('<%=btnUnAssignLocation.ClientID%>').disabled = true;
            document.getElementById('<%=btnAssignPrincipal.ClientID%>').disabled = true;
            document.getElementById('<%=btnAssignAllPrincipal.ClientID%>').disabled = true;
            document.getElementById('<%=btnUnAssignPrincipal.ClientID%>').disabled = true;
            document.getElementById('<%=btnUnAssignAllPrincipal.ClientID%>').disabled = true;
            document.getElementById('<%=btnUnAssignCustomer.ClientID%>').disabled = true;
            document.getElementById('<%=btnUnAssignCustomerAll.ClientID%>').disabled = true;
            document.getElementById('<%=btnAssignCustomer.ClientID%>').disabled = true;
            document.getElementById('<%=btnAssignCustomerAll.ClientID%>').disabled = true;
        }

        function endRequest(sender, e) {
            document.getElementById('<%=btnAssignLocation.ClientID%>').disabled = false;
            document.getElementById('<%=btnUnAssignLocation.ClientID%>').disabled = false;
            document.getElementById('<%=btnAssignPrincipal.ClientID%>').disabled = false;
            document.getElementById('<%=btnAssignAllPrincipal.ClientID%>').disabled = false;
            document.getElementById('<%=btnUnAssignPrincipal.ClientID%>').disabled = false;
            document.getElementById('<%=btnUnAssignAllPrincipal.ClientID%>').disabled = false;
            document.getElementById('<%=btnUnAssignCustomer.ClientID%>').disabled = false;
            document.getElementById('<%=btnUnAssignCustomerAll.ClientID%>').disabled = false;
            document.getElementById('<%=btnAssignCustomer.ClientID%>').disabled = false;
            document.getElementById('<%=btnAssignCustomerAll.ClientID%>').disabled = false;
        }

        function pageLoad() {
            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }

        function HidePopupPanel(source, args) {
            //find the panel associated with the extender
            objPanel = document.getElementById(source._PopupControlID);

            //check the panel exists
            if (objPanel) {
                //set the display attribute, so it remains hidden on postback
                objPanel.style.display = 'none';
            }
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <div id="right_data">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div>
                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                    <ProgressTemplate>
                        <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                            Width="31px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                    PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
            </div>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td colspan="3">
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="107px" Text="User"></asp:Label>
                                                </strong>
                                                <asp:DropDownList ID="ddUser" runat="server" Width="200px" __designer:wfdid="w62"
                                                    CssClass="DropList" OnSelectedIndexChanged="ddUser_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 345px">
                                <cc1:TabContainer ID="tbUserAssignment" runat="server" Width="550px" Height="260px"
                                    AutoPostBack="True" ActiveTabIndex="0" OnActiveTabChanged="tbUserAssignment_ActiveTabChanged">
                                    <cc1:TabPanel runat="server" ID="TabPanel1">
                                        <HeaderTemplate>
                                            Location Assignment
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                        <td style="width: 90%" valign="top">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="width: 40%" rowspan="4">
                                                                                            <asp:ListBox ID="lstUnAssignDistributor" runat="server" Width="200px" Height="200px"
                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                        <td style="width: 52px" align="center">
                                                                                        </td>
                                                                                        <td style="width: 40%" rowspan="4">
                                                                                            <asp:ListBox ID="lstAssignDistributor" runat="server" Width="200px" Height="200px"
                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                            <asp:Button ID="btnAssignLocation" OnClick="btnAssignLocation_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text=">" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                            <asp:Button ID="btnUnAssignLocation" OnClick="btnUnAssignLocation_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text="<" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 52px" align="center">
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 5%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" TabIndex="1" ID="TabPanel2">
                                        <HeaderTemplate>
                                            Supplier Assignment
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 2%; height: 250px">
                                                        </td>
                                                        <td style="width: 96%; height: 250px" colspan="3">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 265px">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid;
                                                                                            width: 102px; border-bottom: gray thin solid" rowspan="4">
                                                                                            <asp:ListBox ID="lstUnAssignBrand" runat="server" Width="200px" Height="200px" CssClass="DropList"
                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnAssignPrincipal" OnClick="btnAssignPrincipal_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text=">" CssClass="Button" />
                                                                                        </td>
                                                                                        <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid;
                                                                                            width: 112px; border-bottom: gray thin solid" rowspan="4">
                                                                                            <asp:ListBox ID="lstAssignBrand" runat="server" Width="200px" Height="200px" CssClass="DropList"
                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnAssignAllPrincipal" OnClick="btnAssignAllPrincipal_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text=">>" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnUnAssignPrincipal" OnClick="btnUnAssignPrincipal_Click" runat="server"
                                                                                                Width="30px" Font-Size="8pt" Text="<<" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnUnAssignAllPrincipal" OnClick="btnUnAssignAllPrincipal_Click"
                                                                                                runat="server" Width="30px" Font-Size="8pt" Text="<" CssClass="Button" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td rowspan="1">
                                                                                        </td>
                                                                                        <td style="width: 20%; height: 14px">
                                                                                        </td>
                                                                                        <td rowspan="1">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 21px" align="center" colspan="3" rowspan="1">
                                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" HeaderText="TabPanel3" TabIndex="2" ID="TabPanel3">
                                        <HeaderTemplate>
                                            Customer Assignment
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 2%; height: 250px">
                                                        </td>
                                                        <td style="width: 96%; height: 250px" colspan="3">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 265px">
                                                                            <table>
                                                                                <tbody>
                                                                                    <tr>
                                                                                        <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid;
                                                                                            width: 102px; border-bottom: gray thin solid" rowspan="4">
                                                                                            <asp:ListBox ID="lstUnAssignCustomer" runat="server" Width="200px" Height="200px"
                                                                                                SelectionMode="Multiple"></asp:ListBox>
                                                                                        </td>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnAssignCustomer" runat="server" Width="30px" Font-Size="8pt" Text=">"
                                                                                                CssClass="Button" OnClick="btnAssignCustomer_Click" />
                                                                                        </td>
                                                                                        <td style="border-right: gray thin solid; border-top: gray thin solid; border-left: gray thin solid;
                                                                                            width: 112px; border-bottom: gray thin solid" rowspan="4">
                                                                                            <asp:ListBox ID="lstAssignCustomer" runat="server" Width="200px" Height="200px" SelectionMode="Multiple">
                                                                                            </asp:ListBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnAssignCustomerAll" runat="server" Width="30px" Font-Size="8pt"
                                                                                                Text=">>" CssClass="Button" OnClick="btnAssignCustomerAll_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnUnAssignCustomerAll" runat="server" Width="30px" Font-Size="8pt"
                                                                                                Text="<<" CssClass="Button" OnClick="btnUnAssignCustomerAll_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="width: 75px" align="center">
                                                                                            <asp:Button ID="btnUnAssignCustomer" runat="server" Width="30px" Font-Size="8pt"
                                                                                                Text="<" CssClass="Button" OnClick="btnUnAssignCustomer_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td rowspan="1">
                                                                                        </td>
                                                                                        <td style="width: 20%; height: 14px">
                                                                                        </td>
                                                                                        <td rowspan="1">
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="height: 21px" align="center" colspan="3" rowspan="1">                                                                                            
                                                                                        </td>
                                                                                    </tr>
                                                                                </tbody>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </td>
                                                        <td style="width: 2%; height: 250px">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
