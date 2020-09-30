<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="EditStockItemCSharp.aspx.cs" Inherits="Intranet_EVO_EditStockItemCSharp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div style="width:100%;margin-bottom:30px;margin-top:5px">        <%--margin-left:10%;--%>
            <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="Edit Stock Web Ver-2012.Oct.15"></asp:Label></div>
                </td>
              </tr>
            </table>
           
            <asp:Panel ID="Panel1" style="width:100%" runat="server" BorderWidth="3px" BorderStyle="Inset" BorderColor="#CCCCCC">
                 <table style="width:98%;margin-bottom:10px;margin-top:10px;margin-left:1%;" border="0px">
                      <tr style="height:60px">
                        <td style="width:18%;padding-left:20px" align="left" >
                            <asp:Label ID="lbl" runat="server" Text="Connect with Database" Font-Bold="true" Font-Size="15px" Visible="true"></asp:Label>
                        </td>
                        <td style="width:20%"  align="left">
                            <asp:DropDownList ID="ddlDB" runat="server" Width="200px" AutoPostBack="true" 
                                Font-Bold="true" BackColor="#F2F2F2" 
                                onselectedindexchanged="ddlDB_SelectedIndexChanged">
                                <asp:ListItem Value="JA">Triangle</asp:ListItem>
                                <asp:ListItem Value="TZ">RedDotTanzania</asp:ListItem>
                                <asp:ListItem Value="EPZ">RED DOT DISTRIBUTION EPZ LTD</asp:ListItem>
                                <asp:ListItem Value="KE">Red Dot Distribution Limited - Kenya</asp:ListItem>
                                <asp:ListItem Value="UG">Uganda</asp:ListItem>
                            </asp:DropDownList>
                         </td>
                         
                         <td style="width:35%;padding-left:10px"  align="left">
                             <font color="#003366" size="2px"><b>Fetch Stock Item starting with character :</b></font>&nbsp;
                             <asp:DropDownList ID="ddlCodeFilter" runat="server" Width="60px" 
                                 Font-Bold="true" BackColor="#F2F2F2" AutoPostBack="true" 
                                 onselectedindexchanged="ddlCodeFilter_SelectedIndexChanged"></asp:DropDownList>
                         </td>
                         <td style="width:27%"  align="right">
                            <asp:Label ID="lblConMsg" runat="server" Text="Not Connected" ForeColor="Green" Font-Bold="true" Visible="true" Font-Size="9px"></asp:Label>&nbsp;
                            <asp:Button ID="btnConnect" runat="server" Font-Bold="true" ForeColor="#990033" 
                                 Text="Fetch Stock Items" width="150px" onclick="btnConnect_Click" />
                         </td>
                    </tr>
                    
                     <tr style="height:50px">
                        <td colspan="4">
                         <asp:Panel ID="pnlItem" runat="server" BorderWidth="0px" BorderStyle="Inset" Width="100%" BorderColor="#999999" >
                                <table style="width:100%;background-color:White" border="0px">
                                   <tr style="height:40px">
                                        <td style="width:10%" align="right" valign="top">
                                            <font color="#003366" size="3px"><b>Stock Code</b></font>
                                        </td>
                                        <td style="width:48%;padding-left:5px" valign="top">
                                            <asp:DropDownList ID="ddlSimpleCode" runat="server" Font-Bold="true" 
                                                ForeColor="White" AutoPostBack="true" Width="72%" BackColor="#CCCCCC" 
                                                onselectedindexchanged="ddlSimpleCode_SelectedIndexChanged"></asp:DropDownList>
                                            &nbsp;<asp:Label ID="lblSimpleCodeCount" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                        </td>
                                        <td style="width:12%;padding-right:20px" align="right" valign="top">
                                            <font color="#003366" size="2px"><b>Is Available In</b></font>
                                        </td>
                                        <td style="width:30%;" valign="top">
                                            <asp:Panel runat="server" ID="pnlExistItem" BorderColor="Black" >
                                                <table width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="CheckStatusTZ" runat="server" ForeColor="#333300" Text="Tanzania" Font-Bold="true" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="CheckStatusDU" runat="server" Text="Dubai" ForeColor="#333300" Font-Bold="true" Enabled="false" />
                                                        </td>
                                                         <td>
                                                            <asp:CheckBox ID="CheckStatusKE" runat="server" ForeColor="#333300" Text="Kenya" Font-Bold="true" Enabled="false" />
                                                        </td>
                                                         <td>
                                                            <asp:CheckBox ID="CheckStatusEPZ" runat="server" Text="EPZ" ForeColor="#333300" Font-Bold="true" Enabled="false" />
                                                         </td>
                                                         <td>
                                                            <asp:CheckBox ID="CheckStatusUG" runat="server" ForeColor="#333300" Text="Uganda" Font-Bold="true" Enabled="false" />
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
                        <td colspan="4">
                            <asp:Panel ID="pnlEditItem" runat="server"  BorderWidth="0px" BorderStyle="Inset" Width="100%" BorderColor="#999999" >
                                <table style="width:100%;padding-left:20px" align="left" >
                                    <tr style="height:50px">
                                        <td colspan="2" align="left">
                                          <h2>Stock Item Details</h2>
                                        </td>
                                    </tr>
                                  <tr style="height:30px">
                                     <td colspan="2" >
                                        <asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="Red" Font-Underline="false" Text="" Visible="true"></asp:Label>
                                     </td>
                                   </tr>
                                   <tr style="height:50px">
                                        <td colspan="2" valign="top">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:20%;padding-left:45px">
                                                        <font color="#003366" size="2px"><b>Existing Item Code : </b></font>
                                                    </td>
                                                    <td style="padding-left:5px" align="left">
                                                        <asp:Label ID="lblCode" runat="server" Font-Bold="true" Font-Underline="false" ForeColor="#003366" Text="" Visible="true"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height:50px">
                                        <td colspan="2" valign="top">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:15%;padding-left:45px">
                                                        Simple Code
                                                    </td>
                                                    <td style="width:30%;padding-left:5px" align="left">
                                                        <asp:TextBox ID="txtSimpleCode" runat="server" Width="150px" MaxLength="20" ></asp:TextBox>&nbsp;&nbsp;Max 20 Chrs
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUniqueDesc" runat="server" Font-Bold="true" Font-Underline="false" ForeColor="#003366" Text="" Visible="true" BackColor="Silver"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width:50%">
                                            <table width="100%" style="height:250px">
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg1" runat="server" Text="Category" AutoPostBack="true" Checked="true" Enabled="false" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbCatTRI" runat="server"  Width="280px" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg2" runat="server" Text="Manufacture" 
                                                            AutoPostBack="true" Enabled="true" oncheckedchanged="chkSeg2_CheckedChanged" />
                                                     </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbManTRI" runat="server"  Width="280px" Enabled="false" ></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg3" runat="server" Text="Business Unit" 
                                                            AutoPostBack="true" Enabled="false" oncheckedchanged="chkSeg3_CheckedChanged" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbBUTRI" runat="server"  Width="280px" Enabled="false"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg4" runat="server" Text="Product Line" 
                                                            AutoPostBack="true" Enabled="false" oncheckedchanged="chkSeg4_CheckedChanged" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbPLTRI" runat="server"  Width="180px" Enabled="false"></asp:DropDownList>
                                                         <asp:TextBox ID="txtPLTRI" Width="180px" runat="server" Visible="false" Enabled="false" Height="15px"></asp:TextBox>
                                                            &nbsp;
                                                          <asp:CheckBox ID="chkNewPL" runat="server" text="New PL" Checked="false" 
                                                            AutoPostBack="true" ForeColor="#993399" Font-Bold="true" Enabled="false" 
                                                            oncheckedchanged="chkNewPL_CheckedChanged" />
                                                      </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg5" runat="server" Text="Model" AutoPostBack="true" 
                                                            Enabled="false" oncheckedchanged="chkSeg5_CheckedChanged" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbModelTRI" runat="server"  Width="180px" 
                                                            Enabled="false"></asp:DropDownList>
                                                        <asp:TextBox ID="txtModelTRI" Width="180px" runat="server" Visible="false" 
                                                            Enabled="false" Height="15px"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:CheckBox ID="chkNewModel" runat="server" text="New Model" Checked="false" 
                                                            AutoPostBack="true" ForeColor="#993399" Font-Bold="true" Enabled="false" 
                                                            oncheckedchanged="chkNewModel_CheckedChanged" />                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:CheckBox ID="chkSeg6" runat="server" Text="Part #" AutoPostBack="true" 
                                                            Visible="false" Enabled="false" oncheckedchanged="chkSeg6_CheckedChanged" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbPartTRI" runat="server"  Width="180px" Enabled="false" 
                                                            Visible="false"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        <asp:label ID="chkSeg7" runat="server" Text="Dashboard Catagory" AutoPostBack="true" Font-Bold="true" />
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbDashCategory" runat="server" Width="280px"  
                                                            BackColor="Silver"></asp:DropDownList><asp:Label ID="lblColumnCount" runat="server" Font-Bold="true" ForeColor="#003366"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    
                                        <td style="width:50%">
                                            <table width="100%" style="height:250px">
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Group
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbGroup" runat="server" Width="250px"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Description 1
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:TextBox ID="txtDescription1" runat="server" Width="245px" MaxLength="50" ></asp:TextBox>&nbsp;Max 50 Chrs
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Description 2
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:TextBox ID="txtDescription2" runat="server" Width="245px" MaxLength="50" ></asp:TextBox>&nbsp;Max 50 Chrs
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Description 3
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:TextBox ID="txtDescription3" runat="server" Width="245px" MaxLength="50" ></asp:TextBox>&nbsp;Max 50 Chrs
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Average Cost
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:TextBox ID="txtavgCost" runat="server" Width="245px" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        GRV Cost
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:TextBox ID="txtgrvCost" runat="server" Width="245px" ></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:30%;padding-left:40px">
                                                        Item Active
                                                    </td>
                                                    <td style="width:70%;padding-left:5px">
                                                        <asp:DropDownList ID="cmbIsActive" runat="server" Width="250px">
                                                            <asp:ListItem Value="1">True</asp:ListItem>
                                                            <asp:ListItem Value="0">False</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:10%;" valign="top">
                                                        
                                                    </td>
                                                    <td style="padding-left:6px">
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="height:50px">
                                        <td colspan="2">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:88%">
                                                        <asp:Label ID="lblSuccess" runat="server" Font-Bold="true" ForeColor="#003399" Visible="true"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Update Item" Width="100px" 
                                                            Font-Bold="true" onclick="btnUpdate_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>  <%--MAIN TABLE--%>
              </asp:Panel>
        </div>
        </ContentTemplate>
    </asp:UpdatePanel>

        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:50%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
</asp:Content>



