<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="newDo.aspx.cs" Inherits="Intranet_WMS_newDo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4
        {
            width: 418px;
        }
        .style8
        {
            width: 100%;
        }
        .style9
        {
            height: 26px;
        }
        .style10
        {
            width: 418px;
            height: 31px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                <table align="center" width="100%" style="background-image: url('../images/bgimg.png');>
                    <tr>
                        <td align="center">
                            <h4>
                            <centre>    Deliver Order  </centre>
                                
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center">
                                <tr align="center">
                                    <td align="center">
                                        <asp:Button ID="btsave" runat="server" Text="Save" OnClick="btsave_Click" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btconfirm" runat="server" Text="Confirm" Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnpickTicket" runat="server" Text="Pick Ticket " 
                                            Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnRelease" runat="server" Text="Release " Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnDeliveryNote" runat="server" Text="Delivery Note" 
                                            Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnCustomerInvoice" runat="server" Text="Customer Invoice" 
                                            Width="127px" Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnInvoice" runat="server" Text="Invoice" Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnDeliveryAdvice" runat="server" Text="Delivery Advice" 
                                            Enabled="False" />
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btnXfrOwnership" runat="server" Text="Xfr Ownership " 
                                            Width="99px" EnableViewState="False" />
                                    </td>
                                    <td align="center">
                                        &nbsp;
                                    </td>
                                    <td align="center">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr> <td> 
                        <table class="style8">
                            <tr>
                                <td>
                                        <asp:Label ID="LbDO" runat="server" Text="DO#"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="tbDO" runat="server" BorderColor="Silver" 
                                            style="margin-left: 0px"></asp:TextBox>
                                    </td>
                                <td>
                                        <asp:Label ID="lbdriver" runat="server" Text="Driver"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="TbDriver" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvdriver" runat="server" ErrorMessage="Required" ControlToValidate="TbDriver"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                                <td>
                                        <asp:Label ID="lbVehicle" runat="server" Text="Vehicle"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="TbVehicle" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvehicle" runat="server" ErrorMessage="Required" 
                                            ControlToValidate="TbVehicle"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                        <asp:Label ID="lbStatus" runat="server" Text="Status"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="tbStatus" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </td>
                                <td>
                                        <asp:Label ID="Label2" runat="server" Text="Shipping  Reference"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="TbShipReference" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </td>
                                <td>
                                        <asp:Label ID="Label3" runat="server" Text="Container"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="TbContainer" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfcontainer" runat="server" ErrorMessage="Required" 
                                            ControlToValidate="TbContainer"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                        <asp:Label ID="Label5" runat="server" Text="Creation Date"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="tbCreationDate" runat="server" BorderColor="Silver"></asp:TextBox>
                                    </td>
                                <td>
                                        <asp:Label ID="Label6" runat="server" Text="Effective Date"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="TbEffectiveDate" runat="server" BorderColor="Silver"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TbEffectiveDate">
                                        </cc1:CalendarExtender>
                                    </td>
                                <td>
                                        <asp:Label ID="Label1" runat="server" Text="Invoice Number"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="Tbinvoicenumber" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvinvoince" runat="server" ErrorMessage="Required" 
                                            ControlToValidate="Tbinvoicenumber"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                        <asp:Label ID="Label22" runat="server" Text="Release No"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="tbReleaseNo" runat="server" BorderColor="Silver" 
                                            ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="frvrealease" runat="server" ErrorMessage="Required" 
                                            ControlToValidate="tbReleaseNo"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                                <td>
                                        <asp:Label ID="Label27" runat="server" Text="Release Order"></asp:Label>
                                    </td>
                                <td>
                                        <asp:TextBox ID="txtReleaseOrder" runat="server" BorderColor="Silver"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvorder" runat="server" ErrorMessage="Required" 
                                            ControlToValidate="txtReleaseOrder"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                    </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                        <asp:Label ID="Label4" runat="server" Text="Shipping Method"></asp:Label>
                                        </td>
                                <td>
                                        <asp:RadioButton ID="rbsea" runat="server" Text="Sea" GroupName="shipMeth" 
                                            Checked="True" />
                                        <asp:RadioButton ID="rbair" runat="server" Text="Air" GroupName="shipMeth" />
                                        <asp:RadioButton ID="rbland" runat="server" Text="Land" GroupName="shipMeth" />
                                    </td>
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
                        </td></tr>
                   
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <table>
                                            <tr>
                                            <td>  <asp:Label ID="Label7" runat="server" Text="Customer"></asp:Label> </td>
                                                <td >
                                                   
                                                   
                                                    <asp:DropDownList ID="ddlcustomer" runat="server" Height="16px" Width="290px" 
                                                        DataSourceID="ObjectDataSource1" DataTextField="customer_name" 
                                                        DataValueField="customer_id">
                                                    </asp:DropDownList>
                                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="customerList"
                                TypeName="WMSclsCustomerList"></asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td> <asp:Label ID="Label8" runat="server" Text="Release To"></asp:Label> </td>
                                                <td >  
                                                   
                                                    <asp:DropDownList ID="ddlRelease" runat="server" Height="22px" Width="290px" 
                                                        DataSourceID="ObjectDataSource1" DataTextField="customer_name" 
                                                        DataValueField="customer_id">
                                                    </asp:DropDownList>
                                                
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>   <asp:Label ID="Label18" runat="server" Text="Freight Forwarder"></asp:Label></td>
                                                <td >
                                                  
                                                  
                                                    <asp:DropDownList ID="ddlFreightForwarder" runat="server" Height="22px" 
                                                        Width="290px" DataSourceID="ObjectDataSource3" 
                                                        DataTextField="freight_forwarder_name" DataValueField="freight_forwarder_id">
                                                    </asp:DropDownList>
                                                        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="FreightFowarderList"
                                TypeName="WMSclsFreightFowarderList"></asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="Right">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label19" runat="server" Text="Note"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnotes" runat="server" BorderColor="Silver" Height="91px" Width="457px"
                                                        Style="margin-left: 0px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table width="100px">
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

             <tr id="trPartSelect" runat ="server" visible ="false" >
            <td>
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="Label14" runat="server" Text="Please Select the part " ForeColor="#99CCFF"></asp:Label>
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
        <tr id="trmessage" runat="server" visible="false">
            <td>
                <center>
                    <asp:Label ID="lbmsg" runat="server" Text="The delivery order has been successfully updated"
                        ForeColor="Red"></asp:Label>
                </center>
            </td>
        </tr>
        <tr id="trdetailds" runat="server" visible="false">
            <td>
                <fieldset style="color: #33CCFF; border-color: #0000FF">
                    <legend>Delivery Order Details</legend>
                    <table class="style8">
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Part Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpartnumber" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hdnStockID" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label24" runat="server" Text="Location"></asp:Label>
                                
                            </td>
                            <td>
                                <asp:TextBox ID="txtlocation" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hdnLocationID" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label25" runat="server" Text="Warehouse"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtwarehouse" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hdnWarehouseID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label32" runat="server" Text="Boe"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBoe" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hdndoDetailsID" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="PO  Customer"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPoNumber" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbcountry" runat="server" Text="Country"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcountry" runat="server" ReadOnly="True"></asp:TextBox>
                                <asp:HiddenField ID="hdnCountry" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbPrice" runat="server" Text="Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtItemPrice" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Inv Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvPrice" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="Tot Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotPrice" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label33" runat="server" Text="Qty"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty" runat="server" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label26" runat="server" Text="Inv Qty"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvQty" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text="Vol"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVol" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Total Vol"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotVol" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label31" runat="server" Text="Gross Wt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrossWt" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label34" runat="server" Text="Tot Gross Wt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotGrossVol" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>

                     <tr>
                        <td>
                            <asp:GridView ID="GvPrealertDetail" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                Width="100%" onrowcommand="GvPrealertDetail_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Part Number">
                                        <ItemTemplate>
                                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("part_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BOE">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("boe_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("invoice_qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("warehouse_description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="Label14" runat="server" Text='<%# Bind("item_price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv Price">
                                        <ItemTemplate>
                                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("invoice_price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer PO">
                                        <ItemTemplate>
                                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("PO_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tot Price">
                                        <ItemTemplate>
                                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("total_price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vol">
                                        <ItemTemplate>
                                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("item_volume") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Vol">
                                        <ItemTemplate>
                                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("total_volume") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gross Wt">
                                        <ItemTemplate>
                                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("gross_weight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tot GrossWt">
                                        <ItemTemplate>
                                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("total_gross_weight") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                     <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbedit" runat="server" CommandName="Editing" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red"><img src="../images/edit.png" title="Edit"/></asp:LinkButton>
                                <asp:LinkButton ID="lbdelete" runat="server" CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>"
                             OnClientClick="javascript:return confirm('Are you sure to proceed?');"         ForeColor="Red"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
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
                </table>
            
</asp:Content>
