<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="sellOut.aspx.vb" Inherits="Intranet_sellOut" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <table width="100%" bgcolor="#CCCCCC">
        <tr>
            <td style="width:40%">&nbsp;
            </td>
            <td style="width:20%">&nbsp;</td>
            <td style="width:40%">&nbsp;</td>
        </tr>
        <tr style="height:30px">
            <td colspan="3" align="center" style="background-color: Maroon"><asp:Label ID="lblTitle" runat="server" Text="Sell Out Report" ForeColor="White" Font-Size="20px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" align="left">
                <asp:Label ID="lblMsg" runat="server" ForeColor="#990033" 
                    Font-Size="13px" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
            <td align="right"><asp:Label ID="lblSupplier" runat="server" Text="Supplier" 
                    Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
            <td align="left">
                <asp:DropDownList ID="ddlSupplier" runat="server" Width="152px" 
                    AutoPostBack="True">
                    <asp:ListItem>APC</asp:ListItem>
                    <asp:ListItem>Logitech</asp:ListItem>
                    <asp:ListItem>Microsoft</asp:ListItem>
                    <asp:ListItem>Samsung</asp:ListItem>
                    <asp:ListItem>Toshiba</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td valign="top"><%--<asp:TextBox ID="txtStatus" runat="server" 
                    Height="55px" TextMode="MultiLine" Width="261px" Enabled="False"></asp:TextBox>--%></td>
        </tr>
        <tr>
            <td align="right"><asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                    Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
            <td align="left"><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                    TodaysDateFormat="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <%--<asp:ImageButton ID="btnFromDate" runat="server" ImageUrl="~/images/cal_btn.gif" />--%>
            &nbsp; (MM-DD-YYYY)</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right"><asp:Label ID="lblToDate" runat="server" Text="To Date" 
                    Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
            <td align="left"><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                    TodaysDateFormat="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <%--<asp:ImageButton ID="btnToDate" runat="server" ImageUrl="~/images/cal_btn.gif" />--%>
            &nbsp;(MM-DD-YYYY)</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnGenerate" runat="server" Text="Generate Report" 
                    Font-Bold="True"  ForeColor="Black" Font-Size="13px"/>
            </td>
            <td>
                <asp:LinkButton ID="lnkDwld" runat="server" Visible="False" Font-Bold="True" 
                    PostBackUrl="#">Download Report Now</asp:LinkButton>
            </td>
        </tr>
       
        <tr>
            <td colspan="3" align="left">Errors : <br />
             <asp:Label ID="lblErrors" runat="server" ForeColor="Red" Font-Size="13px"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;</td>
        </tr>   
    </table>
</asp:Content>

