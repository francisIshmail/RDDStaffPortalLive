<%@ Page Language="C#" MasterPageFile="~/Intranet/MasterFunnel.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="LoadFunnelDataSingleCustBasedQt.aspx.cs" Inherits="Intranet_Marketing_LoadFunnelDataSingleCustBasedQt" Title="Manage Funnel" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <link href="../css/adminGrid.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
     function clearLbl()
     {
      document.getElementById("lblMsg").innerText="";
     }
   </script>

   <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

       <table style="width:99%" border="0">
        <tr style="height:25px;"valign="middle">
        <td style="padding-left:5px;width:70%" align="left"><p class="title-txt">Manage Funnel Data</p></td>
        <td style="padding-right:5px;width:30%;color:Purple" align="right">
           <%-- <a href="/admin/default.aspx">
            <img src="/images/adminhome.jpg" usemap="" border="0" alt="Admin Home" />
            </a>    --%>
             Note : Data Listed belongs to Logged in User
        </td>
        </tr>
    </table>

   <div style="margin-bottom: 5px; margin-top:0px; text-align:left;">
   <center>
      <table id="tblMain" runat="server" border="1" cellpadding="0" cellspacing="0" class="innerTable" width="99%">
       <tr align="left">
           <td >&nbsp;<asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
           &nbsp;<asp:Label ID="lblprevCntryDbCode" runat="server" Text="" ForeColor="Blue" Visible="false"></asp:Label>
           </td>
       </tr>
           
       <tr style="height:15">
          <td style="width: 100%">
            <table width="100%" style="background-color:#c0c0c0" border="1">
             <tr style="background-color:#c0c0c0">
                <td align="left" style="width: 12%; color: Blue;">
                 <asp:Label ID="lblRowCnt" runat="server" ForeColor="#990099" Text="0" Font-Bold="true"></asp:Label>
                 &nbsp;
                 <asp:Button ID="btnNewDeal" runat="server" Text="New Deal" 
                       ForeColor="#990099" Font-Bold="True"  Visible="true"
                        ToolTip="Click !  to create new deal"
                        onclick="btnNewDeal_Click" Height="18px" Width="64px" Font-Size="10px"/>
                </td>

               <td align="left" style="width: 6%; color: Blue;"><asp:CheckBox ID="CheckAll" Text="List All ??" runat="server" Checked="true" Font-Bold="true" 
                       oncheckedchanged="CheckAll_CheckedChanged" AutoPostBack="true" ToolTip="If selected then no filter is applicable" /></td>
               <td align="left" style="width: 12%; color: Blue;"><asp:CheckBox ID="chkCountry" 
                       Text="Country" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkCountry_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrCountry" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="80px" onselectedindexchanged="ddlFltrCountry_SelectedIndexChanged"></asp:DropDownList>
                       </td>
                <td align="left" style="width: 18%; color: Blue;"><asp:CheckBox ID="chkReseller" 
                       Text="Reseller" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkReseller_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrReseller" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="100px" onselectedindexchanged="ddlFltrReseller_SelectedIndexChanged"></asp:DropDownList>
                         &nbsp;
                    <asp:Button ID="btnNewReseller" runat="server" Text="Add New" 
                       ForeColor="#990099" ToolTip="Click !Add New Customer To Funnel List" Font-Bold="True"
                        onclick="btnNewReseller_Click"  Height="18px" Width="64px" Font-Size="10px"/>

                       </td>
               <td align="left" style="width: 12%; color: Blue;"><asp:CheckBox ID="chkQT" 
                       Text="QT ID" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkQT_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrQT" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="100px" onselectedindexchanged="ddlFltrQT_SelectedIndexChanged"></asp:DropDownList>
                       </td>
               
              
               <td align="left" style="width: 10%; color: Blue;"><asp:CheckBox ID="chkBU" 
                       Text="BU" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkBU_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrBU" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="80px" onselectedindexchanged="ddlFltrBU_SelectedIndexChanged"></asp:DropDownList>
                       </td>
               <td align="left" style="width: 10%; color: Blue;"><asp:CheckBox ID="chkDealStatus" 
                       Text="Status" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkDealStatus_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrDealStatus" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="80px" onselectedindexchanged="ddlFltrDealStatus_SelectedIndexChanged">
                     </asp:DropDownList>
                       </td>
               <td align="left" style="width: 10%; color: Blue;"><asp:CheckBox ID="chkYear" 
                       Text="QT Year" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkYear_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrYear" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="60px" onselectedindexchanged="ddlFltrYear_SelectedIndexChanged">
                     </asp:DropDownList>
                       </td>
               <td align="left" style="width: 14%; color: Blue;"><asp:CheckBox ID="chkMonth" 
                       Text="QT MMM" runat="server" Checked="false" Font-Bold="true" 
                       oncheckedchanged="chkMonth_CheckedChanged" AutoPostBack="true" />
                       &nbsp;
                       <asp:DropDownList ID="ddlFltrMonth" runat="server" AutoPostBack="True" 
                         BackColor="#c0c0c0" ForeColor="Blue" Font-Size="9pt" 
                         width="50px" onselectedindexchanged="ddlFltrMonth_SelectedIndexChanged">
                     </asp:DropDownList>
                       </td>
               </tr>
           </table>
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
                        OnRowDeleting="Grid1_RowDeleting" onrowupdating="Grid1_RowUpdating" Font-Size="11px" >
                        
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
                                         CommandName="Update" Text="Update"></asp:LinkButton>
                                     &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                         CommandName="Cancel" Text="Cancel"></asp:LinkButton>
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
                                <EditItemTemplate>
                                  <asp:Label ID="lblFidEdt" runat="server" Text=""></asp:Label>  
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="30px"  />
                           </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="BDM" SortExpression="bdm">
                                <ItemTemplate> 
                                  <asp:Label ID="lblBDM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"bdm")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                 <asp:Label Font-Size="Small" ID="lbl2" runat="server" Text="BDM :"></asp:Label><br />
                                  <asp:TextBox ID="txtBDM" Width="120px" runat="server" MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Quote ID" SortExpression="quoteID">
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteID")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl3" runat="server" Text="Quote ID"></asp:Label><br />
                                  <asp:TextBox ID="txtquoteID" Width="90px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="End User" SortExpression="endUser">
                                <ItemTemplate> 
                                  <asp:Label ID="lblendUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"endUser")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl4" runat="server" Text="End User :"></asp:Label><br />
                                  <asp:TextBox ID="txtendUser" Width="120px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Country" SortExpression="country">
                                <ItemTemplate> 
                                  <asp:Label ID="lblcountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "country")%>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl6" runat="server" Text="Country"></asp:Label><br />
                                  <asp:DropDownList ID="ddlcountry" width="100px" BackColor="#9dd8ff" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" > </asp:DropDownList>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Reseller" SortExpression="resellerName">
                                <ItemTemplate> 
                                  <asp:Label ID="lblresellerCode" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "resellerCode")%>'></asp:Label>  
                                  <asp:Label ID="lblresellerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "resellerName")%>'></asp:Label>  
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl5" runat="server" Text="Reseller"></asp:Label><br />
                                  <asp:DropDownList ID="ddlreseller" width="100px" BackColor="#9dd8ff" runat="server" ></asp:DropDownList>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="150px"/>
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="BU" SortExpression="BU">
                                <ItemTemplate> 
                                  <asp:Label ID="lblBU" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BU")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                 <asp:Label ID="lbl7" runat="server" Text="BU"></asp:Label><br />
                                  <asp:DropDownList ID="ddlBU" width="100px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>
                               <ItemStyle CssClass="lighterData" HorizontalAlign="Left" Width="130px" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Goods Descr" SortExpression="goodsDescr">
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
                                          <asp:TextBox ID="txtremarks" Width="260px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox></td> 
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

                            <asp:TemplateField HeaderText="Quote Date" SortExpression="quoteDate">
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteDate","{0:MM/dd/yyyy}")%>'></asp:Label>   
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl9" Font-Size="Smaller" runat="server" Text="Quote Date"></asp:Label><br />
                                       <asp:TextBox ID="txtquoteDate" Width="65px" runat="server" Font-Size="Small"  BackColor="#ffffcc"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtquoteDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtquoteDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Quote MMM" SortExpression="quoteMonthMMM">
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteMonthMMM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteMonthMMM")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl10" Font-Size="Smaller" runat="server" Text="Quote MMM"></asp:Label><br />
                                  <%--<asp:DropDownList ID="ddlquoteMonthMMM" width="60px" runat="server" > </asp:DropDownList>--%>
                                  <asp:TextBox ID="txtquoteMonthMMM" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Quote YR" SortExpression="quoteYear">
                                <ItemTemplate> 
                                  <asp:Label ID="lblquoteYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"quoteYear")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl11" Font-Size="Smaller" runat="server" Text="Quote YR"></asp:Label><br />
                                  <%--<asp:DropDownList ID="ddlquoteYear" width="60px" runat="server" > </asp:DropDownList>--%>
                                  <asp:TextBox ID="txtquoteYear" Enabled="false" Width="50px" Text="0" runat="server"  BackColor="#9dd8ff"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="$ Value" SortExpression="value">
                                <ItemTemplate> 
                                  <asp:Label ID="lblvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"value","{0:F}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl12" runat="server" Text="$ Value"></asp:Label><br />
                                  <asp:TextBox ID="txtvalue" Width="70px" Text="0" runat="server" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Close MMM" SortExpression="expClosingMonthMMM">
                                <ItemTemplate> 
                                  <asp:Label ID="lblexpClosingMonthMMM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"expClosingMonthMMM")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl13" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                  <asp:DropDownList ID="ddlexpClosingMonthMMM" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Close YR" SortExpression="expClosingYear">
                                <ItemTemplate> 
                                  <asp:Label ID="lblexpClosingYear" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"expClosingYear")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl14" Font-Size="Smaller" runat="server" Text="Exp. Close"></asp:Label><br />
                                  <asp:DropDownList ID="ddlexpClosingYear" width="50px" BackColor="#9dd8ff" runat="server" > </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Remarks" SortExpression="remarks">
                                <ItemTemplate> 
                                  <asp:Label ID="lblremarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"remarks")%>'></asp:Label>
                                </ItemTemplate>
                                 <%--<EditItemTemplate>
                                  <asp:Label ID="lbl15" Font-Size="Smaller" runat="server" Text="Remarks"></asp:Label><br />
                                   <asp:TextBox ID="txtremarks" Width="250px" runat="server"  MaxLength="250" BackColor="#ffffcc"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"   />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" SortExpression="dealStatus">
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
                            
                            <asp:TemplateField HeaderText="Last Update" SortExpression="userLastUpdateDate">
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

                            <asp:TemplateField HeaderText="Order Date" SortExpression="orderBookedDate">
                                <ItemTemplate> 
                                  <asp:Label ID="lblorderBookedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"orderBookedDate","{0:MM/dd/yyyy}")%>'></asp:Label>  
                                </ItemTemplate>
                                 <EditItemTemplate>
                                  <asp:Label ID="lbl18" runat="server" Text="Order Date"></asp:Label><br />
                                       <asp:TextBox ID="txtorderBookedDate" Width="57px" runat="server" Font-Size="Small" BackColor="#ffffcc"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtorderBookedDate_CalendarExtender" runat="server" 
                                                Enabled="True" TargetControlID="txtorderBookedDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                TodaysDateFormat="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
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
                   </center>
                </div>
</asp:Content>





