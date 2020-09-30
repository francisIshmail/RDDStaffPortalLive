<%@ Page Title="" Language="VB" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="false" CodeFile="sapItemUpdationAndAddition.aspx.vb" Inherits="Intranet_sapBase_sapItemUpdationAndAddition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                    <div class="Page-Title"><asp:Label ID="lblVersionNo" runat="server" Text="SAP - Update Or ADD Stock Items"></asp:Label>
                        &nbsp;Ver 13-Feb-2017</div>
                </td>
              </tr>
            </table>   
    <table width="100%" style="background-color:White">
      <tr style="height:25px">
             <td width="25%" align="left">
               Target Database Name (Ex. SAPAE) :
            </td> 
            <td width="30%" align="center">
               <asp:TextBox ID="txtTargetDb" Text="SAPAE-TEST" runat="server" 
                    BorderColor="#FFCC99"></asp:TextBox>
            </td>
         </tr>

        <tr style="height:25px">
             <td width="25%" align="left">
                
               <asp:Button ID="cmdConnectDb" runat="server" 
                    Text="Click! Connect To SAP &amp; Update Item List from Source Tbl" 
                     width="86%" BackColor="#00CC99" 
                    BorderColor="#FF9966" Font-Bold="True" 
                     ToolTip="Item List should be stored in table in TejSap Db"/>
            </td> 
            <td width="30%" align="center">
                <asp:CheckBox ID="CheckUpdateDesc" runat="server" Text="Update Part Descritions ?" checked="True" />&nbsp;&nbsp;
                <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Text="Not Connected" ForeColor="#993399"></asp:Label>
            </td>
         </tr>
        <tr> 
            <td colspan="4" align="left" style="height:25px">
               <asp:Label ID="lblError" Text="" runat="server" ForeColor="Red" ></asp:Label>
            </td>
        </tr>
        <tr> 
            <td colspan="4" align="left" valign="top" style="height:25px">
                Items N/F So Added&nbsp;(<asp:Label ID="lblNotUpdatedCnt" runat="server" Font-Bold="True" Text="0" ForeColor="Red"></asp:Label>)
              &nbsp;
              <asp:TextBox ID="txtItemsNotUpdated" runat="server" 
                    ForeColor="Red" Text="" TextMode="MultiLine" Height="91px" Width="769px"></asp:TextBox>
            </td>
        </tr>
        <tr> 
            <td colspan="4" align="left" valign="top" style="height:25px">
                Items Updated&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(<asp:Label ID="lblUpdatedCnt" runat="server" Font-Bold="True" Text="0" ForeColor="Red"></asp:Label>)
                &nbsp;
                <asp:TextBox ID="txtItemsUpdated" runat="server" 
                    ForeColor="Green" Text="" TextMode="MultiLine" Height="171px" Width="767px"></asp:TextBox>
            </td>
        </tr>
    </table>

 <asp:Panel ID="Panel1" runat="server" >
    <table width="100%" style="background-color:White">
        <tr>
            <td style="Width:30%">
            &nbsp;
            </td>
            <td style="Width:50%">
            &nbsp;
            </td>
            <td style="Width:20%">
            &nbsp;
            </td>
        </tr>
     </table>
    </asp:Panel>
    
    </ContentTemplate>
        
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:35%;left:50%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
     </asp:UpdateProgress>

</asp:Content>

