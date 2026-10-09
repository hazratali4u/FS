<%@ Page Title="Fast Services: Documnet Types" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmDocumentType.aspx.cs" Inherits="Forms_frmDocumentType" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
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
        <asp:UpdatePanel ID="upDocumentType" runat="server">
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
                    <tr>
                        <td colspan="3">
                            <asp:Literal ID="ltrlDocumentType" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            <strong>Document Type</strong>
                        </td>
                        <td width="1%">
                        </td>
                        <td width="89%">
                            <asp:TextBox ID="txtDocumentType" runat="server" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="480px" Height="300px"
                    ScrollBars="Vertical">
                    <asp:GridView ID="gvDocumentType" runat="server" Width="450px" ForeColor="SteelBlue"
                        OnPageIndexChanging="gvDocumentType_PageIndexChanging" HorizontalAlign="Center"
                        BorderColor="White" BackColor="White" AutoGenerateColumns="False" OnRowEditing="gvDocumentType_RowEditing"
                        OnRowDeleting="gvDocumentType_RowDeleting" EnableModelValidation="True">
                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                            PreviousPageText="Previous"></PagerSettings>
                        <Columns>
                            <asp:BoundField DataField="REF_ID">
                                <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                <ItemStyle CssClass="HidePanel"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SLASH_DESC" HeaderText="Document Type">
                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="300px">
                                </ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="tblhead"></HeaderStyle>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
