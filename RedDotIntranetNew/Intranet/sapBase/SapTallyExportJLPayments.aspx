<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="SapTallyExportJLPayments.aspx.cs" Inherits="Intranet_sapBase_SapTallyExportJLPayments" %>
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
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="Export JE Payments IN & OUT SAP Tally Database using Direct Currency ( Ver : 01-Nov-17 )"></asp:Label></div>
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
                        runat="server" Width="100%" AllowSorting="True" Enabled="true"
                        CellSpacing="1" Font-Size="11px"  onrowdatabound="Grid1_RowDataBound" >
                        <HeaderStyle CssClass="GrdHdr" Font-Underline="true"  />
                     <Columns>
                        <asp:TemplateField HeaderText="Export?">
                                 <ItemTemplate>
                                  <asp:CheckBox ID="chkExport" Text="" runat="server" Checked="true" Visible="false" Font-Bold="true" />&nbsp;
                                  
                                 </ItemTemplate>
                                 
                                 <ItemStyle HorizontalAlign="Center" Width="30px" />
                              </asp:TemplateField>

                               <asp:TemplateField HeaderText="TransID" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblDocNum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"DocNum")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                               </asp:TemplateField>

                               <asp:TemplateField HeaderText="Line No." Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineNum" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineNum")%>'></asp:Label>  
                                  /<asp:Label ID="lblTotalInvLines" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TotalInvLines")%>' Visible="true"></asp:Label>  
                                </ItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="TrackID" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTrackID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TrackID")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="80px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Ref1 Hdr" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblRef1Hdr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Ref1Hdr")%>'></asp:Label>  
                                </ItemTemplate>
                                 <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="80px"  />
                              </asp:TemplateField>

                               <asp:TemplateField HeaderText="Memo" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblMemo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Memo")%>'></asp:Label>  
                                </ItemTemplate>
                                 <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="80px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="TaxDate" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblTaxDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TaxDate")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                             </asp:TemplateField>

                             <asp:TemplateField HeaderText="PostingDate" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblPostingDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PostingDate")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                             </asp:TemplateField>

                              <asp:TemplateField HeaderText="Credit" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCred" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Credit")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="50px"  />
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="Debit" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblDeb" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Debit")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="50px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="CreditSYS" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblSYSCred" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"useCredit")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="50px"  />
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="DebitSYS" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblSYSDeb" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"useDebit")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="50px"  />
                              </asp:TemplateField>


                               <asp:TemplateField HeaderText="LineMemo" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineMemo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"LineMemo")%>'></asp:Label>  
                                </ItemTemplate>
                                 <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="100px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Ref1 Line" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblRef1Line" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Ref1Line")%>'></asp:Label>  
                                </ItemTemplate>
                                 <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="100px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Account" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblAccount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Account")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="40px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="AcctName" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblAcctName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AcctName")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                             
                             <asp:TemplateField HeaderText="ShortName" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblShortName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ShortName")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="ContraAct" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblContraAct" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContraAct")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>

                               <asp:TemplateField HeaderText="BizType" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblBizType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BizType")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="ProjAtHdr" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblProjAtHdr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProjAtHdr")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="ProjAtLine" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblProjAtLine" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProjAtLine")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="ProjectFinal" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblProjectFinal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProjectFinal")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"  />
                              </asp:TemplateField>
                              <asp:TemplateField HeaderText="AutoVat" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblAutoVat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"AutoVat")%>'></asp:Label>  
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Curr" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblLineCurr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Curr")%>'></asp:Label>
                                  <asp:Label ID="lblExp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Exportable")%>'></asp:Label>    
                                </ItemTemplate>
                              <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
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

