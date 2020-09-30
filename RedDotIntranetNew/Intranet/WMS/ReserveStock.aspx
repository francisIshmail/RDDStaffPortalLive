<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="ReserveStock.aspx.cs" Inherits="Intranet_WMS_ReserveStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    Reserve Stock</h4>
            </td>
        </tr>
        <tr>
        <td align="center">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Reserve" OnClick="Button1_Click" />
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" />
                    </td>
                </tr>
            </table>
            </td> 
        </tr>
        <tr>
            <td align ="center">
                <asp:Label ID="lbmsg" runat="server" Text="Invalid Reserved quantity" 
                    ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Quantity In Stock:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyInStock" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Quantity Available:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAvailable" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Quantity Allocated:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAllocated" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Quantity to Reserve:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyToReserve" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Quantity Reserved:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyReserved" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvReserveStock" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" Width="100%" OnPageIndexChanging="gvReserveStock_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="Lbdescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                <asp:HiddenField ID="hdwarehouse" runat="server" Value='<%# Bind("warehouse_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="lblocation" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                                <asp:HiddenField ID="hdlocation" runat="server" Value='<%# Bind("location_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOE">
                            <ItemTemplate>
                                <asp:Label ID="loboedid" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Allocated">
                            <ItemTemplate>
                                <asp:Label ID="lballoacted" runat="server" Text='<%# Bind("allocd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reserved">
                            <ItemTemplate>
                                <asp:Label ID="lbreserved" runat="server" Text='<%# Bind("reserved") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="price">
                            <ItemTemplate>
                                <asp:Label ID="lbprice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                                <asp:HiddenField ID="hdprice" runat="server" Value='<%# Bind("price") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COO">
                            <ItemTemplate>
                                <asp:Label ID="lbcoo" runat="server" Text='<%# Bind("country_name") %>'></asp:Label>
                                <asp:HiddenField ID="hdcoo" runat="server" Value='<%# Bind("country_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
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
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
