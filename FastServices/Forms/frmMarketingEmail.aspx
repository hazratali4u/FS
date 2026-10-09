<%@ Page Title="Fast Services: Send Email" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmMarketingEmail.aspx.cs" Inherits="Forms_frmMarketingEmail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<script language="JavaScript" type="text/javascript">
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
<asp:Panel ID="pnlAdd" runat="server">
<asp:UpdatePanel ID="upEmailTemplate" runat="server">
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
    <table width="70%">
        <tr>
            <td>                
                <asp:Literal ID="ltrlTemplate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td width="100%">
                <asp:DropDownList ID="ddlTemplate" runat="server" Width="25%" Visible="false" AutoPostBack="True" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:TextBox ID="txtTemplate" runat="server" Width="25%"></asp:TextBox>
                <span style="font-size:x-large;">
                    <asp:LinkButton ID="btnAddTemplate" runat="server" style="text-decoration:none;" OnClick="btnAddTemplate_Click">+</asp:LinkButton>
                </span>
            </td>
        </tr>
        
        <tr>
            <td width="100%">
                <FCKeditorV2:FCKeditor ID="txtBody" runat="server" BasePath="~/FCKeditor/" Height="280px" Width="100%" ToolbarSet="NormalUser"> </FCKeditorV2:FCKeditor>                    
            </td>
        </tr>
        <tr>
            <td style="width:100%">
                <table width="100%">
                    <tr>
                        <td style="width:50%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" 
                            onclick="btnSave_Click"  />
                        </td>
                        <td style="width:50%" align="right">
                            <asp:TextBox ID="txtEmailRange" runat="server" Width="80%"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeEmailRange" runat="server" FilterType="Custom"
                                    ValidChars="0123456789-" TargetControlID="txtEmailRange">
                                </cc1:FilteredTextBoxExtender>
                            <asp:Button ID="btnSendEmail" runat="server" Text="Send" CssClass="Button" 
                    onclick="btnSendEmail_Click"  />
                        </td>
                    </tr>
                </table>                
            </td>
        </tr>
    </table>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
</div>
</asp:Content>

