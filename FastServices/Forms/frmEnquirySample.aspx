<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmEnquirySample.aspx.cs" Inherits="Forms_frmEnquirySample" Title="Fast Services: Add Sample Enquiry" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">

          function HideListBox() {
              document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
          }
     
 function ValidateForm()
 {
		var str;
    
		str  = document.getElementById('<%=txtskuName.ClientID%>').value; 
		if(str == null || str.length == 0)
		{
			alert('Must Select Product Name');
			return false;
		}
		str  = document.getElementById('<%=txtQuantity.ClientID%>').value;
		if (str == null || str.length == 0) {
		    alert('Must Enter Quantity');
		    return false;
		}

		str = document.getElementById('<%=ddlDelivery.ClientID%>').value;
		if (str == -1) {
		    alert('Must Select Delivery Location');
		    return false;
		}

		str = document.getElementById('<%=ddlUOM.ClientID%>').value;
		if (str == -1) {
		    alert('Must Select UOM');
		    return false;
		}

        return true;
 }
 function SearchList()
 {
     var l =  document.getElementById('<%= lstCode.ClientID %>');
     var tb = document.getElementById('<%= txtskuName.ClientID %>');

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
    var str2;
    for (var i=0; i < l.options.length; i++)
    {
        if(l.options[i].selected)
        {
            str = l.options[i].value;
            str2 = l.options[i].text;
           ClearSelection(l);
           break;
        }
        else
        {
            str = "";
        }
    }
    var stroption = document.getElementById("<%= txtskuName.ClientID %>").value;
    if (str.length > 0 && str2 == stroption)
    {
        var str = document.getElementById("<%=lstCode.ClientID %>");
        document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
        document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
        document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
    }
    else 
    {
       document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
       document.getElementById("<%= lstCode.ClientID %>").focus();
    }

}

 function SelectCode(e) {
     var theKey = 0;
     e = (window.event) ? event : e;
     theKey = (e.keyCode) ? e.keyCode : e.charCode;
     if (theKey == 13) {
         var str = document.getElementById("<%=lstCode.ClientID %>");
         document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
         document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
         document.getElementById("<%=Panel1.ClientID %>").className = "HidePanel";
         document.getElementById("<%=txtQuantity.ClientID %>").focus();
     }
 }

 function SelectCodeMouse(e) {
     var str = document.getElementById("<%=lstCode.ClientID %>");
     document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
     document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
     document.getElementById("<%=Panel1.ClientID %>").className = "HidePanel";
     document.getElementById("<%=txtQuantity.ClientID %>").focus();
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
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
<TABLE><TBODY><TR><TD style="HEIGHT: 25px" align=left>
<strong>
    <asp:Label id="Label4" runat="server" Text="Document No" 
        CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
        <asp:DropDownList id="drpDocumentNo" runat="server" Width="206px" 
            CssClass="DropList" AutoPostBack="True" OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged">
                                         </asp:DropDownList></TD><TD align=left></TD><TD style="WIDTH: 99px" align=left>
                                         &nbsp;</TD><TD align=left>
        &nbsp;</TD></TR><TR><TD style="HEIGHT: 25px" align=left>
                                         <strong> <asp:Label id="Label9" runat="server" Width="87px" Text="Location" 
                                             CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
            <asp:DropDownList id="ddlLocation" runat="server" Width="206px" 
                CssClass="DropList">
                                         </asp:DropDownList></TD>
        <TD style="width: 15px;" align=left></TD><TD style="WIDTH: 99px; HEIGHT: 18px" align=left>
                                         &nbsp;</TD><TD style="HEIGHT: 18px" align=left>
            &nbsp;</TD></TR><TR><TD style="HEIGHT: 25px" align=left>
                                         <strong> <asp:Label id="Label6" runat="server" Width="76px" Text="Customer" 
                                             CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
                                         <asp:DropDownList ID="DrpCustomer" runat="server" CssClass="DropList" 
                                             Width="206px">
                                         </asp:DropDownList>
        </TD><TD align=left></TD><TD style="WIDTH: 99px" align=left>
                                         &nbsp;</TD><TD style="HEIGHT: 20px" align=left>
            &nbsp;</TD></TR><TR><TD style="HEIGHT: 25px" align=left valign="top">
                                         <strong> <asp:Label id="lblRemarks" runat="server" Width="76px" Text="Remarks" 
                                             CssClass="lblbox"></asp:Label></strong> </TD><TD style="HEIGHT: 20px" align=left>
            <asp:TextBox id="txtRemarks" runat="server" Width="200px" CssClass="txtBox " 
                Rows="5" TextMode="MultiLine"></asp:TextBox></TD><TD align=left></TD>
        <TD style="WIDTH: 99px; HEIGHT: 20px" align=left valign="top">
                                         &nbsp;</TD>
        <td style="height: 20px" align="left" valign="top">
            <div style="left: 15px; position: absolute; top: 345px">
                <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="Silver" 
                    BorderStyle="Groove" BorderWidth="1px" CssClass="HidePanel" Height="170px" Width="350px">
                     <table width="100%">
                        <tbody>
                            <tr>
                                <td align="left"><strong>Product List</strong></td>
                                <td  align="right" valign="top">
                                    <a style="text-decoration:none;" href="javascrip:void();" onblur="HideListBox">X</a>
                            </tr>
                        </tbody>
                    </table>

                    <asp:ListBox ID="lstCode" runat="server" Height="151px" onclick="SelectCodeMouse();" onkeypress="SelectCode(event)" SelectionMode="Multiple" Width="350px" onblur="HideListBox();">
                    </asp:ListBox>
                </asp:Panel>
            </div>
        </td></TR></TBODY></TABLE>
                                         &nbsp;&nbsp; <asp:TextBox id="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox><asp:TextBox id="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox> &nbsp; &nbsp; &nbsp; 
</ContentTemplate>
                     </asp:UpdatePanel>
                     &nbsp;</td>
             </tr>
         </table>        
           </div>
    <div >
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
<table>
    <tbody>
        <tr>
            <td style="HEIGHT: 16px"><asp:Label id="lblskuname" runat="server" Width="206px" Height="16px" ForeColor="White" Font-Bold="True" Text="   Product Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td><asp:Label id="lblquantity" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td><asp:Label id="lblUOM" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="UOM" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td><asp:Label ID="lblDelivery" runat="server" Text="Delivery Location" Height="16px" ForeColor="White" Font-Bold="True" BackColor="#006699" Width="120px"></asp:Label></td>
            <td><asp:Label ID="lblLeadTime" runat="server" Text="Lead Time" Height="16px" ForeColor="White" Font-Bold="True" BackColor="#006699" Width="90px"></asp:Label></td>
            <td style="HEIGHT: 16px" align="center">&nbsp;</td>
            <td ><asp:HiddenField ID="hfSKUID" runat="server"/> </td>
        </tr>
        <tr>
            <td><asp:TextBox id="txtskuName" onkeyup="SearchList()" runat="server" Width="206px" Font-Bold="True" CssClass="txtBox"></asp:TextBox></td>
            <td><asp:TextBox id="txtQuantity" onfocus="SearchedCode()" runat="server"  Width="73px" CssClass="txtBox "></asp:TextBox></td>
            <td style="width:90px">
                <asp:DropDownList ID="ddlUOM" runat="server" Width="73px">
                </asp:DropDownList>
            </td>
            <td style="width:90px">
                <asp:DropDownList ID="ddlDelivery" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
            <td style="width:120px">
                <asp:TextBox ID="txtLeadTime" runat="server" Width="90px"></asp:TextBox>
                <asp:ImageButton ID="ibtnStartDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
            </td>
            <td> <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
            <cc1:calendarextender ID="ceLeadTime" runat="server" Format="dd-MMM-yyyy"
                PopupButtonID="ibtnStartDate" TargetControlID="txtLeadTime">
            </cc1:CalendarExtender></td>
            <td><asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Font-Size="8pt" Text="Add Product" Width="95px" ValidationGroup="vg" CssClass="Button" /> </td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:Panel id="Panel2" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                        OnRowDeleting="GrdPurchase_RowDeleting" OnRowEditing="GrdPurchase_RowEditing" ShowHeader="False">
                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next" PreviousPageText="Previous" />
                        <RowStyle ForeColor="Black" />
                        <Columns>
                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="206px" BorderWidth="2px" HorizontalAlign="Left"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="73px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM" HeaderText="UOM">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="73px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DelLocation" HeaderText="Delivery Location">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="120px" />
                                
                            </asp:BoundField>
                            <asp:BoundField DataField="LEAD_TIME" HeaderText="Lead Time">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right" Width="120px" />
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
                            <asp:BoundField DataField="PAYMENT_ID" HeaderText="PAYMENT_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
                            </asp:BoundField>
                             <asp:BoundField DataField="SHIPMENT_ID" HeaderText="SHIPMENT_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SHIPMENTMODE_ID" HeaderText="SHIPMENTMODE_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DELIVERYLOCATION_ID" HeaderText="DELIVERYLOCATION_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UOM_ID" HeaderText="UOMID">
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
    </Ttbody>
</table>
<asp:Button accessKey="S" id="btnSaveDocument" runat="server" Font-Size="8pt" Text="Save" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click" CssClass="Button" /> <asp:Button accessKey="C" id="btnCancel" runat="server" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" /> 
<strong><asp:Label id="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong> <asp:TextBox id="txtTotalQuantity" onkeyup="SearchList()" runat="server" Width="88px" CssClass="txtBox" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    </div>
   
</asp:Content>



