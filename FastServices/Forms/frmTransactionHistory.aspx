<%@ Page Title="Fast Services: Transaction History" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmTransactionHistory.aspx.cs" Inherits="Forms_frmTransactionHistory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<script language="javascript" type="text/javascript">
    function pageLoad() {
        $('#<%=txtMonth.ClientID %>').datepicker(
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
    <table width="95%">
<tr>
    <td>
        <asp:Literal ID="ltrlHistory" runat="server"></asp:Literal>
    </td>
</tr>
<tr>  
        <td>
            <asp:TextBox ID="txtMonth" runat="server"></asp:TextBox>
            <asp:Button ID="btnView" runat="server" Text="Show" CssClass="Button" 
                onclick="btnView_Click" />
        </td>
    </tr>
    <tr>
    <td>
        <table width="100%" border="0" cellspacing="0" cellpadding="2" > 
    <tr>
            <td class="tblhead" style="width:8%;">
                Type</td>
            <td class="tblhead" style="width:10%;">
               User
            </td>
            <td class="tblhead" style="width:15%;">
                  Entity
                </td>
            <td class="tblhead" style="width:52%;">
               Detail
            </td>
                <td class="tblhead" style="width:15%;">
                   Date Time
            </td>                  
    </tr>
</table>
    <asp:Repeater ID="rTranHistory" runat="server">      
        <ItemTemplate>
            <div style="background:#EFF3FB">
                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width:8%;"><%# Eval("TransactionType")%> </td>
                        <td style="width:10%;"> <%# Eval("TransactionBy")%></td>
                        <td style="width:15%;"><%# Eval("Entity")%></td>
                        <td style="width:52%;"><%# Eval("TransactionDetail")%></td>
                        <td style="width:15%;">
                            <%#DataBinder.Eval(Container.DataItem,"TimeStamp","{0:dd/MM/yyyy hh:mm:ss tt}")%>
                        </td>                                                   
                    </tr>
                </table>                                                            
            </div>
        </ItemTemplate> 
        <AlternatingItemTemplate>
            <div style="background:#ffffff">
                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width:8%;"><%# Eval("TransactionType")%> </td>
                        <td style="width:10%;"> <%# Eval("TransactionBy")%></td>
                        <td style="width:15%;"><%# Eval("Entity")%></td>
                        <td style="width:52%;"><%# Eval("TransactionDetail")%></td>
                        <td style="width:15%;">
                            <%#DataBinder.Eval(Container.DataItem,"TimeStamp","{0:dd/MM/yyyy hh:mm:ss tt}")%>
                        </td>                                                  
                    </tr>
                </table>                                                           
            </div>
        </AlternatingItemTemplate>                                                                                                
    </asp:Repeater>
    <table width="95%">
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
</table> 
    </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>

