<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChangePassword.aspx.cs" Inherits="Forms_fmChangePassword" Title="Fast Services: My Settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="cphHeadPage">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".divPass").hide();
            $(".divEmail").hide();

            $(".PassParagraph").live("click", function () {
                $(this).next(".divPass").slideToggle(600);
                $(".divEmail").hide();
            });

            $(".EmailParagraph").live("click", function () {
                $(this).next(".divEmail").slideToggle(600);
                $(".divPass").hide();
            });
        });

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
</asp:Content>
<asp:Content ID="content1" runat="server" ContentPlaceHolderID="cphPage">
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
        <table width="90%">
            <tr>
                <td>
                    <p class="PassParagraph">
                        &nbsp;Change Password</p>
                    <div class="divPass">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Literal ID="ltrlPassword" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px; width: 15%" align="left">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="122px" Text="Current Password" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtCurrentPassword" runat="server" MaxLength="20" TextMode="Password"
                                                    Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <strong>
                                                    <asp:Label ID="lbltoLocation" runat="server" Width="121px" Text="New Password" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txtBox" MaxLength="20"
                                                    TextMode="Password" Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="143px" Text="Confirm New Password" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtConfirmNewPassword" runat="server" CssClass="txtBox" MaxLength="20"
                                                    TextMode="Password" Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnSave" runat="server" CssClass="Button" Text="Save" OnClick="btnSave_Click">
                                                </asp:Button>
                                                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" CssClass="Button"
                                                    Text="Cancel"></asp:Button>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <p class="EmailParagraph">
                        &nbsp;Email Settings</p>
                    <div class="divEmail">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Literal ID="ltrlEmail" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px; width: 15%" align="left">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="122px" Text="Email Account"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtEmailAccount" ReadOnly="true" runat="server" Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label4" runat="server" Width="121px" Text="Account Password"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtAccountPassword" runat="server" TextMode="Password" Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="left">
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label5" runat="server" Width="143px" Text="SMTP Address"></asp:Label></strong>
                                            </td>
                                            <td style="height: 30px" align="left">
                                                <asp:TextBox ID="txtSMTP" runat="server" ReadOnly="true" Width="153px"></asp:TextBox>
                                            </td>
                                            <td style="height: 30px" align="left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnSaveEmail" runat="server" OnClick="btnSaveEmail_Click" CssClass="Button"
                                                    Text="Save"></asp:Button>
                                                <asp:Button ID="btnCancelEmail" runat="server" OnClick="btnCancelEmail_Click" CssClass="Button"
                                                    Text="Cancel"></asp:Button>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
