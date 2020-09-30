<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="NewPrealert.aspx.cs" Inherits="Intranet_WMS_NewPrealert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style3
        {
            width: 803px;
        }
        .style7
        {
            width: 100%;
        }
        .style8
        {
            text-align: left;
        }
        .style9
        {
            height: 22px;
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
                    PREALERT
                </h4>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="center">
                    <tr align="center">
                        <td align="center">
                            <asp:Button ID="btsave" runat="server" Text="Save" OnClick="btsave_Click" Height="26px"
                                Width="132px" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btconfirm" runat="server" Text="Confirm" Height="26px" Width="132px" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btReceive" runat="server" Text="Receive Goods   " Enabled="False"
                                Height="26px" Width="132px" />
                        </td>
                        <td align="center">
                            <asp:Button ID="bttally" runat="server" Text="Unstuffing Tally" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center" class="style9">
                <asp:Label ID="lbmsg" runat="server" Text="Prealert saved successfully." Visible="False"
                    ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <table class="style7">
                    <tr>
                        <td class="style8">
                            <asp:Label ID="lb1" runat="server" Text="Prealeret ID"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="tbPrealertID" runat="server" ReadOnly="True" />
                        </td>
                        <td class="style8">
                            <asp:Label ID="ld2" runat="server" Text="Status"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="TbStatus" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lb3" runat="server" Text="Creation Date"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="TbCreateDate" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="Label1" runat="server" Text="Supplier"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="DPLsupplier" runat="server" 
                                DataSourceID="ObjectDataSource5" DataTextField="supplier_name" 
                                DataValueField="supplier_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" 
                                SelectMethod="SupplierList" TypeName="WMSclsSupplierList">
                            </asp:ObjectDataSource>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label2" runat="server" Text="Supplier Reference"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="TbSupReference" runat="server" BorderColor="Silver" Style="margin-left: 0px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvref" runat="server" ErrorMessage="Required" ControlToValidate="TbSupReference"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label8" runat="server" Text="Forwarder"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:DropDownList ID="DPLForwarder" runat="server" Width="150px" 
                                DataSourceID="ObjectDataSource6" DataTextField="freight_forwarder_name" 
                                DataValueField="freight_forwarder_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" 
                                SelectMethod="FreightFowarderList" TypeName="WMSclsFreightFowarderList">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            <asp:Label ID="Label5" runat="server" Text="Shipping Reference"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="tbShipRef" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvship" runat="server" ErrorMessage="Required" ControlToValidate="tbShipRef"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                        </td>
                        <td class="style8">
                            <asp:Label ID="Label6" runat="server" Text="Eta"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="Tbeta" runat="server" BorderColor="Silver"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Tbeta">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="rfveta" runat="server" ErrorMessage="Required" ControlToValidate="Tbeta"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                        </td>
                        <td class="style8">
                            <asp:Label ID="lb4" runat="server" Text="Total Qty"></asp:Label>
                        </td>
                        <td class="style8">
                            <asp:TextBox ID="TbToal" runat="server" BorderColor="Silver"></asp:TextBox>
                            <%-- <asp:RequiredFieldValidator ID="rfvQty" runat="server" ErrorMessage="Required" ControlToValidate="TbToal"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TbToal"
                                ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="left" width="50%">
                    <tr>
                        <td align="left" width="16.6%">
                            &nbsp;
                        </td>
                        <td align="left" width="16.6%">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left">
                <table align="left">
                    <tr>
                        <td width="19.6%">
                            <asp:Label ID="Label4" runat="server" Text="Shipping Method"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:RadioButton ID="rbsea" runat="server" Text="Sea" GroupName="shipMeth" />
                            <asp:RadioButton ID="rbair" runat="server" Text="Air" GroupName="shipMeth" Checked="True" />
                            <asp:RadioButton ID="rbland" runat="server" Text="Land" GroupName="shipMeth" />
                        </td>
                        <td width="17.6%">
                            <asp:Label ID="Label7" runat="server" Text="Delivery Method"></asp:Label>
                        </td>
                        <td width="35%">
                            <asp:RadioButton ID="rbddu" runat="server" Text="DDU" GroupName="delvMeth" />
                            <asp:RadioButton ID="rbfob" runat="server" Text="FOB" GroupName="delvMeth" Checked="True" />
                            <asp:RadioButton ID="rbexword" runat="server" Text="ExWorks" GroupName="delvMeth" />
                            <asp:RadioButton ID="rbcnf" runat="server" Text="CNF" GroupName="delvMeth" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Comment"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbcomment" runat="server" Height="52px" TextMode="MultiLine" Width="844px"
                                BorderColor="Silver"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Button ID="Btnew" runat="server" Text="New Part Number" OnClick="Btnew_Click" />
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:Label ID="Label16" runat="server" Text="Part #"></asp:Label>
                <asp:TextBox ID="tbpart" runat="server" BorderColor="Silver"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="100%">
                <asp:GridView ID="GvPrealertDetail" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#3366CC" BorderStyle="Double" BorderWidth="1px" CellPadding="4"
                    Width="100%" ShowFooter="True" OnRowCommand="GvPrealertDetail_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Part">
                            <FooterTemplate>
                                <asp:DropDownList ID="Dplpart" runat="server" DataSourceID="ObjectDataSource1" DataTextField="part_number"
                                    DataValueField="stock_id" Height="24px" Width="123px">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="PartnumberList"
                                    TypeName="WMSclsStocklist"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnPrealertDetailID" runat="server" Value='<%# Bind("prealert_detail_id") %>' />
                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("stock_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <FooterTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TbQTY" runat="server" BorderColor="Silver" Width="75px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TbQTY"
                                                ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000"></span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COO">
                            <FooterTemplate>
                                <asp:DropDownList ID="DllCoo" runat="server" DataSourceID="ObjectDataSource2" DataTextField="country_name"
                                    DataValueField="country_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="countryList"
                                    TypeName="WMSclsCooList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("country_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO">
                            <FooterTemplate>
                                <asp:TextBox ID="TbPO" runat="server" BorderColor="Silver" Width="69px"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("PO_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WeareHouse">
                            <FooterTemplate>
                                <asp:DropDownList ID="DPLWeareHouse" runat="server" DataSourceID="ObjectDataSource3"
                                    DataTextField="description" DataValueField="warehouse_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="WarehouseList"
                                    TypeName="WMSclsWarehouseList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("warehouseEVO") %>'></asp:Label>
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
                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:Button ID="btadd" runat="server" OnClick="Button1_Click" Text="ADD" CommandArgument="<%# Container.DataItemIndex %>" />
                            </FooterTemplate>
                            <ItemTemplate>
                              <asp:LinkButton ID="lbedit" runat="server" CommandName="Editing" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red"><img src="../images/edit.png" title="Edit"/></asp:LinkButton>

                                <asp:LinkButton ID="lbdelete" runat="server" CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
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
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
