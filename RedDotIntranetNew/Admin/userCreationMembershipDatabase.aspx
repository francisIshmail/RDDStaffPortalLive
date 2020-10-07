<%@ Page Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="userCreationMembershipDatabase.aspx.cs" Inherits="Admin_userCreationMembershipDatabase" Title="Manage Users & Roles Wizard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

         function getConfirmation() {
            return confirm('Are you sure you want to perfom this action ?');
        }
        </script>
    <%--<center>--%>
        <style type="text/css">
            fieldset {border:1px solid red} /*this is the border color*/
            legend {color:#507CD1} /* this is the GroupingText color */
            legend {font-weight:bold}
        </style>

      <table style="width:100%;background-color:white;padding-left:0px;padding-right:0px" >
         
         <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
            <td>
                <div class="Page-Title">Roles & Users Creation Wizard for Membership Database</div>
            </td>
         </tr>

         <tr>
            <td>
                <table width="60%">
                        
                        <tr style="height:53px">
                            <%--<td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgDepartment" runat="server" ImageUrl="~/Admin/images/pluss.png" />
                                        </td>
                                        <td style="background: url(/Admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp; <font color="White"><b>Manage Departments</b></font>
                                        </td>
                                    </tr>
                                </table>     
                            </td>--%>

                            <td style=" width:5%">
                                &nbsp;
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgRoles" runat="server" ImageUrl="~/Admin/images/pluss.png" onclick="imgRoles_Click" />
                                        </td>
                                        <td style="background: url(/Admin/images/buttonBack.png) no-repeat 0 0;">                 <%--style="background: url(/Admin/images/buttonBack.png) no-repeat 0 0;"--%>
                                            &nbsp; <asp:Label runat="server" ID="lblRoles" Text="Manage Roles" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage Roles</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgUsers" runat="server" ImageUrl="~/Admin/images/pluss.png" onclick="imgUsers_Click" />
                                        </td>
                                        <td style="background: url(/Admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp; <asp:Label runat="server" ID="lblUsers" Text="Manage Users" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="white"><b>Manage Users</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgUserRoles" runat="server" 
                                                ImageUrl="~/Admin/images/pluss.png" onclick="imgUserRoles_Click" />                   
                                        </td>
                                        <td style="background: url(/Admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp; <asp:Label runat="server" ID="lblUserRoles1" Text="Manage User Roles" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage User Roles</b></font></td>--%>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
            </td>
         </tr>
       
         <tr style="height:20px"> <%--row 2--%>
            <td style="padding-left:6%">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="#990099" Font-Bold="true"></asp:Label>
            </td>
         </tr>

         <tr style="visibility:hidden"> <%--row 3--%>
            <td style="padding-left:20px"">Enter Your Access Password&nbsp;
                <asp:TextBox ID="txtPwd" runat="server" Text="" Width="100px" BackColor="#996633" TextMode="Password" MaxLength="10"></asp:TextBox>(10 Chars Long)
             </td>
        </tr>
        </table>

        <table width="100%">
            <tr>
                <td style="width:5%" rowspan="3">    <%--Centralising Whole Page--%>
                    &nbsp;
                </td>
                <td>
                    <asp:Panel ID="pnlRoles" runat="server" Visible="false">
                        <table width="100%">

                            <%--<tr style="height:40px;background-color:Gray" >
                                <td colspan="2" align="center">
                                    <h3><b>Manage Roles</b></h3>
                                </td>
                            </tr>--%>
                            <tr> <%--row 4--%>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel1" runat="server" Height="150px" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center"><h3>Create Roles</h3></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Enter Your New Role
                                                </td>
                                                <td style="width:60%">
                                                    <asp:TextBox ID="txtRole" runat="server"  Width="170px" BackColor="#6cc5ff" BorderColor="#83C100"></asp:TextBox>
                                                    <asp:Button ID="btnRole" runat="server" Text="Create Role" onclick="btnRole_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel11" runat="server" BorderWidth="1px" Height="150px" BorderColor="Red" >
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center"><h3>Remove Roles</h3></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Remove Selected Role
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlDeleteRole" runat="server" Width="170px" BackColor="Silver" Font-Bold="true" ></asp:DropDownList>&nbsp;
                                                    Roles : (<asp:Label ID="lblDeleteRoleCount" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                    <asp:Button ID="btnDeleteRole" runat="server" Text="Remove Role" onclick="btnDeleteRole_Click"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td rowspan="3" style="width:5%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel id="pnlUsers" runat="Server" Visible="false">
                        <table width="100%">
                            <tr>  <%--row 5--%>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" Height="200px"  BorderColor="Red" >
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center"><h3>Create Users (Mail will be sent to User)</h3></td>
                                            </tr>
                                            <tr>
                                                <td style="width:45%">
                                                    Select Role From Registered Role List
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlRoles" runat="server" Width="170px" BackColor="Silver" Font-Bold="true" ></asp:DropDownList>
                                                    &nbsp;
                                                    Roles : (<asp:Label ID="lblRoleCount" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Enter User Name
                                                </td>
                                                <td style="width:60%">
                                                    <asp:TextBox ID="txtUser" runat="server"  Width="170px" BackColor="#6cc5ff"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Enter User Email
                                                </td>
                                                <td style="width:60%">
                                                    <asp:TextBox ID="txtEmail" runat="server"  Width="170px" BackColor="#6cc5ff"></asp:TextBox>
                                                    &nbsp;<asp:Button ID="BtnUser" runat="server" Text="Create User" onclick="BtnUser_Click" />
                                                </td>
                                            </tr>
                                             <tr>
                                                <td colspan="2">
                                                    <b>User password is : </b>
                                                    &nbsp;<asp:TextBox ID="txtUserPassword" runat="server"  Width="200px" BackColor="White" ForeColor="Red"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel22" runat="server" BorderWidth="1px" Height="200px"  BorderColor="Red" >
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center"><h3>Remove Users</h3></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%" valign="top">
                                                    Remove Selected User
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlDeleteUser" runat="server" Width="170px" BackColor="Silver" Font-Bold="true" ></asp:DropDownList>
                                                    &nbsp;
                                                    Roles : (<asp:Label ID="lblDeleteUser" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                    <asp:Button ID="btnDeleteUser" runat="server" Text="Remove User" onclick="btnDeleteUser_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                 <asp:Label ID="lblErrorMsg" runat="server" Text="" ForeColor="Red" Font-Size="9px" Font-Bold="true"></asp:Label>
                                                 <br />
                                                    Enter new password to be &nbsp;<asp:TextBox ID="txtUserPassword1" ToolTip="Minimum 8 Chars from  [a-z , 0-9 , * ] , at least one special char is a must" runat="server" Text=""  Width="170px" BackColor="White" ForeColor="Red"></asp:TextBox>
                                                    <br />
                                                    <asp:LinkButton ID="lnkRestPassword" runat="server" Text="Click ! Reset Password" 
                                                        onclick="lnkRestPassword_Click" OnClientClick="return getConfirmation();" 
                                                        Font-Bold="True" ForeColor="#009900" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlUserRoles" runat="server" Visible="false">
                        <table width="100%">
                            <tr>  <%--row 6--%>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel3" runat="server" BorderWidth="1px" Height="245px" BorderColor="Red"  >
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center">
                                                    <h3>Assign Roles To Users</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Assign Selected Role
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlRoles2" runat="server" Width="170px" BackColor="#9999ff" Font-Bold="true" ></asp:DropDownList>
                                                    &nbsp;
                                                    Roles : (<asp:Label ID="lblRoleCount2" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    To Selected User
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlUser2" runat="server" Width="170px" BackColor="#cc6699" Font-Bold="true" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlUser2_SelectedIndexChanged"></asp:DropDownList>
                                                    &nbsp;
                                                    Users : (<asp:Label ID="lblUserCount2" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Existing Roles for Selected User are 
                                                </td>
                                                <td style="width:60%">
                                                    <br />
                                                    <asp:DropDownList ID="ddlUserRoles" runat="server" Width="170px" BackColor="White" Font-Bold="true"></asp:DropDownList>&nbsp;
                                                    Users Roles : (<asp:Label ID="lblUserRoles" runat="server" Text="0" ForeColor="Green" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr style="height:40px">
                                                <td style="width:40%">&nbsp;</td>
                                                <td style="width:60%;padding-right:85px" align="right">
                                                    <asp:Button ID="btnAssignUserRoles" runat="server" Text="Assign Role To User" onclick="btnAssignUserRoles_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td style="width:50%;padding-left:3px;padding-right:3px">
                                    <asp:Panel ID="Panel33" runat="server" BorderWidth="1px" Height="245px" BorderColor="Red"  >
                                        <table style="width:99%;padding-left:10px;vertical-align:bottom">
                                            <tr style="height:80px">
                                                <td colspan="2" align="center"><h3>Un-Assign Roles To Users</h3></td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    From Selected User
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlUserUnAssign" runat="server" Width="170px" BackColor="#cc6699" Font-Bold="true" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlUserUnAssign_SelectedIndexChanged" ></asp:DropDownList>
                                                    &nbsp;
                                                    Users : (<asp:Label ID="lblUserCountUnAssign" runat="server" Text="0" ForeColor="Blue" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width:40%">
                                                    Seleted Role To Un Assign
                                                </td>
                                                <td style="width:60%">
                                                    <asp:DropDownList ID="ddlUserRolesUnAssign" runat="server" Width="170px" BackColor="Gray" Font-Bold="true"></asp:DropDownList>
                                                    &nbsp;
                                                    Users Roles : (<asp:Label ID="lblUserRolesUnAssign" runat="server" Text="0" ForeColor="Green" Font-Bold="true"></asp:Label>)
                                                </td>
                                            </tr>
                                            <tr style="height:40px">
                                                <td style="width:40%">&nbsp;</td>
                                                <td style="width:60%;padding-right:85px" align="right">
                                                    <asp:Button ID="btnUnAssignUserRoles" runat="server" Text="Un-Assign Role To User" onclick="btnUnAssignUserRoles_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
 
      <%--</center>--%>
  </asp:Content>
