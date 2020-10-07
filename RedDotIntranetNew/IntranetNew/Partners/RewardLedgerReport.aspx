<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="RewardLedgerReport.aspx.cs" Inherits="IntranetNew_Partners_RewardLedgerReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" colspan="2">
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Reward Points Ledger Report </Lable>
    </td>
</tr>

<tr>
        <td width="50%">
           &nbsp;
        </td>
        <td width="50%">
        &nbsp;
        </td>
</tr>

<tr>
        <td width="100%" align="center">
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:#be19c1; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlRewardLedgerRpt" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
     
    <tr>
        <td width="30%">
           &nbsp;
        </td>
        <td width="35%">
        &nbsp;
        </td>
        <td width="35%">
        &nbsp;
        </td>
    </tr>

    <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> SAP Database   &nbsp;  </label>
        </td>
        <td width="35%" >
           
                <asp:DropDownList ID="ddlDatabase" runat="server" AutoPostBack="True" 
                    style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                    onselectedindexchanged="ddlDatabase_SelectedIndexChanged"  >  </asp:DropDownList>
        </td>
         <td width="35%" >
        </td>
    </tr>

     <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> SAP Customer   &nbsp;  </label>
        </td>
        <td width="35%" >
           
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" 
                    style="width:95%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                    onselectedindexchanged="ddlCustomer_SelectedIndexChanged"  >  </asp:DropDownList>
        </td>
         <td width="35%" >
            <asp:TextBox ID="txtCardCode" runat="server" Enabled="false"
                   style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
    </tr>

  <tr>
        <td width="30%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Date  &nbsp; </label>  
        </td>
        <td width="35%">
            <asp:TextBox ID="txtFromDt" runat="server" placeholder="MM/DD/YYYY" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
             <cc1:CalendarExtender ID="txtFromDt_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtFromDt" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
        <td width="35%">
            <asp:TextBox ID="txtToDt" runat="server"  placeholder="MM/DD/YYYY" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
            <cc1:CalendarExtender ID="_txtToDtCalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtToDt" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>
     
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>

     <tr>
        <td width="100%" align="left" colspan="3">
      
            &nbsp;&nbsp; &nbsp;&nbsp; 
             <asp:Button ID="BtnGetLedger" runat="server" Text="Get Ledger" 
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnGetLedger_Click" />  &nbsp;&nbsp; 

            <asp:Button ID="BtnDownloadLedger" runat="server" Text="Download Ledger" 
                
                style="width:25%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnDownloadLedger_Click"  />  &nbsp;&nbsp; 

           <asp:Button ID="BtnGoBack" runat="server" Text="Go Back" 
                style="width:15%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  />
       
        
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

<tr>
    <td> &nbsp;</td>
</tr>
   <tr>
         <td align="center" width="70%" align="center" style="color:#797979;font-weight:bolder;font-size:medium"> <asp:Label  ID="lblRewardSummery" runat="server" Text="Reward Summary" ></asp:Label> </td>
   </tr>

    <tr >
        <td width="100%"  align="center" >
         
            <%--<asp:Panel ID="pnlQrterlyRow" runat="server" Width="90%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">--%>
                   <asp:GridView ID="grvRewardSummery" runat="server" 
                       AutoGenerateColumns="False" ShowFooter="false" 
                        ForeColor="#333333"  Width="70%" CellPadding="4"  GridLines="None"   >
                       <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Reward Status" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblRewardPointStatus" runat="server" Text='<%#Eval("RewardPointStatus")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Credit Points" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblCreditPoints" runat="server" Text='<%#Eval("CreditPoints")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Debit Points" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblDebitPoints" runat="server" Text='<%#Eval("DebitPoints")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                          <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Balance Points" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblBalancePoints" runat="server" Text='<%#Eval("BalancePoints")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     
                      <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Credit Amt" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblCreditAmount" runat="server" Text='<%#Eval("CreditAmount")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Debit Amt" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblDebitAmount" runat="server" Text='<%#Eval("DebitAmount")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                          <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Balance Amt" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblBalanceAmount" runat="server" Text='<%#Eval("BalanceAmount")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>

                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" 
                           HorizontalAlign="Center" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
          
        </td>
    </tr>

    <tr>
    <td>   &nbsp; </td>
    </tr>

     <tr>
         <td align="center"  width="70%" style="color:#797979;font-weight:bolder;font-size:medium"> <asp:Label  ID="LblRewardLedger" runat="server" Text="Reward Ledger" ></asp:Label> </td>
    </tr>


     <tr >
        <td width="100%"  align="center" > 
                   <asp:GridView ID="grvDetailLedger" runat="server" 
                       AutoGenerateColumns="False" ShowFooter="false" 
                        ForeColor="#333333"  Width="70%" CellPadding="4"  GridLines="None"   >
                       <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:TemplateField ItemStyle-Width="6%"  HeaderText="DocNum" HeaderStyle-Font-Size="Smaller" >
                            <ItemTemplate>
                                <asp:Label ID="lblDocNum" runat="server" Text='<%#Eval("DocNum")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="6%"  HeaderText="DocDate" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblDocDate" runat="server" Text='<%#Eval("DocDate")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="6%"  HeaderText="TransType" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionType" runat="server" Text='<%#Eval("TransactionType")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Reward Status" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblPointStatus" runat="server" Text='<%#Eval("PointStatus")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Credit Points" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblCreditPoints" runat="server" Text='<%#Eval("CreditPoints")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Debit Points" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblDebitPoints" runat="server" Text='<%#Eval("DebitPoints")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                     
                      <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Credit Amt" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblCreditAmount" runat="server" Text='<%#Eval("CreditAmount")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Debit Amt" HeaderStyle-Font-Size="Smaller">
                            <ItemTemplate>
                                <asp:Label ID="lblDebitAmount" runat="server" Text='<%#Eval("DebitAmount")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Font-Size="Smaller"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>

                <FooterStyle BackColor="#990000" Font-Bold="false" ForeColor="White" 
                           HorizontalAlign="Center" />
                <HeaderStyle BackColor="#990000" Font-Bold="false" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="false" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
        </td>
    </tr>



</table>



</asp:Content>

