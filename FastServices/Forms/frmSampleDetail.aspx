<%@ Page Title="Fast Services: Sample Detail" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmSampleDetail.aspx.cs" Inherits="Forms_frmSampleDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<link href="../SpryAssets/SpryTabbedPanels.css" rel="stylesheet" type="text/css">
<script src="../SpryAssets/SpryTabbedPanels.js" type="text/javascript"></script>
<style>
a {
	color:#006699;
	text-decoration:underline;
}
a:hover {
	color:#3399cc;
}
</style>

<script language="JavaScript" type="text/javascript">

    function pageLoad() {
        $('#<%=ddlBatch.ClientID %>').change(function () {
            document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
        });
        document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
        document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();


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


        $('#<%=pnlEdit.ClientID %>').show();
        $('#<%=pnlSupplier.ClientID %>').show();
        $('#<%=pnlApprove.ClientID %>').show();
        $('#<%=pnlIssue.ClientID %>').show();

    }

    function HideListBox() {
        document.getElementById("<%= pnlProductList.ClientID %>").className = "HidePanel";
    }

    function ValidateForm() {
        var str;

        str = document.getElementById('<%=txtskuName.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Select Product Name');
            return false;
        }
        str = document.getElementById('<%=txtQuantity.ClientID%>').value;
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

    function SearchList() {
        var l = document.getElementById('<%= lstCode.ClientID %>');
        var tb = document.getElementById('<%= txtskuName.ClientID %>');

        if (tb.value == "") {
            ClearSelection(l);
        }
        else {
            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                    l.options[i].selected = true;
                    return false;
                }
                else {
                    ClearSelection(l);
                }
            }
        }
    }


    function SearchedCode() {
        var l = document.getElementById('<%= lstCode.ClientID %>');
        var str;
        var str2;
        for (var i = 0; i < l.options.length; i++) {
            if (l.options[i].selected) {
                str = l.options[i].value;
                str2 = l.options[i].text;
                ClearSelection(l);
                break;
            }
            else {
                str = "";
            }
        }
        var stroption = document.getElementById("<%= txtskuName.ClientID %>").value;
        if (str.length > 0 && str2 == stroption) {
            var str = document.getElementById("<%=lstCode.ClientID %>");
            document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
            document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
            document.getElementById("<%= pnlProductList.ClientID %>").className = "HidePanel";
        }
        else {
            document.getElementById("<%= pnlProductList.ClientID %>").className = "ShowPanel";
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
            document.getElementById("<%=pnlProductList.ClientID %>").className = "HidePanel";
            document.getElementById("<%=txtQuantity.ClientID %>").focus();
        }
    }

    function SelectCodeMouse() {
        alert('');
        var str = document.getElementById("<%=lstCode.ClientID %>");
        document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
        document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
        document.getElementById("<%=pnlProductList.ClientID %>").className = "HidePanel";
        document.getElementById("<%=txtQuantity.ClientID %>").focus();
    }
        
    function ClearSelection(lb) {
        lb.selectedIndex = -1;
    }

    //Issue Sample

    function HideListBoxIssue() {
        document.getElementById("<%= pnlProductIssue.ClientID %>").className = "HidePanel";
    }
    function ValidateFormIssue() {
        var str;
        str = document.getElementById('<%=txtskuNameIssue.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Select Product Name');
            return false;
        }
        str = document.getElementById('<%=txtQuantityIssue.ClientID%>').value;
        if (str == null || str.length == 0) {
            alert('Must Enter Quantity');
            return false;
        }

        return true;
    }
    function SearchListIssue() {
        var l = document.getElementById('<%= lstCodeIssue.ClientID %>');
        var tb = document.getElementById('<%= txtskuNameIssue.ClientID %>');

        if (tb.value == "") {
            ClearSelection(l);
        }
        else {
            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].value.toLowerCase().match(tb.value.toLowerCase())) {
                    l.options[i].selected = true;
                    return false;
                }
                else {
                    ClearSelection(l);
                }
            }
        }
    }
    function SearchedCodeIssue() {
        var l = document.getElementById('<%= lstCodeIssue.ClientID %>');
        var str;
        var str2;
        for (var i = 0; i < l.options.length; i++) {
            if (l.options[i].selected) {
                str = l.options[i].value;
                str2 = l.options[i].text;
                ClearSelection(l);
                break;
            }
            else {
                str = "";
            }
        }
        var stroption = document.getElementById("<%= txtskuNameIssue.ClientID %>").value;
        if (str.length > 0 && str2 == stroption) {
            var str = document.getElementById("<%=lstCodeIssue.ClientID %>");
            document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = str.options[str.selectedIndex].value
            document.getElementById("<%=txtskuNameIssue.ClientID %>").value = str.options[str.selectedIndex].text;
            document.getElementById("<%= pnlProductIssue.ClientID %>").className = "HidePanel";
        }
        else {
            document.getElementById("<%= pnlProductIssue.ClientID %>").className = "ShowPanel";
            document.getElementById("<%= lstCodeIssue.ClientID %>").focus();
        }

    }
    
    function SelectCodeIssue(e) {
        var theKey = 0;
        e = (window.event) ? event : e;
        theKey = (e.keyCode) ? e.keyCode : e.charCode;
        if (theKey == 13) {
            var str = document.getElementById("<%=lstCodeIssue.ClientID %>");
            document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = str.options[str.selectedIndex].value;
            document.getElementById("<%=txtskuNameIssue.ClientID %>").value = str.options[str.selectedIndex].text;
            document.getElementById("<%=pnlProductIssue.ClientID %>").className = "HidePanel";
            document.getElementById("<%=txtQuantityIssue.ClientID %>").focus();

            var ddlBatch = $('#<%=ddlBatch.ClientID %>');
            $('#<%=ddlBatch.ClientID %> option').remove(); ;
            var l = document.getElementById('<%= lbBatch.ClientID %>');

            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].value == document.getElementById("<%=hfSKUIDIssue.ClientID %>").value) {
                    var opts = $("<option />");
                    opts = $("<option />").html(l.options[i].text).attr("value", l.options[i].value);
                    ddlBatch.append(opts);
                }
            }
            document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
            document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();
        }
    }


    function SelectCodeIssueMouse() {
        var str = document.getElementById("<%=lstCodeIssue.ClientID %>");
        document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = str.options[str.selectedIndex].value;
        document.getElementById("<%=txtskuNameIssue.ClientID %>").value = str.options[str.selectedIndex].text;
        document.getElementById("<%=pnlProductIssue.ClientID %>").className = "HidePanel";
        document.getElementById("<%=txtQuantityIssue.ClientID %>").focus();

        var ddlBatch = $('#<%=ddlBatch.ClientID %>');
        $('#<%=ddlBatch.ClientID %> option').remove(); ;
        var l = document.getElementById('<%= lbBatch.ClientID %>');

        for (var i = 0; i < l.options.length; i++) {
            if (l.options[i].value == document.getElementById("<%=hfSKUIDIssue.ClientID %>").value) {
                var opts = $("<option />");
                opts = $("<option />").html(l.options[i].text).attr("value", l.options[i].value);
                ddlBatch.append(opts);
            }
        }
        document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
        document.getElementById("<%=hfSKUIDIssue.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();
    }
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">
<div id="action">
    <div id="tab_done"></div><h2>Action </h2>    
    <div id="TabbedPanels1" class="TabbedPanels">
        <asp:Literal ID="ltrlTabs" runat="server"></asp:Literal>    
        <div class="TabbedPanelsContentGroup">

        <div class="TabbedPanelsContent">        
         <asp:UpdatePanel id="UpdatePanel5" runat="server">
                        <contenttemplate>         
           <div >
        <table width="100%">
             <tr>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
<TABLE><tbody><tr><td style="HEIGHT: 25px" align=left>
                                         <strong> 
                                         <asp:Label id="Label1" runat="server" Width="87px" Text="Document No" 
                                             CssClass="lblbox"></asp:Label></strong> </td><td style="HEIGHT: 20px" align=left>
            
            <strong><asp:Label ID="lblDocumentNoView" runat="server" CssClass="lblbox" Text="" Width="87px"></asp:Label>
            </strong></td>
        <td style="width: 15px;" align=left>&nbsp;</td><td style="WIDTH: 99px; HEIGHT: 18px" align=left>
                                         &nbsp;</td><td style="HEIGHT: 18px" align=left>
            &nbsp;</td></tr><tr><td style="HEIGHT: 25px" align=left>
                                         <strong> 
                                         <asp:Label id="Label3" runat="server" Width="87px" Text="Location" 
                                             CssClass="lblbox"></asp:Label></strong> </td><td style="HEIGHT: 20px" align=left>
                                         <strong><asp:Label ID="lblLocation" runat="server" CssClass="lblbox" Text="" Width="200px"></asp:Label></strong>
        </td><td align=left style="width: 15px;"></td>
        <td style="WIDTH: 99px; height: 18px;" align=left>
                                         &nbsp;</td><td style="HEIGHT: 18px" align=left>
            &nbsp;</td></tr><tr><td style="HEIGHT: 25px" align=left>
                                         <strong> 
                                         <asp:Label id="Label4" runat="server" Width="76px" Text="Customer" 
                                             CssClass="lblbox"></asp:Label></strong> </td><td style="HEIGHT: 20px" align=left>
            <strong><asp:Label ID="lblCustomerView" runat="server" CssClass="lblbox" Text="" Width="200px"></asp:Label></strong>
        </td><td align=left></td>
        <td style="WIDTH: 99px; " align=left>
                                         &nbsp;</td>
        <td style="height: 20px" align="left">
            &nbsp;</td></tr>
   
   <tr>
        <td align="left" style="HEIGHT: 25px" valign="top">
            <strong>
            <asp:Label ID="Label2" runat="server" CssClass="lblbox" Text="Remarks" 
                Width="76px"></asp:Label>
            </strong>
        </td>
        <td align="left" style="HEIGHT: 20px">
        <strong><asp:Label ID="lblRemarksView" runat="server" CssClass="lblbox" Text="" Width="200px" Height="50px"></asp:Label></strong>
        </td>
        <td align="left">
        </td>
        <td align="left" style="WIDTH: 99px; HEIGHT: 20px" valign="top">
            &nbsp;</td>
        <td align="left" style="height: 20px" valign="top">
            
        </td>
    </tr>
    
    
     
    </tbody></TABLE>
</ContentTemplate>
                     </asp:UpdatePanel>
                     &nbsp;</td>
             </tr>
         </table>     
        <table width="100%">
        <tr>
            <td>
                
<table>
    <tbody>
    <tr>
            <td style="height: 16px;width:206px;"><asp:Label id="Label5" runat="server" Width="210px" Height="16px" ForeColor="White" Font-Bold="True" Text="   Product Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td style="height: 16px;width:73px;"><asp:Label id="Label8" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td><asp:Label id="lblUOM" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="UOM" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td style="height: 16px;width:73px;"><asp:Label ID="Label12" runat="server" Text="Delivery Location" Height="16px" ForeColor="White" Font-Bold="True" BackColor="#006699" Width="120px"></asp:Label></td>
            <td style="height: 16px;width:120px;"><asp:Label ID="Label13" runat="server" Text="Lead Time" Height="16px" ForeColor="White" Font-Bold="True" BackColor="#006699" Width="120px"></asp:Label></td>
            <td style="height: 16px" align="center">&nbsp;</td>
            <td ><asp:HiddenField ID="HiddenField1" runat="server"/> </td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:Panel id="Panel5" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                    <asp:GridView ID="gvMainView" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center" ShowHeader="False">
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
<strong><asp:Label id="lblTotalQty" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong> 
<asp:Label id="txtTotalQty" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label>
</td>
        </tr>
    </table>   
           </div>
           </contenttemplate>
           </asp:UpdatePanel>
    <div >
    
    </div>
        </div>

        <div class="TabbedPanelsContent">       
           <asp:Panel ID="pnlEdit" runat="server" CssClass="HidePanel">
        <table width="100%">
             <tr>
                 <td>
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                         <ContentTemplate>
<table>
    <tbody>
    <tr>
        <td colspan="5">
            <asp:Literal ID="ltrlEdit" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td style="HEIGHT: 25px" align=left>
          <strong> <asp:Label id="Label10" runat="server" Width="87px" Text="Document No" CssClass="lblbox"></asp:Label></strong> 
        </td>
        <td style="HEIGHT: 20px" align=left>
            <strong><asp:Label ID="lblDocumentNo" runat="server" CssClass="lblbox" Text="" Width="87px"></asp:Label></strong>
         </td>
        <td style="width: 15px;" align=left>&nbsp;</td>
        <td style="WIDTH: 99px; HEIGHT: 18px" align=left>&nbsp;</td>
        <td style="HEIGHT: 18px" align=left>&nbsp;</td>
</tr>
        
    <tr><td style="HEIGHT: 25px" align=left>
                                         <strong> 
                                         <asp:Label id="Label9" runat="server" Width="87px" Text="Location" 
                                             CssClass="lblbox"></asp:Label></strong> </td><td style="HEIGHT: 20px" align=left>
                                         <asp:DropDownList ID="ddlLocation" runat="server" CssClass="DropList" 
                                             Width="206px">
                                         </asp:DropDownList>
        </td><td align=left style="width: 15px;"></td>
        <td style="WIDTH: 99px; height: 18px;" align=left>
                                         &nbsp;</td><td style="HEIGHT: 18px" align=left>
            &nbsp;</td></tr><tr><td style="HEIGHT: 25px" align=left>
                                         <strong> 
                                         <asp:Label id="Label6" runat="server" Width="76px" Text="Customer" 
                                             CssClass="lblbox"></asp:Label></strong> </td><td style="HEIGHT: 20px" align=left>
            <asp:DropDownList ID="DrpCustomer" runat="server" CssClass="DropList" 
                Width="206px">
            </asp:DropDownList>
        </td><td align=left></td>
        <td style="WIDTH: 99px; " align=left>
                                         &nbsp;</td>
        <td style="height: 20px" align="left">
            &nbsp;</td></tr>
    <tr>
        <td align="left" style="HEIGHT: 25px" valign="top">
            <strong>
            <asp:Label ID="lblRemarks" runat="server" CssClass="lblbox" Text="Remarks" 
                Width="76px"></asp:Label>
            </strong>
        </td>
        <td align="left" style="HEIGHT: 20px">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Rows="5" 
                TextMode="MultiLine" Width="200px"></asp:TextBox>
        </td>
        <td align="left">
        </td>
        <td align="left" style="WIDTH: 99px; HEIGHT: 20px" valign="top">
            &nbsp;</td>
        <td align="left" style="height: 20px" valign="top">
            <div style="left: 15px; position: absolute; top: 420px">
                <asp:Panel ID="pnlProductList" runat="server" BackColor="Silver" 
                    BorderColor="Silver" BorderStyle="Groove" BorderWidth="1px" 
                    CssClass="HidePanel" Height="170px" Width="350px">
                    <table width="100%">
                        <tbody>
                            <tr>
                                <td align="left">
                                    <strong>Product List</strong></td>
                                <td align="right" valign="top">
                                    <a href="javascrip:void();" onblur="HideListBox();" style="text-decoration:none;">X</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:ListBox ID="lstCode" runat="server" Height="151px" onclick="SelectCodeMouse();"  onblur="HideListBox();" 
                        onkeypress="SelectCode(event)" Width="350px">
                    </asp:ListBox>
                </asp:Panel>
            </div>
        </td>
    </tr>
    </tbody></table>

</ContentTemplate>
                     </asp:UpdatePanel>
                     &nbsp;</td>
             </tr>
         </table>        
           </asp:Panel>
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
            <td><asp:Label id="Label11" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="UOM" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
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
            </cc1:calendarextender></td>
            <td><asp:Button accessKey="A" id="btnSaveEnquiry" onclick="btnSaveEnquiry_Click" runat="server" Font-Size="8pt" Text="Add Product" Width="95px" ValidationGroup="vg" CssClass="Button" /> </td>
        </tr>
        <tr>
            <td colspan="9">
                <asp:Panel id="Panel4" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                    <asp:GridView ID="gvMainEdit" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center" 
                        OnRowDeleting="gvMainEdit_RowDeleting" OnRowEditing="gvMainEdit_RowEditing" ShowHeader="False">
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
<asp:Button accessKey="S" id="btnSaveDocument" runat="server" Font-Size="8pt" Text="Save" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click" CssClass="Button" />
<strong><asp:Label id="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong> 
<asp:TextBox id="txtTotalQuantity" onkeyup="SearchList()" runat="server" Width="88px" CssClass="txtBox" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
        </div>

        <div class="TabbedPanelsContent">
            <table width="95%">
            <tr>               
                <td>
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
                            <asp:Panel ID="pnlSupplier" runat="server" CssClass="HidePanel" Width="100%">                       
                                <table width="100%">
                                    <tbody>
                                        <tr>
                                    <td style="width:100%;"> 
                                        <asp:Literal ID="ltrlEmail" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                        <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td style="width:15%;">
                                                    <strong>Enquiry ID</strong>
                                                </td>
                                                <td >
                                                    <asp:Label ID="lblEnquiry" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hfEnquiryID" runat="server" />
                                                    <asp:HiddenField ID="hfTabIndex" runat="server" />
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </td>
                                </tr>
                                        <tr>
                                    <td>
                                       <table width="100%">
                                            <tr>
                                                <td style="width:10%;">
                                                    <strong>Customer</strong>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCustomer" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="hfCustomerID" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gvMain" runat="server" AutoGenerateColumns="False" 
                                                    Width="100%" DataKeyNames="SKU_ID" CellPadding="0" CellSpacing="0" 
                                                    ForeColor="Black" GridLines="None"
                                                    ShowHeader="false" onrowdatabound="gvMain_RowDataBound">
    <Columns>
        <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField>
                <asp:BoundField DataField="QUANTITY" HeaderText="QUANTITY">
            <HeaderStyle CssClass="HidePanel" />
            <ItemStyle CssClass="HidePanel" />
        </asp:BoundField> 
        <asp:TemplateField>
            <ItemTemplate>
               <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td >
                            <h1> <asp:Label ID="lblSKU" runat="server" Text='<%# Eval("SKU_NAME")%>' ></asp:Label></h1>
                        </td>
                    </tr>                    
                    <tr>
                        <td colspan="2">
                            <div>
                                <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" BackColor="White" DataKeyNames="ORGANIZATION_ID"
                                 BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" OnRowDataBound="gvSupplier_RowDataBound"
                                 HorizontalAlign="Center" EnableModelValidation="True" >
                                    <Columns>
                                  <asp:BoundField DataField="ENQUIREY_DETAIL_ID" HeaderText="ENQUIREY_DETAIL_ID">
                                         <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                     </asp:BoundField>   
                                    <asp:BoundField DataField="ORGANIZATION_ID" HeaderText="ORGANIZATION_ID">
                                         <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                     </asp:BoundField>                                                        
                                     <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="cbQuotation" runat="server" Checked='<%# Eval("IS_QUOTATION")%>' />
                                         </ItemTemplate>
                                          <HeaderTemplate>
                                            <asp:CheckBox id="cbQuotationAll" runat="server" onclick="javascript:HeaderClick(this);"></asp:CheckBox>Select
                                         </HeaderTemplate>
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="70px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>                                                       
                                     <asp:BoundField DataField="NAME" HeaderText="Supplier">
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>    
                                      <asp:BoundField DataField="LastEmailDateTime" HeaderText="Email DateTime"  DataFormatString="{0:dd/MM/yyyy hh:mm}">
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>                  
                                     <asp:TemplateField HeaderText="Email To">
                                         <ItemTemplate>
                                             <asp:CheckBoxList ID="cblEmailID" runat="server">
                                             </asp:CheckBoxList>
                                         </ItemTemplate>
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Email Cc">
                                         <ItemTemplate>
                                             <asp:TextBox ID="txtEmailCc" runat="server" Width="250px"></asp:TextBox>
                                         </ItemTemplate>
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="PRODUCT_ID" HeaderText="PRODUCT_ID">
                                         <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                     </asp:BoundField> 
                                                                                      
                                 </Columns>
                                 <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
                                     VerticalAlign="Middle" />
                                     <EmptyDataTemplate>                                        
                                             <strong>No Supplier Assigned</strong>                                     
                                     </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
               </table>                     
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                    <td style="width:100%;"> 
                                         <asp:Button ID="btnAddEmail" runat="server" CssClass="Button" 
                                             Text="Email" onclick="btnSendEmail_Click" />
                                        &nbsp;                   
                                    </td>
                                </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>                        
                        </contenttemplate>
                    </asp:UpdatePanel>
                    <div style="z-index: 101; left: 640px; width: 100px; position: absolute; top: 10px;
                        height: 50px">
                        &nbsp;<asp:Panel ID="Panel2" runat="server">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="btnEmailImage" runat="server" Height="26px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="23px" />
                                    Wait Update</ProgressTemplate>
                            </asp:UpdateProgress>
                        </asp:Panel>
                    </div>
                </td>               
            </tr>
        </table>
        </div>
        
        <div class="TabbedPanelsContent">
            <asp:Panel ID="pnlApprove" runat="server" CssClass="HidePanel" Width="100%">                       
     <table width="100%">
         <tr>
             <td>
                 <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                     <ContentTemplate>
<TABLE><TBODY>
    <tr>
        <td align="left" colspan="2" style="WIDTH: 100px">
            <strong>
            <asp:Literal ID="ltrlApprove" runat="server"></asp:Literal>            
            </strong>
        </td>
    
        <td style="HEIGHT: 25px">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="WIDTH: 100px">
            <strong>
            <asp:Label ID="Label17" runat="server" CssClass="lblbox" Text="Location" 
                Width="67px"></asp:Label>
            </strong>
        </td>
        <td style="WIDTH: 100px">
            <asp:DropDownList ID="drpDistributor" runat="server"  Enabled="false"
                CssClass="DropList" Width="200px">
            </asp:DropDownList>
        </td>
        <td style="HEIGHT: 25px">
        </td>
    </tr>
    <TR><TD style="WIDTH: 100px" align="left">
                                     <strong><asp:Label id="Label18" runat="server" Width="104px" Text="Customer" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 100px">
        <asp:DropDownList id="DrpCustomerSample" runat="server" Width="200px" 
            CssClass="DropList" Enabled="false">
                                     </asp:DropDownList></TD><TD style="HEIGHT: 25px"></TD></TR><TR>
    <TD vAlign=top align=left>
                                     &nbsp;</TD><TD align=left>&nbsp;</TD><TD align=left></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 12px" valign="bottom" align=left>
                                     </TD><TD style="WIDTH: 100px; HEIGHT: 12px" align=left>
                                     <asp:Button id="btnGenerate" onclick="btnGenerate_Click" runat="server" Text="Approve" CssClass="Button" />
                                      <DIV style="Z-INDEX: 101; LEFT: 368px; WIDTH: 100px; POSITION: absolute; TOP: 100px; HEIGHT: 100px"><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                             <ProgressTemplate>
                                                 <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                     Width="22px" />
                                                 Record Update
                                             </ProgressTemplate>
                                         </asp:UpdateProgress> </DIV></TD><TD style="HEIGHT: 25px" align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
                 </asp:UpdatePanel>
                 <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                     <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GrdQuotation" runat="server" AutoGenerateColumns="False" BackColor="White"
                                 BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" 
                                 HorizontalAlign="Center" EnableModelValidation="True" 
                                        onrowcreated="GrdQuotation_RowCreated">
                                 
                                 <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                     PreviousPageText="Previous" />
                                     
                                 <Columns>
                                    
                                    <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                         <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                     </asp:BoundField>
                                      <asp:BoundField DataField="ORGANIZATION_ID" HeaderText="ORGANIZATION_ID">
                                         <HeaderStyle CssClass="HidePanel" />
                                         <ItemStyle CssClass="HidePanel" />
                                     </asp:BoundField>
                                     <asp:TemplateField>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="cbApproved" runat="server" Checked='<%# Eval("IS_APPROVED")%>' />
                                         </ItemTemplate>
                                          <HeaderTemplate>
                                            <asp:CheckBox id="cbApproveAll" runat="server" onclick="javascript:HeaderClick(this);"></asp:CheckBox>Approve All
                                         </HeaderTemplate>
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="100px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:TemplateField>
                                    <asp:BoundField DataField="SKU_NAME" HeaderText="Product">
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="250px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                     <asp:BoundField DataField="NAME" HeaderText="Supplier">
                                         <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Width="150px" HorizontalAlign="Left" />
                                         <HeaderStyle HorizontalAlign="Left" />
                                     </asp:BoundField>
                                 </Columns>
                                 <HeaderStyle CssClass="tblhead" HorizontalAlign="Center"
                                     VerticalAlign="Middle" />
                             </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfGrdQuotationRows" runat="server" />
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
        <asp:Panel ID="pnlIssue" runat="server" CssClass="HidePanel" Width="100%">                       
        <table width="100%">
            <tr>
                <td >
                    <asp:UpdatePanel id="UpdatePanel9" runat="server">
                        <contenttemplate>
<TABLE>
<TBODY>
<tr>
    <td colspan="4">
        <asp:Literal ID="ltrlIssue" runat="server"></asp:Literal>
    </td>
</tr>
<TR>
<TD style="HEIGHT: 25px" align="left">
    <strong> <asp:Label ID="Label19" runat="server" Text="Document No" 
        Width="94px"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="drpDocumentNoIssue" runat="server" AutoPostBack="True" 
            CssClass="DropList" 
            OnSelectedIndexChanged="drpDocumentNoIssue_SelectedIndexChanged" Width="200px">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px">
    </TD>
    <TD style="width: 316px;" align="center" colspan="1" rowspan="3" 
        valign="middle">
        <strong><asp:Label id="Label20" runat="server" Width="249px" Height="4px"></asp:Label></strong>
        
        <div style="left: 15px; position: absolute; top: 270px">
                <asp:Panel ID="pnlProductIssue" runat="server" BackColor="Silver" BorderColor="Silver" 
                    BorderStyle="Groove" BorderWidth="1px" CssClass="HidePanel" Height="170px" Width="350px">
                     <table width="100%">
                        <tbody>
                            <tr>
                                <td align="left"><strong>Product List</strong></td>
                                <td  align="right" valign="top">
                                    <a style="text-decoration:none;" href="javascrip:void();" onblur="HideListBox">X</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:ListBox ID="lbBatch" runat="server" CssClass="HidePanel">
                    </asp:ListBox>
                    <asp:ListBox ID="lstCodeIssue" runat="server" Height="151px" onclick="SelectCodeIssueMouse();" onkeypress="SelectCodeIssue(event)" SelectionMode="Multiple" Width="350px" onblur="HideListBoxIssue();">
                    </asp:ListBox>
                </asp:Panel>
            </div>
    </TD></TR>
<TR><TD align=left style="HEIGHT: 25px">
    <strong>
    <asp:Label ID="Label21" runat="server" Text="Location" 
        Width="94px" CssClass="lblbox"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="drpDistributorIssue" runat="server" Enabled="false"
            CssClass="DropList" 
            Width="200px">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px"></TD></TR><TR><TD align=left style="HEIGHT: 25px">
    <strong>
    <asp:Label ID="lblfromLocation0" runat="server" CssClass="lblbox" 
        Text="Customer" Width="94px"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="DrpCustomerIssue" runat="server" Enabled="false"
            CssClass="DropList" 
            Width="200px">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px">&nbsp;</TD></TR>
    </TBODY>
</TABLE>
</contenttemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>        
    <table width="100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
<table>
    <tbody>
        <tr>
            <td style="HEIGHT: 16px"><asp:Label id="Label22" runat="server" Width="206px" Height="16px" ForeColor="White" Font-Bold="True" Text="   Product Name" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td><asp:Label id="Label23" runat="server" Width="73px" Height="16px" ForeColor="White" Font-Bold="True" Text="Quantity" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td style="HEIGHT: 16px" align="center"><asp:Label id="Label24" runat="server" Width="76px"  Height="16px" ForeColor="White" Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td >
                <asp:HiddenField ID="hfSKUIDIssue" runat="server"/> 
                <asp:HiddenField ID="hfBatchNo" runat="server" />
            </td>
        </tr>
        <tr>
            <td><asp:TextBox id="txtskuNameIssue" onkeyup="SearchListIssue()" runat="server" Width="206px" Font-Bold="True" CssClass="txtBox"></asp:TextBox></td>
            <td><asp:TextBox id="txtQuantityIssue" onfocus="SearchedCodeIssue()" runat="server"  Width="73px" CssClass="txtBox "></asp:TextBox></td>
            <td><asp:DropDownList ID="ddlBatch" runat="server" Width="76px"></asp:DropDownList>
            </td>
            <td><asp:Button accessKey="A" id="btnSaveIssue" onclick="btnSaveIssue_Click" runat="server" Font-Size="8pt" Text="Issue Sample" Width="95px" ValidationGroup="vg" CssClass="Button" /> </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel id="Panel3" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                    <asp:GridView ID="gvIssue" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="White" CssClass="gridRow2" ForeColor="SteelBlue" HorizontalAlign="Center"
                        OnRowDeleting="gvIssue_RowDeleting" OnRowEditing="gvIssue_RowEditing" ShowHeader="False">
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
                            <asp:BoundField DataField="BATCH_NO" HeaderText="BatchNo">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="76px" BorderWidth="1px" HorizontalAlign="Right" />
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
                    </asp:GridView>
                </asp:Panel> 
            </td>
        </tr>
    </Ttbody>
</table>
<asp:Button accessKey="S" id="btnSaveDocumentIssue" runat="server" Font-Size="8pt" Text="Save" UseSubmitBehavior="False" OnClick="btnSaveDocumentIssue_Click" CssClass="Button" /> 
<asp:Button accessKey="C" id="btnCancelIssue" runat="server" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancelIssue_Click" CssClass="Button" /> 
<strong><asp:Label id="Label25" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong> 
<asp:TextBox id="txtTotalQuantityIssue" onkeyup="SearchList()Issue" runat="server" Width="88px" CssClass="txtBox" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </asp:Panel>
        </div>
                
        </div>
        <asp:HiddenField ID="ActiveTabIndex" runat="server" />
    </div>
    </div>
</div>
</asp:Content>

