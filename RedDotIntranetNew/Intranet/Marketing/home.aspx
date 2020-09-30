<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true"
    CodeFile="home.aspx.cs" Inherits="home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="style1" width="100%">
        <tr>
            <td>
                <h2>
                   Mails Shot History
                </h2>
            </td>
        </tr>
        <tr>
            <td align="center" id="trError" runat="server" visible="false" class="Error">
                <img src="../images/ErrorMessage.png" alt="*" />
                <asp:Label ID="lbmsgErr" runat="server" Text=""></asp:Label>
            </td>
        </tr>
          <tr>
            <td colspan="4" align="center" id="trmsg" runat ="server" visible ="false" class="alert">
        <asp:Label  ID="lbmsg" runat="server" Text=""></asp:Label></td>
        </tr>

        <tr>
            <td>
         
                        <asp:GridView ID="gvsnap" runat="server" AllowPaging="True" Width="100%" AutoGenerateColumns="False"
                            CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvsnap_PageIndexChanging"
                            OnRowCommand="gvsnap_RowCommand" style="text-align: left">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="Date">
                                    <EditItemTemplate>
                                    
                                    
                                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("ModifiedDate", "{0:MMMM d, yyyy}") %>' /></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                    <asp:HiddenField ID="hdnmailshotid" Value='<%# Bind("mailshotid") %>' runat="server" />
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("ModifiedDate", "{0:MMMM d, yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sender">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("username") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BU">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("bu") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("bu") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Subject">
                                    <EditItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("suject") %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("suject") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("TargetSale") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("TargetSale") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Archieved">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("SaleArchived") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" Text='<%# Bind("SaleArchived") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Response">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Response") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("Response") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton4" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            title="view Details" CommandName="Details" ImageUrl="~/images/view.png" />
                                        &nbsp; &nbsp;
                                        <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                            title="Delete" OnClientClick="javascript:return confirm('Are you sure to proceed?');"
                                            CommandName="Deleting" ImageUrl="~/images/delete.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowModalPopup"
                            PopupControlID="divPopUp" BackgroundCssClass="popUpStyle" PopupDragHandleControlID="panelDragHandle"
                            DropShadow="true" />
                        <br />
                        <div class="popUpStyle" id="divPopUp" style="display: none;">
                            <asp:Panel runat="Server" ID="panelDragHandle" CssClass="drag">
                                Update Mail Shot Info
                            </asp:Panel>
                            <table>
                                <tr>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        <asp:Label ID="lbdate" runat="server"></asp:Label>
                                    </td>

                                    </tr>
                                    <tr>
                                    <td>
                                        Sender
                                    </td>
                                    <td>
                                        <asp:Label ID="lbSender" runat="server"></asp:Label>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        BU
                                    </td>
                                    <td>
                                        <asp:Label ID="lbu" runat="server" ></asp:Label>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        Subject
                                    </td>
                                    <td>
                                        <asp:Label ID="lbSubject" runat="server" ></asp:Label>
                                    </td>
                                    </tr>

                                <tr>
                                    <td>
                                        Tagert Sale
                                    </td>
                                    <td>
                                        <asp:Label ID="lbtarget" runat="server" ></asp:Label>
                                    </td>
                                    </tr>
                                    <tr>
                                  
                                    <td>
                                        Archived
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtArchieved" runat="server"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td>
                                        Response
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtResponse" runat="server"></asp:TextBox>
                                    </td>
                                    </tr>

                                   <tr>
                                    </td>
                                    <td> <asp:Button ID="btSave" runat="server"  OnClick ="btnSave_Click" Text="Save" />
                                    </td>
                                    <td>   <asp:Button ID="btnClose" runat="server" Text="Close" />
                                    </td>
                                </tr>
                            </table>
                         
                        </div>
                 
            </td>
        </tr>
    </table>
</asp:Content>
