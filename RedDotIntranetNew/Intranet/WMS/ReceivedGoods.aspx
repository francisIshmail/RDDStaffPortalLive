<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="ReceivedGoods.aspx.cs" Inherits="Intranet_WMS_ReceivedGoods" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 26px;
        }
        .style8
        {
            width: 100%;
        }
    </style>
    <script language="JavaScript" type="text/javascript">

        function beforesave() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_TbBoe").value == "") {
                alert("Please enter The BOE#.");
                return false;
            }

            else {
                return true;
            }

        }
        
    </script>
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" class="style1" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td>
                <table align="center">
                    <tr align="center">
                        <td align="center">
                            <asp:Button ID="btsave" runat="server" Text="Save" 
                              ValidationGroup="arrive"   onclick="btsave_Click" />
                        </td>
                       
                        <td>
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                        <td align="center">
                            <asp:Button ID="bttally" runat="server" Text="Unstuffing Tally" 
                                CausesValidation="False" onclick="bttally_Click" />
                        </td>
                        <td align="center">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <h4>
                    Receive Goods</h4>
            </td>
        </tr>

        <tr> <td> <center > <asp:Label ID="lbmsgtop" runat="server" Text="The good has been successfully updated." ForeColor="Red"/>     </center></td></tr>
        <tr> <td>      
            <table class="style1">
                <tr visible="False">
                    <td>
                            <asp:Label ID="Label3" runat="server" Text="Boe#"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbBoe" runat="server" BorderColor="Silver"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="ld2" runat="server" Text="Status"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbStatus" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="lb3" runat="server" Text="Creation Date"></asp:Label>
                        </td>
                        <td width="16.6%">
                            <asp:TextBox ID="TbCreateDate" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                   
                </tr>
                <tr>
                    <td class="style2">
                            <asp:Label ID="lb1" runat="server" Text="Prealeret ID"></asp:Label>
                        </td>
                    <td class="style2">
                            <asp:TextBox ID="tbPrealertID" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    <td class="style2">
                            <asp:Label ID="Label16" runat="server" Text="Actual Arrival Date"></asp:Label>
                        </td>
                    <td class="style2">
                            <asp:TextBox ID="txtarrivalDate" runat="server"></asp:TextBox>
                                     <cc1:calendarextender ID="CalendarExtender1" runat="server" 
                                TargetControlID="txtarrivalDate"> </cc1:calendarextender>
                                <asp:RequiredFieldValidator ID="rfvarrivalDate" runat="server" 
                                ErrorMessage="Required" ControlToValidate="txtarrivalDate" 
                                ValidationGroup="arrive"> <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                        </td>
                    <td class="style2">
                            <asp:Label ID="lb4" runat="server" Text="Total Qty"></asp:Label>
                        </td>
                    <td class="style2">
                            <asp:TextBox ID="TbToal" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td>
                            <asp:Label ID="Label1" runat="server" Text="Supplier"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="tbsupplier" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="Label2" runat="server" Text="Supplier Reference"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="TbSupReference" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="Label8" runat="server" Text="Forwarder"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="tbForwarder" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td>
                            <asp:Label ID="Label5" runat="server" Text="Shipping Reference"></asp:Label>
                        </td>
                    <td>
                            <asp:TextBox ID="tbShipRef" runat="server" BorderColor="Silver" ReadOnly="True"></asp:TextBox>
                        </td>
                    <td>
                            <asp:Label ID="Label4" runat="server" Text="Shipping Method"></asp:Label>
                        </td>
                    <td>
                            <asp:RadioButton ID="rbsea" runat="server" Text="Sea" />
                            <asp:RadioButton ID="rbair" runat="server" Text="Air" />
                            <asp:RadioButton ID="rbland" runat="server" Text="Land" />
                        </td>
                    <td>
                            <asp:Label ID="Label7" runat="server" Text="Delivery Method"></asp:Label>
                        </td>
                    <td>
                            <asp:RadioButton ID="rbddu" runat="server" Text="DDU" />
                            <asp:RadioButton ID="rbfob" runat="server" Text="FOB" />
                            <asp:RadioButton ID="rbexword" runat="server" Text="ExWorks" />
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
                            <asp:Label ID="Label9" runat="server" Text="Remark"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbcomment" runat="server" Height="52px" TextMode="MultiLine" Width="844px"
                                BorderColor="Silver"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr> <td> <center>                      <asp:Button ID="btnAdd" runat="server" Text="Add New Detail" OnClick="btnAdd_Click" />
                </center></td></tr>
        <tr id="trmessage" runat="server" visible="false">
            <td>
                <center>
                    <asp:Label ID="lbmsg" runat="server" Text="The delivery order has been successfully updated"
                        ForeColor="Red"></asp:Label>
                </center>
            </td>
        </tr>
        <tr id="trDetails" runat="server" visible="false">
            <td>
                <fieldset style="color: #33CCFF; border-color: #0000FF">
                    <legend>Delivery Order Details</legend>
                    <table class="style8">
                        <tr>
                            <td>
                                <asp:Label ID="Label23" runat="server" Text="Part Number"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlPartNumber" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource1"
                                    DataTextField="part_number" DataValueField="stock_id" 
                                   >
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
                                <asp:Label ID="Label32" runat="server" Text="Boe"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBoe" runat="server"></asp:TextBox>
                                <asp:HiddenField ID="hdnBoeDetailID1" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="Label35" runat="server" Text="PO Number"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPoNumber" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lbcountry" runat="server" Text="Country"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="dplCountry" runat="server" Height="22px" Width="200px" DataSourceID="ObjectDataSource4"
                                    DataTextField="country_name" DataValueField="country_id">
                                </asp:DropDownList>
                                <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="countryList"
                                    TypeName="WMSclsCooList"></asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label33" runat="server" Text="Qty"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
                                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQty"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="lbPrice0" runat="server" Text="Damages"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDamaged" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDamaged"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label36" runat="server" Text="Packages"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtpackages" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtpackages"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label37" runat="server" Text="Height"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtheight" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtheight"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label38" runat="server" Text="Width"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtWidth"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label39" runat="server" Text="Length"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLength" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtLength"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                        </tr>
                 
                        <tr>
                            <td>
                                <asp:Label ID="Label29" runat="server" Text="Vol"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVol" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtVol"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label30" runat="server" Text="Total Vol"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotVol" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtTotVol"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
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
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtGrossWt"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label34" runat="server" Text="Tot Gross Wt"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotGrossVol" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtTotGrossVol"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                               <tr>
                            <td>
                                <asp:Label ID="lbPrice" runat="server" Text="Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtItemPrice" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtItemPrice"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                <asp:Label ID="Label28" runat="server" Text="Tot Price"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTotPrice" runat="server"></asp:TextBox>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtTotPrice"
                               ValidationGroup="arrive"  ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                      
                            </td>
                            <td>
                                &nbsp;<asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  ValidationGroup="arrive"  />
                            </td>
                            <td>
                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>

        <tr> <td> <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td>
                <asp:GridView ID="GvreceiveGood" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                    OnRowCommand="GvreceiveGood_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Part Number">
                            <ItemTemplate>
                                <asp:Label ID="Label15" runat="server" Text='<%# Bind("part_number") %>'></asp:Label>
                                <asp:HiddenField ID="hdnBoeDetailID" runat="server" Value='<%# Bind("boe_detail_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Warehouse">
                            <ItemTemplate>
                                <asp:Label ID="Label12" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate>
                                <asp:Label ID="Label10" runat="server" Text='<%# Bind("location_description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO">
                            <ItemTemplate>
                                <asp:Label ID="lbponimber" runat="server" Text='<%# Bind("PO_number") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="COO">
                            <ItemTemplate>
                                <asp:Label ID="lbcountry" runat="server" Text='<%# Bind("country_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                      
                           <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Dmg">
                            <ItemTemplate>
                                <asp:Label ID="lbdmgs" runat="server" Text='<%# Bind("damaged") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pkgs">
                            <ItemTemplate>
                                <asp:Label ID="lbpckgs" runat="server" Text='<%# Bind("packages") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="Height">
                            <ItemTemplate>
                                <asp:Label ID="lbheight" runat="server" Text='<%# Bind("item_height") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText="Width">
                            <ItemTemplate>
                                <asp:Label ID="lbwidth" runat="server" Text='<%# Bind("item_width") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Length">
                            <ItemTemplate>
                                <asp:Label ID="lblength" runat="server" Text='<%# Bind("item_length") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                       
                          <asp:TemplateField HeaderText="Vol">
                            <ItemTemplate>
                                <asp:Label ID="lbvol" runat="server" Text='<%# Bind("item_volume") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Tot Vol">
                            <ItemTemplate>
                                <asp:Label ID="lbtotvol" runat="server" Text='<%# Bind("total_volume") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="GrossWt">
                            <ItemTemplate>
                                <asp:Label ID="lbgrosweight" runat="server" Text='<%# Bind("item_gross_weight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tot Wt">
                            <ItemTemplate>
                                <asp:Label ID="lbtotwt" runat="server" Text='<%# Bind("total_gross_weight") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField HeaderText="Price">
                            <ItemTemplate>
                                <asp:Label ID="lbboeid" runat="server" Text='<%# Bind("item_price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField HeaderText="Tot Price">
                            <ItemTemplate>
                                <asp:Label ID="lbtotprice" runat="server" Text='<%# Bind("total_price") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbedit" runat="server" CommandName="Editing" CommandArgument="<%# Container.DataItemIndex %>"
                                    ForeColor="Red" CausesValidation="False">
  <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
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
    </table> </td></tr>
    </table>
</asp:Content>
