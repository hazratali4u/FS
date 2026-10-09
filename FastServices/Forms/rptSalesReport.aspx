<%@ Page Title="Fast Services: Sales Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptSalesReport.aspx.cs" Inherits="Forms_rptSalesReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<script language="javascript" type="text/javascript">
    function pageLoad() {
        $('#<%=txtFrom.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
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


        $('#<%=txtFrom.ClientID %>').focus(function () {
            $(".ui-datepicker-calendar").hide();
            $("#ui-datepicker-div").position({
                my: "center top",
                at: "center bottom",
                of: $(this)
            });
        });

        $('#<%=txtTo.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
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


        $('#<%=txtTo.ClientID %>').focus(function () {
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
                          <strong> <asp:Label ID="lblSubType" runat="server" Text="Category"></asp:Label> </strong>
                        </td>
                        <td style="width:25%">
                          <asp:DropDownList ID="ddlSubType" runat="server" Width="200px">                           
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
                        <td style="width:10%">
                          <strong> <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode"></asp:Label> </strong>
                        </td>
                        <td style="width:25%">                          
                          <asp:CheckBoxList ID="cbPaymentMode" runat="server" RepeatDirection ="Horizontal">                            
                            <asp:ListItem Text="Cheque" Value="18" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Cash" Value="19" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Online Transfer" Value="20" Selected="True"></asp:ListItem>
                          </asp:CheckBoxList>
                        </td>
                        <td style="width:1%">
                                                
                        </td>
                        <td style="width:10%">
                            
                        </td>
                        <td style="width:54%">           
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%">                          
                        </td>
                        <td style="width:25%">
                          <asp:RadioButtonList ID="rblReportType" runat="server" 
                                RepeatDirection="Horizontal" AutoPostBack="True" 
                                onselectedindexchanged="rblReportType_SelectedIndexChanged">
                            <asp:ListItem Text="Monthly" Selected="True" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yearly" Value="1"></asp:ListItem>
                          </asp:RadioButtonList>
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
                                    </td>
                                    <td style="width:45%">
                                    </td>
                                </tr>          
                                <tr>
                                    <td style="width:15%">
                                       <strong><asp:Label ID="lblFromMonth" Text="From Month" runat="server"></asp:Label> </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:TextBox ID="txtFrom" runat="server" ></asp:TextBox>
                                    </td>
                                    <td style="width:5%">                            
                                    </td>
                                    <td style="width:10%">
                                        <strong><asp:Label ID="lblToMonth" Text="To Month" runat="server" ></asp:Label> </strong>
                                    </td>
                                    <td style="width:45%">
                                        <asp:TextBox ID="txtTo" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%">
                                       <strong><asp:Label ID="lblFromYear" Text="From Year" runat="server" ></asp:Label> </strong>
                                    </td>
                                    <td style="width:25%">
                                        <asp:DropDownList ID="ddlFromYear" runat="server" Width="100px">
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
                                    </td>
                                    <td style="width:5%">                            
                                    </td>
                                    <td style="width:10%">
                                        <strong><asp:Label ID="lblToYear" Text="To Year" runat="server" ></asp:Label> </strong>
                                    </td>
                                    <td style="width:45%">
                                        <asp:DropDownList ID="ddlToYear" runat="server" Width="100px">
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
</div>
</asp:Content>

