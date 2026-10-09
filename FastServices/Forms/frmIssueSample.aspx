<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmIssueSample.aspx.cs" Inherits="Forms_frmIssueSampe" Title="Fast Services: Issue Sample" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit"   Namespace="AjaxControlToolkit"    TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
      <script language="JavaScript" type="text/javascript">

          function pageLoad() {
              $('#<%=ddlBatch.ClientID %>').change(function () {
                  document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
              });
              document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
              document.getElementById("<%=hfSKUID.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();
          }

          function HideListBox() {
              document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
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
                    str2 = "";
                }
            }

            var stroption = document.getElementById("<%= txtskuName.ClientID %>").value;

            if (str.length > 0 && str2 == stroption) {
                var str = document.getElementById("<%=lstCode.ClientID %>");
                document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
                document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
                document.getElementById("<%= Panel1.ClientID %>").className = "HidePanel";
            }
            else {
                document.getElementById("<%= Panel1.ClientID %>").className = "ShowPanel";
                document.getElementById("<%= lstCode.ClientID %>").focus();
            }
            ClearSelection(document.getElementById('<%= lstCode.ClientID %>'));

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

                var ddlBatch = $('#<%=ddlBatch.ClientID %>');
                $('#<%=ddlBatch.ClientID %> option').remove(); ;
                var l = document.getElementById('<%= lbBatch.ClientID %>');
                for (var i = 0; i < l.options.length; i++) {
                    if (l.options[i].value == document.getElementById("<%=hfSKUID.ClientID %>").value) {
                        var opts = $("<option />");
                        opts = $("<option />").html(l.options[i].text).attr("value", l.options[i].value);
                        ddlBatch.append(opts);
                    }
                }

                document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
                document.getElementById("<%=hfSKUID.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();
            }
        }

        function SelectCodeMouse(e) {
            var str = document.getElementById("<%=lstCode.ClientID %>");
            document.getElementById("<%=hfSKUID.ClientID %>").value = str.options[str.selectedIndex].value;
            document.getElementById("<%=txtskuName.ClientID %>").value = str.options[str.selectedIndex].text;
            document.getElementById("<%=Panel1.ClientID %>").className = "HidePanel";
            document.getElementById("<%=txtQuantity.ClientID %>").focus();

            var ddlBatch = $('#<%=ddlBatch.ClientID %>');
            $('#<%=ddlBatch.ClientID %> option').remove(); ;
            var l = document.getElementById('<%= lbBatch.ClientID %>');
            for (var i = 0; i < l.options.length; i++) {
                if (l.options[i].value == document.getElementById("<%=hfSKUID.ClientID %>").value) {
                    var opts = $("<option />");
                    opts = $("<option />").html(l.options[i].text).attr("value", l.options[i].value);
                    ddlBatch.append(opts);
                }
            }

            document.getElementById("<%=hfBatchNo.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').text();
            document.getElementById("<%=hfSKUID.ClientID %>").value = $('#<%=ddlBatch.ClientID %> option:selected').val();
        }

        function ClearSelection(lb) {
            lb.selectedIndex = -1;
        }
 
  </script>
  <div id="right_data">
    <div >
        <table width="100%">
            <tr>
                <td >
                    <asp:UpdatePanel id="UpdatePanel2" runat="server">
                        <contenttemplate>
<TABLE>
<TBODY>
<TR>
<TD style="HEIGHT: 25px" align="left">
    <strong> <asp:Label ID="lblDocumentNo" runat="server" Text="Document No" 
        Width="94px"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="drpDocumentNo" runat="server" AutoPostBack="True" 
            CssClass="DropList" 
            OnSelectedIndexChanged="drpDocumentNo_SelectedIndexChanged" Width="200px">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px">
    </TD>
    <TD style="width: 316px;" align="center" colspan="1" rowspan="3" 
        valign="middle">
        <strong><asp:Label id="Label6" runat="server" Width="249px" Height="4px"></asp:Label></strong>
        
        <div style="left: 15px; position: absolute; top: 235px">
                <asp:Panel ID="Panel1" runat="server" BackColor="Silver" BorderColor="Silver" 
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
                    <asp:ListBox ID="lstCode" runat="server" Height="151px" onclick="SelectCodeMouse();" onkeypress="SelectCode(event)" SelectionMode="Multiple" Width="350px" onblur="HideListBox();">
                    </asp:ListBox>
                </asp:Panel>
            </div>
    </TD></TR>
    <TR><TD align=left style="HEIGHT: 25px">
    <strong>
    <asp:Label ID="Label1" runat="server" Text="Supplier" 
        Width="94px" CssClass="lblbox"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="True" 
            Width="200px" onselectedindexchanged="ddlSupplier_SelectedIndexChanged">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px"></TD></TR>
<TR><TD align=left style="HEIGHT: 25px">
    <strong>
    <asp:Label ID="lblfromLocation" runat="server" Text="Location" 
        Width="94px" CssClass="lblbox"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="drpDistributor" runat="server" AutoPostBack="True" 
            CssClass="DropList" 
            Width="200px">
        </asp:DropDownList>
    </TD><TD style="HEIGHT: 25px"></TD></TR>
        
    <TR><TD align=left style="HEIGHT: 25px">
    <strong>
    <asp:Label ID="lblfromLocation0" runat="server" CssClass="lblbox" 
        Text="Customer" Width="94px"></asp:Label></strong>
    </TD><TD style="HEIGHT: 25px; width: 1px;">
        <asp:DropDownList ID="DrpCustomer" runat="server" 
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
            <td style="HEIGHT: 16px" align="center"><asp:Label id="lblBatchNo" runat="server" Width="76px"  Height="16px" ForeColor="White" Font-Bold="True" Text="Batch No" CssClass="lblbox" BackColor="#006699"></asp:Label></td>
            <td >
                <asp:HiddenField ID="hfSKUID" runat="server"/>
                <asp:HiddenField ID="hfBatchNo" runat="server"/>
             </td>
        </tr>
        <tr>
            <td><asp:TextBox id="txtskuName" onkeyup="SearchList()" runat="server" Width="206px" Font-Bold="True" CssClass="txtBox"></asp:TextBox></td>
            <td><asp:TextBox id="txtQuantity" onfocus="SearchedCode()" runat="server"  Width="73px" CssClass="txtBox "></asp:TextBox></td>
            <td><asp:DropDownList ID="ddlBatch" runat="server" Width="76px"></asp:DropDownList></td>
            <td><asp:Button accessKey="A" id="btnSave" onclick="btnSave_Click" runat="server" Font-Size="8pt" Text="Add Product" Width="95px" CssClass="Button" /> </td>
        </tr>
        <tr>
            <td colspan="4">
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
                            <asp:BoundField DataField="BATCH_NO" HeaderText="BatchNo">
                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="76px" BorderWidth="1px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRINCIPAL_ID" HeaderText="PRINCIPAL_ID">
                                <HeaderStyle CssClass="HidePanel" />
                                <ItemStyle CssClass="HidePanel" />
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
<asp:Button accessKey="S" id="btnSaveDocument" runat="server" Font-Size="8pt" Text="Save" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click" CssClass="Button" /> 
<asp:Button accessKey="C" id="btnCancel" runat="server" Font-Size="8pt" Text="Cancel" UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" /> 
<strong><asp:Label id="Label7" runat="server" Width="103px" Height="16px" Text="Total Quantity"></asp:Label></strong> 
<asp:TextBox id="txtTotalQuantity" onkeyup="SearchList()" runat="server" Width="88px" CssClass="txtBox" ReadOnly="True"></asp:TextBox> 
</ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    </div>
   
</asp:Content>



