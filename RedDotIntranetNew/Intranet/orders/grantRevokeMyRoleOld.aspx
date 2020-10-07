<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="grantRevokeMyRoleOld.aspx.cs" Inherits="Intranet_orders_grantRevokeMyRoleold" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title">Grant / Revoke Your Permissions / Rights While Out Of Office</div>
                </td>
             </tr>
    </table>

    <table width="100%" bgcolor="#FFFFEC">
        <tr style="height:50px">
            <td colspan="3" style="font-size:15px">Welcome <asp:Label ID="lblLoggedUser" runat="server" Text="" Font-Bold="true"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="width:60%">
              <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
                ForeColor="#234669" Width="100%" 
                onactivetabchanged="TabContainer1_ActiveTabChanged" AutoPostBack="true">
                <asp:TabPanel ID="tabGrant" runat="server" ScrollBars="Auto">
                    <HeaderTemplate>Grant Role</HeaderTemplate>
                    <ContentTemplate>
                    <br />
                       <table id="tblGrant" runat="server" width="100%" border="0">
                            <tr><td colspan="3">&nbsp;</td></tr>
                            <tr style="height:40px;background-color:silver;">
                              <td style="width:30%" align="center">
                               Your are currently on roles
                              </td>
                              <td style="width:27%" align="center">
                               &nbsp;
                              </td>
                              <td style="width:22%" align="center">
                               Users List
                              </td>
                              <td style="width:20%" align="center">
                               &nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td align="center">
                               <asp:RadioButtonList ID="rbListLoggedUserRoles" runat="server" ></asp:RadioButtonList>
                              </td>
                               <td align="center" style="color:Green">
                               Grant Selected Role to ...
                              </td>
                              <td align="center">
                               <asp:DropDownList ID="ddlAllUsers" runat="server" Width="150px"></asp:DropDownList>
                              </td>
                              <td  align="right">
                               <asp:Button ID="btnAssign" runat="server" Text="Grant" Width="130px" onclick="btnAssign_Click" /> 
                              </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabRevoke" runat="server" ScrollBars="Auto">
                    <HeaderTemplate>Revoke Role</HeaderTemplate>
                    <ContentTemplate>
                        <%--<table id="tblRevoke" runat="server" width="100%">
                            <tr runat="server">
                                <td style="width:50%" runat="server">
                                    Revoke Role from User&nbsp;<asp:DropDownList ID="ddlRevokeUsers" runat="server" 
                                        AutoPostBack="True" 
                                        onselectedindexchanged="ddlRevokeUsers_SelectedIndexChanged"></asp:DropDownList><br />
                                </td>
                                <td style="width:40%" runat="server">
                                    <asp:Panel ID="pnlRevokeRoles" runat="server" GroupingText="Revoke Role">
                                        <asp:RadioButtonList ID="rbListRevokeRoles" runat="server"></asp:RadioButtonList>
                                    </asp:Panel> 
                                </td>
                                <td style="width:10%" runat="server">
                                    <asp:Button ID="btnRevoke" runat="server" Text="Revoke" 
                                        OnClick="btnRevoke_Click" /> 
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblRevokeUsers" runat="server" Visible="False"></asp:Label>--%>

                        <br />
                       <table id="tblRevoke" runat="server" width="100%" border="0">
                            <tr><td colspan="3">&nbsp;</td></tr>
                            <tr style="height:40px;background-color:silver;">
                              <td style="width:30%" align="center">
                               Granted Users
                              </td>
                              <td style="width:27%" align="center">
                               &nbsp;
                              </td>
                              <td style="width:22%" align="center">
                               Revoke Role
                              </td>
                              <td style="width:20%" align="center">
                               &nbsp;
                              </td>
                            </tr>
                            <tr>
                              <td align="center">
                               <asp:DropDownList ID="ddlRevokeUsers" runat="server" AutoPostBack="True" width="150px" onselectedindexchanged="ddlRevokeUsers_SelectedIndexChanged"></asp:DropDownList>
                              </td>
                               <td align="center" style="color:Green">
                               Revoke Selected Role ...
                              </td>
                              <td align="center">
                               <asp:RadioButtonList ID="rbListRevokeRoles" runat="server"></asp:RadioButtonList>
                              </td>
                              <td  align="right">
                               <asp:Button ID="btnRevoke" runat="server" Text="Revoke" Width="130px" OnClick="btnRevoke_Click" />
                              </td>
                            </tr>
                            
                        </table>
                        <br />
                        <asp:Label ID="lblRevokeUsers" runat="server" Visible="False"></asp:Label>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        </td>
    </tr>
     <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
</table> 
</asp:Content>

