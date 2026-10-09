<%@ Page Title="Fast Services: Salary Letter To Bank" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmLetterToBank.aspx.cs" Inherits="Forms_frmLetterToBank" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
    <div id="right_data">
        <asp:UpdatePanel runat="server" ID="upLetter">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width:5%">
                            &nbsp; 
                        </td>
                        <td style="width:95%" valign="top">
                            <table width="70%">
                    <tr>
                        <td style="width:100%" colspan="2" align="center">
                            <strong><asp:Label runat="server" ID="lblCompany" Text="FAST INTELLIGENT SOLUTIONS (SMC-PVT) LTD."></asp:Label></strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;padding-left:450px;" colspan="2" align="center">
                            Ref # <asp:TextBox runat="server" ID="txtRefNo" Text="FS/E/B-09-009"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;padding-left:450px;" colspan="2" align="center">
                            Date
                            <asp:TextBox runat="server" ID="txtDate" AutoPostBack="True" 
                                ontextchanged="txtDate_TextChanged"></asp:TextBox>
                            <asp:ImageButton id="ibDate" runat="server" Width="16px" ImageUrl="~/App_Themes/Granite/Images/date.gif">
                            </asp:ImageButton>
                            <cc1:CalendarExtender id="CEDate" runat="server" TargetControlID="txtDate" PopupButtonID="ibDate" Format="dd-MMM-yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            <asp:Label runat="server" ID="lblDate" Text="The Manager,"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            <asp:DropDownList runat="server" ID="ddlBank" Width="200px"></asp:DropDownList>,
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            <asp:TextBox runat="server" ID="txtAddress1" Text="Cavalry Ground," Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            <asp:TextBox runat="server" ID="txtAddress2" Text="Lahore." Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;padding-left:200px;" colspan="2">
                            <strong><u>Subject: Salary Transfer Letter</u></strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            Dear Sir,
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            We are maintaining account in the name of M/S Fast Intelligent Solutions (SMC-PVT) LTD in
                            <br/>
                            your branch having current account number 0010025849180015 in PKR. 
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            We request you to kindly Credit salary of our staff to their accounts vide Cheque No. 
                            <asp:TextBox ID="txtChequeNo" runat="server"></asp:TextBox> . The 
                            <br />
                            detail of staff with account details is mentioned below: 
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%;padding-left:30px" colspan="2">
                            <asp:GridView id="gvEmployee" runat="server" Width="90%" Font-Size="9pt"  
                            AutoGenerateColumns="False" HorizontalAlign="Center">
                            <Columns>
                                <asp:BoundField DataField="PEOPLE_ID" HeaderText="PEOPLE_ID">
                                    <HeaderStyle CssClass="HidePanel" />
                                    <ItemStyle CssClass="HidePanel" />
                                </asp:BoundField>
                                <asp:BoundField DataField="GL_REMARKS" HeaderText="Name Of Employee">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left"/>
                            </asp:BoundField>
                                <asp:BoundField DataField="ACCOUNT_NO" HeaderText="Account No">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BranchCode" HeaderText="Branch Code">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>                                
                            <asp:BoundField DataField="Salary" HeaderText="Salary" DataFormatString="{0:0.00}">
                            <ItemStyle BorderColor="Silver" BorderWidth="1px" BorderStyle="Solid" ></ItemStyle>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>                                
                            </Columns>
                            <HeaderStyle CssClass="tblhead"></HeaderStyle>
    
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            Regards,
                            <br />
                            For Fast Intelligent Solutions (SMC-PVT) LTD.
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width:100%" colspan="2">
                            <asp:TextBox runat="server" ID="txtSender" Text="Farooq Aslam"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br/>
        <table width="100%">
            <tr>
                <td style="width:10%">
                    
                </td>
                <td style="width:90%">
                    <asp:Button ID="btnViewPDF" runat="server" CssClass="Button" Text="View PDF" 
                        Width="90" onclick="btnViewPDF_Click"/>
                    <asp:Button ID="btnViewExcel" runat="server" CssClass="Button" 
                        Text="View Excel" Width="90" onclick="btnViewExcel_Click"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

