<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="MailShotManagement.aspx.cs" Inherits="Intranet_EVO_MailShotManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="../../MsgBoxControl.ascx" tagname="MsgBoxControl" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="javascript" type="text/javascript">

    function ConfirmForUpdate() {

        var ans = confirm('Are you sure you want to perfom this action ????');

        if (ans) {
            document.getElementById("btnSubmitHtml").style.visibility = 'hidden';
            document.getElementById('<%= btnSave.ClientID %>').style.display = 'inherit';
            document.getElementById('<%= btnSave.ClientID %>').click();
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
  </script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title">MailShot Management for Marketing</div>
                </td>
             </tr>
    </table>

    <div>
      <center>
        <asp:Panel ID="Panel1" runat="server" BorderColor="#006666" BorderStyle="Solid">
            <table width="90%" border="0px">
                <tr>
                    <td colspan="5">
                        <b>Send MailShot Section</b>
                        <hr />
                    </td>
                </tr>
                <tr>
                        <td style="width:10%">&nbsp;</td>
                        <td style="width:15%">&nbsp;</td>
                        <td style="width:15%">&nbsp;</td>
                        <td style="width:20%">&nbsp;</td>
                        <td style="width:40%">&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="5">
                       <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                       <%--<h3> <asp:Label ID="lblAddEdit" runat="server" Text="" Font-Bold="true" ForeColor="#333399"></asp:Label></h3>--%>
                        
                    </td>
                </tr>
            
            
                <tr style="height:30px;">
                    <td valign="top">
                        Country :-
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="100px" BackColor="Gray" 
                            AutoPostBack="true" onselectedindexchanged="ddlCountry_SelectedIndexChanged" >
                        </asp:DropDownList>&nbsp;
                        <asp:Label ID="lblCntryCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Pink"></asp:Label>
                    </td>
                    <td valign="top">
                       BU :- &nbsp;<asp:DropDownList ID="ddlBu" runat="server" Width="80px" BackColor="Gray" ></asp:DropDownList>&nbsp;
                        <asp:Label ID="lblBuCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Pink"></asp:Label>
                    </td>
                    <td valign="top">
                         Purpose :- &nbsp;<asp:DropDownList ID="ddlPurpose" runat="server" Width="110px" BackColor="Gray" ></asp:DropDownList>&nbsp;
                        <%--<asp:Label ID="lblPurpose" runat="server" Text="0" Font-Bold="true" ForeColor="Pink"></asp:Label>--%>
                    </td>
                    <td >
                        Phone :- &nbsp; <asp:TextBox ID="txtPhone" runat="server" Width="120px" MaxLength="250"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlCountryIntimationMailId" runat="server" Width="100px" BackColor="Gray" Visible="false">
                        </asp:DropDownList>&nbsp;
                    </td>
                    <td>
                        &nbsp;<asp:LinkButton ID="lnkNewBU" runat="server" Text="New BU" 
                            ForeColor="#993399" Font-Bold="false" onclick="lnkNewBU_Click"></asp:LinkButton>
                    </td>
                    <td>
                      <asp:Panel ID="PanelBU" runat="server" GroupingText="ADD New BU" Font-Bold="true" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" valign="bottom" colspan="2">
                                                <asp:TextBox ID="txtNewBU" runat="server" Width="150px" BackColor="#d9ffd9"></asp:TextBox>
                                            </td>
                                         </tr>
                                         <tr>
                                            <td>
                                                <asp:Button ID="btnSaveBU" runat="server" Text="Save" 
                                                    onclick="btnSaveBU_Click"/>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgBtnClose" runat="server" 
                                                    ImageUrl="~/images/close-icon.png" ToolTip="Close This Panel..." 
                                                    onclick="imgBtnClose_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        Subject (max 250)
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtSubject" runat="server" Width="95%" MaxLength="250"></asp:TextBox>
                    </td>
                 </tr>

                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>

                <tr style="height:100px">
                    <td>
                        Message (max 4000)
                    </td>
                    <td colspan="4">
                        <asp:TextBox ID="txtMsg" runat="server" Width="95%" Height="90px" MaxLength="4000" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                        <td style="width:10%">MailShot File</td>
                        <td style="width:15%">
                           <asp:FileUpload ID="FileUpload1" runat="server" />
                           &nbsp;<asp:Label ID="lblFilePth" runat="server" Text=""></asp:Label>
                         </td>
                        <td style="width:10%"></td>
                        <td style="width:20%">
                           <%--<asp:Button ID="btnSave" runat="server" Text="Save & Send" width="120px" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;--%>

                           <asp:Button ID="btnSave" runat="server" Width="150px" Text="Submit is hidden" onclick="btnSave_Click" OnClientClick="disableBtn(this.id, 'Submitting...')"  UseSubmitBehavior="false" style="display:none" />
                           <input id="btnSubmitHtml" type="button" value="Save & Send" onclick="ConfirmForUpdate();" class="btnStyle" />

                           <asp:Button ID="btnCancel" runat="server" Text="Cancel" width="80px" Enabled="true" OnClick="btnCancel_Click" />
                        </td>
                        <td style="width:45%">
                                <b>MailShot will be sent to below emails of ( 
                              <asp:Label ID="lblDealerCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Red"></asp:Label> 
                              ) registered dealers for 
                              <asp:Label ID="lblSelectedCntry1" runat="server" Text="None" Font-Bold="true" ForeColor="Pink"></asp:Label>
                              .
                              </b>
                             &nbsp;
                             <asp:LinkButton ID="lnkRegDealer" runat="server" Text="Register Dealer" PostBackUrl="~/Intranet/Marketing/DealerManagement.aspx"></asp:LinkButton>
                        </td>
                </tr>
                <tr >
                    <td colspan="4">
                     &nbsp;
                    </td>
                     <td>
                         <asp:BulletedList ID="lstEmails" runat="server" ForeColor="#cc00ff">
                         </asp:BulletedList>
                     </td>
                </tr>
               
            </table>
        <br />
        
        <table width="90%" border="0px">
                <tr>
                    <td colspan="5">
                        <b>MailShot History from the database for (<asp:Label ID="lblSelectedCntry" runat="server" Text="None" Font-Bold="true" ForeColor="Pink"></asp:Label>)  : ( <asp:Label ID="lblListCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Blue"></asp:Label> ) </b>
                        <hr />
                    </td>
                </tr>
                <tr>
                        <td style="width:10%">&nbsp;</td>
                        <td style="width:15%">&nbsp;</td>
                        <td style="width:10%">&nbsp;</td>
                        <td style="width:20%">&nbsp;</td>
                        <td style="width:45%">&nbsp;</td>
                </tr>
                
                <tr>
                    <td colspan="5">
                        <asp:GridView ID="Grid1" AutoGenerateColumns="False" CssClass="overallGrid" 
                        runat="server" AllowSorting="True" 
                        CellSpacing="2" 
                        AllowPaging="True" PageSize="30"  Width="95%"
                        OnPageIndexChanging="Grid1_PageIndexChanging" 
                        OnSorting="Grid1_Sorting" 
                        >
                        
                        
                        <FooterStyle CssClass="footerGrid" />
                        <RowStyle CssClass="DataCellGrid" HorizontalAlign="right" />
                        <PagerStyle CssClass="pagerGrid" />
                        <SelectedRowStyle BackColor="#3366FF" />
                        <HeaderStyle CssClass="GrdHdr" Font-Underline="true"  />
                        <AlternatingRowStyle CssClass="DataCellGridAlt" />
                        <EditRowStyle BackColor="LightGreen" CssClass="DataCellGridEdit" />
                        
                        <Columns>
                            <asp:TemplateField HeaderText="ID" SortExpression="scId" Visible="false">
                                <ItemTemplate > 
                                    <asp:Label ID="lblmailShotID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mailShotID")%>'></asp:Label>&nbsp;
                                    <%--<asp:Label ID="lblfk_CountryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_CountryID")%>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle  CssClass="lighterData" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="For Country" SortExpression="Country">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"country")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="BU" SortExpression="BU">
                                <ItemTemplate > 
                                  <asp:Label ID="lblBU" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"BU")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="Purpose" SortExpression="Purpose">
                                <ItemTemplate > 
                                  <asp:Label ID="lblPurpose" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Purpose")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subject" SortExpression="Subject">
                                <ItemTemplate > 
                                  <asp:Label ID="lblSubject" runat="server" Width="200px" Text='<%# DataBinder.Eval(Container.DataItem,"subject")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Message" SortExpression="Msg">
                                <ItemTemplate> 
                                  <asp:Label ID="lblMsg" runat="server" Width="450px" Text='<%# DataBinder.Eval(Container.DataItem, "msg")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Phone" SortExpression="phone">
                                <ItemTemplate> 
                                  <asp:Label ID="lblphone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"phone")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="File" SortExpression="filepath" Visible="false">
                                <ItemTemplate> 
                                  <asp:Label ID="lblEmail1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"filepath")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="By User" SortExpression="byUserName">
                                <ItemTemplate> 
                                  <asp:Label ID="lblbyUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "byUserName")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mail Status" SortExpression="mailSentStatus">
                                <ItemTemplate> 
                                  <asp:Label ID="lblmailSentStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "mailSentStatus")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sent On" SortExpression="mailingDate" Visible="true">
                                <ItemTemplate> 
                                  <asp:Label ID="lblmailingDate" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem,"mailingDate","{0:MMM/dd/yyyy hh:mm:ss tt}")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"/>
                            </asp:TemplateField>
                            
                           <%--<asp:TemplateField HeaderText="Action" >
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" OnClick="lnkEdit_Click"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lnkDel" Text="Delete" runat="server" OnClick="lnkDel_Click" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                           </asp:TemplateField>--%>

                        </Columns>
                        
                    </asp:GridView>
                     

                    </td>
                </tr>

                <tr>
                    <td colspan="5"> &nbsp; </td>
                </tr>
        </table>

        </asp:Panel>
        </center>
    </div>
   
   </ContentTemplate>

        <Triggers>
         <asp:PostBackTrigger ControlID = "btnSave" />  <%--this need to be there for fullpostback in case we want fileupload control to be working--%>
        </Triggers>

    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div style="top:50%;left:30%;width:150px;height:80px;position:absolute;opacity:0.3;background-color:Gray;text-align:center" ><asp:Image ID="Image1" runat="server" ImageUrl="~/images/loadingImg.gif" /></div>
            </ProgressTemplate>
        </asp:UpdateProgress>
</asp:Content>

