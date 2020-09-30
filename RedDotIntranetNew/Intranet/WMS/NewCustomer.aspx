<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="NewCustomer.aspx.cs" Inherits="Intranet_WMS_NewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 107px;
            text-align: left;
        }
        .style6
        {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="background-image: url('../images/bgimg.png');" align="center" width="100%">
        <tr>
            <td align="center">
                <h4>
                    <asp:Label ID="lbtitle" runat="server" Text="New Customer"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Button ID="btnback" runat="server" OnClick="btnback_Click" Text="back" />
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Style="text-align: center" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Customer Info</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Customer Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcustName" runat="server" Width="235px"></asp:TextBox>
                                <asp:HiddenField ID="hdcustid" runat="server" />
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label2" runat="server" Text="Cell"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                     
                        <tr>
                            <td class="style6">
                                <asp:Label ID="Label19" runat="server" Text="Contact"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcontact" runat="server" Width="231px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label18" runat="server" Text="Territory"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTerritory" runat="server" Width="237px"></asp:TextBox>
                            </td>
                        </tr>
                     
                        <tr>
                            <td class="style6">
                                <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label17" runat="server" Text="Fax">   </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                      
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label8" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label11" runat="server" Text="Telephone2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone2" runat="server" Height="22px" Width="235px"></asp:TextBox>
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
                                <asp:Label ID="Label4" runat="server" Text="Address2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" Height="22px" Width="235px"></asp:TextBox>
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
                                <asp:Label ID="Label6" runat="server" Text="Address4"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress4" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                      
                            <td class="style1">
                                <asp:Label ID="Label12" runat="server" Text="Post1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpost1" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label13" runat="server" Text="Post2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpost2" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label14" runat="server" Text="Post3"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPost3" runat="server" Width="236px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label15" runat="server" Text="Post4"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpost4" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label16" runat="server" Text="Post5"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpost5" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style6">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
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
