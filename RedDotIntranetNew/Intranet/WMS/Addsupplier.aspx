<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Addsupplier.aspx.cs" Inherits="Intranet_WMS_Addsupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: left;
        }
        .style2
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
                    <asp:Label ID="lbtitle" runat="server" Text="NEW Suppier"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Button ID="btnback" runat="server" Text="Back" OnClick="btnback_Click" />
            </td>
        </tr>
        <tr>
            <asp:HiddenField ID="hdsupplierid" runat="server" />
                <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Supplier Info</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Supplier  Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSupplierName" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style1">
                                <asp:Label ID="Label2" runat="server" Text="Contact Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcontact" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label11" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style1">
                                <asp:Label ID="Label4" runat="server" Text="Cell"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label8" runat="server" Text="Fax">   </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
