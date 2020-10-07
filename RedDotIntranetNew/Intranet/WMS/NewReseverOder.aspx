<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="NewReseverOder.aspx.cs" Inherits="Intranet_WMS_NewReseverOder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">

        function beforesave() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_txtreserveOrder").value == "") {
                alert("Please enter the Reserve Order");
                return false;
            }

            else {
                return true;
            }
        }
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    New Reserve Oder</h4>
            </td>
        </tr>
        <tr>
              <td align="center">
                <asp:Button ID="Button1" OnClientClick="beforesave()" runat="server" Text="Save"
                    OnClick="btnSave_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="label1" runat="server" Text="DO#"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDoid" runat="server" Style="margin-left: 0px" Width="208px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Expiry Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExperiyDate" runat="server" Height="22px" Width="216px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStatus" runat="server" Height="22px" Width="216px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Reserve Order #"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtreserveOrder" runat="server" Height="22px" Width="216px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Creation Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreationDate" runat="server" Height="22px" Width="216px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Customer"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddpCustname" runat="server" 
                                DataSourceID="ObjectDataSource1" DataTextField="customer_name" 
                                DataValueField="customer_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                SelectMethod="customerList" TypeName="WMSclsCustomerList">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtnote" runat="server" Height="72px" Width="830px" 
                                TextMode="MultiLine" style="margin-left: 50px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Please Select the part " 
                                ForeColor="#99CCFF"></asp:Label>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                                SelectMethod="PartnumberList" TypeName="WMSclsReserveOrderStockList">
                            </asp:ObjectDataSource>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddpPartnumber" runat="server" OnSelectedIndexChanged="ddpPartnumber_SelectedIndexChanged"
                                AutoPostBack="True" ValidationGroup="part" 
                                DataSourceID="ObjectDataSource2" DataTextField="part_number" 
                                DataValueField="stock_id">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvReserve" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" Width="100%" OnPageIndexChanging="gvReserve_PageIndexChanging"
                    OnRowCommand="gvReserve_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Part Number">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("part_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOE">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
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
        <tr>
            <td align="right" width="100%">
                <asp:Label ID="lbTotal" runat="server" Text="" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
