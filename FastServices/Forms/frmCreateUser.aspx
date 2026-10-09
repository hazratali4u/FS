<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCreateUser.aspx.cs" Inherits="Forms_frmCreateUser" Title="Fast Services: User Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script language="JavaScript" type="text/javascript">
        function ValidateForm() {
            var str;

            str = document.getElementById('<%=txtLoginId.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Login Id');
                return false;
            }
            str = document.getElementById('<%=txtpassword.ClientID%>').value;
            if (str == null || str.length == 0) {
                alert('Must enter Password');
                return false;
            }
            return true;
        }

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
            <table width="100%">
                <tr>
                    <td style="width: 100px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Literal ID="ltrlUserLogin" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="lbldesignationID" runat="server" Width="97px" Text="Base Location"
                                                        CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddDistributorId" runat="server" Width="205px" CssClass="DropList"
                                                    OnSelectedIndexChanged="ddDistributorId_SelectedIndexChanged" AutoPostBack="True">
                                                </asp:DropDownList>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px; height: 18px">
                                                <strong>
                                                    <asp:Label ID="Label1" runat="server" Width="69px" Text="Name" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td style="height: 18px">
                                                <asp:DropDownList ID="DrpUser" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="lblNickName" runat="server" Width="68px" Text="Roles" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddRole" runat="server" Width="205px" CssClass="DropList">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="Label3" runat="server" Width="82px" Text="Login Id" CssClass="lblbox"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLoginId" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>
                                                    <asp:Label ID="Label2" runat="server" Width="69px" Text="Password"></asp:Label></strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtpassword" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>Email Account</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmailAccount" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px">
                                                <strong>Account Password</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAccountPassword" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                <strong>SMTP Address</strong>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSMTP" runat="server" Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 98px">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkIsActive" runat="server" Checked="True" Text="IsActive" Width="93px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" style="width: 98px">
                                            </td>
                                            <td>
                                                &nbsp;
                                                <asp:Button ID="btnSave" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnSave_Click"
                                                    Text="Save" ValidationGroup="vg" Width="82px" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Font-Size="8pt" OnClick="btnCancel_Click"
                                                    Text="Cancel" Width="73px" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                    
                    <table class="tblhead">
                        <tbody>
                            <tr>
                                <td style="color: White; font-weight: bold;">
                                    <asp:Label ID="Label10" runat="server" Width="153px" Text="Select Searching Type"></asp:Label>
                                </td>
                                <td style="width: 170px; height: 21px" align="left">
                                    <asp:DropDownList ID="ddSearchType" runat="server" Width="200px" CssClass="DropList">
                                        <asp:ListItem Value="SKU_code">All Records</asp:ListItem>
                                        <asp:ListItem Value="USER_NAME">User Name</asp:ListItem>
                                        <asp:ListItem Value="LOGIN_ID">Login</asp:ListItem>
                                        <asp:ListItem Value="role_name">Role</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 224px; height: 21px" align="left">
                                    <asp:TextBox ID="txtSeach" runat="server" Width="200px" CssClass="txtBox "></asp:TextBox>
                                </td>
                                <td style="width: 250px; height: 21px" align="left">
                                    <asp:Button ID="btnFilter" runat="server" Width="85px" Font-Size="8pt" Text="Filter"
                                        OnClick="btnFilter_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="250px" ScrollBars="Vertical">
                        <asp:GridView ID="Grid_users" runat="server" Width="99%" ForeColor="SteelBlue" CssClass="gridRow2"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="White" BorderColor="White"
                            OnPageIndexChanging="Grid_users_PageIndexChanging" OnRowEditing="Grid_users_RowEditing"
                            OnRowDataBound="Grid_users_RowDataBound">
                            <PagerSettings FirstPageText="" LastPageText="" Mode="NextPrevious" NextPageText="Next"
                                PreviousPageText="Previous"></PagerSettings>
                            <Columns>
                                <asp:BoundField DataField="USER_ID" HeaderText="User Id">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Distributor_ID" HeaderText="Distributor">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel "></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USER_NAME" HeaderText="Name">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="LOGIN_ID" HeaderText="Login">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PASSWORD" HeaderText="Password">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="role_name" HeaderText="Role">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IS_ACTIVE" HeaderText="Status">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ROLE_ID" HeaderText="ROLE_ID">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_ACCOUNT" HeaderText="EMAIL_ACCOUNT">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EMAIL_PASSWORD" HeaderText="EMAIL_PASSWORD">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SMTP_ADDRESS" HeaderText="SMTP_ADDRESS">
                                    <HeaderStyle CssClass="HidePanel"></HeaderStyle>
                                    <ItemStyle CssClass="HidePanel"></ItemStyle>
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Edit">
                                    <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                </asp:CommandField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="tblhead">
                            </HeaderStyle>
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
