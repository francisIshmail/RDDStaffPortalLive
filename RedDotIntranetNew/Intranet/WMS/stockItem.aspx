<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="stockItem.aspx.cs" Inherits="Intranet_WMS_stockItem" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td style="text-align: center">
                <h4>
                    Products
                </h4>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" class="style1">
                <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
            </td>
        </tr>

        <tr>
                <td align="center">
                    <fieldset style="color: #33CCFF; border-color: #0000FF" >
                        <legend>Search</legend>
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdbSupplier" runat="server" Text="By Supplier" 
                                        GroupName="Search" />
                                    <asp:RadioButton ID="rdbPartNumber" runat="server" Text="By PartNumber" 
                                        GroupName="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                    <asp:Button ID="btmSearch" runat="server" Text="Search" OnClick="btmSearch_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        <tr>
            <td>
                <asp:GridView ID="GvNewPart" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" Width="100%" OnPageIndexChanging="GvNewPart_PageIndexChanging"
                    OnRowCommand="GvNewPart_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Supplier">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part Number">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("part_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HScode">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("HScode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PackID">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("PackID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gross WT">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("gross_weight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Length">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("length") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Width">
                            <ItemTemplate>
                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("width") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Height">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("height") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="center" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </td>
        </tr>
          <tr> <td align ="center">  <table> <tr> <td> 
                <asp:LinkButton ID="lbFirst" runat="server" onclick="lbFirst_Click" >First</asp:LinkButton>
                </td> <td> 
                    <asp:LinkButton ID="lbNext" runat="server" onclick="lbNext_Click" >Next</asp:LinkButton>
                </td> <td> 
                    <asp:LinkButton ID="lbPrev" runat="server" onclick="lbPrev_Click" >Prev</asp:LinkButton>
                </td> <td>  
                    <asp:LinkButton ID="lbLAst" runat="server" onclick="lbLAst_Click" >Last</asp:LinkButton>
                </td></tr></table></td></tr>
    </table>
</asp:Content>
