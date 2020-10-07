<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="ManagePlan.aspx.cs" Inherits="Intranet_orders_ManagePlan" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:Panel ID="panel1" runat="server" CssClass="PlansPanel" GroupingText="Marketing Plans">

        <center>
         <table width="100%">
          <tr>
            <td style="width:97%" align="center">
             <h3>Manage New and Existing Marketing Plans Ver. 1-Apr-2017</h3>
            </td>
            <td style="width:3%">
              <a href="viewOrdersMKT.aspx?wfTypeId=10031">Back</a>
            </td>
          </tr>
         </table>
       </center>

        <br />

        <table width="100%">
          <tr style="height:5px;">
            <td style="width:15%"><asp:Label ID="lblAddEdit" runat="server" Text="" Font-Size="12px" 
                           ForeColor="#cc66ff" Font-Bold="false"></asp:Label>
             </td>
             <td style="width:35%" valign="bottom">
                 <asp:DropDownList ID="ddlPlans" runat="server" BackColor="#CCCCCC" 
                     Font-Size="9pt" Height="20px" Width="250px" 
                     onselectedindexchanged="ddlPlans_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                 &nbsp;Work For&nbsp;
                 <asp:DropDownList ID="ddlBaseYear" runat="server" BackColor="#CCCCCC" 
                     Font-Size="9pt" Height="20px" Width="80px" 
                     onselectedindexchanged="ddlBaseYear_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

             </td>
             <td style="width:50%">
              
              <table width="100%">
               <tr>
               <td style="width:29%">Total (<asp:Label ID="lblcnt" runat="server" Text="0" Font-Size="12px"></asp:Label>)</td>
                <td style="width:17%"><asp:Button ID="btnnew" runat="server" Text="New Plan" 
                        Width="70px" Font-Size="8pt" onclick="btnnew_Click" /></td>
                <td style="width:17%"><asp:Button ID="btnsave" runat="server" Text="Save Plan" 
                        Width="70px" Font-Size="8pt" Enabled="false" onclick="btnsave_Click" /></td>
                <td style="width:17%"><asp:Button ID="btndel" runat="server" Text="Delete Plan" 
                        Width="70px" Font-Size="8pt" Enabled="false" onclick="btndel_Click" /></td>
                <td style="width:20%"><asp:Button ID="btncancel" runat="server" Text="Cancel" 
                        Width="70px" Font-Size="8pt" Enabled="false" onclick="btncancel_Click" /></td>
               </tr>
              </table>
             </td>
            </tr>
            <tr>
             <td colspan="3"><hr style="color:Silver" /></td>
            </tr>
          <tr>
            <td colspan="3">
              <asp:Label ID="lblautoindex" runat="server" Text="" Font-Size="12px" ForeColor="Silver" Font-Bold="true"  Visible="true"></asp:Label>
              <asp:Label ID="lblmsg" runat="server" Text="" Font-Size="12px" ForeColor="Red" Font-Bold="false"></asp:Label>
            </td>
          </tr> 
          <tr>
            <td colspan="3">
                  <table width="100%" border="1px">
                       <tr>
                        <td style="width:16%">Vendor/BU</td>
                        <td style="width:34%">
                         <%-- <asp:TextBox ID="txtVendor" runat="server" Text="" Width="40px"></asp:TextBox>--%>
                          <asp:DropDownList ID="ddlVendor" runat="server" Font-Bold="True" Height="20px" Width="145px" ></asp:DropDownList>
                        </td>
                        <td style="width:15%">Vendor Activity ID</td>
                        <td style="width:35%"><asp:TextBox ID="txtActivityId" runat="server" Text="" Width="290px"></asp:TextBox></td>
                       </tr>
                       <tr>
                        <td style="width:16%">RDD Quater</td>
                        <td style="width:34%"><asp:TextBox ID="txtQuarter" runat="server" Text="" Width="90px"></asp:TextBox>
                        &nbsp;Vendor Quater&nbsp;<asp:TextBox ID="txtVendorQuarter" runat="server" Text="" Width="90px"></asp:TextBox>
                        </td>
                        <td style="width:15%">Plan Year</td>
                        <td style="width:35%">
                          <%--<asp:TextBox ID="txtyear" runat="server" Text="" Width="40px"></asp:TextBox>--%>
                          <asp:DropDownList ID="ddlyear" runat="server" Font-Bold="True" Height="20px" Width="89px">
                              <%--<asp:ListItem Selected="True">2014</asp:ListItem>
                              <asp:ListItem>2013</asp:ListItem>
                              <asp:ListItem>2012</asp:ListItem>
                              <asp:ListItem>2011</asp:ListItem>
                              <asp:ListItem>2010</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>

                       </tr>
                       <tr>
                        <td style="width:16%">Vendor Approved $</td>
                        <td style="width:34%">
                                  <asp:TextBox ID="txtApprovedAmt" runat="server" Text="" Width="75px" BackColor="#99cc00"></asp:TextBox>
                            &nbsp;Plan Actual Cost $
                            &nbsp;<asp:TextBox ID="txtActualCost" runat="server" Text="" Width="75px"  BackColor="#ffcc99"></asp:TextBox>
                        </td>
                        <td style="width:15%">Vendor  Approval Date</td>
                        <td style="width:35%"><asp:TextBox ID="txtApprovedDate" runat="server" Text="" Width="95px"></asp:TextBox>
                        &nbsp;DeadLine Date&nbsp;<asp:TextBox ID="txtDeadLineDate" runat="server" Text="" Width="95px" BackColor="#ffb7b7"></asp:TextBox>&nbsp;(mm-dd-yyyy)
                        </td>
                       </tr>
                       <tr>
                        <td style="width:16%">Upload Actual Plan</td>
                        <td colspan="3">
                         <asp:FileUpload ID="FileUpload1" runat="server" />
                         &nbsp;<asp:Label ID="lblFile" runat="server" Text=""></asp:Label>
                        </td>
                       </tr>
                       <tr>
                        <td style="width:16%">Upload Vendor Plan</td>
                        <td colspan="3">
                          <asp:FileUpload ID="FileUpload2" runat="server" />
                          &nbsp;<asp:Label ID="lblFileVendor" runat="server" Text=""></asp:Label><br />
                        </td>
                       </tr>
                       <tr>
                        <td style="width:16%">Plan Description</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDesc" runat="server" Text="" Width="742px" Height="60px" ></asp:TextBox></td>
                       </tr>
                       <tr>
                        <td style="width:16%">Status</td>
                        <td style="width:34%"><asp:Label ID="lblStatus" runat="server" Text="Plan creation"></asp:Label>&nbsp;
                        (<asp:Label ID="lblCurrStatusId" runat="server" Text="0" Font-Size="10px" ForeColor="Red" Font-Bold="false"></asp:Label>)</td>
                        <td style="width:15%">Last Modified</td>
                        <td style="width:35%"><asp:Label ID="lblLastModified" runat="server" Text="."></asp:Label></td>
                       </tr>
                       <tr>
                        <td style="width:16%">Comments</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtcomments" runat="server" Text="New Marketing Plan" Width="742px" Height="60px" ></asp:TextBox></td>
                       </tr>
                  </table>
            </td>
          </tr>
        </table>
    </asp:Panel>
    
</asp:Content>

