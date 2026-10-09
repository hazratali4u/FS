<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSupplier.aspx.cs" Inherits="Forms_frmSupplier" Title="Fast Services: Supplier"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
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
         $('#<%=lblField.ClientID %>').val('Factory Fax ' + counteraddfac);
         $('#<%=hfFieldType.ClientID %>').val('Factory Fax');
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

 </script>
 <div id="right_data">
    <div >
        <table width="95%">
            <tr>               
                <td style="width: 100px">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
                        <asp:Panel ID="pnlAddOrganization" runat="server" Visible="false" Width="100%">                        
                            <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width:100%;"> 
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        <asp:HiddenField ID="hfOrganizationID" runat="server" />
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                        <asp:Panel ID="pnlBasic" GroupingText="Basic Information" runat="server" Width="80%">
                                            <table width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td style="width:20%">
                                                            <strong> <asp:Label ID="lblName" runat="server" Text="Supplier Name"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:TextBox ID="txtName" runat="server" Width="180"></asp:TextBox>
                                                            <asp:Label Visible="false" ID="lblName2" runat="server"></asp:Label>
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
                                                             <strong> <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                          <asp:DropDownList ID="ddlCountry" runat="server" Width="180px" 
                                                                AutoPostBack="True" onselectedindexchanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:Label ID="lblCountry2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                        <td style="width:20%">
                                                           <strong> <asp:Label ID="lblCity" runat="server" Text="City"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                             <asp:DropDownList ID="ddlCity" runat="server" Width="180px"></asp:DropDownList>
                                                            <asp:Label ID="lblCity2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100%;height:10px" colspan="7">
                                                            <asp:GridView ID="gvField" runat="server" AutoGenerateColumns="False" CellPadding="5"
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
                                                                                                <asp:TextBox ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>' BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                                                <%--<asp:Label ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>'></asp:Label>--%>
                                                                                             </strong>
                                                                                        </td>
                                                                                        <td style="width:1%">&nbsp;</td>
                                                                                        <td style="width:79%">
                                                                                         <asp:TextBox ID="txtVl" runat="server" Text='<%# Eval("FIELD_VALUE")%>'></asp:TextBox>
                                                                                         <asp:Label ID="lblFld2" runat="server" Visible="false"></asp:Label>
                                                                                         <asp:ImageButton AlternateText="Delete" ToolTip="Delete Field" ID="btnDelete" runat="server"
                                                                                            ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this Field?');"
                                                                                            CommandName="del" CommandArgument='<%# Eval("ORGANIZATION_DETAIL_ID")%>' />  
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

                                                                <asp:HiddenField ID="hfWebsiteCounter" runat="server" Value="1" />
                                                                <asp:HiddenField ID="hfEmailCounter" runat="server" Value="1" />
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
                                        </asp:Panel> 
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
                        </contenttemplate>
                    </asp:UpdatePanel>
                    <asp:Panel ID="pnlOrganizationList" runat="server" Width="80%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td> <asp:LinkButton ID="btnNewSupplier" runat="server" Text="Add New Supplier" 
                                                onclick="btnNewSupplier_Click"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                        <td class="tblhead" style="width:59%;">                                                           
                                                             <asp:LinkButton ForeColor="White" ID="btnOrganizationSor" runat="server" 
                                                                Text="Supplier Name" onclick="btnOrganizationSor_Click">
                                                            </asp:LinkButton>   
                                                            <asp:TextBox ID="txtFilter" runat="server" Width="200px"></asp:TextBox>
                                                            <asp:Button ID="btnFilter" runat="server" Text="Filter" 
                                                                onclick="btnFilter_Click" />
                                                         </td>
                                                         <td class="tblhead" style="width:11%;">                                                            
                                                            <asp:LinkButton ForeColor="White" ID="btnCountrySor" runat="server" 
                                                                Text="Country" onclick="btnCountrySor_Click">
                                                            </asp:LinkButton>
                                                         </td>
                                                        <td class="tblhead" style="width:20%;">
                                                            Organization Type
                                                        </td>
                                                        <td class="tblhead" style="width:10%;" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rOrganization" runat="server" OnItemCommand="rOrganization_ItemCommand" onitemdatabound="rOrganization_ItemDataBound">                                                
                                                <ItemTemplate>
                                                    <div style="background:#EFF3FB">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width:59%;">                                                                        
                                                                             <asp:LinkButton ID="btnOrg" runat="server" CommandName="view" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="View Organization">
                                                                                <%# Eval("Name") %> 
                                                                            </asp:LinkButton>  
                                                                            <asp:HiddenField ID="hfOrganization" Value='<%# Eval("ORGANIZATION_ID") %>' runat="server" />
                                                                        </td>
                                                                        <td style="width:11%;"><%# Eval("CountryName")%></td>
                                                                        <td style="width:20%;"><%# Eval("Type")%></td>
                                                                        <td style="width:5%;">
                                                                        <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="View Organization">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                        </td>
                                                                        <td style="width:5%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="Delete Organization" OnClientClick="return confirm('Are you sure? you are going to delete this Organization.');">
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
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width:59%;">                                                                        
                                                                        <asp:LinkButton ID="btnOrg" runat="server" CommandName="view" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="View Organization">
                                                                                <%# Eval("Name") %> 
                                                                            </asp:LinkButton>                                                                        
                                                                        <asp:HiddenField ID="hfOrganization" Value='<%# Eval("ORGANIZATION_ID") %>' runat="server" />                                                                        
                                                                        </td>
                                                                        <td style="width:11%;"><%# Eval("CountryName")%></td>
                                                                        <td style="width:20%;"><%# Eval("Type")%></td>
                                                                        <td style="width:5%;">
                                                                         <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="View Organization">
                                                                                <img src="../images/view.gif" width="16" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                        <td style="width:5%;">
                                                                          
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("ORGANIZATION_ID") %>' ToolTip="Delete Organization" OnClientClick="return confirm('Are you sure? you are going to delete this Organization.');">
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
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 10px;
                        height: 50px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update</ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </td>               
            </tr>
        </table>
        
           </div>
    <div >
</div>
</div>
</asp:Content>

