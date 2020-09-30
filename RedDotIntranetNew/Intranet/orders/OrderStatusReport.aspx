<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="OrderStatusReport.aspx.cs" Inherits="Intranet_orders_OrderStatusReport" %>
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
                                     Order Status Report
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
            <td align="right"><asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp; </td>
            <td align="right"><asp:Label ID="lblToDate" runat="server" Text="To Date" 
                            Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    &nbsp;</td>
            <td> <asp:Button ID="btnReportExl" runat="server" Text="Get Excel Report" 
                    Font-Bold="True" Enabled="true"
                             Width="150px" onclick="btnReportExl_Click" /></td>
           </tr>
           <tr>
            <td><asp:Label ID="lblUseQryNow" runat="server" Text="." Visible="False"></asp:Label></td>
            <td><asp:Label ID="lblFileName" runat="server" Text="." Visible="False"></asp:Label></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
           <tr style="background:silver">
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
           <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
           <tr>
            <td>Find Status For PO No.(Shows below)</td>
            <td><asp:TextBox ID="txtPONo" Text="" runat="server" BackColor="#66FFCC"></asp:TextBox></td>
            <td><asp:Button ID="btnFind" runat="server" Text="Click ! Get Status"  
                        Width="100px" Font-Size="8pt" Enabled="true" onclick="btnFind_Click" /></td>
            <td>PO Found :<asp:Label ID="lblcnt" runat="server" Text="0" Font-Size="12px" ForeColor="#660033"></asp:Label></td>
           </tr>
           <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:ListBox ID="lstOrders" runat="server" BackColor="#FFFFCC" Font-Bold="True" 
                    Font-Size="8pt" Height="66px" Width="158px" AutoPostBack="true"
                    onselectedindexchanged="lstOrders_SelectedIndexChanged"></asp:ListBox>
               </td>
           </tr>
           <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
           </tr>
           <tr>
            <td colspan="4">
                              <div style="font-size:13px;margin-top:10px">
                                        <table width="99%">
                                            <tr>
                                                <td style="width:100%">
                                                    <font color="black"><b>Escalation History:</b></font>
                                                    <br />
                                                    <asp:GridView ID="GridEscalationHistory" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%" BackColor="White" Font-Bold="true"
                                                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" CssClass="mGrid">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action Stage">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaction_Stage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "action_Stage")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="18%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Comments">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="62%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Updated By">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastUpdatedBy")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="7%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Updated On">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblUpdatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastModified","{0:MM-dd-yyyy hh:mm tt}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="11%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#FFE6D9" ForeColor="#003399" />
                                                            <HeaderStyle BackColor="#FFE6D9" Font-Bold="True" ForeColor="black" HorizontalAlign="Left"/>
                                                            <PagerStyle BackColor="#99CCCC" ForeColor="black" HorizontalAlign="Left" />
                                                            <RowStyle BackColor="White" ForeColor="Gray" />
                                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="black" />
                                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                                        </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
           </tr>
         </table>

  </div> <%--main div--%>


</asp:Content>

