<%@ Page Title="Fast Services: Enquiry View" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmEnquiryView.aspx.cs" Inherits="Forms_frmQutationSupplier" %>
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
        $('#<%=txtDate.ClientID %>').datepicker(
                {
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });

        $('#<%=txtMonth.ClientID %>').datepicker(
                {
                    dateFormat: 'MM yy',
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,

                    onClose: function (dateText, inst) {
                        var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                        var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                        $(this).val($.datepicker.formatDate('MM yy', new Date(year, month, 1)));
                    }
                });


        $('#<%=txtMonth.ClientID %>').focus(function () {
            $(".ui-datepicker-calendar").hide();
            $("#ui-datepicker-div").position({
                my: "center top",
                at: "center bottom",
                of: $(this)
            });
        });

        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
    }

            function ShowDateCalander() {
                $("#divDate").show();
                $("#divMonth").hide();
                $("#divYear").hide();
                $('#<%=hfDateType.ClientID %>').val('D');
                $('#<%=txtDate.ClientID %>').focus();
            }

            function ShowMonthCalander() {
                $("#divDate").hide();
                $("#divMonth").show();
                $("#divYear").hide();
                $('#<%=hfDateType.ClientID %>').val('M');
                $('#<%=txtMonth.ClientID %>').focus();
            }

            function ShowYearCalander() {
                $("#divDate").hide();
                $("#divMonth").hide();
                $("#divYear").show();
                $('#<%=hfDateType.ClientID %>').val('Y');
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
                            <asp:Panel ID="pnEnquiryList" runat="server" Width="95%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="ltrlEnquiry" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="rblCustomerType" runat="server" 
                                                RepeatDirection="Horizontal" AutoPostBack="true" 
                                                onselectedindexchanged="rblCustomerType_SelectedIndexChanged">
                                                <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Leads" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Customers" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <th class="tblhead">
                                                        <td class="tblhead" style="width:53%;">
                                                            Category <asp:DropDownList ID="ddlCategory" runat="server" Width="90px"></asp:DropDownList>
                                                            Customer Name <asp:TextBox ID="txtCustomer" runat="server" Width="150px" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" onclick="btnFilter_Click"/>
                                                         </td>
                                                         <td class="tblhead" style="width:22%;" align="right">
                                                            <div id="divDate">
                                                                Enquiry Date <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                            <div id="divMonth" style="display:none;">
                                                                Enquiry Month <asp:TextBox ID="txtMonth" runat="server" Width="100px"></asp:TextBox>
                                                            </div>
                                                             <div id="divYear" style="display:none;">
                                                                Enquiry Year <asp:DropDownList ID="ddlYear" runat="server">
                                                                                <asp:ListItem Text="2006" Value="2006"></asp:ListItem>
                                                                                <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                                                                <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                                                                <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                                                                <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                                                                <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                                                                <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                                                                <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                                                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                                                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                                                                <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                                                <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                                                <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                                                <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                                                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>                                                                                
                                                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                                                <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                                                                                <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                                                                                <asp:ListItem Text="2026" Value="2026"></asp:ListItem>
                                                                                <asp:ListItem Text="2027" Value="2027"></asp:ListItem>
                                                                                <asp:ListItem Text="2028" Value="2028"></asp:ListItem>
                                                                                <asp:ListItem Text="2029" Value="2029"></asp:ListItem>
                                                                                <asp:ListItem Text="2030" Value="2030"></asp:ListItem>
                                                                                <asp:ListItem Text="2031" Value="2031"></asp:ListItem>
                                                                                <asp:ListItem Text="2032" Value="2032"></asp:ListItem>
                                                                                <asp:ListItem Text="2033" Value="2033"></asp:ListItem>
                                                                                <asp:ListItem Text="2034" Value="2034"></asp:ListItem>
                                                                                <asp:ListItem Text="2035" Value="2035"></asp:ListItem>
                                                                             </asp:DropDownList>
                                                            </div>
                                                            </td>
                                                            <td valign="middle" class="tblhead" style="width:8%;">
                                                            <span class="open"> 
                                                             <img src="images/calendar.png" />
                                                                <div style="background-color: #f5f5f6; border: solid 1px #a0a0a0; width:100px; margin-top:-10px;">                                                                     
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowDateCalander();">Date</a>                                                                        
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowMonthCalander();">Month</a>
                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="ShowYearCalander();">Year</a>                                                                              
                                                                </div>
                                                             </span>
                                                            <asp:Button ID="btnFilterDate" runat="server" Text="Filter" onclick="btnFilterDate_Click" />  
                                                            <asp:HiddenField ID="hfDateType" Value = "D" runat="server"/>
                                                            </div>                                                              
                                                         </td>
                                                         <td align="right" class="tblhead" style="width:17%;">
                                                           Status <asp:DropDownList ID="dllEnquiryStatus" runat="server" AutoPostBack="True" Width="90px" 
                                                                 onselectedindexchanged="dllEnquiryStatus_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            
                                                        </td>                                                        
                                                    </th>
                                                </tr>
                                            </table>  
                                            <br />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="2" > 
                                                <tr>
                                                    <th class="tblhead">
                                                        <td class="tblhead" style="width:5%;">
                                                            ID</td>
                                                         
                                                        <td class="tblhead" style="width:25%;">
                                                            <asp:LinkButton ForeColor="White" ID="btnCustomerSor" runat="server" 
                                                                Text="Customer Name" onclick="btnCustomerSort_Click">
                                                            </asp:LinkButton>
                                                        </td>
                                                        <td class="tblhead" style="width:10%;">
                                                            Category</td>
                                                        <td class="tblhead" style="width:10%;">
                                                             <asp:LinkButton ForeColor="White" ID="btnDate" runat="server" 
                                                                    Text="Enquiry Date" onclick="btnDateSort_Click">
                                                                </asp:LinkButton>
                                                         </td>
                                                        <td class="tblhead" style="width:15%;">
                                                            Updated By
                                                        </td>
                                                         <td class="tblhead" style="width:15%;">
                                                             <asp:LinkButton ForeColor="White" ID="btnStatus" runat="server" 
                                                                    Text="Status" onclick="btnStatusSort_Click">
                                                                </asp:LinkButton>
                                                        </td>
                                                        <td class="tblhead" style="width:15%;">
                                                         <asp:LinkButton ForeColor="White" ID="btnLastUpdate" runat="server" 
                                                            Text="Last Update" onclick="btnLastUpdate_Click">
                                                        </asp:LinkButton>                                                            
                                                        </td>
                                                        <td class="tblhead" style="width:2.5%;"></td>
                                                        <td class="tblhead" style="width:2.5%;"></td>
                                                    </th>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rEnquiryPrice" runat="server" 
                                                OnItemCommand="rEnquiryPrice_ItemCommand" 
                                                onitemdatabound="rEnquiryPrice_ItemDataBound">      
                                                <ItemTemplate>
                                                    <div style="background:#EFF3FB">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width:5%;"><%# Eval("ENQUIREY_MASTER_ID")%> </td>                                                                        
                                                                        <td style="width:25%;">
                                                                            <asp:LinkButton ID="btnCustomer" runat="server" CommandName="view" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="View Enquiry">
                                                                                <%# Eval("CUSTOMER")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width:10%;"><%# Eval("Category") %></td>
                                                                        <td style="width:10%;"><%# DataBinder.Eval(Container.DataItem, "DOCUMENT_DATE", "{0:dd/MM/yyyy}")%> </td>
                                                                        <td style="width:15%;"><%# Eval("NoOfProducts")%></td>
                                                                        <td style="width:15%;"><%# Eval("Status")%></td>
                                                                        <td style="width:15%;"><%# DataBinder.Eval(Container.DataItem, "LAST_UPDATE", "{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                                        <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="View Enquiry">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="Delete Enquiry" >                                                                            
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnRestore" runat="server" CommandName="restore" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="Restore Enquiry" Visible="false">
                                                                            <img src="../images/left_navi-icon1_off.png" width="11" height="11">
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hfStatusID" runat="server" Value='<%# Eval("Status")%>' />
                                                                        </td>

                                                                    </tr>
                                                                </table>                                                            
                                                    </div>
                                                </ItemTemplate> 
                                                <AlternatingItemTemplate>
                                                    <div style="background:#ffffff">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width:5%;"><%# Eval("ENQUIREY_MASTER_ID")%> </td>
                                                                        <td style="width:25%;">
                                                                            <asp:LinkButton ID="btnCustomer" runat="server" CommandName="view" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="View Enquiry">
                                                                                <%# Eval("CUSTOMER")%>
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width:10%;"><%# Eval("Category") %></td>
                                                                        <td style="width:10%;"><%# DataBinder.Eval(Container.DataItem, "DOCUMENT_DATE", "{0:dd/MM/yyyy}")%> </td>
                                                                        <td style="width:15%;"><%# Eval("NoOfProducts")%></td>
                                                                        <td style="width:15%;"><%# Eval("Status")%></td>
                                                                        <td style="width:15%;"><%# DataBinder.Eval(Container.DataItem, "LAST_UPDATE", "{0:dd/MM/yyyy HH:mm:ss}")%></td>
                                                                        <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="View Enquiry">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                         <td style="width:2.5%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="Delete Enquiry">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnRestore" runat="server" CommandName="restore" CommandArgument='<%# Eval("ENQUIREY_MASTER_ID") %>' ToolTip="Restore Enquiry" Visible="false">
                                                                            <img src="../images/left_navi-icon1_off.png" width="11" height="11">
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hfStatusID" runat="server" Value='<%# Eval("Status")%>' />
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
           </div>
    <div >
</div>
</div>
</asp:Content>

