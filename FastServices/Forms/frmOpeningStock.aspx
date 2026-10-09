<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOpeningStock.aspx.cs" Inherits="Forms_frmOpeningStock" Title="Fast Services: Stock Adjustment" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
 function ConfirmDelete()
 {
	    if( confirm("Do you want to Cancel this record?")==true)
		return true;
		
	    else
	    {
	    return false;
	    }
 }
    
 function ValidateForm()
 {
		var str;
    
		str  = document.getElementById('<%=txtskuName.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Select SKU Name');
			return false;
		}
		str  = document.getElementById('<%=txtQuantity.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Enter Quantity');
			return false;
		}
		
        return true;
 }
 function SearchList()
 {
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuCode.ClientID %>');

            if(tb.value == "")
            {
                ClearSelection(l);
            }
            else
            {
                for (var i=0; i < l.options.length; i++)
                {
                    if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase()))
                    {
                        l.options[i].selected = true;
                        return false;
                    }
                    else
                    {
                        ClearSelection(l);
                    }
                }
           }
 }
 function SearchedCode()
 {
    var l =  document.getElementById('<%= lstCode.ClientID %>');
    var str;
    for (var i=0; i < l.options.length; i++)
    {
        if(l.options[i].selected)
        {
           str = l.options[i].value;
           ClearSelection(l);
           break;
        }
        else
        {
            str = "";
        }
    }
    var stroption  = document.getElementById("<%= txtskuCode.ClientID %>").value;
    if(str.length > 0)
    {
        document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0,str.indexOf('-'));
        document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1);
        document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
    }
    else if(stroption.length == 0)
    {
       document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
       document.getElementById("<%= lstCode.ClientID %>").focus();
       
    }
    
 }
 function SelectCode(e)
 {
      if(e.keyCode == 13)
      {
        var str  = document.getElementById("<%= lstCode.ClientID %>").value;
        document.getElementById("<%= txtskuCode.ClientID %>").value = str.substring(0,str.indexOf('-'));
        document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1);
        document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
        document.getElementById("<%= txtQuantity.ClientID %>").focus();
        
      }
 }
 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
  <div id="right_data">
    <div >
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<TABLE>
<TBODY><TR><TD align=left colSpan=2>
<asp:Label id="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label> </TD>
<TD align=left colSpan=1>&nbsp; &nbsp; &nbsp; &nbsp; </TD>
<TD style="WIDTH: 316px" vAlign=middle align=center colSpan=1 rowSpan=7>
<strong><asp:Label id="Label4" runat="server" Width="239px" Height="8px" CssClass="lblbox"></asp:Label></strong> &nbsp; &nbsp;&nbsp; 
<asp:Panel id="Panel1" runat="server" Width="250px" Height="170px" CssClass="HidePanel" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver" BackColor="Silver">
            <strong><asp:Label ID="Label3" runat="server" CssClass="lblbox" Width="170px">Select from SKU List</asp:Label></strong><br />
        <asp:ListBox ID="lstCode" runat="server" Height="154px" onkeyup="SelectCode(event)"
                SelectionMode="Multiple" Width="245px"></asp:ListBox></asp:Panel> </TD></TR><TR><TD style="HEIGHT: 25px" align=left>
                <strong><asp:Label id="Label2" runat="server" Width="98px" Height="14px" Text="Transaction Type" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:DropDownList id="DrpDocumentType" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="DrpDocumentType_SelectedIndexChanged"><asp:ListItem Value="7">Opening Stock</asp:ListItem>
    <asp:ListItem Value="8">Short</asp:ListItem>
    <asp:ListItem Value="9">Execes</asp:ListItem>
</asp:DropDownList></TD><TD style="HEIGHT: 25px">
<strong><asp:Label id="Label5" runat="server" Width="50px" CssClass="lblbox"></asp:Label></strong></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lblDocumentNo" runat="server" Width="94px" Text="Document No" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:DropDownList id="drpDocumentNo" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged"></asp:DropDownList></TD><TD style="HEIGHT: 25px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lbltoLocation" runat="server" Width="94px" Text="Principal" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:DropDownList id="drpPrincipal" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpPrincipal_SelectedIndexChanged"></asp:DropDownList></TD><TD style="HEIGHT: 25px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="lblfromLocation" runat="server" Width="94px" Text="Location" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:DropDownList id="drpDistributor" runat="server" Width="200px" CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDistributor_SelectedIndexChanged"></asp:DropDownList></TD><TD style="HEIGHT: 25px"></TD></TR><TR><TD style="HEIGHT: 25px" align=left>
<strong><asp:Label id="Label1" runat="server" Width="94px" Text="Remarks" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 25px"><asp:TextBox id="txtDocumentNo" runat="server" Width="195px" CssClass="txtBox"></asp:TextBox></TD><TD style="HEIGHT: 25px"></TD></TR><TR><TD style="HEIGHT: 25px" vAlign=top align=left><asp:CheckBox id="ChbBatchNo" runat="server" Width="77px" Text="Batch No" AutoPostBack="True"></asp:CheckBox></TD><TD style="WIDTH: 1px; HEIGHT: 25px" vAlign=top><asp:CheckBox id="ChbFreeSKU" runat="server" Width="146px" Text="Apply Free SKU" AutoPostBack="True"></asp:CheckBox></TD><TD style="WIDTH: 1px; HEIGHT: 25px" vAlign=top></TD></TR></TBODY>
</TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
           </div>
    <div >
    <table width="100%">
        <tr>
            <td >
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
<TABLE><TBODY><TR><TD>
<asp:Label id="lblskuCode" runat="server" Width="87px" Height="16px" ForeColor="White" Font-Bold="True" Text="  SKU Code"  BackColor="#006699"></asp:Label></TD><TD style="WIDTH: 100px"><asp:Label id="lblskuname" runat="server" Width="206px" Height="16px" ForeColor="White" Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD><asp:Label id="lblquantity" runat="server" Width="76px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></TD><TD><asp:Label id="lblFreeSKU" runat="server" Width="77px" Height="16px" ForeColor="White" Font-Bold="True" Text="Free SKU" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label></TD><TD><asp:Label id="lblBatchNo" runat="server" Width="82px" Height="16px" ForeColor="White" Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699" Enabled="False"></asp:Label></TD><TD style="WIDTH: 100px"><asp:Label id="Label41" runat="server" Width="92px" Height="16px" ForeColor="White" Font-Bold="True" Text="Add SKU" CssClass="lblbox" BackColor="#006699"></asp:Label></TD></TR><TR><TD><asp:TextBox id="txtskuCode" onkeyup="SearchList()" runat="server" Width="82px" CssClass="txtBox"></asp:TextBox></TD><TD style="WIDTH: 100px"><asp:TextBox id="txtskuName" runat="server" Width="200px" Font-Bold="True" CssClass="txtBox" Enabled="False"></asp:TextBox></TD><TD><asp:TextBox id="txtQuantity" onfocus="SearchedCode()" runat="server" Width="70px" CssClass="txtBox "></asp:TextBox></TD><TD><asp:TextBox id="txtFreeSKU" runat="server" Width="70px" CssClass="txtBox" Enabled="False">0</asp:TextBox></TD><TD><asp:TextBox id="txtBatchNo" runat="server" Width="76px" CssClass="txtBox" Enabled="False">N/A</asp:TextBox></TD><TD style="WIDTH: 100px">
<asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Width="95px" Font-Size="8pt" Text="Add Sku" ValidationGroup="vg" CssClass="Button" /> </TD></TR><TR><TD align=left colSpan=6><asp:Panel id="Panel2" runat="server" Width="640px" Height="140px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver"><asp:GridView id="GrdPurchase" runat="server" Width="620px" ForeColor="SteelBlue" CssClass="gridRow2" BorderColor="White" BackColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" HorizontalAlign="Center" AutoGenerateColumns="False">
                                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                PreviousPageText="Previous" />
                                            <RowStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                    <HeaderStyle CssClass="HidePanel" />
                                                    <ItemStyle CssClass="HidePanel" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                        Width="85px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Left"
                                                        Width="205px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                        Width="75px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="FREE_SKU" HeaderText="Free SKU">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                        Width="75px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="BATCH_NO" HeaderText="BatchNo">
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px" />
                                                </asp:CommandField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;"
                                                            Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" Width="45px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="White" />
                                            <PagerStyle BackColor="Transparent" />
                                            <HeaderStyle BackColor="#007395" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" CssClass="GridAlternateRowStyle" ForeColor="#333333" />
                                        </asp:GridView> </asp:Panel> </TD></TR></TBODY></TABLE>
                                        <asp:Button accessKey="S" id="btnSaveDocument" onclick="btnSaveDocument_Click" runat="server" Width="119px" Font-Size="8pt" Text="Save Document" UseSubmitBehavior="False" CssClass="Button" /> 
                                        <asp:Button accessKey="C" id="btnCancel" onclick="btnCancel_Click" runat="server" Width="120px" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" CssClass="Button" /> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
        &nbsp;
    </div>
    </div>
   
</asp:Content>

