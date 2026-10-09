<%@ Page Title="Fast Services: Contacts" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmMarketingContactView.aspx.cs" Inherits="Forms_frmMarketingContactView" %>
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

        ///////////Search for Section////////////////////     
        $("#ctl00_ctl00_mainCopy_cphPage_txtSearch").keyup(function (e) {
            $("#tblSection tr:has(td)").hide();
            var iCounter = 0;
            var sSearchTerm = $("#ctl00_ctl00_mainCopy_cphPage_txtSearch").val(); //Get the search box value            
            if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
            {
                $("#tblSection tr:has(td)").show();
                return false;
            }
            $("#tblSection tr:has(td)").children().each(function () {
                var cellText = $(this).text().toLowerCase();
                if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                {
                    $(this).parent().show();
                    iCounter++;
                    return true;
                }
            });
        });        
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
                                            <asp:Literal ID="ltrlContact" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>            
                                            <table width="100%">
                                                <tr>
                                                    <td class="tblhead" style="font-size:medium">
                                                        <asp:TextBox ID="txtSearch" runat="server" style="width: 30%;" placeholder="Enter Text To Search " onkeydown="javascript:return (event.keyCode!=13);">
                                                            </asp:TextBox>
                                                            <asp:LinkButton ID="btnSearch" runat="server" ToolTip="Search" OnClick="btnSearch_Click" >                                                                            
                                                                <img src="../images/view.gif" width="16" height="16">
                                                            </asp:LinkButton>                            
                                                        <asp:Literal ID="ltlAddContact" runat="server"></asp:Literal>
                                                        
                                                    </td>                                
                                                </tr>
                                            </table>                                                
                                            <asp:Repeater ID="rContact" runat="server" 
                                                OnItemCommand="rContact_ItemCommand">      
                                                <HeaderTemplate>
                                                <table style="width: 100%; margin-top: 0px">
                                                <tbody id="tblSection">                                                    
                                                <tr>
                                                        <td class="tblhead" style="width:6%;font-size:medium">No#</td>                                                         
                                                        <td class="tblhead" style="width:20%;font-size:medium">Name</td>
                                                        <td class="tblhead" style="width:15%;font-size:medium">Designation</td>
                                                        <td class="tblhead" style="width:20%;font-size:medium">Company</td>
                                                        <td class="tblhead" style="width:15%;font-size:medium">Cell#</td>
                                                        <td class="tblhead" style="width:20%;font-size:medium">Email ID</td>
                                                        <td class="tblhead" style="width:2%;font-size:x-large">                                                            
                                                            
                                                        </td>
                                                        <td class="tblhead" style="width:2%;font-size:x-large">                                                            
                                                            
                                                        </td>                                                                                                           
                                                </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                    
                                                                    <tr style="background:#EFF3FB">
                                                                        <td style="width:6%;"><%# Eval("CONTACT_NO")%> </td>                                                                        
                                                                        <td style="width:20%;"><%# Eval("CONTACT_NAME")%></td>
                                                                        <td style="width:15%;"><%# Eval("CONTACT_DESIGNATION") %></td>
                                                                        <td style="width:20%;"><%# Eval("CONTACT_COMPANY")%></td>
                                                                        <td style="width:15%;"><%# Eval("CONTACT_CELL")%></td>
                                                                        <td style="width:20%;"><%# Eval("CONTACT_EMAIL")%></td>                                                                        
                                                                        <td style="width:2%;">                                                                            
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt" CommandArgument='<%# Eval("MARKETING_CONTACT_ID") %>' ToolTip="Edit Contact" >                                                                            
                                                                                <img src="../images/edit.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("MARKETING_CONTACT_ID") %>' ToolTip="Delete Contact" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                </ItemTemplate> 
                                                <AlternatingItemTemplate>                                                    
                                                                    <tr style="background:#ffffff">
                                                                        <td style="width:6%;"><%# Eval("CONTACT_NO")%> </td>                                                                        
                                                                        <td style="width:20%;"><%# Eval("CONTACT_NAME")%></td>
                                                                        <td style="width:15%;"><%# Eval("CONTACT_DESIGNATION") %></td>
                                                                        <td style="width:20%;"><%# Eval("CONTACT_COMPANY")%></td>
                                                                        <td style="width:15%;"><%# Eval("CONTACT_CELL")%></td>
                                                                        <td style="width:20%;"><%# Eval("CONTACT_EMAIL")%></td>                                                                        
                                                                        <td style="width:2%;">                                                                            
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edt" CommandArgument='<%# Eval("MARKETING_CONTACT_ID") %>' ToolTip="Edit Contact" >                                                                            
                                                                                <img src="../images/edit.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("MARKETING_CONTACT_ID") %>' ToolTip="Delete Contact" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
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