<%@ Page Title="Fast Services: Recurrsive Invoices" Language="C#" MasterPageFile="~/Forms/PageMaster.master" 
    AutoEventWireup="true" CodeFile="frmRecurrsiveInvoices.aspx.cs" Inherits="Forms_frmRecurrsiveInvoices" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/Forms/UserControls/wucCheckedDropDown.ascx" TagName="MultiCheckCombo" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
<script src="../AjaxLibrary/jquery.tablesorter.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    var TotalChkBx;
    var Counter;

    function pageLoad() {

        $('#<%=gvRecurringInvoices.ClientID %>').tablesorter(
	     {
	         headers: {
	             4: {
	                 sorter: false
	             }
	         }
	     });  
            
        //Get total no. of CheckBoxes in side the GridView.
        TotalChkBx = parseInt('<%= this.gvRecurringInvoices.Rows.Count %>');
        Counter = 0;

        $('.gvDate').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });
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

    function HeaderClick(CheckBox) {
        //Get target base & child control.
        var TargetBaseControl = document.getElementById('<%= this.gvRecurringInvoices.ClientID %>');
        var TargetChildControl = "gvRecurringInvoices";

        //Get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        //Checked/Unchecked all the checkBoxes in side the GridView.
        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)

                if (!Inputs[n].disabled) {
                    Inputs[n].checked = CheckBox.checked;
                }

        //Reset Counter
        Counter = CheckBox.checked ? TotalChkBx : 0;
    }

    function ChildClick(CheckBox, HCheckBox) {
        //get target control.
        var HeaderCheckBox = document.getElementById(HCheckBox);

        //Modifiy Counter; 
        if (CheckBox.checked && Counter < TotalChkBx)
            Counter++;
        else if (Counter > 0)
            Counter--;
        //Change state of the header CheckBox.        
        if (Counter < TotalChkBx)
            HeaderCheckBox.checked = false;
        else if (Counter == TotalChkBx)
            HeaderCheckBox.checked = true;
    }

</script>
    <div id="right_data">
     <div>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                    Width="31px" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
            PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
    </div>    
        <asp:UpdatePanel ID="upReport" runat="server">
            <ContentTemplate>                
                <asp:Literal ID="ltrlReport" runat="server"></asp:Literal>                
                <asp:Panel ID="pnlFilter" GroupingText="Filter Options" runat="server" Width="70%">
                    <table width="100%">
                        <tr>
                            <td style="width: 15%">
                                <asp:Label ID="lblCustomer" Text="Customer" runat="server"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                <asp:DropDownList ID="ddlCustomer" runat="server" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 10%">
                                <strong>
                                    <asp:Label ID="lblCategory" Text="Category" runat="server"></asp:Label>
                                </strong>
                            </td>
                            <td style="width: 45%">
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>                
                <br />
                <table width="80%">
                    <tr>
                        <td style="width:50%;" align="center">
                            <asp:Button ID="btnGenerateInvoice" runat="server" Text="Generate Invoice" 
                                CssClass="button" onclick="btnGenerateInvoice_Click"/>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnSendEmail" runat="server" Text="Send Invoice" 
                                CssClass="button" onclick="btnSendEmail_Click"/>
                        </td>
                    </tr>
                </table>
                <br />
                <strong>
                    <asp:Label ID="lblCc" runat="server" Text="Cc:" Width="100px"></asp:Label>
                </strong>
                <uc1:MultiCheckCombo ID="cblInvoiceCc" runat="server" />
                <br />
                <br />
                <asp:UpdatePanel ID="upGrids" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvRecurringInvoices" runat="server" AutoGenerateColumns="False" Width="95%"
                    CellPadding="2" CellSpacing="0" BorderColor="White" ForeColor="SteelBlue" ShowHeader="true"
                    RowStyle-ForeColor="#525252" OnRowDataBound="gvRecurringInvoices_RowDataBound" CssClass="tablesorter">
                    <Columns>
                        <asp:BoundField DataField="INVOICE_MASTER_ID" HeaderText="INVOICE_MASTER_ID">
                            <ItemStyle CssClass="HidePanel">
                            </ItemStyle>
                            <HeaderStyle CssClass="HidePanel"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="ENQUIREY_MASTER_ID" HeaderText="ENQUIREY_MASTER_ID">
                            <ItemStyle CssClass="HidePanel">
                            </ItemStyle>
                            <HeaderStyle CssClass="HidePanel"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                            <ItemStyle CssClass="HidePanel">
                            </ItemStyle>
                            <HeaderStyle CssClass="HidePanel"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PDFFileName" HeaderText="PDFFileName">
                            <ItemStyle CssClass="HidePanel">
                            </ItemStyle>
                            <HeaderStyle CssClass="HidePanel"/>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" onclick="javascript:HeaderClick(this);">
                                </asp:CheckBox>
                            </HeaderTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Left"  BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Customer" HeaderText="Customer">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="250px">
                            </ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Email To">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlEmailTo" runat="server" Width="100%"></asp:DropDownList>
                            </ItemTemplate>                            
                            <ItemStyle Width="150px" HorizontalAlign="Left"  BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>   
                        <asp:BoundField DataField="REFERENCE_NO" HeaderText="Invoice No">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px">
                            </ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date" DataFormatString="{0:dd-MMM-yyyy}">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SLASH_DESC" HeaderText="Currency">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoiceTotal" HeaderText="Invoice Amount" DataFormatString="{0:0,0}">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InvoiceStatus" HeaderText="Payment Status">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField> 
                         <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date">
                            <ItemStyle Width="70px" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="New Invoice Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="gvDate" Width="100px"></asp:TextBox>
                            </ItemTemplate>                            
                            <ItemStyle Width="120px" HorizontalAlign="Left"  BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>                       
                    </Columns>
                    <HeaderStyle CssClass="tblhead" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>                        
    </div>
</asp:Content>
