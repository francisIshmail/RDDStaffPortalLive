<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="sapAddOnceProjects.aspx.vb" Inherits="Intranet_sapBase_sapAddOnceProjects" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    
    <style type="text/css">
        .style1
        {
            width: 40%;
            height: 30px;
        }
        .style2
        {
            width: 60%;
            height: 30px;
        }
        .style3
        {
            height: 22px;
        }
    </style>
    
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
        <ContentTemplate>
     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="SAP - Addonce Project"></asp:Label></div>
                </td>
              </tr>
            </table>   
    <table width="100%" style="background-color:White">
        <tr style="height:25px">
             <td width="25%" align="left">
               <asp:Button ID="cmdConnectDb" runat="server" 
                    Text="Click! Connect To SAP" width="90%" BackColor="#00CC99" 
                    BorderColor="#FF9966" Font-Bold="True"/>
            </td> 
            <td width="30%" align="center">
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Text="Not Connected" ForeColor="#993399"></asp:Label>
            </td>
             <td width="55%" align="left">
                <asp:CheckBox ID="chkAE" runat="server" Text="AE" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkUG" runat="server" Text="UG" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkTZ" runat="server" Text="TZ" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />&nbsp;
                <asp:CheckBox ID="chkKE" runat="server" Text="KE" Checked="false" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#FF3300" Enabled="false" />
           </td>
        </tr>
        <tr> 
            <td colspan="4" align="left" style="height:25px">
               <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
    </table>

 <asp:Panel ID="Panel1" runat="server" >

    <table width="100%" style="background-color:White">
        <tr>
            
            
            <td valign="top" style="width:40%">

                <asp:Panel ID="pnlItem" runat="server" GroupingText="Item" CssClass="addOncePanel" Enabled="False">
                       <table style="width:100%;">
                            <tr>
                                <td>
                                    Existing Project (<asp:Label ID="lblLstCnt" runat="server" Text="0" Font-Bold="True" ForeColor="#990099"></asp:Label>)</td>
                                <td>
                                    <asp:DropDownList ID="ddl1" runat="server" Width="150px" BackColor="#FFCC66">
                                    </asp:DropDownList>     
                                </td>
                            </tr>
                            <tr>
                                <td style="width:30%">
                                    Project Code</td>
                                <td style="width:70%">
                                    <asp:TextBox ID="txtSimpleCode" runat="server" Width="50%" MaxLength="20"></asp:TextBox>
                                    &nbsp;Max 20 Chrs</td>
                            </tr>
                            
                            <tr>
                                <td>
                                    Project&nbsp; Name </td>
                                <td>
                                    <asp:TextBox ID="txtDesc1" runat="server" Width="60%" MaxLength="50"></asp:TextBox>
                                    &nbsp;Max 50 Chrs</td>
                            </tr>
                        </table>
                </asp:Panel>    
            </td>

            <td valign="top" style="width:30%">
                &nbsp;</td>
            
            
            <td valign="top" style="width:30%">

                &nbsp;</td>
        </tr>

        <tr>
            <td colspan="3" class="style3">
                </td>
        </tr>

        <tr>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                &nbsp;</td>
            <td valign="top">
                
                &nbsp;</td>
        </tr>
   
        <tr>
            <td style="width:100%" colspan="3">
                &nbsp;</td>

        </tr>
                 
            <tr>
                <td valign="top" colspan="3">
                    <asp:Panel ID="pnlBtn1" runat="server" CssClass="addOncePanel" GroupingText="Insert Values">
                        <table width="100%">
                            <tr>
                                <td align="left" class="style1" valign="top">
                                    <asp:CheckBox ID="chkAEdo" runat="server" Text="AE" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkUGdo" runat="server" Text="UG" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkTZdo" runat="server" Text="TZ" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />&nbsp;
                                    <asp:CheckBox ID="chkKEdo" runat="server" Text="KE" Checked="false" 
                                        Font-Bold="True" Font-Size="10pt" ForeColor="#009900" Enabled="true" />
                                    &nbsp;
                                     <asp:Button ID="cmdAdd" runat="server" Text="Add Item to selected DBs" width="200px" 
                                        Enabled="False" BackColor="#00CC99" BorderColor="#00CC99" Font-Bold="True" 
                                        ForeColor="Black" ToolTip="Please Check/Uncheck the Databases on left you want to add to.." />
                                  </td>
                                <td align="center" class="style2">
                                        <asp:Label ID="lblInfo" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                            
            </tr>
        </table>
    </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <%--<asp:PostBackTrigger ControlID = "cmdConnectDb" /> --%>
    </Triggers>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:50%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImgWait.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>
</asp:Content>

