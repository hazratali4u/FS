<%@ Page Title="Fast Services: Product View" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmProductView.aspx.cs" Inherits="Forms_frmProductView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                            <asp:Panel ID="pnEnquiryList" runat="server" Width="90%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td> <a href="frmSKUData.aspx?LevelID=13&LevelType=2">Add New Product</a> </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <th class="tblhead">
                                                        <td class="tblhead" style="width:10%;">
                                                        <asp:LinkButton ForeColor="White" ID="btnProductIDSor" runat="server" 
                                                            Text="Product ID" onclick="btnProductIDSor_Click">
                                                        </asp:LinkButton>                                                            
                                                         </td>
                                                         <td class="tblhead" style="width:20%;">
                                                         <asp:LinkButton ForeColor="White" ID="btnCategorySor" runat="server" 
                                                            Text="Category" onclick="btnCategorySor_Click">
                                                        </asp:LinkButton>
                                                             <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" 
                                                                 onselectedindexchanged="ddlType_SelectedIndexChanged">                                                                
                                                            </asp:DropDownList>
                                                          </td>
                                                        <td class="tblhead" style="width:55%;">
                                                            <asp:LinkButton ForeColor="White" ID="btnCustomerSor" runat="server" 
                                                                Text="Product Name" onclick="btnCustomerSort_Click">
                                                            </asp:LinkButton>
                                                            <asp:TextBox Width="150px" runat="server" ID="txtFilter" ></asp:TextBox>
                                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" 
                                                                onclick="btnFilter_Click" />
                                                        </td>
                                                        <td class="tblhead" style="width:10%;">
                                                        <asp:LinkButton ForeColor="White" ID="btnDateAddedSor" runat="server" 
                                                            Text="Date Added" onclick="btnDateAddedSor_Click">
                                                        </asp:LinkButton>                                                                
                                                        </td>
                                                        <td class="tblhead" colspan="3" style="width:5%;"></td>
                                                    </th>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rEnquiryPrice" runat="server" OnItemCommand="rEnquiryPrice_ItemCommand" OnItemDataBound="rEnquiryPrice_ItemDataBound">      
                                                <ItemTemplate>
                                                    <div style="background:#EFF3FB">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                       <td style="width:10%;"><%# Eval("SKU_ID")%> </td>
                                                                        <td style="width:20%;"><%# Eval("Category")%></td>
                                                                        <td style="width:55%;">
                                                                        
                                                                        <asp:LinkButton ID="btnProduct" runat="server" CommandName="view" CommandArgument='<%# Eval("SKU_ID") %>' ToolTip="View Product">
                                                                                <%# Eval("SKU_NAME")%>
                                                                            </asp:LinkButton>
                                                                        
                                                                        </td>                                                                        
                                                                        <td style="width:10%;"><%# Eval("TIME_STAMP")%></td>

                                                                        <td style="width:5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("SKU_ID") %>' ToolTip="View Product">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                    </tr>
                                                                </table>                                                            
                                                    </div>
                                                </ItemTemplate> 
                                                <AlternatingItemTemplate>
                                                    <div style="background:#ffffff">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                       <td style="width:10%;"><%# Eval("SKU_ID")%> </td>
                                                                        <td style="width:20%;"><%# Eval("Category")%></td>
                                                                        <td style="width:55%;">
                                                                        
                                                                         <asp:LinkButton ID="btnProduct" runat="server" CommandName="view" CommandArgument='<%# Eval("SKU_ID") %>' ToolTip="View Product">
                                                                                <%# Eval("SKU_NAME")%>
                                                                            </asp:LinkButton>
                                                                        
                                                                        </td>
                                                                        <td style="width:10%;"><%# Eval("TIME_STAMP")%></td>
                                                                        <td style="width:5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("SKU_ID") %>' ToolTip="View Product">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                    </tr>
                                                                </table>                                                           
                                                    </div>
                                                </AlternatingItemTemplate>                                                                                                
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
        <asp:HiddenField ID="hfgvEnquiryRows" runat="server" />
           </div>
    <div >
</div>
</div>
</asp:Content>

