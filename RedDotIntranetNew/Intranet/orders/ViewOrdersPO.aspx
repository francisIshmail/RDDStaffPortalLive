<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="ViewOrdersPO.aspx.cs" Inherits="Intranet_orders_ViewOrdersPO" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table width="100%">
        <tr>
            <td>
              <asp:Panel ID="panel1ProcessType" runat="server" CssClass="POPanel4" GroupingText="Select Process & Status">
                 <table width="100%">
                  <tr>
                        <td style="width:10%">
                            <asp:Label ID="Label11" runat="server" Text="Process" Font-Size="14px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:35%">
                            <asp:DropDownList ID="ddlProcessType" runat="server" Width="350px"  
                                Font-Size="13px" AutoPostBack="True" 
                                onselectedindexchanged="ddlProcessType_SelectedIndexChanged" 
                                BackColor="Silver" Font-Bold="true" Enabled="true" ></asp:DropDownList>&nbsp;
                            <asp:CheckBox ID="chkProcessAll" runat="server"  AutoPostBack="True" 
                                oncheckedchanged="chkProcessAll_CheckedChanged" Checked="false" />
                        </td>
                        <td align="center" style="width:55%">
                            <asp:Label ID="lblTypeSelected" runat="server" Text="" Font-Size="18px" ForeColor="Blue" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:10%">
                            <asp:Label ID="Label1" runat="server" Text="Status" Font-Size="14px" ForeColor="#666666" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width:35%">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="350px" 
                                Font-Size="14px" AutoPostBack="True" 
                                onselectedindexchanged="ddlStatus_SelectedIndexChanged" BackColor="Silver" 
                                Font-Bold="true" Enabled="false" ></asp:DropDownList>&nbsp;
                            <asp:CheckBox ID="chkStatusAll" runat="server"  AutoPostBack="True" 
                                oncheckedchanged="chkStatusAll_CheckedChanged" Checked="true" />
                        </td>
                        <td align="center" style="width:55%">
                            <asp:Label ID="lblprocessSubType" runat="server" Text="BTBs" Font-Size="14px" ForeColor="Blue" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                 </table>
               </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
              <asp:Panel ID="panelWorkingRoles" runat="server" CssClass="POPanel1" GroupingText="Your are on Roles listed below  or Your are Granted rights on behaf of Users if shown in list ( Work As selected one)">
                                <table width="100%">
                                    <tr>
                                      <td style="width:70%">
                                       <asp:RadioButtonList ID="RadWorkingRoles" runat="server" 
                                            RepeatDirection="Horizontal" 
                                            onselectedindexchanged="RadWorkingRoles_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:RadioButtonList>
                                       </td>
                                      <td style="width:30%">
                                         Work As : &nbsp;<asp:Label ID="lblWorkingAs" runat="server" Text="." Font-Size="14px" ForeColor="Blue" Font-Bold="True"></asp:Label>
                                      </td>
                                    </tr>
                                      
                                </table>
               </asp:Panel>
            </td>
        </tr>

        <%--<tr>
            <td>
              <asp:Panel ID="panelWorkingRolesTemp" runat="server" CssClass="POPanel1" GroupingText="Your are Granted rights on behaf of Users below ( Work As selected one)">
                                <table width="100%">
                                    <tr>
                                      <td style="width:70%">
                                       <asp:RadioButtonList ID="RadWorkingRolesTemp" runat="server" 
                                            RepeatDirection="Horizontal" 
                                           onselectedindexchanged="RadWorkingRolesTemp_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:RadioButtonList>
                                       </td>
                                      <td style="width:30%">
                                         Work As User : &nbsp;<asp:Label ID="lblWorkingAsTemp" runat="server" Text="." Font-Size="14px" ForeColor="Blue" Font-Bold="True"></asp:Label>
                                      </td>
                                    </tr>
                                      
                                </table>
               </asp:Panel>
            </td>
        </tr>--%>

        <tr>
            <td>
                    <asp:Panel ID="panelTasks" runat="server" CssClass="POPanel3" GroupingText="Tasks">
                    <table width="100%">
                        <tr style="height:5px;">
                        <td style="width:15%"><asp:Label ID="lblworkForRole" runat="server" Text="." Font-Size="16px" Font-Bold="True" Visible="false"></asp:Label></td>
                        <td style="width:85%"><asp:Label ID="lblworkForUser" runat="server" Text="." Font-Size="16px" Font-Bold="True" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="lblTitle1" runat="server" Text="Task List" Font-Size="16px" Font-Bold="True"></asp:Label></td>
                    </tr>
                    </table>

                    <table id="tblTasks" runat="server" width="100%" style="padding:10px">
                    <tr style="height:10px;">
                        <td style="width:15%">&nbsp;</td><td style="width:85%">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2" style="padding-right:15px">
                        <asp:Label ID="Label6" runat="server" Font-Bold="false" Text="Tasks Found: "></asp:Label>
                        &nbsp;:&nbsp;<asp:Label ID="lblRecords1" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td  colspan="2">
                            <asp:GridView ID="gridTasks" runat="server" Width="100%" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="2px" CellPadding="4" 
                                GridLines="Vertical" HorizontalAlign="Center" 
                                ShowHeaderWhenEmpty="True" DataKeyNames="refId" 
                                onselectedindexchanged="gridTasks_SelectedIndexChanged" 
                                ToolTip="Double click to open order details" ForeColor="Black" 
                                onrowdatabound="gridTasks_RowDataBound" 
                                onpageindexchanging="gridTasks_PageIndexChanging" AllowSorting="True" 
                                onsorting="gridTasks_Sorting">
                                <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                    
                                    <asp:TemplateField HeaderText="Type" SortExpression="processAbbr">
                                        <ItemTemplate>
                                           <asp:Label ID="lblpid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_processId")%>' Visible="false"></asp:Label>
                                           <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "refId")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblprocessAbbr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "processAbbr")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField  HeaderText="Created By" SortExpression="createdBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdBy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                   <%-- <asp:TemplateField  HeaderText="Task For" SortExpression="RoleLevel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleLevel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RoleLevel")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="BU" SortExpression="bu">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "bu")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor" SortExpression="vendor" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvendor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vendor")%>'></asp:Label>
                                        </ItemTemplate>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer" SortExpression="customerName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustomerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "customerName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Value $" SortExpression="totalSelling">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalSelling" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totalSelling")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Process Value" SortExpression="refValue">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrefValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "refValue")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="processStatusName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprocessStatusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "processStatusName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Last Updated By" SortExpression="ByUser">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ByUser")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Last Modified" SortExpression="lastModified">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastModfied" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastModified")%>'></asp:Label>
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
                </asp:Panel>
            </td>         
        </tr>
        <tr>
            <td>
                <asp:Panel ID="panelButtons" runat="server" CssClass="POPanel2" GroupingText="Manage Orders">
                    <table width="100%">
                      <tr>
                       <td style="width:35%" align="center"><h4>Purchase Order</h4></td>
                       <td style="width:35%" align="center"><h4>Release Order</h4></td>
                       <td style="width:30%" align="center"><h4>&nbsp;</h4></td>
                      </tr>
                      <tr>
                       <td style="width:35%" align="center">
                         <asp:Panel ID="panelPOLinks" runat="server" GroupingText="">
                             <asp:LinkButton ID="lnkNewPO" runat="server" style="color:blue;font-weight:bold" onclick="lnkNewPO_Click">New PO Form</asp:LinkButton>
                         </asp:Panel>
                       </td>
                       <td style="width:35%" align="center">
                         <asp:Panel ID="panelROLinks" runat="server" GroupingText="">
                             <asp:LinkButton ID="lnkNewRO" runat="server" style="color:blue;font-weight:bold" onclick="lnkNewRO_Click">New RO Form</asp:LinkButton>
                         </asp:Panel>
                       </td>
                       <td style="width:30%" align="center">&nbsp;</td>
                      </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="panelPlansAccess" runat="server" CssClass="POPanel2" GroupingText="Manage Plans">
                    <table width="100%">
                      <tr>
                       <td align="center" style="width:50%"><h4>Marketing Plans</h4></td>
                       <td align="center" style="width:50%"><h4>Dashboard</h4></td>
                      </tr>
                      <tr>
                       <td align="center">                   
                           <asp:LinkButton ID="LinkManagePlans" runat="server" onclick="LinkManagePlans_Click" ForeColor="Blue" Font-Bold="true">Manage Marketing Plans</asp:LinkButton>
                        </td>
                       <td align="center">                   
                          <asp:LinkButton ID="LinkMKTDashboard" runat="server" onclick="LinkMKTDashboard_Click" ForeColor="Blue" Font-Bold="true">Marketing Dashboard</asp:LinkButton>
                       </td>
                      </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="panelOrders" runat="server" CssClass="POPanel1" GroupingText="View Process" >
                        <table width="100%">
                            <tr style="height:5px;">
                            <td style="width:15%"></td><td style="width:85%"></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Label ID="lblTitle" runat="server" Text="Process List" Font-Size="16px" Font-Bold="True"></asp:Label></td>
                        </tr>
                        </table>
                    <table id="tblGrid" runat="server" width="100%" style="padding:10px">
                        <tr style="height:10px;">
                            <td style="width:15%">&nbsp;</td><td style="width:85%">&nbsp;</td>
                        </tr>
     
                        <tr>
                            <td align="right" colspan="2" style="padding-right:15px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="false" Text="Found: "></asp:Label>
                            &nbsp;:&nbsp;<asp:Label ID="lblRecords" Font-Bold="true" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gridOrders" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="100%" 
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="2px"
                                    DataKeyNames="refId" 
                                    onselectedindexchanged="gridOrders_SelectedIndexChanged" 
                                    onrowdatabound="gridOrders_RowDataBound" AllowPaging="True" 
                                    onpageindexchanging="gridOrders_PageIndexChanging" 
                                    ShowHeaderWhenEmpty="True" AllowSorting="True" 
                                    onsorting="gridOrders_Sorting">
                                    <Columns>
                                    <asp:TemplateField HeaderText="Type" SortExpression="processAbbr">
                                        <ItemTemplate>
                                           <asp:Label ID="lblpid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_processId")%>' Visible="false"></asp:Label>
                                           <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "refId")%>' Visible="false"></asp:Label>
                                           <asp:Label ID="lblprocessAbbr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "processAbbr")%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Created By" SortExpression="createdBy">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "createdBy")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Task For" SortExpression="RoleLevel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleLevel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RoleLevel")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     
                                    <asp:TemplateField HeaderText="BU" SortExpression="bu">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "bu")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vendor" SortExpression="vendor"  Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvendor" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vendor")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer" SortExpression="customerName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcustomerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "customerName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Value $" SortExpression="totalSelling">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltotalSelling" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totalSelling")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Process Value" SortExpression="refValue">
                                        <ItemTemplate>
                                           <asp:Label ID="lblrefValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "refValue")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" SortExpression="processStatusName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblprocessStatusName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "processStatusName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Last Updated By" SortExpression="ByUser">
                                        <ItemTemplate>
                                            <asp:Label ID="lbluser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ByUser")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField  HeaderText="Last Modified" SortExpression="lastModified">
                                        <ItemTemplate>
                                            <asp:Label ID="lbllastModfied" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastModified")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Right" Height="10px" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
         
        
    </table>
</asp:Content>

