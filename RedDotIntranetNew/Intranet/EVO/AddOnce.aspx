<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="AddOnce.aspx.vb" Inherits="Intranet_EVO_AddOnce1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="Addonce Web Ver-2012.Oct.15"></asp:Label></div>
                </td>
              </tr>
            </table>   
    <table width="100%" style="background-color:White">
        <tr style="height:25px">
            <td width="70%" align="center">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Text="Not Connected" ForeColor="#993399"></asp:Label>
            </td> 
            <td align="center" >
                <asp:Button ID="cmdConnectDb" runat="server" 
                    Text="Click! To Connect To The Database" width="230px"/>
            </td>
        </tr>
   
        <tr> 
            <td colspan="2" align="left" style="height:25px">
               <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
    </table>

 <asp:Panel ID="Panel1" runat="server" >

    <table width="100%" style="background-color:White">
        <tr>
            
            
            <td valign="top" style="width:30%">

                <asp:Panel ID="pnlItem" runat="server" GroupingText="Item" CssClass="addOncePanel" Enabled="False">
                       <table style="width:100%;height:259px">
                            <tr>
                                <td>
                                    Simple Code</td>
                                <td>
                                    <asp:TextBox ID="txtSimpleCode" runat="server" MaxLength="20"></asp:TextBox>
                                    &nbsp;Max 20 Chrs</td>
                            </tr>
                            <tr>
                                <td>
                                    Unique Code</td>
                                <td>
                                    <asp:Label ID="lblUniqueDesc" runat="server" BackColor="Gray" 
                                        Width="200px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Description 1</td>
                                <td>
                                    <asp:TextBox ID="txtDesc1" runat="server" MaxLength="50"></asp:TextBox>
                                    &nbsp;Max 50 Chrs</td>
                            </tr>
                            <tr>
                                <td>
                                    Description 2</td>
                                <td>
                                    <asp:TextBox ID="txtDesc2" runat="server" MaxLength="50"></asp:TextBox>
                                    &nbsp;Max 50 Chrs</td>
                            </tr>
                            <tr>
                                <td>
                                    Description 3</td>
                                <td>
                                    <asp:TextBox ID="txtDesc3" runat="server" MaxLength="50"></asp:TextBox>
                                    &nbsp;Max 50 Chrs</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                </asp:Panel>    
            </td>

            <td valign="top" style="width:30%">
                <asp:Panel ID="pnlItemType" runat="server" GroupingText="Item Type" CssClass="addOncePanel" Enabled="False">
                <table style="height: 259px">
                        <tr>
                            <td><asp:CheckBox ID="chkActive" runat="server" 
                                    Text="Active" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkSegment" runat="server" 
                                    Text="Segmented Item" AutoPostBack="True" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkService" runat="server" 
                                    Text="Service Item" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkWarehouse" runat="server" 
                                    Text="Warehouse Item" Checked="True" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkSerial" runat="server" 
                                    Text="Serial Number Item" /></td>
                        </tr>
                        <tr>
                            
                            <td>&nbsp&nbsp
                                <asp:CheckBox ID="chkAllowDupSerial" runat="server" 
                                    Text="Allow Duplicate Serial Number" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp&nbsp
                                <asp:CheckBox ID="chkStrictSerial" runat="server" 
                                    Text="Strict Serial Number Tracking" />
                            </td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkCommission" runat="server" 
                                    Text="Commissionable Item" /></td>
                        </tr>
                        <tr>
                            <td><asp:CheckBox ID="chkReconcile" runat="server" Text="Reconcile" /></td>
                        </tr>
                        </table>   
                </asp:Panel>
            </td>
            
            
            <td valign="top" style="width:30%">

                <asp:Panel ID="pnlsegments" runat="server" GroupingText="Segments" CssClass="addOncePanel" Enabled="False">

                    <table style="height: 259px" width="100%">
                        <tr>
                            <td width="40%">
                                <asp:CheckBox ID="chkSeg1" runat="server" Text="Category" AutoPostBack="True" Checked="True" Enabled="False" />
                            </td>
                            <td width="60%">
                                <asp:DropDownList ID="cmbCatTRI" runat="server" Height="17px" Width="120px" AutoPostBack="True">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </td>   
                        </tr>

                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSeg2" runat="server" Text="Manufacture" AutoPostBack="True" />
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbManTRI" runat="server" Height="17px" Width="120px" Enabled="False" AutoPostBack="True"></asp:DropDownList>   
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:CheckBox ID="chkSeg3" runat="server" Text="Business Unit" AutoPostBack="True" Enabled="False" />
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbBUTRI" runat="server" Height="17px" Width="120px" Enabled="False" AutoPostBack="True" style="margin-bottom: 0px">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>    
                            </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSeg4" runat="server" Text="Product Line" AutoPostBack="True" Enabled="False" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbPLTRI" runat="server" Height="17px" Width="120px" Enabled="False" AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtPLTRI" Width="120px" runat="server" Visible="False" Enabled="false" Height="15px"></asp:TextBox>
                                    &nbsp;
                                    <asp:CheckBox ID="chkNewPL" runat="server" text="New PL" Checked="false" AutoPostBack="True" ForeColor="#993399" Font-Bold="true" />
                                </td>
                            </tr>

                           <tr>
                                <td>
                                    <asp:CheckBox ID="chkSeg5" runat="server" Text="Model" AutoPostBack="True" Enabled="False" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbModelTRI" runat="server" Height="17px" Width="120px" Enabled="False" AutoPostBack="True">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtModelTRI" Width="120px" runat="server" Visible="False" Enabled="false" Height="15px"></asp:TextBox>
                                    &nbsp;
                                    <asp:CheckBox ID="chkNewModel" runat="server" text="New Model" Checked="false" AutoPostBack="True" ForeColor="#993399" Font-Bold="true" />
                                </td>
                            </tr>

                            <tr>
                                <td >
                                    <asp:label ID="chkSeg7" runat="server" Text="Dashboard Catagory" AutoPostBack="True" Font-Bold="true" />
                                    
                                </td>
                                <td width="121px">
                                    <asp:DropDownList ID="cmbDashCategory" runat="server" Width="150px" AutoPostBack="True"  BackColor="Silver">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblColumnCount" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                                    &nbsp;
                                    <asp:LinkButton ID="lnkNewDash" runat="server" Text="New DC" ForeColor="#993399" Font-Bold="true"></asp:LinkButton>
                                </td>
                            </tr>

                             <tr>
                                <td>
                                    <asp:CheckBox ID="chkSeg6" runat="server" Text="Part #" AutoPostBack="True" Visible="False" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbPartTRI" runat="server" Height="17px" Width="120px" 
                                        Enabled="False" AutoPostBack="True" Visible="False">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList> 
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                 </td>
        </tr>

        <tr>
            <td colspan="3">
                <asp:Panel ID="pnlNewDashCategory" runat="server" GroupingText="Manage Dashboard Categories" Visible="false" Font-Bold="true" BackColor="#ffffcc">
                    <%--<table width="100%">
                        <tr>
                            <td align="center" style="width:98%">
                                <h2>Manage Dashboard categories</h2>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/images/close-icon.png" ToolTip="Close This Panel..." />
                            </td>
                        </tr>
                    </table>--%>

                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/images/close-icon.png" ToolTip="Close This Panel..." />
                            </td>   
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel runat="server" GroupingText="New Dashboard Category" Font-Bold="true" >
                                    <table width="100%">
                                        <%--<tr style="height:60px">
                                            <td colspan="2" align="center" valign="top">
                                                <h3><u>Add New Dashboard Category</u></h3>                                        
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="center">
                                                <asp:TextBox ID="txtNewCategory" runat="server" Width="220px" BackColor="#d9ffd9"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add Category" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:Panel runat="server" GroupingText="Delete Selected Category" Font-Bold="true">
                                    <table width="100%">
                                        <%--<tr style="height:60px">
                                            <td colspan="2" align="center" valign="top">
                                                <h3><u>Selecte Dashboard Category</u></h3>                                        
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td align="center">
                                                <asp:DropDownList ID="ddlDashCategory" runat="server" Width="220px" AutoPostBack="true" BackColor="Silver" ></asp:DropDownList>&nbsp;
                                                <asp:Label ID="lblCount" runat="server" Font-Bold="True" ForeColor="#003366"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete Category" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>

        <tr>
            <td valign="top">
                <asp:Panel ID="pnlGrouping" runat="server" GroupingText="Grouping" CssClass="addOncePanel" Enabled="False">
                    <table style="height: 234px">
                        <tr>
                            <td width="144px">
                                Group</td>
                            <td>
                                <asp:DropDownList ID="cmbGroup" runat="server" Height="20px" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Bin Location</td>
                            <td>
                                <asp:DropDownList ID="cmbLocation" runat="server" Height="20px" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Pack Code</td>
                            <td>
                                <asp:DropDownList ID="cmbPack" runat="server" Height="20px" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Re-Order Level</td>
                            <td>
                                <asp:TextBox ID="txtReOrderLevel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Re-Order Qty</td>
                            <td>
                                <asp:TextBox ID="txtReOrderQty" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Minimum Level</td>
                            <td>
                                <asp:TextBox ID="txtMinLevel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="144px">
                                Maximum Level</td>
                            <td>
                                <asp:TextBox ID="txtMaxLevel" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
            <td valign="top">
                <asp:Panel ID="pnlPricing" runat="server" GroupingText="Pricing" CssClass="addOncePanel" Enabled="False" >
                    <table style="height: 234px">
                        <tr>
                            <td style="width:60%">Average Unit Cost</td>
                            <td style="width:40%">
                                <asp:TextBox ID="txtAvgCost" runat="server" Width="120px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width:60%">Last GRV Cost</td>
                            <td style="width:40%"><asp:TextBox ID="txtGRVCost" runat="server" Width="120px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:RadioButton ID="optAvg" runat="server" GroupName="List111" 
                                    Text="Weighted Average Cost" />
                                <br />
                                <asp:RadioButton ID="optLatest" runat="server" GroupName="List111" 
                                    Text="Latest Cost" />
                                <br />
                                <asp:RadioButton ID="optManual" runat="server" GroupName="List111" 
                                    Text="Manual Cost" />
                                   
                            </td>
                        </tr>
                    </table>                                    
                </asp:Panel>
            </td>
            <td valign="top">
                
                <asp:Panel ID="pnlStatusItem" runat="server" GroupingText="Status Item Created Or Not" CssClass="addOncePanel" Enabled="true">
                    <table style="height: 234px" width="100%">
                        <tr>
                            <td style="width:35%" align="left">
                                <asp:Label ID="Label1" runat="server" Text="Database" ForeColor="Green" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:30%" align="center">
                                <asp:Label ID="Label2" runat="server" Text="Create In" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width:35%" align="center">
                                <asp:Label ID="Label3" runat="server" Text="Status Created or Not" ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text="Tanzania" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="CheckCreateInTZ" runat="server" Checked="True" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblResTZ" runat="server" Text="..." ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                         <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Dubai" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="CheckCreateInDU" runat="server" Checked="True" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblResDU" runat="server" Text="..." ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Kenya" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="CheckCreateInKE" runat="server" Checked="True" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblResKE" runat="server" Text="..." ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="EPZ" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="CheckCreateInEPZ" runat="server" Checked="True" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblResEPZ" runat="server" Text="..." ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="UgandaKE" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:CheckBox ID="CheckCreateInUG" runat="server" Checked="True" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblResUG" runat="server" Text="..." ForeColor="Red" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
   
        <tr>
            <td style="width:100%" colspan="3">
                <asp:Panel ID="pnlMatrix" runat="server" GroupingText="Price Matrix" CssClass="addOncePanel" >
                            
                    <asp:CheckBox ID="chkPriceList" runat="server" Text="Price List Matrix Is Applicable If Selected, Price List Matrix Is Currently Disabled, as it is not applicable for now" ForeColor="#FF3535" Enabled="False" Font-Bold="true" /><br />
                            
                    <asp:Label ID="lblmsgPriceListMatrix" runat="server" Text=""></asp:Label>

                    <asp:GridView ID="MSFlexGrid1" runat="server" CellPadding="4" GridLines="Horizontal" Width="60%" AutoGenerateColumns="False"  AllowSorting="True" 
                    AllowPaging="True" PageSize="5" Enabled="False" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px">
                                
                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price List">
                                <ItemTemplate>
                                    <asp:Label ID="lblPriceList" runat="server" Text='<%#Eval("cName") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mark UP%">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMarkUp" runat="server" Text="0"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excl Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtExclPrice" runat="server" Text="0"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Incl Price">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtInclPrice" runat="server" Text="0"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </asp:Panel>
            </td>

        </tr>
                 
            <tr>
                <td valign="top" colspan="3">
                    <asp:Panel ID="pnlBtn1" runat="server" CssClass="addOncePanel" GroupingText="Insert Values">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblInfo" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="cmdAdd" runat="server" Text="Add Stock Item" width="150px" 
                                        Enabled="False" />
                                </td>
                            </tr>
                                       
                        </table>
                    </asp:Panel>
                </td>
                            
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

