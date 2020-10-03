<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="AddWarehouse.aspx.cs" Inherits="Intranet_WMS_AddWarehousel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    <asp:Label ID="lbtitle" runat="server" Text="NEW Warehouse"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Button ID="btnback" runat="server" Text="bacK" OnClick="btnback_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                                <asp:HiddenField ID="hdwarehouseID" runat="server" />
                <asp:Label ID="lbmsg" runat="server" Text="" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Warehouse Info</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Warehouse  Code"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWarehouseCode" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style1">
                                <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label11" runat="server" Text="Warehouse Evo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEvo" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style1">
                                <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSatus" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
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
