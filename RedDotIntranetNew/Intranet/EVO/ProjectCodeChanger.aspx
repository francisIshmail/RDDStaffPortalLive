<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="ProjectCodeChanger.aspx.cs" Inherits="Intranet_EVO_ProjectCodeChanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content-area">
            <center>
            <br />
            <p class="title-txt">Project Code Changer</p>   
            <br />
            <asp:Panel ID="PanelProjectCodeChanger" runat="server" GroupingText="ProjectCodeChanger" BackColor="#EDEDE4">
            <table width="95%" border="0">
                 <tr> 
                  <td colspan="3"><asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td> 
                 </tr>
                 
                 <tr> 
                  <td colspan="3">&nbsp;</td> 
                 </tr>

                 <%------------------------------------------------------Level 1-------------------------------------------------------------%>
                 <tr>  
                   <td style="width:30%" valign="bottom">
                      Select A Region
                   </td>
                   <td style="width:40%" valign="bottom"> 
                     <asp:DropDownList ID="ddlDB" runat="server" Width="200px" AutoPostBack="true" 
                           Font-Bold="true" BackColor="#F2F2F2" 
                           onselectedindexchanged="ddlDB_SelectedIndexChanged" >
                         <asp:ListItem Value="DU">Triangle</asp:ListItem>
                         <asp:ListItem Value="TZ">Tanzania</asp:ListItem>
                         <asp:ListItem Value="EPZ">EPZ</asp:ListItem>
                         <asp:ListItem Value="KE">Kenya</asp:ListItem>
                         <asp:ListItem Value="UG">Uganda</asp:ListItem>
                       </asp:DropDownList>
                    </td> 
                   <td style="width:30%" valign="bottom">
                      &nbsp;
                   </td>
                  </tr>

                 <tr> 
                  <td colspan="3">&nbsp;</td> 
                 </tr>

                 <tr> 
                  <td style="width:30%" valign="bottom">
                    Enter Audit No.
                  </td>
                  <td style="width:40%" valign="bottom"> 
                      <asp:TextBox ID="txtAuditNo" Text="" Width="130px" runat="server" 
                          Font-Bold="True" Font-Size="8pt"></asp:TextBox>
                   </td>
                   <td style="width:30%" valign="bottom">
                    &nbsp;
                   </td>
                 </tr>

                 <tr> 
                  <td style="width:30%" valign="bottom">
                    &nbsp;
                  </td>
                  <td style="width:40%" valign="bottom">
                        Associated Invoice       : &nbsp;
                        <asp:Label ID="lblInvoice" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        <asp:Label ID="lblInvoiceId" runat="server" Text="" ForeColor="Blue" Visible="false"></asp:Label>
                  </td>
                   <td style="width:30%" valign="bottom">
                   &nbsp;
                   </td>
                 </tr>

                 <tr> 
                  <td style="width:30%" valign="bottom">
                    &nbsp;
                  </td>
                  <td style="width:40%" valign="bottom"> 
                     Current Project Code : &nbsp;
                        <asp:Label ID="lblPrjCode" runat="server" Text="" ForeColor="Blue"></asp:Label>&nbsp;
                        <asp:Label ID="lblPrjId" runat="server" Text="" ForeColor="Blue"  Visible="false"></asp:Label>
                  </td>
                   <td style="width:30%" valign="bottom">
                    <asp:Button ID="btnFind" runat="server" Text="? Find Invoice" Font-Bold="True" 
                           Width="100px" Font-Size="8pt" onclick="btnFind_Click" />
                   </td>
                 </tr>

                 <tr> 
                  <td colspan="3"><hr /><br /></td> 
                 </tr>
             <%------------------------------------------------------Level 2-------------------------------------------------------------%>
                 <tr>  
                   <td style="width:30%" valign="top">
                      Select New Project
                   </td>
                   <td style="width:40%" valign="top"> 
                     <asp:DropDownList ID="ddlPrjs" runat="server" Width="200px" AutoPostBack="false" Enabled="false"
                           Font-Bold="False" BackColor="#F2F2F2" Font-Size="9pt" >
                       </asp:DropDownList>
                       &nbsp; Total : <asp:Label ID="lblPrjsCount" runat="server" Text="0" ForeColor="Blue"></asp:Label>

                   </td> 
                   <td style="width:30%" valign="bottom">
                      &nbsp;
                   </td>
                 </tr>
             
                <tr> 
                  <td colspan="3">
                  &nbsp;
                   </td> 
                 </tr>

                 <tr>  
                   <td style="width:30%" valign="top">
                     &nbsp;
                   </td>
                   <td style="width:40%" valign="top"> 
                    <asp:Button ID="btnUpdate" runat="server" Text="Click ! Update Now" 
                           Font-Bold="True" Width="200px" Enabled="False" onclick="btnUpdate_Click" />
                   </td> 
                   <td style="width:30%" valign="bottom">
                      &nbsp;
                   </td>
                 </tr>

                  <tr> 
                  <td colspan="3">
                    <hr /><br />
                   </td> 
                 </tr>

                 <tr>  
                   <td style="width:30%" valign="top">
                    Process Update :
                   </td>
                   <td style="width:40%" valign="top"> 
                     <asp:Label ID="lblUpdate" runat="server" Text="" ForeColor="Blue"></asp:Label>
                   </td> 
                   <td style="width:30%" valign="bottom">
                      &nbsp;
                   </td>
                 </tr>
                 
                 <tr> 
                  <td colspan="3">
                  &nbsp;
                   </td> 
                 </tr>

            </table>
            <br />
             NOTE: Before executing this program, make sure to have a backup of the database and every one must log out of Evolution.
            </asp:Panel>
            <br />
          </center>
      </div>
</asp:Content>

