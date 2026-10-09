<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="RptPrincipalWiseExp.aspx.cs" Inherits="Forms_RptPrincipalWiseExp" Title="Fast Services: Petty Expense Summary" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ID" runat="server" ContentPlaceHolderID="cphPage">
    <script language="javascript" type="text/javascript">
        function SelectAllAccountHead() {
            var chkBoxList = document.getElementById('<%= LstAccountHead.ClientID %>');
            var chkBox = document.getElementById('<%= ChbAllAccountHead.ClientID %>');
            if (chkBox.checked == true) {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = true;
                }
            }
            else {
                var chkBoxCount = chkBoxList.getElementsByTagName("input");

                for (var i = 0; i < chkBoxCount.length; i++) {
                    chkBoxCount[i].checked = false;
                }
            }
        }

        function UnCheckSelectAll() {
            var chkBox = document.getElementById('<%= ChbAllAccountHead.ClientID %>');
            var chkBoxList = document.getElementById('<%= LstAccountHead.ClientID %>');
            var chkBoxCount = chkBoxList.getElementsByTagName("input");
            var count = 0;
            for (var i = 0; i < chkBoxCount.length; i++) {
                if (chkBoxCount[i].checked == false) {
                    count += 1;
                }
            }
            if (count > 0) {
                chkBox.checked = false;
            }
            else {
                chkBox.checked = true;
            }
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
        <table width="100%">
            <tr>
                <td>
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
                            <table>
                                <tbody>                                    
                                    <tr>
                                        <td align="left">
                                            &nbsp;
                                        </td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label4" runat="server" Text="Report For"></asp:Label>
                                            </strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true"
                                                OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="Expenses"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Payments"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td align="left" style="width: 90px">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Location" Width="48px"></asp:Label>
                                            </strong>
                                        </td>
                                        <td align="left" style="width: 1px">
                                        </td>
                                        <td align="left" style="width: 204px; height: 25px">
                                            <asp:DropDownList ID="DrpLocation" runat="server" AutoPostBack="True" CssClass="DropList"
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="width: 1px; height: 25px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label7" runat="server" Width="91px" Text="Account Type" Visible="false"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:DropDownList ID="DrpMasterHead" runat="server" Width="200px" CssClass="DropList"
                                                AutoPostBack="True" OnSelectedIndexChanged="DrpMasterHead_SelectedIndexChanged"
                                                Visible="false">
                                                <asp:ListItem Selected="True" Value="55">Administrative Expenses</asp:ListItem>
                                                <asp:ListItem Value="56">Selling Expenses</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="width: 90px; height: 2px" align="left">
                                            <strong>
                                                <asp:Label ID="Label5" runat="server" Width="84px" Text="Account Head" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                        </td>
                                        <td style="width: 204px; height: 2px" align="left">
                                            <asp:CheckBox ID="ChbAllAccountHead" onclick="SelectAllAccountHead()" runat="server"
                                                Text="All Account Head"></asp:CheckBox>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 2px" align="left">
                                        </td>
                                        <td style="height: 2px" align="left" colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" Width="295px" Height="150px" ScrollBars="Vertical"
                                                BorderWidth="1px" BorderStyle="Groove">
                                                <asp:CheckBoxList ID="LstAccountHead" onclick="UnCheckSelectAll()" runat="server">
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                        </td>
                                        <td style="width: 1px; height: 2px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Height="9px" Text="From Date"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtFromDate" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntFromDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                        <td style="width: 90px" align="left">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="55px" Text="To Date" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td style="width: 1px" align="left">
                                        </td>
                                        <td style="width: 204px; height: 25px" align="left">
                                            <asp:TextBox ID="txtToDate" runat="server" Width="150px" CssClass="txtBox" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="ImgBntToDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                                            </asp:ImageButton>
                                        </td>
                                        <td style="width: 1px; height: 25px" align="left">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                PopupButtonID="ImgBntFromDate" EnableViewState="False" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                PopupButtonID="ImgBntToDate" EnableViewState="False" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                            &nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" Width="90"
                        OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcell" runat="server" CssClass="Button" Text="View Excel"
                        Width="90" OnClick="btnViewExcell_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
