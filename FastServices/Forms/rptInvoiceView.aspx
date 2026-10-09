<%@ Page Title="Fast Services: Sales Report" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="rptInvoiceView.aspx.cs" Inherits="rptInvoiceView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
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
                <table width="100%" cellpadding="3" cellspacing="3">
                    <tbody>
                        <tr>
                            <td colspan="5">
                                <asp:Literal ID="ltrlInvoice" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="lblInvoiceType" runat="server" Text="Invoice Type"></asp:Label>
                                </strong>
                            </td>
                            <td style="width: 25%">
                                <asp:CheckBox id="cbCash" runat="server" Text="Cash Invoice" Checked="true"/>
                                <asp:CheckBox id="cbTax" runat="server" Text="Sales Tax Invoice" Checked="true"/>
                            </td>
                            <td style="width: 1%"></td>
                            <td style="width: 15%"></td>
                            <td style="width: 44%"></td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="lblFromMonth" Text="From Month" runat="server"></asp:Label>
                                </strong>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtFrom" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 1%"></td>
                            <td style="width: 15%"></td>
                            <td style="width: 44%"></td>
                        </tr>
                        <tr>
                            <td style="width: 15%">
                                <strong>
                                    <asp:Label ID="lblToMonth" Text="To Month" runat="server"></asp:Label>
                                </strong>
                            </td>
                            <td style="width: 25%">
                                <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                            </td>
                            <td style="width: 1%"></td>
                            <td style="width: 15%"></td>
                            <td style="width: 44%"></td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <table width="100%" cellpadding="3" cellspacing="3">
            <tr>
                <td style="width: 10%"></td>
                <td style="width: 90%">
                    <asp:Button ID="btnViewPDF" runat="server" Text="View PDF" CssClass="button" OnClick="btnViewPDF_Click" />
                    <asp:Button ID="btnViewExcel" runat="server" Text="View Excel" CssClass="button" OnClick="btnViewExcel_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

