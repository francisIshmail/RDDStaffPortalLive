<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs" Inherits="IntranetNew_change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 44%;
        }
        .style2
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="main-content-area">
        <%--<p class="title-txt">
            Change Password</p>--%>

            <table width="100%">
                <tr style="height:35px">
                    <td align="center" style="width:100%;color:#797979;font-weight:bolder;font-size:x-large">
                        <asp:Label ID="lblTitle" runat="server" Text="Change Your Password" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr style="height:25px">
                    <td>
                        &nbsp;
                    </td>
                </tr>

                <tr>
                  <td style="width:100%" >
        <center>
        <asp:Panel ID="pnlChangePwd" runat="server" Width="42%"  BorderWidth="1px" BorderColor="Red" EnableTheming="true"> 
            <table width="90%" border="0" cellpadding="5" cellspacing="10" align="center">
            <tr>
                <td class="style2">
            <asp:ChangePassword ID="ChangePassword" runat="server"  style="width:100%" >
                <ChangePasswordTemplate >
                    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;width:100%;">
                        <tr>
                            <td>
                                <table cellpadding="0" width="100%">
                                    <tr>
                                        <td align="center" colspan="3" width="100%" >
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1" >
                                            <asp:Label ID="CurrentPasswordLabel" runat="server" 
                                                AssociatedControlID="CurrentPassword">Password: </asp:Label> &nbsp;
                                        </td>
                                        <td width="40%" >
                                            <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox>
                                            
                                        </td>
                                        <td width="30%">
                                                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                                                ControlToValidate="CurrentPassword" ErrorMessage="Password is required." 
                                                ToolTip="Password is required." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1" >
                                            <asp:Label ID="NewPasswordLabel" runat="server" 
                                                AssociatedControlID="NewPassword">New Password:</asp:Label> &nbsp;
                                        </td>
                                        <td width="40%" >
                                            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox>
                                        </td>
                                         <td width="30%">
                                            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                                                ControlToValidate="NewPassword" ErrorMessage="New Password is required." 
                                                ToolTip="New Password is required." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1" >
                                            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                                                AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label> &nbsp;
                                        </td>
                                        <td width="40%" >
                                            <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox>
                                        </td>
                                        <td width="30%">
                                             <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                                                ControlToValidate="ConfirmNewPassword" 
                                                ErrorMessage="Confirm New Password is required." 
                                                ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                                                ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                                                Display="Dynamic" 
                                                ErrorMessage="The Confirm New Password must match the New Password entry." 
                                                ValidationGroup="ChangePassword"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3"  >
                                           &nbsp;  <asp:Button ID="ChangePasswordPushButton" runat="server" 
                                                CommandName="ChangePassword" Text="Change Password" 
                                                ValidationGroup="ChangePassword" /> &nbsp; &nbsp;
                                        
                                            <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" 
                                                CommandName="Cancel" Text="Cancel" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ChangePasswordTemplate>
                </asp:ChangePassword>
            </td>
            </tr>

            <tr style=" height:20px">
                <td> &nbsp;
                </td>
            </tr>

            </table>
        </asp:Panel>
        </center>

                    </td>
                </tr>
            </table>

        <br />
    </div>
</asp:Content>

