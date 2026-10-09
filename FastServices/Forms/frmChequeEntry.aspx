<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmChequeEntry.aspx.cs" Inherits="Forms_frmChequeEntry" Title="Fast Services: Cheque Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function pageLoad() {
            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }

            $('.gvDate').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });
        }

        function startRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;

        }

        function ValidateForm() {
            var str;
            str = document.getElementById("<%= txtAmount.ClientID %>").value;
            if (str == null || str.length == 0) {
                alert('Must enter Amount');
                return false;
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

        function CalculateWHTax(objID, objCheckBox, objWHITax, objWHSTax, objTaxPayable) {
            var incometax = 0;
            var servicetax = 0;
            var salestax = 0;
            var invoicetotal = 0;
            if (objCheckBox.checked) {
                var obj = jQuery.parseJSON($("#<%= hfData.ClientID %>").val());
                for (var ii = 0; ii < obj.length; ii++) {
                    var item = obj[ii];
                    if (item.INVOICE_MASTER_ID == objID) {
                        incometax += parseFloat(item.InvoiceTotal) * parseFloat(item.INCOME_TAX) / 100;
                        var exclgst = parseFloat(item.InvoiceTotal2) + (parseFloat(item.InvoiceTotal2) * parseFloat(item.INCOME_TAX) / 100);
                        var salestax2 = parseFloat(exclgst) * parseFloat(item.SERVICE_TAX) / 100;
                        salestax += parseFloat(salestax2);
                        servicetax += parseFloat(salestax2) * 20 / 100;
                        invoicetotal += item.InvoiceTotal;

                        var whitax = (parseFloat(item.InvoiceTotal) * parseFloat(item.INCOME_TAX) / 100).toFixed(2);

                        if (parseFloat(whitax) > 0) {
                        }
                        else {
                            whitax = 0;
                        }
                        objWHITax.value = whitax;
                        objWHSTax.value = parseFloat(servicetax).toFixed(2);
                        objTaxPayable.value = (parseFloat(salestax) - parseFloat(servicetax)).toFixed(2);                                        

                    }
                }
            }
            else {
                objWHITax.value = 0;
                objWHSTax.value = 0;
                objTaxPayable.value = 0;
            }
            CalculateTotal();
        }

        function CalculateTotal() {
            var incometax = 0;
            var servicetax = 0;
            var salestax = 0;
            var invoicetotal = 0;
            Parent = document.getElementById('<%= GrdCredit.ClientID %>');
            for (var i = 0, row; row = Parent.rows[i]; i++) {
                if (inputList = row.cells[1].getElementsByTagName("input")) {
                    for (var n = 0; n < inputList.length; n++) {
                        if (inputList[n].type == "checkbox") {
                            if (inputList[n].checked) {
                                var obj = jQuery.parseJSON($("#<%= hfData.ClientID %>").val());
                                for (var ii = 0; ii < obj.length; ii++) {
                                    var item = obj[ii];
                                    if (item.INVOICE_MASTER_ID == row.cells[0].innerHTML) {
                                        incometax += parseFloat(item.InvoiceTotal) * parseFloat(item.INCOME_TAX) / 100;
                                        var exclgst = parseFloat(item.InvoiceTotal2) + (parseFloat(item.InvoiceTotal2) * parseFloat(item.INCOME_TAX) / 100);
                                        var salestax2 = parseFloat(exclgst) * parseFloat(item.SERVICE_TAX) / 100;
                                        salestax += parseFloat(salestax2);
                                        servicetax += parseFloat(salestax2) * 20 / 100;
                                        invoicetotal += item.InvoiceTotal;

                                        var whitax = (parseFloat(item.InvoiceTotal) * parseFloat(item.INCOME_TAX) / 100).toFixed(2);

                                        if (parseFloat(whitax) > 0) {
                                        }
                                        else {
                                            whitax = 0;
                                        }
                                    }
                                }
                            }
                            else {
                            }
                        }
                    }
                }
            }
            document.getElementById("<%= txtTaxIncome.ClientID %>").value = parseFloat(incometax).toFixed(2);
            document.getElementById("<%= txtTaxSales.ClientID %>").value = parseFloat(servicetax).toFixed(2);
            document.getElementById("<%= txtTaxPayable.ClientID %>").value = (parseFloat(salestax) - parseFloat(servicetax)).toFixed(2);
            document.getElementById("<%= hfWHTax.ClientID %>").value = parseFloat(salestax2).toFixed(2);
            document.getElementById("<%= txtTotal.ClientID %>").value = parseFloat(invoicetotal).toFixed(2);
        }
    </script>
    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" RenderMode="Inline">
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
                                        <td style="width:40%;" valign="top">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="lblfromLocation" runat="server" Width="94px" Text="Location"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">
                                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="220px" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"
                                                            AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="Label10" runat="server" Width="86px" Text="Status"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">
                                                    <asp:DropDownList ID="DrpStatus" runat="server" Width="220px" CssClass="DropList"
                                                            OnSelectedIndexChanged="DrpStatus_SelectedIndexChanged" AutoPostBack="True">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="Label14" runat="server" Width="98px" Text="Deposit Account"></asp:Label>
                                                        </strong>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">
                                                    <asp:DropDownList ID="DrpBankAccount" runat="server" Width="220px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="Label5" runat="server" Width="94px" Text="Chque No"></asp:Label>
                                                        </strong>
                                                        <asp:TextBox ID="txtChequeNo" runat="server" Width="94px" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">
                                                    <strong>
                                                            <asp:Label ID="Label3" runat="server" Width="94px" Text="Bank Name"></asp:Label></strong>
                                                        <br />
                                                        <asp:TextBox ID="txtBankName" runat="server" Width="192px" CssClass="txtBox" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:100%" colspan="3">
                                                        <strong>
                                                            <asp:Label ID="Label13" runat="server" Width="94px" Text="Remarks"></asp:Label>
                                                        </strong>
                                                        <br />
                                                        <asp:TextBox ID="txtRemarks" runat="server" Width="298px" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                  </tr>
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="Label6" runat="server" Width="74px" Text="Chq Amount"></asp:Label></strong>
                                                        <asp:TextBox ID="txtAmount" runat="server" Width="94px" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">
                                                    <strong>
                                                            <asp:Label ID="Label9" runat="server" Text="Cheque Date"></asp:Label></strong>
                                                        <br />
                                                        <asp:TextBox ID="txtStartDate" runat="server" Width="132px" CssClass="gvDate" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td style="width:25%">
                                                        <strong>
                                                            <asp:Label ID="Label2" runat="server" Width="96px" Text="Slip No"></asp:Label></strong>
                                                        <asp:TextBox ID="txtSlipNo" runat="server" Width="94px" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                    <td style="width:1%">
                                                    </td>
                                                    <td style="width:76%">                                                    
                                                    <strong>
                                                            <asp:Label ID="lblRealizationDate" runat="server" Text="Realization Date"></asp:Label></strong>
                                                        <br />
                                                        <asp:TextBox ID="txtRealDate" runat="server" Width="132px" CssClass="gvDate" onkeydown="javascript:return (event.keyCode!=13);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:100%" colspan="3">
                                                        <asp:HiddenField ID="hfData" runat="server" />
                                                        <asp:HiddenField ID="hfWHTax" runat="server" />
                                                        <br />
                                                        <asp:Button AccessKey="S" ID="btnSave" OnClick="btnSave_Click" runat="server" Width="102px"
                                                            Font-Size="8pt" Text="Save" CssClass="Button" />
                                                        &nbsp;&nbsp;
                                                        <asp:Button AccessKey="C" ID="btnCancel" runat="server" Width="120px" Font-Size="8pt"
                                                            Text="Cancel" OnClick="btnCancel_Click" CssClass="Button" />
                                                    </td>                                                    
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width:60%;">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:5%"></td>
                                                    <td style="width:95%">
                                                        <strong>
                                                            <asp:Label ID="lblCustomer" runat="server" Text="Customer" Width="66px"></asp:Label>
                                                        </strong>
                                                        <asp:DropDownList ID="DrpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged"
                                                            Width="220px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td style="width:5%"></td>
                                                    <td style="width:95%">
                                                        <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Groove" ScrollBars="Auto"
                                                            BorderWidth="1px" Height="200px" Width="500px">
                                                            <asp:GridView ID="GrdCredit" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                BackColor="White" BorderColor="White" DataKeyNames="INVOICE_MASTER_ID" ForeColor="SteelBlue"
                                                                HorizontalAlign="Center" onrowdatabound="GrdCredit_RowDataBound">
                                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                    PreviousPageText="Previous" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="INVOICE_MASTER_ID" HeaderText="INVOICE_MASTER_ID">
                                                                        <ItemStyle CssClass="HidePanel" />
                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChbIsAssigned" runat="server" Width="14px"/>
                                                                        </ItemTemplate>
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="REFERENCE_NO" HeaderText="Invoice No">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InvoiceTotal" DataFormatString="{0:F2}" HeaderText="Credit Amount">
                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="100px" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="W.H.I. Tax">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtWHITax" runat="server" Width="100%"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="W.H.S. Tax">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtWHSTax" runat="server" Width="100%"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tax Payable">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtPayable" runat="server" Width="100%"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="100px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ENQUIREY_MASTER_ID" HeaderText="ENQUIREY_MASTER_ID">
                                                                        <ItemStyle CssClass="HidePanel" />
                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="InvoiceTotal2" HeaderText="InvoiceTotal2">
                                                                        <ItemStyle CssClass="HidePanel" />
                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:5%"></td>
                                                    <td style="width:95%">
                                                        <asp:Panel ID="Panel2" runat="server" Height="20px" Width="500px" 
                                                            HorizontalAlign="Right">
                                                        <asp:TextBox ID="txtTotal" runat="server" Text="0" Enabled="false"  style="text-align: right" Width="80px"></asp:TextBox>
                                                        <asp:TextBox ID="txtTaxIncome" runat="server" Text="0" Enabled="false"  style="text-align: right" Width="60px"></asp:TextBox>                                                        
                                                        <asp:TextBox ID="txtTaxSales" runat="server" Text="0" Enabled="false"  style="text-align: right" Width="70px"></asp:TextBox>
                                                        <asp:TextBox ID="txtTaxPayable" runat="server" Text="0" Enabled="false"  style="text-align: right" Width="80px"></asp:TextBox>
                                                        </asp:Panel>                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    ValidChars="0123456789." TargetControlID="txtAmount">
                                </cc1:FilteredTextBoxExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtStartDate"
                                    Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                                <asp:HiddenField ID="HFChqueProcessId" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                    width: 700px; border-bottom: silver thin inset">
                                    <tbody>
                                        <tr>
                                            <td style="height: 20px" align="left" colspan="5">
                                                <asp:Panel ID="Panel12" runat="server" Width="750px" Height="200px" ScrollBars="Vertical">
                                                    <table style="border-right: silver thin inset; border-top: silver thin inset; border-left: silver thin inset;
                                                        border-bottom: silver thin inset; background-color: silver" width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="height: 21px" align="left">
                                                                    <strong>
                                                                        <asp:Label ID="Label110" runat="server" Width="154px" Text="Select Searching Type"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 170px; height: 21px" align="left">
                                                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                                                        <asp:ListItem Value="CHEQUE_NO">All Records</asp:ListItem>
                                                                        <asp:ListItem Value="CUSTOMER_NAME">Customer</asp:ListItem>
                                                                        <asp:ListItem Value="BANK_NAME">Bank Name</asp:ListItem>
                                                                        <asp:ListItem Value="SlipNo">Slip No</asp:ListItem>
                                                                        <asp:ListItem Value="account_name">Deposit Account </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 224px; height: 21px" align="left">
                                                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                                                </td>
                                                                <td style="width: 250px; height: 21px" align="left">
                                                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                                                        OnClick="btnFilter_Click"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <asp:GridView ID="GrdOrder" runat="server" Width="100%" ForeColor="SteelBlue" BorderColor="White"
                                                        HorizontalAlign="Center" BackColor="White" AutoGenerateColumns="False" OnRowEditing="GrdOrder_RowEditing"
                                                        OnRowDeleting="GrdOrder_RowDeleting">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CHEQUE_PROCESS_ID" HeaderText="CHEQUE_PROCESS_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Customer">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_NO" HeaderText="Chq. No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_DATE" HeaderText="Chq.Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="RECEIVED_DATE" HeaderText="Received Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DEPOSIT_DATE" HeaderText="Deposit Date">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CHEQUE_AMOUNT" DataFormatString="{0:F2}" HeaderText="Chq.Amount">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SlipNo" HeaderText="Slip No">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_name" HeaderText="Deposit Account">
                                                                <ControlStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="account_head_id" HeaderText="account_head_id">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="PROCESS_DATE" HeaderText="RealizationDate">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                             <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                        <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
