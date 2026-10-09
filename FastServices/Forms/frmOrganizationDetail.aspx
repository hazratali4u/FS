<%@ Page Title="Fast Services: Organization Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOrganizationDetail.aspx.cs" Inherits="Forms_frmOrganizationDetail" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<link href="../SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css">
<script src="../SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>

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
 <script language="JavaScript" type="text/javascript">

     function pageLoad() {

         //find the current popup
         var popUp = $find('ModelPopup');

         //check it exists so the script won't fail
         if (popUp) {
             //Add the function below as the event
             popUp.add_hidden(HidePopupPanel);
         }

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
         else {
             TabbedPanels1.showPanel(parseInt(ActiveTabIndex));
         }

         $('#<%=pnlContact.ClientID %>').show();
         $('#<%=pnlEdit.ClientID %>').show();

         $('#<%=txtDOB.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    changeMonth: true
                });
                
     }
     function AddAddressPeople() {
         var counteradd = $('#<%=hfAddCounterOrganization.ClientID %>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Address ' + counteradd);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Address');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddPhonePeople() {
         var counterph = $('#<%=hfPhoneCounterOrganization.ClientID %>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Phone ' + counterph);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Phone');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddCell() {
         var countercell = $('#<%=hfCellCounter.ClientID %>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Cell ' + countercell);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Cell');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddFax() {
         var counterfax = $('#<%=hfFaxCounter.ClientID %>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Fax ' + counterfax);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Fax');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddEmailPeople() {
         var counteremail = $('#<%=hfEmailCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Email ' + counteremail);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Email');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddCNIC() {
         var countercnic = $('#<%=hfCNICCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('CNIC ' + countercnic);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('CNIC');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddPassport() {
         var counterpass = $('#<%=hfPassportCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Passport ' + counterpass);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Passport');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }
     function AddCitizenship() {
         var countercitizen = $('#<%=hfCitizenshipCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Citizenship ' + countercitizen);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Citizenship');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }
     function AddName() {
         var countername = $('#<%=hfEmergencyNameCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Emergency Name ' + countername);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Emergency Name');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }
     function AddRelation() {
         var counterrelation = $('#<%=hfRelationCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Relation ' + counterrelation);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Relation');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }
     function AddHomePhone() {
         var counterhomeph = $('#<%=hfHomePhoneCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Home Phone ' + counterhomeph);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Home Phone');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddWorkPhone() {
         var counterworkph = $('#<%=hfWorkPhoneCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Work Phone ' + counterworkph);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Work Phone');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddHomeAddress() {
         var counterhomeadd = $('#<%=hfHomeAddressCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Home Address ' + counterhomeadd);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Home Address');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddWorkAddress() {
         var counterworkadd = $('#<%=hfWorkAddressCounter.ClientID%>').val();
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Work Address ' + counterworkadd);
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Work Address');
         $('#<%=txtValueOrganization.ClientID %>').focus();
     }

     function AddCustomPeople() {
         $("#divFieldOrganization").show();
         $("#divValueOrganization").show();
         $('#<%=lblFieldOrganization.ClientID %>').val('Custom ');
         $('#<%=hfFieldTypeOrganization.ClientID %>').val('Custom');
         $('#<%=txtValueOrganization.ClientID %>').focus();
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

     function ValidateForm() {
         var str;
         str = document.getElementById('<%=txtName.ClientID%>').value;
         if (str == null || str.length == 0) {
             alert('Must enter Organization Name');
             return false;
         }
         return true;
     }

     function AddAddressOffice() {
         var counteraddoffice = $('#<%=hfAddOfficeCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Office Address ' + counteraddoffice);
         $('#<%=hfFieldType.ClientID %>').val('Office Address');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddAddressFactory() {
         var counteraddfac = $('#<%=hfAddFactoryCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Factory Address ' + counteraddfac);
         $('#<%=hfFieldType.ClientID %>').val('Factory Address');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddEmail() {
         var counteremail = $('#<%=hfEmailCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Email ' + counteremail);
         $('#<%=hfFieldType.ClientID %>').val('Email');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddPhoneOffice() {
         var counterphoneoff = $('#<%=hfPhoneOfficeCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Office Phone ' + counterphoneoff);
         $('#<%=hfFieldType.ClientID %>').val('Office Phone');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddPhoneFactory() {
         var counterphonefac = $('#<%=hfPhoneFactoryCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Factory Phone ' + counterphonefac);
         $('#<%=hfFieldType.ClientID %>').val('Factory Phone');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddFaxOffice() {
         var counterfaxoffice = $('#<%=hfFaxOfficeCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Office Fax ' + counterfaxoffice);
         $('#<%=hfFieldType.ClientID %>').val('Office Fax');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddFaxFactory() {
         var counterfaxfac = $('#<%=hfFaxFactoryCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Factory Fax ' + counterfaxfac);
         $('#<%=hfFieldType.ClientID %>').val('Factory Fax');
         $('#<%=txtValue.ClientID %>').focus();
     }


     function AddWebsite() {
         var counterwebsite = $('#<%=hfWebsiteCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Website ' + counterwebsite);
         $('#<%=hfFieldType.ClientID %>').val('Website');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddFacsimile() {
         var counterfacsimile = $('#<%=hfFacsimileCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Facsimile ' + counterfacsimile);
         $('#<%=hfFieldType.ClientID %>').val('Facsimile');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddUAN() {
         var counteruan = $('#<%=hfUANCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('UAN ' + counteruan);
         $('#<%=hfFieldType.ClientID %>').val('UAN');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddNTN() {
         var counterntn = $('#<%=hfNTNCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('NTN ' + counterntn);
         $('#<%=hfFieldType.ClientID %>').val('NTN');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddGST() {
         var counterGST = $('#<%=hfGSTCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('GST ' + counterGST);
         $('#<%=hfFieldType.ClientID %>').val('GST');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddBankAccount() {
         var counterBankAccount = $('#<%=hfBankAccountCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Bank Account ' + counterBankAccount);
         $('#<%=hfFieldType.ClientID %>').val('Bank Account');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddBranchCode() {
         var counterBranchCode = $('#<%=hfBranchCodeCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Branch Code ' + counterBranchCode);
         $('#<%=hfFieldType.ClientID %>').val('Branch Code');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddBranchAddress() {
         var counterBranchAddress = $('#<%=hfBranchAddressCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Branch Address ' + counterBranchAddress);
         $('#<%=hfFieldType.ClientID %>').val('Branch Address');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddSwiftCode() {
         var counterSwiftCode = $('#<%=hfSwiftCodeCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Swift Code ' + counterSwiftCode);
         $('#<%=hfFieldType.ClientID %>').val('Swift Code');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddIBAN() {
         var counterIBAN = $('#<%=hfIBANCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('IBAN ' + counterIBAN);
         $('#<%=hfFieldType.ClientID %>').val('IBAN');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddRTGS() {
         var counterRTGS = $('#<%=hfRTGSCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('RTGS ' + counterRTGS);
         $('#<%=hfFieldType.ClientID %>').val('RTGS');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddOthers() {
         var counterOthers = $('#<%=hfOthersCounter.ClientID %>').val();
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('PABX Cell ' + counterOthers);
         $('#<%=hfFieldType.ClientID %>').val('PABX Cell');
         $('#<%=txtValue.ClientID %>').focus();
     }

     function AddCustom() {
         $("#divField").show();
         $("#divValue").show();
         $('#<%=lblField.ClientID %>').val('Custom ');
         $('#<%=hfFieldType.ClientID %>').val('Custom');
         $('#<%=txtValue.ClientID %>').focus();
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
<div id="action">
<asp:UpdatePanel id="UpdatePanel8" runat="server">
                        <contenttemplate>
<table>
    <tr>
        <td style="width:20%">
            <strong> <asp:Label ID="lblName" runat="server" Text="Organization Name"></asp:Label></strong>
        </td>
        <td style="width:1%">&nbsp;</td>
        <td style="width:23%">
            <asp:Label ID="lblName2" runat="server" Text="Organization Name"></asp:Label>
        </td>
        <td style="width:5%">&nbsp;</td>
        <td style="width:20%">
            <strong> <asp:Label ID="lblType" runat="server" Text="Organization Type"></asp:Label></strong>
        </td>
        <td style="width:1%">&nbsp;</td>
        <td style="width:30%">
            <asp:Label ID="lblType2" runat="server"></asp:Label>
        </td>
    </tr>
</table>
</contenttemplate>
</asp:UpdatePanel>
    <div id="tab_done"></div><h2>Action </h2>
    
    <div id="TabbedPanels1" class="TabbedPanels">

    <asp:Literal ID="ltrlTabs" runat="server"></asp:Literal>
        <div class="TabbedPanelsContentGroup">
        
        <div class="TabbedPanelsContent">            
        <asp:UpdatePanel id="UpdatePanel7" runat="server">
                        <contenttemplate>     
                        <asp:HiddenField ID="hfTabIndex" runat="server" />                        
          <table width="100%">
              <tbody>

                  <tr>
                                                        <td style="width:20%">
                                                             <strong> <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:Label ID="lblCountry2" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                           <strong> <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:Label ID="lblCity2" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                  <tr>
                      <td style="width:100%;height:10px" colspan="7">
                                                            <asp:GridView ID="gvField" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                                Width="100%" GridLines="None" ShowHeader="false" >
                                                                <Columns>
                                                                    <asp:TemplateField >
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                        <ItemTemplate>
                                                                            <table width="100%">
                                                                                <tbody>
                                                                                 <tr>
                                                                                        <td style="width:20%">
                                                                                             <strong> <asp:Label ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>'></asp:Label></strong>
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:79%">
                                                                                         <asp:Label ID="lblFldValue" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:Label>
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
          </contenttemplate>
          </asp:UpdatePanel>        
        </div>

        <div class="TabbedPanelsContent">
          <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                        <asp:Panel ID="pnlEdit" runat="server" CssClass="HidePanel">
                        <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width:100%;"> 
                                        <asp:Literal ID="ltrlEdit" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                        <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="Label1" runat="server" Text="Organization Name"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:TextBox ID="txtName" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="Label2" runat="server" Text="Organization Name"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblOrgType" runat="server" Text="Organization Type"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:DropDownList id="ddlType" runat="server" Width="180">
                                                                <asp:ListItem Text="Customer" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="Label4" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                             <strong> <asp:Label ID="Label5" runat="server" Text="Country"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                          <asp:DropDownList ID="ddlCountry" runat="server" Width="180px" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:Label ID="Label6" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                           <strong> <asp:Label ID="Label7" runat="server" Text="City"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                             <asp:DropDownList ID="ddlCity" runat="server" Width="180px"></asp:DropDownList>
                                                            <asp:Label ID="Label8" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100%;height:10px" colspan="7">
                                                            <asp:GridView ID="gvFieldEdit" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                                Width="100%" GridLines="None" ShowHeader="false" 
                                                                onrowcommand="gvField_RowCommand" onrowediting="gvField_RowEditing" 
                                                                onrowdatabound="gvField_RowDataBound" >
                                                                <Columns>
                                                                    <asp:TemplateField >
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                        <ItemTemplate>
                                                                            <table width="100%">
                                                                                <tbody>
                                                                                 <tr>
                                                                                        <td style="width:20%">
                                                                                             <strong> 
                                                                                                <%--<asp:Label ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>'></asp:Label>--%>
                                                                                                <asp:TextBox ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>' BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                                             </strong>
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:23%">
                                                                                         <asp:TextBox ID="txtVl" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:TextBox>
                                                                                         <asp:ImageButton AlternateText="Delete" ToolTip="Delete Field" ID="btnDelete" runat="server"
                                                                                            ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this Field?');"
                                                                                            CommandName="del" CommandArgument='<%# Eval("ORGANIZATION_DETAIL_ID")%>' />  
                                                                                        </td>
                                                                                        <td style="width:5%">&nbsp;</td>
                                                                                        <td style="width:20%">
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:30%">
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
                                                        <td colspan="3" style="width:44%">
                                                            <div id="divAdd" runat="server">
                                                               <span class="open"> 
                                                                    <a href="javascript:void()" style="text-decoration:none;"> Add <img src="../App_Themes/Granite/Images/arrow_down_dark.png" /> </a>
                                                                    <div style="margin-top:-200px;">
                                                                    <asp:Panel ID="pnlFields" runat="server" Width="175px" Height="200px" ScrollBars="Vertical">
                                                                     <div style="background-color: #f5f5f6; border: solid 1px #a0a0a0; width:150px;">
                                                                       <br />
                                                                       <a href='javascript:void()' style="text-decoration:none;" onclick="AddEmail();">Email</a>
                                                                       <a href='javascript:void()' style="text-decoration:none;" onclick="AddWebsite();">Website</a>
                                                                       
                                                                         <a href='javascript:void()' style="text-decoration:none;" onclick="AddPhoneOffice();">Office Phone</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddPhoneFactory();">Factory Phone</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddAddressOffice();">Office Address</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddAddressFactory();">Factory Address</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddFaxOffice();">Office Fax</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddFaxFactory();">Factory Fax</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddOthers();">PABX Cell</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddFacsimile();">Facsimile</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddUAN();">UAN</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddNTN();">NTN</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddGST();">GST</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddBankAccount();">Bank Account #</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddBranchCode();">Branch Code</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddBranchAddress();">Branch Address</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddSwift Code();">Swift Code</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddIBAN();">IBAN</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddRTGS();">RTGS</a>        
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddCustom();">Custom</a>                                                                
                                                                    </div>
                                                                    </asp:Panel>
                                                                    </div>
                                                                </span>                         
                                                            </div>                                       
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <div id="divField" style="display:none;">
                                                                <strong> 
                                                                    <%--<asp:Label ID="lblField" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblField" runat="server" BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                </strong>
                                                            </div>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:48%" colspan="3">
                                                            <div id="divValue" style="display:none;">
                                                                <asp:TextBox ID="txtValue" runat="server" Width="180"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvFieldValue" runat="server" ErrorMessage="*" ControlToValidate="txtValue" ValidationGroup="vgField"></asp:RequiredFieldValidator>
                                                                <asp:Button ID="btnAddField" runat="server" Text="Add" ValidationGroup="vgField" CssClass="ButtonSmall" 
                                                                    onclick="btnAddField_Click" />
                                                                <asp:HiddenField ID="hfFieldType" runat="server" />
                                                                <asp:HiddenField ID="hfAddOfficeCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfAddFactoryCounter" runat="server" Value="1" />

                                                                 <asp:HiddenField ID="hfFaxOfficeCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfFaxFactoryCounter" runat="server" Value="1" />

                                                                <asp:HiddenField ID="hfEmailCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfWebsiteCounter" runat="server" Value="1" />
                                                                
                                                                <asp:HiddenField ID="hfPhoneOfficeCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfPhoneFactoryCounter" runat="server" Value="1" />

                                                                <asp:HiddenField ID="hfFacsimileCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfUANCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfNTNCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfGSTCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfBankAccountCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfBranchCodeCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfBranchAddressCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfSwiftCodeCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfIBANCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfRTGSCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfOthersCounter" runat="server" Value="1" />
                                                            </div>
                                                        </td>
                                                        
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                         &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                         <asp:Button ID="btnSave" runat="server" CssClass="Button" onclick="btnSave_Click" Text="Save" Width="80px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancel" runat="server" CssClass="Button" Text="Cancel" 
                                             Width="80px" onclick="btnCancel_Click"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hfEditIndex" runat="server" />
</contenttemplate>
                    </asp:UpdatePanel>
        </div>

        <div class="TabbedPanelsContent">
        <asp:Panel ID="pnlContact" runat="server" CssClass="HidePanel">
        <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                        <asp:Panel ID="pnlAddPeople" runat="server"  Width="100%">                        
                            <table width="100%">
                            <tbody>
                            <tr>
                                    <td style="width:100%;"> 
                                        <asp:Literal ID="ltrlAddContact" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label></strong>
                                                            
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:DropDownList id="ddlTitle" runat="server" Width="60">
                                                                <asp:ListItem Text="Mr." Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Ms." Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Mrs." Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label Visible="false" ID="lblTitle2" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                         <td style="width:20%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                           
                                                        </td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblFName" runat="server" Text="First Name"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                           <asp:TextBox ID="txtFName" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="lblFName2" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblLName" runat="server" Text="Last Name"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:TextBox ID="txtLName" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="lblLName2" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                           <asp:TextBox ID="txtEmail" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="lblEmail2" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblPhone" runat="server" Text="Phone"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:TextBox ID="txtPhone" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="lblPhone2" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblDOB" runat="server" Text="Date Of Birth"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:TextBox ID="txtDOB" runat="server" Width="100"></asp:TextBox>
                                                            <asp:Label ID="lblDOB2" Visible="false" runat="server" ></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblAccountNo" runat="server" Text="Account No"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:TextBox ID="txtAccountNo" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label ID="lblAccountNo2" Visible="false" runat="server" ></asp:Label>
                                                        </td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                          <asp:DropDownList ID="ddlDepartment" runat="server" Width="180">                                                                
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblDepartment2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                          <strong> <asp:Label ID="lblDesignation" runat="server" Text="Designation"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                           <asp:TextBox ID="txtDesignation" runat="server" Text="IT Manager" Width="180px">
                                                           </asp:TextBox>
                                                            <asp:Label ID="lblDesignation2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblLocation" runat="server" Text="Location" Visible="false"></asp:Label></strong>
                                                            
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:DropDownList id="ddlLocation" runat="server" Width="180" Visible="false"></asp:DropDownList>
                                                            <asp:Label ID="lblLocation2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                         <td style="width:20%">
                                                            <strong>Status</strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:CheckBox ID="cbActive" runat="server" Text="Active" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100%;height:10px" colspan="7">
                                                            <asp:GridView ID="gvFieldContact" runat="server" AutoGenerateColumns="False" CellPadding="5"
                                                                Width="100%" GridLines="None" ShowHeader="false" 
                                                                onrowcommand="gvFieldContact_RowCommand" onrowediting="gvFieldContact_RowEditing" 
                                                                onrowdatabound="gvFieldContact_RowDataBound" >
                                                                <Columns>
                                                                    <asp:TemplateField >
                                                                        <HeaderStyle HorizontalAlign="Left" Width="20%" />
                                                                        <ItemTemplate>
                                                                            <table width="100%">
                                                                                <tbody>
                                                                                 <tr>
                                                                                        <td style="width:20%">
                                                                                             <strong> 
                                                                                                <asp:TextBox ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>' BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                                                <%--<asp:Label ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>'></asp:Label>--%>
                                                                                             </strong>
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:23%">
                                                                                         <asp:TextBox ID="txtVl" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:TextBox>
                                                                                         <asp:Label ID="lblFld2" runat="server" Visible="false"></asp:Label>
                                                                                         <asp:ImageButton AlternateText="Delete" ToolTip="Delete Field" ID="btnDelete" runat="server"
                                                                                            ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this Field?');"
                                                                                            CommandName="del" CommandArgument='<%# Eval("PEOPLE_DETAIL_ID")%>' />  
                                                                                        </td>
                                                                                        <td style="width:5%">&nbsp;</td>
                                                                                        <td style="width:20%">
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:30%">
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
                                                        <td colspan="3" style="width:44%">
                                                            <div id="div3" runat="server">
                                                               <span class="open"> 
                                                                    <a href="javascript:void()" style="text-decoration:none;"> Add <img src="../App_Themes/Granite/Images/arrow_down_dark.png" /> </a>
                                                                    <div style="margin-top:-200px;">
                                                                    <asp:Panel ID="pnlContactDetail" runat="server" Width="175px" Height="200px" ScrollBars="Vertical">
                                                                     <div style="background-color: #f5f5f6; border: solid 1px #a0a0a0; width:150px;">
                                                                       <br />
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddAddressPeople();">Address</a>                                                                        
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddPhonePeople();">Phone</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddCell();">Cell</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddFax();">Fax</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddEmailPeople();">Email</a>

                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddCNIC();">CNIC</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddPassport();">Passport </a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddCitizenship();">Citizenship</a>
                                                                           <span style="color:#006699;">
                                                                           ....................................
                                                                           <br />
                                                                           Emergency Locator
                                                                           <br />
                                                                           ....................................
                                                                           </span>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddName();">Name</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddRelation();">Relation</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddHomePhone();">Home Phone</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddWorkPhone();">Work Phone</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddHomeAddress();">Home Address</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddWorkAddress();">Work Address</a>
                                                                        <a href='javascript:void()' style="text-decoration:none;" onclick="AddCustomPeople();">Custom</a>        
                                                                    </div>
                                                                    </asp:Panel>
                                                                    </div>
                                                                </span>                         
                                                            </div>                                       
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <div id="divFieldOrganization" style="display:none;">
                                                                <strong> 
                                                                    <asp:TextBox ID="lblFieldOrganization" runat="server" BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                    <%--<asp:Label ID="lblFieldOrganization" runat="server"></asp:Label>--%>
                                                                </strong>
                                                            </div>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:48%" colspan="3">
                                                            <div id="divValueOrganization" style="display:none;">
                                                                <asp:TextBox ID="txtValueOrganization" runat="server" Width="180"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtValueOrganization" ValidationGroup="vgFieldOrg"></asp:RequiredFieldValidator>
                                                                <asp:Button ID="btnAddFieldOrganization" runat="server" Text="Add" ValidationGroup="vgFieldOrg" CssClass="ButtonSmall" 
                                                                    onclick="btnAddFieldOrganization_Click" />
                                                                <asp:HiddenField ID="hfFieldTypeOrganization" runat="server" />
                                                                <asp:HiddenField ID="hfAddCounterOrganization" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfPhoneCounterOrganization" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfCellCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfFaxCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="HiddenField4" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfCNICCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfPassportCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfCitizenshipCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfEmergencyNameCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfRelationCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfHomePhoneCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfWorkPhoneCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfHomeAddressCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfWorkAddressCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfPeopleID" runat="server" />
                                                            </div>
                                                        </td>
                                                        
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                         &nbsp;
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                         <asp:Button ID="btnSaveContact" runat="server" CssClass="Button" onclick="btnSaveContact_Click" Text="Save" Width="80px" />
                                        &nbsp;
                                        <asp:Button ID="btnCancelContact" runat="server" CssClass="Button" Text="Cancel" 
                                             Width="80px" onclick="btnCancel_Click"/>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </asp:Panel>
                        <asp:HiddenField ID="hfContactIndex" runat="server" />
                        
</contenttemplate>
                    </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
        <asp:Panel ID="pnlPeopleList" runat="server" Width="90%">
                            <table width="100%">
                                <tbody>                                    
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <td class="tblhead" style="width:40%;">
                                                        Filter For &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlFilterFor" runat="server" Width="200px" >
                                                        <asp:ListItem Text="All Records" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="First Name" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Last Name" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Designation" Value="2"></asp:ListItem>                                                        
                                                            <asp:ListItem Text="Department" Value="3"></asp:ListItem>                                                        
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="tblhead" style="width:40%;">
                                                        Filter Text &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                                                                                   
                                                    </td>
                                                    <td class="tblhead" colspan="3" style="width:20%;">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Filter" 
                                                            onclick="btnSearch_Click"/>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <td class="tblhead" style="width:19%;">
                                                        First Name &nbsp;&nbsp;
                                                    </td>
                                                    <td class="tblhead" style="width:17%;">
                                                        Last Name &nbsp;&nbsp;
                                                    </td>
                                                     <td class="tblhead" style="width:17%;">
                                                        Designation &nbsp;&nbsp;
                                                    </td>
                                                    <td class="tblhead" style="width:17%;">
                                                        Department &nbsp;&nbsp;
                                                    </td>

                                                    <td class="tblhead" style="width:18%;">
                                                       Organization                                                                                                   
                                                    </td>
                                                    <td class="tblhead" style="width:6%;">
                                                       Status
                                                    </td>
                                                    <td class="tblhead" colspan="3" style="width:6%;">
                                                       
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rPeople" runat="server" OnItemCommand="rPeople_ItemCommand" onitemdatabound="rPeople_ItemDataBound">                                                
                                                <ItemTemplate>
                                                    <div style="background:#EFF3FB">
                                                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width:19%;"><%# Eval("FNAME") %> </td>
                                                                        <td style="width:17%;"><%# Eval("LNAME")%></td>
                                                                        <td style="width:17%;"><%# Eval("SLASH_DESC")%></td>
                                                                        <td style="width:17%;"><%# Eval("DEPARTMENT")%></td>
                                                                        <td style="width:18%;"><%# Eval("NAME")%></td>
                                                                        <td style="width:6%;"><%# Eval("Status")%></td>
                                                                        <td style="width:3%;">
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Edit People">
                                                                                <img src="../images/edit.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                          </td>
                                                                        <td style="width:3%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Delete People" OnClientClick="return confirm('Are you sure? you are going to delete this People.');">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background:#ffffff">
                                                        <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                                    <tr>
                                                                        <td style="width:19%;"><%# Eval("FNAME") %> </td>
                                                                        <td style="width:17%;"><%# Eval("LNAME")%></td>
                                                                        <td style="width:17%;"><%# Eval("SLASH_DESC")%></td>
                                                                        <td style="width:17%;"><%# Eval("DEPARTMENT")%></td>
                                                                        <td style="width:18%;"><%# Eval("NAME")%></td>
                                                                        <td style="width:6%;"><%# Eval("Status")%></td>
                                                                        <td style="width:3%;">
                                                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Edit People">
                                                                                <img src="../images/edit.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                          </td>
                                                                        <td style="width:3%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Delete People" OnClientClick="return confirm('Are you sure? you are going to delete this People.');">
                                                                                <img src="../images/delete.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                        </ContentTemplate>                    
                    </asp:UpdatePanel>
                    </asp:Panel>
        </div>
                                
        </div>
        <asp:HiddenField ID="ActiveTabIndex" runat="server" />
    </div>
    </div>
</div>
</asp:Content>

