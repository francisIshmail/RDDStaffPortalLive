<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="releaseOrderDetails.aspx.cs" Inherits="Intranet_orders_releaseOrderDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">

    //Important ****** same functions are on RO and PO details page make changes to both in case need to

//    function OpenOrderImportPage(pType, pId) //param is the name of the recipient textbox
//    {
//        TheNewWin = window.open("orderImportPage.aspx?importOrderType=" + pType + "&importOrderID=" + pId.toString(), "Order Import Popup", "width=600,height=230,status=0,titlebar=0,toolbar=0,resizable=no,scrollbars=0", true);
//        TheNewWin.moveTo(100, 100);
//    }
    
    
    function callImportfun(pType) {

        //alert(pType.toString());
        var pId = document.getElementById('<%= lblOrderId.ClientID %>').innerHTML;
        //alert(pId.toString());
        OpenOrderImportPage(pType, pId);

    }
 </script>
    
     <table id="tblHead" runat="server" width="100%" style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;" bgcolor="#e2e2e2">
            
            <tr style="height:45px">
                <td style="width:15%" align="left">&nbsp;</td> 
                <td colspan="3" align="center">
                      <center>
                         <table width="100%">
                          <tr>
                            <td style="width:97%" align="center">
                               <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" Font-Names="Calibri"></asp:Label>
                            </td>
                            <td style="width:3%">
                              <a href="ViewOrdersPO.aspx?wfTypeId=10021">Back</a>
                            </td>
                          </tr>
                         </table>
                      </center>
                 </td>
            </tr>
           <tr >
                <td style="width:15%" align="left"><b>Last Updated By User :</b></td> 
                <td style="width:20%" align="left">
                   <asp:Label ID="lblLatestUser" runat="server" Font-Bold="false" Font-Size="12px" Font-Names="Calibri" ForeColor="Blue"></asp:Label>
                </td>
                <td style="width:15%" align="left"><b>Current Status :</b></td> 
                <td style="width:50%" align="left"><asp:Label ID="lblCurrStatus" runat="server" Font-Bold="false" Font-Size="12px" Font-Names="Calibri" ForeColor="Blue"></asp:Label></td>
           </tr>
           <tr>
                <td style="width:15%" align="left"><b>Latest Comments</b></td> 
                <td colspan="3" align="left">
                   <asp:Label ID="lblLatestComments" runat="server" Font-Bold="false" Font-Size="12px" Font-Names="Calibri" ForeColor="Blue"></asp:Label>
                </td>
           </tr>
    </table>

    <table id="tblTask" runat="server" width="100%" style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;" bgcolor="#e2e2e2">
           <tr style="height:55px">
            <td align="left"><b>Updatation Comments :</b></td>
            <td>
                <asp:TextBox ID="txtComments" Width="500px" MaxLength="254" runat="server" 
                    Height="50px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="right"><asp:Button ID="btnAccept" runat="server" Text="Accept" Font-Bold="True" Width="122px" onclick="btnAccept_Click"/></td>
            <td align="right"><asp:Button ID="btnDecline" runat="server" Text="Decline" Font-Bold="True" Width="128px" onclick="btnDecline_Click" /></td>
          </tr>
          <tr>
            <td align="left">&nbsp;</td>
            <td align="left">
                       <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Width="110px" onclick="btnCancel_Click" Text="Cancel Order" Visible="False" />&nbsp;
                       <asp:Button ID="btnConfirm" runat="server" Text="Confirm Order" Font-Bold="true" Width="110px" Visible="false" onclick="btnConfirm_Click" />&nbsp;
                       <asp:Button ID="btnReimport" runat="server" Text="Re-Import" Font-Bold="true" Width="110px" Visible="false" onclick="btnReimport_Click" OnClientClick="callImportfun('RO');" />&nbsp;
                       <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="110px" Font-Bold="true" Visible="false" onclick="btnEdit_Click"/>
            </td>
            <td align="right">&nbsp;</td>
            <td align="right">&nbsp;</td>
         </tr>


            <%--<tr>
                <td align="right">
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Font-Bold="true" Width="100px" Visible="false" onclick="btnConfirm_Click" /></td>

                <td align="right">
                    <asp:Button ID="btnReimport" runat="server" Text="Re-Import" Font-Bold="true" Width="100px" Visible="false" OnClientClick="callImportfun('RO');" onclick="btnReimport_Click" />
                
                </td>
           </tr>

           <tr style="height:35px">
            <td>Updatation Comments :&nbsp<asp:TextBox ID="txtComments" Width="700px" MaxLength="254" runat="server"></asp:TextBox></td>
            <td align="right"><asp:Button ID="btnEdit" runat="server" Text="Edit" Width="70px" 
                    Font-Bold="true" onclick="btnEdit_Click" /></td>
          </tr>
          <tr>
            <td colspan="2" align="left" width="60%">
                <asp:Button ID="btnAccept" runat="server" Text="Accept" Font-Bold="True" 
                    onclick="btnAccept_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnDecline" runat="server" Text="Decline" Font-Bold="True" 
                    onclick="btnDecline_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Font-Bold="True" 
                    onclick="btnCancel_Click" Text="Cancel Order" Visible="False" />
            </td>
           </tr>--%>

          <tr>
           <td align="left" valign="top"><b>File Uploads (optional) :</b></td>
           <td colspan="3">
             <asp:FileUpload ID="fileUpload1" runat="server" CssClass="" />&nbsp;
             <input type="button" onclick="AddNewRow(); return false;"  value="Browse More Files....." style="font-size: 9px" />&nbsp;
             <asp:Button ID="Button1" Text="Cancel All" runat="server" style="font-size: 9px"  />
             <div id="divFileUploads">
             </div>
           </td>
         </tr>
      </table>

    <table id="tblDetails" runat="server" width="100%" style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;">
    
    <tr>
        <td style="width:75%"></td>
        <td style="width:25%"></td>
    </tr>
   <%-- <tr>
        <td colspan="2" align="center">
            <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" Font-Names="Calibri"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td colspan="2"><asp:Label ID="lblOrderId" runat="server" Font-Bold="false" Font-Size="5px" Font-Names="Calibri" Visible="true"></asp:Label></td>
    </tr>
    <tr>
        <td style="width:75%">
            <table width="100%" style="border-collapse: collapse">
                <tr>
                    <td align="center" style="background-color:#FFE6D9; border-style: solid; border-width: 1px; font-weight:bold; width:15%; font-size: 12px;">RELEASE FORM</td>
                    <td align="center" style="width:10%">&nbsp;</td>
                    <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:12%;background-color:#FFE6D9;">CUST ORDER NO</td>
                    <td style="border-style: solid; border-width: 1px; width:13%"><asp:Label ID="lblCustOrdNo" runat="server" Text=""></asp:Label></td>
                    <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:10%; background-color:#FFE6D9; ">BANK REF</td>
                    <td style="border-style: solid; border-width: 1px; width:10%"><asp:Label ID="lblBankRef" runat="server" Text=""></asp:Label></td>
                    <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:10%; background-color:#FFE6D9; ">RO NUMBER</td>
                    <td style="border-style: solid; border-width: 1px; width:10%"><asp:Label ID="lblRONumber" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold; background-color:#FFE6D9; ">RO DATE</td>
                    <td style="border-style: solid; border-width: 1px; "><asp:Label ID="lblRODate" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </td>
        <td style="width:25%">&nbsp;</td>
    </tr>

    <tr>
        <td  style="width:75%">
            <table width="100%" border="1" style="border-collapse: collapse">
                <tr>
                    <td style="font-weight:bold">&nbsp</td>
                    <td align="center" style="font-weight:bold;background-color:#FFE6D9">BILL TO</td>
                    <td align="center" style="font-weight:bold;background-color:#FFE6D9">SHIP TO</td>
                    <td align="center" style="font-weight:bold;background-color:#FFE6D9">CONSIGNEE</td>
                    <td align="center" style="font-weight:bold;background-color:#FFE6D9">NOTIFY</td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">NAME</td>
                    <td><asp:Label ID="lblNameBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblNameShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblNameConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblNameNotify" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">ADDRESS</td>
                    <td><asp:Label ID="lblAddBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblAddShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblAddConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblAddNotify" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">CITY</td>
                    <td><asp:Label ID="lblCityBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCityShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCityConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCityNotify" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">COUNTRY</td>
                    <td><asp:Label ID="lblCntryBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCntryShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCntryConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblCntryNotify" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">CONTACT</td>
                    <td><asp:Label ID="lblContactBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblContactShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblContactConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblContactNotify" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td style="font-weight:bold;background-color:#FFE6D9">PHONE</td>
                    <td><asp:Label ID="lblPhoneBillto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblPhoneShipto" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblPhoneConsignee" runat="server" Text=""></asp:Label></td>
                    <td><asp:Label ID="lblPhoneNotify" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </td>
        <td  style="width:25%">&nbsp;</td>
    </tr>
        <tr>
        <td style="width:75%">&nbsp;</td>
        <td style="width:25%">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gridDetails1" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" Width="100%" BackColor="White" 
                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                ShowHeaderWhenEmpty="True" >
                <Columns>
                    <asp:TemplateField HeaderText="PART NO">
                        <ItemTemplate>
                            <asp:Label ID="lblPartNo1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "partNo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION">
                         <ItemTemplate>
                            <asp:Label ID="lblDetails1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "description")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="25%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VIRTUAL WHS">
                         <ItemTemplate>
                            <asp:Label ID="lblVirtualWhs" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "virtualWHS")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="QTY">
                         <ItemTemplate>
                            <asp:Label ID="lblQty1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT PRICE">
                         <ItemTemplate>
                            <asp:Label ID="lblUnitPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "unitSellingPrc")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL">
                         <ItemTemplate>
                            <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totallSellingPrc")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UNIT COST PRICE">
                         <ItemTemplate>
                            <asp:Label ID="lblunitCostPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "unitCostPrc")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TOTAL COST">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totallCostPrc")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MARGIN%">
                         <ItemTemplate>
                            <asp:Label ID="lblMargin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "margin","{0:p}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFE6D9" ForeColor="#003399" />
                <HeaderStyle BackColor="#FFE6D9" Font-Bold="True" ForeColor="black" 
                    HorizontalAlign="Left"/>
                <PagerStyle BackColor="#99CCCC" ForeColor="black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="black" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="black" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="width:75%">
            <table width="100%">
                <tr>
                    <td style="width:59%"></td>
                    <td style="width:41%">
                        <table width="100%" style="border-collapse: collapse">
                            <tr>
                                <td style="width:18%; font-weight:bold; background-color:#FFE6D9;border-style: solid; border-width: 1px"><asp:Label ID="lblTotalQty" runat="server" Text=""></asp:Label></td>
                                <td style="width:48%;font-weight:bold; background-color:#FFE6D9;border-style: solid; border-width: 1px" align="center">NET TOTAL</td>
                                <td style="width:34%;font-weight:bold; background-color:#FFE6D9;border-style: solid; border-width: 1px"><asp:Label ID="lblNetTotal" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:18%">&nbsp;</td>
                                <td style="width:48%;font-weight:bold; background-color:#FFE6D9;border-style: solid; border-width: 1px" align="center"">DISCOUNT</td>
                                <td style="width:34%; background-color:#FFE6D9;border-style: solid; border-width: 1px"><asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width:18%">&nbsp;</td>
                                <td style="width:48%;font-weight:bold; background-color:#FFE6D9;border-style: solid; border-width: 1px" align="center"">GRAND TOTAL</td>
                                <td style="width:34%; background-color:#FFE6D9;border-style: solid; border-width: 1px"><asp:Label ID="lblGrandTotal" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:25%">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:75%">
            <table id="tblDesc" runat="server" width="100%">
                <tr>
                    <td style="width:45%">
                        <table  width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td align="center" style="width:20%;font-weight:bold; background-color:#FFE6D9">TERMS</td>
                                <td align="center" style="width:10%;font-weight:bold; background-color:#FFE6D9">TICK</td>
                                <td align="center" style="width:10%;font-weight:bold; background-color:#FFE6D9">DAYS</td>
                                <td align="center" style="width:60%;font-weight:bold; background-color:#FFE6D9">DETAILED DESCRIPTION</td>
                            </tr>
                            <tr>
                                <td>CDC</td>
                                <td><asp:Label ID="lblCDCTick" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblCDCDays" runat="server" Text=""></asp:Label></td>
                                <td rowspan="5" valign="bottom"><asp:Label ID="lblDetailedDesc" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>PDC</td>
                                <td><asp:Label ID="lblPDCTick" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblPDCDays" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Cash</td>
                                <td><asp:Label ID="lblCashTick" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblCashDays" runat="server" Text=""></asp:Label></td>
                            </tr>
                             <tr>
                                <td>CAD</td>
                                <td><asp:Label ID="lblCADTick" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblCADDays" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Credit</td>
                                <td><asp:Label ID="lblCreditTick" runat="server" Text=""></asp:Label></td>
                                <td><asp:Label ID="lblCreditDays" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:5%">&nbsp;</td>
                    <td style="width:50%" valign="top">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td align="center" style="font-weight:bold; background-color:#FFE6D9">Special Shipping Instructions</td>
                                <td align="center" style="font-weight:bold; background-color:#FFE6D9">Special Packing Instructions</td>
                            </tr>
                            <tr style="height:60px">
                                <td valign="bottom"><asp:Label ID="lblSpclShippingInst" runat="server" Text=""></asp:Label></td>
                                <td valign="bottom"><asp:Label ID="lblSpclPackingInst" runat="server" Text=""></asp:Label></td>
                            </tr>
                            </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:25%">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td style="width:75%">
            <table id="tblShipment" runat="server" width="100%">
                <tr>
                    <td style="width:20%">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td colspan="2" align="center" style="font-weight:bold; background-color:#FFE6D9">MODE OF SHIPMENT</td>
                            </tr>
                            <tr>
                                <td style="width:60%">AIR</td>
                                <td style="width:40%"><asp:Label ID="lblAirMOS" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>LAND</td>
                                <td><asp:Label ID="lblLandMOS" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>SEA</td>
                                <td><asp:Label ID="lblSeaMOS" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:5%">&nbsp;</td>
                    <td style="width:20%">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td colspan="2" align="center" style="font-weight:bold; background-color:#FFE6D9">PACKING</td>
                            </tr>
                            <tr>
                                <td style="width:60%">NORMAL</td>
                                <td style="width:40%"><asp:Label ID="lblNormalPacking" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>SPECIAL</td>
                                <td><asp:Label ID="lblSpecialPacking" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>LABEL/MARK</td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:15%">&nbsp;</td>
                    <td style="width:40%" valign="top">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td style="width:80%">Cust/Fr Fwd to Pickup(ExWorks)</td>
                                <td style="width:20%"><asp:Label ID="lblExWorks" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>We Deliver to customer(FOB)</td>
                                <td><asp:Label ID="lblFOB" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>We Ship(C&F)</td>
                                <td><asp:Label ID="lblCF" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:25%">&nbsp;</td>
    </tr>

    <tr>
        <td style="width:75%">
            <table id="tblInspection" runat="server">
                <tr>
                    <td style="width:40%">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td style="width:80%">&nbsp;</td>
                                <td align="center" style="width:20%;font-weight:bold; background-color:#FFE6D9">YES/NO</td>
                            </tr>
                            <tr>
                                <td>Inspection Mandetory?</td>
                                <td><asp:Label ID="lblInspectionMand" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Doc Submission(cotecna/SGS)?</td>
                                <td><asp:Label ID="lblDocSubmission" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Chamberizing Docs/Submission to Bank?</td>
                                <td><asp:Label ID="lblChamberDocSub" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:30%">&nbsp;</td>
                    <td style="width:30%">
                        <table width="100%" border="1" style="border-collapse: collapse">
                            <tr>
                                <td align="center" style="width:80%;font-weight:bold; background-color:#FFE6D9">DOCS REDQ BY CUST</td>
                                <td style="width:20%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>AWB/BL</td>
                                <td><asp:Label ID="lblAWB" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Final Invoice</td>
                                <td><asp:Label ID="lblFinalInvoice" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>COO</td>
                                <td><asp:Label ID="lblCOO" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Load List/Pack List</td>
                                <td><asp:Label ID="lblLoadList" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width:25%">&nbsp;</td>
    </tr>
</table>

</asp:Content>

