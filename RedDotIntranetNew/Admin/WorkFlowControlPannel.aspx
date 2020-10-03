<%@ Page Title="" Language="C#" MasterPageFile="~/reddotIntranet.master" AutoEventWireup="true" CodeFile="WorkFlowControlPannel.aspx.cs" Inherits="Admin_WorkFlowControlPannel" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
    
    <style type="text/css">
        fieldset {border:1px solid red} /*this is the border color*/
        legend {color:#507CD1} /* this is the GroupingText color */
        legend {font-weight:bold}

        div#container 
        {
          width: 800px;
          vertical-align:top;
          height:20px;
        }

        div#pop-up 
        {
          display: none;
          position: absolute;
          width: 550px;
          padding: 10px;
          background: #eeeeee;
          color: #000000;
          border: 1px solid #1a1a1a;
          font-size: 90%;
          border-radius: 15px;
        }
        
        div#pop-up1
        {
          display: none;
          position: absolute;
          width: 550px;
          padding: 10px;
          background: #eeeeee;
          color: #000000;
          border: 1px solid #1a1a1a;
          font-size: 90%;
          border-radius: 15px;
        }
        
        div#pop-up2
        {
          display: none;
          position: absolute;
          width: 550px;
          padding: 10px;
          background: #eeeeee;
          color: #000000;
          border: 1px solid #1a1a1a;
          font-size: 90%;
          border-radius: 15px;
        }    
    </style>

    <script type="text/javascript" src="../js/jquery-1.3.1.min.js" ></script>
    <script type="text/javascript">

        $(function () {
            var moveLeft = 20;
            var moveDown = 10;

            $('a#trigger').hover(function (e) {
                $('div#pop-up').show();
            }, function () {
                $('div#pop-up').hide();
            });
            $('a#trigger').mousemove(function (e) {
                $("div#pop-up").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });



            $('a#trigger1').hover(function (e) {
                $('div#pop-up1').show();
            }, function () {
                $('div#pop-up1').hide();
            });
            $('a#trigger1').mousemove(function (e) {
                $("div#pop-up1").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });



            $('a#trigger2').hover(function (e) {
                $('div#pop-up2').show();
            }, function () {
                $('div#pop-up2').hide();
            });
            $('a#trigger2').mousemove(function (e) {
                $("div#pop-up2").css('top', e.pageY + moveDown).css('left', e.pageX + moveLeft);
            });
        });

</script>

    <div id="main" style="padding: 5px 5px 0px 5px">
        <table width="100%">
         <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
            <td>
                <div class="Page-Title">Work flow Automation</div>
            </td>
         </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red" Visible="true"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    <table width="55%">
                        
                        <tr style="height:53px">
                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgDepartment" runat="server" ImageUrl="images/pluss.png" onclick="imgDepartment_Click" />
                                        </td>
                                        <td style="background: url(images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblDepartment" Text="Manage Departments" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage Departments</b></font>--%>
                                        </td>
                                    </tr>
                                </table>     
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                        <asp:ImageButton ID="imgRoles" runat="server" ImageUrl="images/pluss.png" onclick="imgRoles_Click" />
                                            
                                        </td>
                                        <td style="background: url(images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblRoles" Text="Manage Roles" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage Roles</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgEsclation" runat="server" ImageUrl="images/pluss.png"  onclick="imgEsclation_Click" />
                                        </td>
                                        <td style="background: url(images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblEsclation" Text="Manage Escalation" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage Escalation</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>

                            <td style=" width:15%">
                                <table width="100%">
                                    <tr style="height:53px">
                                        <td>
                                            <asp:ImageButton ID="imgProcesses" runat="server" ImageUrl="images/pluss.png" onclick="imgProcesses_Click" />                   
                                        </td>
                                        <td style="background: url(images/buttonBack.png) no-repeat 0 0;">
                                            &nbsp;<asp:Label runat="server" ID="lblProcesses" Text="Manage Processes" Font-Bold="true" ForeColor="White" ></asp:Label>
                                            <%--&nbsp; <font color="White"><b>Manage Processes</b></font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                  
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Panel ID="pnlDepartments" runat="server" GroupingText="Departments" Visible="False" EnableTheming="True" Font-Bold="False" >

                        <table width="100%">

                            <tr style="height:30px">
                                <td style="width:20%">
                                    &nbsp;
                                </td>
                                <td style="width:10%">
                                    <b>Select Department:</b>
                                </td>
                                <td style="width:20%">
                                    <asp:DropDownList ID="ddldeptName" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="ddldeptName_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btnNew" runat="server" Text="Create New" onclick="btnNew_Click" Width="90px" />--%>
                                    <asp:LinkButton ID="btnNew" runat="server" Text="Create department" onclick="btnNew_Click" ></asp:LinkButton>
                                </td>
                            </tr>
            
            
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <b>Department Name:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDeptName" runat="server" Width="200px" Enabled="False" ></asp:TextBox>
                                </td>
                                <td>
                                    <%--<asp:Button ID="btnEdit" runat="server" Text="Edit" Width="90px" onclick="btnEdit_Click" />--%>
                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit Department" onclick="btnEdit_Click" ></asp:LinkButton>
                                </td>
                            </tr>

                            <tr style="height:40px">
                                <td colspan="2">
                                        &nbsp;
                                </td>
                                <td  colspan="2">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" onclick="btnUpdate_Click" Width="60px" Enabled="False" />&nbsp;&nbsp;
                                    <asp:Button ID="btndelete" runat="server" Text="Delete" onclick="btndelete_Click" Width="60px" Enabled="False" />&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" Width="60px" Enabled="False" />
                                </td>
                            </tr>
        
                        </table>
        
                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width:50%">
                                <asp:Panel ID="pnlDepartmentRoles" runat="server" GroupingText="Select Roles" Visible="False" Font-Bold="False" >

                                    <table width="100%">
                            
                                        <tr style="height:40px">
                                            <td style="width:25%">
                                                &nbsp;
                                            </td>
                                            <td style="width:25%">
                                                <b>Select Department:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlDepartments" runat="server" Width="200px" AutoPostBack="True" onselectedindexchanged="ddlDepartments_SelectedIndexChanged" ></asp:DropDownList>  
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td valign="top" style="padding-left:5px">
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <b>Select Roles:</b>
                                            </td>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="width:45%">
                                                            <asp:ListBox ID="lstRoleLevel" runat="server" AutoPostBack="True" Height="80px" Width="200px" onselectedindexchanged="lstRoleLevel_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                        <td style="height:100%">
                                                            <asp:ImageButton ID="imgBtnUp" runat="server" ImageUrl="images/up-arrow.png" onclick="imgBtnUp_Click" />
                                                            <br /><br /><br />        
                                                            <asp:ImageButton ID="imgBtnDown" runat="server" ImageUrl="images/down-arrow.gif" onclick="imgBtnDown_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height:40px">
                                                            <asp:Button ID="btnUpdateList" runat="server" Text="Update Level" Width="100px" onclick="btnUpdateList_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </asp:Panel>
                            </td>





                            <td style="width:50%;height:100%">
                    
                                <asp:Panel id="pnlEditRoles" runat="server" GroupingText="Edit Roles" Visible="False" Font-Bold="False" Height="191px" >
                        
                                    <table width="100%" style="height:100%">
                            
                                        <tr style="height:50px">
                                
                                            <td style="width:10%">
                                                &nbsp;
                                            </td>
                                            <td style="width:15%" valign="bottom">
                                                <b>Role Name:</b></td>
                                            <td valign="bottom" style="width:40%">
                                                <asp:TextBox ID="txtRoleName" runat="server" Width="200px" Enabled="False" ></asp:TextBox>
                                            </td>
                                            <td valign="bottom">
                                                <asp:LinkButton ID="btnEditRole" runat="server" onclick="btnEditRole_Click">Edit Role</asp:LinkButton>
                                                <%--<asp:Button ID="btnEditRole" runat="server" Text="Edit Role" Width="100px" onclick="btnEditRole_Click" />--%>
                                            </td>
                                        </tr>

                                        <tr style="height:70px">
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <b>Email ID:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="200px" Enabled="False" ></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="btnNewRole" runat="server" onclick="btnNewRole_Click">Create Role</asp:LinkButton>
                                                <%--<asp:Button ID="btnNewRole" runat="server" Text="Create Role" Width="100px" onclick="btnNewRole_Click" />--%>
                                            </td>
                                        </tr>
                                        <tr style="height:44px">
                                            <td colspan="2">
                                                &nbsp;
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="btnUpdateRole" runat="server" Text="Update" Width="90px" onclick="btnUpdateRole_Click" Enabled="False" />&nbsp;&nbsp;
                                                <asp:Button ID="btnDeleteRole" runat="server" Text="Delete" Width="90px" onclick="btnDeleteRole_Click" Enabled="False" />&nbsp;&nbsp;
                                                <asp:Button ID="btnCancelRole" runat="server" Text="Cancel" Width="90px" onclick="btnCancelRole_Click" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblError1" runat="server" Text="" ForeColor="Red" Font-Bold="true" ></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Panel ID="pnlEscalation" runat="server" GroupingText="Define Escalation" 
                        Visible="false" Font-Bold="False"  >
                    
                        <table width="100%">
                            <tr style="height:40px">
                                <td style="width:5%">
                                    &nbsp;
                                </td>
                                <td style="width:10%">
                                    <b>Process Name:-</b>
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlProcessType" runat="server" AutoPostBack="true" Width="200px" onselectedindexchanged="ddlProcessType_SelectedIndexChanged" ></asp:DropDownList>             
                                </td>
                                <td rowspan="2" valign="top" style="padding-left:20px;padding-top:10px">
                                    <asp:Panel ID="pnlList" runat="server">
                                        <table width="100%">
                                            <tr>
                                                <td style="width:20%">
                                                    <asp:ListBox ID="lstEscalate" runat="server" Width="200px"></asp:ListBox>
                                                </td>
                                                <td style="height:100%">
                                                    <table width="100%" style="height:100%">
                                                        <tr valign="top">
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnEscUp" runat="server" ImageUrl="images/up-arrow.png" onclick="imgbtnEscUp_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr valign="bottom">
                                                            <td>
                                                                <asp:ImageButton ID="imgbtnEscDown" runat="server" ImageUrl="images/down-arrow.gif" onclick="imgbtnEscDown_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>                                                       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height:40px" colspan="2">
                                                    <asp:Button ID="btnEscReOrder" runat="server" Text="Update Level" Width="100px" onclick="btnEscReOrder_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnEscdelete" runat="server" Text="Delete Level" Width="100px" onclick="btnEscdelete_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>

                            <tr style="height:40px">
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <b>Select Role Name:</b>
                                </td>
                                <td style="width:18%">
                                    <asp:DropDownList ID="ddlEscRoleName" runat="server" Width="200px" AutoPostBack="true" ></asp:DropDownList>
                                </td>
                                <td style="width:15%">
                                    <%--<asp:LinkButton ID="lnkEscNew" runat="server" Text="Create New"></asp:LinkButton>--%>
                                    <asp:LinkButton ID="lnkInsert" runat="server" Text="Insert Escalation" onclick="lnkInsert_Click"></asp:LinkButton>
                                </td>
                                <td>
                                    
                                    
                                </td>
                            </tr>

                            


                        </table>

                    </asp:Panel>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Panel ID="pnlProcessState" runat="server" 
                        GroupingText="Department Processes" Visible="False" Font-Bold="False"  >
    
                        <table width="100%">
                            <tr style="height:25px">
                                <td style="width:5%">
                                    &nbsp;
                                </td>
                                <td style="width:10%">
                                    <b>Process Name:-</b>
                                </td>
                                <td style="width:15%">
                                    <asp:DropDownList ID="ddlProcessTypeID" runat="server" AutoPostBack="true" Width="180px" onselectedindexchanged="ddlProcessTypeID_SelectedIndexChanged" ></asp:DropDownList>
                                </td>
                                <td>
                                    &nbsp;           
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnAddField" runat="server" Text="Add Rows" onclick="btnAddField_Click" />
                                </td>
                            </tr>
                        </table>
   
          
                            <br />


                        <asp:GridView ID="grdDepartmentProcesses" runat="server" 
                            AutoGenerateColumns="False" Width="100%" onrowediting="grdDepartmentProcesses_RowEditing" onrowcancelingedit="grdDepartmentProcesses_RowCancelingEdit" 
                            onrowdeleting="grdDepartmentProcesses_RowDeleting" onrowupdating="grdDepartmentProcesses_RowUpdating" CellPadding="4" 
                            AccessKeyForeColor="#333333" GridLines="None" EnableViewState="true" >
                        
                            <AlternatingRowStyle BackColor="White" />
                        
                            <Columns>

                                <asp:TemplateField HeaderText="Process name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessStatusID" Visible="false" runat="server" Text='<%# Eval("processStatusID")%>'></asp:Label>
                                        <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("processStatusName")%>'></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProcessName" runat="server" Text='<%# Eval("processStatusName")%>' ></asp:TextBox>
                                        <asp:DropDownList ID="ddlProcessStatusID" Visible="false" runat="server" Width="120px" ></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Process Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessDescription" runat="server" Text='<%# Eval("processStatusDesc") %>'></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtProcessDescription" runat="server" Text='<%# Eval("processStatusDesc") %>' ></asp:TextBox> 
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Next Process Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNextProcessStatusID" runat="server" Visible="false" Text='<%# Eval("nextProcessStatusID") %>'></asp:Label>
                                         <asp:Label ID="lblNextProcessStatusIDName" runat="server" Text='<%# Eval("nxtStatusName") %>'></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlNextProcessStatusID" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Prev Process Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrevProcessStatusID" runat="server" Visible="false" Text='<%# Eval("prevprocessStatusID") %>'></asp:Label>
                                        <asp:Label ID="lblPrevProcessStatusIDName" runat="server" Text='<%# Eval("prevStatusName") %>'></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPrevProcessStatusID" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Next Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNextRole" runat="server" Visible="false" Text='<%# Eval("nextRole") %>'></asp:Label>
                                        <asp:Label ID="lblNextRoleName" runat="server" Text='<%# Eval("nxtRoleName") %>'></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlNextRole" runat="server" Width="140px"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Previous Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPreviousRole" runat="server" Visible="false" Text='<%# Eval("prevRole") %>'></asp:Label>
                                        <asp:Label ID="lblPreviousRoleName" runat="server" Text='<%# Eval("prevRoleName") %>'></asp:Label>
                                        <asp:Label ID="lblAuto" runat="server" Text='<%# Eval("autoIndex") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                    
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPreviousRole" runat="server" Width="140px"></asp:DropDownList>
                                        <asp:TextBox ID="txtAuto" runat="server" Text='<%# Eval("autoIndex") %>' Visible="false"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:CommandField  HeaderText="Edit Data" ShowEditButton="true" 
                                    ShowHeader="true">
                                <HeaderStyle HorizontalAlign="Left" />
                                </asp:CommandField>
                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" 
                                    ShowHeader="True" >


                                <HeaderStyle HorizontalAlign="Left" />
                                </asp:CommandField>


                            </Columns>   
            
                     
                         
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>                
                        </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
    <div id="container" style="padding-left:20px;height:35px">
        <!--[if lte IE 6]><![if gte IE 7]><![endif]-->
<!-- code here -->
<!--[if lte IE 6]><![endif]><![endif]-->

        <a href="#" id="trigger" ><font color="gray"><b>Manage Roles Help</b></font></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" id="trigger1"><font color="gray"><b>Manage Escalation Help</b></font></a> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" id="trigger2"><font color="gray"><b>Manage Processes Help</b></font></a>
        <!-- HIDDEN / POP-UP DIV -->
            
        <div id="pop-up">
            <p>
                Select Department From Department List And The Roles For Selected Department 
                Will Be Displayed In The ListBox . Move Roles UP / DOWN as Needed and Click 
                UPDATE LEVEL Button to Re-Arrange Their Order. You Can Also Create,Edit Or 
                Delete The Roles. For Creating and Editing Roles Click  CREATE ROLE/EDIT 
                ROLE Buttons Respectively. To Delete A Role Click edit Button, Select a 
                Role in Role list and then Click DELETE Button.
            </p>
        </div>

        <div id="pop-up1">
            <p>
                Select Process Name From DropDownList To View Escalations. Click UP/DOWN Arrow To Rearrange Escalations as Needed
                and Click UPDATE Button.
                You Can Also Insert a new escalation In Selected Process By Selecting The Required level From Role Name DropDown 
                and Then Clicking on INSERT ESCALATION Button.
                To Delete a Escalation Level Select Level in List and Click DELETE Button.
            </p>
        </div>

        <div id="pop-up2">
            <p>
                Select Process Name From DropDownList To View or Manage processes. Click ADD ROWS Button to Add a New Process. 
                For First Time You Can't Give Next/Previous Processes But You Can Select Next/Previous Escalation Roles From dropDownList. 
                To Give Next/Previous Processes Again Go in edit Mode and Then Select processes as Needed.
            </p>
        </div>
        
    </div>
     </asp:Content>