<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="SapTallyExportInvoicesV1.aspx.cs" Inherits="Intranet_sapBase_SapTallyExportInvoicesV1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/adminGrid.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript">

     function getConfirmationExportInvoices() {
         return confirm('Are you sure you want to Export all the seleted invoices to destination database ?');
     }
     function getConfirmationExportCustomers() {
         return confirm('Are you sure you want to Export all  Customers to destination database ?');
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>



        <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="Export AR-Invoices to SAP Tally Database  ( Ver : 10-July-17 )"></asp:Label></div>
                </td>
              </tr>
       </table>   

    <table width="100%" style="background-color:White" border="0" cellpadding="5px">
       <tr>
        <td width="20%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
        <td width="20%">&nbsp;</td>
       </tr>

       <tr> 
            <td colspan="5" align="left" style="height:25px">
               <asp:Label ID="lblError" runat="server" Text=".." ForeColor="Red" ></asp:Label>
               &nbsp;
               <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Text=" ." ForeColor="Red"></asp:Label>
            </td>
        </tr>
        
        <tr>
         <td colspan="5"><asp:Label ID="lblCustAddedList" runat="server" Font-Bold="True" Text="" ForeColor="Green"></asp:Label></td>
        </tr>
       
       
       <tr style="background-color:#ecd9ff">
        
        <td>From Source Database : &nbsp;<asp:DropDownList ID="ddlSourceDB" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="80px">
                                    </asp:DropDownList></td>
        <td><asp:Button ID="btnGetInvList" runat="server" 
                Text="Click ! Get Exportable Invoices !" ForeColor="#990099" Font-Bold="True"  Visible="true"
                        ToolTip="Get Exportable Invoices" Height="22px" Width="200px" 
                Font-Size="10px" onclick="btnGetInvList_Click"/></td>
        <td>&nbsp;</td> 
        <td>&nbsp;</td>
        <td><asp:Button ID="btnLoadCustList" runat="server" 
                Text="Export Customers To Tally" ForeColor="#990099" 
                BackColor="Gray" BorderColor="#FF9966" Font-Bold="True" 
                ToolTip="Export Customers to Tally From TZ not in Tally" Height="22px" 
                Width="200px" Font-Size="10px" Enabled="false"  Visible="false"
                OnClientClick="return getConfirmationExportCustomers();" 
                onclick="btnLoadCustList_Click" /></td>
       </tr>

       <tr><td colspan="5">&nbsp;</td></tr>

       <tr style="background-color:#b8e7b8">
        
        <td>To Database : &nbsp;<asp:DropDownList ID="ddlDestDB" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="80px">
                                    </asp:DropDownList>
             &nbsp;Rate : &nbsp;<asp:TextBox ID="txtExchangeRate" Width="50px" runat="server" Font-Size="Small" Font-Bold="true" BackColor="#99cc00" Text=""></asp:TextBox>
                                    </td>
        <td>SAP Login  ID&nbsp;<asp:TextBox ID="txtSapUser" Width="80px" runat="server" 
                Font-Size="Small" BackColor="#66ccff" Text="manager"></asp:TextBox></td>
        <td>SAP Login PWD&nbsp;<asp:TextBox ID="txtSapPwd" Width="80px" runat="server" 
                Font-Size="Small" BackColor="#66ccff" Text="" TextMode="Password"></asp:TextBox></td>
        <td><asp:Button ID="btnExport" runat="server" Text="Click ! Export Checked Invoices Now" ForeColor="#990099" 
                BackColor="Green" BorderColor="#FF9966" Font-Bold="True"  Visible="true" ToolTip="Export Checked Invoices in the Grid below" Height="22px" 
                Width="200px" Font-Size="10px" onclick="btnExport_Click" Enabled="false" OnClientClick="return getConfirmationExportInvoices();" /></td>
        <td><asp:Label ID="lblConnectStatus" runat="server" ForeColor="#990033" Text="Not Connected" Font-Bold="true"></asp:Label></td>
       </tr>
    </table>
    <br />
    <asp:Panel ID="Panel1" runat="server" >
         <table width="100%" style="background-color:White">
            <tr>
                <td style="width:40%" align="left">Invoices Found : <asp:Label ID="lblRowCnt" runat="server" ForeColor="#990099" Text="0" Font-Bold="true"></asp:Label></td>
                <td style="width:30%" align="left"><asp:Label ID="lblNote" runat="server" ForeColor="#3333cc" Text="Note : Max Limit to export Rows at a time has been fixed at backend, so retry export untill get appropriate message " Font-Bold="true"></asp:Label></td>
                 <td width="35%" align="right">Invoices Exported : <asp:Label ID="lblExprtedCnt" runat="server" ForeColor="Green" Text="0" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3">
                     <%--grid--%>
                    <asp:GridView ID="Grid1" AutoGenerateColumns="False" CssClass="overallGrid" 
                        runat="server" Width="100%" AllowSorting="True" Enabled="false"
                        CellSpacing="1" Font-Size="11px"  onrowdatabound="Grid1_RowDataBound" >
                        <HeaderStyle CssClass="GrdHdr" Font-Underline="true"  />
                     <Columns>
                        <asp:TemplateField HeaderText="Export?">
                                 <ItemTemplate>
                                  <asp:CheckBox ID="chkExport" Text="" runat="server" Checked="true" Visible="false" Font-Bold="true" />
                                 </ItemTemplate>
                                 
                                 <ItemStyle HorizontalAlign="Center" Width="30px" />
                              </asp:TemplateField>

                               <asp:TemplateField HeaderText="DocNum" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblDocNum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocNum")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Type" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblDocType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocType")%>'></asp:Label>  
                                </ItemTemplate>
                                 <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="20px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="TaxDate" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTaxDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaxDate")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                             </asp:TemplateField>

                              <asp:TemplateField HeaderText="Line No." Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineNum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineNum")%>'></asp:Label>  
                                  /<asp:Label ID="lblTotalInvLines" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalInvLines")%>' Visible="true"></asp:Label>  
                                </ItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
                              </asp:TemplateField>
                                                  
                              <asp:TemplateField HeaderText="ItemCode" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ItemCode")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="70px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Qty" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblInvQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"InvQty")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
                              </asp:TemplateField>
                                
                              <asp:TemplateField HeaderText="Price USD" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblPriceUSD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PriceUSD")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="50px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Amt USD" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblAmtUSD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AmtUSD")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="60px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="Vat USD" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblVatUSD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VatUSD")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="60px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="LineTotal USD" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineTotalUSD" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineTotalUSD")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="60px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="CardCodeTZ" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCardCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CardCodeTZ")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="CUST TZ" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCardNameTZ" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CardNameTZ")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                            
                              <asp:TemplateField HeaderText="Exists In TLY ?" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCustExistsInTly" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustExistsInTly")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                                                          
                              <asp:TemplateField HeaderText="Remarks" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Remarks")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="320px"  />
                              </asp:TemplateField>

                              

                              <asp:TemplateField HeaderText="TrackID" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTrackID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TrackID")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="LineDescr" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineDescr")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="200px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="CustRef" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCustRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CustRef")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="SalesGLAccountCode" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblSalesGLAccountCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"SalesGLAccountCode")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="VatGroup" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblVatGroup" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VatGroup")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="VatPrcnt" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblVatPrcnt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"VatPrcnt")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                              
                              <asp:TemplateField HeaderText="slpCode" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblslpCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"slpCode")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="TallySalesAcct" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTallySalesAcct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TallySalesAcct")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="TallyVatForSalesAcct" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTallyVatForSalesAcct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TallyVatForSalesAcct")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="DebtorsAcct" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTallySundryDebtorsAcct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TallySundryDebtorsAcct")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>
                             
                              
                              
                      </Columns>
                    </asp:GridView>
                </td>
            </tr>

          </table>
    </asp:Panel>

</ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID = "cmdConnectDb" /> --%>
    </Triggers>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:18%;left:80%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImgWait.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>
</asp:Content>

