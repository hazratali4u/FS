<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmRoleManagement.aspx.cs" Inherits="frmRoleManagement" Title="Role Management" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphPage" runat="Server">
<script language="javascript" type="text/javascript">
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
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 82px; height: 17px">
                                            <asp:Label ID="lblmsg" runat="server" Visible="False" Width="114px" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="height: 17px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label1" runat="server" Width="107px" Text="Role Description" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddRole" runat="server" Width="200px" CssClass="DropList" OnSelectedIndexChanged="ddRole_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TextBox1" runat="server" Visible="False" Width="195px" CssClass="txtBox "
                                                Enabled="False"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNew" OnClick="btnNew_Click" runat="server" Width="55px" Font-Size="8pt"
                                                Text="New" CssClass="Button" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label11" runat="server" Width="112px" Text="Module 1st Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule1stLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule1stLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px">
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Width="112px" Text="Module 2nd Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule2ndLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule2ndLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 16px">
                                            <strong>
                                                <asp:Label ID="Label3" runat="server" Width="112px" Text="Module 3rd Layer" CssClass="lblbox"></asp:Label></strong>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DrpModule3rdLayer" runat="server" Width="200px" CssClass="DropList"
                                                OnSelectedIndexChanged="DrpModule3rdLayer_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>
                                                <asp:Label ID="Label4" runat="server" Width="142px" Text="Module Assignment" CssClass="lblbox"></asp:Label></strong>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 182px" colspan="2">
                                            <table>
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 100px" rowspan="4">
                                                            <asp:ListBox ID="lstUnAssignModule" runat="server" Width="150px" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 102px" rowspan="4">
                                                            <asp:ListBox ID="lstAssignModule" runat="server" Width="150px" Height="200px" CssClass="DropList">
                                                            </asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnAssign" runat="server" Width="30px" Font-Size="8pt" Text=">>"
                                                                OnClick="btnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Button ID="btnUnAssign" runat="server" Width="30px" Font-Size="8pt" Text="<<"
                                                                OnClick="btnUnAssign_Click" CssClass="Button" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 75px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="1">
                                                        </td>
                                                        <td style="width: 75px; height: 24px">
                                                        </td>
                                                        <td rowspan="1">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" width="10%">
                    <asp:Button ID="btnReport" runat="server" Text="View Report" OnClick="btnReport_Click"
                        Width="120px" CssClass="Button" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
