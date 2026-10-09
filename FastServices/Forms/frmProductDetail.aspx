<%@ Page Title="Fast Services: Product Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmProductDetail.aspx.cs" Inherits="Forms_frmProductDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
    <link href="../SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css">
    <script src="../SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function pageLoad() {
            var hfTabIndex = document.getElementById('<%=hfTabIndex.ClientID%>').value;
            var ActiveTabIndex = document.getElementById('<%=ActiveTabIndex.ClientID %>').value;
            var TabbedPanels1 = new Spry.Widget.TabbedPanels("TabbedPanels1");
            if (hfTabIndex == '0') {
                TabbedPanels1.showPanel(0);
            }
            else if (hfTabIndex == '1') {
                TabbedPanels1.showPanel(1);
            }
            else if (hfTabIndex == '2') {
                TabbedPanels1.showPanel(2);
            }
            else if (hfTabIndex == '3') {
                TabbedPanels1.showPanel(3);
            }
            else {
                TabbedPanels1.showPanel(parseInt(ActiveTabIndex));
            }

            $('#<%=pnlAssignProduct.ClientID %>').show();
            $('#<%=pnlEdit.ClientID %>').show();

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
    <style>
        a
        {
            color: #006699;
            text-decoration: underline;
        }
        a:hover
        {
            color: #3399cc;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
    <div id="right_data">
    <asp:UpdatePanel ID="upProgress" runat="server">
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
        </ContentTemplate>
    </asp:UpdatePanel>
        <div id="action">
            <div id="tab_done">
            </div>
            <h2>
                Action
            </h2>
            <div id="TabbedPanels1" class="TabbedPanels">
                <asp:Literal ID="ltrlTabs" runat="server"></asp:Literal>
                <div class="TabbedPanelsContentGroup">
                    <div class="TabbedPanelsContent">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td align="left" colspan="4">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label3" runat="server" Width="100px" Text="Category"></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                                <strong>
                                                                    <asp:Label ID="lblCategory" runat="server" Width="200px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td align="left" rowspan="3">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px; height: 20px;" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label5" runat="server" Text="Code"></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblProductCode" runat="server" Width="200px"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px; height: 20px;" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label7" runat="server" Text="Name"></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                                <asp:Label ID="lblPrductName" runat="server" Width="200px"></asp:Label>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px;" align="left">
                                                            </td>
                                                            <td style="width: 37px">
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px; height: 12px;">
                                                            </td>
                                                            <td style="width: 37px; height: 12px;">
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td style="width: 37px; height: 12px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100px; height: 12px;">
                                                            </td>
                                                            <td style="height: 12px;" align="left" colspan="2">
                                                                <asp:HiddenField ID="hfTabIndex" runat="server" />
                                                            </td>
                                                            <td align="left" style="width: 37px; height: 12px;">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="TabbedPanelsContent">
                        <div>
                            <asp:Panel ID="pnlEdit" runat="server" CssClass="HidePanel">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:Literal ID="ltrlEdit" runat="server"></asp:Literal>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label2" runat="server" Width="100px" Text="Category"></asp:Label></strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="ddskucategory" runat="server" Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td align="left" rowspan="3">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 20px;" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label6" runat="server" Text="Code"></asp:Label></strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtCode" runat="server" CssClass="txtBox " Width="192px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 20px;" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label></strong>
                                                                </td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtskuname" runat="server" CssClass="txtBox " Width="192px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <tr>
                                                                    <td style="width: 100px; height: 20px;" align="left">
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="ChIsActive" runat="server" Text="Is Active" Checked="True"></asp:CheckBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <td style="width: 100px;" align="left">
                                                                </td>
                                                                <td style="width: 37px">
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 12px;">
                                                                </td>
                                                                <td style="width: 37px; height: 12px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td style="width: 37px; height: 12px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px; height: 12px;">
                                                                </td>
                                                                <td style="height: 12px;" align="left" colspan="2">
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                                                                        Text="Save" Width="86px" />
                                                                </td>
                                                                <td align="left" style="width: 37px; height: 12px;">
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                <ProgressTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" Width="26px" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif">
                                                    </asp:ImageButton>&nbsp; Loading....
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:HiddenField ID="hfEditIndex" runat="server" />
                        </div>
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlAssignProduct" runat="server" CssClass="HidePanel">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="width: 82px;">
                                                                <asp:Label ID="lblmsg" runat="server" Visible="False" Width="114px" ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 17px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <strong>
                                                                    <asp:Label ID="Label1" runat="server" Width="107px" Text="Product" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlProduct" runat="server" Width="200px" CssClass="DropList">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="195px" CssClass="txtBox "
                                                                    Enabled="False"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 182px">
                                                                <table>
                                                                    <tbody>
                                                                        <tr>
                                                                            <td>
                                                                                <strong>UnAssigned</strong>
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <strong>Assigned</strong>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td rowspan="4" style="width: 100px">
                                                                                <asp:ListBox ID="lstUnAssignModule" runat="server" Height="200px" Width="150px" SelectionMode="Multiple">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnAssign" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnAssign_Click"
                                                                                    Text="&gt;" Width="30px" />
                                                                            </td>
                                                                            <td rowspan="4" style="width: 102px">
                                                                                <asp:ListBox ID="lstAssignModule" runat="server" Height="200px" Width="150px" SelectionMode="Multiple">
                                                                                </asp:ListBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnAssignAll" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnAssignAll_Click"
                                                                                    Text="&gt;&gt;" Width="30px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:Button ID="btnUnAssignAll" runat="server" CssClass="Button" Font-Size="8pt"
                                                                                    OnClick="btnUnAssignAll_Click" Text="&lt;&lt;" Width="30px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="center" style="width: 75px">
                                                                                <asp:Button ID="btnUnAssign" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnUnAssign_Click"
                                                                                    Text="&lt;" Width="30px" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td rowspan="1">
                                                                            </td>
                                                                            <td style="width: 75px;">
                                                                            </td>
                                                                            <td rowspan="1">
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" width="10%">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hfSupplierIndex" runat="server" />
                    </div>
                </div>
                <asp:HiddenField ID="ActiveTabIndex" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
