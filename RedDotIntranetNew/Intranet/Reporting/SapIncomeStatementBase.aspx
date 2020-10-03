<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master"
    AutoEventWireup="true" CodeFile="SapIncomeStatementBase.aspx.cs" Inherits="Intranet_Reporting_SapIncomeStatementBase" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 54%;
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

    <div class="main-content-area" style="background-image: url('../images/bgimg.png');">
        <center>
            <p class="title-txt">
               SAP Income Statement</p>
            <br />
            <table width="80%" border="0px">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 20%" valign="top">
                        <h3>
                           Database/Country</h3>
                    </td>
                    <td style="width: 80%">
                        <asp:DropDownList ID="ddlDB" runat="server" Width="170px" AutoPostBack="True" Font-Bold="true"
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
                    <td align="right"><asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp; (MM-DD-YYYY)</td>
                    
                </tr>
                <tr>
                    <td align="right"><asp:Label ID="lblToDate" runat="server" Text="To Date" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
                    <td align="left"><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp;(MM-DD-YYYY)</td>
                    
                </tr>
                <%--<tr>
                    <td style="width: 40%" valign="top">
                        <h3>
                            Select A Dealer</h3>
                    </td>
                    <td style="width: 60%">
                        <asp:ListBox ID="lstDealers" runat="server" Height="119px" Width="350px" BackColor="#F2F2F2">
                        </asp:ListBox>
                        &nbsp;Total&nbsp;(<asp:Label ID="lblDealerCount" runat="server" Text="0" ForeColor="blue"></asp:Label>)
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        
                    </td>
                    <td style="width: 80%">
                       <asp:Button ID="btnReport" runat="server" Text="Get PDF Report" Font-Bold="True"
                            Width="150px" OnClick="btnReport_Click1" />
                            
                            &nbsp;

                        <asp:Button ID="btnReportExl" runat="server" Text="Get Excel Report" Font-Bold="True"
                            Width="150px" OnClick="btnReportExl_Click1" />
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
