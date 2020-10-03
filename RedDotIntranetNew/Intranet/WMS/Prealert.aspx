<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Prealert.aspx.cs" Inherits="Intranet_WMS_Prealert" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table align="center" class="style1" style="background-image: url('../images/bgimg.png');">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <tr>
            <td align="center">
                <h4>
                    Prealert</h4>
            </td>
        </tr>
        <tr>
            <td>
                <table align="center">
                    <tr align="center">
                        <td align="center">
                            <asp:Button ID="btsave" runat="server" Text="Save" Enabled="False" 
                                ValidationGroup="boe"   />
                        </td>
                        <td align="center">
                            <asp:Button ID="btconfirm" runat="server" Text="Confirm"  OnClientClick="javascript:return confirm('Are you sure to proceed?');"
                                OnClick="btconfirm_Click" CausesValidation="False" />
                        </td>
                        <td>
                        </td>
                        <td align="center">
                            <asp:Button ID="btReceive" runat="server" Text="Receive Goods   " Enabled="False"  OnClientClick="javascript:return confirm('Are you sure to proceed?');"
                                OnClick="btReceive_Click"  />
                        </td>
                        <td align="center">
                            <asp:Button ID="bttally" runat="server" Text="Unstuffing Tally" 
                                OnClick="bttally_Click" CausesValidation="False" />
                        </td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr> <td>    <center>     <asp:Label ID="lbmsg" runat="server" Text="Prealert saved successfully." Visible="False"
                    ForeColor="Red"></asp:Label> </center> </td></tr>
        <tr><td> 
            <table class="style1">
                <tr>
                    <td>
                            <asp:Label ID="lb1" runat="server" Text="Prealeret ID"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="tbPrealertID" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="ld2" runat="server" Text="Status"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbStatus" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="lb3" runat="server" Text="Creation Date"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbCreateDate" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier"></asp:Label>
                        </td>
                    <td>
                             <asp:DropDownList ID="DPLsupplier" runat="server" 
                                DataSourceID="ObjectDataSource5" DataTextField="supplier_name" 
                                DataValueField="supplier_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" 
                                SelectMethod="SupplierList" TypeName="WMSclsSupplierList">
                            </asp:ObjectDataSource>
                        </td>
                    <td>
                            <asp:Label ID="Label2" runat="server" Text="Supplier Reference"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbSupReference" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="Label3" runat="server" Text="BOE#"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbBoe" runat="server" BorderColor="Silver" OnTextChanged="TbBoe_TextChanged"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvref" runat="server" ErrorMessage="Required" ControlToValidate="TbBoe" ValidationGroup="boe"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                        </td>
                </tr>
                <tr>
                    <td>
                            <asp:Label ID="Label5" runat="server" Text="Shipping Reference"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="tbShipRef" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="Label6" runat="server" Text="ETA"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="Tbeta" runat="server" BorderColor="Silver"></asp:TextBox>
                              <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Tbeta">
                            </cc1:CalendarExtender>
                        </td>
                    <td>
                            <asp:Label ID="lb4" runat="server" Text="Total Qty"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbToal" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TbToal"
                                ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                        </td>
                </tr>
                <tr>
                    <td>
                            <asp:Label ID="Label8" runat="server" Text="Forwarder"></asp:Label>
                        </td>
                    <td>
                             <asp:DropDownList ID="DPLForwarder" runat="server" Width="150px" 
                                DataSourceID="ObjectDataSource6" DataTextField="freight_forwarder_name" 
                                DataValueField="freight_forwarder_id">
                            </asp:DropDownList>
                            <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" 
                                SelectMethod="FreightFowarderList" TypeName="WMSclsFreightFowarderList">
                            </asp:ObjectDataSource>
                        </td>
                    <td>
                            <asp:Label ID="Label4" runat="server" Text="Shipping Method"></asp:Label>
                        </td>
                    <td>
                            <asp:RadioButton ID="rbsea" runat="server" Text="Sea" GroupName="ship"/>
                            <asp:RadioButton ID="rbair" runat="server" Text="Air" GroupName="ship" />
                            <asp:RadioButton ID="rbland" runat="server" Text="Land" GroupName="ship"  />
                        </td>
                    <td>
                            <asp:Label ID="Label7" runat="server" Text="Delivery Method"></asp:Label>
                        </td>
                    <td>
                            <asp:RadioButton ID="rbddu" runat="server" Text="DDU" GroupName="Delivery" />
                            <asp:RadioButton ID="rbfob" runat="server" Text="FOB" GroupName="Delivery"/>
                            <asp:RadioButton ID="rbexword" runat="server" Text="ExWorks" 
                                GroupName="Delivery" />
                            <asp:RadioButton ID="rbcnf" runat="server" Text="CNF" />
                        </td>
                </tr>
            </table>
            </td></tr>
      
        <tr>
            <td align="left">
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
            <td>
                <asp:Label ID="Label16" runat="server" Text="Part #"></asp:Label>
                <asp:TextBox ID="tbpart" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>


           <tr id="trDetails" runat="server" visible="false">
            <td>
                <fieldset style="color: #33CCFF; border-color: #0000FF">
                    <legend>Prealert Details</legend>
                    <table class="style8">
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Part Number"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPartNumber" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource1"
                                    DataTextField="part_number" DataValueField="stock_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="PartnumberList"
                                    TypeName="WMSclsStocklist"></asp:ObjectDataSource>
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="Location"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLocation" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource2"
                                    DataTextField="location_description" DataValueField="location_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="locationList"
                                    TypeName="WMSclsLocationList"></asp:ObjectDataSource>
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text="Warehouse"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlwarehouse" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource3"
                                    DataTextField="description" DataValueField="warehouse_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="WarehouseList"
                                    TypeName="WMSclsWarehouseList"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbcountry" runat="server" Text="Country"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource4"
                                    DataTextField="country_name" DataValueField="country_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="countryList"
                                    TypeName="WMSclsCooList"></asp:ObjectDataSource>
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="PO  Customer"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPoNumber" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label33" runat="server" Text="Qty"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                            </td>
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
                            <td>
                                
                                <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                                    Text="Update" />
                                
                            </td>
                            <td>
                                
                                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click2" 
                                    Text="Cancel" />
                                
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>


        <tr>
            <td class="style1">
                <asp:GridView ID="GvPrealertDetail" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#3366CC" BorderStyle="Double" BorderWidth="1px" CellPadding="4"
                    Width="100%" ShowFooter="True" onrowcommand="GvPrealertDetail_RowCommand" 
                    style="text-align: left">
                    <Columns>
                        <asp:TemplateField HeaderText="Part">
                            <EditItemTemplate>
                                <asp:DropDownList ID="Dplpart" runat="server" DataSourceID="ObjectDataSource1" 
                                    DataTextField="part_number" DataValueField="stock_id" Height="24px" 
                                    Width="123px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="Dplpart" runat="server" DataSourceID="ObjectDataSource1" 
                                    DataTextField="part_number" DataValueField="stock_id" Height="24px" 
                                    Width="123px">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                                    SelectMethod="PartnumberList" TypeName="WMSclsStocklist">
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                             <asp:HiddenField ID="hdnboeDetailID" runat="server" Value='<%# Bind("boe_detail_id") %>' />
                            <asp:HiddenField ID="hdnPrealertDetailID" runat="server" Value='<%# Bind("prealert_detail_id") %>' />
                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("stock_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <EditItemTemplate>
                                <asp:TextBox ID="TbQTY" runat="server" BorderColor="Silver" Width="75px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                    ControlToValidate="TbQTY" ErrorMessage="RegularExpressionValidator" 
                                    ValidationExpression="^\d+$" ValidationGroup="detail">  <img 
    src="../images/ErrorMessage.png" alt ="*" /><span 
    style="color: #FF0000"></span></asp:RegularExpressionValidator>
                            </EditItemTemplate>
                            <FooterTemplate>
                             
                              <table align="left" >
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TbQTY" runat="server" BorderColor="Silver" Width="75px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TbQTY"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d+$" ValidationGroup="detail">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000"></span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="COO">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DllCoo" runat="server" DataSourceID="ObjectDataSource2" 
                                    DataTextField="country_name" DataValueField="country_id">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="DllCoo" runat="server" DataSourceID="ObjectDataSource2" 
                                    DataTextField="country_name" DataValueField="country_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                                    SelectMethod="countryList" TypeName="WMSclsCooList"></asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label14" runat="server" Text='<%# Bind("country_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TbPO" runat="server" BorderColor="Silver" Width="69px"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="TbPO" runat="server" BorderColor="Silver" Width="69px"></asp:TextBox>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label11" runat="server" Text='<%# Bind("PO_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WeareHouse">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DPLWeareHouse" runat="server" 
                                    DataSourceID="ObjectDataSource3" DataTextField="description" 
                                    DataValueField="warehouse_id">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="DPLWeareHouse" runat="server" 
                                    DataSourceID="ObjectDataSource3" DataTextField="description" 
                                    DataValueField="warehouse_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
                                    SelectMethod="WarehouseList" TypeName="WMSclsWarehouseList">
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label13" runat="server" Text='<%# Bind("warehouseEVO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <EditItemTemplate>
                                <asp:DropDownList ID="Dplocation" runat="server" 
                                    DataSourceID="ObjectDataSource4" DataTextField="location_description" 
                                    DataValueField="location_id">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="Dplocation" runat="server" 
                                    DataSourceID="ObjectDataSource4" DataTextField="location_description" 
                                    DataValueField="location_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
                                    SelectMethod="locationList" TypeName="WMSclsLocationList">
                                </asp:ObjectDataSource>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <FooterTemplate>
                                <asp:Button ID="btadd" runat="server"  OnClick="Button1_Click" 
                                    Text="ADD" ValidationGroup="detail" />
                            </FooterTemplate>
                            <ItemTemplate>
                              <asp:LinkButton ID="lbedit" runat="server" CommandName="Editing" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red"><img src="../images/edit.png" title="Edit"/></asp:LinkButton>

                                <asp:LinkButton ID="lbdelete" runat="server" CommandName="Deleting"   CommandArgument="<%# Container.DataItemIndex %>" 
                                  OnClientClick="javascript:return confirm('Are you sure to proceed?');"   ForeColor="Red" CausesValidation="False"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
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
      
    </table>
</asp:Content>
