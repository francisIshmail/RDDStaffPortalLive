<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="WMSAdmin.aspx.cs" Inherits="Intranet_WMS_WMSAdmin" %>

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
    <table class="style1" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td style="text-align: center">
                <h4>
                    System Administration
                </h4>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table class="style1">
                    <tr>
                        <td class="style2">
                            <asp:Button ID="btcustomer" runat="server" Text="Customer" Height="26px" Width="172px"
                                OnClick="btcustomer_Click" />
                        </td>
                        <td class="style2">
                            <asp:Button ID="btproduct" runat="server" Text="Product" Height="26px" Width="172px"
                                OnClick="btproduct_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Button ID="btFrForwarder" runat="server" Text="Freight Forwarders " Height="26px"
                                Width="172px" OnClick="btFrForwarder_Click" />
                        </td>
                        <td class="style2">
                            <asp:Button ID="btWarehouses" runat="server" Text="Warehouses" Height="26px" Width="172px"
                                OnClick="btWarehouses_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Button ID="BtUser" runat="server" Text="Users" Height="26px" Width="172px" OnClick="Users_Click" />
                        </td>
                        <td class="style2">
                            <asp:Button ID="btAuthorisations" runat="server" Text="Authorisations" Height="26px"
                                Width="172px" OnClick="btAuthorisations_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:Button ID="btSuppliers" runat="server" Text="Suppliers" Height="26px" Width="172px"
                                OnClick="btSuppliers_Click" />
                        </td>
                        <td class="style2">
                            <asp:Button ID="btcountry" runat="server" Text="Countries of Origin" Height="26px"
                                Width="172px" OnClick="btcountry_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
