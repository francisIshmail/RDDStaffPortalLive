<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master"
    AutoEventWireup="true" CodeFile="statementBase.aspx.cs" Inherits="Intranet_WMS_statementBase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 54%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content-area" style="background-image: url('../images/bgimg.png');">
        <center>
            <p class="title-txt">
                Dealer Statement</p>
            <br />
            <table width="80%">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%" valign="top">
                        <h3>
                            Select A Database</h3>
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlDB" runat="server" Width="350px" AutoPostBack="True" Font-Bold="true"
                            BackColor="#F2F2F2" OnSelectedIndexChanged="ddlDB_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;Total&nbsp;(<asp:Label ID="lblDbCount" runat="server" Text="0" ForeColor="blue"></asp:Label>)
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%" valign="top">
                        <h3>
                            Select A Dealer</h3>
                    </td>
                    <td style="width: 60%">
                        <asp:ListBox ID="lstDealers" runat="server" Height="119px" Width="350px" BackColor="#F2F2F2">
                        </asp:ListBox>
                        &nbsp;Total&nbsp;(<asp:Label ID="lblDealerCount" runat="server" Text="0" ForeColor="blue"></asp:Label>)
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%">
                        <asp:Button ID="btnReport" runat="server" Text="Generate Statement" Font-Bold="True"
                            Width="200px" OnClick="btnReport_Click1" />
                    </td>
                    <td style="width: 60%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </center>
    </div>
</asp:Content>
