<%@ Page Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="change-password.aspx.cs"
    Inherits="changepassword" Title="Change Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content-area">
        <p class="title-txt">
            Change Password</p>
        <center>
            <table width="550px" border="0" cellpadding="5" cellspacing="10">
            <tr>
            <asp:ChangePassword ID="ChangePassword" runat="server" />
            </tr>
            </table>
        </center>
        <br />
    </div>
</asp:Content>
