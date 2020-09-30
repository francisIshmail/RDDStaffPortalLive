<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="ReleaseOrder.aspx.cs" Inherits="Intranet_orders_ReleaseOrder" EnableEventValidation="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
        <div class="main-content-area" >
        <br />
            <table style="width:99%">
                <tr>
                  <td> <p class="title-txt">Release Order Form</p></td>
                </tr>
                <tr style="height:25px;background-color:#f4f4f4">
                  <td>
                       <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                  </td>
                </tr>
                <tr>
                    <td>
                          <table width="100%" style="border-collapse: collapse">
                            <tr>
                                <td align="center" style="background-color:#FFE6D9; border-style: solid; border-width: 1px; font-weight:bold; width:15%; font-size: 12px;">RELEASE FORM</td>
                                <td align="center" style="width:10%">&nbsp;</td>
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:12%;background-color:#FFE6D9;">CUST ORDER NO</td>
                                <td style="border-style: solid; border-width: 1px; width:13%"><asp:TextBox ID="TtxtCustOrdNo" runat="server"></asp:TextBox></td>
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:10%; background-color:#FFE6D9; ">BANK REF</td>
                                <td style="border-style: solid; border-width: 1px; width:10%"><asp:TextBox ID="txtBankRef" runat="server"></asp:TextBox></td>
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:10%; background-color:#FFE6D9; ">RO NUMBER</td>
                                <td style="border-style: solid; border-width: 1px; width:10%"><asp:TextBox ID="txtRONumber" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold; background-color:#FFE6D9; ">RO DATE</td>
                                <td style="border-style: solid; border-width: 1px; "><asp:TextBox ID="txtRODate" runat="server"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>
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
                                <td><asp:TextBox ID="txtNameBillTo" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtNameShipTo" runat="server"></asp:TextBox></td>
                                <td><asp:TextBox ID="txtNameonsignee" runat="server"></asp:TextBox></td>
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
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>

                <tr style="height:35px">
                  <td valign="middle" style="font-weight:bold;background-color:#f4f4f4;padding-left:5px">
                    Supplier : &nbsp;<asp:DropDownList ID="ddlvendorGlb" Width="150px" runat="server" Visible="true" BackColor="silver"> </asp:DropDownList>
                  </td>
                </tr>
                
                <tr>
                  <td>
                      <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server"  CssClass="GridviewStyleRO" >

                      <HeaderStyle Font-Bold="true"  HorizontalAlign="Center" Height="40px"/>

                        <Columns>
                         <asp:TemplateField HeaderText="Serial">
                             <ItemTemplate> 
                                <asp:Label ID="lblSerial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Serial")%>'></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="40px" Font-Bold="true" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="PartNo">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtPartNo" Text='<%# DataBinder.Eval(Container.DataItem, "PartNo")%>' runat="server" Width="100px"></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="100px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Description">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server" MaxLength="254" Width="280px"></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="280px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Quantity">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtQty" Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server" MaxLength="4" Width="48px"></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="50px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Comments">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtComments" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' runat="server" MaxLength="254" Width="360px"></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="360px" />
                          </asp:TemplateField>
                          <asp:TemplateField>
                             <ItemTemplate> 
                                 <asp:ImageButton ID="imgBtnClose" ImageUrl="~/images/close-icon.png"  runat="server" OnClick="imgBtnClose_Click" />
                             </ItemTemplate>
                                <ItemStyle Width="40px" />
                          </asp:TemplateField>
                        </Columns>
                      </asp:GridView>
                   </td>
                  </tr>

                  <tr style="height:50px;background-color:#f4f4f4">
                   <td valign="bottom">
                    <table style="width:100%;background-color:#f4f4f4">
                     <tr>
                       <td style="width:40%;padding-left:44px" align="left" valign="middle">
                         <asp:ImageButton ID="btnSubmit" runat="server" 
                               ImageUrl="images/submit.jpg" ToolTip="Submit Request for Order List" 
                               onclick="btnSubmit_Click"  Height="20" Width="92" />
                        &nbsp;
                       </td>
                       <td style="width:60%;padding-right:13px" align="right" valign="middle">
                           <asp:ImageButton ID="btnClearAll" runat="server" 
                               ImageUrl="images/clearAll.png" ToolTip="Click! to clear data in all rows" 
                               onclick="btnClearAll_Click"  Height="35" Width="35" />
                           &nbsp;&nbsp;
                           <asp:ImageButton ID="btnAddRow" runat="server" 
                               ImageUrl="images/addRow.jpg" ToolTip="Click! to Add an additional row" 
                               onclick="btnAddRow_Click"  Height="35" Width="35" />
                       </td>
                      </tr>
                    </table>
                   </td>
                  </tr>
                  <tr>
                    <td>
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
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td>
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
                  </tr>
                   <tr>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td>
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
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                  </tr>
                  <tr>
                    <td>
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
                </tr>
            </table>
            <br />
  </div>

</asp:Content>

