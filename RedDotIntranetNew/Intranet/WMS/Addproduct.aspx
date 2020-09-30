<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Addproduct.aspx.cs" Inherits="Intranet_WMS_Addproduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    <asp:Label ID="lbtitle" runat="server" Text="NEW Product"></asp:Label></h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                                <asp:HiddenField ID="hdstockID" runat="server" />
                <asp:Label ID="Lbmsg" runat="server" ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Product Info</legend>
                    <table>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Text="Part Number "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPartNumber" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style6">
                                <asp:Label ID="Label9" runat="server" Text="Hscode"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHscode" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Price">   </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtprice" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label11" runat="server" Text="Gross Weight"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrossWeight" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label4" runat="server" Text="Length"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLength" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label3" runat="server" Text="width"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWidth" runat="server" Width="233px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label6" runat="server" Text="Height"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtHeight" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label12" runat="server" Text="Category ID"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcategory" runat="server" Width="233px"></asp:TextBox>
                            </td>
                            <td class="style6">
                                <asp:Label ID="Label13" runat="server" Text="Product Line "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtProductLine" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label5" runat="server" Text="Supplier"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddplSuppplier" runat="server" Width="235px">
                                </asp:DropDownList>
                            </td>
                            <td class="style6">
                                &nbsp;
                                <asp:Label ID="Label14" runat="server" Text="Pack ID"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpackid" runat="server" Width="235px"></asp:TextBox>
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
