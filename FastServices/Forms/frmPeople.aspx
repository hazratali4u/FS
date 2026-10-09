<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmPeople.aspx.cs" Inherits="Forms_frmPeople" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    function pageLoad() {
        $('#<%=txtDOB.ClientID %>').datepicker(
                {
                    buttonImage: 'images/calendar.png',
                    showOn: 'both',
                    changeMonth: true
                });

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
                $('#<%=lblField.ClientID %>').val('Citizenship ' +  countercitizen);
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
 <div id="right_data">
    <div >
        <table width="100%">
            <tr>               
                <td style="width: 100px">
                    <asp:UpdatePanel id="UpdatePanel1" runat="server">
                        <contenttemplate>
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
                        <asp:Panel ID="pnlAddPeople" runat="server" Visible="false" Width="100%">                        
                            <table width="100%">
                            <tbody>
                                <tr>
                                    <td style="width:100%;"> 
                                        <asp:Literal ID="ltrlPeople" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hfPeopleID" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:100%;"> 
                                        <asp:Panel ID="pnlContact" GroupingText="Contact Information" runat="server" Width="80%">
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
                                                            <strong> <asp:Label ID="lblOrganizationType" runat="server" Text="Organization Type"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:DropDownList id="ddlOrganizationType" runat="server" Width="180" 
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="ddlOrganizationType_SelectedIndexChanged">
                                                                <asp:ListItem Text="Customer" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Fast Services" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblOrganizationType2" Visible="false" runat="server"></asp:Label>
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
                                                            <strong> <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label></strong>
                                                            
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:23%">
                                                            <asp:DropDownList id="ddlOrganization" runat="server" Width="180">
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblOrganization2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width:5%">&nbsp;</td>
                                                         <td style="width:20%">
                                                            <strong> <asp:Label ID="lblLocation" runat="server" Text="Location" Visible="false"></asp:Label></strong>
                                                        </td>
                                                        <td style="width:1%">&nbsp;</td>
                                                        <td style="width:30%">
                                                            <asp:DropDownList id="ddlLocation" runat="server" Width="180" Visible="false"></asp:DropDownList>
                                                            <asp:Label ID="lblLocation2" Visible="false" runat="server"></asp:Label>
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
                                                            <asp:TextBox ID="txtAccountName" runat="server" Width="180"></asp:TextBox>
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
                                                           <asp:TextBox ID="txtDesignation" runat="server" Width="180px" Text="IT Manager">
                                                           </asp:TextBox>
                                                            <asp:Label ID="lblDesignation2" Visible="false" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:100%;height:10px" colspan="7">
                                                            <asp:GridView ID="gvField" runat="server" AutoGenerateColumns="False" Width="100%" 
                                                            GridLines="None" ShowHeader="false" onrowcommand="gvField_RowCommand" 
                                                            onrowediting="gvField_RowEditing" onrowdatabound="gvField_RowDataBound" >
                                                                <Columns>
                                                                    <asp:TemplateField >
                                                                        <HeaderStyle HorizontalAlign="Left"/>
                                                                        <ItemTemplate>
                                                                            <table width="100%">
                                                                                 <tr>
                                                                                        <td style="width:20%">
                                                                                             <strong> 
                                                                                                    <asp:TextBox ID="lblFld" runat="server" Text='<%# Eval("FIELD_TYPE")%>' BorderStyle="None" Font-Bold="true"></asp:TextBox>
                                                                                             </strong>
                                                                                        </td>
                                                                                        <td style="width:80%">
                                                                                         <asp:TextBox ID="txtVl" runat="server" Text='<%# Eval("FIELD_VALUE")%>' Width="180px"></asp:TextBox>
                                                                                         <asp:Label ID="lblFld2" runat="server" Visible="false" Width="180px"></asp:Label>
                                                                                         <asp:ImageButton AlternateText="Delete" ToolTip="Delete Field" ID="btnDelete" runat="server"
                                                                                            ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this Field?');"
                                                                                            CommandName="del" CommandArgument='<%# Eval("PEOPLE_DETAIL_ID")%>' />  
                                                                                        </td>
                                                                                    </tr>
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
                                                                                <a href='javascript:void()' style="text-decoration:none;" onclick="AddAddress();">Address</a>                                                                        
                                                                                <a href='javascript:void()' style="text-decoration:none;" onclick="AddPhone();">Phone</a>
                                                                                <a href='javascript:void()' style="text-decoration:none;" onclick="AddCell();">Cell</a>
                                                                                <a href='javascript:void()' style="text-decoration:none;" onclick="AddFax();">Fax</a>
                                                                                <a href='javascript:void()' style="text-decoration:none;" onclick="AddEmail();">Email</a>

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
                                                                                <span style="color:#006699;">
                                                                                   ....................................
                                                                                   <br />
                                                                                    <a href='javascript:void()' style="text-decoration:none;" onclick="AddCustom();">Custom</a>
                                                                                   </span>                                                                        
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
                    <asp:Panel ID="pnlPeopleList" runat="server" Width="90%">
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Literal ID="ltrlView" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> <asp:LinkButton ID="btnNewPeople" runat="server" Text="Add New People" 
                                                onclick="btnNewPeople_Click"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <td class="tblhead" style="width:25%;">
                                                        Type &nbsp;&nbsp;
                                                         <asp:DropDownList ID="ddlFilterType" runat="server" AutoPostBack="True" 
                                                            onselectedindexchanged="ddlFilterType_SelectedIndexChanged">
                                                            <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="Customer" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Supplier" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Fast Services" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>    
                                                    </td>
                                                    <td class="tblhead" style="width:25%;">
                                                        Filter For &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlFilterFor" runat="server" >
                                                        <asp:ListItem Text="All Records" Value="-1"></asp:ListItem>
                                                            <asp:ListItem Text="First Name" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Last Name" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Designation" Value="2"></asp:ListItem>                                                        
                                                            <asp:ListItem Text="Department" Value="3"></asp:ListItem>                                                        
                                                            <asp:ListItem Text="Organization" Value="4"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="tblhead" style="width:35%;">
                                                        Filter Text &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtSearch" runat="server" Width="200px"></asp:TextBox>
                                                                                                   
                                                    </td>
                                                    <td class="tblhead" colspan="3" style="width:15%;">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Filter" 
                                                            onclick="btnSearch_Click"/>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br/>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" > 
                                                <tr>
                                                    <td class="tblhead" style="width:18%;">
                                                        <asp:LinkButton ForeColor="White" ID="btnFNameSor" runat="server" 
                                                            Text="First Name" onclick="btnFNameSor_Click">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="tblhead" style="width:18%;">
                                                        <asp:LinkButton ForeColor="White" ID="btnLNameSor" runat="server" 
                                                            Text="Last Name" onclick="btnLNameSor_Click">
                                                        </asp:LinkButton>
                                                    </td>
                                                     <td class="tblhead" style="width:18%;">
                                                        <asp:LinkButton ForeColor="White" ID="btnDesignationSor" runat="server" 
                                                            Text="Designation" onclick="btnDesignationSor_Click">
                                                        </asp:LinkButton>
                                                    </td>
                                                    <td class="tblhead" style="width:15%;">
                                                         <asp:LinkButton ForeColor="White" ID="btnDepartmentSor" runat="server" 
                                                            Text="Department" onclick="btnDepartmentSor_Click">
                                                        </asp:LinkButton>
                                                    </td>

                                                    <td class="tblhead" style="width:15%;">
                                                               
                                                       <asp:LinkButton ForeColor="White" ID="btnOrganizationSor" runat="server" 
                                                            Text="Organization" onclick="btnOrganizationSor_Click">
                                                        </asp:LinkButton>                                                                                           
                                                    </td>
                                                    <td class="tblhead" style="width:10%;">
                                                               Status
                                                    </td>
                                                    <td class="tblhead" style="width:3%;"></td>
                                                    <td class="tblhead" style="width:3%;"></td>
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
                                                                        <td style="width:18%;">
                                                                         <asp:LinkButton ID="btnFName" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <%# Eval("FNAME") %> 
                                                                            </asp:LinkButton>             
                                                                            <asp:HiddenField ID="hfFName" runat="server" Value='<%# Eval("FNAME") %>' />                                                        
                                                                        </td>
                                                                        <td style="width:18%;">                                                                        
                                                                        <asp:LinkButton ID="btnLName" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <%# Eval("LNAME") %> 
                                                                            </asp:LinkButton>        
                                                                            <asp:HiddenField ID="hfLName" runat="server" Value='<%# Eval("LNAME") %>' />                                                                
                                                                        </td>                                                                        
                                                                        <td style="width:18%;"><%# Eval("SLASH_DESC")%></td>
                                                                        <td style="width:15%;"><%# Eval("DEPARTMENT")%></td>                                                                        
                                                                        <td style="width:15%;"><%# Eval("NAME")%></td>
                                                                        <td style="width:10%;"><%# Eval("Status")%></td>
                                                                        <td  style="width:3%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <img src="../images/view.gif" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                        <td  style="width:3%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Delete People" OnClientClick="return confirm('Are you sure? you are going to delete this People.');">
                                                                                <img src="../images/delete.gif"  height="16">
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
                                                                        <td style="width:18%;">
                                                                         <asp:LinkButton ID="btnFName" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <%# Eval("FNAME") %> 
                                                                            </asp:LinkButton>
                                                                            <asp:HiddenField ID="hfFName" runat="server" Value='<%# Eval("FNAME") %>' />                                                                                                                                
                                                                        </td>
                                                                        <td style="width:18%;">                                                                        
                                                                        <asp:LinkButton ID="btnLName" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <%# Eval("LNAME") %> 
                                                                                <asp:HiddenField ID="hfLName" runat="server" Value='<%# Eval("LNAME") %>' />                                                                
                                                                            </asp:LinkButton>                                                                        
                                                                        </td>                                                                        
                                                                        <td style="width:18%;"><%# Eval("SLASH_DESC")%></td>
                                                                        <td style="width:15%;"><%# Eval("DEPARTMENT")%></td>                                                                        
                                                                        <td style="width:15%;"><%# Eval("NAME")%></td>
                                                                        <td style="width:10%;"><%# Eval("Status")%></td>
                                                                        <td  style="width:3%;">
                                                                            <asp:LinkButton ID="btnView" runat="server" CommandName="view" CommandArgument='<%# Eval("People_ID") %>' ToolTip="View People">
                                                                                <img src="../images/view.gif" height="16">
                                                                            </asp:LinkButton>
                                                                         </td>
                                                                        <td  style="width:3%;">
                                                                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="del" CommandArgument='<%# Eval("People_ID") %>' ToolTip="Delete People" OnClientClick="return confirm('Are you sure? you are going to delete this People.');">
                                                                                <img src="../images/delete.gif"  height="16">
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
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 300px;
                        height: 100px">
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

