<%@ Page Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="default.aspx.cs"
    Inherits="_default" Title="Welcome to Red Dot" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width:100%">
        <tr>
            <td align="center">      
                <ul id="slider">
                    <li> <img src="images/banner/Home1.jpg" alt="" /></li>
                    <li> <img src="images/banner/Home2.jpg" alt="" /></li>
                    <li> <img src="images/banner/Home3.jpg" alt="" /></li>
                    <li> <img src="images/banner/Home4.jpg" alt="" /></li>
                </ul>
            </td>
        </tr> 
        <tr>
            <td align="center"> <a href="/Intranet/Home.aspx"><h3>Welcome to RedDotDistribution Intranet Services</h3></a></td>
        </tr>
    </table>
</asp:Content>
