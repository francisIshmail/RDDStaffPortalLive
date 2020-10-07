<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="StockbyBoe.aspx.cs" Inherits="Intranet_WMS_StockbyBoe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 85px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4 style="text-align: center">
                    Stock by BOE</h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table align="center">
                    <tr>
                        <td>
                            <asp:Button ID="btnadj" runat="server" Text="ADJ" OnClick="btnadj_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnxfr" runat="server" Text="XFR" OnClick="btnxfr_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" width="100%">
                    <tr>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Text="warehouse:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWarehouse" runat="server" Width="275px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lbpart" runat="server" Text="part#:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPart" runat="server" Width="275px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lbstock" runat="server" Text="Stock Id:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" Width="275px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:Label ID="lbdesc" runat="server" Text="Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="275px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvStockbyboe" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="BOE">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Allocd">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("SumOfquantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lbprice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Creation Date">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("creation_date") %>'></asp:Label>
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
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
