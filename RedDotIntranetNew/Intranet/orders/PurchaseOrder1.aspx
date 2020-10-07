<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="PurchaseOrder1.aspx.cs" Inherits="Intranet_orders_PurchaseOrder1" EnableEventValidation="true" %>

<%@ Register src="../../MsgBoxControl.ascx" tagname="MsgBoxControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
  <script type="text/javascript">

      function updateTotalValues() 
      {
          var grid = document.getElementById('<%=GridView1.ClientID %>');
          var gTotal=0.0;
          var gTotalAfterRebate = 0.0;
          var gTotalSellings = 0.0;
          var TotalMargin = 0.0;

          for (var rw = 1; rw < grid.rows.length; rw++) {

              gTotal = gTotal + parseFloat(grid.rows[rw].cells[4].children[0].innerHTML);
              gTotalAfterRebate = gTotalAfterRebate + parseFloat(grid.rows[rw].cells[7].children[0].innerHTML);
              gTotalSellings = gTotalSellings + parseFloat(grid.rows[rw].cells[9].children[0].innerHTML);
          }

          var dif = gTotalSellings - gTotalAfterRebate;
          if (parseFloat(dif) != 0) {
              TotalMargin = (dif / (gTotalAfterRebate) * 100);
          }
          else
              TotalMargin = 0;

          document.getElementById("<%= lblGrandTotal.ClientID %>").innerHTML = gTotal.toFixed(2);
          document.getElementById("<%= lblGrandTotalAfterRebate.ClientID %>").innerHTML = gTotalAfterRebate.toFixed(2);
          document.getElementById("<%= lblGrandTotalSelling.ClientID %>").innerHTML = gTotalSellings.toFixed(2);
          document.getElementById("<%= lblTotalMargin.ClientID %>").innerHTML = TotalMargin.toFixed(2);
      }


      function updateValues(rw) {

          /*  Important note to use this function designed for grid elements
          This function supports calculation for 2 asp:texts,  1 Label for totals. These fields should be in seperate cols of the grid and 1st element of the cell
          */

          //alert(rw);

          var grid = document.getElementById('<%=GridView1.ClientID %>');

          rw = rw + 1;  //add 1 to row as header row (0th row) is the first row in html where code has sent row index value which starts from  0 excl;uding header row
          var colQty = grid.rows[rw].cells[2];  //supply the col no. in the grid for the textfield qty 
          var colPrice = grid.rows[rw].cells[3]; //supply the col no. in the grid for the textfield price
          var colTotal = grid.rows[rw].cells[4]; //supply the col no. in the grid for the lable total

          var colRebatePerUnit = grid.rows[rw].cells[5]; //supply the col no. in the grid for the textfield rebate per unit
          var colCostAfterRebate = grid.rows[rw].cells[6]; //supply the col no. in the grid for the lable cost after rebate
          var colTotalCostAfterRebate = grid.rows[rw].cells[7]; //supply the col no. in the grid for the lable total cost after rebate

          var colSellingPrice = grid.rows[rw].cells[8]; //supply the col no. in the grid for the textfield SellingPrice
          var colSellingPriceTotal = grid.rows[rw].cells[9]; //supply the col no. in the grid for the lable Selling Price Total
          var colMargin = grid.rows[rw].cells[10]; //supply the col no. in the grid for the lable Margin


          //alert(colQty.children[0].value + colQty.children[0].id);

          var qt, pr, tot;
          qt = 0;
          pr = 0;
          tot = 0;

          if (!isNaN(colQty.children[0].value) && colQty.children[0].value != "")
              qt = parseFloat(colQty.children[0].value);
          else {
              alert("Error! Invalid numeric Value (" + colQty.children[0].value + ") in field Qty, Please supply a valid value to proceed");
              colQty.children[0].value = 0;
          }

          if (!isNaN(colPrice.children[0].value) && colPrice.children[0].value != "")
              pr = parseFloat(colPrice.children[0].value);
          else {
              alert("Error! Invalid numeric Value (" + colPrice.children[0].value + ") in field Price, Please supply a valid value to proceed");
              colPrice.children[0].value = 0;
          }
          tot = qt * pr;
          colTotal.children[0].innerHTML = tot.toFixed(2);

          //-----------------------
          var RebatePerUnit, CostAfterRebate, TotalCostAfterRebate, SellingPrice, SellingPriceTotal, Margin;

          RebatePerUnit = 0;
          CostAfterRebate = 0;
          TotalCostAfterRebate = 0;
          SellingPrice = 0;
          SellingPriceTotal = 0;
          Margin = 0;

          if (!isNaN(colRebatePerUnit.children[0].value) && colRebatePerUnit.children[0].value != "")
              RebatePerUnit = parseFloat(colRebatePerUnit.children[0].value);
          else {
              alert("Error! Invalid numeric Value (" + colRebatePerUnit.children[0].value + ") in field Rebate Per Unit, Please supply a valid value to proceed");
              colRebatePerUnit.children[0].value = 0;
          }

          CostAfterRebate = pr - RebatePerUnit;
          colCostAfterRebate.children[0].innerHTML = CostAfterRebate.toFixed(2);

          TotalCostAfterRebate = qt * CostAfterRebate;
          colTotalCostAfterRebate.children[0].innerHTML = TotalCostAfterRebate.toFixed(2);

          //-----------------------------------

          if (!isNaN(colSellingPrice.children[0].value) && colSellingPrice.children[0].value != "")
              SellingPrice = parseFloat(colSellingPrice.children[0].value);
          else {
              alert("Error! Invalid numeric Value (" + colSellingPrice.children[0].value + ") in field Selling Price, Please supply a valid value to proceed");
              colSellingPrice.children[0].value = 0;
          }

          SellingPriceTotal = qt * SellingPrice;
          colSellingPriceTotal.children[0].innerHTML = SellingPriceTotal.toFixed(2);

          var dif=SellingPriceTotal  - TotalCostAfterRebate;

          //alert(SellingPriceTotal + " " + TotalCostAfterRebate + " " + dif);
          if (parseFloat(dif) != 0) 
          {
              Margin = (dif / (TotalCostAfterRebate) * 100);
             // alert(Margin);
          }
          else
              Margin = 0;

          //alert(Margin);
          colMargin.children[0].innerHTML = Margin.toFixed(2);

          updateTotalValues();
      }

      function disableBtn(btnID, newText) {

          var btn = document.getElementById(btnID);
          setTimeout("setImage('" + btnID + "')", 10);
          btn.disabled = true;
          btn.value = newText;
      }

      function setImage(btnID) {
          var btn = document.getElementById(btnID);
          btn.style.background = 'url(/images/btnDisabled.gif)';
      }
</script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <div class="main-content-area" style="width:95%">
        <center>
                    <table width="100%" style="border-color:#00A9F5;">
                              <tr style="height:30px;background-color:#507CD1"> <%--row 1--%>
                                 <td style="width:96%" align="center">
                                    <div class="Page-Title">
                                      Purchase Order Request Form
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
        <br />
       <asp:Label ID="lblPermissionMsg" runat="server" ForeColor="Red" Text="" Font-Bold="True" Font-Size="14px"></asp:Label>
        <table style="width:100%" runat="server" id="tblMain">
                <tr style="height:40px;background-color:#D8E9E9">
                    <td style="padding-left:10px;font-weight:bold" align="left">
                       Puchase OrderType : &nbsp;
                       <asp:Label ID="lblPOType" runat="server" Text="" ForeColor="#cc0099" Font-Bold="true" Font-Size="16px"></asp:Label>
                    </td>
                </tr>
                <tr style="height:25px;background-color:#D8E9E9">
                  <td style="padding-left:10px">
                       <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="True" 
                           Font-Size="14px"></asp:Label>
                  </td>
                </tr>
                
                <tr style="height:45px">
                  <td valign="middle" style="font-weight:bold;background-color:#D8E9E9;padding-left:5px">
                     
                       <table width="100%" border="0px">
                        <tr>
                            <td style="width:5%;font-weight:bold" align="left">Vendor :</td>
                            <td style="width:28%;font-weight:bold" align="left">
                             <asp:DropDownList ID="ddlvendorGlb" Width="280px" runat="server" Visible="true" BackColor="silver" AutoPostBack="True" onselectedindexchanged="ddlvendorGlb_SelectedIndexChanged"> </asp:DropDownList>
                             &nbsp;(<asp:Label ID="lblVendorCount" runat="server" Text="0" ForeColor="#cc0099" Font-Bold="true" Font-Size="14px"></asp:Label>)
                            </td>
                            <td style="width:12%;font-weight:bold" align="left">
                             <asp:Label ID="lblItemsCountForSelectedGroup" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="true" ForeColor="Blue"></asp:Label>
                            </td>
                            <td style="width:55%;font-weight:bold" align="left">
                               
                                  <table width="100%" border="0px">
                                        <tr>
                                            <td style="width:40%;font-weight:bold" align="right">
                                              Select Database :&nbsp;
                                              <asp:DropDownList ID="ddlDB" runat="server" Width="170px" AutoPostBack="true" 
                                                    Font-Bold="true" BackColor="#F2F2F2" 
                                                    onselectedindexchanged="ddlDB_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                             </td>
                                             
                                             <td style="width:60%;font-weight:bold" align="right">
                                                  <asp:Panel ID="panelDbNCust" runat="server">
                                                      <table width="100%" border="0px">
                                                        <tr>
                                                            <td style="width:90%;font-weight:bold" align="right">
                                                                Select Customer :&nbsp;<asp:DropDownList ID="ddlCustList" Width="180px" runat="server" Visible="true" BackColor="silver" > </asp:DropDownList>
                                                            </td>
                                                            <td style="width:10%" align="left">
                                                             (<asp:Label ID="lblCustCount" runat="server" Text="0" ForeColor="#cc0099" Font-Bold="true" Font-Size="14px"></asp:Label>)
                                                            </td>
                                                        </tr>
                                                      </table>
                                                  </asp:Panel>
                                             </td>
                                            
                                        </tr>    
                                    </table>
                              
                            </td>
                        </tr>    
                    </table>
                  </td>
                </tr>
                
                <tr style="height:45px">
                  <td style= "background-color:#D8E9E9">
                   <table width="100%" border="0px">
                        <tr>
                            <td style="width:5%;font-weight:bold" align="left">FPO Ref :</td>
                            <td style="width:20%;font-weight:bold" align="left"> 
                              <asp:TextBox ID="txtFpoRef" Text="UNASSIGNED" runat="server" MaxLength="250" Width="90%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                            </td>
                            
                            <td style="width:25%;font-weight:bold" align="right">
                                CNB Name :&nbsp;
                                <asp:DropDownList ID="ddlCBNName" runat="server" Width="170px" AutoPostBack="false" 
                                    Font-Bold="false" BackColor="#ffbbbb" >
                                </asp:DropDownList>
                             </td>
                           <td style="width:50%;font-weight:bold" align="left">&nbsp;</td>
                        </tr>
                   </table>
                  </td>
                </tr>

                <tr>
                  <td style= "background-color:#D8E9E9">
                      <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" 
                          Width="100%"  CssClass="GridviewStylePO"
                          onrowdatabound="GridView1_RowDataBound" Font-Size="10px" ForeColor="#666666" Font-Bold="true">

                      <HeaderStyle Font-Bold="true"  HorizontalAlign="Center" VerticalAlign="Bottom" Height="40px"  />

                        <Columns>
                         <asp:TemplateField HeaderText="Serial">
                             <ItemTemplate> 
                                <asp:Label ID="lblSerial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Serial")%>'></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="2%" Font-Bold="true" HorizontalAlign="Center" Height="70px" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="PartNo">
                             <ItemTemplate> 
                                <table style="width:100%">
                                     <tr>
                                          <td style="width:20%">
                                            Select PartNo
                                          </td>
                                          <td style="width:40%">
                                            <asp:DropDownList ID="ddlPartNo" Width="97%" Font-Size="10px" ForeColor="#666666"  BackColor="#D8E9E9" Font-Bold="true" runat="server" onselectedindexchanged="ddlPartNo_SelectedIndexChanged"  Visible="true" AutoPostBack="true"> </asp:DropDownList>
                                          </td>
                                          <td style="width:40%">
                                            <asp:TextBox ID="txtPartNo" Text='<%# DataBinder.Eval(Container.DataItem, "PartNo")%>' runat="server" Width="97%" Font-Size="10px" ForeColor="#666666"  BackColor="#eaeaea" Font-Bold="true" ></asp:TextBox>
                                          </td>
                                     </tr>
                                     <tr>
                                          <td style="width:20%">
                                            Description
                                          </td>
                                          <td colspan="2">
                                           <asp:TextBox ID="txtDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server" MaxLength="254" Width="98%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" BackColor="#eaeaea"></asp:TextBox>
                                          </td>
                                     </tr>
                                </table>

                             </ItemTemplate>
                                <ItemStyle Width="39%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Quantity">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtQty"  Text='<%# DataBinder.Eval(Container.DataItem, "Qty")%>' runat="server" MaxLength="4" Width="97%"  Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Current Price">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtCurrPrice" Text='<%# DataBinder.Eval(Container.DataItem, "CurrPrice")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Amount Total">
                             <ItemTemplate> 
                                 <asp:Label ID="lblAmountTotal" Text='<%# DataBinder.Eval(Container.DataItem, "AmountTotal")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Rebate Per Unit">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtRebatePerUnit" Text='<%# DataBinder.Eval(Container.DataItem, "RebatePerUnit")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Cost After Rebate">
                             <ItemTemplate> 
                                 <asp:Label ID="lblCostAfterRebate" Text='<%# DataBinder.Eval(Container.DataItem, "CostAfterRebate")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Total Cost After Rebate">
                             <ItemTemplate> 
                                <asp:Label ID="lblTotalCostAfterRebate" Text='<%# DataBinder.Eval(Container.DataItem, "TotalCostAfterRebate")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="6%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Selleing Price">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtSelleingPrice" Text='<%# DataBinder.Eval(Container.DataItem, "SelleingPrice")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField HeaderText="Total Selleing">
                             <ItemTemplate> 
                                 <asp:Label ID="lblTotalSelleing" Text='<%# DataBinder.Eval(Container.DataItem, "TotalSelleing")%>' runat="server" MaxLength="10" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                                 <FooterTemplate>
                                     Total: <span id="spnTotalSelleing"></span>
                                </FooterTemplate>
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Margin %">
                             <ItemTemplate> 
                                 <asp:Label ID="lblMargin" Text='<%# DataBinder.Eval(Container.DataItem, "Margin")%>' runat="server" MaxLength="10" Width="70%" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>&nbsp;%
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Order Type">
                             <ItemTemplate> 
                                 <%--<asp:TextBox ID="txtOrderType" Text='<%# DataBinder.Eval(Container.DataItem, "OrderType")%>' runat="server" MaxLength="10" Width="97%"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlOrderType" runat="server" Font-Size="10px" ForeColor="#666666" Width="50px" Font-Bold="true">
                                    <%--<asp:ListItem Value="1">BTB</asp:ListItem>
                                    <asp:ListItem Value="2">CDP</asp:ListItem>
                                    <asp:ListItem Value="3">DMFI</asp:ListItem>
                                    <asp:ListItem Value="4">RUN RATE</asp:ListItem>--%>
                                 </asp:DropDownList>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                          <asp:TemplateField HeaderText="Comments" Visible="false">
                             <ItemTemplate> 
                                 <asp:TextBox ID="txtComments" Text='<%# DataBinder.Eval(Container.DataItem, "Comments")%>' runat="server" MaxLength="254" Width="97%" Font-Size="10px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>
                          
                          <asp:TemplateField>
                             <ItemTemplate> 
                                 <asp:ImageButton ID="imgBtnClose" ImageUrl="~/images/close-icon.png"  runat="server" OnClick="imgBtnClose_Click" ToolTip="Delete this item row" />
                             </ItemTemplate>
                                <ItemStyle Width="5%" HorizontalAlign="Center" VerticalAlign="Bottom" />
                          </asp:TemplateField>

                         

                        </Columns>
                      </asp:GridView>
                   </td>
                  </tr>
                  
                  <tr style="height:50px;background-color:#D8E9E9">
                    <td>
                      <table width="100%" border="0px">
                        <tr >
                            <td style="width:47%;font-weight:bold" align="left">&nbsp;</td>
                            <td style="width:6%;font-weight:bold" align="left">Totals</td>
                            <td style="width:15%;font-weight:bold" align="left">$ &nbsp;<asp:Label ID="lblGrandTotal" runat="server" Text="0" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label></td>
                            <td style="width:11%;font-weight:bold" align="left">$ &nbsp;<asp:Label ID="lblGrandTotalAfterRebate" runat="server" Text="0" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label></td>
                            <td style="width:6%;font-weight:bold" align="left">$ &nbsp;<asp:Label ID="lblGrandTotalSelling" runat="server" Text="0" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label></td>
                            <td style="width:15%;font-weight:bold" align="left">&nbsp;<asp:Label ID="lblTotalMargin" runat="server" Text="0" Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>&nbsp;%</td>
                        </tr>    
                    </table>
                   </td>
                  </tr>

                  <tr style="height:50px;background-color:#D8E9E9">
                    <td style="width:100%;padding-left:10px" valign="top">
                        <asp:Label ID="Label1" runat="server" Text="Comments : " Font-Size="10px" ForeColor="#666666" Font-Bold="true"></asp:Label>&nbsp;&nbsp;
                        <asp:TextBox ID="txtOrderCmnts" runat="server" Width="90%" Height="50px" TextMode="MultiLine" Font-Size="12px" ForeColor="#666666" Font-Bold="true" ></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                        <td style="padding-top:10px;padding-bottom:10px" align="left" valign="top">
                           <asp:CheckBox ID="chkUploadFilesWish" runat="server" 
                                Text=" Upload New Files (optional) ???" Font-Bold="true" ForeColor="Black"/> &nbsp;
                            <br />
                            <asp:FileUpload ID="fileUpload1" runat="server" CssClass="" />&nbsp;
                            <input type="button" onclick="AddNewRow(); return false;"  value="Browse More Files....." style="font-size: 9px" />&nbsp;
                            <asp:Button ID="Button1" Text="Cancel All" runat="server" style="font-size: 9px"  />
                            <div id="divFileUploads">
                            </div>
                        </td>
                  </tr>
                 
                  <tr style="height:50px;background-color:#D8E9E9">
                   <td valign="bottom">
                    <table style="width:100%;background-color:#D8E9E9">
                     <tr>
                       
                       <td style="width:60%;padding-right:13px" align="left" valign="middle">
                           <asp:ImageButton ID="btnClearAll" runat="server" 
                               ImageUrl="../images/clearAll.png" ToolTip="Click! to clear data in all rows" 
                               onclick="btnClearAll_Click"  Height="35" Width="35" />
                           &nbsp;&nbsp;
                           <asp:ImageButton ID="btnAddRow" runat="server" 
                               ImageUrl="../images/addRow.jpg" ToolTip="Click! to Add an additional row" 
                               onclick="btnAddRow_Click"  Height="35" Width="35" />
                       </td>
                       <td style="width:40%;padding-left:44px" align="right" valign="middle">
                         <asp:Button ID="btnSubmit" runat="server" Width="100px" Text="Submit" onclick="btnSubmit_Click" OnClientClick="disableBtn(this.id, 'Submitting...')"  UseSubmitBehavior="false" CssClass="btnStyle" />
                         <asp:Button ID="btnDraft" runat="server" Width="125px" Text="Save as draft" onclick="btnDraft_Click" OnClientClick="disableBtn(this.id, 'Submitting...')"  UseSubmitBehavior="false" CssClass="btnStyle" />

                       </td>
                      </tr>
                    </table>
                   </td>
                  </tr>
                  
                  
                 <%-- <tr>
                     <td>&nbsp;</td>
                   </tr>
                  <tr>
                    <td>
                        <table width="100%" style="border-collapse: collapse">
                            <tr>
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:45%;border-style: solid; border-width: 0px; font-weight:bold;width:40%;background-color:#FFE6D9;">PRODUCT MANAGER</td>
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:45%;border-style: solid; border-width: 0px; font-weight:bold;width:40%;background-color:#FFE6D9;">HEAD OF OFFICE</td>
                            </tr>
                            <tr style="height:25px">
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:45%;border-style: solid; border-width: 0px;" valign="top">
                                    <asp:TextBox ID="txtProductManager" runat="server" 
                                        Width="400px" MaxLength="250"></asp:TextBox></td>
                                <td style="width:5%">&nbsp;</td>
                                <td style="width:45%;border-style: solid; border-width: 0px;" valign="top">
                                    <asp:TextBox ID="txtHeadOfOffice" runat="server" 
                                        Width="400px" MaxLength="250"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                 </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>--%>
            </table>
            
            <br />
            <asp:Label ID="lblBU" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;
            <asp:Label ID="lblItemGroup" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>&nbsp;
            <asp:Label ID="lblVendorForBU" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>
            <asp:Label ID="lblVendorAcct" runat="server" Text="" Font-Size="12px" Font-Bold="true" Visible="false"></asp:Label>
            <uc1:MsgBoxControl ID="MsgBoxControl1" runat="server" />

  </div>
       </ContentTemplate>

       <Triggers>
         <asp:PostBackTrigger ControlID = "btnSubmit" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID = "btnDraft" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
        </Triggers>

     </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:50%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>
</asp:Content>

