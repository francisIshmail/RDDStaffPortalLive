<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="ReportTrackingUploads.aspx.cs" Inherits="Intranet_Reporting_ReportTrackingUploads" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <div class="main-content-area" style="background-image: url('../images/bgimg.png');">
        <center>
            <p class="title-txt" >
              Reports Upload Tracking  System</p>
            <br />
            <br />
             <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
            <br />
            <br />

            <table width="80%" id="tblMain" runat="server" border="0">
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr style="background-color:White;" valign="bottom">
                  <td style="width: 20%" valign="bottom">Report
                          &nbsp;
                      <asp:RadioButton ID="radSingle" runat="server" GroupName="reps"  
                          ForeColor="Blue" AutoPostBack="true" Text="Single" 
                          oncheckedchanged="radSingle_CheckedChanged" />&nbsp;
                      <asp:RadioButton ID="radMultiple" runat="server" GroupName="reps"  
                          ForeColor="Blue" AutoPostBack="true" Text="Multiple" 
                          oncheckedchanged="radMultiple_CheckedChanged" />
                   </td>
                  <td style="width: 15%" valign="bottom">
                     BU
                      &nbsp;<asp:Label ID="lblIsApplicable" runat="server" Text="Not Applicable" ForeColor="Silver"></asp:Label>
                  </td>
                  <td style="width: 20%" valign="bottom"><b>Select User</b>
                   <asp:CheckBox runat="server" ID="chkAllUsers" Text="All" Font-Bold="true" Font-Size="12px" ForeColor="Blue"  />
                  </td>
                  <td style="width: 15%" valign="bottom">From Date&nbsp;(MM-DD-YYYY)</td>
                  <td style="width: 30%" valign="bottom">To Date&nbsp;(MM-DD-YYYY)</td>
                  
                </tr>

                <tr style="background-color:White;" valign="top">
                   <td valign="top">
                       <asp:ListBox ID="lstRepType" runat="server" Width="180px" Height="220px" 
                          BackColor="#eaeaea" SelectionMode="Multiple" 
                          onselectedindexchanged="lstRepType_SelectedIndexChanged" AutoPostBack="true" Font-Bold="true" ToolTip="Press ctrl to select multiple items" >
                      </asp:ListBox>
                  </td>
                  <td valign="top">
                        &nbsp;
                  </td>
                  <td>
                         <asp:DropDownList ID="ddlUser" runat="server" Width="130px" AutoPostBack="false" 
                            Font-Bold="true" BackColor="#F2F2F2" 
                           > 
                        </asp:DropDownList>
                        &nbsp;Total&nbsp;(<asp:Label ID="lblUsrCount" runat="server" Text="0" ForeColor="black"></asp:Label>)
                  </td>
                  <td valign="top">
                            <asp:TextBox ID="txtDate" runat="server" Width="120px" Font-Bold="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                TodaysDateFormat="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                  </td>
                  <td valign="top">
                            <asp:TextBox ID="txtDateTo" runat="server" Width="120px" Font-Bold="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtDateTo" DaysModeTitleFormat="dd/MM/yyyy" 
                                TodaysDateFormat="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                  </td>
                 
                </tr>
                
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>

               
                <tr>
                    <td colspan="5">
                         
                        
                        <asp:Button ID="btnReport" runat="server" Text="Refresh Report!" 
                            Font-Bold="True" Width="150px" onclick="btnReport_Click" />
                        &nbsp;
                        <b>Rows :</b> &nbsp;<asp:Label ID="lblCntRows" runat="server" Text="0" ForeColor="blue" Font-Bold="true"></asp:Label>
                        &nbsp;<asp:Label ID="lblWhere" runat="server" Text="" ForeColor="blue" Visible="false"></asp:Label>

                    </td>
                </tr>
               <tr>
                    <td colspan="5" valign="top">
                        <asp:GridView ID="gridRep" runat="server" Width="100%" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="Black" 
                                BorderStyle="Solid" BorderWidth="2px" CellPadding="4" 
                                GridLines="Vertical" HorizontalAlign="Center" 
                                ShowHeaderWhenEmpty="True" onpageindexchanging="gridRep_PageIndexChanging" 
                                >
                                <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                    
                                    <asp:TemplateField HeaderText="Report Type" SortExpression="reportType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreportType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "reportType")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="BU" SortExpression="BU">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BU")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Report For Date" SortExpression="reportForDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblreportForDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "reportForDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="File Name" SortExpression="fileNameOnly">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfileNameOnly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fileNameOnly")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comments" SortExpression="comments">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="300px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Downloaded Date" SortExpression="actionDated">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactionDated" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "actionDated")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User" SortExpression="ByUser">
                                        <ItemTemplate>
                                            <asp:Label ID="lblByUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ByUser")%>'></asp:Label>
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


