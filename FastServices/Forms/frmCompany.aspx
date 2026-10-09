<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmCompany.aspx.cs" Inherits="Forms_frmCompany" Title="Fast Services: Company" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script language="JavaScript" type="text/javascript">
    function pageLoad() {
        $('#<%=txtNTN.ClientID %>').mask("9999999-9");
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
                            <td style="width:5%">
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                            </td>                            
                            <td style="width:95%" colspan="2">
                                <asp:Literal ID="ltrlCompany" runat="server"></asp:Literal>
                            </td>                            
                        </tr>

                        <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblCompany" Text="Name:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtCompany" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblAddress" Text="Address:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtAddress" runat="server" Width="200px" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblPhone" Text="Phone:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtPhone" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblNTN" Text="NTN #:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtNTN" runat="server" MaxLength="9" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblDisplayName" Text="Display Name:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtDisplayName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                <strong><asp:Label ID="lblEmail" Text="Email:" runat="server"></asp:Label></strong>
                            </td>
                            <td style="width:85%">
                               <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:5%"></td>
                            <td style="width:10%">
                                &nbsp;
                            </td>
                            <td style="width:85%">
                               <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" 
                                    onclick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>                                 
            </ContentTemplate>
    </asp:UpdatePanel>
 </div>   
</asp:Content>



