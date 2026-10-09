<%@ Page Title="Fast Services: Documents" Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="frmDocument.aspx.cs" Inherits="Forms_frmDocument" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" Runat="Server">
<script type="text/javascript">
    $(document).ready(function () {
        var hfUpload = document.getElementById('<%=hfUpload.ClientID%>').value;
        if (hfUpload == '1') {
            $(".Documnt").show();
        }
        else {
            $(".Documnt").hide();
        }
        $(".DocuemntParagraph").live("click", function () {
            if (hfUpload == 1) {
                document.getElementById('<%=hfUpload.ClientID%>').value = 0;
            }
            else {
                document.getElementById('<%=hfUpload.ClientID%>').value = 1;
            }
            $(this).next(".Documnt").slideToggle(600);
        });
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" Runat="Server">
<div id="right_data">

<table cellspacing="0" cellpadding="3" border="0" width="80%">
       
       <tr>
        <td>
            <asp:Literal ID="ltrlDoc" runat="server"></asp:Literal>
        </td>
       </tr>
       <tr>
                <td>
            <p class="DocuemntParagraph" >&nbsp&nbsp;Upload Document</p>
    		<div class="Documnt">
            <br />
            <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                    <td><strong>Title </strong>&nbsp;<span style="color: #C60000; font-weight: bold">*</span></td>
                    <td>&nbsp;</td>
                <td>
                   <strong> Document Type</strong>
                </td>
                <td>&nbsp;</td>
                <td>
                    &nbsp;
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
                    <tr>
                    <td><asp:TextBox ID="txtDocTitle" Width="200" runat="server"></asp:TextBox> </td>
                    <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="ddlDoc" runat="server">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>
                    <asp:FileUpload ID="fuDoc" runat="server" Width="400px"/>
                </td>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnAddDoc" runat="server" Text="Save" onclick="btnAddDoc_Click" CssClass="Button"/></td>
            </tr>
                     </table>
            
            </div>
                </td>
            </tr>
        <tr>
            <td>
                <asp:Panel ID="pnlFilter" runat="server" GroupingText="Filtered By">
                        <table width="100%" cellpadding="3">
                            <tr>
                                <td width="120">
                                    <strong>Name:</strong>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" Width="149px"></asp:TextBox>
                                </td>
                                <td width="120">
                                    <strong>Type:</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" CssClass="Button" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
            </td>
        </tr>
        <tr>
           <td>
           <asp:UpdatePanel ID="upDoc" runat="server">
<ContentTemplate>
                    <asp:GridView ID="gvDocument" runat="server" AutoGenerateColumns="False" CellPadding="2"
                        OnRowCommand="gvDocument_RowCommand" Width="100%" 
                        DataKeyNames="NAME" GridLines="Horizontal" 
                        
                        EmptyDataText="<div style='padding:10px; margin:10px; background-color:#fbefef; border: Solid 1px #bb0000; color:#bb0000;'>There is no document to display.</div>" 
                        onrowdatabound="gvDocument_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Document">
                            <HeaderStyle HorizontalAlign="Left"/>

                                <ItemTemplate>
                                    <asp:LinkButton AlternateText="View" ID="lnkViewDoc" runat="server" CommandName="view" CommandArgument='<%# Eval("DOCUMENT_ID")%>'>
                                        <%# Eval("TITLE")%>, <%# Eval("NAME")%>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Doc Type">
                            <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("SLASH_DESC")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Uploaded By">
                             <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("USER_NAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DateTime">
                                <HeaderStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <%# Eval("TIME_STAMP")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>


                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:ImageButton AlternateText="View" ToolTip="View Document" ID="btnViewDoc" runat="server"
                                                  ImageUrl="~/images/view.gif" CommandName="view" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />                                                  
                                            </td>
                                            <td width="2%">&nbsp;</td>
                                            <td>
                                                <asp:ImageButton AlternateText="Delete" ToolTip="Delete Document" ID="btnDel" runat="server"
                                                    ImageUrl="~/images/delete.gif" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to permanently delete this document?');"
                                                    CommandName="del" CommandArgument='<%# Eval("DOCUMENT_ID")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <HeaderStyle BackColor="#006699" ForeColor="White"/>
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>

                    
</ContentTemplate>
</asp:UpdatePanel>
                </td>
        </tr>
        <tr>
            <td><asp:HiddenField ID="hfUpload" runat="server" /> </td>
        </tr>
        
        </table>

</div>
</asp:Content>

