<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="CreditLimitUpdater.aspx.cs" Inherits="Intranet_EVO_CreditLimitUpdater" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="main-content-area">
            <center>
            <br />
            <p class="title-txt">Manage Credit Limit Updation</p>   
            <br />
            <asp:Panel ID="PanelDetails" runat="server" GroupingText="Credit Limit Update" BackColor="#EDEDE4">
            <table width="95%">
                 <tr> 
                  <td colspan="2"><asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td> 
                 </tr>

                 <tr>  
                   <td style="width:30%" valign="bottom">
                      Select A Database
                   </td>
                   <td style="width:70%" valign="bottom"> 
                     <asp:DropDownList ID="ddlDB" runat="server" Width="280px" AutoPostBack="True" 
                           Font-Bold="true" BackColor="#F2F2F2" onselectedindexchanged="ddlDB_SelectedIndexChanged" ></asp:DropDownList>
                     &nbsp;Total&nbsp;(<asp:Label ID="lblDbCount" runat="server" Text="0" ForeColor="Black"></asp:Label>)
                     &nbsp;&nbsp;Current Exchange Rate&nbsp;[<asp:Label ID="lblRate" runat="server" Text="0" ForeColor="Red" Font-Bold="true"></asp:Label>]
                     &nbsp;<asp:Label ID="lblRateId" runat="server" Text="0" ForeColor="Green" Font-Bold="true" Visible="false"></asp:Label>
                    </td> 
                 </tr>

                 <tr> 
                  <td colspan="2">&nbsp;</td> 
                 </tr>
             
                 <tr>  
                   <td style="width:30%" valign="top">
                      Credit Limit Update History
                   </td>
                   <td style="width:70%" valign="top"> 
                       <asp:Label ID="lblNoHistMsg" runat="server" Text="There is no history for this database till now." Visible="false"></asp:Label>
                     <asp:GridView ID="Grid1" AutoGenerateColumns="False" 
                        runat="server" Width="100%" AllowSorting="false" 
                        CellSpacing="2" AllowPaging="false" PageSize="10" >
                        <FooterStyle />
                        <RowStyle  HorizontalAlign="left" VerticalAlign="Bottom" Height="30px" />
                        <PagerStyle  />
                        <SelectedRowStyle BackColor="#3366FF" />
                        <HeaderStyle  Font-Underline="false" VerticalAlign="Bottom" Height="25px" HorizontalAlign="left" />
                        <EditRowStyle BackColor="LightGreen" />
                        <Columns>

                            <asp:TemplateField HeaderText="Last Modified" SortExpression="lastModified" Visible="true">
                                <ItemTemplate> 
                                  <asp:Label ID="lbllastModified" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"lastModified","{0:MMM/dd/yyyy hh:mm:ss tt}")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="By User" SortExpression="updatedByUser">
                                <ItemTemplate> 
                                  <asp:Label ID="lblupdatedByUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"updatedByUser")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Hist ID" SortExpression="histId" Visible="false">
                                <ItemTemplate > 
                                    <asp:Label ID="lblhistId"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "histId")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="From Exchange Rate" SortExpression="prevRate">
                                <ItemTemplate > 
                                  <asp:Label ID="lblprevRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"prevRate")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="To Exchange Rate" SortExpression="currRate">
                                <ItemTemplate > 
                                  <asp:Label ID="lblcurrRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"currRate")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Database" SortExpression="databaseName" Visible="false">
                                <ItemTemplate> 
                                  <asp:Label ID="lbldatabaseName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "databaseName")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>
                            
                            
                            
                           <%-- <asp:TemplateField HeaderText="Currency ID" SortExpression="idCurrencyHist" Visible="false">
                                <ItemTemplate > 
                                  <asp:Label ID="lblidCurrencyHist" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"idCurrencyHist")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="New Rate" SortExpression="newRate"  Visible="false">
                                <ItemTemplate > 
                                  <asp:Label ID="lblnewRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"newRate")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Bold="true" ForeColor="#660033" />
                           </asp:TemplateField>--%>
                            
                        </Columns>
                        
                    </asp:GridView>
                    </td> 
                 </tr>
             
                <tr> 
                  <td colspan="2">
                  &nbsp;
                   </td> 
                 </tr>

                 <tr> 
                  <td colspan="2">
                 
                    <table width="100%">
                      <tr> 
                        <td style="width:99%" valign="bottom" align="right">
                         <asp:Button ID="btnUpdate" runat="server" Text="Update" Font-Bold="True" Width="230px" onclick="btnUpdate_Click" />
                        </td> 
                      </tr>
                    </table>
                 
                   </td> 
                 </tr>
                 
                 <tr> 
                  <td colspan="2">&nbsp;</td> 
                 </tr>

            </table>
            </asp:Panel>
            <br />

          </center>
      </div>
</asp:Content>

