<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="ListReserveOrder.aspx.cs" Inherits="Intranet_WMS_ListReserveOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td style="text-align: center">
                <h4>
                    Reserve Stock
                </h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnew" runat="server" Text="New Reserve Order" Width="144px" Height="28px"
                                OnClick="btnew_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btreport" runat="server" Text="Reserved Stock Report" OnClick="btreport_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>

          <tr>
                <td align="center">
                    <fieldset style="color: #33CCFF; border-color: #0000FF" >
                        <legend>Search</legend>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbID" runat="server" Text="By ID" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbStatus" runat="server" Text="By Status" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbCustomer" runat="server" Text="By Customer" 
                                        GroupName="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                <asp:GridView ID="Gvreservestockorder" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" OnPageIndexChanging="Gvreservestockorder_PageIndexChanging" Width="100%"
                    OnRowCommand="Gvreservestockorder_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Button ID="btndoId" runat="server" Text='<%# Bind("do_id") %>' BackColor="Transparent"
                                    BorderColor="Transparent" ForeColor="Blue" CommandName="do_id" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expiry Date">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("effective_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("customer_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creation Date">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("creation_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" ForeColor="Red" CommandArgument="<%# Container.DataItemIndex %>"  CommandName="do_id" runat="server">
  <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
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
        <tr> <td align ="center">  <table> <tr> <td> 
                <asp:LinkButton ID="lbFirst" runat="server" onclick="lbFirst_Click" >First</asp:LinkButton>
                </td> <td> 
                    <asp:LinkButton ID="lbNext" runat="server" onclick="lbNext_Click" >Next</asp:LinkButton>
                </td> <td> 
                    <asp:LinkButton ID="lbPrev" runat="server" onclick="lbPrev_Click" >Prev</asp:LinkButton>
                </td> <td>  
                    <asp:LinkButton ID="lbLAst" runat="server" onclick="lbLAst_Click" >Last</asp:LinkButton>
                </td></tr></table></td></tr>
    </table>
</asp:Content>
