<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="List_Prealert.aspx.cs" Inherits="Intranet_WMS_List_Prealert" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center" style="background-image: url('../images/bgimg.png');">
        <table align="center" width="100%">
            <tr>
                <td align="center">
                    <table align="center">
                        <tr>
                            <td align="center">
                                &nbsp;
                            </td>
                            <td align="center">
                                <h3>
                                    List Prealert</h3>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trMessage" runat="server" visible="false">
                <td align="center">
                    <asp:Label ID="lbmsg" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="Button1" runat="server" Text="New Prealeart" OnClick="Button1_Click" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <fieldset style="color: #33CCFF; border-color: #0000FF">
                        <legend>Search</legend>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbPrealert" runat="server" Text="By Prealert" GroupName="Search" />
                                    <asp:RadioButton ID="rdbBoe" runat="server" Text="By BOE" GroupName="Search" />
                                    <asp:RadioButton ID="rdbstatus" runat="server" Text="By Status" GroupName="Search" />
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" ></asp:TextBox>
                                    <asp:Button ID="btmSearch" runat="server" Text="Search" OnClick="btmSearch_Click" />
                                </td>
                                <td><asp:Label ID="lblRowCount" runat="server" Text="Rows : 0" ForeColor="#cc0066"></asp:Label></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvListprealert" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                        CellPadding="4" Width="100%" OnPageIndexChanging="GvListprealert_PageIndexChanging"
                        OnRowCommand="GvListprealert_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                   <%-- <asp:Button ID="BtPrealert_id" runat="server" Text='<%# Bind("Prealert_id") %>' BackColor="Transparent"
                                        BorderColor="Transparent" ForeColor="#33CCFF" CommandName="prealert" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                    <asp:LinkButton ID="lnkBtPrealert_id" runat="server" Text='<%# Bind("Prealert_id") %>' ForeColor="#33CCFF" CommandName="prealert" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Eta">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("eta","{0:dd/mm/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supplier Reference">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("supplier_reference") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Forwarder">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("freight_forwarder_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BOE">
                                <ItemTemplate>

                                <%--    <asp:Button ID="BtBoe_id" runat="server" Text='<%# Bind("Boe_id") %>' BackColor="Transparent"
                                        BorderColor="Transparent" ForeColor="#33CCFF" CommandName="boe" CommandArgument="<%# Container.DataItemIndex %>" />--%>
                                    
                                    <asp:LinkButton ID="lnkBtBoe_id" runat="server" Text='<%# Bind("Boe_id") %>' ForeColor="#33CCFF" CommandName="boe" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                  
                                 <asp:LinkButton ID="lbview" runat="server"   CommandName="prealert" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Red"><img src="../images/edit.png" title="Edit" alt =""/>
                                  </asp:LinkButton>
                                    
                                 <asp:LinkButton ID="lbdelete" runat="server" OnClientClick="javascript:return confirm('Are you sure to proceed?');" CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>"
                                   ForeColor="Red">  <img src="../images/DeleteRed.png" title="Delete" alt =""/>

                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click">First</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click">Next</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbPrev" runat="server" OnClick="lbPrev_Click">Prev</asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="lbLAst" runat="server" OnClick="lbLAst_Click">Last</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
