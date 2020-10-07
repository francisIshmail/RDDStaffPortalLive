<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="ReserveOrder.aspx.cs" Inherits="Intranet_WMS_ReserveOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 111px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td align="center">
                <h4>
                    Reserve Oder</h4>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnsave_Click" />
            </td>
        </tr>
           <tr>
            <td align ="center">
                <asp:Label ID="lbmsg" runat="server" Text="" 
                    ForeColor="Red" Visible="False"></asp:Label>
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
                            <asp:TextBox ID="txtDoid" runat="server" Height="22px" Width="216px"></asp:TextBox>
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
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server" Text="Note"></asp:Label>
                        </td>
                        <td colspan ="3">
                            <asp:TextBox ID="txtnote" runat="server" Height="72px" Width="692px" 
                                TextMode="MultiLine"></asp:TextBox>
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
                            <asp:Label ID="Label8" runat="server" Text="Please Select the part " ForeColor="#99CCFF"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddpPartnumber" runat="server" OnSelectedIndexChanged="ddpPartnumber_SelectedIndexChanged"
                                AutoPostBack="True" ValidationGroup="part" DataSourceID="ObjectDataSource2" DataTextField="part_number"
                                DataValueField="stock_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="PartnumberList"
                                TypeName="WMSclsReserveOrderStockList"></asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvReserveOrder" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="4" OnPageIndexChanging="gvReserveOrder_PageIndexChanging" OnRowCommand="gvReserveOrder_RowCommand"
                    Width="100%" OnSelectedIndexChanged="gvReserveOrder_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Part Number">
                            <FooterTemplate>
                                <asp:DropDownList ID="Dplpart" runat="server" DataSourceID="ObjectDataSource1" CommandArgument="<%# Container.DataItemIndex %>"
                                    DataTextField="part_number" DataValueField="stock_id" Height="24px" Width="123px"
                                    OnSelectedIndexChanged="Dplpart_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="PartnumberList"
                                    TypeName="WMSclsReserveOrderStockList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                              <asp:HiddenField ID="hdnDodetailID" runat="server" Value='<%# Bind("do_detail_id") %>' />
                                <asp:HiddenField ID="hdnflag" runat="server" Value='<%# Bind("flag") %>' />
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("part_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <FooterTemplate>
                                <asp:DropDownList ID="Dplocation" runat="server" DataSourceID="ObjectDataSource4"
                                    DataTextField="location_description" DataValueField="location_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="locationList"
                                    TypeName="WMSclsLocationList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BOE">
                            <FooterTemplate>
                                <asp:TextBox ID="txtboedID" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse">
                            <FooterTemplate>
                                <asp:DropDownList ID="DPLWeareHouse" runat="server" DataSourceID="ObjectDataSource3"
                                    DataTextField="description" DataValueField="warehouse_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="WarehouseList"
                                    TypeName="WMSclsWarehouseList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <FooterTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbQuanity" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:Button ID="btadd" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    Text="ADD" OnClick="btadd_Click" />
                            </FooterTemplate>
                            <ItemTemplate>
                             <asp:LinkButton ID="lbedit" runat="server" CommandName="Editing" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red" CausesValidation="False">
  <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
                                <asp:LinkButton ID="lbdelete" runat="server" CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>"
                                  OnClientClick="javascript:return confirm('Are you sure to proceed?');"   ForeColor="Red"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
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
