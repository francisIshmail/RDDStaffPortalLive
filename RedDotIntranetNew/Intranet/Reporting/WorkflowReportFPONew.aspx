<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="WorkflowReportFPONew.aspx.cs" Inherits="Intranet_Reporting_WorkflowReportFPONew" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <div class="main-content-area" style="background-image: url('../images/bgimg.png');">
        <center>
            <p class="title-txt" >
               Workflow Report for FPO System</p>
            <br />
            
             <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
            
             <table width="99%" id="tblMain" runat="server" border="0">
              <tr>
                <td style="width: 35%" valign="top" >
                    
                    <table width="100%" id="Table1" runat="server" border="0">
                         <tr style="background-color:White;height:35px">
                               <td style="width: 30%" valign="bottom" align="left" >
                                  <b>Order Type</b>
                               </td>
                               <td style="width: 40%" valign="bottom" >
                                 <asp:DropDownList ID="ddlProcessType" runat="server" Width="140px" Font-Size="12px" AutoPostBack="True" onselectedindexchanged="ddlProcessType_SelectedIndexChanged" BackColor="#ccccff" Font-Bold="true" Enabled="true" ></asp:DropDownList>
                               </td>
                               <td style="width: 30%" valign="bottom" >
                                  <asp:Label ID="lblTypeSelected" runat="server" Text="" Font-Size="14px" ForeColor="Blue" Font-Bold="True"></asp:Label>
                               </td>
                         </tr>
                 
                         <tr style="background-color:White;">
                           <td colspan="3">&nbsp;</td>
                         </tr>

                         <tr style="background-color:White;height:35px">
                           <td valign="bottom" align="left" >
                              <b>Status</b>
                           </td>
                           <td valign="bottom" >
                             <asp:DropDownList ID="ddlStatus" runat="server" Width="140px" AutoPostBack="true" Font-Bold="true" BackColor="#ccccff" onselectedindexchanged="ddlStatus_SelectedIndexChanged" ></asp:DropDownList>
                           </td>
                           <td valign="bottom" >
                                <asp:CheckBox runat="server" ID="chkStatus" Text="All" Font-Bold="true" Font-Size="12px" ForeColor="Blue" Checked="True" AutoPostBack="True" oncheckedchanged="chkStatus_CheckedChanged"  />
                           </td>
                         </tr>
                         <tr style="background-color:White;height:35px">
                           <td valign="bottom" align="left" >
                             <b>BU</b>
                           </td>
                           <td valign="bottom" >
                             <asp:DropDownList ID="ddlBus" runat="server" Width="140px" AutoPostBack="true" Font-Bold="true" BackColor="#F2F2F2" onselectedindexchanged="ddlBus_SelectedIndexChanged" > </asp:DropDownList>
                           </td>
                           <td valign="bottom" >
                              <asp:CheckBox runat="server" ID="chkBUAll" Text="All" Font-Bold="true" Font-Size="12px" ForeColor="Blue" Checked="True" AutoPostBack="True" oncheckedchanged="chkBUAll_CheckedChanged"  />
                           </td>
                         </tr>

                         <tr style="background-color:White;height:35px">
                           <td valign="bottom" align="left" >
                             <b>Created By User</b>
                           </td>
                           <td valign="bottom" >
                               <asp:DropDownList ID="ddlUser" runat="server" Width="140px" AutoPostBack="true" Font-Bold="true" BackColor="#F2F2F2" onselectedindexchanged="ddlUser_SelectedIndexChanged" ></asp:DropDownList>
                               &nbsp;(<asp:Label ID="lblUsrCount" runat="server" Text="0" ForeColor="black"></asp:Label>)
                           </td>
                           <td valign="bottom" >
                            <asp:CheckBox runat="server" ID="chkAllUsers" Text="All" Font-Bold="true" Font-Size="12px" ForeColor="Blue" Checked="True" AutoPostBack="True" oncheckedchanged="chkAllUsers_CheckedChanged"  />
                           </td>
                         </tr>
                         <tr style="background-color:White;height:35px">
                           <td valign="bottom" align="left" >
                            <b>Date</b><br />(MM-DD-YYYY)
                           </td>
                           <td valign="bottom" >
                            From &nbsp;
                               <asp:TextBox ID="txtDate" runat="server" Width="90px" Font-Bold="true" 
                                        ontextchanged="txtDate_TextChanged"  AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                           </td>
                           <td valign="bottom" >
                            To &nbsp;
                            <asp:TextBox ID="txtDateTo" runat="server" Width="90px" Font-Bold="true" 
                                        ontextchanged="txtDateTo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtDateTo" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                           </td>
                         </tr>
                     </table>
                </td>
                
                <td style="width: 65%" valign="top" >
                   <table width="100%" id="Table2" runat="server" border="0">
                         <tr style="background-color:White">
                               <td style="width: 25%" valign="bottom" align="left" >
                                   <asp:Button ID="btnReport" runat="server" Text="Get Orders!!" Font-Bold="True" Width="100px" ToolTip="Click! to fetch order list for selected filters" onclick="btnReport_Click" BackColor="#333333" ForeColor="Wheat" />
                                   &nbsp;
                                   Orders List&nbsp;(<asp:Label ID="lblOrdCnt" runat="server" Text="0" ForeColor="blue" Font-Bold="true"></asp:Label>)
                               </td>
                               <td style="width: 75%" valign="bottom" >
                                    <b>Select an Order to  view escaltion details</b>
                               </td>
                         </tr>
                         <tr style="background-color:White">
                               <td colspan="2" valign="bottom" align="left" style="padding-left:2px">
                                  <asp:ListBox ID="lstOrders" runat="server" Width="99%" Height="183px" BackColor="#6B696B" ForeColor="White" SelectionMode="Single" Font-Size="10px"
                                       onselectedindexchanged="lstOrders_SelectedIndexChanged" AutoPostBack="true" Font-Bold="true" ToolTip="BU - EVO PO No. - FPO No. - PO Date - User - Systemid" >
                                     </asp:ListBox>
                               </td>
                         </tr>
                    </table>
                </td>
              </tr>
              
              <tr style="height:60px">
                <td colspan="2">
                  <b>Escalation Rows :</b> &nbsp;<asp:Label ID="lblCntRows" runat="server" Text="0" ForeColor="blue" Font-Bold="true"></asp:Label>
                  &nbsp;
                  <asp:Label ID="lblWhere" runat="server" Text="" ForeColor="blue" Visible="false"></asp:Label>
                </td>
              </tr>

              <tr>
                <td colspan="2" style="padding-left:2" valign="top">
                    <asp:GridView ID="gridRep" runat="server" Width="100%" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="2px" CellPadding="4" 
                                GridLines="Vertical" HorizontalAlign="Center" 
                                ShowHeaderWhenEmpty="True" onpageindexchanging="gridRep_PageIndexChanging" 
                                >
                                <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                    <asp:TemplateField HeaderText="autoIndex" SortExpression="autoIndex" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblautoIndex" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "autoIndex")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="action_StatusID" SortExpression="action_StatusID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblaction_StatusID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "action_StatusID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Action Stage" SortExpression="processStatusName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprocessStatusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "processStatusName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Accept/Decline" SortExpression="StatusAccept">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatusAccept" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StatusAccept")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By User" SortExpression="lastUpdatedBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastUpdatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastUpdatedBy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Recieved On" SortExpression="lastModified">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastModified" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastModified")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Aprox HRS Taken">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTimeTaken" runat="server" Text="0"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Comments" SortExpression="comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                <PagerStyle BackColor="#6B696B" ForeColor="White" HorizontalAlign="Right" Height="10px" />
                                <RowStyle BackColor="#F7F7DE"/>
                                <SelectedRowStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>

                </td>
              </tr>
            </table>
        </center>
    </div>
</asp:Content>


