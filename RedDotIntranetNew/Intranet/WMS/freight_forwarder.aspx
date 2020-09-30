<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="freight_forwarder.aspx.cs" Inherits="Intranet_WMS_freight_forwarder" %>

<%@ Register Src="UserControl/WebUserControl.ascx" TagName="WebUserControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 162px;
        }
        .style3
        {
            height: 30px;
        }
        .style4
        {
            text-align: left;
        }
        .style5
        {
            width: 100%;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="background-image: url('../images/bgimg.png');" width="100%">
        <tr>
            <td style="font-style: italic" class="style2">
                <uc1:WebUserControl ID="WebUserControl1" runat="server" />
            </td>
            <td>
                <table align="center" class="style1">
                    <tr>
                        <td style="text-align: center">
                            <h4>
                                Freight Forwarders</h4>
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
                                            <asp:RadioButton ID="RbtCompany" runat="server" Text="Company Name" 
                                               GroupName="Search" />
                                            <asp:RadioButton ID="rbtContact" runat="server" Text="By Contact" 
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
                        <td align="center" class="style3">
                        <table> <tr> <td>  <asp:Button ID="btnAdd" runat="server" Text="Add New Freight Forwarder" OnClick="btnAdd_Click1" /></td> 
                        <td> </td> 
                        <td>  
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                onclick="btnCancel_Click" Visible="False"  />  </td></tr></table>
                           
                        </td>
                    </tr>

                      <tr runat="server" id="trdetail" visible="false">
            <td align="center">
                <fieldset style="color: #33CCFF; border-color: #0000FF">
                    <legend>Freight Forwarder Info</legend>
                    <table>
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label1" runat="server" Text="Freight Forwarder Name"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFreightFowarderName" runat="server" Width="235px"></asp:TextBox>
                                <asp:HiddenField ID="hdfreight" runat="server" />
                            </td>
                            <td class="style4">
                                <asp:Label ID="Label2" runat="server" Text="Contact"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcontact" runat="server" Height="22px" Width="235px"></asp:TextBox>
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
                                <asp:Label ID="Label8" runat="server" Text="Fax">   </asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtfax" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label11" runat="server" Text="Telephone"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtphone" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                <asp:Label ID="Label4" runat="server" Text="Cell"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcell" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label3" runat="server" Text="Address1"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress" runat="server" Width="233px"></asp:TextBox>
                            </td>
                            <td class="style4">
                                <asp:Label ID="Label6" runat="server" Text="Address2"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress2" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="style5">
                                <asp:Label ID="Label5" runat="server" Text="Address3"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddress3" runat="server" Height="22px" Width="235px"></asp:TextBox>
                            </td>
                            <td class="style4">
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
                            <asp:GridView ID="gvfreightfowarder" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#3366CC" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="4" Width="100%" OnPageIndexChanging="gvfreightfowarder_PageIndexChanging"
                                OnRowCommand="gvfreightfowarder_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Company Name">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("freight_forwarder_name") %>'></asp:Label>
                                            <asp:HiddenField ID="hdfreightID" runat="server" Value='<%# Bind("freight_forwarder_id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Contact">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("contact") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cell">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("cell") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fax">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("fax") %>'></asp:Label>
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
        </tr>
    </table>
</asp:Content>
