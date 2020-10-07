<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="ClosedOrderReport.aspx.cs" Inherits="Intranet_orders_ClosedOrderReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
<div class="main-content-area">
        <center>
                    <table width="100%" style="border-color:#00A9F5;">
                              <tr style="height:30px;background-color:#507CD1"> <%--row 1--%>
                                 <td style="width:96%" align="center">
                                    <div class="Page-Title">
                                     Closed Orders Report
                                    </div>
                                    
                                </td>
                                <td style="width:4%;">
                                   <div class="Page-Title">
                                    &nbsp;
                                    </div>
                                </td>
                             </tr>
                    </table>
        </center>

         <table width="100%" style="border-color:#00A9F5;">
           <tr>
             <td style="width:25%"><asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label></td>
             <td style="width:25%">&nbsp;</td>
             <td style="width:25%">&nbsp;</td>
             <td style="width:25%">&nbsp;</td>
           </tr>
           <tr>
            <td style="font-weight:bold">Get Excel Report Of Closed Orders for Date Range :</td>
            <td align="right"><asp:Label ID="lblFromDate" runat="server" Text="From Date (MM-dd-yyyy)" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp; </td>
            <td align="right"><asp:Label ID="lblToDate" runat="server" Text="To Date (MM-dd-yyyy)" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp;</td>
            <td> <asp:Button ID="btnViewData" runat="server" Text="View Report" Font-Bold="True"
                                                 Width="150px" onclick="btnViewData_Click" />

            </td>
           </tr>
           <tr>
            <td><asp:Label ID="lblUseQryNow" runat="server" Text="." Visible="false"></asp:Label></td>
            <td><asp:Label ID="lblFileName" runat="server" Text="." Visible="false"></asp:Label></td>
            <td>Report View  :  Rows : &nbsp; <asp:Label ID="lblCnt" runat="server" Text="0" ForeColor="Red"></asp:Label></td>
            <td><asp:Button ID="btnReportExl" runat="server" Text="Get Excel Report" 
                    Font-Bold="True" Enabled="true"
                             Width="150px" onclick="btnReportExl_Click" />
            </td>
           </tr>
           <tr style="background:silver">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
            <tr style="background:white">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
            <tr>
              <td colspan="4">
                        <div style="overflow:auto;width:99%;background-color:White;">
                                     <asp:GridView ID="GridView1" runat="server" Width="99%" 
                                          AllowPaging="false" Font-Size="12px"
                                          CssClass="CGrid"                    
                                          AlternatingRowStyle-CssClass="alt"
                                          PagerStyle-CssClass="pgr" AllowSorting="False" AlternatingRowStyle-BackColor="#FFFFCC">
                                      </asp:GridView>
                      </div>
              </td>
           </tr>
         </table>

  </div> <%--main div--%>


</asp:Content>

