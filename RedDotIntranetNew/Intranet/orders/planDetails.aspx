<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="planDetails.aspx.cs" Inherits="Intranet_orders_PlanDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../MsgBoxControl.ascx" tagname="MsgBoxControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        
        #Button1
        {
            width: 94px;
        }
        
        /*Accordian*/
        
            .accordionContent 
            {
               
                border-color: -moz-use-text-color #2F4F4F #2F4F4F;
                border-right: 1px dashed #2F4F4F;
                border-style: none dashed dashed;
                border-width: medium 1px 1px;
                padding: 10px 5px 5px;
                width:99%;
            }
            
            .accordionHeaderSelected 
            {
                background-color: #5078B3;
                border: 1px solid #2F4F4F;
                color: white;
                cursor: pointer;
                font-family: Arial,Sans-Serif;
                font-size: 12px;
                font-weight: bold;
                margin-top: 5px;
                padding: 5px;
                width:99%;
            }
            .accordionHeader 
            {
                background-color: #2E4D7B;
                border: 1px solid #2F4F4F;
                color: white;
                cursor: pointer;
                font-family: Arial,Sans-Serif;
                font-size: 12px;
                font-weight: bold;
                margin-top: 5px;
                padding: 5px;
                width:99%;
            }
            .href
            {
                color:White; 
                font-weight:bold;
                text-decoration:none;
                width:100%;
            }
            
            
        /* Accordian Closed */
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">

       function CheckChanged() {
           var checkObject = $get('<%=chkExecutionCompleted.ClientID %>'); 

           if (checkObject.checked==true) {
               alert("Information ! You will be escalated to IFF form submission stage after processing the current stage you are in");
           }
           else {
               //alert("Iff closed");
           }
       }

       function getIndex(index) {
           alert(index);
       }

       function ConfirmForUpdate() {

           var ans = confirm('Are you sure you want to perfom this action ????');

           if (ans) {
               document.getElementById("btnSubmitHtml").style.visibility = 'hidden';
               document.getElementById('<%= btnSubmit.ClientID %>').style.display = 'inherit';
               document.getElementById('<%= btnSubmit.ClientID %>').click();
           }

           // else
           // alert("canceled");  
       }


       function disableBtn(btnID, newText) {

           var btn = document.getElementById(btnID);
           setTimeout("setImage('" + btnID + "')", 10);
           btn.disabled = true;
           btn.value = newText;
       }

       function setImage(btnID) {
           var btn = document.getElementById(btnID);
           btn.style.background = 'url(/images/btnDisabled.gif)';
       }
       function getConfirmationForDecline() {
           return confirm('Being Declined!!! Are you sure you want to perfom this action ?');
       }
function btnSubmitHtml_onclick() {

}

    </script>

      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        <div class="main-content-area">
       
            <table id="tblDetails" runat="server" width="100%" style="border-style:solid; border-width:0px; padding:0px; font-size:12px;background-color:#DFEFFF">
                <tr>
                    <td style="width:100%">
                            <table width="100%">
                                <tr>
                                    <td style="width:97%" align="center">
                                        <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="18px" Font-Names="Calibri"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblClaimsPaymentsMsg" runat="server" Font-Bold="True" Font-Size="15px" Font-Names="Calibri" ForeColor="#cc66ff"></asp:Label>
                                    </td>
                                    <td style="width:3%">
                                        <a href="viewOrdersMKT.aspx?wfTypeId=10031">Back</a>
                                    </td>
                                </tr>
                            </table>
                    </td>
                </tr>
   
                <tr>
                    <td style="width:100%">
                        <b>Plan ID :</b>&nbsp;<asp:Label ID="lblrefId" runat="server" Font-Bold="True" Font-Size="14px" Font-Names="Calibri" Visible="true" ForeColor="Silver"></asp:Label>
                        &nbsp;Version Dated :&nbsp;<asp:Label ID="lblThisVerDate" runat="server" Font-Bold="True" Font-Size="14px" Font-Names="Calibri" Visible="true" ForeColor="Silver" Text="23-Jun-2017"></asp:Label>
                        &nbsp;<asp:Label ID="lblPlanTableSts" runat="server" Font-Bold="True" Font-Size="14px" Font-Names="Calibri" Visible="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
    
                <tr>
                    <td style="width:100%">
                        <asp:Panel ID="panelInfo" runat="server" CssClass="PlansPanel" GroupingText="Plan Information" >
                            <table width="95%">
                                <tr>
                                    <td style="width:20%">
                                        Vendor/BU
                                    </td>
                                    <td style="width:22%">
                                        <asp:Label ID="lblVendorBUId" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblVendorBUAbbr" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="txtVendor" runat="server" Text="" ></asp:Label>
                                    </td>
                                    <td style="width:20%">
                                        Vendor Activity ID
                                    </td>
                                    <td style="width:38%">
                                        <asp:Label ID="txtActivityId" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>                  
                                <tr>
                                    <td style="width:20%">
                                        RDD Quater
                                    </td>
                                    <td style="width:22%">
                                        <asp:Label ID="txtQuarter" runat="server" Text="" Font-Bold="true"></asp:Label>&nbsp;&nbsp;Vendor Quater :
                                        &nbsp;<asp:Label ID="txtVendorQuarter" runat="server" Text="" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width:20%">
                                        Plan Year
                                    </td>
                                    <td style="width:38%">
                                        <asp:Label ID="txtyear" runat="server" Text="" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:20%" valign="top">Vendor Approved $</td>
                                    <td style="width:22%;font-weight:bold" valign="top"><asp:Label ID="txtApprovedAmt" Font-Bold="true" runat="server" Text="" ></asp:Label>
                                    <br />
                                        <asp:ListBox ID="lstAmtsSplit" runat="server" Width="99%" Height="50px" BackColor="#ccffff" Font-Size="Small"></asp:ListBox>
                                    </td>
                                    <td style="width:20%">Approval Date</td>
                                    <td style="width:38%"><asp:Label ID="txtApprovedDate" runat="server" Text=""></asp:Label>&nbsp;(mm-dd-yyyy)</td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Plan Actual Cost $</td>
                                    <td style="width:22%"><asp:Label ID="txtActualCost" runat="server" Text="" ></asp:Label></td>
                                    <td style="width:20%">DeadLine Date</td>
                                    <td style="width:38%"><asp:Label ID="lblDeadLineDate" runat="server" Text="" Font-Bold="true" ForeColor="#cc3399" Font-Size="13px"></asp:Label>&nbsp;(mm-dd-yyyy)</td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Actual Plan File</td>
                                    <td colspan="3"><asp:Label ID="lblFile" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Vendor Plan File</td>
                                    <td colspan="3"><asp:Label ID="lblFileVendor" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Current Status</td>
                                    <td style="width:22%"><asp:Label ID="lblStatus" runat="server" Font-Bold="true" Text="" ForeColor="Blue"></asp:Label></td>
                                    <td style="width:20%">Last Modified</td>
                                    <td style="width:38%"><asp:Label ID="lblLastModified" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Plan Description</td>
                                    <td colspan="3">
                                    <asp:Label ID="txtDesc" runat="server" Text="" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:20%">Updation Comments</td>
                                    <td colspan="3">
                                    <asp:Label ID="txtcomments" runat="server" Text="" ></asp:Label></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width:100%">
                        <asp:Label ID="lblMsg" runat="server" Font-Bold="false" Font-Size="12px" Font-Names="Calibri" Visible="true" ForeColor="Red" ></asp:Label>
                        <asp:Panel ID="panelDownLoads" runat="server" CssClass="PlansPanel" GroupingText="Downloads & Uploads">
                            <table width="100%">
                                <tr>
                                    <td style="width:30%"  valign="top">
                                        <table width="100%">
                                            <tr style="height:40px;font-weight:bold">
                                                <td style="width:30%;" >File List</td>
                                                <td style="width:70%">Available Downloads</td>
                                            </tr>
                                            <tr>
                                                <td style="width:30%">Actual Plan File</td>
                                                <td style="width:70%"><a id="lnkPlan" runat="server" href="#" style="color:Blue">Actual Plan File</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width:30%">Vendor Plan File</td>
                                                <td style="width:70%"><a id="lnkPlanVendor" runat="server" href="#" style="color:Blue">Vendor Plan File</a></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width:70%" valign="top">
                                        <b>Check from the list of existing attachements of this Plan to be farwarded</b><br />
                                        <asp:Label ID="lblNone" Font-Bold="true" runat="server" Text="None" Visible="true" ForeColor="Black" ></asp:Label>
                                        <asp:GridView ID="GridFiles" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="White" ShowHeader="False" BorderStyle="None" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chklstAttachedFiles" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "websitefilePath")%>' /> &nbsp;
                                                        <asp:HyperLink ID="lnkFileLoc" Text="Download" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <b>File Uploads (optional)</b><br /><br />
                                        <asp:FileUpload ID="fileUpload1" runat="server" CssClass="" />&nbsp;
                                        <input type="button" onclick="AddNewRow(); return false;"  value="Browse More Files....." style="font-size: 9px" />&nbsp;
                                        <asp:Button ID="btnCan" Text="Cancel All" runat="server" style="font-size: 9px"  />
                                        <div id="divFileUploads">
                                        </div>
                                   </td>
                                   
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width:100%">
                        <table width="100%" border="1">
                            <tr>
                                <td valign="middle">
                                   <asp:Panel ID="paneMUFFormView" runat="server" Visible="false">
                                   <%--Accordian--%>
                                        <cc1:Accordion ID="UserAccordion" runat="server" SelectedIndex="-1" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" Width="100%" Height="100%"
                                        ContentCssClass="accordionContent" FadeTransitions="false" SuppressHeaderPostbacks="true" TransitionDuration="500" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None" >
                                            <Panes>
                                                <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                                    <Header><a href="#" class="href" >Click here ! To View/Hide MUF Forms</a></Header>
                                                    <Content>
                                                      <asp:Label ID="lblMUFFormView" runat="server" Text="" Visible="true" ForeColor="Blue"></asp:Label>
		                                             </Content>
                                                </cc1:AccordionPane>
                                             </Panes>
                                        </cc1:Accordion>
                                    <%--Accordian Closed--%>
                                 </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td style="width:100%">
                        <table width="100%" border="1">
                            <tr>
                                <td valign="middle">
                                    <asp:Panel ID="panelAccrual" runat="server" Visible="false">
                                        <table width="65%">
                                            <tr>
                                                <td style="width:20%"><asp:Label ID="lblAccrSuppDocTitle" runat="server" Text=" Accrual Supporting Documents :"></asp:Label></td>
                                                <td style="width:80%"><asp:TextBox ID="txtAccrualSupportingDocumentText" Text="Supporting Documents" runat="server" Width="780px" TextMode="MultiLine" Height="33px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width:20%"><asp:Label ID="lblAttachedEmailtxt" runat="server" Text=" MDF Confirmation e-Mail :"></asp:Label></td>
                                                <td style="width:80%"><asp:TextBox ID="txtAttachedEmailtxt" Text="" runat="server" Width="780px" TextMode="MultiLine" Height="33px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                <asp:Label ID="lblAccrualFormView" runat="server" Text="" Visible="true" ForeColor="Blue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width:100%">
                        <asp:Panel ID="panelActivity" runat="server" CssClass="PlansPanel" GroupingText="Activity Allocation" Visible="true">
                        <asp:Label ID="lbltest" runat="server" Text="" Visible="true" ForeColor="Blue"></asp:Label>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table style="width:100%">
                                            <tr>
                                                <td style="width:48%" align="left">
                                                    Accrual Form No.&nbsp;
                                                    <asp:Label ID="lblAccrualFormNo" runat="server" Text="" Visible="true" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblnxtAccrualFormSerial" runat="server" Text="" Visible="false" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                    <asp:Label ID="lblnxtActivitySrno" runat="server" Text="" Visible="false" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width:52%" align="left">
                                                    <asp:Button ID="btnAddRow" runat="server" Font-Bold="true" Font-Size="9px" Text="Add Row.." CssClass="btnAddRowsCss" onclick="btnAddRow_Click" ToolTip="Click! to Add an additional row"  />       
                                                    <asp:Button ID="btnClearAll" runat="server" Font-Bold="true" Font-Size="9px" Text="Clear All" CssClass="btnAddRowsCss" onclick="btnClearAll_Click" ToolTip="Click! to clear data in all rows"  />       
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server"  CssClass="GridviewStyle" >
                                        <HeaderStyle Font-Bold="true"  HorizontalAlign="Center" Height="40px"/>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Serial">
                                                    <ItemTemplate> 
                                                        <asp:Label ID="lblautoindex" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "autoindex")%>'></asp:Label>
                                                        <asp:Label ID="lblSerial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Serial")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" Font-Bold="true" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Month">
                                                    <ItemTemplate> 
                                                        <asp:Label ID="lblMonth" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "month")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate> 
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "date","{0:MM-dd-yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="55px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ActivityID">
                                                    <ItemTemplate> 
                                                        <asp:Label ID="lblActivityIDFormed" runat="server" Text="" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                        typeid:<asp:Label ID="lblActivityTypeID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "activityTypeId")%>' ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                        <asp:DropDownList ID="ddlActivityType" Width="140px" runat="server" Height="16" Font-Size="10px" onselectedindexchanged="ddlActivityType_SelectedIndexChanged" AutoPostBack="true"> </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="142px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount $">
                                                    <ItemTemplate> 
                                                        <asp:TextBox ID="txtAmount" Text='<%# DataBinder.Eval(Container.DataItem, "amount")%>' runat="server" Width="70px" Visible="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="75px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Activity Detail">
                                                    <ItemTemplate> 
                                                        <asp:TextBox ID="txtActivityDetail" Text='<%# DataBinder.Eval(Container.DataItem, "detail")%>' runat="server" Width="445px" Visible="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="450px" />
                                                </asp:TemplateField>
                          
                                                <asp:TemplateField>
                                                    <ItemTemplate> 
                                                        <asp:ImageButton ID="imgBtnClose" ImageUrl="/images/close-icon.png"  runat="server"  OnClick="imgBtnClose_Click" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width:100%">

                    <asp:Panel ID="panelExecutionCheck" runat="server" CssClass="PlansPanel" GroupingText="Execution Status Update" Visible="false">
                         <table width="100%" border="0px">
                          <tr>
                           <td style="width:60%">
                             <asp:CheckBox ID="chkExecutionCompleted" runat="server" 
                                   style="color:Blue;font-size:13px;font-weight:bold" 
                                   Text="Plan Execution Completed, MUF forms submitted for all the expenses under this plan, Move to Filing IFF form Stage" 
                                   ToolTip="Check! to See additional rows" AutoPostBack="false" Checked="False" 
                                    />  
                           </td>
                           <td style="width:40%">
                           &nbsp;
                           </td>
                          </tr>
                          <tr>
                           <td colspan="2" style="font-weight:bold;padding-left:6px" >
                             <font color="Red">Important Note :  </font>Once submitted with this option as checked , system will escalate to stage filing IFF form, therefore no Muf forms can be added at further levels.
                            </td>
                          </tr>
                         </table>
                        <br />
                        </asp:Panel>

              <asp:Panel ID="panelStatusUpdate" runat="server" CssClass="PlansPanel" GroupingText="Activity Status Update" Visible="false"> 
                <asp:GridView ID="GridActStatusUpdate" runat="server" 
                    AutoGenerateColumns="False" EnableViewState="true" Width="100%" >
                
                    <AlternatingRowStyle BackColor="White" />
                
                    <Columns>
                        <asp:TemplateField>

                            <ItemTemplate>
                                <table width="100%" border="1px">
                                    <tr>

                                        <td style="width:4%">
                                            <b>Sr No. </b>
                                        </td>
                                        <td style="width:4%">
                                            <b>AF No.</b>
                                        </td>
                                        <td style="width:9%">
                                            <b>RDD Activity No. </b>
                                        </td>
                                        <td style="width:28%">
                                            <b>Description </b>
                                        </td>
                                        <td style="width:8%">
                                            <b>Execution Status</b>
                                        </td>
                                        <td style="width:9%">
                                           <asp:Label ID="Label2" Font-Bold="true" runat="server" Text="Vendor Amount $"></asp:Label>
                                        </td>
                                        <td style="width:9%">
                                           <asp:Label ID="Label3" Font-Bold="true" runat="server" Text="Funds Available $"></asp:Label>
                                        </td>
                                        <td style="width:6%">
                                           <asp:Label ID="Label1" Font-Bold="true" runat="server" Text="Child Rows"></asp:Label>
                                        </td>
                                        <td style="width:8%">
                                           <asp:Label ID="Label4" Font-Bold="true" runat="server" Text="Has Expences ?"></asp:Label>
                                        </td>
                                        <td style="width:14%" align="right">
                                            <asp:Button ID="btnAddField" runat="server" CommandName="ADD" CssClass="btnAddRowsCss" Font-Bold="true" Font-Size="9px" Text="Add Expense" 
                                            ToolTip="Click! to Add an additional row" Enabled="false" onclick="btnAddField_Click" />     
                                        
                                        
                                            <asp:CheckBox ID="chkView" runat="server" Text="View.." ToolTip="Check! to See additional rows" AutoPostBack="true" Checked="False" 
                                            EnableViewState="false" oncheckedchanged="chkView_CheckedChanged" /><br />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>
                                            <asp:Label ID="lblsno" runat="server" Text='<%# Eval("sno")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAccFormNo" runat="server" Text='<%# Eval("AccrualFormNo")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblActivityCode" runat="server" Text='<%# Eval("ActivityCode")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("Activitydetail")%>'></asp:Label>
                                        </td>
                                        <td colspan="0">
                                            <asp:Label ID="lblActExeStatus" runat="server" Text='<%# Eval("ActivityExecutionStatus")%>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlActExeStatus" runat="server" Width="110px">
                                               <asp:ListItem>Executed</asp:ListItem>
                                               <asp:ListItem>Not Initiated</asp:ListItem>
                                                <asp:ListItem>Initiated</asp:ListItem>
                                                <asp:ListItem>In Process</asp:ListItem>
                                            
                                            </asp:DropDownList>
                                        </td>
                                         <td>
                                           <asp:Label ID="lblActivityVendorAmount" runat="server" Text='<%# Eval("ActivityVendorAmount")%>'></asp:Label>
                                        </td> 
                                        <td>
                                           <asp:Label ID="lblFundsAvailable" runat="server" Text='<%# Eval("FundsAvailable")%>' Font-Bold="true"></asp:Label>
                                        </td>           
                                         <td>
                                           <asp:Label ID="lblchildRowsCount" runat="server" Text='<%# Eval("childRowsCount")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblhasExpenses" runat="server" Text='<%# Eval("hasExpenses")%>' Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlhasExpenses" runat="server" Width="70px" onselectedindexchanged="ddlhasExpenses_SelectedIndexChanged" AutoPostBack="true">
                                               <asp:ListItem>Yes</asp:ListItem>
                                               <asp:ListItem>No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;</td>                            
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                                    <FooterStyle BackColor="#990000" Font-Bold="True" 
                                        ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" 
                                        ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" 
                                        HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" 
                                        ForeColor="Navy" />
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
                <br />
                <asp:Label ID="lblGridMsg" Text="Activity Details Are Shown Below In a Grid, For The Selected Activity Above, You Are Currently Working On Activity : " ForeColor="Blue" Font-Bold="True" runat="Server" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="lblSelectedActivity" runat="server" ForeColor="#800000" Font-Bold="True" Visible="true"></asp:Label> 
                &nbsp;Funds Available $&nbsp;<asp:Label ID="lblFundBalanceAvailableForSelectedActivity" runat="server" ForeColor="#800000" Font-Bold="True" Visible="true" Text="0"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" Font-Bold="true" Font-Size="15px" Font-Names="Calibri" Visible="true" ForeColor="Red" ></asp:Label>
                <br />
                <asp:GridView ID="Grid2" runat="server" AutoGenerateColumns="False" onrowcancelingedit="Grid2_RowCancelingEdit" 
                            onrowediting="Grid2_RowEditing" onrowupdating="Grid2_RowUpdating" 
                            onrowdeleting="Grid2_RowDeleting" Width="100%" BackColor="White" 
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                ForeColor="Black" GridLines="Vertical" 
                      onrowdatabound="Grid2_RowDataBound" >

                                <AlternatingRowStyle BackColor="White" />

                                <Columns>
                                    <asp:TemplateField>
                                            <ItemTemplate>
                                              <table style="width:100%" >
                                                    <tr>
                                                        <td style="width:100%">
                                                                <table width="100%" border="0px">
                                                                    <tr>
                                                                        <td style="width:14%">
                                                                            <b>MUF No</b>&nbsp;(<asp:Label ID="lblAutoIndex_MUFNo" Font-Bold="true" runat="server" Text='<%#Eval("AutoIndex_MUFNo") %>'></asp:Label>)
                                                                        </td>
                                                                        <td style="width:18%"><b>Modified</b>&nbsp;<asp:Label ID="lbllastModified" runat="server" Text='<%#Eval("lastModified") %>'></asp:Label></td>

                                                                        <td style="width:15%;font-size:10px" >
                                                                            <b>Execution Date </b> (MM-DD-YYYY)
                                                                        </td>
                                                                        <td style="width:18%"><asp:Label ID="lblAcDateOfExecution" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"ActualDateofExecution","{0:MM/dd/yyyy}")%>'></asp:Label></td>
                                                        
                                                                        <td style="width:15%">
                                                                            <b>Activity Cost $</b>
                                                                        </td>
                                                                        <td style="width:20%"><asp:Label ID="lblCosted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Costed") %>'></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Activity Type</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblTypeOfActivity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ActivityType") %>'></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <b>Activity Location</b>&nbsp;<asp:Label ID="lblLocationOfActivity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ActivityLocation") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <b>Expensed At</b>&nbsp;<asp:Label ID="lblExpensedAt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpensedAt") %>'></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <b>VAT $</b>
                                                                        </td>
                                                                        <td><asp:Label ID="lblVat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"vat") %>'></asp:Label></td>
                                                                    </tr>
                                                                        
                                                                    <tr>
                                                                        <td>
                                                                            <b>Expense Detail</b>
                                                                        </td>
                                                                        <td colspan="3"><asp:Label ID="lblExpDetail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ExpenseDetail") %>'></asp:Label></td>
                                                                        
                                                                        <td>
                                                                            <b>Activity Description</b>
                                                                        </td>
                                                                        <td> <asp:Label ID="lblActivityDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ActivityDesc") %>'></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Third Party Name</b>
                                                                        </td>
                                                                        <td colspan="3"><asp:Label ID="lblThirdPartyName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ThirdPartyName") %>'></asp:Label></td>
                                                                            
                                                                        <td>
                                                                            <b>Third Party Ref. No</b>
                                                                        </td>
                                                                        <td><asp:Label ID="lblThirdPartyInvoiceReference" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ThirdPartyInvoiceReference") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <b>Attached MUF Document</b>
                                                                        </td>
                                                                        <td colspan="5"><asp:Label ID="lblmuffilePath" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"muffilePath") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                      <td colspan="6">
                                                                         <table width="100%" border="0px">
                                                                           <tr>
                                                                               <td style="width:25%" align="center">Submitted By</td>
                                                                               <td style="width:25%" align="center">Verified By</td>
                                                                               <td style="width:25%" align="center">Accrued By</td>
                                                                               <td style="width:25%" align="center">Authorised By</td>
                                                                           </tr>
                                                                           <%--<tr>
                                                                             <td align="center"><asp:Label ID="lblsubmittedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblsubmittedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblverfiedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblverfiedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblaccruedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblaccruedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblAuthorisedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblAuthorisedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedDate") %>'></asp:Label></td>
                                                                           </tr>--%>
                                                                           <tr>
                                                                             <td align="center"><asp:Label ID="lblmufsubmittedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufsubmittedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufverfiedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufverfiedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufaccruedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufaccruedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufAuthorisedBy" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufAuthorisedDate" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedDate") %>'></asp:Label></td>
                                                                           </tr>
                                                                         </table>
                                                                      </td>
                                                                    </tr>
                                                                </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlItemTemplateChild" runat="server" Visible="false">
                                                                <table width="100%" border="0px">
                                                                    <tr>
                                                                        <td style="width:14%">
                                                                            <b>Recieved In RDD Qtr</b>
                                                                        </td>
                                                                        <td style="width:18%"><asp:Label ID="lblRecievedInRDDQtr" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"ReceivedInRDDQTR") %>'></asp:Label></td>

                                                                        <td style="width:15%">
                                                                            <b>RDD Invoice No</b>
                                                                        </td>
                                                                        <td style="width:18%"><asp:Label ID="lblRddInvoiceNo" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"RddInvoiceNo") %>'></asp:Label></td>
                                               
                                                                        <td style="width:15%">
                                                                            <b>RDD Paid Status</b>
                                                                        </td>
                                                                        <td style="width:20%"><asp:Label ID="lblRddPaidStatus" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"RddPaidStatus") %>'></asp:Label></td>

                                                                        
                                                                    </tr>

                                                                    <tr>

                                                                        <td style="width:10%">
                                                                            <b>Total Payment Received</b>
                                                                        </td>
                                                                        <td style="width:15%"><asp:Label ID="lblTotalPaymentRecieved" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"TotalPaymentReceived") %>'></asp:Label></td>  
                                                                        
                                                                        <td>
                                                                            <b>Third Party Paid Status</b>
                                                                        </td>
                                                                        <td ><asp:Label ID="lblThirdPartyPaidStatus" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"ThirdPartyPaidStatus") %>'></asp:Label></td>
                                                                        <td>
                                                                            <b>Third Party Invoice No.</b>
                                                                        </td>
                                                                        <td ><asp:Label ID="lblThirdPartyInvoiceNo" runat="server" Width="90%" Text='<%# DataBinder.Eval(Container.DataItem,"ThirdPartyInvoiceNo") %>'></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>RDD Payment Details</b>
                                                                        </td>
                                                                        <td colspan="5">
                                                                            <asp:Label ID="lblRddPaymentDetails" runat="server" Width="98%" Text='<%# DataBinder.Eval(Container.DataItem,"RDDPaymentDetails") %>'></asp:Label>
                                                                            <asp:Label ID="lblUniqueID" runat="server" Width="98%" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"fk_ActivitySno") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <table style="width:100%" >
                                                    <tr>
                                                        <td style="width:100%">
                                                                
                                                                <table width="100%" border="0px">
                                                                    <tr>
                                                                        <td style="width:14%">
                                                                            <b>MUF No</b>&nbsp;<asp:TextBox ID="txtAutoIndex_MUFNo" runat="server" Enabled="false" Width="25%" Text="" TabIndex="0"> </asp:TextBox>
                                                                        </td>
                                                                        <td style="width:18%">
                                                                          <b>Modified</b>&nbsp;<asp:Label ID="lbllastModifiedEdt" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        
                                                                        <td style="width:15%;font-size:10px" >
                                                                            <b>Execution Date </b> (MM-DD-YYYY)
                                                                        </td>
                                                                        <td style="width:18%"><asp:TextBox ID="txtAcDateOfExecution" runat="server" Width="90%" Text="" TabIndex="1"></asp:TextBox>
                                                                        
                                                                        <td style="width:15%">
                                                                            <b>Cost Of Activity $</b>
                                                                        </td>
                                                                        <td style="width:20%">
                                                                         <asp:Label ID="lblCostedPrev" runat="server" Width="90%" Text="" Visible="false"></asp:Label>
                                                                         <asp:TextBox ID="txtCosted" runat="server" Width="90%" Text="" TabIndex="2"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Type Of Activity</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlTypeOfActivity" runat="server" Width="90%" TabIndex="3">
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                        <td>
                                                                            <b>Location</b>
                                                                            <asp:DropDownList ID="ddlLocationOfActivity" runat="server" Width="60%" TabIndex="4">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <b>Expensed at</b>
                                                                            <asp:DropDownList ID="ddlExpensedAt" runat="server" Width="56%" TabIndex="5">
                                                                            </asp:DropDownList>
                                                                        </td>

                                                                        <td>
                                                                            <b>VAT $</b>
                                                                        </td>
                                                                        <td>
                                                                         <asp:Label ID="lblVatPrev" runat="server"  Text="" Visible="false"></asp:Label>
                                                                         <asp:TextBox ID="txtVat" runat="server" Width="90%" Text="" TabIndex="6"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Expense Detail</b>
                                                                        </td>
                                                                        <td colspan="3"><asp:TextBox ID="txtExpDetail" runat="server" Width="97%" Text="" TabIndex="7"></asp:TextBox></td>
                                                                        <td>
                                                                            <b>Activity Description</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlActivityDescription" runat="server" Width="90%" TabIndex="8">
                                                                                <asp:ListItem>PM</asp:ListItem>
                                                                                <asp:ListItem>Marketing</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Third Party Name</b>
                                                                        </td>
                                                                        <td colspan="3">
                                                                            <asp:TextBox ID="txtThirdPartyName" runat="server" Width="97%" Text="" MaxLength="255" TabIndex="9"></asp:TextBox>
                                                                        </td>
                                                                        
                                                                        <td>
                                                                            <b>Third Party Reference No</b>
                                                                        </td>
                                                                        <td><asp:TextBox ID="txtThirdPartyInvoiceReference" runat="server" Width="90%"  Text="" MaxLength="255" TabIndex="10"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <b>Attached MUF Document</b>
                                                                        </td>
                                                                        <td colspan="5">
                                                                           <asp:FileUpload ID="fileUploadMuf" runat="server" />&nbsp;
                                                                           <asp:Label ID="lblmuffilePathEdt" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                      <td colspan="6">
                                                                         <table width="100%" border="0px">
                                                                           <tr>
                                                                               <td style="width:25%" align="center">Submitted By</td>
                                                                               <td style="width:25%" align="center">Verified By</td>
                                                                               <td style="width:25%" align="center">Accrued By</td>
                                                                               <td style="width:25%" align="center">Authorised By</td>
                                                                           </tr>
                                                                           <tr>
                                                                             <td align="center"><asp:Label ID="lblmufsubmittedByEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufsubmittedDateEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"submittedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufverfiedByEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufverfiedDateEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"verifiedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufaccruedByEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufaccruedDateEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"accruedDate") %>'></asp:Label></td>
                                                                             <td align="center"><asp:Label ID="lblmufAuthorisedByEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedBy") %>'></asp:Label>&nbsp;--&nbsp;<asp:Label ID="lblmufAuthorisedDateEdt" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem,"authorisedDate") %>'></asp:Label></td>
                                                                           </tr>
                                                                         </table>
                                                                      </td>
                                                                    </tr>
                                                                </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlEditItemTemplateChild" runat="server" Visible="false">
                                                                <table width="100%" border="0px">
                                                                    <tr>
                                                                        <td style="width:14%">
                                                                            <b>Recieved In RDD Qtr</b>
                                                                        </td>
                                                                        <td style="width:18%"><asp:TextBox ID="txtRecievedInRDDQtr" runat="server" Width="90%" Text="" TextMode="SingleLine" MaxLength="50" TabIndex="11"></asp:TextBox></td>

                                                                        <td style="width:15%">
                                                                            <b>RDD Invoice No</b>
                                                                        </td>
                                                                        <td style="width:18%"><asp:TextBox ID="txtRddInvoiceNo" runat="server" Width="90%" Text="" MaxLength="250" TabIndex="12"></asp:TextBox></td>
                                                        
                                                                        <td style="width:15%">
                                                                            <b>RDD Paid Status</b>
                                                                        </td>
                                                                        <td style="width:20%">
                                                                            <asp:DropDownList ID="ddlRddPaidStatus" runat="server" Width="90%" TabIndex="13">
                                                                                <asp:ListItem Selected="True">Not Paid</asp:ListItem>
                                                                                <asp:ListItem >Paid</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Total Payment Received</b>
                                                                        </td>
                                                                        <td><asp:TextBox ID="txtTotalPaymentRecieved" runat="server" Width="90%" Text="" TabIndex="14"></asp:TextBox></td>

                                                                        <td>
                                                                            <b>Third Party Paid Status</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlThirdPartyPaidStatus" runat="server" Width="72%" TabIndex="15">
                                                                                <asp:ListItem Selected="True">Not Paid</asp:ListItem>
                                                                                <asp:ListItem >Paid</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td>
                                                                            <b>Third Party Invoice No.</b>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtThirdPartyInvoiceNo" runat="server" Width="90%" Text="" MaxLength="250" TabIndex="16"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>RDD Payment Details</b>
                                                                        </td>
                                                                        <td colspan="5">
                                                                            <asp:TextBox ID="txtRddPaymentDetails" runat="server"  Width="98%"  Text="" TabIndex="17"></asp:TextBox>
                                                                            <asp:TextBox ID="txtUniqueID" runat="server" Width="98%" Visible="false" Text=""></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                            
                               <asp:TemplateField>
                                 <ItemTemplate>
                                         <asp:LinkButton ID="LinkEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" ></asp:LinkButton>
                                   &nbsp;<asp:LinkButton ID="LinkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton><br />
                                         <asp:Label ID="lblMUFStatus" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"mufFormFilledStatus") %>' Visible="false" Font-Bold="true" ForeColor="Red" ></asp:Label>
                                         <asp:Label ID="lblMUFLable" runat="server"  Text="MUF Done" Visible="false" Font-Bold="true" ForeColor="#009933" ></asp:Label>
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                           <asp:LinkButton ID="LinkUpd" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                                     &nbsp;<asp:LinkButton ID="LinkCanc" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                         
                                 </EditItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" />
                                 <ItemStyle Width="100px" />
                              </asp:TemplateField>
                              
                                                                      
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
            </asp:Panel>
                    </td>
                </tr>

                <tr>
                    <td style="width:100%">
                        <asp:Panel ID="panelIFF" runat="server" CssClass="PlansPanel" GroupingText="IFF Form" >
               <table id="Table1" runat="server" width="100%" style="border-style: solid; border-width: 0px; padding: 0px; font-size: 10px;" bgcolor="#DFEFFF">
                  <tr>
                    <td style="width:15%" align="left">Invoice Dated :</td>
                    <td style="width:85%"><asp:TextBox ID="txtInvDate" Width="100px"  MaxLength="12" runat="server"></asp:TextBox>&nbsp;(MM-dd-yyyy)</td>
                  </tr>
                  <tr>
                    <td style="width:15%" align="left">Billed To :</td>
                    <td style="width:85%"><asp:TextBox ID="txtBilledTo" Width="700px"  MaxLength="254" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td style="width:15%" align="left">Address Line1:</td>
                    <td style="width:85%"><asp:TextBox ID="txtAdd1" Width="700px"  MaxLength="254" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td style="width:15%" align="left">Address Line2 :</td>
                    <td style="width:85%"><asp:TextBox ID="txtAdd2" Width="700px"  MaxLength="254" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td style="width:15%" align="left">Invoice Detail :</td>
                    <td style="width:85%"><asp:TextBox ID="txtInvDetail" Width="700px" MaxLength="254" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td colspan="2">Activity Detail<br /> 
                       <asp:GridView ID="GridViewIff" AutoGenerateColumns="False" runat="server"  CssClass="GridviewStyle" Visible="false" >
                          <HeaderStyle Font-Bold="true"  HorizontalAlign="Center" Height="40px"/>
                            <Columns>
                             <asp:TemplateField HeaderText="autoindex">
                                 <ItemTemplate> 
                                    <asp:Label ID="lblautoindex" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "autoindex")%>'></asp:Label>
                                 </ItemTemplate>
                                    <ItemStyle Width="40px" Font-Bold="true" />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Activity No.">
                                 <ItemTemplate> 
                                    <asp:Label ID="lblActivityCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActivityCode")%>'></asp:Label>
                                 </ItemTemplate>
                                    <ItemStyle Width="100px" Font-Bold="true" />
                              </asp:TemplateField>
                          
                              <asp:TemplateField HeaderText="AF No.">
                                 <ItemTemplate> 
                                    <asp:Label ID="lblAccrualFormNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AccrualFormNo")%>'></asp:Label>
                                 </ItemTemplate>
                                    <ItemStyle Width="100px" Font-Bold="true" />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Expense Detail">
                                 <ItemTemplate> 
                                    <asp:Label ID="lblExpenseDetail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpenseDetail")%>'></asp:Label>
                                 </ItemTemplate>
                                    <ItemStyle Width="300px" Font-Bold="true" />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Activity Date">
                                 <ItemTemplate> 
                                    <asp:TextBox ID="txtactivityProcessDate" Width="70px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"actualDateOfExecution","{0:MM-dd-yyyy}")%>' Enabled="False"></asp:TextBox>
                                 </ItemTemplate>
                                    <ItemStyle Width="70px" />
                              </asp:TemplateField>

                          
                              <asp:TemplateField HeaderText="Actual Cost">
                                 <ItemTemplate> 
                                     <%--<asp:TextBox ID="txtActivityActualCost" Width="70px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "actualActivityCostedNVat")%>' Enabled="False"></asp:TextBox>--%>
                                     <asp:TextBox ID="txtActivityActualCost" Width="70px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Costed")%>' Enabled="False"></asp:TextBox>
                                 </ItemTemplate>
                                    <ItemStyle Width="75px" Font-Bold="true" />
                              </asp:TemplateField>

                              <asp:TemplateField HeaderText="Vat">
                                 <ItemTemplate> 
                                     <asp:TextBox ID="txtVat" Width="50px" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Vat")%>' Enabled="False"></asp:TextBox>
                                 </ItemTemplate>
                                    <ItemStyle Width="75px" Font-Bold="true" />
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                    </td>
                  </tr>
                </table>
              </asp:Panel>
                  <asp:Label ID="lblIffFormHtml" runat="server" Text=""></asp:Label>
             </td>
           </tr>
                    
                <tr>
                    <td style="width:100%">
                        <asp:Panel ID="panelTasks" runat="server" CssClass="PlansPanel" GroupingText="Your Task List" >
                            Current Status :<asp:Label ID="lblStatusCopy" runat="server" Font-Bold="true" Text="" ForeColor="Blue"></asp:Label>
                            <br />

                              <asp:GridView ID="gridTaskList" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="100%" 
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="2px"
                                    
                                        DataKeyNames="autoindex"
                                        ShowHeaderWhenEmpty="True">
                                      <Columns>
                                        <asp:TemplateField HeaderText="Check if Processed">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" Checked="false" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Task">
                                            <ItemTemplate>
                                               <asp:Label ID="lblTask" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "task")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="90%" />
                                        </asp:TemplateField>
                                    </Columns>
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                        <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Right" Height="10px" />
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
         
                <tr>
                    <td style="width:100%">
                        <table id="tblTask" runat="server" width="100%" style="border-style: solid; border-width: 0px; padding: 0px; font-size: 10px;" bgcolor="#DFEFFF">
                            <tr>
                                <td style="width:80%">
                                    <b>Updatation Comments</b>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtupdtComments" Width="100%" MaxLength="254" runat="server" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                </td>
                                 <td align="right">
                                    <asp:Button ID="btnDecline" runat="server" Text="Decline" Font-Bold="True" Width="80px" onclick="btnDecline_Click" Visible="false"  CssClass="btnStyle"/>
                                    &nbsp;
                                    <asp:Button ID="btnSubmit" runat="server" Width="150px" Text="Submit is hidden" onclick="btnSubmit_Click" OnClientClick="disableBtn(this.id, 'Submitting...')"  UseSubmitBehavior="false" style="display:none" />
                                     <input id="btnSubmitHtml" type="button" value="Submit" onclick="ConfirmForUpdate();" class="btnStyle" onclick="return btnSubmitHtml_onclick()" />
                                 </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:ListBox ID="LstIntimation" runat="server" Height="100px" Width="80%" Visible="false"></asp:ListBox>
            <uc1:MsgBoxControl ID="MsgBoxControl1" runat="server" />
            <br />
            <br />
        </div>
            
        </ContentTemplate>

        <Triggers>
         <asp:PostBackTrigger ControlID = "btnSubmit" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
         <asp:PostBackTrigger ControlID="Grid2" />
        </Triggers>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:50%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>

</asp:Content>

