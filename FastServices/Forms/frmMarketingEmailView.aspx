<%@ Page Title="Fast Services: Email Log" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmMarketingEmailView.aspx.cs" Inherits="Forms_frmMarketingEmailView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<style type="text/css">        
    .open div
    {
        display: none;
        float: left;
        position: absolute;
        padding-top:10;
        padding-left: 5px;
        padding-right: 10px;
    }
    .open:hover div
    {
        display: block;
    }
    .open div a
    {
        display: block;
        height: 25px;
    }
    .open div a:hover
    {
        text-decoration: none;
    }
</style>
<script language="javascript" type="text/javascript">
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">

<div id="right_data">
    <div >
        <table width="95%">
            <tr>               
                <td>
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
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
                            <asp:Panel ID="pnlContactView" runat="server" Width="95%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>            
                                            <asp:Repeater ID="rContact" runat="server">      
                                                <HeaderTemplate>
                                                <table style="width: 100%; margin-top: 0px">
                                                <tbody id="tblSection">                                                    
                                                <tr>
                                                        <td class="tblhead" style="width:40%;font-size:medium">Template</td>                                                         
                                                        <td class="tblhead" style="width:20%;font-size:medium">Date</td>
                                                        <td class="tblhead" style="width:20%;font-size:medium">Range</td>
                                                        <td class="tblhead" style="width:20%;font-size:medium">Email Status</td>                                                        
                                                </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                    
                                                    <tr style="background:#EFF3FB">
                                                        <td style="width:40%;"><%# Eval("TEMPLATE_NAME")%> </td>                                                                        
                                                        <td style="width:20%;"><%# DataBinder.Eval(Container.DataItem, "DOCUMENT_DATE", "{0:ddd dd MMM yyyy}")%></td>
                                                        <td style="width:20%;"><%# Eval("Range")%></td>                                                        
                                                        <td style="width:20%;"><%# Eval("EmailSent")%></td>                                                                        
                                                    </tr>
                                                </ItemTemplate> 
                                                <AlternatingItemTemplate>                                                    
                                                    <tr style="background:#ffffff">
                                                        <td style="width:40%;"><%# Eval("TEMPLATE_NAME")%> </td>                                                                        
                                                        <td style="width:20%;"><%# DataBinder.Eval(Container.DataItem, "DOCUMENT_DATE", "{0:ddd dd MMM yyyy}")%></td>
                                                        <td style="width:20%;"><%# Eval("Range")%></td>                                                        
                                                        <td style="width:20%;"><%# Eval("EmailSent")%></td
                                                    </tr>
                                                </AlternatingItemTemplate>         
                                                <FooterTemplate>
                                                </table>
                                                </FooterTemplate>                                                                                       
                                            </asp:Repeater>
                                            <table width="100%">
                                            <tr>
                                                <td align="right" width="56%">
                                                        <asp:LinkButton ID="linkbtnprev" runat="server" 
                                                  Enabled="False" onclick="linkbtnprev_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Previous</asp:LinkButton>
                                                        &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" 
                                                  Enabled="False" onclick="linkbtnnext_Click" CausesValidation="false" Visible="false" style="color: #6C5A10;">Next</asp:LinkButton>
                                                </td>
                                                <td align="right" width="44%">
                                                <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                    <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" style="color: #6C5A10;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>                        
                        </contenttemplate>
                    </asp:UpdatePanel>
                </td>               
            </tr>
        </table>
           </div>
    <div >
</div>
</div>
</asp:Content>