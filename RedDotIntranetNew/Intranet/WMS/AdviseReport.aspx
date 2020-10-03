<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterWMS.master" AutoEventWireup="true"
    CodeFile="AdviseReport.aspx.cs" Inherits="Intranet_WMS_AdviseReportt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" align="center" style="background-image: url('../images/bgimg.png');">
        <tr>
            <td>
                <center>
                    <h4>
                        Advice Report</h4>
                </center>
            </td>
        </tr>
        <tr>
            <td align="center">
                <fieldset width="80%">
                    <legend>Advice Report Details</legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Marks & Numbers"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMarksNumber" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Type"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtType" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Qty"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQty" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Weight KGS"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtWeightKgs" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Volume CBM"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtVolumeCbm" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Exit Point"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExitPoint" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Destination"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDestination" runat="server" Width="235px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Text="Value (USD)"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtvalue" runat="server" Width="235px"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table>
                                 <tr> 
                                 <td> <asp:CheckBox ID="chkImport" runat="server" Text="Import" /> </td>
                                <td> <asp:CheckBox ID="chkExport" runat="server" Text="Export" /> </td>
                                 <td> <asp:CheckBox
                                ID="chkReExport" runat="server" Text="Rexport" /> </td> 
                                <td> <asp:CheckBox ID="chkFZTranfer"
                                runat="server" Text="FZTranfer" /> </td>
                                 <td> <asp:CheckBox ID="chkTemp" runat="server"
                                Text="Temporary" /> </td>
                                
                                </tr> 
                                </table> 
                                </td> 
                                </tr> 


                                <tr>                              
                                  <td colspan="4"> 
                                
                                <table> <tr> 
                                <td> <asp:CheckBox ID="chkCDRCash" runat="server" Text="CDR Cash" /> </td>
                                 <td> <asp:CheckBox ID="chkCDTBank" runat="server" Text="CDT Bank" /> </td>
                                  <td> <asp:CheckBox ID="chkDeposit" runat="server" Text="Deposit" /> </td>
                                <td> <asp:CheckBox ID="chkCreditAC" runat="server" Text="Credit A/C*" /></td> 
                                 
                          
                            </tr> </table> </td>
                        </tr>

                        <tr> <td colspan="4">  <table >  <tr>  <td> <asp:CheckBox ID="chkStanG" runat="server" Text="Stan. G*" /> </td>
                                <td> <asp:CheckBox ID="chkBankG" runat="server" Text="Bank G*" /> </td>
                                <td><asp:CheckBox ID="Chkftt" runat="server" Text="FTT" />  </td>
                                <td> <asp:CheckBox ID="Chkalcohol" runat="server" Text="Alcohol" /> </td>
                                <td> <asp:CheckBox ID="ChkOther" runat="server" Text="Other" /> </td></tr></table></td></tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnPrint" runat="server" Text="View Report" OnClick="btnPrint_Click"
                    Style="height: 26px" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
