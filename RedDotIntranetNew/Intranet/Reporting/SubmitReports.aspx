<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="SubmitReports.aspx.cs" Inherits="Intranet_Reporting_SubmitReports" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <script language="javascript" type="text/javascript">
    
    function showMsgWait(pMsg) 
    {
        var ct = document.getElementById('<%= lblMsg.ClientID %>')
        ct.innerHTML = pMsg;
    }
  
    </script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
    <div class="main-content-area" style="background-image: url('../images/bgimg.png');">
        <center>
            <p class="title-txt" >
                Submit Your  Reports</p>
            <br />
            <br />
             <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red" Font-Bold="true"></asp:Label>
            <br />
            <br />

            <table width="80%" id="tblMain" runat="server" border="0">
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                               
                <tr>
                    <td style="width: 15%" align="left"><b>Report Name</b></td>
                    <td style="width: 20%" align="left"><asp:TextBox ID="txtRepName" runat="server" Width="220px" Font-Bold="true"></asp:TextBox></td>
                    <td style="width: 15%" align="left"><b>For Date</b>&nbsp;(MM-DD-YYYY)</td>
                    <td style="width: 20%" align="left">
                            <asp:TextBox ID="txtDate" runat="server" Width="120px" Font-Bold="true"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                Enabled="True" TargetControlID="txtDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                TodaysDateFormat="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                     </td>
                      <td style="width: 30%">
                         &nbsp;
                      </td>
                </tr>

                <tr>
                    <td align="left">
                        <b>Upload Report File</b>
                    </td>
                    <td colspan="4" align="left">
                     <asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;supports Excel/PDF/Word
                    </td>
                </tr>

                <tr>
                     <td align="left">
                        <b>Comments :</b>
                    </td>
                    <td colspan="3" align="left">
                       <asp:TextBox ID="txtComments" runat="server" Width="783px" Font-Bold="true" 
                            MaxLength="2000" TextMode="MultiLine" Height="68px"></asp:TextBox>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                 <tr>
                     <td colspan="5" align="center">
                         <asp:Button ID="btnReport" runat="server" Text="Upload Report!" 
                            Font-Bold="True" Width="130px" onclick="btnReport_Click" OnClientClick="showMsgWait('Wait! ........  while it is processing . Do not press back button or refresh page untill it finishes');" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                         &nbsp;
                    </td>
                </tr>
               
            </table>
        </center>
    </div>
</asp:Content>


