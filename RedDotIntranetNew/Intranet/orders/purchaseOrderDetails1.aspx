<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="purchaseOrderDetails1.aspx.cs" Inherits="Intranet_orders_purchaseOrderDetails1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="../../MsgBoxControl.ascx" tagname="MsgBoxControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <style type="text/css">
        
        /*Accordian*/
        
            .accordionContent 
            {
                background-color: #D3DEEF;
                border-color: -moz-use-text-color #2F4F4F #2F4F4F;
                border-right: 1px dashed #2F4F4F;
                border-style: none dashed dashed;
                border-width: medium 1px 1px;
                padding: 10px 5px 5px;
                width:99%;
            }
            
            .accordionHeaderSelected 
            {
                background-color: #5078B3;
                border: 1px solid #2F4F4F;
                color: white;
                cursor: pointer;
                font-family: Arial,Sans-Serif;
                font-size: 12px;
                font-weight: bold;
                margin-top: 5px;
                padding: 5px;
                width:99%;
            }
            .accordionHeader 
            {
                background-color: #2E4D7B;
                border: 1px solid #2F4F4F;
                color: white;
                cursor: pointer;
                font-family: Arial,Sans-Serif;
                font-size: 12px;
                font-weight: bold;
                margin-top: 5px;
                padding: 5px;
                width:99%;
            }
            .href
            {
                color:White; 
                font-weight:bold;
                text-decoration:none;
                width:100%;
            }
            
            
        /* Accordian Closed */
        
        
        .mGrid {   
            width: 100%;   
            background-color: #fff;   
            margin: 5px 0 10px 0;   
            border: solid 1px #525252;   
            border-collapse:collapse;   
            font-size:13px;
            font-weight:bold;
        }  
        .mGrid td {   
            padding: 2px;   
            border: solid 1px #c1c1c1;   
            color: #717171;   
        }  
        .mGrid th {   
            padding: 4px 2px;   
            color: #fff;   
            background: #424242 url(grd_head.png) repeat-x top;   
            border-left: solid 1px #525252;   
            font-size: 0.9em;   
        }  
        .mGrid .alt { background: #fcfcfc url(grd_alt.png) repeat-x top; }  
        .mGrid .pgr { background: #424242 url(grd_pgr.png) repeat-x top; }  
        .mGrid .pgr table { margin: 5px 0; }  
        .mGrid .pgr td {   
            border-width: 0;   
            padding: 0 6px;   
            border-left: solid 1px #666;   
            font-weight: bold;   
            color: #fff;   
            line-height: 12px;   
         }     
        .mGrid .pgr a { color: #666; text-decoration: none; }  
        .mGrid .pgr a:hover { color: #000; text-decoration: none; }
    </style>

    <script language="javascript" type="text/javascript">

        //Important ****** same functions are on RO and PO details page make changes to both in case need to

        //function OpenOrderImportPage(pType, pId) //param is the name of the recipient textbox
        //{
        //    TheNewWin = window.open("orderImportPage.aspx?importOrderType=" + pType + "&importOrderID=" + pId.toString(), "Order Import Popup", "width=600,height=230,status=0,titlebar=0,toolbar=0,resizable=no,scrollbars=0", true);
        //    TheNewWin.moveTo(100, 100);
        //}

        function callImportfun(pType) {
            var pId = document.getElementById('<%= lblOrderId.ClientID %>').innerHTML;
            //alert(pId.toString());
            OpenOrderImportPage(pType, pId);
        }

        function getConfirmation() {
            return confirm('Are you sure you want to perfom this action ?');
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div class="main-content-area">
        <center>
                    <table width="100%" style="border-color:#00A9F5;">
                              <tr style="height:30px;background-color:#507CD1"> <%--row 1--%>
                                 <td style="width:96%" align="center">
                                    <div class="Page-Title">
                                     <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" Font-Names="Calibri"></asp:Label>
                                    </div>
                                    
                                </td>
                                <td style="width:4%;">
                                   <div class="Page-Title">
                                     <a href="viewOrdersPO.aspx?wfTypeId=10011" style="color:White;font-size:12px" >Back</a>
                                    </div>
                                </td>
                             </tr>
                    </table>
        </center>
      
        <table id="tblHead" runat="server" width="100%" style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;" bgcolor="white" border="0">  <%--bgcolor="#e2e2e2"--%>
            <tr >
                <td style="width:15%" align="left">
                  <b>Last Updated By User :</b>&nbsp;
                  <asp:Label ID="lblLatestUser" runat="server" Font-Bold="false" Font-Size="14px" Font-Names="Calibri" ForeColor="#3366cc"></asp:Label></td> 
                <td style="width:20%" align="left">
                   <b>Created By User :</b>&nbsp;
                   <asp:Label ID="lblCreatedBy" runat="server" Text="" Font-Size="14px" Font-Bold="false" Font-Names="Calibri" ForeColor="#3366cc" Visible="true"></asp:Label>
                    
                </td>
                <td style="width:15%" align="left"><b>Current Status :</b></td> 
                <td style="width:25%" align="left"><asp:Label ID="lblCurrStatus" runat="server" Font-Bold="false" Font-Size="14px" Font-Names="Calibri" ForeColor="#3366cc"></asp:Label></td>
                <td style="width:10%" align="left"><b>Order Type:</b></td> 
                <td style="width:15%" align="left"><asp:Label ID="lblprocessSubType" runat="server" Font-Bold="True" Font-Size="14px" Font-Names="Calibri" ForeColor="#3366cc" ></asp:Label></td>
            </tr>
            <tr>
                <td style="width:15%" align="left"><b>Latest Comments</b></td> 
                <td colspan="3" align="left">
                    <asp:Label ID="lblLatestComments" runat="server" Font-Bold="false" Font-Size="14px" Font-Names="Calibri" ForeColor="#3366cc"></asp:Label>
                </td>
                <td colspan="2" align="left">
                 <div runat="server" id="divPrnSaveDwnl" visible="true">
                    <%--<asp:LinkButton ID="LnkView" runat="server" Font-Bold="true" Font-Size="12px" ForeColor="#993300" onclick="LnkView_Click">View / Print PDF</asp:LinkButton>&nbsp;--%>
                     View/Download&nbsp;
                     <asp:LinkButton ID="LnkSaveDwnl" runat="server" Font-Bold="true" Font-Size="12px"  onclick="LnkSaveDwnl_Click" ForeColor="#cc0066">Order HTML</asp:LinkButton>&nbsp;
                     <%--<asp:Button ID="Button2" runat="server" Text="Button" Visible="true" onclick="Button2_Click" />--%>
                     <asp:LinkButton ID="LnkSaveDwnlCVS" runat="server" Font-Bold="true" Font-Size="12px" onclick="LnkSaveDwnlCVS_Click" ForeColor="#9900ff">Order CSV</asp:LinkButton>
                 </div>
                </td> 
            </tr>
    </table>

    <table id="tblDetails" runat="server" width="100%"    style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;">
    <tr>
        <td>
            <table width="100%" style="border-collapse: collapse">
                <tr>
                   <td style="width:50%">
                        <table width="100%" style="border-collapse: collapse" border="0">
                            <tr>
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:20%;background-color:#FFE6D9;">Vendor [&nbsp;BU&nbsp;]</td>
                                <td style="border-style: solid; border-width: 1px; width:70%">
                                <asp:Label ID="lblVendor" runat="server" Text="" Font-Bold="true"></asp:Label>
                                &nbsp;[<asp:Label ID="lblBU" runat="server" Text="" Font-Bold="true" Visible="true"></asp:Label>]
                                </td>
                                <td style="width:10%">
                                  &nbsp;
                                </td>
                            </tr>
                            <tr style="height:40px">
                                <td align="center" style="border-style: solid; border-width: 1px; font-weight:bold;width:20%;background-color:#FFE6D9;">
                                    User Comments</td>
                                <td style="border-style: solid; border-width: 1px; width:70%">
                                    <asp:Label ID="lblComments" runat="server" Height="20px" Font-Bold="true"></asp:Label></td>
                                <td style="width:10%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                 <td colspan="3">&nbsp;</td>
                             </td>
                            </tr>
                        </table>
                   </td>
                   <td style="width:50%">
                        <table width="100%"  style="border-collapse: collapse">
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">System PO NO</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                  <asp:Label ID="lblOrderId" runat="server" Font-Bold="true" Font-Size="12px" Font-Names="Calibri" Visible="true"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">FPO Ref </td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblFpoRef" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtFpoRef" Font-Size="10px" BackColor="#ffaa95" MaxLength="250" runat="server" Text="" Width="68%" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">EVO PO NO</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblEvoPONO" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtEvoPONO" Font-Size="10px" BackColor="#ffaa95" runat="server" Text="" Width="68%" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">PO DATE</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblPODate" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtPODate" Font-Size="10px" BackColor="#ffaa95" runat="server" Text="" Width="68%"></asp:TextBox>&nbsp;(MM-DD-YYYY)</td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">REQ DEL DATE</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblReqDelDate" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtReqDelDate" Font-Size="10px" BackColor="#ffaa95" runat="server" Text="" Width="68%"></asp:TextBox>&nbsp;(MM-DD-YYYY)</td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">OPG CODE</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblOPGCode" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtOPGCode" Font-Size="10px" BackColor="#ffaa95" runat="server" Text="" Width="68%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">Ship To</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblCBNNo" runat="server" Text="" Font-Size="10px" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:TextBox ID="txtCBNNo" Font-Size="10px" BackColor="#ffaa95" runat="server" Text="" Width="68%"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="border-style: solid; border-width: 1px; font-weight:bold;width:25%;background-color:#FFE6D9;">CBN Name</td>
                                <td style="border-style: solid; border-width: 1px; width:75%;background-color:#e2e2e2">
                                <asp:Label ID="lblCBNName1" runat="server" Text="" Font-Size="11px" Font-Bold="true" Visible="false"></asp:Label>
                                 <asp:DropDownList ID="ddlCBNName" runat="server" Width="200px" AutoPostBack="false" 
                                        Font-Bold="false" BackColor="#ffbbbb" Enabled="true" 
                                         Visible="true" >
                                  </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                   </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr>
        <td>
            <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="12px" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width:100%">
            <asp:GridView ID="gridDetails1" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" Width="100%" BackColor="White" BorderColor="#3366CC" 
                BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" 
                onrowcancelingedit="gridDetails1_RowCancelingEdit" 
                onrowediting="gridDetails1_RowEditing" 
                onrowupdating="gridDetails1_RowUpdating" 
                onrowdatabound="gridDetails1_RowDataBound" 
                onrowdeleting="gridDetails1_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Line No">
                        <ItemTemplate>
                            <asp:Label ID="lblLineNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lineNum")%>'></asp:Label>
                            <asp:Label ID="lblAutoID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "poLineId")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lblpoId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_poId")%>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblLineNoEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lineNum")%>' ></asp:Label>
                            <asp:Label ID="lblAutoIDEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "poLineId")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lblpoIdEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_poId")%>' Visible="false"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="2%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cust Name">
                        <ItemTemplate>
                            <asp:Label ID="lblCustName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "customerName")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCustNameEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "customerName")%>' ></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Region">
                        <ItemTemplate>
                            <asp:Label ID="lblRegion" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "region")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblRegionEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "region")%>' ></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part No">
                        <ItemTemplate>
                            <asp:Label ID="lblPartNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "partNo")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblPartNoEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "partNo")%>'></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Small Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "smallDescription")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblDescEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "smallDescription")%>'></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="17%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "qty")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblQtyEdit" Width="90%" runat="server" Text="" ></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Curr Price">
                         <ItemTemplate>
                            <asp:Label ID="lblCurrPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "currPrice")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblCurrPriceEdit" runat="server" Text="" Width="90%" ></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Total">
                         <ItemTemplate>
                            <asp:Label ID="lblAmountTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "amountTotal","{0:n}")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblAmountTotalEdit" runat="server" Text="-"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rebate Per Unit">
                         <ItemTemplate>
                            <asp:Label ID="lblRebatePerUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rebatePerUnit")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblRebatePerUnitEdit" runat="server" Text="" Width="90%" ></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cost After Rebate">
                        <ItemTemplate>
                            <asp:Label ID="lblCostAftrRebate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "costAfterRebate")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCostAftrRebateEdit" runat="server" Text="-"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Cost After Rebate">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalCostAftrRebate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totalCostAfterRebate","{0:n}")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblTotalCostAftrRebateEdit" runat="server" Text="-"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Selling Price">
                         <ItemTemplate>
                            <asp:Label ID="lblSellingPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "sellingPrice")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="lblSellingPriceEdit" runat="server" Text="" Width="90%" ></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Selling">
                         <ItemTemplate>
                            <asp:Label ID="lblTotalSelling" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "totalSelling","{0:n}")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblTotalSellingEdit" runat="server" Text="-"></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Margin %">
                         <ItemTemplate>
                            <asp:Label ID="lblMargin" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "margin")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblMarginEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "margin")%>'></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Type">
                         <ItemTemplate>
                            <asp:Label ID="lblorderType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderType")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblorderTypeEdit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "orderType")%>' ></asp:Label>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                         <ItemStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton><br />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkUpd" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkCanc" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle Width="100px" />
                    </asp:TemplateField>
                    

                </Columns>

                <FooterStyle BackColor="#FFE6D9" ForeColor="#003399" />
                <HeaderStyle BackColor="#FFE6D9" Font-Bold="True" ForeColor="black" 
                    HorizontalAlign="Left"/>
                <PagerStyle BackColor="#99CCCC" ForeColor="black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="black" />
                <EditRowStyle BackColor="#99cc00" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="black" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />

            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td style="padding-left:10px">
            <table width="100%" border="0px">
                <tr>
                    <td style="width:35%">&nbsp;</td>
                    <td style="width:5%;border-style: solid; border-width: 0px; font-weight:bold;">&nbsp;</td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:15%;background-color:Silver">Amount $</td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:15%;background-color:Silver">Cost After Rebate $</td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:10%;background-color:Silver">Selling $</td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:10%;background-color:Silver">Margin %</td>
                    <td style="border-style: solid; border-width: 0px; font-weight:bold;width:10%" align="right">&nbsp;</td>
                </tr>    
                <tr>
                    <td style="width:35%">&nbsp;</td>
                    <td style="width:5%;border-style: solid; border-width: 0px; font-weight:bold">Totals $</td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:15%"><asp:Label ID="lblLineTotal" runat="server" Text=""></asp:Label></td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:15%"><asp:Label ID="lblLineTotalRebate" runat="server" Text=""></asp:Label></td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:10%"><asp:Label ID="lblLineTotalSelling" runat="server" Text=""></asp:Label></td>
                    <td style="border-style: solid; border-width: 1px; font-weight:bold;width:10%"><asp:Label ID="lblLineMargin" runat="server" Text=""></asp:Label></td>
                    <td style="border-style: solid; border-width: 0px; font-weight:bold;width:10%" align="right"><asp:LinkButton ID="lnkNewItem" ForeColor="#336699" runat="server" Text="Add New Item Row" Font-Bold="true" onclick="lnkNewItem_Click" style="font-size:13px" ></asp:LinkButton></td>
                </tr>    
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div style="background-color:White">
                <table width="100%">
                    <tr>
                        <td>
                            <div style="width:100%;font-size:13px;color:Black;border:1px solid Gray;background-color:#72a0cf" runat="server" id="pnlNewRow" visible="false">
                                
                                <table width="100%">
                                    <tr>
                                        <td  colspan="6">
                                            <asp:Label ID="lblError1" runat="server" Text="" ForeColor="Red" Font-Bold="true" Font-Size="12px" ></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:45%" align="center">
                                            <b>Select PartNo</b>
                                        </td>

                                        <td style="width:11%" align="center">
                                            <b>Quantity</b>
                                        </td>

                                        <td style="width:11%" align="center">
                                            <b>Current Price</b>
                                        </td>

                                        <td style="width:11%" align="center">
                                            <b>Rebate Per Unit</b>
                                        </td>

                                        <td style="width:11%" align="center">
                                            <b>Selling Price</b>
                                        </td>

                                        <td style="width:11%" align="center">
                                            <b>Order Type</b>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:20%">
                                                        <b>Select Part NO</b>
                                                    </td>
                                                    <td style="width:40%">
                                                        <asp:DropDownList ID="ddlPartNo" Width="91%" Font-Size="10px" ForeColor="#666666"  BackColor="#D8E9E9" Font-Bold="true" runat="server" 
                                                        Visible="true" AutoPostBack="true" onselectedindexchanged="ddlPartNo_SelectedIndexChanged"> </asp:DropDownList>
                                                    </td>
                                                    <td style="width:40%">
                                                        <asp:TextBox ID="txtPartNo" Text="" runat="server" Width="90%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Description</b>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtDescription" Text="" runat="server" Width="95%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtQuantity" Text="" runat="server" Width="90%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtCurrentPrice" Text="" runat="server" Width="90%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtRebatePerUnit" Text="" runat="server" Width="90%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtSellingPrice" Text="" runat="server" Width="90%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlOrderType" runat="server" Font-Size="10px" ForeColor="#666666"  BackColor="#D8E9E9" Font-Bold="true" Width="50%" >
                                             </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="6" align="right">
                                            <asp:Button ID="btnInsert" runat="server" Text="Save" onclick="btnInsert_Click" Width="80px" Font-Bold="true" CssClass="btnStyle" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnClose" runat="server" Text="Cancel" onclick="btnClose_Click" Width="80px" Font-Bold="true" CssClass="btnStyle" />
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" style="border-collapse: collapse">
                <tr>
                    <td style="width:10%">&nbsp;</td>
                    <td style="width:40%;border-style: solid; border-width: 1px; font-weight:bold;width:40%;background-color:#FFE6D9;">PRODUCT MANAGER</td>
                    <td style="width:10%">&nbsp;</td>
                    <td style="width:40%;border-style: solid; border-width: 1px; font-weight:bold;width:40%;background-color:#FFE6D9;">HEAD OF FINANCE</td>
                </tr>
                <tr style="height:40px">
                    <td style="width:10%">&nbsp;</td>
                    <td style="width:40%;border-style: solid; border-width: 1px;background-color:#e2e2e2;padding-left:5px"  align="left">
                      <asp:Label ID="lblProdctManager" runat="server" Text="" Font-Size="14px" Font-Bold="true" Visible="true"></asp:Label>&nbsp;
                      <asp:TextBox ID="txtProdctManager" Font-Size="10px" BackColor="#ffaa95" runat="server" Enabled="false" Text="" Width="40%" Visible="false" ></asp:TextBox>
                     </td>
                    <td style="width:10%">&nbsp;</td>
                    <td style="width:40%;border-style: solid; border-width: 1px;background-color:#e2e2e2;padding-left:5px" align="left">
                     <asp:Label ID="lblHeadOfOffice" runat="server" Text="" Font-Size="14px" Font-Bold="true" Visible="true"></asp:Label>&nbsp;
                      <asp:TextBox ID="txtHeadOfOffice" Font-Size="10px" BackColor="#ffaa95" runat="server" Enabled="false" Text="" Width="40%" Visible="false" ></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbldbCode" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;
            

            <asp:Label ID="lblCustomer" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;
            <asp:Label ID="lblCustAcct" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;
            
            
            <asp:Label ID="lblVendorAcct" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;

        </td>
    </tr>
</table>
<%--Accordian--%>
        <cc1:Accordion ID="UserAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" Width="100%" Height="100%"
        ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="500" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None" >
            <Panes>
                <cc1:AccordionPane ID="AccordionPane1" runat="server">
                    <Header><a href="#" class="href" >Click here ! To View/Hide Statistics</a></Header>
                    <Content>
                        <table id="tblTask" runat="server" width="100%" style="border-style: solid; border-width: 2px; padding: 2px; font-size: 10px;" bgcolor="#FDF3CD">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkStats" runat="server" Text="Have you verified the data ??" Checked="false" Font-Bold="true" Font-Size="14px" ForeColor="#006699" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <div style="font-size:13px;margin-top:10px">
                                        <asp:Panel ID="pnlCustomerInfo" runat="server"  Visible="true" ForeColor="Black">
                                            <table width="100%" style="border-style: solid;border-width:0px;background-color:#FDF3CD">
                                                <tr>
                                                    <td style="width:50%;">
                                                        <table width="100%" style="border-style:solid;border-width:0px;background-color:#FDF3CD" border="0">
                                                            <tr>
                                                                <td style="width:40%;padding-left:10px">
                                                                    <b>Customer Name</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblCustomerName" runat="server" Text=""  ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>GP Margin (This order)</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblCustomerGPMargin" runat="server" Text="0" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Credit Limit</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblDollar" runat="server" Text="$" ForeColor="#000066" ></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblCustomerCreditLimit" runat="server" Text="0" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            
                                                            
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Settlement Day(s)</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblCustomerSettlementDay" runat="server" Text="0" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Customer Outstanding Total :</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <b>$&nbsp;<asp:Label ID="lblCustomerOutstandingTotal" runat="server" Text="0" ForeColor="Gray"></asp:Label></b>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Customer Outstanding Age Wise </b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px" colspan="2">
                                                                    
                                                                    <table width="100%" style="border-style:solid;border-width:0px;background-color:#FDF3CD" border="1">
                                                                      <tr style="background-color:gray;color:#fff;height:30px">
                                                                        <td style="width:12%;padding-left:3px"><b>Current</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 30</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 45</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 60</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 90</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 120</b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>Age 150</b></td>
                                                                        <td style="width:16%;padding-left:3px"><b>Age 150+</b></td>
                                                                      </tr>
                                                                      <tr style="background-color:#fff">
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding0" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding30" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding45" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding60" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding90" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding120" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:12%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding150" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        <td style="width:16%;padding-left:3px"><b>$&nbsp;<asp:Label ID="lblCustomerOutstanding150plus" runat="server" Text="0" ForeColor="Gray"></asp:Label></b></td>
                                                                        
                                                                      </tr>
                                                                     </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>

                                                    <td style="width:50%;">
                                                        <table width="100%" style="border-style:solid;border-width:0px;background-color:#FDF3CD">
                                                            <tr>
                                                                <td style="width:40%;padding-left:10px">
                                                                    <b>Vendor Name (Account)</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblVendorName" runat="server" Text="" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Credit Limit</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblDollar2" runat="server" Text="$" ForeColor="#000066" ></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblVendorCreditLimit" runat="server" Text="" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Amount in backLog</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblDollar4" runat="server" Text="$" ForeColor="#000066" ></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblVendorAmountinbackLog" runat="server" Text="" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Payment Terms</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblVendorPaymentTerms" runat="server" Text="" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-left:10px">
                                                                    <b>Vendor Outstanding Amount</b>
                                                                </td>
                                                                <td style="padding-left:10px">
                                                                    <asp:Label ID="lblDollar3" runat="server" Text="$" ForeColor="#000066" ></asp:Label>&nbsp;
                                                                    <asp:Label ID="lblVendorOutstandingAmount" runat="server" Text="" ForeColor="#000066"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div style="font-size:13px;margin-top:10px">
                                        <asp:Panel ID="pnlVendorInfo" runat="server" Visible="false">
                                            <table width="50%" style="border-style: solid;border-width:0px" border="0px">
                                                <tr>
                                                    <td style="width:100%">
                                                        <font color="black"><b>Stock level for selected BU (All Regions) in terms of $ value</b></font>
                                                        <br />
                                                        <asp:GridView ID="GridViewBU" Width="99%" runat="server" CssClass="mGrid">
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div style="font-size:13px;margin-top:10px">
                                        <table width="50%">
                                            <tr>
                                                <td style="width:100%">
                                                    <font color="black"><b>Stock level for selected Items in this order (All Regions) in terms of Quantity</b></font>
                                                    <br />
                                                    <asp:GridView ID="GridViewProducts" Width="99%" runat="server" CssClass="mGrid">
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            
                            <tr>
                                <td colspan="4">
                                    <div style="font-size:13px;margin-top:10px">
                                        <table width="99%">
                                            <tr>
                                                <td style="width:100%">
                                                    <font color="black"><b>Escalation History:</b></font>
                                                    <br />
                                                    <asp:GridView ID="GridEscalationHistory" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="100%" BackColor="White" Font-Bold="true"
                                                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" ShowHeaderWhenEmpty="True" CssClass="mGrid">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="2%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action Stage">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblaction_Stage" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "action_Stage")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="18%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Comments">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "comments")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="62%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Updated By">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblUpdatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastUpdatedBy")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="7%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Updated On">
                                                                     <ItemTemplate>
                                                                        <asp:Label ID="lblUpdatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lastModified","{0:MM-dd-yyyy hh:mm tt}")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                     <ItemStyle Width="11%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#FFE6D9" ForeColor="#003399" />
                                                            <HeaderStyle BackColor="#FFE6D9" Font-Bold="True" ForeColor="black" HorizontalAlign="Left"/>
                                                            <PagerStyle BackColor="#99CCCC" ForeColor="black" HorizontalAlign="Left" />
                                                            <RowStyle BackColor="White" ForeColor="Gray" />
                                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="black" />
                                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                                        </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
    <%--Accordian Closed--%>
    <br />
    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Font-Size="16px" Font-Bold="true" Font-Names="Calibri" Visible="true"></asp:Label>
    <div style="border:2px solid Gray" id="Div1">
    <table width="100%">
            <tr>
                <td align="left" valign="top">
                    <b>File Uploads (optional) :</b>
                </td>
                <td colspan="2">
                    <b>Check from the list of existing attachements for current stage to be farwarded</b><br />
                    
                    <asp:Label ID="lblNone" Font-Bold="true" runat="server" Text="None" Visible="false" ForeColor="Red" ></asp:Label>

                    <asp:GridView ID="GridFiles" runat="server" Width="100%" 
                        AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" 
                        BorderStyle="None" BorderWidth="0px" GridLines="None" >

                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chklstAttachedFiles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "websitefilePath")%>' /> &nbsp;
                                    <asp:HyperLink ID="lnkFileLoc" Text="Download" runat="server"></asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="80%" />
                            </asp:TemplateField>
                            
                        </Columns>

                    </asp:GridView>
                   </td>
                </tr>
    </table></div>
    <div style="border:2px solid Gray" id="pnlTaskUpd" runat="server">
        <table width="100%">
            
              <tr style="height:30px;background-color:white">
                  <td align="left" valign="middle">
                          <asp:CheckBox ID="chkUploadFilesWish" runat="server" 
                              Text=" Upload New Files ???" Font-Bold="true" ForeColor="Black"/> &nbsp;
                  </td>
                  <td colspan="2">
                    <asp:FileUpload ID="fileUpload1" runat="server" CssClass="" />&nbsp;
                    <input type="button" onclick="AddNewRow(); return false;"  value="Browse More Files....." style="font-size: 9px" />&nbsp;
                    <asp:Button ID="Button1" Text="Cancel All" runat="server" style="font-size: 9px"  />
                    <div id="divFileUploads">
                    </div>
                </td>
            </tr>

            <tr>
                <td align="left">
                    <asp:Label ID="lblIntimationMailIdTitle" Font-Bold="true" runat="server" Text="Send Intimation Mail on Emial ID (Optional):" Visible="true"></asp:Label>
                </td>
                <td>
                    <asp:Panel ID="PanelInitimationMailId" runat="server">
                        <asp:TextBox ID="txtInitimationMailId" runat="server" Width="500px"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID="txtInitimationMailId_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtInitimationMailId" WatermarkText="Enter your email here." WatermarkCssClass="watermark">
                        </cc1:TextBoxWatermarkExtender>
                    </asp:Panel>
                </td>
                <td align="right" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblWait" Font-Bold="true" runat="server" Text="Wait Request being processed........, do not refresh page or click back button utill finished" Visible="false" Font-Size="14px" ForeColor="Red" ></asp:Label>
                </td>
            </tr>
            <tr style="height:55px">
                <td style="width:15%">
                    <b>Updatation Comments :</b>
                </td>
                <td style="width:60%">
                    <asp:TextBox ID="txtComments" Width="98%" runat="server" Height="70px" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="width:25%" align="center">
                    <asp:Button ID="btnAccept" runat="server" Text="Accept" Font-Bold="True" Width="80px" onclick="btnAccept_Click" CssClass="btnStyle"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnDecline" runat="server" Text="Decline" Font-Bold="True" Width="80px" onclick="btnDecline_Click"  CssClass="btnStyle"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnHold" runat="server" Text="Put On Hold" Width="90px" Font-Bold="true" Visible="true" onclick="btnHold_Click" CssClass="btnStyle" ToolTip="Clicking Hold button lets the user to save the new comments at the same level and does not escalates order to further level, New uploaded files will be saved but No emails will be sent in this case." />

                </td>
            </tr>
            
            <tr style="height:35px">
                <td align="left">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Width="110px" onclick="btnCancel_Click" Text="Cancel Order" Visible="False"  CssClass="btnStyle" ToolTip="Order once canceled will be permanantly out of escalation system. Please verify before you proceed." />&nbsp;
                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm Order" Font-Bold="true" Width="110px" Visible="false" onclick="btnConfirm_Click" CssClass="btnStyle" />&nbsp;
                    <asp:Button ID="btnReimport" runat="server" Text="Re-Import" Font-Bold="true" Width="110px" Visible="false" onclick="btnReimport_Click" OnClientClick="callImportfun('PO');" CssClass="btnStyle" />&nbsp;
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="110px" Font-Bold="true" Visible="false" onclick="btnEdit_Click" CssClass="btnStyle" />
                    
                </td>
                <td align="right" colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
            <uc1:MsgBoxControl ID="MsgBoxControl1" runat="server" />
 <br />
</div>

 </ContentTemplate>

        <Triggers>
         <asp:PostBackTrigger ControlID = "btnCancel" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID = "btnConfirm" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID = "btnAccept" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID = "btnDecline" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID = "btnHold" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
        </Triggers>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
            <ProgressTemplate>
                <div style="top:90%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>

</asp:Content>