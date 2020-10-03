<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="CardCodeReport.aspx.cs" Inherits="IntranetNew_DailySalesReport_CardCodeReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        
        <tr>
            <td width="2%">
            </td>
            <td width="2%">
            </td>
        </tr>
        <tr>
            <td colspan=2 >
                <label>
                    Select Date(YYYY-MM-dd) &nbsp;
                </label>
            </td>
            <td>
                <asp:TextBox ID="txtstartdate" runat="server" autocomplete="off" class="form-control"
                    TabIndex="8" Style="width: 15%" OnTextChanged="txtstartdate_TextChanged" AutoPostBack="true"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server"
                    TargetControlID="txtstartdate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
            </td>
           <%-- <td width="5%">
                PLEASE SELECT MONTH
            </td>
            <td width="5%">
                <asp:DropDownList ID="ddlmonth" runat="server">
                    <asp:ListItem>--SELECT--</asp:ListItem>
                    <asp:ListItem>JANUARY</asp:ListItem>
                    <asp:ListItem>FEBRUARY</asp:ListItem>
                    <asp:ListItem>MARCH</asp:ListItem>
                    <asp:ListItem>APRIL</asp:ListItem>
                    <asp:ListItem>MAY</asp:ListItem>
                    <asp:ListItem>JUNE</asp:ListItem>
                    <asp:ListItem>JULY</asp:ListItem>
                    <asp:ListItem>AUGUST</asp:ListItem>
                    <asp:ListItem>SEPTEMBER</asp:ListItem>
                    <asp:ListItem>OCTOBER</asp:ListItem>
                    <asp:ListItem>NOVEMBER</asp:ListItem>
                    <asp:ListItem>DECEMBER</asp:ListItem>
                </asp:DropDownList>
            </td>--%>
        </tr>
        </table>


    
        <table style="width: 30%; text-align: center; background-color: skyblue;">  
            <tr>  
                <td align="center">  
                    <asp:PlaceHolder ID="DBDataPlaceHolder" runat="server"></asp:PlaceHolder>  
                </td>  
            </tr>  
                
         </table>
     

      
</asp:Content>
