<%@ Page Title="Fast Services: Recurring Invoices" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptRecurringInvoice.aspx.cs" Inherits="Forms_rptRecurringInvoice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
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
   <asp:UpdatePanel ID="upReport" runat="server">
    <ContentTemplate>
    <table width="100%">
                <tbody>
                    <tr>
                        <td colspan="5">
                           <asp:Literal ID="ltrlReport" runat="server"></asp:Literal>
                        </td>                        
                    </tr>                    
                     <tr>
                        <td style="width:10%">
                          <strong> <asp:Label ID="lblRecurringType" runat="server" Text="Report Type"></asp:Label></strong>
                        </td>
                        <td style="width:25%">
                          <asp:DropDownList ID="ddlRecurringType" runat="server" Width="200px">
                          </asp:DropDownList>
                        </td>
                        <td style="width:1%">
                                                
                        </td>
                        <td style="width:10%">
                            
                        </td>
                        <td style="width:54%">           
                        </td>
                    </tr>                                                            
                    <tr>
                        <td colspan="5">
                          <asp:Panel ID="pnlFilter" GroupingText="Filter Options" runat="server" Width="70%">
                            <table width="100%">
                                <tr>
                                    <td style="width:15%">
                                       <strong><asp:Label ID="lblCustomer" Text="Customer" runat="server"></asp:Label> </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                    <td style="width:5%">
                            
                                    </td>
                                    <td style="width:10%">
                                        <strong><asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label> </strong>
                                    </td>
                                    <td style="width:45%">
                                        <asp:DropDownList ID="ddlCategory" runat="server" Width="200px"></asp:DropDownList>
                                    </td>
                                </tr>                                                         
                            </table>
                        </asp:Panel>  
                        </td>
                    </tr>                   
                </tbody>
            </table>
    </ContentTemplate>
   </asp:UpdatePanel>
                        
            <table width="100%">
                <tr>
                        <td style="width:10%">
                        </td>
                        <td style="width:25%" align="right">
                          <asp:Button ID="btnShowReport" runat="server" Text="Generate Report" CssClass="button" onclick="btnShowReport_Click" />                                                                
                        </td>
                        <td style="width:1%">                                                
                        </td>
                        <td style="width:10%">
                            <asp:Button ID="btnExcel" runat="server" Text="Export to Excel" CssClass="button" onclick="btnExcel_Click" />
                        </td>
                        <td style="width:54%">                            
                            
                        </td>
                    </tr>
            </table>           
            <br />  
            <asp:UpdatePanel ID="upGrids" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvNotification" runat="server" AutoGenerateColumns="False" 
                    Width="95%" CellPadding="2" CellSpacing="0" BorderColor="White" ForeColor="SteelBlue" ShowHeader="true" RowStyle-ForeColor="#525252" >       
                    <Columns>
                         <asp:BoundField DataField="Customer" HeaderText="Customer">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="250px"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                                  
                         <asp:BoundField DataField="SKU_NAME" HeaderText="Product">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="150px"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>    
                        <asp:BoundField DataField="REMARKS" HeaderText="Remarks">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="150px"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="PAYMENTTYPE" HeaderText="Type">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>    

                         <asp:BoundField DataField="QUANTITY" HeaderText="Qty">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>      
                         <asp:BoundField DataField="RATE" HeaderText="Price">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField> 
                         <asp:BoundField DataField="Amount" HeaderText="Amount">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField> 
                         <asp:BoundField DataField="SUBSCRIPTION_DATE" HeaderText="Subscription Date" DataFormatString="{0:d/MM/yyyy}" HtmlEncode="false">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>                   
                        <asp:BoundField DataField="SUBSCRIPTION_ENDDATE" HeaderText="End Date" DataFormatString="{0:d/MM/yyyy}" HtmlEncode="false">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>                        

                        <asp:BoundField DataField="IsRed" HeaderText="IsRed" >
                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                            <HeaderStyle CssClass="HidePanel"/>
                        </asp:BoundField>

                    </Columns>
                    <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle"  />
                </asp:GridView>                                                                
                </ContentTemplate>
            </asp:UpdatePanel>                      
</div>
</asp:Content>

