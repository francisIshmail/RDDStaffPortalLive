<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="StockAdj.aspx.cs" Inherits="Intranet_WMS_StockAdj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    Stock Adj</h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnAdjust" runat="server" Text="Adjust" OnClick="btnAdjust_Click" />
                        </td>
                        <td width="20%">
                        </td>
                        <td>
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="style1">
                <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Part#:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPart" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Description:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="276px" ReadOnly="True"></asp:TextBox>
                            <asp:HiddenField ID="Hddescription" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="BOE:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBoe" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lbstock" runat="server" Text="Stock ID:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStock" runat="server" Width="276px" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Warehouse:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWarehouse" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Qty Available:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAvailable" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Adjustment Qty:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAdjustedQty" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="left">
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="From Location:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtlocation" runat="server" ReadOnly="True"></asp:TextBox>
                            <asp:HiddenField ID="hdlocation" runat="server" />
                        </td>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Comments"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtcomment" runat="server" Height="99px" Width="421px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
