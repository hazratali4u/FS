<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmApproveSample.aspx.cs" Inherits="Forms_frmApproveSample" Title="Fast Services: Approve Sample" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script type="text/javascript">
    var TotalChkBx;
    var Counter;        

    function pageLoad() {
        //Get total no. of CheckBoxes in side the GridView.

        //Get total no. of checked Select Product CheckBoxes in side the GridView.
        Counter = 0;
    }

    function HeaderClick(CheckBox) {
        //Get target base & child control.
        var TargetBaseControl = document.getElementById('<%= this.GrdQuotation.ClientID %>');
        var TargetChildControl = "cbApproved";

        //Get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");

        //Checked/Unchecked all the checkBoxes in side the GridView.
        for (var n = 0; n < Inputs.length; ++n)
            if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[n].checked = CheckBox.checked;

        //Reset Counter
        Counter = CheckBox.checked ? TotalChkBx : 0;
    }

    function ChildClick(CheckBox, HCheckBox) {
        //get target control.
        var HeaderCheckBox = document.getElementById(HCheckBox);
        
        //Modifiy Counter; 
        if (CheckBox.checked && Counter < TotalChkBx)
            Counter++;
        else if (Counter > 0)
            Counter--;

        //Change state of the header CheckBox.
        if (Counter < TotalChkBx)
            HeaderCheckBox.checked = false;
        else if (Counter == TotalChkBx)
            HeaderCheckBox.checked = true;
    }

</script>
<div id="right_data">
     <div>
     <table width="100%">
         <tr>
             <td>
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <ContentTemplate>
<TABLE><TBODY>
    <tr>
        <td align="left" colspan="2" style="WIDTH: 100px">
            <strong>
            <asp:Label ID="lblErrorApprove" runat="server" CssClass="lblbox" ForeColor="Green"></asp:Label>
            </strong>
        </td>
    
        <td style="HEIGHT: 25px">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="left" style="WIDTH: 100px">
            <strong>
            <asp:Label ID="Label17" runat="server" CssClass="lblbox" Text="Location" Width="67px"></asp:Label>
            </strong>
        </td>
        <td style="WIDTH: 100px">
            <asp:DropDownList ID="drpDistributor" runat="server" Width="200px" 
                AutoPostBack="True" 
                onselectedindexchanged="drpDistributor_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td style="HEIGHT: 25px">
        </td>
    </tr>
    <TR><TD style="WIDTH: 100px" align="left">
                                     <strong><asp:Label id="Label18" runat="server" Width="104px" Text="Customer" CssClass="lblbox"></asp:Label></strong></TD><TD style="WIDTH: 100px">
        <asp:DropDownList id="DrpCustomer" runat="server" Width="200px" AutoPostBack="True" 
                onselectedindexchanged="DrpCustomer_SelectedIndexChanged" >
                                     </asp:DropDownList></TD><TD style="HEIGHT: 25px"></TD></TR><TR>
    <TD vAlign=top align=left>
                                     &nbsp;</TD><TD align=left>&nbsp;</TD><TD align=left></TD></TR><TR><TD style="WIDTH: 100px; HEIGHT: 12px" valign="bottom" align=left>
                                     </TD><TD style="WIDTH: 100px; HEIGHT: 12px" align=left>
                                     <asp:Button id="btnGenerate" onclick="btnGenerate_Click" runat="server" Text="Approve" CssClass="Button" />
                                      </TD><TD style="HEIGHT: 25px" align=left></TD></TR></TBODY></TABLE>
</ContentTemplate>
                 </asp:UpdatePanel>
                 <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                     <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlApprove" runat="server" ScrollBars="Vertical" Width="100%" Height="400px">
                                    <asp:GridView ID="GrdQuotation" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" 
                                    ForeColor="SteelBlue" HorizontalAlign="Center" EnableModelValidation="True" onrowcreated="GrdQuotation_RowCreated">                                 
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
                                    </asp:Panel>
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
        <DIV style="Z-INDEX: 101; LEFT: 368px; WIDTH: 100px; POSITION: absolute; TOP: 100px; HEIGHT: 100px"><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                             <ProgressTemplate>
                                                 <asp:ImageButton ID="ImageButton1" runat="server" Height="23px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                                     Width="22px" />
                                                 Record Update
                                             </ProgressTemplate>
                                         </asp:UpdateProgress> </DIV>
           </div>
    </div>
</asp:Content>

