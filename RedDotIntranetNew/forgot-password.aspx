<%@ Page Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="forgot-password.aspx.cs"
    Inherits="forgotpassword" Title="Recover Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="main-content-area">
        
        
        <center>
            
            
            <table width="550px" border="0" cellpadding="5" cellspacing="10">
            
            <tr>
                <asp:PasswordRecovery ID="PasswordRecovery1" runat="server"><UserNameTemplate>
                      

                    <table>
                        <tr>
                            <td colspan="2">
                                <p class="title-txt"><b>Forgot Password?</b></p>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        
                        <tr style="height:40px">
                            <td colspan="2">
                                Enter your User Name to receive your password..
                            </td>
                        </tr>
                        
                        <tr>
                            <td width="30%">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="center" colspan="2" style="color:Red;">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="height:40px">
                                <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" ValidationGroup="PasswordRecovery1" Font-Bold="true" Width="100px" />
                            </td>
                        </tr>
                    </table>
                    </UserNameTemplate>
                </asp:PasswordRecovery>

            <%--<asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
        onsendingmail="PasswordRecovery1_SendingMail">
    </asp:PasswordRecovery>--%>

            </tr>
            </table>
        </center>
        <br />
    </div>
</asp:Content>
