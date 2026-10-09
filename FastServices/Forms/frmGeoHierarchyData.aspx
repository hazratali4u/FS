<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmGeoHierarchyData.aspx.cs" Inherits="Forms_frmGeoHierarchyData" Title="Fast Services: Geo Hierarchy" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" Runat="Server">
    <script language="JavaScript" type="text/javascript">
        function ValidatRegion()
		{
			var str;
			
			str  = document.getElementById('<%=txtRegionName.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Region Name');
				return false;
			}
			return true;
	    }
	    
	    function ValidatZone()
		{
			var str;
					
			str  = document.getElementById('<%=txtZoneName.ClientID%>').value; 
			if(str == null || str.length == 0)
			{
				alert('Must enter Zone Name');
				return false;
			}
			return true;
	    }
	    function ValidateTerritory()
		{
			var str;

			return true;
	    }
	    function ValidateTown()
		{
			var str;
	    }
    </script>
    
     <div id="right_data">
              <table>
             <tr>
                 <td>
         <table>
             <tr>
                 <td>
                     <cc1:TabContainer ID="TabContainer1" runat="server"
                         Width="650px" ActiveTabIndex="0">
                         <cc1:TabPanel ID="TabPanel1" runat="server">
                                <HeaderTemplate>
                                    Country&nbsp;
                             </HeaderTemplate>
                             <ContentTemplate>
                                 <table width="100%">
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td style="width: 100px">
                                         </td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td style="width: 100px">
                                             <asp:UpdatePanel id="UpdatePanel6" runat="server">
                                                 <contenttemplate>
<table width="100%">
                                                         <TBODY>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <td colspan="4" style="width:100%">
                                                                     <asp:Literal ID="ltrlCountry" runat="server"></asp:Literal>
                                                                 </TD>                                                                 
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 28px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px; HEIGHT: 28px">
                                                                     <strong>
                                                                     <asp:Label ID="Label2" runat="server" Text="Name" Width="65px" Height="14px"></asp:Label>
                                                                     </strong>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 28px">
                                                                     <asp:TextBox ID="txtRegionName" runat="server" CssClass="txtBox " Width="192px" Height="17px"></asp:TextBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 28px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 29px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px; HEIGHT: 29px">
                                                                     &nbsp;</TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 29px">
                                                                     &nbsp;<asp:CheckBox id="ChIsActive" runat="server" Text="Is Active" Checked="True">
                                                                     </asp:CheckBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 29px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px; HEIGHT: 37px" align=right>
                                                                     <asp:Label ID="Label3" runat="server" Text="Code" Width="65px" Visible="False"></asp:Label>
                                                                     &nbsp;</TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                     <asp:Button id="btnRegionSave" onclick="btnRegionSave_Click" runat="server" Width="85px" Font-Size="8pt" Text="Save" CssClass="Button" />
                                                                     <asp:TextBox ID="txtRegionCode" runat="server" CssClass="txtBox " Width="77px" Visible="False">N/A</asp:TextBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                             </TR>
                                                         </TBODY>
                                                     </TABLE>
</contenttemplate>
                                             </asp:UpdatePanel></td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="left" colspan="3">
                                             <asp:UpdatePanel id="UpdatePanel5" runat="server">
                                                 <contenttemplate>
<asp:GridView id="grdRegionData" runat="server" Width="90%" Font-Size="9pt"  AutoGenerateColumns="False" HorizontalAlign="Center" 
                                                         OnRowEditing="grdRegionData_RowEditing" 
                                                         onrowdatabound="grdRegionData_RowDataBound">
<Columns>
    <asp:BoundField DataField="GEO_ID" HeaderText="GEO_ID">
        <HeaderStyle CssClass="HidePanel" />
        <ItemStyle CssClass="HidePanel" />
    </asp:BoundField>
    <asp:BoundField DataField="GEO_CODE" HeaderText="Region Code">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
</asp:BoundField>
    <asp:BoundField DataField="GEO_NAME" HeaderText="Country Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
    <asp:BoundField DataField="ISDELETED" HeaderText="Status">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
    <asp:CommandField ShowEditButton="True" HeaderText="Edit">
    </asp:CommandField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
    
</asp:GridView>
</contenttemplate>
                                             </asp:UpdatePanel>                                             
                                         </td>
                                     </tr>
                                 </table>
                             </ContentTemplate>
                         </cc1:TabPanel>
                         <cc1:TabPanel ID="TabPanel2" runat="server">
                             <HeaderTemplate>
                                 City
                             </HeaderTemplate>
                             <ContentTemplate>
                                 <table width="100%">
                                     <tr>
                                         <td style="width: 100px">
                                         </td>
                                         <td align="center" style="width: 100px">
                                             <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                 <ContentTemplate>
<TABLE width="100%">
                                                         <TBODY>
                                                             <TR>
                                                                 <td colspan="4">
                                                                     <asp:Literal ID="ltrlCity" runat="server"></asp:Literal>
                                                                 </TD>
                                                                 
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 16px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px; HEIGHT: 28px" align=left>
                                                                     <strong>
                                                                     <asp:Label id="Label1" runat="server" Width="47px" Text="Country"></asp:Label>
                                                                     </strong>
                                                                 </TD>
                                                                 <TD style="HEIGHT: 16px" align=left>
                                                                     <asp:DropDownList id="DrpRegion" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="DrpRegion_SelectedIndexChanged" CssClass="DropList">
                                                                     </asp:DropDownList>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 16px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 8px">
                                                                 </TD>
                                                                 <TD style="WIDTH: 49px; HEIGHT: 28px" align=left>
                                                                     <strong>
                                                                     <asp:Label id="Label21" runat="server" Width="43px" Text="Name"></asp:Label>
                                                                     </strong>
                                                                 </TD>
                                                                 <TD align=left>
                                                                     <asp:TextBox id="txtZoneName" runat="server" Width="194px" CssClass="txtBox "></asp:TextBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 8px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                                 <TD align=right>
                                                                 </TD>
                                                                 <TD align=left>
                                                                     <asp:CheckBox id="chbIsZoneActive" runat="server" Text="Is Active" Checked="True">
                                                                     </asp:CheckBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                             </TR>
                                                             <TR>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                                 <TD align=right>
                                                                     <asp:Label ID="Label4" runat="server" Text="Code" Width="43px" Visible="False"></asp:Label>
                                                                     &nbsp;</TD>
                                                                 <TD align=left>
                                                                     <asp:Button id="btnSaveZone" onclick="btnSaveZone_Click" runat="server" Width="85px" Font-Size="8pt" Text="Save" CssClass="Button" />
                                                                     <asp:TextBox ID="txtZoneCode" runat="server" CssClass="txtBox " Width="100px" Visible="False">N/A</asp:TextBox>
                                                                 </TD>
                                                                 <TD style="WIDTH: 100px; HEIGHT: 37px">
                                                                 </TD>
                                                             </TR>
                                                         </TBODY>
                                                     </TABLE>
</ContentTemplate>
                                             </asp:UpdatePanel>
                                         </td>
                                         <td style="width: 100px">
                                         </td>
                                     </tr>
                                     <tr>
                                         <td align="center" colspan="3">
                                             
                                             <asp:UpdatePanel id="UpdatePanel1" runat="server">
                                                 <contenttemplate>
                                                     <asp:Panel ID="pnlCity" runat="server" Width="550px" Height="300px" ScrollBars="Vertical">
<asp:GridView id="grdZoneData" runat="server" Width="100%" Font-Size="9pt" ForeColor="SteelBlue" BorderColor="White" BackColor="White" HorizontalAlign="Center" 
                                                         AutoGenerateColumns="False" OnRowEditing="grdZoneData_RowEditing" 
                                                         onrowdatabound="grdZoneData_RowDataBound">
<Columns>
<asp:BoundField DataField="PARENT_GEO_ID" HeaderText="PARENT_GEO_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GEO_ID" HeaderText="GEO_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="GEO_CODE" HeaderText="Zone Code">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" CssClass="HidePanel"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" CssClass="HidePanel" />
</asp:BoundField>
<asp:BoundField DataField="GEO_NAME" HeaderText="City Name">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:BoundField DataField="ISDELETED" HeaderText="Status">
<ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
    <HeaderStyle HorizontalAlign="Left" />
</asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" HorizontalAlign="Center" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
</asp:CommandField>
</Columns>

<FooterStyle BackColor="White"></FooterStyle>

<PagerStyle BackColor="Transparent"></PagerStyle>

<HeaderStyle CssClass="tblhead"></HeaderStyle>

<AlternatingRowStyle CssClass="GridAlternateRowStyle"></AlternatingRowStyle>
</asp:GridView>
                                                         </asp:Panel>
</contenttemplate>
                                             </asp:UpdatePanel>
                                                 
                                                 </td>
                                     </tr>
                                 </table>
                             </ContentTemplate>
                         </cc1:TabPanel>
                     </cc1:TabContainer>
            </td>
             </tr>
         </table>
                 </td>
                 <td>
                 </td>
             </tr>
         </table>
     </div>
</asp:Content>
