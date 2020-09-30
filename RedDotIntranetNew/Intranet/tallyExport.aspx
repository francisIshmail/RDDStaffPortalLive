<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="tallyExport.aspx.vb" Inherits="Intranet_tallyExport" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
    <tr>
        <td style="width:40%">&nbsp;</td>
        <td style="width:35%">&nbsp;</td>
        <td style="width:25%">&nbsp;</td>
    </tr>
    <tr style="height:40px;background-color:#0000CC">
        <td colspan="3" align="center"><asp:Label ID="lblTitle" runat="server" 
                Text="Tally Export" ForeColor="White" Font-Bold="True" Font-Size="Large"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
    <tr style="height:50px;">
        <td align="right"><asp:Label ID="lblCompany" runat="server" Text="Select Company" 
                ForeColor="#0000CC" Font-Bold="True" Font-Size="14px"></asp:Label></td>
        <td colspan="2">
            <asp:RadioButton ID="radioRDD" runat="server" Text="RedDot Distribution555" 
                ForeColor="#0000CC" Font-Bold="True" Font-Size="14px" GroupName="company" 
                Width="200px" AutoPostBack="True" Checked="True"/>
            <asp:RadioButton ID="radioCC" runat="server" Text="Computer Centre 444" 
                ForeColor="#0000CC" Font-Bold="True" Font-Size="14px" GroupName="company" 
                Width="200px" AutoPostBack="True" Enabled="False" />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTallyCompany" runat="server" 
                Text="Tally Company" Font-Bold="False" Font-Italic="False"></asp:Label></td>
        <td colspan="2"><asp:TextBox ID="txtCo" runat="server" Width="300px" 
                Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblInvoice" runat="server" Text="Invoice#" 
                Font-Bold="False"></asp:Label></td>
        <td>
            <asp:TextBox ID="txtInvNum" runat="server"></asp:TextBox>
            &nbsp;&nbsp;<asp:Button ID="findButton" runat="server" Text="Find" Font-Bold="true" />
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Invoice No." ControlToValidate="txtInvNum">*</asp:RequiredFieldValidator>
            <asp:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" 
                runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1" 
                PopupPosition="BottomRight">
            </asp:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblEvoCust" runat="server" Text="Evo Customer" 
                Font-Bold="False"></asp:Label></td>
        <td colspan="2"><asp:TextBox ID="custEvo" runat="server" Width="300px"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblTallyCust" runat="server" Text="Tally Customer" 
                Font-Bold="False"></asp:Label></td>
        <td><asp:TextBox ID="custTally" runat="server" Width="300px"></asp:TextBox></td>
        <td><asp:Button ID="exportButton" runat="server" Text="Export" Font-Bold="true" /></td>
    </tr>
    <tr>
        <td align="right" valign="top">
            <asp:Label ID="Label1" runat="server" 
                Text="Messages From Tally" Font-Bold="False"></asp:Label></td>
        <td colspan="2">
            <textarea id="txtHtml" cols="35" rows="8" disabled="disabled" 
                runat="server" style="color: #333399"></textarea></td>
    </tr>
    <tr style="height:50px">
        <td colspan="3" align="center">
            <asp:Label ID="lblMsg" runat="server" 
                ForeColor="Red" Height="50px" Width="450px"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>
</table>
</asp:Content>

