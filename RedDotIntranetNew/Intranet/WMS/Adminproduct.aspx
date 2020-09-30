<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Adminproduct.aspx.cs" Inherits="Intranet_WMS_Adminproduct" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td>
                <uc1:WebUserControl ID="WebUserControl1" runat="server" />
            </td>
            <td>
                <table class="style1">
                    <tr>
                        <td style="text-align: center">
                            <h4>
                                Products
                            </h4>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <asp:Label ID="lbmsg" runat="server" ForeColor="Red" Style="text-align: center" Visible="False"></asp:Label>
                            </center>
                        </td>
                    </tr>
                    <tr>
                        <td id="trSerach" runat="server" align="center">
                            <fieldset style="color: #33CCFF; border-color: #0000FF">
                                <legend>Search</legend>
                                <table align="center">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="RbtSupplier" runat="server" Text="By Supplier" GroupName="Search" />
                                            <asp:RadioButton ID="rbtpartnumber" runat="server" Text="By Part Number" GroupName="Search" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtSearch" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                            <asp:Button ID="btmSearch" runat="server" Text="Search" OnClick="btmSearch_Click"
                                                CausesValidation="False" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnadd" runat="server" Text="Add New Product" OnClick="btnadd_Click"
                                            ValidationGroup="arrive" />
                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SupplierList"
                                            TypeName="WMSclsSupplierList"></asp:ObjectDataSource>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" OnClick="btnCancel_Click"
                                            CausesValidation="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trdetail" visible="false">
                        <td>
                            <fieldset style="color: #33CCFF; border-color: #0000FF">
                                <legend>Product Info</legend>
                                <table>
                                    <tr>
                                        <td class="style2">
                                            <asp:Label ID="Label1" runat="server" Text="Part Number "></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPartNumber" runat="server" Width="235px"></asp:TextBox>
                                            <asp:HiddenField ID="hdstockID" runat="server" />
                                            <asp:RequiredFieldValidator ID="rfvref" runat="server" ControlToValidate="txtPartNumber"
                                                ErrorMessage="Required" ValidationGroup="arrive"> <img 
                                    alt="*" src="../images/ErrorMessage.png" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="style6">
                                            Pack ID
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpackid" runat="server" Width="235px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <asp:Label ID="Label9" runat="server" Text="Hscode"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHscode" runat="server" Width="235px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHscode"
                                                ErrorMessage="Required" ValidationGroup="arrive"> <img 
                                    alt="*" src="../images/ErrorMessage.png" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label8" runat="server" Text="Price">   </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtprice" runat="server" Width="235px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <asp:Label ID="Label11" runat="server" Text="Gross Weight"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGrossWeight" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style6">
                                            <asp:Label ID="Label4" runat="server" Text="Length"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLength" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            <asp:Label ID="Label3" runat="server" Text="width"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWidth" runat="server" Width="233px" Style="margin-left: 0px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                        <td class="style6">
                                            <asp:Label ID="Label6" runat="server" Text="Height"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtHeight" runat="server" Width="235px"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtprice"
                                                ValidationGroup="arrive" ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$">  <img src="../images/ErrorMessage.png" alt ="*" /><span style="color: #FF0000">Numeric</span></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
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
                                        <td class="style2">
                                            <asp:Label ID="Label5" runat="server" Text="Supplier"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddplSuppplier" runat="server" Width="235px" DataSourceID="ObjectDataSource1"
                                                DataTextField="supplier_name" DataValueField="supplier_id">
                                            </asp:DropDownList>
                                        </td>
                                      
                                            <td>
                                                &nbsp;
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="style2">
                                            Description
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtDescription" runat="server" Height="59px" Width="554px" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                                                ErrorMessage="Required" ValidationGroup="arrive"> <img 
                                    alt="*" src="../images/ErrorMessage.png" /><span style="color: #FF0000">Required</span> </asp:RequiredFieldValidator>
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
                                            <asp:HiddenField ID="HdnStockID" runat="server" Value='<%# Bind("stock_id") %>' />
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
                                    <asp:TemplateField HeaderText="Update">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbupdate" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="Red" CommandName="Updating"> <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
                                            <asp:LinkButton ID="lbdelete" runat="server" OnClientClick="javascript:return confirm('Are you sure to proceed?');"
                                                CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Red"> <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </EditItemTemplate>
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
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lbFirst" runat="server" OnClick="lbFirst_Click">First</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbNext" runat="server" OnClick="lbNext_Click">Next</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbPrev" runat="server" OnClick="lbPrev_Click">Prev</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbLAst" runat="server" OnClick="lbLAst_Click">Last</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                </td>
                </tr> </table>
</asp:Content>
