<%@ Page Language="C#" MasterPageFile="~/reddotIntranetnew.master" AutoEventWireup="true" CodeFile="login.aspx.cs"
    Inherits="login" Title="Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--<script runat="server">

    protected void LinkbtnDwnld_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("forgot-password.aspx");
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="main-content-area">
        <center>
            <table width="700px" border="0" cellpadding="5" cellspacing="10">
                <tr style="height:100px">
                    <td colspan="2"><p class="title-txt"><b>Login</b></p></td>
                </tr>
                <tr>
                    <td style="width:80%">
                        
                        
                        <asp:Login ID="Login1" runat="server" onAuthenticate="Login1_Authenticate" 
                            TitleText="" Font-Bold="true" Font-Size="13px" Height="139px" Width="80%">
                            <LayoutTemplate>
                                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;" width="100%">
                                    <tr>
                                        <td>
                                            
                                            
                                            <table cellpadding="0">

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="UserName" runat="server" Width="150px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                            ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td> &nbsp; </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." 
                                                            ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr style="height:30px">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="RememberMe" runat="server" Text=" Remember me next time."/>
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td align="center" colspan="2" style="color:Red;">
                                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                    </td>
                                                </tr>

                                                <tr style="height:25px">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" Font-Bold="true" ValidationGroup="Login1" width="100px"/>
                                                    </td>
                                                </tr>

                                                <tr style="height:25px">
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <b>Forgot Your password??Recieve It <asp:LinkButton ID="forgotPassword" runat="server" Text="Here" href="forgot-password.aspx" OnClick="LinkbtnDwnld_Click" Font-Bold="True"/></b>
                                                    </td>
                                                </tr>

                                            </table>
                                        
                                        
                                        </td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                        </asp:Login>
                        <asp:ValidationSummary ID="ValidationSummary" runat="server" ValidationGroup="Login1" />
                    </td>
                    <td style="width:20%">
                       &nbsp;
                    </td>
                </tr>
                <tr>
                  <td style="width:20%">
                    &nbsp;
                  </td>
                  <td align="center" valign="top" style="width:80%">
                                    
                   </td>
                </tr>
            </table>
<br />
<br />
** You may only login if you are an authorised, registered dealer.  Accounts may not be shared. See <a href="/terms-of-use.aspx">Terms Of Use</a> for details.
        </center>
        <br />
    </div>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <span class="mif-vpn-lock mif-4x place-right" style="margin-top: -60px"></span>
    <%--<h3 class="text-bold" style="color: #d71313">
        Staff Login</h3>--%>
    <hr class="thin mt-4 mb-4 pb-3 bg-red">
    <p style="color: #808080">
        The portal offers a quick and easy way to interact with your tasks and various other
        responsibilities as it pertains to your job description. Get started by signing
        in.</p>
    <hr class="thin mt-4 mb-4 bg-red">
    <form action="">
    <asp:Login ID="Login1" runat="server" OnAuthenticate="Login1_Authenticate">
        <LayoutTemplate>
            <table style="border-collapse: collapse;" width="100%">
                <tr>
                    <td>
                        <table cellpadding="0">
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Style="color: #a9a9a9;
                                        font-family: raleway; font-size: 13px; padding-bottom: 5pt" Height="11">User Name :  &nbsp;&nbsp;&nbsp;&nbsp; </asp:Label>
                                    <br />
                                    <asp:TextBox ID="UserName" runat="server" Width="220px" class="input required" Height="20px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"
                                        Height="11"><span style="color:red;font-size: 20px;"> &nbsp;*</span></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Style="color: #a9a9a9;
                                        font-family: raleway; font-size: 13px; padding-bottom: 5pt" Height="11">Password  &nbsp; :  &nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                    <br />
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="220px" class="input required"
                                        Height="20px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"
                                        Height="11"><span style="color:red;font-size: 20px;"> &nbsp;*</span></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>

                                 <table>
                                <tr>
                                    <td> What is sum of</td>
                                    <td>  <asp:Label ID="LblMathCaptch" runat="server" Style="color: #f22105; 
                                         font-size: 18px;" Height="15"></asp:Label>  </td>
                                    <td> = &nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                         <asp:TextBox ID="txtCaptchInput" runat="server" Style="font-size:17px" Width="70px" class="input required" Height="20px"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                </table>

                              <%--<asp:Image ID="imgCaptcha" runat="server"/>--%>
                               <%--   <asp:ImageButton ID="btnReset" runat="server" ImageUrl="images/Captcha-Refresh.jpg" AlternateText="Refresh" 
                                       Height="25px" Width="34px" />--%>

                                   <%-- <asp:CheckBox ID="RememberMe" runat="server" Text=" Remember me next time." Style="color: #a9a9a9;
                                        font-family: raleway; font-size: 13px; padding-bottom: 5pt" /> --%>
                                </td>
                            </tr>
                             <tr>
                                <td> 
                                </td>
                                <td >
 <%--                                    <asp:Label ID="LblCaptch" runat="server" AssociatedControlID="txtCaptchInput" Style="color: #a9a9a9;
                                        font-family: raleway; font-size: 13px; padding-bottom: 5pt" Height="11">Enter Captcha  &nbsp; :  &nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
                                        <br />
                                     <asp:TextBox ID="txtCaptchInput" runat="server" Width="250px" class="input required" Height="20px"></asp:TextBox>--%>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                </td>
                                <td align="center" colspan="2" style="color: Red;">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <br />
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" Font-Bold="true"
                                        ValidationGroup="Login1" Width="100px" Height="35px" class="button large bg-red bg-crimson-hover"
                                        Style="width: 100px; color: white" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:Login>
    </form>
    <div class="form-group">
        <h4 style="font-family: Raleway; padding-bottom: 10px; color: #a9a9a9; padding-top: 30px;
            font-weight: bold">
            Forgot Your Password?</h4>
        <p style="padding-bottom: 20px">
            If you forgot your password,
            <asp:LinkButton ID="forgotPassword" runat="server" Text="Click here" href="forgot-password.aspx"   Font-Bold="True" />
            to recover and a new password will be assigned to you via email</p>
    </div>
  
</asp:Content>

