<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="BOelist.aspx.cs" Inherits="Intranet_WMS_BOelist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%" class="style1" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td class="style2" align="center">
                <h4>
                    Bills Of Entry</h4>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
                <td align="center">
                    <asp:Label ID="lbmsg" runat="server" Text="" ForeColor="Red" ></asp:Label>
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
                                    <asp:RadioButton ID="rdbsupplier" runat="server" Text="By Supplier" 
                                        GroupName="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" >
                                    <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btmSearch" runat="server" Text="Search" OnClick="btmSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        <tr>


            <td align="center">
                <asp:GridView ID="Gvboes" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" OnPageIndexChanging="Gvboes_PageIndexChanging" 
                    onrowcommand="Gvboes_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Prealert id">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("prealert_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOE">
                            <ItemTemplate>
                                <asp:Label ID="lbBoeID" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Arrival Date">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("actual_arrival_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("delivery_method") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Supplier">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipping Reference">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("shipping_reference") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("total_quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandArgument="<%# Container.DataItemIndex %>"  CommandName="view" runat="server" ForeColor="Red"> <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
                               <asp:LinkButton ID="lbdelete" runat="server" OnClientClick="javascript:return confirm('Are you sure to proceed?');" CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>"
                                        ForeColor="Red"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="center" />
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
</asp:Content>
