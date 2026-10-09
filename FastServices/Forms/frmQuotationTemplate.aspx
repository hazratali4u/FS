<%@ Page Title="Fast Services: Quotation Templates" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmQuotationTemplate.aspx.cs" Inherits="Forms_frmQuotationTemplate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script language="javascript" type ="text/javascript">
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
        <asp:Panel ID="pnlAdd" runat="server" Visible="false">
            <asp:UpdatePanel ID="upEmailTemplate" runat="server">
                <ContentTemplate>
                    <table width="90%">
                        <tr>
                            <td colspan="3">
                                <asp:Literal ID="ltrlTemplate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>Category</strong>
                            </td>
                            <td width="1%">
                            </td>
                            <td width="84%">
                                <asp:DropDownList ID="ddlTemplate" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <strong>Template Name</strong>
                            </td>
                            <td width="1%">
                            </td>
                            <td width="89%">
                                <asp:TextBox ID="txtSubject" runat="server" Width="370px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <strong>Body </strong>
                            </td>
                            <td width="1%">
                            </td>
                            <td width="89%">
                                <FCKeditorV2:FCKeditor ID="txtBody" runat="server" BasePath="~/FCKeditor/" Height="280px"
                                    Width="80%" ToolbarSet="NormalUser">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <strong>Signature</strong>
                            </td>
                            <td width="1%">
                            </td>
                            <td width="89%">
                                <FCKeditorV2:FCKeditor ID="txtSignature" runat="server" BasePath="~/FCKeditor/" Height="180px"
                                    Width="80%" ToolbarSet="NormalUser">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <strong>Terms and Conditions</strong>
                            </td>
                            <td width="1%">
                            </td>
                            <td width="89%">
                                <FCKeditorV2:FCKeditor ID="txtTerms" runat="server" BasePath="~/FCKeditor/" Height="220px"
                                    Width="80%" ToolbarSet="NormalUser">
                                </FCKeditorV2:FCKeditor>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="Button" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="pnlView" runat="server">            
                <table width="90%">
                <tbody>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrlView" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnNewTemplate" runat="server" Text="Add New Template" OnClick="btnNewTemplate_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="tblhead" style="width: 60%;">
                                        Template Name
                                    </td>
                                    <td class="tblhead" style="width: 30%;">
                                        Category
                                    </td>
                                    <td class="tblhead" style="width: 5%;">
                                    </td>
                                    <td class="tblhead" style="width: 5%;">
                                    </td>
                                </tr>
                            </table>
                            <asp:Repeater ID="rTemplate" runat="server" OnItemCommand="rTemplate_ItemCommand"
                                OnItemDataBound="rTemplate_ItemDataBound">
                                <ItemTemplate>
                                    <div style="background: #EFF3FB">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                        <tr>
                                                            <td style="width: 60%;">
                                                                <asp:LinkButton ID="btnTemplateName" runat="server" CommandName="edit" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="View Template">
                                                    <%# Eval("SUBJECT")%> 
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="width: 30%;">
                                                                <%# Eval("SLASH_DESC")%>
                                                                <asp:HiddenField ID="hfQuotation" runat="server" Value='<%# Eval("SLASH_DESC") %>' />
                                                            </td>
                                                            <td style="width: 5%;">
                                                                <asp:LinkButton ID="btnView" runat="server" CommandName="edit" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="View Template">
                                                    <img src="../images/view.gif" height="16">
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%;">
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="Delete Template" OnClientClick="return confirm('Are you sure? you are going to delete this Template.');">
                                                    <img src="../images/delete.gif"  height="16">
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div style="background: #ffffff">
                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                        <tr>
                                                            <td style="width: 60%;">
                                                                <asp:LinkButton ID="btnTemplateName" runat="server" CommandName="edit" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="View Template">
                                                    <%# Eval("SUBJECT")%> 
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="width: 30%;">
                                                                <%# Eval("SLASH_DESC")%>
                                                                <asp:HiddenField ID="hfQuotation" runat="server" Value='<%# Eval("SLASH_DESC") %>' />
                                                            </td>
                                                            <td style="width: 5%;">
                                                                <asp:LinkButton ID="btnView" runat="server" CommandName="edit" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="View Template">
                                                    <img src="../images/view.gif" height="16">
                                                                </asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%;">
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("EMAIL_Template_ID") %>'
                                                                    ToolTip="Delete Template" OnClientClick="return confirm('Are you sure? you are going to delete this Template.');">
                                                    <img src="../images/delete.gif"  height="16">
                                                                </asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                            <table width="100%">
                                <tr>
                                    <td align="right" width="56%">
                                        <asp:LinkButton ID="linkbtnprev" runat="server" Enabled="False" OnClick="linkbtnprev_Click"
                                            CausesValidation="false" Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" Enabled="False" OnClick="linkbtnnext_Click"
                                            CausesValidation="false" Visible="false" Style="color: #6C5A10;">Next</asp:LinkButton>
                                    </td>
                                    <td align="right" width="44%">
                                        <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                        <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                        <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                        <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                        <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>        
        </asp:Panel>
    </div>
</asp:Content>
