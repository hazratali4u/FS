<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCommission.aspx.cs" Inherits="frmCommission" Title="Fast Services: Commission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">    
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

    function HeaderClick(objRef) {
        var IsChecked = objRef.checked;
        var mode;
        var amount;
        var commission;
        var netcommission;
        var tax = document.getElementById('<%= txtTax.ClientID %>').value;

        Parent = document.getElementById('<%= gvPayment.ClientID %>');
        for (var i = 0, row; row = Parent.rows[i]; i++) {
            if (i > 0) {
                if (inputCheckBox = row.cells[2].getElementsByTagName("input")) {
                    inputCheckBox[0].checked = IsChecked;
                }
                mode = row.cells[4].innerHTML;
                if (inputAmount = row.cells[8].getElementsByTagName("input")) {
                    if (inputAmount[0].value != '') {
                        amount = inputAmount[0].value;
                    }
                    else {
                        amount = 0;
                    }
                }
                if (inputComission = row.cells[9].getElementsByTagName("input")) {
                    if (inputComission[0].value != '') {
                        commission = inputComission[0].value;
                    }
                    else {
                        commission = '0%';
                    }
                }
                if (inputNetComission = row.cells[10].getElementsByTagName("input")) {
                    if (inputNetComission[0].value != '') {
                        netcommission = inputNetComission[0].value;
                    }
                    else {
                        netcommission = 0;
                    }
                }

                if (commission.indexOf('%') === -1) {
                    if (tax.indexOf('%') === -1) {
                        netcommission = parseFloat(commission) - parseFloat(tax);
                    }
                    else {
                        var totaltax = 0;
                        totaltax = parseFloat(commission) * parseFloat(tax) / 100;
                        if (mode == 'Cheque') {
                            netcommission = parseFloat(commission - totaltax);
                        }
                        else {
                            netcommission = parseFloat(commission);
                        }
                    }
                }
                else {
                    commission = parseFloat(amount) * parseFloat(commission) / 100;
                    if (tax.indexOf('%') === -1) {
                        netcommission = parseFloat(commission) - parseFloat(tax);
                    }
                    else {
                        var totaltax = 0;
                        totaltax = parseFloat(commission) * parseFloat(tax) / 100;
                        if (mode == 'Cheque') {
                            netcommission = parseFloat(commission - totaltax);
                        }
                        else {
                            netcommission = parseFloat(commission);
                        }
                    }
                }
                if (IsChecked) {
                    inputNetComission[0].value = netcommission;
                }
                else {
                    inputNetComission[0].value = 0;
                }
            }
        }
    }

    function CalculateTotal() {
        document.getElementById('<%=txtComm.ClientID %>').value = 0;
        document.getElementById('<%=txtNetComm.ClientID %>').value = 0;
        var gv = document.getElementById('<%=gvPayment.ClientID %>');
        var tb = gv.getElementsByTagName("input");
        var comm = 0;
        var netcomm = 0;
        for (var i = 0; i < tb.length; i++) {
            if (tb[i].type == "text") {
                if (tb[i].name.indexOf("txtCommission") !== -1) {
                    var objValue = parseFloat(tb[i].value.replace(',', ''));
                    if (parseFloat(objValue) > 0) {
                        comm += parseFloat(objValue);
                    }
                }
                else if (tb[i].name.indexOf("txtNetCommission") !== -1) {
                    var objValue = parseFloat(tb[i].value.replace(',', ''));
                    if (parseFloat(objValue) > 0) {
                        netcomm += parseFloat(objValue);
                    }
                }
            }
        }
        document.getElementById('<%=txtComm.ClientID %>').value = comm.toFixed(2);
        document.getElementById('<%=txtNetComm.ClientID %>').value = netcomm.toFixed(2);
    }


    function CalNetCommission(chk, mode, amount, commission, netcommission) {
        var total = 0;
        var net = 0;
        var tax = document.getElementById('<%= txtTax.ClientID %>').value;

        var amountvalue = document.getElementById(amount).value;
        var commissionvalue = document.getElementById(commission).value;

        if (parseFloat(amountvalue) > 0) {
        }
        else {
            amountvalue = 0;
        }

        if (parseFloat(commissionvalue) > 0) {
        }
        else {
            commissionvalue = '0%';
        }
        if (commissionvalue.indexOf('%') === -1) {
            total = commissionvalue;
            if (tax.indexOf('%') === -1) {
                if (chk.checke) {
                    net = parseFloat(total) - parseFloat(tax);
                }
            }
            else {
                var totaltax = 0;
                totaltax = parseFloat(total) * parseFloat(tax) / 100;
                if (chk.checked) {
                    if (mode == 'Cheque') {
                        net = parseFloat(total - totaltax);
                    }
                    else {
                        net = parseFloat(total);
                    }
                }
            }
            document.getElementById(netcommission).value = net;
        }
        else {
            if (chk.checked) {
                total = parseFloat(amountvalue) * parseFloat(commissionvalue) / 100;
            }
            if (tax.indexOf('%') === -1) {
                if (chk.checked) {
                    net = parseFloat(total) - parseFloat(tax);
                }
            }
            else {
                var totaltax = 0;
                totaltax = parseFloat(total) * parseFloat(tax) / 100;
                if (chk.checked) {
                    if (mode == 'Cheque') {
                        net = parseFloat(total - totaltax);
                    }
                    else {
                        net = parseFloat(total);
                    }
                }
            }

            document.getElementById(netcommission).value = net;
        }

        CalculateTotal();
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
        <div>
            <table width="100%">
                <tr>
                    <td>                        
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom"
                                    TargetControlID="txtTax" ValidChars="0123456789.%">
                                </cc1:FilteredTextBoxExtender>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table>
                                        <tbody>                                           
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="lblfromLocation" runat="server" Width="65px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="drpDistributor" runat="server" Width="250px" 
                                                        CssClass="DropList" AutoPostBack="True" 
                                                        onselectedindexchanged="drpDistributor_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label1" runat="server" Width="116px" Text="Expense Head" CssClass="lblbox"></asp:Label></strong>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:DropDownList ID="DrpAccountHead" runat="server" Width="250px" 
                                                        CssClass="DropList" AutoPostBack="True" 
                                                        onselectedindexchanged="DrpAccountHead_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label9" runat="server" Width="71px" Text="Employee" CssClass="lblbox"></asp:Label></strong>&nbsp;
                                                </td>
                                                <td style="height: 20px;" align="left">
                                                    <asp:DropDownList ID="ddlEmployee" runat="server" Width="250px" 
                                                        AutoPostBack="True" onselectedindexchanged="ddlEmployee_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label3" runat="server" Width="71px" Text="Category"></asp:Label></strong>
                                                </td>
                                                <td style="height: 20px;" align="left">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="250px" 
                                                        AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label6" runat="server" Width="63px" Text="Tax"></asp:Label></strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:TextBox ID="txtTax" runat="server" Width="163px" Text="10%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px" align="left">
                                                    <strong>
                                                        <asp:Label ID="Label2" runat="server" Width="63px" Text="Month"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="height: 20px" align="left">
                                                    <asp:RadioButtonList ID="rblMonth" runat="server" RepeatDirection="Horizontal" 
                                                        AutoPostBack="true" onselectedindexchanged="rblMonth_SelectedIndexChanged">                                                    
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                                &nbsp;&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <strong>
                            <asp:Label ID="lblRowId" runat="server" Text="Label" Visible="False"></asp:Label></strong>
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
                                <asp:Panel ID="Panel12" runat="server" Height="200px" ScrollBars="Vertical" Width="950px"
                                    BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px">
                                    <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" 
                                        GridLines="Horizontal"  Width="98%" HorizontalAlign="Center"
                                            CellSpacing="3" onrowdatabound="gvPayment_RowDataBound" >
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous" />
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="InvoiceID" HeaderText="InvoiceID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="ORGANIZATION_ID" HeaderText="ORGANIZATION_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbCommission" runat="server" Checked='<%# Eval("Is_Commission")%>' />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbCommissionAll" runat="server" onclick="javascript:HeaderClick(this);">
                                                        </asp:CheckBox>
                                                    </HeaderTemplate>
                                                    <ItemStyle Width="5px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Customer" HeaderText="Customer Name">
                                                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="Bank" HeaderText="Bank">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DocumentDate" HeaderText="Document Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:BoundField>                                                
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amount")%>' Enabled="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Commision">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCommission" runat="server" Width="100%" CausesValidation="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Net Commision">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNetCommission" runat="server" Width="100%" CausesValidation="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign = "Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" />
                                            <PagerStyle BackColor="Transparent" />
                                            <HeaderStyle CssClass="tblhead" />
                                            <AlternatingRowStyle BackColor="#ffffff" />
                                        </asp:GridView>
                                </asp:Panel>
                                <table width="950px">
                                    <tr>
                                        <td align="right">
                                            <asp:TextBox ID="txtAmt" runat="server" Text="0" Width="100" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtComm" runat="server" Text="0" Width="100" Enabled="false"></asp:TextBox>
                                            <asp:TextBox ID="txtNetComm" runat="server" Text="0" Width="75px" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="lblDummy" runat="server" Width="30px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:Button ID="btnSave" UseSubmitBehavior="false" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                                    Width="117px" CssClass="Button" />                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
