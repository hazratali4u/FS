<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmEnquiry.aspx.cs" Inherits="Forms_frmEnquiry" Title="Fast Services: Add Enquiry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
<script language="JavaScript" type="text/javascript">
    
    function pageLoad() {
        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
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
    <asp:UpdatePanel ID="upProgress" runat="server">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
        <div>
        <cc1:ModalPopupExtender ID="ModalPopupExtender" runat="server" BackgroundCssClass="modalBackground"
            CancelControlID="CancelButton" DropShadow="true" OkControlID="dummyLink" PopupControlID="pnlCustomer" TargetControlID="dummyLink" BehaviorID="ModelPopup">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlCustomer" runat="server" Style="display: none;background-color:White;" ScrollBars="Auto" Width="500px" Height="200px">
        <br />
        <br />
        <br />
        <center>
        <strong>This Customer already has Enquiry for this Category.</strong>
        <br />
        <br />
        <br />
        <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="Button" OnClick="btnContinue_Click"/>
        <asp:Button ID="CancelButton" runat="server" Text="Close" CssClass="Button" />
        <a href="#" style="display:none;visibility:hidden;" onclick="return false" id="dummyLink" runat="server">dummy</a>
        </center>
        </asp:Panel>

            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="5">
                                                <asp:Literal ID="ltrlEnquiry" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label9" runat="server" Text="Location" CssClass="lblbox" Width="87px"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="206px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left" style="width: 15px;">
                                            </td>
                                            <td style="width: 99px; height: 18px;" align="left">
                                                &nbsp;
                                            </td>
                                            <td align="left" style="height: 18px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label6" runat="server" Width="76px" Text="Customer" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="DrpCustomer" runat="server" Width="206px" CssClass="DropList"
                                                    AutoPostBack="True" OnSelectedIndexChanged="DrpCustomer_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px;" align="left">
                                                &nbsp;
                                            </td>
                                            <td style="height: 20px" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label10" runat="server" Width="110px" Text="Enquiry Originator" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="ddlOriginator" runat="server" CssClass="DropList" Width="206px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td style="width: 99px" align="left">
                                                &nbsp;
                                            </td>
                                            <td style="height: 20px" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="110px" Text="Category" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:DropDownList ID="ddlCategotry" runat="server" Width="206px" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCategotry_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                &nbsp;
                                            </td>
                                            <td style="width: 99px;" align="left">
                                                &nbsp;
                                            </td>
                                            <td style="height: 20px" align="left">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 25px" align="left" valign="top">
                                                <strong>
                                                    <asp:Label ID="lblRemarks" runat="server" Width="76px" Text="Remarks" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 20px" align="left">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtBox " Rows="5" TextMode="MultiLine"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td style="width: 99px; height: 20px;" align="left" valign="top">
                                                &nbsp;
                                            </td>
                                            <td style="height: 20px" align="left" valign="top">
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                &nbsp;&nbsp;
                                <asp:TextBox ID="txtTotalDebit" runat="server" Width="100px" CssClass="HidePanel"></asp:TextBox><asp:TextBox
                                    ID="txtTotalCredit" runat="server" Width="73px" CssClass="HidePanel"></asp:TextBox>
                                &nbsp; &nbsp; &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="2">
                                    <tbody>
                                        <tr>
                                            <td style="height: 16px;">
                                                <asp:Label ID="lblskuname" runat="server" Width="350px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Product Name" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblquantity" runat="server" Width="50px" Height="16px" ForeColor="White"
                                                    Font-Bold="True" Text="Quantity" BackColor="#006699"></asp:Label>
                                            </td>
                                            <td style="height: 16px" align="center">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 350px;">
                                                <asp:DropDownList ID="ddlProduct" runat="server" Width="350">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 50px;">
                                                <asp:TextBox ID="txtQuantity" onfocus="SearchedCode()" runat="server" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button AccessKey="A" ID="btnSave" OnClick="btnSave_Click" runat="server" Font-Size="8pt"
                                                    Text="Add Product" Width="95px" ValidationGroup="vg" CssClass="Button" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="9">
                                                <asp:Panel ID="Panel2" runat="server" Height="130px" ScrollBars="Vertical" BorderWidth="1px"
                                                    BorderStyle="Groove" BorderColor="Silver">
                                                    <asp:GridView ID="GrdPurchase" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="White" ForeColor="SteelBlue" HorizontalAlign="Center" OnRowDeleting="GrdPurchase_RowDeleting"
                                                        OnRowEditing="GrdPurchase_RowEditing" ShowHeader="False">
                                                        <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                                            PreviousPageText="Previous" />
                                                        <RowStyle ForeColor="Black" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SKU_ID" HeaderText="SKU_ID">
                                                                <HeaderStyle CssClass="HidePanel" />
                                                                <ItemStyle CssClass="HidePanel" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="SKU_NAME" HeaderText="Product Name">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" Width="350px" BorderWidth="1px"
                                                                    HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="50px" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="DOCUMENT_DATE" HeaderText="" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle BorderColor="Silver" BorderStyle="Solid" BorderWidth="2px" HorizontalAlign="Right"
                                                                    Width="50px" />
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
                                                            <asp:BoundField DataField="ENQUIREY_DETAIL_ID2" HeaderText="ENQUIREY_DETAIL_ID2">
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
                                <asp:Button AccessKey="S" ID="btnSaveDocument" runat="server" Font-Size="8pt" Text="Save" UseSubmitBehavior="False" OnClick="btnSaveDocument_Click" CssClass="Button" />
                                <asp:Button AccessKey="C" ID="btnCancel" runat="server" Font-Size="8pt" Text="Cancel"
                                    UseSubmitBehavior="False" OnClick="btnCancel_Click" CssClass="Button" />                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
