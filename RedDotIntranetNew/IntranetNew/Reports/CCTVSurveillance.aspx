<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="CCTVSurveillance.aspx.cs" Inherits="IntranetNew_Reports_CCTVSurveillance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Red dot CCTV  </Lable>
    </td>
</tr>

<tr>
    <td width="100%" align="center">&nbsp;</td>
</tr>


<tr>
    <td width="100%" align="center">
        
        <asp:Panel ID="pnl" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

            <table  width="50%" class="table table-stripped table-bordered" align="center" >
                <tr class="info">
                    <td width="40%" align="center" > <span style="font-size:large;" > <b> COUNTRY </b> </span> </td>
                    <td width="30%" align="center" > <span style="font-size:large;" > <b> UserName </b> </span></td>
                    <td width="30%" align="center" > <span style="font-size:large;" > <b> Password </b> </span></td>
                </tr>
                <tr>
                    <td width="40%" align="center" > <a href="http://41.60.252.140:99/doc/page/login.asp?_1568026594217&page=preview" target="_blank"> <span style="font-size:large" > <b>Tanzania</b> </span> </a> </td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Blue"  >admin</span></td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Red" >8Reddot1</span></td>
                </tr>
                <tr>
                    <td width="40%" align="center" > <a href="http://41.191.229.130:99/doc/page/login.asp?_1568026827926&page=preview" target="_blank"> <span style="font-size:large" > <b>Kenya</b> </span> </a> </td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Blue"  >admin</span></td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Red" >8Reddot1</span></td>
                </tr>
                 <tr>
                    <td width="40%" align="center" > <a href="http://41.190.133.214:99/doc/page/preview.asp" target="_blank"> <span style="font-size:large" > <b>Uganda</b> </span> </a> </td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Blue"  >admin</span></td>
                    <td width="30%" align="center" ><span style="font-size:large;color:Red" >8Reddot1</span></td>
                </tr>
            </table>

        </asp:Panel>
    </td>
</tr>


</table>


</asp:Content>

