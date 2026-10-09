<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmOrderEntry.aspx.cs" Inherits="Forms_frmOrderEntry" Title="Fast Services: Order/Invoice Step 2" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">
        
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest( startRequest );

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest( endRequest );

        function startRequest( sender, e )
        { 
            
            document.getElementById('<%=btnSave.ClientID%>').disabled = true;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = true;

        }

        function endRequest( sender, e ) 
        { 
           
            document.getElementById('<%=btnSave.ClientID%>').disabled = false;
            document.getElementById('<%=btnCalculate.ClientID%>').disabled = false;
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
 function SearcSKUList(e)
 {
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuCode.ClientID %>');
     if (e.keyCode == 27)
     {
        document.getElementById("<%= numtxtTotalExtraDiscnt.ClientID %>").focus();
     }
     else
     {
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
 }
 function SearchList()
 {
     var l =  document.getElementById('<%= ListCustomer.ClientID %>');
     var tb = document.getElementById('<%= txtOutletCode.ClientID %>');
        
            
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
    var str  
    var stroption
    str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
    stroption = document.getElementById("<%= txtOutletCode.ClientID %>").value;
    
     if(str.length > 0)
     {      
            document.getElementById("<%= txtOutletCode.ClientID %>").value =  'OT' + str.substring(str.indexOf('| ') + 2);
            document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
            document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
    
     }
     else if(stroption.length == 0)
     {
            document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= ListCustomer.ClientID %>").focus();
     }
     ClearSelection(document.getElementById('<%= ListCustomer.ClientID %>'));
    
 }
 function SearchSKUCode()
 {
    var str  = document.getElementById("<%= lstCode.ClientID %>").value;
    var stroption  = document.getElementById("<%= txtskuCode.ClientID %>").value; 
    
    if(str.length > 0)
     {      
            document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
            document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
            document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
            document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
    
     }
     else if(stroption.length == 0)
     {
            document.getElementById("<%= Panel3.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= lstCode.ClientID %>").focus();
     }
     ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));
    
 }
  function SelectSkuCode(e)
 {
      if(e.keyCode == 13)
      {
        var str  = document.getElementById("<%= lstCode.ClientID %>").value;
        document.getElementById("<%= txtskuCode.ClientID %>").value =  str.substring(0,str.indexOf('-'));
        document.getElementById("<%= txtskuName.ClientID %>").value =  str.substring(str.indexOf('-') + 1,str.indexOf(':'));
        document.getElementById("<%= txtUnitRate.ClientID %>").value =  str.substring(str.indexOf(':') + 1);
        document.getElementById("<%= Panel3.ClientID %>").className = "HidePanel";
        
        document.getElementById("<%= txtQuantity.ClientID %>").focus();
      }
 }
 function SelectCode(e)
 {
      if(e.keyCode == 13)
      {
        var str  = document.getElementById("<%= ListCustomer.ClientID %>").value;
        document.getElementById("<%= txtOutletCode.ClientID %>").value = "OT" + str.substring(str.indexOf('|') + 2);
        document.getElementById("<%= txtOutletName.ClientID %>").value =  str.substring(0,str.indexOf('|'));
        document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
        document.getElementById("<%= txtskuCode.ClientID %>").focus();
      }
 }
 function ClearSelection(lb)
 {
    lb.selectedIndex = -1;
 }
 
  </script>
  <div id="right_data">
    <div>
     <span class="heading">Order/Invoice Step 2</span>
     </div>
     <div >
         <table width="100%">
             <tr>
                 
                 <td align="left">
                 <div style="LEFT: 120px; POSITION: absolute; TOP: 190px;">
                 <asp:Panel id="Panel1" runat="server" Width="327px" Height="237px" BorderWidth="1px" BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                 <TABLE style="BORDER-RIGHT: #ffffff thin groove; BORDER-TOP: #ffffff thin groove; BORDER-LEFT: #ffffff thin groove; WIDTH: 99%; BORDER-BOTTOM: #ffffff thin groove">
                    <TBODY>
                        <TR>
                            <TD style="BORDER-BOTTOM: black thin solid" align=left colSpan=2>
                            &nbsp;<strong>Select Customer from List Press Enter</strong>
                            </TD>
                            <TD style="BORDER-BOTTOM: black thin solid" vAlign=top align=right>
                            <asp:Button accessKey="S" id="Button4" runat="server" Width="21px" Height="16px" Font-Size="8pt" Text="X" BorderWidth="1px" BorderStyle="Groove" CssClass="Button" />
                            </TD>
                         </TR>
                    </TBODY>
                 </TABLE>
                 <asp:ListBox id="ListCustomer" onkeyup="SelectCode(event)" runat="server" Width="95%" Height="87%"></asp:ListBox>
                 </asp:Panel>
                 </div>
                 
                 <div style="LEFT: 15px; POSITION: absolute; TOP: 275px; height: 270px;">
                  <asp:Panel id="Panel3" runat="server" Width="327px" Height="237px" BorderWidth="1px" BorderStyle="Inset" BorderColor="White" BackColor="Silver" CssClass="HidePanel">
                    <TABLE style="BORDER-RIGHT: #ffffff thin groove; BORDER-TOP: #ffffff thin groove; BORDER-LEFT: #ffffff thin groove; WIDTH: 99%; BORDER-BOTTOM: #ffffff thin groove">
                        <TBODY>
                            <TR>
                                <TD style="BORDER-BOTTOM: black thin solid" align=left colSpan=2>
                                    &nbsp;<strong>Select SKU from List Press Enter</strong></td>
                                <TD style="BORDER-BOTTOM: black thin solid" vAlign=top align=right>
                                    <asp:Button ID="Button5" runat="server" AccessKey="S" BorderStyle="Groove" BorderWidth="1px"
                                        Font-Size="8pt" Height="16px" Text="X" Width="21px" CssClass="Button" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:ListBox id="lstCode" onkeyup="SelectSkuCode(event)" runat="server" Width="95%" Height="87%" SelectionMode="Multiple"></asp:ListBox>
                  </asp:Panel>
                         &nbsp;
                 </div>

                     <div style="z-index: 101; left: 612px; width: 100px; position: absolute; top: 369px;
                         height: 100px">
                         &nbsp;<asp:Panel ID="Panel21" runat="server">
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                 <ProgressTemplate>
                                     <asp:ImageButton ID="ImageButton1" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                         Width="31px" />
                                     Wait Update
                                 </ProgressTemplate>
                             </asp:UpdateProgress>
                         </asp:Panel>
                     </div>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
<TABLE><TBODY><TR><TD vAlign=top align=left>
<strong> <asp:Label id="lblDocumentNo" runat="server" Width="74px" Text="Order No" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left colSpan=3><asp:DropDownList id="drpDocumentNo" runat="server" Width="231px" CssClass="DropList" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> 
<strong><asp:Label id="lblBillBook" tabIndex=1 runat="server" Text="Bill Book No:" CssClass="lblbox"></asp:Label></strong> <asp:TextBox id="txtBillBookNo" runat="server" CssClass="uppercase" MaxLength="10"></asp:TextBox> <asp:CheckBox id="ChbDiscount" runat="server" Visible="False" Width="90px" Text="Promotion" AutoPostBack="True" Checked="True"></asp:CheckBox> </TD></TR><TR><TD vAlign=middle align=left>
<strong><asp:Label id="Label11" runat="server" Width="77px" Text="Order Type" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left colSpan=2><asp:RadioButtonList id="RblPayMode" runat="server" Width="228px" Height="1px" RepeatDirection="Horizontal"><asp:ListItem Selected="True" Value="214">Cash</asp:ListItem>
<asp:ListItem Value="215">Credit</asp:ListItem>
<asp:ListItem Value="216">Advance</asp:ListItem>
</asp:RadioButtonList> </TD><TD vAlign=top align=left colSpan=1><asp:CheckBox id="ChbBatchNo" runat="server" Width="94px" Text="Batch No" AutoPostBack="True" OnCheckedChanged="ChbBatchNo_CheckedChanged"></asp:CheckBox></TD></TR><TR><TD vAlign=middle align=left>
<strong><asp:Label id="Label12" runat="server" Width="101px" Text="Route/Saleman" CssClass="lblbox"></asp:Label></strong></TD><TD vAlign=top align=left colSpan=2><asp:TextBox id="txtprincipal" onkeyup="SearchList()" runat="server" Width="277px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD><TD vAlign=top align=left colSpan=1><asp:TextBox id="txtDeliveryMan" onkeyup="SearchList()" runat="server" Width="180px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 23px" align=left>
<strong><asp:Label id="Label1" runat="server" Width="74px" Text="Customer" CssClass="lblbox"></asp:Label></strong></TD><TD style="HEIGHT: 23px" align=left><asp:TextBox id="txtOutletCode" onkeyup="SearchList()" runat="server" Width="76px" CssClass="txtBox "></asp:TextBox></TD><TD style="HEIGHT: 23px" align=left><asp:TextBox id="txtOutletName" onfocus="SearchedCode()" runat="server" Width="192px" CssClass="txtBox "></asp:TextBox></TD><TD style="HEIGHT: 23px" align=left><asp:TextBox id="txtDiscountType" onkeyup="SearchList()" runat="server" Width="180px" CssClass="txtBox " ReadOnly="True"></asp:TextBox></TD></TR><TR><TD align=left></TD><TD vAlign=top align=left colSpan=2></TD><TD vAlign=top align=left colSpan=1></TD></TR></TBODY></TABLE>&nbsp; 
</ContentTemplate>
                     </asp:UpdatePanel>
                     &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                 </td>
                 <td style="height: 126px">
                 </td>
             </tr>
             <tr>
                
                 <td align="left">
                     <asp:Panel ID="Panel5" runat="server" DefaultButton="btnSave">
        <asp:UpdatePanel id="UpdatePanel1" runat="server">
            <contenttemplate>
                <table>
                    <tr>
                        <td style="height: 16px">
                            <asp:Label id="lblskuCode" runat="server" Width="74px" Height="16px" ForeColor="White" Font-Bold="True" Text="  SKU Code" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label id="lblskuname" runat="server" Width="187px" Height="16px" ForeColor="White" Font-Bold="True" Text="   SKU Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label3" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Quantity" Width="61px"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label id="lblquantity" runat="server" Width="62px" Height="16px" ForeColor="White" Font-Bold="True" Text="Unit Price" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
                        <td align="center" style="height: 16px">
                            <asp:Label ID="Label7" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Batch No" Width="100%" Enabled="False"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label2" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Amount" Width="100%"></asp:Label></td>
                        <td style="height: 16px">
                            <asp:Label ID="Label4" runat="server" BackColor="#006699" CssClass="lblbox" Font-Bold="True"
                                ForeColor="White" Height="16px" Text="Add SKU" Width="100%"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox id="txtskuCode" onkeyup="SearcSKUList(event)" runat="server" Width="68px" CssClass="txtBox"></asp:TextBox></td>
                        <td>
                            <asp:TextBox id="txtskuName" runat="server" Width="181px" Font-Bold="True" CssClass="txtBox" Enabled="False" ></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtBox " onfocus="SearchSKUCode()"
                                Width="55px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox id="txtUnitRate"  runat="server" Width="56px" CssClass="txtBox" Enabled="False"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtBatchNo" runat="server" CssClass="txtBox " Enabled="False" onfocus="SearchSKUCode()"
                                Width="62px">N/A</asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="txtBoxnNum" Enabled="False" onfocus="SearchSKUCode()"
                                Width="72px"></asp:TextBox></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Add Sku"
                                ValidationGroup="vg" Width="100px" AccessKey="A" CssClass="Button" /></td>
                    </tr>
                    <tr>
                        <td align="left" colspan="7">
                            <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" Width="640px" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
                                <asp:GridView id="GrdPurchase" runat="server" ForeColor="SteelBlue" CssClass="gridRow2" BackColor="White" HorizontalAlign="Center" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>
<Columns>
<asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_CODE" HeaderText="SKU Code">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="75px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_NAME" HeaderText="SKU Name">
<ItemStyle HorizontalAlign="Left" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="200px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="QUANTITY_UNIT" HeaderText="Quantity">
<ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="65px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UNIT_PRICE" HeaderText="PRICE" DataFormatString="{0:F2}">
<ItemStyle HorizontalAlign="Right" BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="65px"></ItemStyle>
</asp:BoundField>
    <asp:BoundField DataField="BATCH_NO" HeaderText="Batch No">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" />
    </asp:BoundField>
    <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}">
        <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Right" Width="80px" />
    </asp:BoundField>
    <asp:BoundField DataField="QUANTITY_CTN" HeaderText="QUANTITY_CTN">
        <HeaderStyle CssClass="HidePanel" />
        <ItemStyle CssClass="HidePanel" />
    </asp:BoundField>
<asp:CommandField ShowEditButton="True" HeaderText="Edit">
<ItemStyle BorderColor="Silver" BorderWidth="1px" Width="40px"></ItemStyle>
</asp:CommandField>
<asp:TemplateField HeaderText="Delete"><ItemTemplate>
<asp:LinkButton id="btnDelete" runat="server" Text="Delete" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;" CommandName="Delete"></asp:LinkButton>
</ItemTemplate>

<ItemStyle BorderColor="Silver" BorderWidth="2px" BorderStyle="Solid" Width="45px"></ItemStyle>
</asp:TemplateField>
</Columns>
</asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
</contenttemplate>
        </asp:UpdatePanel>
                     </asp:Panel>
                 </td>
                 <td style="width: 100px">
                     <asp:HiddenField ID="hfBillBookNo" runat="server" />
                 </td>
             </tr>
         </table>
        
           </div>
    <div >
        <table width="100%">
            <tr>
            <td align="left">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
        <contenttemplate>
<TABLE><TBODY><TR><TD vAlign=top align=left colSpan=2 rowSpan=7>
<strong><asp:Label id="Label10" runat="server" Width="60px" Text="Free SKU" CssClass="lblbox"></asp:Label></strong><asp:Panel id="Panel4" runat="server" Width="350px" Height="90px" BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px">
        <asp:GridView id="GrdFreeSKU" runat="server" Width="100%" ForeColor="Silver" CssClass="gridRow2" BorderColor="White" BackColor="White" AutoGenerateColumns="False" HorizontalAlign="Center">
<PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous"></PagerSettings>

<RowStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ForeColor="Black"></RowStyle>
<Columns>
<asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
<HeaderStyle CssClass="HidePanel"></HeaderStyle>

<ItemStyle CssClass="HidePanel"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_Code" HeaderText="SKU Code">
<ItemStyle Width="80px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SKU_Name" HeaderText="SKU Name">
<ItemStyle Width="200px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Quantity" HeaderText="Qty">
<ItemStyle Width="50px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"></ItemStyle>
</asp:BoundField>
</Columns>
<HeaderStyle CssClass="tblhead"></HeaderStyle>
</asp:GridView>
    </asp:Panel> </TD><TD style="HEIGHT: 20px" align=left></TD><TD style="HEIGHT: 20px" align=left>
    <strong><asp:Label id="lblDocumentNo1" runat="server" Width="94px" Text="Gross Sale" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 20px"><asp:TextBox id="txtGrossAmount" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD align=left></TD><TD align=left>
    <strong><asp:Label id="lblfromLocation1" runat="server" Width="94px" Text="Extra Discount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px"><asp:TextBox id="numtxtTotalExtraDiscnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="HEIGHT: 18px" align=left></TD><TD style="HEIGHT: 18px" align=left>
    <strong><asp:Label id="Label51" runat="server" Width="110px" Text="Standard Discount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 18px"><asp:TextBox id="numTxtTotalStndrdDiscnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 1px; HEIGHT: 20px" vAlign=top></TD><TD style="WIDTH: 1px; HEIGHT: 20px" align=left>
    <strong><asp:Label id="Label8" runat="server" Width="116px" Text="Claimable  Discount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 20px" align=right><asp:TextBox id="numtxtUnClaimabledist" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 1px; HEIGHT: 20px" vAlign=top></TD><TD style="WIDTH: 1px; HEIGHT: 20px" align=left>
    <strong><asp:Label id="Label81" runat="server" Width="93px" Text="GST Amount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 20px" align=right><asp:TextBox id="numTxtTotalGST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 1px; HEIGHT: 20px" vAlign=top></TD><TD style="WIDTH: 1px; HEIGHT: 20px" align=left>
    <strong><asp:Label id="Label9" runat="server" Width="93px" Text="TST Amount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 20px" align=right><asp:TextBox id="numTxtTotalTST" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD style="WIDTH: 1px; HEIGHT: 20px" vAlign=top></TD><TD style="WIDTH: 1px; HEIGHT: 20px" align=left>
    <strong><asp:Label id="Label91" runat="server" Width="105px" Text="Net Amount" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px; HEIGHT: 20px" align=right><asp:TextBox id="numTxtTotlAmnt" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" ReadOnly="True"></asp:TextBox></TD></TR><TR><TD align=left colSpan=2><TABLE><TBODY><TR><TD style="WIDTH: 100px">
    <asp:Button accessKey="C" id="btnCalculate" tabIndex=100 onclick="btnCalculate_Click" runat="server" Width="100px" Font-Size="8pt" Text="Calculate" Enabled="False" CssClass="Button" /> </TD><TD style="WIDTH: 100px">
    <asp:Button accessKey="S" id="btnSaveOrder" tabIndex=101 onclick="btnSaveOrder_Click" runat="server" Width="110px" Font-Size="8pt" Text="Save Order" Enabled="False" CssClass="Button" /> </TD><TD style="WIDTH: 100px">
    <asp:Button accessKey="H" id="btnCancel" tabIndex=102 runat="server" Width="110px" Font-Size="8pt" Text="Home" OnClick="btnCancel_Click" CssClass="Button" /> </TD></TR></TBODY></TABLE></TD><TD style="WIDTH: 1px" vAlign=top align=left>&nbsp; &nbsp; &nbsp;&nbsp; </TD><TD style="WIDTH: 1px" align=left>
    <strong><asp:Label id="Label5" runat="server" Width="105px" Text="Cash Received" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 7px" align=right><asp:TextBox id="txtCashReceived" runat="server" Width="150px" ForeColor="Black" Font-Bold="True" CssClass="txtBoxnNum" Enabled="False"></asp:TextBox></TD></TR></TBODY></TABLE>
</contenttemplate>
    </asp:UpdatePanel>
                    &nbsp;&nbsp;
        <asp:TextBox ID="numTxtTotalSED" runat="server" CssClass="txtBox " Font-Bold="False"
            ForeColor="Black" Width="139px" ReadOnly="True" Visible="False"></asp:TextBox>&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>   
   </div>
</asp:Content>


