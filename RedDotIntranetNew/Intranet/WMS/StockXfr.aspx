<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="StockXfr.aspx.cs" Inherits="Intranet_WMS_StockXfr" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="background-image: url('../images/bgimg.png');" align="center" width="100%">
        <tr>
            <td align="center">
                <h4>
                    Stock XFR
                </h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Transfer" OnClick="Button1_Click" />
                        </td>
                        <td width="10%">
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Cancel" OnClick="Button2_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="Lbmsg" runat="server" Text="" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lb1" runat="server" Text="Part#:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpart" runat="server" Width="205px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lb2" runat="server" Text="Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="251px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lb3" runat="server" Text="BOE:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtboe" runat="server" Height="22px" Width="213px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lb4" runat="server" Text="Stock ID"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStockid" runat="server" Height="22px" Width="259px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="From Warehouse:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtwarehouse" runat="server" Height="22px" Width="213px"></asp:TextBox>
                            <asp:HiddenField ID="hdwarehouse" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="To Warehouse:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dplwarehouse" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="From Location:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtlocation" runat="server" Height="22px" Width="213px"></asp:TextBox>
                            <asp:HiddenField ID="hdlocation" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="To Location:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="dplLocation" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Qty Available:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAvail" runat="server" Height="22px" Width="213px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label12" runat="server" Text="Qty To Transfer:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyTransfer" runat="server" Width="182px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="comments:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcomment" runat="server" Height="62px" Width="491px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </talbe>
</asp:Content>
