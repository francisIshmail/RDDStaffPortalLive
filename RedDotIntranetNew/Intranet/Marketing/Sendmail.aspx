<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="Sendmail.aspx.cs" Inherits="Sendmail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <h2> Send Mail</h2>
    <table class="style1">
    <tr>
            <td  colspan="4" align="center" id="trError" runat="server" visible="false" class="Error">
                <img src="images/ErrorMessage.png" alt="*" />
                <asp:Label ID="lbmsgErr" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center" id="trmsg" runat ="server" visible ="false" class="alert">
        <asp:Label  ID="lbmsg" runat="server" Text=""></asp:Label></td>
        </tr>
      <%--  <tr>
            <td>
                Date</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                                            CssClass="cal_Theme1">
                                        </cc1:CalendarExtender>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>--%>
        <tr>
            <td>
                BU</td>
            <td>
                <asp:DropDownList ID="ddlBu" runat="server">
                    
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Target Sale</td>
            <td>
                <asp:TextBox ID="txttargetSale" runat="server"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttargetSale"
                                                        ErrorMessage="RegularExpressionValidator" ValidationExpression="^\d*\.?\d+$"
                                                        ValidationGroup="save">  <img 
                                                        src="images/ErrorMessage.png" alt ="*" /></asp:RegularExpressionValidator>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Country</td>
            <td>
                <asp:TextBox ID="txtCountry"  ReadOnly="true"  runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Subject </td>
            <td>
                <asp:TextBox ID="txtSuject" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Mailshot</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSend" runat="server" Text="Send" onclick="btnSend_Click" />
            </td>
            <td>
              
        </tr>
    </table>

</asp:Content>

