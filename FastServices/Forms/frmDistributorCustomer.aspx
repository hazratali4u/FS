<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDistributorCustomer.aspx.cs" Inherits="Forms_frmDistributorCustomer" Title="Fast Services: New Customer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">

       <script language="JavaScript" type="text/javascript">
        
    function NameValidation(event)
    {
        // Allow: backspace, delete, tab and escape
        if ( event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 || 
             // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) || 
             // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||            
             // Allow: Dash, Underscoor
            (event.keyCode == 189) ||            
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                 // let it happen, don't do anything
                 return;
        }
        else {
            // Ensure that it is a number and stop the keypress
                event.preventDefault(); 
        }
    }
    
    function AddressValidation(event)
    {
        // Allow: backspace, delete, tab , escape and space bar
        if ( event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 32 || 
             // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) || 
             // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash, Underscoor
            (event.keyCode == 189) || 
            // Allow: Open bracket, Close bracket
            ((event.keyCode == 57 || event.keyCode == 48) && event.shiftKey === true) ||            
            //Allow Comma,Period
            ((event.keyCode == 190 || event.keyCode == 188) && event.shiftKey === false) ||
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            (event.keyCode >= 96 && event.keyCode <= 105) || //Keypad numbers
            //Allow a-z
            (event.keyCode >= 65 && event.keyCode <= 90)) {
                 // let it happen, don't do anything
                 return;
        }
        else {
            // Ensure that it is a number and stop the keypress
                event.preventDefault(); 
        }
    }
    
    function PhoneValidation(event)
    {
        // Allow: backspace, delete, tab , escape
        if ( event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 ||
             // Allow: Ctrl+A
            (event.keyCode == 65 && event.ctrlKey === true) || 
             // Allow: home, end, left, right
            (event.keyCode >= 35 && event.keyCode <= 39) ||
            // Allow: Dash
            (event.keyCode == 189 && event.shiftKey === false) || 
            //Allow 0-9
            ((event.keyCode >= 48 && event.keyCode <= 57) && event.shiftKey === false) || //Standard Numbers
            //Keypad numbers
            (event.keyCode >= 96 && event.keyCode <= 105))
            {
                 // let it happen, don't do anything
                 return;
        }
        else {
            // Ensure that it is a number and stop the keypress
                event.preventDefault(); 
        }
    }
    
        function pageLoad() {
            $('#<%=txtCNIC.ClientID %>').mask("99999-9999999-9");
            $('#<%=txtNTN.ClientID %>').mask("9999999-9");
            $('#<%=txtIsRegister.ClientID %>').mask("99-99-9999-999-99");
                       
            $('#<%=Grid_users.ClientID %>').tablesorter(
	     {
        headers: {
            25: {
                sorter: false
            }
        }
    }
	     );  
	     
        }
       Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = true;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = true;

        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnSearch.ClientID%>').disabled = false;
            document.getElementById('<%=btnCancel.ClientID%>').disabled = false;
        }
 
 
        function ValidateForm()
		{
			var str;
			str  = document.getElementById('<%=txtCustomerName.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Customer Name');
				return false;
			}
			str  = document.getElementById('<%=txtContactPerson.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Contact Person Name');
				return false;
			}
			str  = document.getElementById('<%=txtAddress.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Address');
				return false;
			}
					
			str  = document.getElementById('<%=txtPhoneNo.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Phone No');
				return false;
			}
			str  = document.getElementById('<%=txtRegdate.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Register Date');
				return false;
			}
			
			str  = document.getElementById('<%=txtCNIC.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter CNIC #');
				return false;
			}
			
			str  = document.getElementById('<%=txtNTN.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter NTN #');
				return false;
			}
			
			if($('#<%= ChbIsRegister.ClientID %>').is(':checked'))
			{
			    str  = document.getElementById('<%=txtIsRegister.ClientID%>').value; 
			    if(str == null || str.length == 0)
			    {
				    alert('Must enter STRN #');
				    return false;
			    }			
			}
			
			return true;		
		}
	function SearchRecord()
	{
		var str;
		str  = document.getElementById('<%=txtSeach.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must enter Key Word for Searching');
			return false;
		}
		return true;
	}
    
    </script>
    <div id="right_data">
    <div >
        <table width="100%">
            <tr>               
                <td style="width: 100px">
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<TABLE><TBODY><TR><TD style="WIDTH: 143px" align=left></TD><TD style="WIDTH: 175px"><asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label></TD><TD style="WIDTH: 1px"></TD><TD align=left></TD><TD style="WIDTH: 219px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong> <asp:Label id="Label7" runat="server" Width="77px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px"><asp:DropDownList id="DrpDistributor" runat="server" Width="205px" CssClass="DropList" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></TD><TD style="WIDTH: 1px"></TD><TD align=left>
<strong><asp:Label id="Label4" runat="server" Width="56px" Text="Town" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px"><asp:DropDownList id="DrpTown" runat="server" Width="205px" CssClass="DropList" OnSelectedIndexChanged="DrpTown_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="Label2" runat="server" Width="78px" Text="Route" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 12px"><asp:DropDownList id="DrpRoute" runat="server" Width="205px" CssClass="DropList" OnSelectedIndexChanged="DrpRoute_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></TD><TD style="WIDTH: 1px; HEIGHT: 12px"></TD><TD style="HEIGHT: 12px" align=left>
<strong><asp:Label id="Label5" runat="server" Width="59px" Text="Market" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px; HEIGHT: 12px"><asp:DropDownList id="DrpMarket" runat="server" Width="206px" CssClass="DropList"></asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lblNickName" runat="server" Width="79px" Text="Channel Type" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px"><asp:DropDownList id="drpChannelType" runat="server" Width="205px" CssClass="DropList"></asp:DropDownList></TD><TD style="WIDTH: 1px"></TD><TD align=left>
<strong><asp:Label id="lblMobileNo" runat="server" Width="96px" Text="Business Type" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px"><asp:DropDownList id="DrpBusinessType" runat="server" Width="206px" CssClass="DropList"></asp:DropDownList></TD></TR><TR><TD style="HEIGHT: 26px" align=left>
<strong><asp:Label id="Label3" runat="server" Width="101px" Text="Promotion Class" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 26px"><asp:DropDownList id="DrpVolumeClass" runat="server" Width="205px" CssClass="DropList"></asp:DropDownList></TD><TD style="WIDTH: 1px; HEIGHT: 26px" vAlign=top>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD><TD align=left>
<strong><asp:Label id="Label6" runat="server" Width="106px" Text="Contact Person" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px; HEIGHT: 26px"><asp:TextBox id="txtContactPerson" runat="server" Width="180px" CssClass="txtBox" MaxLength="50"></asp:TextBox> 

</TD></TR><TR><TD style="HEIGHT: 26px" align=left>
<strong><asp:Label id="Label1" runat="server" Width="51px" Text="Name" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 26px"><asp:TextBox id="txtCustomerName" runat="server" Width="180px" CssClass="txtBox" MaxLength="100"></asp:TextBox> 
</TD><TD style="WIDTH: 1px; HEIGHT: 26px" vAlign=top></TD><TD align=left>
<strong><asp:Label id="lblAddress1" runat="server" Width="44px" Text="Address " CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px; HEIGHT: 26px"><asp:TextBox id="txtAddress" runat="server" Width="180px" CssClass="txtBox " MaxLength="250"></asp:TextBox> </TD></TR><TR><TD style="HEIGHT: 26px" align=left>
<strong><asp:Label id="lblPhNo" runat="server" Width="100px" Text="Phone No" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 26px"><asp:TextBox id="txtPhoneNo" runat="server" Width="180px" CssClass="txtBox" MaxLength="15"></asp:TextBox> </TD><TD style="WIDTH: 1px; HEIGHT: 26px" vAlign=top></TD><TD align=left>
<strong><asp:Label id="lbldesignationID" runat="server" Width="80px" Text="Register Date" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px; HEIGHT: 26px"><asp:TextBox id="txtRegdate" runat="server" Width="200px" CssClass="txtBox" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 26px" align=left>
<strong><asp:Label id="Label8" runat="server" Width="100px" Text="CNIC #" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 175px; HEIGHT: 26px"><asp:TextBox id="txtCNIC" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox></TD><TD style="WIDTH: 1px; HEIGHT: 26px" vAlign=top></TD><TD align=left>&nbsp;
<strong><asp:Label id="lblNTN" runat="server" Text="NTN #" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 219px; HEIGHT: 26px"><asp:TextBox id="txtNTN" runat="server" Width="200px" CssClass="txtBox"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 26px" align=left><asp:CheckBox id="ChbIsRegister" runat="server" Width="96px" Text="Is Register" AutoPostBack="True" OnCheckedChanged="ChbIsRegister_CheckedChanged"></asp:CheckBox></TD><TD style="WIDTH: 175px; HEIGHT: 26px"><asp:TextBox id="txtIsRegister" runat="server" Width="200px" CssClass="txtBox " Enabled="False"></asp:TextBox></TD><TD style="WIDTH: 1px; HEIGHT: 26px" vAlign=top></TD><TD align=left>&nbsp;</TD><TD style="WIDTH: 219px; HEIGHT: 26px"></TD></TR><TR><TD align=left colSpan=5 rowSpan=3></TD></TR><TR></TR><TR></TR><TR><TD style="WIDTH: 143px" align=left></TD><TD style="WIDTH: 175px">
<asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Width="80px" Font-Size="8pt" Text="Save" ValidationGroup="vg" CssClass="Button" /> &nbsp; 
<asp:Button id="btnCancel" onclick="btnCancel_Click" runat="server" Width="80px" Font-Size="8pt" Text="Cancel" CssClass="Button" /> </TD><TD style="WIDTH: 1px"></TD><TD><asp:CheckBox id="chkIsActive" runat="server" Width="93px" Text="IsActive" Checked="True"></asp:CheckBox></TD><TD style="WIDTH: 219px"></TD></TR></TBODY></TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 244px;
                        height: 100px">
                        &nbsp;<asp:Panel ID="Panel21" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </td>               
            </tr>
        </table>
        
           </div>
    <div >
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
<TABLE style="BORDER-RIGHT: silver thin inset; BORDER-TOP: silver thin inset; BORDER-LEFT: silver thin inset; WIDTH: 100%; BORDER-BOTTOM: silver thin inset; BACKGROUND-COLOR: silver"><TBODY><TR><TD style="HEIGHT: 21px" align=left>
<asp:Label id="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label> </TD><TD style="WIDTH: 170px; HEIGHT: 21px" align=left><asp:DropDownList id="ddSearchType" runat="server" Width="200px" CssClass="DropList"><asp:ListItem Value="CUSTOMER_CODE">Customer Code</asp:ListItem>
<asp:ListItem Value="CUSTOMER_NAME">Customer Name</asp:ListItem>
<asp:ListItem Value="CONTACT_PERSON">Contact Person</asp:ListItem>
<asp:ListItem Value="CONTACT_NUMBER">Contact Number</asp:ListItem>
<asp:ListItem Value="ADDRESS">Address</asp:ListItem>
<asp:ListItem Value="EMAIL_ADDRESS">Email Address</asp:ListItem>
<asp:ListItem Value="GEO_NAME">Town Name</asp:ListItem>
<asp:ListItem Value="AREA_NAME">Route Name</asp:ListItem>
<asp:ListItem Value="ROUTE_NAME">Market Name</asp:ListItem>
<asp:ListItem Value="SLASH_DESC">Channel Type</asp:ListItem>
<asp:ListItem>CNIC</asp:ListItem>
<asp:ListItem>NTN</asp:ListItem>
<asp:ListItem Value="GST_NUMBER">STRN</asp:ListItem>
</asp:DropDownList> </TD><TD style="WIDTH: 224px; HEIGHT: 21px" align=left><asp:TextBox id="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox> </TD><TD style="HEIGHT: 21px" align=left width=250>
<asp:Button id="btnSearch" runat="server" Width="85px" Font-Size="8pt" Text="Filter" OnClick="btnSearch_Click"></asp:Button> </TD></TR></TBODY></TABLE><asp:Panel id="Panel2" runat="server" Width="100%" Height="200px" ScrollBars="Vertical"><asp:GridView id="Grid_users" runat="server" Width="100%" ForeColor="SteelBlue" CssClass="tablesorter" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White" OnRowEditing="Grid_users_RowEditing">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<RowStyle ForeColor="Black"></RowStyle>
<Columns>
<asp:BoundField DataField="CUSTOMER_ID" HeaderText="Customer Id">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="DISTRIBUTOR_ID" HeaderText="DISTRIBUTOR_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="BUSINESS_TYPE_ID" HeaderText="BUSINESS_TYPE_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PROMOTION_CLASS" HeaderText="PROMOTION_CLASS">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CHANNEL_TYPE_ID" HeaderText="CHANNEL_TYPE_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="TOWN_ID" HeaderText="TOWN_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AREA_ID" HeaderText="AREA_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ROUTE_ID" HeaderText="ROUTE_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUSTOMER_CODE" HeaderText="Code">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CUSTOMER_NAME" HeaderText="Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CONTACT_PERSON" HeaderText="Contact Person">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CONTACT_NUMBER" HeaderText="Contact Number">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="EMAIL_ADDRESS" HeaderText="Email">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ADDRESS" HeaderText="ADDRESS">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GST_NUMBER" HeaderText="Gst No">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ChannelType" HeaderText="Channel Type">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GEO_NAME" HeaderText="Town">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="AREA_NAME" HeaderText="Route">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ROUTE_NAME" HeaderText="Market">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="REGDATE">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="IS_STAND" HeaderText="Stand">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="IS_COOLER" HeaderText="Cooler">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="CNIC" HeaderText="CNIC">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="NTN" HeaderText="NTN">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:CommandField>
</Columns>

<FooterStyle BackColor="White"></FooterStyle>

<PagerStyle BackColor="Transparent"></PagerStyle>

<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#007395" Font-Bold="True" ForeColor="White"></HeaderStyle>

<AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333"></AlternatingRowStyle>
</asp:GridView></asp:Panel> 
</contenttemplate>
</asp:UpdatePanel>
</div>
</div>
</asp:Content>