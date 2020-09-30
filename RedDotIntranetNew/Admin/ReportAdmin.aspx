<%@ Page Title="" Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="ReportAdmin.aspx.cs" Inherits="Admin_ReportAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <script type="text/javascript">
        function getConfirmation() 
        {
            return confirm('Are you sure you want to perfom this action ?');
        }
    </script>
    <style type="text/css">
        fieldset {border:1px solid red} /*this is the border color*/
        legend {color:#507CD1} /* this is the GroupingText color */
        legend {font-weight:bold}
        </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


        <table width="100%" style="border-color:#00A9F5;">
          <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
            <td>
                <div class="Page-Title">Assign/Revoke Permissions To Users</div>
            </td>
         </tr>
        </table>

        <table width="50%">
                 
            <tr style="height:53px">
                <%--<td style="width:5%">
                    &nbsp;
                </td>--%>
                <td>
                    <table>
                        <tr>
                            <td style=" width:10%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td style="width:12px">
                                            <asp:ImageButton ID="imgAddReport" runat="server" ImageUrl="~/admin/images/pluss.png" onclick="imgAddReport_Click"/>
                                        </td>
                                        <td style="background: url(/admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblAddReport" Text="Add Files" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Add Files</b></font>--%>
                                        </td>
                                    </tr>
                                </table>     
                            </td>

                            <td style=" width:10%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td style="width:12px">
                                        <asp:ImageButton ID="imgBU" runat="server" ImageUrl="~/admin/images/pluss.png" 
                                                onclick="imgBU_Click" />
                                        </td>
                                        <td style="background: url(/admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblBU" Text="Add BU's" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Add BU's</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td style=" width:10%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td style="width:12px">
                                            <asp:ImageButton ID="imgAssignFile" runat="server" 
                                                ImageUrl="~/admin/images/pluss.png" onclick="imgAssignFile_Click" />
                                        </td>
                                        <td style="background: url(/admin/images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblAssignFile" Text="Assign File Roles" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Assign File Roles</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td style="width:25%">
                    &nbsp;
                </td>--%>
            </tr>
        </table>

      
        <table width="90%">
            <tr>
                <td style="padding-left:25%">
                    <asp:Label ID="lblError" runat="server" ForeColor="red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
  
    <br />
    

                
        <table  width="99%" style="padding-left:1%">
            <tr>
                <td>

                    <!--************************************************************Level 1*********************************************************-->

                    <asp:Panel ID="pnlAddReport" runat="server" Visible="false" GroupingText="Add Files">
    
                        <table width="100%">

                            <tr style="height:30px">
                                <td style="width:25%">  
                                    &nbsp;
                                </td>
                                <td style="width:15%">
                                    <b>Select Report:</b>
                                </td>
                                <td style="width:30%">
                                    <asp:DropDownList ID="ddlReport" runat="server" Width="200px" BackColor="Silver" AutoPostBack="True" onselectedindexchanged="ddlReport_SelectedIndexChanged" ></asp:DropDownList>
                                    <asp:Label ID="lblReportCount" runat="server" Font-Bold="true" ForeColor="#006666"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>

                            <tr style="height:30px">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <b>Report Name:</b>
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtReport" runat="server" Width="300px" Enabled="False" ></asp:TextBox>
                                </td>
                            </tr>
                
                            <tr style="height:30px">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <b>Report Title:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReportTitle" runat="server" Width="300px" Enabled="False" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkReportAdd" runat="server" Text="New Report" onclick="lnkReportAdd_Click" ></asp:LinkButton>
                                </td>
                            </tr>

                            <tr style="height:30px">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <b>Report Location:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReportPath" runat="server" Width="300px" Enabled="False" ></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkReportEdit" runat="server" Text="Edit Report" onclick="lnkReportEdit_Click" ></asp:LinkButton>
                                </td>
                            </tr>

                            <tr style="height:40px">
                                <td colspan="2">
                                    &nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btnReportUpdate" runat="server" Text="Update"  Width="60px" Enabled="False" onclick="btnReportUpdate_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnReportdelete" runat="server" Text="Delete"  Width="60px" Enabled="False" onclick="btnReportdelete_Click" />&nbsp;&nbsp;
                                    <asp:Button ID="btnReportCancel" runat="server" Text="Cancel"  Width="60px" Enabled="False" onclick="btnReportCancel_Click" />
                                </td>
                            </tr>

                        </table>

                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td>
                    <!--************************************************************Level 2*********************************************************-->

                    <asp:Panel ID="pnlAddBU" runat="server" Visible="false" GroupingText="Add BU">
        
                        <table width="100%">

                                <tr style="height:30px">
                                    <td style="width:25%">  
                                        &nbsp;
                                    </td>
                                    <td style="width:15%">
                                        <b>Select Report:</b>
                                    </td>
                                    <td style="width:30%" colspan="2">
                                        <asp:DropDownList ID="ddlBUReport" runat="server" Width="200px" BackColor="Silver" AutoPostBack="True" onselectedindexchanged="ddlBUReport_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label ID="lblBUReportCount" runat="server" Font-Bold="true" ForeColor="#006666"></asp:Label>
                                    </td>
                                </tr>

                                <tr style="height:30px">
                                    <td style="width:25%">  
                                        &nbsp;
                                    </td>
                                    <td style="width:15%">
                                        <b>Available BU List:</b>
                                    </td>
                                    <td style="width:30%" colspan="2">
                                        <asp:DropDownList ID="ddlBUName" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="ddlBUName_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label ID="lblBUNameCount" runat="server" Font-Bold="true" ForeColor="#006666"></asp:Label>
                                    </td>
                                </tr>

                                <tr style="height:30px">
                                    <td style="width:25%">  
                                        &nbsp;
                                    </td>
                                    <td style="width:15%">
                                        <b>BU:</b>
                                    </td>
                                    <td style="width:30%" colspan="2">
                                        <asp:TextBox ID="txtBUName" runat="server" Width="300px" Enabled="False" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkBUAdd" runat="server" Text="New BU" onclick="lnkBUAdd_Click"></asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="height:30px">
                                    <td style="width:25%">  
                                        &nbsp;
                                    </td>
                                    <td style="width:15%">
                                        <b>File Name:</b>
                                    </td>
                                    <td style="width:30%" colspan="2">
                                        <asp:TextBox ID="txtBUFileName" runat="server" Width="300px" Enabled="False" ></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkBUEdit" runat="server" Text="Edit BU" onclick="lnkBUEdit_Click"></asp:LinkButton>
                                    </td>
                                </tr>

                                <tr style="height:40px">
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnBUUpdate" runat="server" Text="Update"  Width="60px" Enabled="False" onclick="btnBUUpdate_Click"  />&nbsp;&nbsp;
                                        <asp:Button ID="btnBUDelete" runat="server" Text="Delete"  Width="60px" Enabled="False" onclick="btnBUDelete_Click"  />&nbsp;&nbsp;
                                        <asp:Button ID="btnBUCancel" runat="server" Text="Cancel"  Width="60px" Enabled="False" onclick="btnBUCancel_Click"  />
                                    </td>
                                </tr>

                            </table>

                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td>
                    <!--************************************************************Level 3*********************************************************-->

                    <asp:Panel ID="pnlAddRole" runat="server" Visible="false" GroupingText="Add Role">

                        <table width="100%">
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
                                                <b>Available Files</b>
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
                                                            <asp:ImageButton ID="imgbtnAssign" runat="server" ImageUrl="~/admin/images/right-arrow.png" Heigh="25px" Width="50px" ToolTip="Add Selected File To Permitted List" onclick="imgbtnAssign_Click"/>
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
                                                <b><font color="#750026"><asp:LinkButton ID="lnkAddUser" runat="server" Text="New User" onclick="lnkAddUser_Click"></asp:LinkButton></font></b>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>

                                        <tr style="height:30px">
                                            <td>
                                                <b>Permitted List</b>
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
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/admin/images/Delete.png" ToolTip="Remove Selected File From Permitted List" onclick="imgbtnDelete_Click" Height="25px" Width="25px" />
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
                </td>
            </tr>

            <tr>
                <td>
                    <!--************************************************************Level 4*********************************************************-->

                    <asp:Panel runat="server" id="pnlAddUser" GroupingText="New User" Visible="false" Font-Bold="true" BorderColor="Red">
                        <table width="100%">
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
                                            <td style="width:75%">
                                
                                                    <table width="100%">
                                                        <tr  style="height:50px">
                                                            <td colspan="2" align="center">
                                                                <table style="width:100%">
                                                                    <tr>
                                                                        <td style="width:70%" align="left" valign="top">
                                                                            <h3>Create Users (Mail will be sent to User)</h3>
                                                                        </td>
                                                                        <td style="width:30%" align="right" valign="top">
                                                                            <asp:ImageButton ID="imgBtnClose" runat="server" ImageUrl="~/admin/images/close-icon.png" ToolTip="Close This Window..." onclick="imgBtnClose_Click" />
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
                               
                                            </td>
                                            <td style="width:25%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>

        </table>
    
    <br />
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>

