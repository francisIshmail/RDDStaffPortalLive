<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="ReportAdmin.aspx.cs" Inherits="IntranetNew_Admin_ReportAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager runat="server" ID="scriptManager1" ></asp:ScriptManager>
 <script type="text/javascript">
     function getConfirmation() {
         return confirm('Are you sure you want to perfom this action ?');
     }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <br />    
        <table width="90%">
            
            <tr>
                <td align="center">
                   <lable style="color: #d71313; font-size: x-large; font-weight: bold; font-family: Raleway;"> &nbsp;&nbsp;&nbsp; Reports Authorization  </lable>
                </td>
            </tr>
            <tr>
             <td>  &nbsp; </td>
            </tr>
            <tr>
                <td style="padding-left:5%">
                    <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    <br />

    <asp:Panel ID="pnlAddRole" runat="server">

        <table width="90%">
            <tr>
                <td style="width:50%">
                    <table width="100%">
                            
                        <tr>
                            <td style="width:50%">
                                &nbsp;
                            </td>
                            <td>
                                <b>Report Type</b>
                            </td>
                        </tr>
                            
                        <tr style="height:40px">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlReportType" runat="server" Width="200px" BackColor="Silver" AutoPostBack="True" onselectedindexchanged="ddlReportType_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr style="height:30px">
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <b>Available Files</b>&nbsp;
                                <asp:Label ID="lblFileCount" runat="server" Font-Bold="true" ForeColor="#006666"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width:220px">
                                            <asp:ListBox ID="lstFiles" runat="server" Height="100px" Width="200px" BackColor="#ddffdd" SelectionMode="Multiple" ></asp:ListBox>
                                        </td>
                                        <td valign="middle">
                                            <asp:ImageButton ID="imgbtnAssign" runat="server" ImageUrl="~/Intranet/images/right-arrow.png" Heigh="25px" Width="50px" ToolTip="Add Selected File To Permitted List" onclick="imgbtnAssign_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>

                <td>
                    <table width="100%">

                        <tr>
                            <td style="width:55%">
                                <b>Select User</b>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>

                        <tr style="height:40px">
                            <td>
                                <asp:DropDownList ID="ddlUsers" runat="server" Width="200px" BackColor="Silver" AutoPostBack="True" onselectedindexchanged="ddlUsers_SelectedIndexChanged"></asp:DropDownList>&nbsp;
                                
                            </td>
                            <td>
                               <b><font color="#750026"><asp:LinkButton ID="lnkAddUser" runat="server" Text="New User" onclick="lnkAddUser_Click"></asp:LinkButton></font></b>
                            </td>
                        </tr>

                        <tr style="height:30px">
                            <td>
                                <b>Permitted List</b>&nbsp;
                                <asp:Label ID="lblUserFileCount" runat="server" Font-Bold="true" ForeColor="#006666"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>

                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width:220px">
                                            <asp:ListBox ID="lstUserFiles" runat="server" Height="100px" Width="200px" BackColor="#ddffdd" SelectionMode="Multiple" ></asp:ListBox>
                                        </td>
                                        <td valign="top">
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Intranet/images/Delete.png" ToolTip="Remove Selected File From Permitted List" onclick="imgbtnDelete_Click" Height="25px" Width="25px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
    </asp:Panel>

        <table width="90%">
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td style="width:25%">
                                &nbsp;
                            </td>
                            <td style="width:50%">
                                <asp:Panel runat="server" id="pnlAddUser" GroupingText="New User" Visible="false" BackColor="#ffe6e6">
                                    <table width="100%">
                                        <tr  style="height:50px">
                                            <td colspan="2" align="center">
                                                <table style="width:100%">
                                                    <tr>
                                                        <td style="width:70%" align="left" valign="top">
                                                            <h3>Create Users (Mail will be sent to User)</h3>
                                                        </td>
                                                        <td style="width:30%" align="right">
                                                            <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/Intranet/images/close-icon.png" ToolTip="Close This Window..." onclick="imgBtnClose_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                           
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="#990099" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr  style="height:50px">
                                            <td style="width:20%">
                                                Enter User Name
                                            </td>
                                            <td style="width:80%">
                                                <asp:TextBox ID="txtUser" runat="server"  Width="225px" BackColor="Silver"></asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width:20%">
                                                Enter User Email
                                            </td>
                                            <td style="width:80%">
                                                <asp:TextBox ID="txtEmail" runat="server"  Width="225px" BackColor="Silver"></asp:TextBox>
                                                &nbsp;<asp:Button ID="BtnUser" Width="120px" runat="server" Text="Create User" onclick="BtnUser_Click" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                            <td style="width:25%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

            </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

