<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true" CodeFile="SelectStock.aspx.cs" Inherits="Intranet_WMS_SelectStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            background-image: url('../images/bgimg.png');
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="style1">
        <tr>
            <td>
                  <center> <h4>
                  Select Stock
                </h4></center></td>
        </tr>
             <tr>
            <td align='center'>
            <table> <tr> <td>   <asp:Button ID="BtnAllocate" runat="server" Text="Allocate" 
                    onclick="BtnAllocate_Click" />     </td> <td>   <asp:Button ID="btnCancel" 
                        runat="server" Text="Cancel" onclick="btnCancel_Click" 
                   />      </td></tr></table>
               </td>
        </tr>
       <tr>
            <td align ="center">
                <asp:Label ID="lbmsg" runat="server" Text="Invalid Allocated quantity" 
                    ForeColor="Red" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table class="style2">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Quantity In Stock:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyStock" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Quantity Available:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAvailable" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Quantity Allocated:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyAllocated" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Quantity to Allocate:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyToAllocate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Quantity Reserved"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtQtyReserved" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvSelectStock" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4"  Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Creation Date">
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("creation_date") %>'></asp:Label>
                                      <asp:HiddenField ID="hdwarehouse" runat="server" Value='<%# Bind("warehouse_id") %>' />
                                         <asp:HiddenField ID="hdlocation" runat="server" Value='<%# Bind("location_id") %>' />
                                           <asp:HiddenField ID="hdcoo" runat="server" Value='<%# Bind("COO") %>' />
                                           <asp:HiddenField ID="hfstockid" runat="server" Value='<%# Bind("stock_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Number">
                            <ItemTemplate>
                                <asp:Label ID="lbPonumber" runat="server" 
                                    Text='<%# Bind("PO_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="lbwaresouse" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="lblocation" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOE">
                            <ItemTemplate>
                                <asp:Label ID="lbboe" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Allocd">
                            <ItemTemplate>
                                <asp:Label ID="lballocd" runat="server" 
                                    Text='<%# Bind("allocd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lbprice" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grosswt">
                            <ItemTemplate>
                                <asp:Label ID="lbgrosswt" runat="server" Text='<%# Bind("grosswt") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Length">
                            <ItemTemplate>
                                <asp:Label ID="lblength" runat="server" Text='<%# Bind("length") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Width">
                            <ItemTemplate>
                                <asp:Label ID="lbwidth" runat="server" Text='<%# Bind("width") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Height">
                            <ItemTemplate>
                                <asp:Label ID="lbheight" runat="server" Text='<%# Bind("height") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COO">
                            <ItemTemplate>
                                <asp:Label ID="lbcoo" runat="server" 
                                    Text='<%# Bind("country_name") %>'></asp:Label>
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
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

