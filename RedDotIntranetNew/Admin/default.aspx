<%@ Page Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true"
    CodeFile="default.aspx.cs" Inherits="admin_default" Title="Welcome to Red Dot Admin" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <%-- <link href="css/adminGrid.css" rel="stylesheet" type="text/css" />--%>

    <table cellpadding="8" cellspacing="0" style="width: 100%;padding-right:15px" >
        <tr>
            <td align="left"  valign="top" style="width:70%">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <table cellpadding="2" cellspacing="0" width="100%" class="title-heading">
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="content" height="10">
                            &nbsp;
                        </td>
                    </tr>
                    <%--New additions Vishav starts--%>
                    <tr>
                        <td>
                            <table width="99%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="15px">
                                        &nbsp;
                                    </td>
                                    <td colspan="6" height="50px">
                                                <p class="title-txt">Website Administration</p>
                                    </td>
                                    <td width="15px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td width="15px">
                                        &nbsp;
                                    </td>
                                    <td width="35">
                                        &nbsp;
                                    </td>
                                    <td width="160">
                                        &nbsp;
                                    </td>
                                    <td width="35">
                                        &nbsp;
                                    </td>
                                    <td width="160">
                                        &nbsp;
                                    </td>
                                    <td width="35">
                                        &nbsp;
                                    </td>
                                    <td width="160">
                                        &nbsp;
                                    </td>
                                    <td width="15px">
                                        &nbsp;
                                    </td>
                                </tr>                  
                               
                                <tr  style="height:100px">
                                    <td width="15px">
                                        &nbsp;
                                    </td>
                                    <td width="35">
                                        <span><em>
                                            <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="~/admin/icons/add-new-user.jpg"
                                                NavigateUrl="~/Admin/userCreationMembershipDatabase.aspx">HyperLink</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="160">
                                        &nbsp; <span><em>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Font-Bold="False" Font-Names="Verdana"
                                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" NavigateUrl="~/Admin/userCreationMembershipDatabase.aspx">Manage Users & Roles</asp:HyperLink>
                                        </em></span>
                                    </td>  
                                    <td width="35">
                                        <span><em>
                                            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl="~/admin/icons/Escalation.jpeg"
                                                NavigateUrl="~/Admin/EscalationConfig.aspx">HyperLink</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="160">
                                        &nbsp; <span><em>
                                            <asp:HyperLink ID="HyperLink4" runat="server" Font-Bold="False" Font-Names="Verdana"
                                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" NavigateUrl="~/Admin/EscalationConfig.aspx">Escalation Configration</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="35">
                                        <span><em>
                                            <asp:HyperLink ID="HyperLink5" runat="server" ImageUrl="~/admin/images/sysConfig.bmp"
                                                NavigateUrl="~/Admin/WorkFlowControlPannel.aspx">HyperLink</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="160">
                                        &nbsp; <span><em>
                                            <asp:HyperLink ID="HyperLink6" runat="server" Font-Bold="False" Font-Names="Verdana"
                                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" NavigateUrl="~/Admin/WorkFlowControlPannel.aspx">WorkFlow Control Pannel</asp:HyperLink>
                                        </em></span>
                                    </td>
                                     <td width="35">
                                        <span><em>
                                            <asp:HyperLink ID="HyperLink7" runat="server" ImageUrl="~/admin/images/sysConfig.bmp"
                                                NavigateUrl="~/Admin/ReportAdmin.aspx">HyperLink</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="160">
                                        &nbsp; <span><em>
                                            <asp:HyperLink ID="HyperLink8" runat="server" Font-Bold="False" Font-Names="Verdana"
                                                Font-Size="8pt" Font-Underline="False" ForeColor="Black" NavigateUrl="~/Admin/ReportAdmin.aspx">Manage Reports</asp:HyperLink>
                                        </em></span>
                                    </td>
                                    <td width="15px">
                                        &nbsp;
                                    </td>                    
                                </tr>
                            </table>
                        </td>
                   </tr>
                </table>                   
           </td>
                <td valign="top" style="width:30%">&nbsp;   
                            <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="orange-tab">
                                        Dashboard
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left-table">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="inner-table2">
                                            <tr>
                                                <td>
                                                    <table align="center" cellpadding="5" cellspacing="0" width="100%" style="border: 1px solid #D0D0BF">
                                                        <tr>
                                                            <td style="border-bottom-style: solid; border-bottom-color: #D0D0BF; border-bottom-width: 1px;
                                                                font-family: Verdana; font-style: normal; text-transform: none; text-decoration: none;
                                                                font-weight: normal; font-size: 9pt;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="150" align="left">
                                                                            Quote Requests
                                                                        </td>
                                                                        <td>
                                                                            :
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Literal ID="NewQuotesLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-bottom-style: solid; border-bottom-color: #D0D0BF; border-bottom-width: 1px;
                                                                font-family: Verdana; font-style: normal; text-transform: none; text-decoration: none;
                                                                font-weight: normal; font-size: 9pt;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="150" align="left">
                                                                            Registration Requests
                                                                        </td>
                                                                        <td>
                                                                            :
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Literal ID="newRegistrationsLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-bottom-style: solid; border-bottom-color: #D0D0BF; border-bottom-width: 1px;
                                                                font-family: Verdana; font-style: normal; text-transform: none; text-decoration: none;
                                                                font-weight: normal; font-size: 9pt;">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td width="150" align="left">
                                                                            Registered Users
                                                                        </td>
                                                                        <td>
                                                                            :
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Literal ID="RegisteredUserLiteral" runat="server"></asp:Literal>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="orange-tab">
                                        Visitor Summary
                                    </td>
                                </tr>
                                <tr>
                                    <td class="left-table">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="inner-table2">
                                            <tr>
                                                <td>
                                                    <table align="center" cellpadding="7" cellspacing="0" width="98%">
                                                        <tr>
                                                            <td>
                                                                Total Logins To Date :
                                                                <asp:Label ID="TotalVisitorsLabel" runat="server">0</asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table cellpadding="5" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td align="left" width="80">
                                                                            From Date
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtStartDate" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                                                Width="101px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate"
                                                                                PopupButtonID="Image1" Format="dd-MMM-yyyy">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:ImageButton runat="Server" ID="Image1" ImageUrl="~/admin/adminImages/Calendar_scheduleHS.png"
                                                                                AlternateText="Click to show calendar" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="60" align="left">
                                                                            To&nbsp; Date
                                                                        </td>
                                                                        <td width="100">
                                                                            <asp:TextBox ID="txtToDate" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="102px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" Format="dd-MMM-yyyy"
                                                                                PopupButtonID="Image2" runat="server">
                                                                            </cc1:CalendarExtender>
                                                                        </td>
                                                                        <td width="50" align="left">
                                                                            <asp:ImageButton ID="Image2" runat="Server" AlternateText="Click to show calendar"
                                                                                ImageUrl="~/admin/adminImages/Calendar_scheduleHS.png" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" height="25" align="center">
                                                                            <asp:Button ID="SearchVisitorsButton" runat="server" Text="Search" OnClick="SearchVisitorsButton_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>
                        </td>
                 </tr>
        </table>
    <br />
</asp:Content>
