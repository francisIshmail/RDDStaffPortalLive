<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="warehouse.aspx.cs" Inherits="Intranet_WMS_warehouse" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 181px;
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
                <table class="style1">
                    <tr>
                        <td style="text-align: center">
                            <h4>
                                Warehouses
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
                                            <asp:RadioButton ID="RbtCode" runat="server" Text="By Code" 
                                                GroupName="Search" />
                                            <asp:RadioButton ID="rbtDescription" runat="server" Text="By Description" 
                                                GroupName="Search" />
                                            <asp:RadioButton ID="rbtWarehouse" runat="server" Text="By Warehouse" 
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
                        <td align="center">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnadd" runat="server" Text="Add New Warehouse" OnClick="btnadd_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trdetail" visible="false">
                        <td align="center">
                            <fieldset style="color: #33CCFF; border-color: #0000FF">
                                <legend>Warehouse Info</legend>
                                <table>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label1" runat="server" Text="Warehouse  Code"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="235px"></asp:TextBox>
                                             <asp:HiddenField ID="hdwarehouseID" runat="server" />
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label2" runat="server" Text="Description"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDescription" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label11" runat="server" Text="Warehouse Evo"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEvo" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="Label4" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSatus" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                    <td align="center">
                        <asp:GridView ID="Gvwarehouse" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="4" Width="100%" OnPageIndexChanging="Gvwarehouse_PageIndexChanging"
                            OnRowCommand="Gvwarehouse_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Warehouse Code">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdwarehouse" runat="server" Value='<%# Bind("warehouse_id") %>' />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("warehouse_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EVO Warehouse">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("warehouseEVO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbupdate" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                         ForeColor="Red"   CommandName="Updating"> <img src="../images/edit.png" title="Edit"/></asp:LinkButton>
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
