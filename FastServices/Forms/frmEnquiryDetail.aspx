<%@ Page Title="Fast Services: Enquiry Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmEnquiryDetail.aspx.cs" Inherits="Forms_frmEnquiryDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Src="~/Forms/UserControls/wucCheckedDropDown.ascx" TagName="MultiCheckCombo"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
    <link href="../SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css">
    <script src="../SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        var TotalChkBx;
        var Counter;
        var TotalChkBxInv;
        var CounterInv;

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {

            document.getElementById('<%=btnWord.ClientID%>').disabled = true;
            document.getElementById('<%=btnPDF.ClientID%>').disabled = true;
            document.getElementById('<%=btnSaveQuotation.ClientID%>').disabled = true;
            document.getElementById('<%=btnSendQuotation.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnWord.ClientID%>').disabled = false;
            document.getElementById('<%=btnPDF.ClientID%>').disabled = false;
            document.getElementById('<%=btnSaveQuotation.ClientID%>').disabled = false;
            document.getElementById('<%=btnSendQuotation.ClientID%>').disabled = false;
        }

        function ShowCcTextBox2() {
            document.getElementById("<%= txtCc22.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= txtCc2.ClientID %>").className = "ShowPanel";
        }

        function ShowCcTextBox() {
            document.getElementById("<%= txtCc3.ClientID %>").className = "ShowPanel";
        }
        function pageLoad() {

            //Get total no. of CheckBoxes in side the GridView.
            TotalChkBx = parseInt('<%= this.gvQuotation.Rows.Count %>');
            TotalChkBxInv = parseInt('<%= this.gvInvoice.Rows.Count %>');
            //Get total no. of checked Select Product CheckBoxes in side the GridView.
            Counter = 0;


            $('#<%=txtPaymentDate.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });


            $('.gvDate').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });

            $('.gvDate2').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    dateFormat: 'dd/mm/yy',
                    changeMonth: true
                });

            var hfTabIndex = document.getElementById('<%=hfTabIndex.ClientID%>').value;
            var ActiveTabIndex = document.getElementById('<%=ActiveTabIndex.ClientID %>').value;
            var TabbedPanels1 = new Spry.Widget.TabbedPanels("TabbedPanels1");

            if (hfTabIndex == '0') {
                TabbedPanels1.showPanel(0);
            }
            else if (hfTabIndex == '1') {
                TabbedPanels1.showPanel(1);
            }
            else if (hfTabIndex == '2') {
                TabbedPanels1.showPanel(2);
            }
            else if (hfTabIndex == '3') {
                TabbedPanels1.showPanel(3);
            }
            else if (hfTabIndex == '4') {
                TabbedPanels1.showPanel(4);
            }
            else if (hfTabIndex == '5') {
                TabbedPanels1.showPanel(5);
            }
            else if (hfTabIndex == '6') {
                TabbedPanels1.showPanel(6);
            }
            else if (hfTabIndex == '7') {
                TabbedPanels1.showPanel(7);
            }
            else {
                TabbedPanels1.showPanel(parseInt(ActiveTabIndex));
            }

            $('#<%=pnlEdit.ClientID %>').show();
            $('#<%=pnlEmail.ClientID %>').show();
            $('#<%=pnlPayment.ClientID %>').show();
            $('#<%=pnlInvoice.ClientID %>').show();
            $('#<%=pnlDocument.ClientID %>').show();
            $('#<%=pnlSendEmail.ClientID %>').show();

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


        function HeaderClick(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvQuotation.ClientID %>');
            var TargetChildControl = "cbQuotation";

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

        function HeaderClickInv(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvInvoice.ClientID %>');
            var TargetChildControl = "cbInvoice";

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

        function ChildClickInv(CheckBox, HCheckBox) {
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


        function GetTotal(tQuantity_id, tPrice_id, lTotal_id) {
            var obj_tQuantity = document.getElementById(tQuantity_id);
            var obj_tPrice = document.getElementById(tPrice_id);
            var obj_lTotal = document.getElementById(lTotal_id);
            if (obj_tQuantity.value != "" && obj_tPrice.value != "") {
                obj_lTotal.value = parseFloat(obj_tQuantity.value) * parseFloat(obj_tPrice.value);
            }
        }

        function SetEndDate(txtEndDate, ddlPaymentType) {
            if (ddlPaymentType.value == '5') {
                txtEndDate.disabled = false;
            }
            else {
                txtEndDate.disabled = true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">
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

        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton" DropShadow="true" OkControlID="dummyLink" PopupControlID="Panel1"
            TargetControlID="dummyLink" BehaviorID="ModelPopup">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" Style="display: none; background-color: White;"
            Height="450px" Width="800px" ScrollBars="Auto">
            <table width="100%">
                <tr>
                    <td align="right" colspan="3">
                        <a href="#" style="display: none; visibility: hidden;" onclick="return false" id="dummyLink"
                            runat="server">dummy</a>
                        <asp:ImageButton ID="CancelButton" runat="server" ImageUrl="~/App_Themes/Granite/Images/closelabel.gif" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 90%">
                        <h1>
                            <asp:Label ID="lblProduct" runat="server"></asp:Label></h1>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" align="right">
                        <strong>Detail </strong>
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 85%">
                        <FCKeditorV2:FCKeditor ID="txtDetail" runat="server" BasePath="~/FCKeditor/" Height="280px"
                            Width="80%" ToolbarSet="NormalUser">
                        </FCKeditorV2:FCKeditor>
                    </td>
                </tr>
                <tr>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 5%">
                    </td>
                    <td style="width: 90%">
                        <asp:Button ID="btnSaveProductDetail" runat="server" CssClass="Button" Text="Save"
                            OnClick="btnSaveProductDetail_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table>
            <tr>
                <td style="width: 5%;">
                    <strong>
                        <asp:Label ID="lblCustomerMain" runat="server" Text=""></asp:Label></strong>
                </td>
            </tr>
        </table>
        <div id="action">
            <div id="tab_done">
            </div>
            <h2>
                Action
            </h2>
            <div id="TabbedPanels1" class="TabbedPanels">
                <asp:Literal ID="ltrlTabs" runat="server"></asp:Literal>
                <div class="TabbedPanelsContentGroup">
                    <div class="TabbedPanelsContent">
                        <div>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label1" runat="server" Width="87px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:Label ID="lblDocumentNoView" runat="server" CssClass="lblbox" Text="" Width="87px"></asp:Label>
                                                            </td>
                                                            <td style="width: 15px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 99px; height: 18px" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 18px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label3" runat="server" Width="87px" Text="Location" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="lblLocation" runat="server" CssClass="lblbox" Text="" Width="200px"></asp:Label></strong>
                                                            </td>
                                                            <td align="left" style="width: 15px;">
                                                            </td>
                                                            <td style="width: 99px; height: 18px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 18px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label4" runat="server" Width="76px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="lblCustomerView" runat="server" CssClass="lblbox" Text=""></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="lblOriginator" runat="server" Width="110px" Text="Enquiry Originator"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="lblOriginatorView" runat="server" Width="200px"></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label11" runat="server" Width="110px" Text="Category"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:Label ID="lblCategoryView" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="height: 25px" valign="top">
                                                                <strong>
                                                                    <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Remarks" Width="76px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td align="left" style="height: 20px">
                                                                <strong>
                                                                    <asp:Label ID="lblRemarksView" runat="server" CssClass="lblbox" Text="" Width="200px"
                                                                        Height="50px"></asp:Label></strong>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td align="left" style="width: 99px; height: 20px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" style="height: 20px" valign="top">
                                                                <asp:HiddenField ID="hfEnquiryID" runat="server" />
                                                                <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                                <asp:HiddenField ID="hfTabIndex" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="height: 16px; width: 350px;">
                                                                            <asp:Label ID="Label5" runat="server" Width="350px" Height="16px" ForeColor="White"
                                                                                Font-Bold="True" Text="   Product Name" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 16px; width: 73px;">
                                                                            <asp:Label ID="Label8" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                                                Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                                                        </td>
                                                                        <td style="height: 16px" align="center">
                                                                            <asp:Label ID="Label20" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                                                Font-Bold="True" Text="Document Date" BackColor="#006699"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="9">
                                                                            <asp:GridView ID="gvMainView" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                                BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                                                                                ShowHeader="False">
                                                                                <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                                    PreviousPageText="Previous" />
                                                                                <RowStyle ForeColor="Black" />
                                                                                <Columns>
                                                                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                                        <HeaderStyle CssClass="HidePanel" />
                                                                                        <ItemStyle CssClass="HidePanel" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="350px" BorderWidth="2px"
                                                                                            HorizontalAlign="Left" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                                            Width="73px" />
                                                                                    </asp:BoundField>
                                                                                    <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}">
                                                                                        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                                            Width="50px" />
                                                                                    </asp:BoundField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="White" />
                                                                                <PagerStyle BackColor="Transparent" />
                                                                                <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                                    VerticalAlign="Middle" />
                                                                                <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </Ttbody>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfViewIndex" runat="server" />
                        </div>
                        <div>
                        </div>
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlEdit" runat="server" CssClass="HidePanel">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <table>
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="5">
                                                                <asp:Literal ID="ltrlEdit" runat="server"></asp:Literal>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label10" runat="server" Width="87px" Text="Document No" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:Label ID="lblDocumentNo" runat="server" CssClass="lblbox" Text="" Width="87px"></asp:Label>
                                                            </td>
                                                            <td style="width: 15px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 99px; height: 18px" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 18px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="height: 25px">
                                                                <strong>
                                                                    <asp:Label ID="Label9" runat="server" CssClass="lblbox" Text="Location" Width="87px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td align="left" style="height: 20px">
                                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="DropList" Width="206px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left" style="width: 15px;">
                                                            </td>
                                                            <td align="left" style="width: 99px; height: 18px;">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" style="height: 18px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label6" runat="server" Width="76px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:DropDownList ID="DrpCustomer" runat="server" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged"
                                                                    AutoPostBack="true" Width="206px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label17" runat="server" Width="110px" Text="Enquiry Originator" CssClass="lblbox"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:DropDownList ID="ddlOriginator" runat="server" CssClass="DropList" Width="206px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 25px" align="left">
                                                                <strong>
                                                                    <asp:Label ID="Label12" runat="server" Width="110px" Text="Category"></asp:Label></strong>
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                <asp:Label ID="lblCategoryEdit" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 99px;" align="left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="height: 20px" align="left">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" style="height: 25px" valign="top">
                                                                <strong>
                                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="lblbox" Text="Remarks" Width="76px"></asp:Label>
                                                                </strong>
                                                            </td>
                                                            <td align="left" style="height: 20px">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Rows="5" TextMode="MultiLine"
                                                                    Width="200px"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                            </td>
                                                            <td align="left" style="width: 99px; height: 20px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                            <td align="left" style="height: 20px" valign="top">
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:HiddenField ID="hfEditIndex" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upEdit" runat="server">
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td style="width: 350px;">
                                                                            <asp:Label ID="lblskuname" runat="server" Width="350px" Height="16px" ForeColor="White"
                                                                                Font-Bold="True" Text="   Product Name" CssClass="lblbox" BackColor="#006699"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 73px;">
                                                                            <asp:Label ID="lblquantity" runat="server" Width="73px" Height="16px" ForeColor="White"
                                                                                Font-Bold="True" Text="Quantity" BackColor="#006699"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlProductEdit" runat="server" Width="350">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuantity" runat="server" Width="73px"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button AccessKey="A" ID="btnSaveEnquiry" OnClick="btnSaveEnquiry_Click" runat="server"
                                                                                Font-Size="8pt" Text="Add Product" Width="95px" ValidationGroup="vg" CssClass="Button" />                                                                            
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <asp:Panel ID="Panel4" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px"
                                                                                BorderStyle="Groove" BorderColor="Silver">
                                                                                <asp:GridView ID="gvMainEdit" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                                                    BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" OnRowCreated="gvMainEdit_RowCreated"
                                                                                    OnRowDeleting="gvMainEdit_RowDeleting" OnRowEditing="gvMainEdit_RowEditing" ShowHeader="False"
                                                                                    OnRowDataBound="gvMainEdit_RowDataBound">
                                                                                    <RowStyle ForeColor="Black" />
                                                                                    <Columns>
                                                                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                                            <ItemStyle CssClass="HidePanel" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="350px" BorderWidth="2px"
                                                                                                HorizontalAlign="Left" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                                                Width="73px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="" DataFormatString="{0:dd/MM/yyyy}">
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                                                Width="50px" />
                                                                                        </asp:BoundField>
                                                                                        <asp:TemplateField HeaderText="Edit">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Delete">
                                                                                            <ItemTemplate>
                                                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                                                                    Text="Delete"></asp:LinkButton>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="fnIsEnquiryPaymentReceived" HeaderText="fnIsEnquiryPaymentReceived">
                                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                                            <ItemStyle CssClass="HidePanel" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="fnIsEnquiryQuotation" HeaderText="fnIsEnquiryQuotation">
                                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                                            <ItemStyle CssClass="HidePanel" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="ENQUIREY_DETAIL_ID2" HeaderText="ENQUIREY_DETAIL_ID2">
                                                                                            <HeaderStyle CssClass="HidePanel" />
                                                                                            <ItemStyle CssClass="HidePanel" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <FooterStyle BackColor="White" />
                                                                                    <PagerStyle BackColor="Transparent" />
                                                                                    <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                                                        VerticalAlign="Middle" />
                                                                                    <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                            <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Font-Size="8pt" Text="Save"
                                                                OnClick="btnSaveDocument_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlEmail" runat="server" CssClass="HidePanel">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width: 100%;">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:Literal ID="ltrlQuotation" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <strong>
                                                                            <asp:Label ID="lblTemplate" runat="server" Text="Quotation Template:" Width="120px"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <strong>
                                                                            <asp:DropDownList ID="ddlTemplate" runat="server" Width="200px" OnSelectedIndexChanged="ddlTemplate_SelectedIndexChanged"
                                                                                AutoPostBack="true">
                                                                            </asp:DropDownList>
                                                                        </strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <strong>
                                                                            <asp:Label ID="Label14" runat="server" Text="Email Template:" Width="100px"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:DropDownList ID="ddlQuotationEmailTemplate" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <strong>
                                                                            <asp:Label ID="lblQuotationFrom" Visible="false" runat="server" Text="Email From:"
                                                                                Width="100px"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:DropDownList ID="ddlEmailFrom" Visible="false" runat="server" Width="200px"
                                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEmailFrom_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <strong>
                                                                            <asp:Label ID="Label15" runat="server" Text="Kind Attn:" Width="100px"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:DropDownList ID="ddlEmailToQuotation" runat="server" Width="200px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <strong>
                                                                            <asp:Label ID="Label7" runat="server" Text="Cc:" Width="100px"></asp:Label>
                                                                        </strong>
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <table width="100%">
                                                                            <tr>
                                                                                <td style="width: 60%">
                                                                                    <uc1:MultiCheckCombo ID="cblQuotationCc" runat="server" />
                                                                                </td>
                                                                                <td align="right" style="width: 5%">
                                                                                </td>
                                                                                <td style="width: 35%">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                        <input id="btnAddmore" value="Add more" type="button" class="Button" size="8" onclick="ShowCcTextBox2();" />
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:TextBox ID="txtCc2" runat="server" Width="200px" CssClass="HidePanel"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Literal ID="ltrlBody" runat="server" Visible="false"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <table>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:GridView ID="gvQuotation" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvQuotation_RowDataBound"
                                                                                            GridLines="Horizontal" OnRowCommand="gvQuotation_RowCommand" Width="95%" HorizontalAlign="Center"
                                                                                            CellSpacing="3" OnRowCreated="gvQuotation_RowCreated">
                                                                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                                                                PreviousPageText="Previous" />
                                                                                            <RowStyle ForeColor="Black" />
                                                                                            <Columns>
                                                                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="CURRENCY_ID" HeaderText="CURRENCY_ID">
                                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:CheckBox ID="cbQuotation" runat="server" Checked='<%# Eval("IS_QUOTATION")%>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="cbQuotationAll" runat="server" onclick="javascript:HeaderClick(this);">
                                                                                                        </asp:CheckBox>
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                                                                                    <ItemStyle Width="350px" HorizontalAlign="Left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="DOCDATE" HeaderText="Enquiry Date" DataFormatString="{0:dd/MM/yyyy}">
                                                                                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                                                                </asp:BoundField>
                                                                                                <asp:TemplateField HeaderText="Currency">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:DropDownList ID="ddlCurrency" runat="server" Width="55px">
                                                                                                        </asp:DropDownList>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Quantity">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtQuantity" runat="server" Width="50px" Text='<%# Eval("QUANTITY")%>'></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Rate">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtRate" runat="server" Width="50px" Text='<%# Eval("RATE")%>'></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtAmount" runat="server" Width="100px" Text='<%# Eval("Amount")%>'></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Remarks">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtRemarks" runat="server" Width="150px" Text='<%# Eval("DURATION")%>'></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Add Detail">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="btnAddDetail" runat="server" CommandArgument='<%# Eval("ENQUIREY_DETAIL_ID") %>'
                                                                                                            CommandName="detail" Text="Add Detail"></asp:LinkButton>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="100px" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:BoundField DataField="fnIsEnquiryPaymentReceived" HeaderText="fnIsEnquiryPaymentReceived">
                                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                                </asp:BoundField>
                                                                                                <asp:BoundField DataField="ENQUIREY_DETAIL_ID" HeaderText="ENQUIREY_DETAIL_ID">
                                                                                                    <HeaderStyle CssClass="HidePanel" />
                                                                                                    <ItemStyle CssClass="HidePanel" />
                                                                                                </asp:BoundField>
                                                                                            </Columns>
                                                                                            <FooterStyle BackColor="White" />
                                                                                            <PagerStyle BackColor="Transparent" />
                                                                                            <HeaderStyle CssClass="tblhead" />
                                                                                            <AlternatingRowStyle BackColor="#ffffff" />
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Literal ID="ltrlSignature" Visible="false" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Label ID="lblFrom" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Label ID="lblFromDesignation" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Label ID="lblFromPhone" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Label ID="lblFromAddress" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Label ID="lblFromEmail" runat="server" Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 5%;">
                                                                    </td>
                                                                    <td style="width: 95%;">
                                                                        <asp:Literal ID="ltrlTerms" Visible="false" runat="server"></asp:Literal>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <asp:HiddenField ID="hfQuotationIndex" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            &nbsp;<asp:Button ID="btnSaveQuotation" runat="server" CssClass="button" OnClick="btnSaveQuotation_Click"
                                                                Text="Save Quotation" />
                                                            &nbsp;<asp:Button ID="btnSendQuotation" runat="server" CssClass="button" Text="Email Quotation"
                                                                OnClick="btnSendQuotation_Click" />
                                                            &nbsp;<asp:Button ID="btnWord" runat="server" OnClick="btnWord_Click" CssClass="button"
                                                                Text="Export Word" />
                                                            &nbsp;<asp:Button ID="btnPDF" runat="server" CssClass="button" Text="Export PDF"
                                                                OnClick="btnPDF_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnWord" />
                                                <asp:PostBackTrigger ControlID="btnPDF" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>                                   
                                </tr>
                            </table>
                        </asp:Panel>                        
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlInvoice" runat="server" CssClass="HidePanel">
                         <asp:UpdatePanel ID="upInvoice" runat="server">
                                            <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <asp:Literal ID="ltrlInvoice" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="width: 100%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 5%;">
                                                </td>
                                                <td style="width: 10%;">
                                                    <strong>
                                                        <asp:Label ID="Label19" runat="server" Text="Email Template:" Width="100px"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 35%;">
                                                    <asp:DropDownList ID="ddlInvoiceEmailFrom" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 1%;">
                                                </td>
                                                <td style="width: 10%;">
                                                    <strong>
                                                        <asp:Label ID="Label1410" runat="server" Text="Cc:" Width="100px"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 39%;">
                                                    <uc1:MultiCheckCombo ID="cblInvoiceCc" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%;">
                                                </td>
                                                <td style="width: 10%;">
                                                    <strong>
                                                        <asp:Label ID="lblEmailFromInvoice" runat="server" Text="Email From:" Width="100px"
                                                            Visible="false"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 35%;">
                                                    <asp:DropDownList ID="ddlEmailFromInvoice" runat="server" Width="200px" Visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 1%;">
                                                </td>
                                                <td style="width: 10%;">
                                                    <input id="btnAddCc" value="Add more" type="button" class="Button" size="8" onclick="ShowCcTextBox();" />
                                                </td>
                                                <td style="width: 39%;">
                                                    <asp:TextBox ID="txtCc3" runat="server" Width="200px" CssClass="HidePanel"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 5%;">
                                                </td>
                                                <td style="width: 10%;">
                                                    <strong>
                                                        <asp:Label ID="Label140" runat="server" Text="Email To:" Width="100px"></asp:Label>
                                                    </strong>
                                                </td>
                                                <td style="width: 35%;">
                                                    <asp:DropDownList ID="ddlEmailToInvoice" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 1%;">
                                                </td>
                                                <td style="width: 10%;">
                                                </td>
                                                <td style="width: 39%;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%" align="right">
                                        <strong>
                                            <%--Reference #:--%>
                                            <asp:Label ID="lblInvoiceReference" runat="server" Visible="false"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%" align="center">
                                        <%-- <b><u>INVOICE</u></b>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <%-- <b>Bill to:</b>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <strong>
                                            <asp:Label ID="lblBillTo" runat="server" Visible="false"></asp:Label>
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <b>
                                            <asp:Label ID="lblBillCompany" runat="server" Visible="false"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <asp:LinkButton ID="btnNewInvoice" runat="server" Text="Data from last Invoice" OnClick="btnNewInvoice_Click"
                                            Visible="false"></asp:LinkButton>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnNewInvoiceFromQuotion" runat="server"
                                            Text="Data from Quotation" OnClick="btnNewInvoiceFromQuotion_Click" Visible="false"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 10%">
                                                    <strong>
                                                        <asp:Label ID="lblInvoiceDate" Text="Invoice Date" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" Width="80px" CssClass="gvDate" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <strong>
                                                        <asp:Label ID="lblConversion" Text="Conversion Rate" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:TextBox ID="txtConversion" runat="server" Width="120px" Text="1" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                    <strong>
                                                        <asp:Label ID="lblUOM" Text="UOM" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:DropDownList ID="ddlUOM" runat="server" Width="120px" Visible="false">
                                                        <asp:ListItem Value="0" Text="No of Users"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="No of Months"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="No of Years"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 15%">
                                                    <strong>
                                                        <asp:Label ID="lblCurrency" Text="Currency" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:DropDownList ID="ddlCurrency" runat="server" Width="120px" Visible="false">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                    <strong>
                                                        <asp:Label ID="lblIncomeTax" Text="Income Tax(%)" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtIncomeTax" runat="server" Width="80px" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <strong>
                                                        <asp:Label ID="lblServiceTax" Text="Service Tax(%)" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:TextBox ID="txtServiceTax" runat="server" Width="120px" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 10%">
                                                    <strong>
                                                        <asp:Label ID="lblPONo" Text="P.O #" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 30%">
                                                    <asp:TextBox ID="txtPONo" runat="server" Width="80px" Visible="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 15%">
                                                    <strong>
                                                        <asp:Label ID="lblPODate" Text="P.O Date" runat="server" Visible="false"></asp:Label></strong>
                                                </td>
                                                <td style="width: 45%">
                                                    <asp:TextBox ID="txtPODate" runat="server" Width="120px" Visible="false" CssClass="gvDate"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                        <asp:HiddenField ID="hfMaxInvoiceID" runat="server" />
                                        <asp:HiddenField ID="hfPDFFileName" runat="server" />
                                    </td>
                                    <td style="width: 95%">
                                       
                                                <asp:GridView ID="gvInvoice" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInvoice_RowDataBound"
                                                    GridLines="Horizontal" OnRowCommand="gvInvoice_RowCommand" HorizontalAlign="Center"
                                                    Width="99%" CellSpacing="3" OnRowCreated="gvInvoice_RowCreated">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <RowStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CURRENCY_ID" HeaderText="CURRENCY_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbInvoice" runat="server" Checked='<%# Eval("IS_INVOICE")%>' />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbInvoiceAll" runat="server" onclick="javascript:HeaderClickInv(this);">
                                                                </asp:CheckBox>
                                                            </HeaderTemplate>
                                                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="SKU_NAME" HeaderText="Item Description">
                                                            <ItemStyle Width="350px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Payment Type">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlPaymentType" runat="server" Width="70px">
                                                                    <asp:ListItem Text="One Time" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Monthly Recurring" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Quaterly Recurring" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Semi-Yearly Recurring" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="Yearly Recurring" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Date Wise Recurring" Value="5"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sub. Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtSubDate" Text='<%# Eval("SUBSCRIPTION_DATE")%>' runat="server"
                                                                    CssClass="gvDate" Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Exp. Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpDate" Text='<%# Eval("EXPIRY_DATE")%>' runat="server" CssClass="gvDate"
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server" Width="50px" Text='<%# Eval("QUANTITY")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                            <HeaderTemplate>
                                                                Quantity
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRate" runat="server" Width="50px" Text='<%# Eval("RATE")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="center" />
                                                            <HeaderTemplate>
                                                                Price
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Line Total">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTotal" runat="server" Width="80px" Text='<%# Eval("Total")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="80px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" Width="100px" Text='<%# Eval("REMARKS")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="fnIsEnquiryPaymentReceived" HeaderText="fnIsEnquiryPaymentReceived">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ENQUIREY_DETAIL_ID" HeaderText="ENQUIREY_DETAIL_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_TYPE" HeaderText="PAYMENT_TYPE">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" />
                                                    <PagerStyle BackColor="Transparent" />
                                                    <HeaderStyle CssClass="tblhead" />
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                </asp:GridView>
                                          
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%" align="center">
                                        <asp:Button ID="btnSaveInvoice" runat="server" OnClick="btnSaveInvoice_Click" CssClass="button"
                                            Text="Save Invoice" />
                                        <asp:Button ID="btnEmailInvoice" runat="server" OnClick="btnEmailInvoice_Click" CssClass="button"
                                            Text="Email Invoice" Visible="false" />
                                        <asp:Button ID="btnInvoicePDF" runat="server" OnClick="btnInvoicePDF_Click" CssClass="button"
                                            Text="Export PDF" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <asp:GridView ID="gvAllInvoice" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                                    HorizontalAlign="Center" Width="95%" CellSpacing="3" OnRowCommand="gvAllInvoice_RowCommand"
                                                    OnRowDataBound="gvAllInvoice_RowDataBound" OnRowCreated="gvAllInvoice_RowCreated">
                                                    <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                        PreviousPageText="Previous" />
                                                    <RowStyle ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundField DataField="INVOICE_MASTER_ID" HeaderText="INVOICE_MASTER_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CURRENCY_ID" HeaderText="CURRENCY_ID">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Invoice #">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton AlternateText="Reference No" ID="lnkref" runat="server" Text='<%# Eval("REFERENCE_NO")%>' ToolTip='<%# Eval("Remarks")%>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}">
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CURRENCY" HeaderText="Currency">
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InvoiceTotal" HeaderText="Invoice Amount">
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AmountStatus" HeaderText="Status">
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:dd-MMM-yyyy}">
                                                            <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Select Action">
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnEmail" runat="server" CommandName="email" CommandArgument='<%# Eval("PDFFileName") %>'
                                                                                ToolTip="Email Invoice" Width="35px"> 
                                                                                <img src="../images/Email.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnPDF" runat="server" CommandName="pdf" CommandArgument='<%# Eval("PDFFileName") %>'
                                                                                ToolTip="Export to PDF" Width="35px">
                                                                                <img src="../images/PDF.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnPDF2" runat="server" CommandName="pdf2" CommandArgument='<%# Eval("PDFFileName") %>'
                                                                                ToolTip="Export to PDF Without Header/Footer" Width="35px">
                                                                                <img src="../images/PDF2.png" width="25px" height="25px" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnWord" runat="server" CommandName="word" CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>'
                                                                                ToolTip="Export to Word" Width="35px">
                                                                                <img src="../images/Word.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="change" CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>'
                                                                                ToolTip="Edit Invoice" Width="35px">
                                                                                <img src="../images/Edit.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnActive" runat="server" CommandName='<%# Eval("IS_ACTIVE") %>'
                                                                                CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>' ToolTip="Active" Width="30px">
                                                                                <img src="../images/Active.png" />
                                                                                </asp:LinkButton>
                                                                            <asp:LinkButton ID="btnInActive" runat="server" CommandName='<%# Eval("IS_ACTIVE") %>'
                                                                                CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>' ToolTip="InActive" Width="30px">
                                                                                <img src="../images/In-Active.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 20px;">
                                                                            <asp:LinkButton ID="btnDuplicate" runat="server" CommandName="duplicate" CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>'
                                                                                ToolTip="Duplicate" Width="35px">
                                                                                <img src="../images/Duplicate.png" />
                                                                                </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="120px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="IS_ACTIVE" HeaderText="IS_ACTIVE">
                                                            <HeaderStyle CssClass="HidePanel" />
                                                            <ItemStyle CssClass="HidePanel" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="White" />
                                                    <PagerStyle BackColor="Transparent" />
                                                    <HeaderStyle CssClass="tblhead" />
                                                    <AlternatingRowStyle BackColor="#ffffff" />
                                                </asp:GridView>                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td style="width: 95%">
                                        <asp:Literal ID="ltrlInvoiceTerms" runat="server" Visible="false"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                              </ContentTemplate>
                                        </asp:UpdatePanel>
                            <asp:HiddenField ID="hfInvoiceIndex" runat="server" />
                            <asp:HiddenField ID="hfInvoiceID" runat="server" />
                        </asp:Panel>                        
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlPayment" runat="server" CssClass="HidePanel">
                            <asp:UpdatePanel ID="upPayment" runat="server">
                                <ContentTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="3" style="width: 100%;">
                                                <asp:Literal ID="ltrlPayment" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>
                                                    <asp:Label ID="lblrefNo" runat="server" Text="Select Invoice #"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:DropDownList ID="ddlPaymentProduct" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>
                                                    <asp:Label ID="lblInv" runat="server" Text="Invoice #" Visible="false"></asp:Label>
                                                </strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:Label ID="lblInvoiceNo" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>Payment Ref #</strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:TextBox ID="txtPaymentRef" runat="server" Width="150px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>Payment Mode</strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="150px">
                                                    <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Online Transfer" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>Amount Received</strong>
                                            </td>
                                            <td style="width: 1%">
                                                <asp:HiddenField ID="hfEnquiryPaymentID" runat="server" Value="0" />
                                            </td>
                                            <td style="width: 79%">
                                                <asp:TextBox ID="txtTotalAmount" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                <strong>Amount Received Date</strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:TextBox ID="txtPaymentDate" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%" valign="top">
                                                <strong>Remarks</strong>
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:TextBox ID="txtRemarksPayment" runat="server" TextMode="MultiLine" Rows="5"
                                                    Columns="30"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                            </td>
                                            <td style="width: 1%">
                                            </td>
                                            <td style="width: 79%">
                                                <asp:Button ID="btnSavePayment" runat="server" Text="Save" CssClass="button" OnClick="btnSavePayment_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="gvPayment" runat="server" Width="95%" HorizontalAlign="Center"
                                                    BorderColor="White" BackColor="White" OnRowDataBound="gvPayment_RowDataBound"
                                                    AutoGenerateColumns="False" OnRowEditing="gvPayment_RowEditing" OnRowDeleting="gvPayment_RowDeleting"
                                                    EnableModelValidation="True" OnRowCommand="gvPayment_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="ENQUIREY_DETAIL_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ENQUIRY_PAYMENT_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ENQUIRY_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="INVOICE_ID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_TYPEID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_MODEID">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CHEQUE_NO">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BANK_NAME">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CHEQUE_DATE">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SLIP_NO">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REMARKS">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="REFERENCE_NO" HeaderText="Invoice #">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px">
                                                            </ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_MODE" HeaderText="Payment Mode">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px">
                                                            </ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_REFERENCE" HeaderText="Payment Ref">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px">
                                                            </ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="AMOUNT" HeaderText="Amount">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="80px">
                                                            </ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PAYMENT_DATE" HeaderText="Payment Date" DataFormatString="{0:dd/MM/yyyy}">
                                                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" Width="100px">
                                                            </ItemStyle>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IS_ACTIVE">
                                                            <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                                            <ItemStyle CssClass="HidePanel"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Select Action">
                                                            <ItemTemplate>
                                                                <table width="90%">
                                                                    <tr>
                                                                        <td style="width: 30%">
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("ENQUIRY_PAYMENT_ID") %>'
                                                                                ToolTip="Edit" Text="Edit" Width="20px"></asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 30%">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" CommandArgument='<%# Eval("ENQUIRY_PAYMENT_ID") %>'
                                                                                ToolTip="Delete" Text="Delete" Width="20px" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this payment?');"></asp:LinkButton>
                                                                        </td>
                                                                        <td style="width: 40%">
                                                                            <asp:LinkButton ID="btnActive" runat="server" CommandName='<%# Eval("IS_ACTIVE") %>'
                                                                                CommandArgument='<%# Eval("INVOICE_MASTER_ID") %>' ToolTip="Active" Text='<%# Eval("IS_ACTIVE") %>'
                                                                                Width="30px"></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="tblhead"></HeaderStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:HiddenField ID="hfPaymentIndex" runat="server" />
                        </asp:Panel>                        
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlDocument" runat="server" CssClass="HidePanel">
                            <table cellspacing="0" cellpadding="3" border="0" width="80%">
                                <tr>
                                    <td>
                                        <asp:Literal ID="ltrlDoc" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="3" border="0">
                                            <tr>
                                                <td>
                                                    <strong>Title </strong>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <strong>Document Type</strong>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDocTitle" Width="200" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDoc" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="fuDoc" runat="server" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAddDoc" runat="server" Text="Save" CssClass="Button" OnClick="btnAddDoc_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="upDocumentView" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                    OnRowCommand="gvDocument_RowCommand" Width="100%" DataKeyNames="DOCUMENT_ID"
                                                    GridLines="Horizontal" EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no document to display.</div>"
                                                    OnRowDataBound="gvDocument_RowDataBound">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Document">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton AlternateText="View" ID="lnkViewDoc" runat="server" CommandName="view"
                                                                    CommandArgument='<%# Eval("DOCUMENT_ID")%>'>
                                        <%# Eval("TITLE")%>, <%# Eval("NAME")%>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doc Type">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <%# Eval("SLASH_DESC")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Uploaded By">
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <%# Eval("USER_NAME")%>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TIME_STAMP" HeaderText="DateTime" HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:ImageButton AlternateText="View" ToolTip="View Document" ID="btnViewDoc" runat="server"
                                                                                ImageUrl="~/images/view.gif" CommandName="view" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />
                                                                        </td>
                                                                        <td width="2%">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton AlternateText="Delete" ToolTip="Delete Document" ID="btnDel" runat="server"
                                                                                ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this document?');"
                                                                                CommandName="del" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Right" Width="10px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <HeaderStyle BackColor="#006699" ForeColor="White" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfDocumentIndex" runat="server" />
                        </asp:Panel>
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:Panel ID="pnlSendEmail" runat="server" CssClass="HidePanel">
                            <table width="100%">
                                <tr>
                                    <td colspan="3">
                                        <asp:Literal ID="ltrlSendEmail" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="Label16" runat="server" Text="Email Type:" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlType" runat="server" Width="200px">
                                            <asp:ListItem Text="Quotation" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Invoice" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="Label13" runat="server" Text="Email Template:" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlEmailTemplate" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="lblEmailFrom" runat="server" Text="Email From:" Width="100px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlEmailFromQuot" runat="server" Width="200px" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="lblEmailTo" runat="server" Text="Kind Attn:" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:DropDownList ID="ddlEmailTo" runat="server" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="lblCc2" runat="server" Text="Cc:" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 95%;">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 60%">
                                                    <uc1:MultiCheckCombo ID="cblQuotationCc2" runat="server" />
                                                </td>
                                                <td align="right" style="width: 5%">
                                                </td>
                                                <td style="width: 35%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <input id="btnAddmore2" value="Add more" type="button" class="Button" size="8" onclick="ShowCcTextBox2();" />
                                    </td>
                                    <td style="width: 90%;">
                                        <asp:TextBox ID="txtCc22" runat="server" Width="200px" CssClass="HidePanel"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%;">
                                    </td>
                                    <td style="width: 5%;">
                                        <asp:Label ID="Label18" runat="server" Text="Attachment:" Width="100px"></asp:Label>
                                    </td>
                                    <td style="width: 90%;">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 40%">
                                                    <asp:FileUpload ID="fuAttachment" runat="server" />
                                                </td>
                                                <td style="width: 60%">
                                                    <asp:Button ID="btnSendEmail" runat="server" Text="Send Email" CssClass="Button"
                                                        OnClick="btnSendEmail_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hfSendEmailIndex" runat="server" />
                        </asp:Panel>
                    </div>
                    <asp:HiddenField ID="ActiveTabIndex" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
