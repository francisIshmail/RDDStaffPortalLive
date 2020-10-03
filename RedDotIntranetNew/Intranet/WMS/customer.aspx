<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="customer.aspx.cs" Inherits="Intranet_WMS_customer" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            text-align: left;
        }
        .style2
        {
            width: 172px;
        }
        .style3
        {
            width: 100%;
            text-align: left;
        }
        .style4
        {
            text-align: left;
        }
        .style5
        {
            width: 100%;
            text-align: left;
            height: 27px;
        }
        .style6
        {
            height: 27px;
        }
        .style7
        {
            text-align: left;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="background-image: url('../images/bgimg.png');" width="100%">
        <tr>
            <td class="style2">
                <uc1:WebUserControl ID="WebUserControl1" runat="server" />
            </td>
            <td>
                <table class="style1" align="center" style="" width="100%">
                    <tr>
                        <td style="text-align: center">
                            <h4>
                                Customer
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
                                            <asp:Label ID="Label20" runat="server" Text="By Customer"></asp:Label>
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
                                        <asp:Button ID="btadd" runat="server" Text="Add New Customer" OnClick="btadd_Click" />
                                        <td>
                                        </td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                            Visible="False" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server" id="trdetail" visible="false">
                        <td align="center" style="text-align: center">
                            <fieldset style="color: #33CCFF; border-color: #0000FF">
                                <legend>Customer Info</legend>
                                <table>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label1" runat="server" Text="Customer Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcustName" runat="server" Width="235px"></asp:TextBox>
                                            <asp:HiddenField ID="hdcustid" runat="server" />
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label2" runat="server" Text="Cell"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">
                                            <asp:Label ID="Label19" runat="server" Text="Contact"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcontact" runat="server" Width="231px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label18" runat="server" Text="Territory"></asp:Label>
                                            <span style="color: #FF0000">(3digits)</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTerritory" runat="server" Width="237px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style4">
                                            <asp:Label ID="Label9" runat="server" Text="Email"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label17" runat="server" Text="Fax">   </asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label8" runat="server" Text="Telephone"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtphone" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label11" runat="server" Text="Telephone2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtphone2" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label3" runat="server" Text="Address1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress" runat="server" Width="233px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label4" runat="server" Text="Address2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress2" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label5" runat="server" Text="Address3"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress3" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label6" runat="server" Text="Address4"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress4" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label7" runat="server" Text="Address5"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAddress5" runat="server" Height="22px" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style3">
                                            <asp:Label ID="Label12" runat="server" Text="Post1"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpost1" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style4">
                                            <asp:Label ID="Label13" runat="server" Text="Post2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpost2" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            <asp:Label ID="Label14" runat="server" Text="Post3"></asp:Label>
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="txtPost3" runat="server" Width="236px"></asp:TextBox>
                                        </td>
                                        <td class="style7">
                                            <asp:Label ID="Label15" runat="server" Text="Post4"></asp:Label>
                                        </td>
                                        <td class="style6">
                                            <asp:TextBox ID="txtpost4" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label16" runat="server" Text="Post5" Style="text-align: left"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpost5" runat="server" Width="235px"></asp:TextBox>
                                        </td>
                                        <td class="style6">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="Gvcustomer" runat="server" BackColor="White" BorderColor="#3366CC"
                                BorderStyle="Solid" BorderWidth="1px" CellPadding="4" AutoGenerateColumns="False"
                                AllowPaging="True" OnPageIndexChanging="Gvcustomer_PageIndexChanging" OnRowCommand="Gvcustomer_RowCommand"
                                OnRowEditing="Gvcustomer_RowEditing" OnRowUpdating="Gvcustomer_RowUpdating" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("customer_name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdcustid" runat="server" Value='<%# Bind("customer_id") %>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcustomer" runat="server" Text='<%# Bind("customer_name") %>'
                                                Height="16px" Width="100px"></asp:TextBox>
                                            <asp:HiddenField ID="hdcustid" runat="server" Value='<%# Bind("customer_id") %>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Contact">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("customer_contact") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcontact" Height="16px" Width="100px" runat="server" Text='<%# Bind("customer_contact") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cell">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("cell") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtcell" Height="16px" Width="100px" runat="server" Text='<%# Bind("cell") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address1">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("address1") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaddress1" Height="16px" Width="100px" runat="server" Text='<%# Bind("address1") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address2">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("address2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaddress2" Height="16px" Width="100px" runat="server" Text='<%# Bind("address2") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Address3">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("address3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtaddress3" Height="16px" Width="100px" runat="server" Text='<%# Bind("address3") %>'></asp:TextBox>
                                        </EditItemTemplate>
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
        </tr>
    </table>
</asp:Content>
