<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="Supplier.aspx.cs" Inherits="Intranet_WMS_Supplier" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 199px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td class="style2">
                <uc1:WebUserControl ID="WebUserControl1" runat="server" />
            </td>
            <td>
                <table width="100%" style="margin-left: 0px">
                    <tr>
                        <td style="text-align: center">
                            <h4>
                                Supplier
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
                                            <asp:RadioButton ID="rbtContact" runat="server" Text="By Contact" GroupName="Search" />
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
                        <td align="center">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnadd" runat="server" Text="Add New Supplier" OnClick="btnadd_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" Visible="false" 
                                            onclick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trdetail" visible="false">
                        <td>
                            <fieldset style="color: #33CCFF; border-color: #0000FF">
                                <legend>Supplier Info</legend>
                                <table>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label1" runat="server" Text="Supplier  Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSupplierName" runat="server" Width="235px"></asp:TextBox>
                                            <asp:HiddenField ID="hdsupplierid" runat="server" />
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label2" runat="server" Text="Contact Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcontact" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label11" runat="server" Text="Telephone"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtphone" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label4" runat="server" Text="Cell"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label8" runat="server" Text="Fax">   </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style1">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvsupplier" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="4" Width="100%" OnPageIndexChanging="gvsupplier_PageIndexChanging"
                                OnRowCommand="gvsupplier_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdsupplier" runat="server" Value='<%# Bind("supplier_id") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("contact_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("phone_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cell">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("fax_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbupdate" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                ForeColor="Red" CommandName="Updating"> <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
                                            <asp:LinkButton ID="lbdelete" runat="server" OnClientClick="javascript:return confirm('Are you sure to proceed?');"
                                                CommandName="Deleting" CommandArgument="<%# Container.DataItemIndex %>" ForeColor="Red">
 <img src="../images/DeleteRed.png" title="Delete"/></asp:LinkButton>
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
        </tr>
    </table>
</asp:Content>
