<%@ Page Title="Fast Services: People Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmPeopleDetail.aspx.cs" Inherits="Forms_frmPeopleDetail"
    EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
    <link href="../SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css">
    <script src="../SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
    <style type="text/css">
        .open div
        {
            display: none;
            float: left;
            position: absolute;
            padding-top: 10;
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
    <script language="JavaScript" type="text/javascript">

        function pageLoad() {
            $('#<%=txtDOB.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
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
            else {
                TabbedPanels1.showPanel(parseInt(ActiveTabIndex));
            }

            $('#<%=pnlEdit.ClientID %>').show();

            //find the current popup
            var popUp = $find('ModelPopup');

            //check it exists so the script won't fail
            if (popUp) {
                //Add the function below as the event
                popUp.add_hidden(HidePopupPanel);
            }
        }
        function AddAddress() {
            var counteradd = $('#<%=hfAddCounter.ClientID %>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Address ' + counteradd);
            $('#<%=hfFieldType.ClientID %>').val('Address');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddPhone() {
            var counterph = $('#<%=hfPhoneCounter.ClientID %>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Phone ' + counterph);
            $('#<%=hfFieldType.ClientID %>').val('Phone');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddCell() {
            var countercell = $('#<%=hfCellCounter.ClientID %>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Cell ' + countercell);
            $('#<%=hfFieldType.ClientID %>').val('Cell');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddFax() {
            var counterfax = $('#<%=hfFaxCounter.ClientID %>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Fax ' + counterfax);
            $('#<%=hfFieldType.ClientID %>').val('Fax');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddEmail() {
            var counteremail = $('#<%=hfEmailCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Email ' + counteremail);
            $('#<%=hfFieldType.ClientID %>').val('Email');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddCNIC() {
            var countercnic = $('#<%=hfCNICCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('CNIC ' + countercnic);
            $('#<%=hfFieldType.ClientID %>').val('CNIC');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddPassport() {
            var counterpass = $('#<%=hfPassportCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Passport ' + counterpass);
            $('#<%=hfFieldType.ClientID %>').val('Passport');
            $('#<%=txtValue.ClientID %>').focus();
        }
        function AddCitizenship() {
            var countercitizen = $('#<%=hfCitizenshipCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Citizenship ' + countercitizen);
            $('#<%=hfFieldType.ClientID %>').val('Citizenship');
            $('#<%=txtValue.ClientID %>').focus();
        }
        function AddName() {
            var countername = $('#<%=hfEmergencyNameCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Emergency Name ' + countername);
            $('#<%=hfFieldType.ClientID %>').val('Emergency Name');
            $('#<%=txtValue.ClientID %>').focus();
        }
        function AddRelation() {
            var counterrelation = $('#<%=hfRelationCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Relation ' + counterrelation);
            $('#<%=hfFieldType.ClientID %>').val('Relation');
            $('#<%=txtValue.ClientID %>').focus();
        }
        function AddHomePhone() {
            var counterhomeph = $('#<%=hfHomePhoneCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Home Phone ' + counterhomeph);
            $('#<%=hfFieldType.ClientID %>').val('Home Phone');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddWorkPhone() {
            var counterworkph = $('#<%=hfWorkPhoneCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Work Phone ' + counterworkph);
            $('#<%=hfFieldType.ClientID %>').val('Work Phone');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddHomeAddress() {
            var counterhomeadd = $('#<%=hfHomeAddressCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Home Address ' + counterhomeadd);
            $('#<%=hfFieldType.ClientID %>').val('Home Address');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddWorkAddress() {
            var counterworkadd = $('#<%=hfWorkAddressCounter.ClientID%>').val();
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Work Address ' + counterworkadd);
            $('#<%=hfFieldType.ClientID %>').val('Work Address');
            $('#<%=txtValue.ClientID %>').focus();
        }

        function AddCustom() {
            $("#divField").show();
            $("#divValue").show();
            $('#<%=lblField.ClientID %>').val('Custom ');
            $('#<%=hfFieldType.ClientID %>').val('Custom');
            $('#<%=txtValue.ClientID %>').focus();
        }


        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(startRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);

        function startRequest(sender, e) {
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest(sender, e) {

            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
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
        <div id="action">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
            <div id="tab_done">
            </div>
            <h2>
                Action
            </h2>
            <div id="TabbedPanels1" class="TabbedPanels">
                <asp:Literal ID="ltrlTabs" runat="server"></asp:Literal>
                <div class="TabbedPanelsContentGroup">
                    <div class="TabbedPanelsContent">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hfTabIndex" runat="server" />
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label Visible="false" ID="lblTitle2" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblOrganizationType" runat="server" Text="Organization Type"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label ID="lblOrganizationType2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label ID="lblOrganization2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblLocation" runat="server" Text="Location" Visible="false"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblLocation2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label Visible="false" ID="lblFName2" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Label Visible="false" ID="lblLName2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label Visible="false" ID="lblEmail2" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Label Visible="false" ID="lblPhone2" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label ID="lblDOB2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                <strong> <asp:Label ID="lblAccountNo" runat="server" Text="Account No"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblAccountNo2" Visible="false" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 23%">
                                                <asp:Label ID="lblDepartment2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 5%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 20%">
                                                <strong>
                                                    <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label></strong>
                                            </td>
                                            <td style="width: 1%">
                                                &nbsp;
                                            </td>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblDesignation2" Visible="false" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; height: 10px" colspan="7">
                                                <asp:GridView ID="gvField" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                    Width="100%" GridLines="None" ShowHeader="false">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                            <ItemTemplate>
                                                                <table width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width: 20%">
                                                                                <strong>
                                                                                    <asp:Label ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>'></asp:Label>
                                                                                </strong>
                                                                            </td>
                                                                            <td style="width: 1%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 23%">
                                                                                <asp:Label ID="lblFld2" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 5%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 20%">
                                                                            </td>
                                                                            <td style="width: 1%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td style="width: 30%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="TabbedPanelsContent">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlEdit" runat="server" CssClass="HidePanel">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td style="width: 100%;">
                                                    <asp:Literal ID="ltrlEdit" runat="server"></asp:Literal>
                                                    <asp:HiddenField ID="hfPeopleID" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;">
                                                    <table width="100%">
                                                        <tbody>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label1" runat="server" Text="Title"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:DropDownList ID="ddlTitle" runat="server" Width="60">
                                                                        <asp:ListItem Text="Mr." Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Ms." Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Mrs." Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label3" runat="server" Text="Organization Type"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:DropDownList ID="ddlOrganizationType" runat="server" Width="180" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlOrganizationType_SelectedIndexChanged">
                                                                        <asp:ListItem Text="Customer" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Fast Services" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label5" runat="server" Text="Organization"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:DropDownList ID="ddlOrganization" runat="server" Width="180">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="lblLocationEdit" runat="server" Text="Location" Visible="false"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:DropDownList ID="ddlLocation" runat="server" Width="180" Visible="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label9" runat="server" Text="First Name"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:TextBox ID="txtFName" runat="server" Width="180"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label11" runat="server" Text="Last Name"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtLName" runat="server" Width="180"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label13" runat="server" Text="Email"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:TextBox ID="txtEmail" runat="server" Width="180"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label15" runat="server" Text="Phone"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtPhone" runat="server" Width="180"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label17" runat="server" Text="Date Of Birth"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:TextBox ID="txtDOB" runat="server" Width="100"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label4" runat="server" Text="Account No"></asp:Label>
                                                                    </strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtAccountNo" runat="server" Width="180"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label19" runat="server" Text="Department"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:DropDownList ID="ddlDepartment" runat="server" Width="180">
                                                                        <asp:ListItem Text="---- Select Department ----" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="IT" Value="IT"></asp:ListItem>
                                                                        <asp:ListItem Text="Marketing" Value="Marketing"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label21" runat="server" Text="Designation"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    <asp:TextBox ID="txtDesignation" runat="server" Width="180px" Text="IT Manager">
                                                                    </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <strong>
                                                                        <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label></strong>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 23%">
                                                                    <asp:CheckBox ID="cbActive" runat="server" Text="Active" />
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100%; height: 10px" colspan="7">
                                                                    <asp:GridView ID="gvFieldEdit" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                                        Width="100%" GridLines="None" ShowHeader="false" OnRowCommand="gvFieldEdit_RowCommand"
                                                                        OnRowEditing="gvFieldEdit_RowEditing" OnRowDataBound="gvFieldEdit_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                                <ItemTemplate>
                                                                                    <table width="100%">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 20%">
                                                                                                    <strong>
                                                                                                        <asp:TextBox ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>' BorderStyle="None"
                                                                                                            Font-Bold="true"></asp:TextBox>
                                                                                                    </strong>
                                                                                                </td>
                                                                                                <td style="width: 1%">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td style="width: 23%">
                                                                                                    <asp:TextBox ID="txtVl" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:TextBox>
                                                                                                    <asp:Label ID="lblFld2" runat="server" Visible="false"></asp:Label>
                                                                                                    <asp:ImageButton AlternateText="Delete" ToolTip="Delete Field" ID="btnDelete" runat="server"
                                                                                                        ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this Field?');"
                                                                                                        CommandName="del" CommandArgument='<%# Eval("PEOPLE_DETAIL_ID")%>' />
                                                                                                </td>
                                                                                                <td style="width: 5%">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td style="width: 20%">
                                                                                                </td>
                                                                                                <td style="width: 1%">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                                <td style="width: 30%">
                                                                                                    &nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" style="width: 44%">
                                                                    <div id="divAdd" runat="server">
                                                                        <span class="open"><a href="javascript:void()" style="text-decoration: none;">Add
                                                                            <img src="../App_Themes/Granite/Images/arrow_down_dark.png" />
                                                                        </a>
                                                                            <div style="margin-top: -200px;">
                                                                                <asp:Panel ID="pnlFields" runat="server" Width="175px" Height="200px" ScrollBars="Vertical">
                                                                                    <div style="background-color: #f5f5f6; border: solid 1px #a0a0a0; width: 150px;">
                                                                                        <br />
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddAddress();">Address</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddPhone();">Phone</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddCell();">Cell</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddFax();">Fax</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddEmail();">Email</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddCNIC();">CNIC</a>
                                                                                        <a href='javascript:void()' style="text-decoration: none;" onclick="AddPassport();">
                                                                                            Passport </a><a href='javascript:void()' style="text-decoration: none;" onclick="AddCitizenship();">
                                                                                                Citizenship</a> <span style="color: #006699;">....................................
                                                                                                    <br />
                                                                                                    Emergency Locator
                                                                                                    <br />
                                                                                                    .................................... </span><a href='javascript:void()' style="text-decoration: none;"
                                                                                                        onclick="AddName();">Name</a> <a href='javascript:void()' style="text-decoration: none;"
                                                                                                            onclick="AddRelation();">Relation</a> <a href='javascript:void()' style="text-decoration: none;"
                                                                                                                onclick="AddHomePhone();">Home Phone</a> <a href='javascript:void()' style="text-decoration: none;"
                                                                                                                    onclick="AddWorkPhone();">Work Phone</a> <a href='javascript:void()' style="text-decoration: none;"
                                                                                                                        onclick="AddHomeAddress();">Home Address</a> <a href='javascript:void()' style="text-decoration: none;"
                                                                                                                            onclick="AddWorkAddress();">Work Address</a>
                                                                                        <span style="color: #006699;">....................................
                                                                                            <br />
                                                                                            <a href='javascript:void()' style="text-decoration: none;" onclick="AddCustom();">Custom</a>
                                                                                        </span>
                                                                                    </div>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </span>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 5%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 20%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 20%">
                                                                    <div id="divField" style="display: none;">
                                                                        <strong>
                                                                            <asp:TextBox ID="lblField" runat="server" BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                        </strong>
                                                                    </div>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 48%" colspan="3">
                                                                    <div id="divValue" style="display: none;">
                                                                        <asp:TextBox ID="txtValue" runat="server" Width="180"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvFieldValue" runat="server" ErrorMessage="*" ControlToValidate="txtValue"
                                                                            ValidationGroup="vgField"></asp:RequiredFieldValidator>
                                                                        <asp:Button ID="btnAddField" runat="server" Text="Add" ValidationGroup="vgField"
                                                                            CssClass="ButtonSmall" OnClick="btnAddField_Click" />
                                                                        <asp:HiddenField ID="hfFieldType" runat="server" />
                                                                        <asp:HiddenField ID="hfAddCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfPhoneCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfCellCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfFaxCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfEmailCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfCNICCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfPassportCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfCitizenshipCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfEmergencyNameCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfRelationCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfHomePhoneCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfWorkPhoneCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfHomeAddressCounter" runat="server" Value="1" />
                                                                        <asp:HiddenField ID="hfWorkAddressCounter" runat="server" Value="1" />
                                                                    </div>
                                                                </td>
                                                                <td style="width: 1%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="width: 30%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="Button" OnClick="btnSave_Click"
                                                        Text="Save" Width="80px" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" Width="80px"
                                                        OnClick="btnCancel_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hfEditIndex" runat="server" />
                    </div>
                </div>
                <asp:HiddenField ID="ActiveTabIndex" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
