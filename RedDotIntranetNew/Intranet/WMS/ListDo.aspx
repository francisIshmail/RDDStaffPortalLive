<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="ListDo.aspx.cs" Inherits="Intranet_WMS_ListDo" %>

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
            <td class="style2">
                <h4>
                    Delivery Orders
                </h4>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="Btnewdo" runat="server" Text="New Do" OnClick="Btnewdo_Click" />
            </td>
        </tr>
         <tr>
                <td align="center">
                    <fieldset style="color: #33CCFF; border-color: #0000FF" >
                        <legend>Search</legend>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbDO" runat="server" Text="By DO" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbStatus" runat="server" Text="By Status" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbRelease" runat="server" Text="By Release #" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbinvoice" runat="server" Text="By Invoice" 
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
                <asp:GridView ID="Gvdolist" runat="server" BackColor="White" BorderColor="#3366CC"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="Gvdolist_PageIndexChanging" Width="100%" OnRowCommand="Gvdolist_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <asp:Button ID="btndo_id" runat="server" Text='<%# Bind("do_id") %>' BackColor="Transparent"
                                    BorderColor="Transparent" ForeColor="Blue" CommandName="view" CommandArgument="<%# Container.DataItemIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Release#">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("release_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice#">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("invoice_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("customer_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shipping Reference">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("shipping_reference") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Freight Forwarder">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("freight_forwarder_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" ForeColor="Red" CommandArgument="<%# Container.DataItemIndex %>"  CommandName="view" runat="server">  <img src="../images/edit.png" title="Edit"/>
</asp:LinkButton>
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
