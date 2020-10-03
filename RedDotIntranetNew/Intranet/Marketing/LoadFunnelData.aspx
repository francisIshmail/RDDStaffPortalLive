<%@ Page Language="C#" MasterPageFile="~/Intranet/MasterFunnel.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="LoadFunnelData.aspx.cs" Inherits="Intranet_Marketing_LoadFunnelData" Title="Manage Funnel" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/adminGrid.css" rel="stylesheet" type="text/css" />
    
    <script type = "text/javascript">

       var ddlText, ddlValue, ddl, lblMesg;

       function CacheItems() {

           ddlText = new Array();

           ddlValue = new Array();

           ddl = document.getElementById("<%=ddlFltrQT.ClientID %>");

           lblMesg = document.getElementById("<%=lblMsgSrch.ClientID%>");

           for (var i = 0; i < ddl.options.length; i++) {

               ddlText[ddlText.length] = ddl.options[i].text;

               ddlValue[ddlValue.length] = ddl.options[i].value;

           }

       }

       window.onload = CacheItems;



       function FilterItems(value) {

           ddl.options.length = 0;

           for (var i = 0; i < ddlText.length; i++) {

               if (ddlText[i].toLowerCase().indexOf(value) != -1) {

                   AddItem(ddlText[i], ddlValue[i]);

               }

           }

           lblMesg.innerHTML = ddl.options.length + " items found.";

           if (ddl.options.length == 0) {

               AddItem("No items found.", "");

           }

       }

       function AddItem(text, value) {

           var opt = document.createElement("option");

           opt.text = text;

           opt.value = value;

           ddl.options.add(opt);

       }

</script>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

       <table style="width:99%" border="0">
        <tr style="height:25px;"valign="middle">
        <td style="padding-left:5px;width:40%" align="left"><p class="title-txt">Manage Funnel Data</p></td>
        <td style="padding-left:5px;width:30%" align="right">
                    <asp:Button ID="btnRep" runat="server" Text="Get Funnel Report" width="150px" 
                    BackColor="#ffcc99" BorderColor="#00CC99" Font-Bold="True" Height="20px"
                    ForeColor="Black" ToolTip="Get Funnel Report" onclick="btnRep_Click"  />
       </td>
        <td style="padding-right:5px;width:30%;color:Purple" align="right">
            Note : Data Listed belongs to Logged in User
        </td>
        </tr>
    </table>
    
   <div style="margin-bottom: 5px; margin-top:0px; text-align:left;">
   <center>
      <table id="tblMain" runat="server" border="0" cellpadding="0" cellspacing="0" class="innerTable" width="99%">
       <tr align="left">
           <td >&nbsp;<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
           &nbsp;<asp:Label ID="lblprevCntryDbCode" runat="server" Text="" ForeColor="Blue" Visible="false"></asp:Label>
           </td>
       </tr>
       
       <tr>
             <td>
               <table style="background-color:#c0c0c0;width: 100%;" border="1">
                 <tr>
                    <td style="width: 34%;"><asp:Label ID="lblMsgSrch" runat="server" Text="" ForeColor="Red"></asp:Label></td>
                    <td align="right" style="width: 14%;">Search Quote ID :</td>
                    <td style="width: 14%;"><asp:TextBox ID="txtSearchQTID" runat="server" Width="175px" BackColor="#99ccff" onkeyup = "FilterItems(this.value)"></asp:TextBox></td>
                     <td style="width: 38%;"> &nbsp;&nbsp; Total Cost : &nbsp;<asp:Label ID="lblTotalCostUSD" runat="server" Text="" ForeColor="#990099" Font-Bold="true"></asp:Label>  &nbsp; | &nbsp;   Total Landed : &nbsp;<asp:Label ID="lblTotalLandedUSD" runat="server" Text="" ForeColor="#990099" Font-Bold="true"></asp:Label> &nbsp;|&nbsp; Total Sale : &nbsp;<asp:Label ID="lblTotalUSD" runat="server" Text="" ForeColor="#990099" Font-Bold="true"></asp:Label></td>
                 </tr>
               </table>
             </td>
        </tr>
              
       <tr style="height:15">
          <td style="width: 100%">
           
           <asp:Panel ID="PanelFltr" runat="server" Enabled="true" >
            <table width="100%" style="background-color:#c0c0c0" border="1">
             <tr style="background-color:#c0c0c0">
                
                <td align="left" style="width: 11%; color: Blue;">
                 <asp:Label ID="lblRowCnt" runat="server" ForeColor="#990099" Text="0" Font-Bold="true"></asp:Label>
                 &nbsp;
                 <asp:Button ID="btnNewDeal" runat="server" Text="New Deal" 
                       ForeColor="#990099" Font-Bold="True"  Visible="true"
                        ToolTip="Click !  to create new deal"
                        onclick="btnNewDeal_Click" Height="17px" Width="62px" Font-Size="10px"/>
                </td>

               <td align="left" style="width: 6%; color: Blue;"><asp:CheckBox ID="CheckAll" Text="List All ??" runat="server" Checked="true" Font-Bold="true" 
                       oncheckedchanged="CheckAll_CheckedChanged" AutoPostBack="true" ToolTip="If selected then no filter is applicable" /></td>
               
               <td align="left" style="width: 12%; color: Blue;">
                 <table>
                    <tr>
                        <td><asp:CheckBox ID="chkCountry" Text="Country" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkCountry_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrCountry" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="80px" onselectedindexchanged="ddlFltrCountry_SelectedIndexChanged">
                         </asp:DropDownList></td>
                    </tr>
                </table>
               </td>
                
                <td align="left" style="width: 18%; color: Blue;">
                   <table>
                    <tr>
                        <td><asp:CheckBox ID="chkReseller" Text="Reseller" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkReseller_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrReseller" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="110px" onselectedindexchanged="ddlFltrReseller_SelectedIndexChanged">
                         </asp:DropDownList></td>
                        <td><asp:Button ID="btnNewReseller" runat="server" Text="Add New" ForeColor="#990099" ToolTip="Click !Add New Customer To Funnel List" Font-Bold="True" onclick="btnNewReseller_Click"  Height="18px" Width="64px" Font-Size="10px"/></td>
                    </tr>
                </table>
               </td>
              
               <td align="left" style="width: 15%; color: Blue;">
                  <table>
                    <tr>
                        <td><asp:CheckBox ID="chkQT" Text="QT ID" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkQT_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrQT" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="110px" onselectedindexchanged="ddlFltrQT_SelectedIndexChanged">
                        </asp:DropDownList></td>
                        <td><asp:Button ID="btnGo" runat="server" Text="Go!" ForeColor="#990099" Font-Bold="True"  Visible="true" ToolTip="Click ! to refresh as selected filter value." Height="17px" Width="30px" Font-Size="10px"/></td>
                    </tr>
                </table>
              </td>
               
              
               <td align="left" style="width: 10%; color: Blue;">
                   <table>
                        <tr>
                            <td><asp:CheckBox ID="chkBU" Text="BU" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkBU_CheckedChanged" AutoPostBack="true" /></td>
                            <td><asp:DropDownList ID="ddlFltrBU" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="90px" onselectedindexchanged="ddlFltrBU_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        </tr>
                    </table>
               </td>

               <td align="left" style="width: 10%; color: Blue;">
                  <table>
                    <tr>
                       <td><asp:CheckBox ID="chkDealStatus" Text="Status" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkDealStatus_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrDealStatus" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="80px" onselectedindexchanged="ddlFltrDealStatus_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    </tr>
                </table>
               </td>
               
               <td align="left" style="width: 9%; color: Blue;">
                  <table>
                    <tr>
                        <td><asp:CheckBox ID="chkYear" Text="QT YR" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkYear_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrYear" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="60px" onselectedindexchanged="ddlFltrYear_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    </tr>
                </table>
               </td>
               
               <td align="left" style="width: 9%; color: Blue;">
                  <table>
                    <tr>
                        <td><asp:CheckBox ID="chkMonth" Text="QT MMM" runat="server" Checked="false" Font-Bold="true" oncheckedchanged="chkMonth_CheckedChanged" AutoPostBack="true" /></td>
                        <td><asp:DropDownList ID="ddlFltrMonth" runat="server" AutoPostBack="True" BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" width="50px" onselectedindexchanged="ddlFltrMonth_SelectedIndexChanged">
                        </asp:DropDownList></td>
                    </tr>
                 </table>
              </td>

             </tr>
           </table>
          <</asp:Panel>
        </td>
       </tr>
           
       <tr>
         <td>&nbsp;</td>
       </tr>

       <tr>
         <td style="width: 100%;">
                    <asp:GridView ID="Grid1" AutoGenerateColumns="False" CssClass="overallGrid" 
                        runat="server" Width="100%" AllowSorting="True" 
                        CellSpacing="1" 
                        onpageindexchanging="Grid1_PageIndexChanging" onsorting="Grid1_Sorting" 
                        onrowcancelingedit="Grid1_RowCancelingEdit" onrowediting="Grid1_RowEditing"
                        OnRowDeleting="Grid1_RowDeleting" onrowupdating="Grid1_RowUpdating" 
                        Font-Size="11px" onrowdatabound="Grid1_RowDataBound" >
                        
                        <FooterStyle CssClass="footerGrid" />
                        <RowStyle CssClass="DataCellGrid1" HorizontalAlign="left" />
                        <PagerStyle CssClass="pagerGrid" />
                        <SelectedRowStyle BackColor="#3366FF" />
                        <HeaderStyle CssClass="GrdHdr" Font-Underline="true"  />
                        <EditRowStyle BackColor="LightGreen" CssClass="DataCellGridEdit" />
                        
                        <Columns>
                        <asp:TemplateField>
                                 <EditItemTemplate>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                         CommandName="Update" Text="Upd"></asp:LinkButton>
                                     &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                         CommandName="Cancel" Text="Can"></asp:LinkButton>
                                 </EditItemTemplate>
                                 <ItemTemplate>
                                     <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                         CommandName="Edit" Text="Edt"></asp:LinkButton>
                                     &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                         CommandName="Delete" Text="Del" OnClientClick="return getConfirmationOnDelete();"></asp:LinkButton>
                                 </ItemTemplate>
                                 
                                 <ItemStyle HorizontalAlign="Center" Width="60px" />
                              </asp:TemplateField>

                            <asp:TemplateField HeaderText="ID" SortExpression="fid" Visible="true">
                                <ItemTemplate > 
                                  <asp:Label ID="lblFid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"fid")%>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate >
                                  <asp:Label ID="lblFidEdt" runat="server" Text=""></asp:Label>  
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
                           </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="BDM" SortExpression="bdm"    >
                                <ItemTemplate> 
                                  <asp:Label ID="lblBDM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"bdm")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                 <asp:Label Font-Size="Small" ID="lbl2" runat="server" Text="BDM :"></asp:Label><br />
                                  <asp:TextBox ID="txtBDM" Width="120px" runat="server" MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quote ID" SortExpression="quoteID" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteID")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl3" runat="server" Text="Quote ID"></asp:Label><br />
                                  <asp:TextBox ID="txtquoteID" Width="90px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="End User" SortExpression="endUser" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblendUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"endUser")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl4" runat="server" Text="End User :"></asp:Label><br />
                                  <asp:TextBox ID="txtendUser" Width="120px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country" SortExpression="country" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblcountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "country")%>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl6" runat="server" Text="Country"></asp:Label><br />
                                  <asp:DropDownList ID="ddlcountry" width="100px" BackColor="#9dd8ff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" > </asp:DropDownList>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reseller" SortExpression="resellerName" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblresellerCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "resellerCode")%>'></asp:Label>  
                                  <asp:Label ID="lblresellerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "resellerName")%>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl5" runat="server" Text="Reseller"></asp:Label>
                                 &nbsp;<asp:LinkButton ID="lnkBtnAddCustForQuote" Text="Add Selected" runat="server" onclick="lnkBtnAddCustForQuote_Click"></asp:LinkButton>
                                  <asp:DropDownList ID="ddlreseller" width="98%" BackColor="#9dd8ff" runat="server" ></asp:DropDownList><br />
                                 &nbsp;<asp:ListBox ID="lstResellerForQt" runat="server" BackColor="#ccffcc" Width="99%" Font-Size="Smaller" Height="100px"></asp:ListBox>
                                    <br /><asp:LinkButton ID="lnkBtnRemoveCustForQuote" Text="Remove Selecet" runat="server" onclick="lnkBtnRemoveCustForQuote_Click"></asp:LinkButton>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="140px"/>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="BU" SortExpression="BU" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblBU" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BU")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl7" runat="server" Text="BU"></asp:Label><br />
                                  <asp:DropDownList ID="ddlBU" width="100px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="130px" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Goods Descr" SortExpression="goodsDescr" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblgoodsDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"goodsDescr")%>'></asp:Label>  
                                </ItemTemplate>
                                 <%--<EditItemTemplate>
                                  <asp:Label ID="lbl8" runat="server" Text="Goods Description"></asp:Label><br />
                                  <asp:TextBox ID="txtgoodsDescr" Width="250px" runat="server"  MaxLength="1000" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>--%>

                                <EditItemTemplate>

                                <table width="100%" border="0">
                                       <tr>
                                          <td colspan="2"><asp:Label ID="lbl8" runat="server" Text="Goods Description"></asp:Label><br />
                                          <asp:TextBox ID="txtgoodsDescr" Width="260px" runat="server"  MaxLength="1000" BackColor="#ffffcc"></asp:TextBox></td>
                                       </tr>
                                       <tr>
                                          <td colspan="2"><asp:Label ID="lbl15" Font-Size="Smaller" runat="server" Text="Remarks"></asp:Label><br />
                                          <asp:TextBox ID="txtremarks" Width="260px" runat="server"  MaxLength="40" BackColor="#ffffcc"></asp:TextBox></td> 
                                       </tr>
                                       <tr>
                                          <td style="width:45%;"><asp:Label ID="lbl16" runat="server" Text="Status"></asp:Label>
                                            <asp:DropDownList ID="ddldealStatusEdt" width="80px" BackColor="#9dd8ff" runat="server" ></asp:DropDownList></td> 
                                          <td style="width:55%;"><asp:Label ID="lbl17" Font-Size="Smaller" runat="server" Text="Last Updated"></asp:Label>
                                              <asp:TextBox ID="txtuserLastUpdateDate" Width="60px" runat="server" Font-Size="Small" BackColor="#ffffcc"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtuserLastUpdateDate_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtuserLastUpdateDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender></td> 
                                       </tr>
                                  </table>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"  Width="150px"  />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quote Date" SortExpression="quoteDate" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteDate","{0:MM/dd/yyyy}")%>'></asp:Label>   
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <table width="100%" border="0">
                                      <tr>
                                        <td>
                                          <asp:Label ID="lbl9" Font-Size="Smaller" runat="server" Text="Quote Date"></asp:Label><br />
                                               <asp:TextBox ID="txtquoteDate" Width="55px" runat="server" Font-Size="Small"  BackColor="#ffffcc"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtquoteDate_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="txtquoteDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                        TodaysDateFormat="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                            </td>
                                      </tr>
                                      <tr>
                                        <td>
                                                <asp:Label ID="lbl10" Font-Size="Smaller" runat="server" Text="Quote MMM"></asp:Label><br />
                                                <asp:TextBox ID="txtquoteMonthMMM" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>
                                        </td>
                                      </tr>

                                      <tr>
                                        <td>
                                                <asp:Label ID="lbl11" Font-Size="Smaller" runat="server" Text="Quote YR"></asp:Label><br />
                                                <asp:TextBox ID="txtquoteYear" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>
                                        </td>
                                      </tr>

                                    </table>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Quote MMM" SortExpression="quoteMonthMMM" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteMonthMMM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteMonthMMM")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <%--<asp:Label ID="lbl10" Font-Size="Smaller" runat="server" Text="Quote MMM"></asp:Label><br />
                                  <asp:TextBox ID="txtquoteMonthMMM" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>--%>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Quote YR" SortExpression="quoteYear" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteYear")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <%--<asp:Label ID="lbl11" Font-Size="Smaller" runat="server" Text="Quote YR"></asp:Label><br />
                                  <asp:TextBox ID="txtquoteYear" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>--%>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="$ Cost" SortExpression="Cost"  >
                                <ItemTemplate> 
                                  <asp:Label ID="lblCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cost","{0:F}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lblECost" runat="server" Text="$ Cost"></asp:Label><br />
                                  <asp:TextBox ID="txtCost" Width="70px" Text="0" runat="server" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="$ Landed" SortExpression="Landed"  >
                                <ItemTemplate> 
                                  <asp:Label ID="lblLanded" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Landed","{0:F}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lblELanded" runat="server" Text="$ Landed"></asp:Label><br />
                                  <asp:TextBox ID="txtLanded" Width="70px" Text="0" runat="server" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>


                             <asp:TemplateField HeaderText="$ Total Sale" SortExpression="value" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"value","{0:F}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate   >
                                  <asp:Label ID="lbl12" runat="server" Text="$ Total Sale"></asp:Label><br />
                                  <asp:TextBox ID="txtvalue" Width="70px" Text="0" runat="server" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Close MMM" SortExpression="expClosingMonthMMM" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblexpClosingMonthMMM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"expClosingMonthMMM")%>'></asp:Label>  
                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                                  <asp:Label ID="lbl13" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                  <asp:DropDownList ID="ddlexpClosingMonthMMM" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>--%>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Close YR" SortExpression="expClosingYear" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblexpClosingYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"expClosingYear")%>'></asp:Label>  
                                </ItemTemplate>
                                <%-- <EditItemTemplate   >
                                  <asp:Label ID="lbl14" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                  <asp:DropDownList ID="ddlexpClosingYear" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>--%>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks" SortExpression="remarks" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblremarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"remarks")%>'></asp:Label>
                                </ItemTemplate>
                                 <%--<EditItemTemplate>
                                  <asp:Label ID="lbl15" Font-Size="Smaller" runat="server" Text="Remarks"></asp:Label><br />
                                   <asp:TextBox ID="txtremarks" Width="250px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" SortExpression="dealStatus" >
                                <ItemTemplate> 
                                  <asp:Label ID="lbldealStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "dealStatus")%>'></asp:Label>  
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                 <asp:Label ID="lbl16" runat="server" Text="Deal Status"></asp:Label><br />
                                  <asp:DropDownList ID="ddldealStatusEdt" width="80px" BackColor="#9dd8ff" runat="server" >
                                  </asp:DropDownList>
                                </EditItemTemplate>--%>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Last Update" SortExpression="userLastUpdateDate" >
                                <ItemTemplate> 
                                  <asp:Label ID="lbluserLastUpdateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"userLastUpdateDate","{0:MM/dd/yyyy}")%>'></asp:Label> 
                                </ItemTemplate>
                                 <%--<EditItemTemplate>
                                 <asp:Label ID="lbl17" Font-Size="Smaller" runat="server" Text="Last Updated"></asp:Label><br />
                                  <asp:TextBox ID="txtuserLastUpdateDate" Width="65px" runat="server" Font-Size="Small" BackColor="#ffffcc"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtuserLastUpdateDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtuserLastUpdateDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                </EditItemTemplate>--%>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Order Date" SortExpression="orderBookedDate" >
                                <ItemTemplate> 
                                  <asp:Label ID="lblorderBookedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"orderBookedDate","{0:MM/dd/yyyy}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                    <table width="100%" border="0">
                                        <tr>
                                            <td>
                                                 <asp:Label ID="lbl13" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                                   <asp:DropDownList ID="ddlexpClosingMonthMMM" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>  
                                                 <asp:Label ID="lbl14" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                                <asp:DropDownList ID="ddlexpClosingYear" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                             <asp:Label ID="lbl18" runat="server" Text="Order Date"></asp:Label><br />
                                               <asp:TextBox ID="txtorderBookedDate" Width="57px" runat="server" Font-Size="Small" BackColor="#ffffcc"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtorderBookedDate_CalendarExtender" runat="server" 
                                                        Enabled="True" TargetControlID="txtorderBookedDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                        TodaysDateFormat="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        
                                    </table>

                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                                  
                        </Columns>
                    </asp:GridView>
            </td></tr>
           </table> 

                   <br />

                   <asp:Panel ID="PanelNewCreation" runat="server" Visible="false" >  
                             <table style="width:700px" border="0">
                              <tr>
                                <td style="width:25%"></td>
                                <td style="width:75%;font-weight:bold"> ADD NEW RESELLER</td>
                              </tr>
                              <tr>
                                  <td colspan="2" align="center">
                                    <asp:Label ID="lblAddNewMsg" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                 </td>
                              </tr>
                             <tr>
                                <td align="left">Select Country</td>
                                <td align="left"><asp:DropDownList ID="ddlCountryForNewReseller" runat="server"  BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                                 width="150px"></asp:DropDownList>
                               </td>
                             </tr>
                            <tr>
                             <td align="left">New Reseller</td>
                              <td align="left">
                                <asp:TextBox ID="txtnewDesc" BackColor="#999966" runat="server" Width="75%" MaxLength="250"></asp:TextBox>
                                &nbsp;Max 250 Chrs
                              </td>
                            </tr>
                             <tr>
                              <td align="left">&nbsp;</td>
                              <td align="left">
                                 <asp:Button ID="btnSaveNew" runat="server" Text="Save" width="100px" 
                                                BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                                ForeColor="Black" ToolTip="Save new value to database" 
                                     onclick="btnSaveNew_Click" />
                                     &nbsp;
                               <asp:Button ID="btnCancel" runat="server" Text="Cancel/Exit" width="100px" 
                                                BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                                ForeColor="Black" ToolTip="Save new value to database" 
                                      onclick="btnCancel_Click" />
                                  
                              </td>
                             </tr>
                           </table>
                        </asp:Panel>
                        
                        <br />

                        <asp:Panel ID="PnlReport" runat="server" Visible="false" >  
                         <div>
                            <center>
                                    <asp:Label ID="lblPageTitle" runat="server" Text="Funnel Report" 
                                        ForeColor="Blue" Font-Size="18pt"></asp:Label>
                                <br />
                                <table width="99%" border="0px">
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMsg1" runat="server" Text="" ForeColor="red"></asp:Label>
                                        </td>
                                    </tr>
               
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblFromDate" runat="server" Text="From Date" 
                                                Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        &nbsp; (MM-DD-YYYY)</td>
                    
                                    </tr>
                                    <tr>
                                        <td align="right"><asp:Label ID="lblToDate" runat="server" Text="To Date" 
                                                Font-Bold="True" ForeColor="Maroon" Font-Size="13px"></asp:Label></td>
                                        <td align="left"><asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        &nbsp;(MM-DD-YYYY)
                                        &nbsp;
                                         <asp:Button ID="btnViewData" runat="server" Text="View Report" Font-Bold="True"
                                                 Width="150px" onclick="btnViewData_Click" /> 
                                                 &nbsp;
                                      </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                        
                                        </td>
                                        <td style="width: 80%">
                        
                                            <asp:Label ID="lblUseQryNow" runat="server" Text="." Visible="False"></asp:Label>
                                            &nbsp;
                                            <asp:Label ID="lblFileName" runat="server" Text="." Visible="False"></asp:Label>
                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                                Report View  :  Rows : &nbsp; <asp:Label ID="lblCnt" runat="server" Text="0" ForeColor="Red"></asp:Label>
                                                &nbsp;
                                                <asp:Button ID="btnReportExl" runat="server" Text="Get Excel Report" width="150px" 
                                                BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                                ForeColor="Black" ToolTip="Get Excel Report" onclick="btnReportExl_Click" />
                                     &nbsp;
                                               <asp:Button ID="btnExit" runat="server" Text="Go Back! to Funnel Data" width="200px" 
                                                                BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                                                ForeColor="Black" ToolTip="Go Back! to Funnel Data" 
                                                    onclick="btnExit_Click"  />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">

                                        &nbsp;
                    
                                        </td>
                                    </tr>
                                </table>
                                <br />

                                <div style="overflow:auto;width:99%;background-color:Gray;">
                                     <asp:GridView ID="GridView1" runat="server" Width="99%" 
                                          AllowPaging="false" Font-Size="12px"
                                          CssClass="CGrid"                    
                                          AlternatingRowStyle-CssClass="alt"
                                          PagerStyle-CssClass="pgr" AllowSorting="False" AlternatingRowStyle-BackColor="#FFFFCC">
                                      </asp:GridView>
                               </div>
                            </center>
                        </div>
                       </asp:Panel>
                   </center>
                </div>
</asp:Content>





