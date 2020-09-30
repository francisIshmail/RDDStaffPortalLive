<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="AddFreightForwarders.aspx.cs" Inherits="Intranet_WMS_AddFreightForwarders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    <asp:Label ID="lbtitle" runat="server" Text="New Freight Forwarder"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" Text="Back" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="Lbmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Freight Forwarder Info</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Freight Forwarder Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFreightFowarderName" runat="server" Width="235px"></asp:TextBox>
                                <asp:HiddenField ID="hdfreight" runat="server" />
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label2" runat="server" Text="Contact"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcontact" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style6">
                                <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Fax">   </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label11" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label4" runat="server" Text="Cell"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label3" runat="server" Text="Address1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Width="233px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label6" runat="server" Text="Address2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label5" runat="server" Text="Address3"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress3" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
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
            <td align="left" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
        
    </table>
</asp:Content>
