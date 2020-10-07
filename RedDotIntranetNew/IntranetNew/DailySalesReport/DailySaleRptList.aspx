<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="DailySaleRptList.aspx.cs" Inherits="IntranetNew_DailySalesReport_DailySaleRptList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <tr>
    <td>
    <panel>
<table width="70%" align="center" cellpadding="3px" cellspacing="3px">
<tr>
<td    width="10%">
</td>
<td    width="5%">

</td><td ></td>

<td    width="5%">

</td>
<td    width="10%">

</td>
</tr>
<tr>
                    <td   width="10%">
                        CLICK HERE:
                        </td>
                        <td    width="5%">
                        <asp:Button ID="btnexporttoex" runat="server" Text="EXPORT TO EXCEL" class="btn btn-success"
                            Font-Bold="true" OnClick="btnexporttoex_Click" />
                       
                    </td>
                 <td      width="2%"></td>
                <td width="5%"> 
                      
                     </td>
                     <td    width="10%">
                    
                    </td>
                    <td width="10%">
                    </td>
                   </tr>
                         <tr>
    <td> &nbsp; </ td>
</tr>
                   <tr>
                 <%--  <td    width="10%">

                   <label >Cutomer</label>
</td>
<td    width="5%"><asp:DropDownList ID="ddlcustomer" runat="server" AutoPostBack="true"></asp:DropDownList></td>
<td  width="2%">
</td>
--%>
 <td width="5%"> 
                       <label >Daily Sales Visit</label>
                     </td>
                     <td    width="10%">
                        
                        <asp:DropDownList ID="ddllist" runat="server" 
        AutoPostBack="true" BackColor="AliceBlue"
                            class="form-control" 
        onselectedindexchanged="ddllist_SelectedIndexChanged">
                           <asp:ListItem>--SELECT--</asp:ListItem>
                            <asp:ListItem>ALL</asp:ListItem>
                            <asp:ListItem>Deleted</asp:ListItem>
                             <asp:ListItem>Active</asp:ListItem>

                        </asp:DropDownList>
                    </td>
                 <td></td>
                   <td >  <b><label >From Date</label></b></td>
                   <td   width="10%"> <asp:TextBox ID="txtstartdate" runat="server" autocomplete="off" class="form-control" BackColor="AliceBlue"
                                            TabIndex="8" Style="width:85%"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" runat="server"
                                            TargetControlID="txtstartdate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender></td>

                   <td > <label >To Date</label></td>
                   <td  width="10%">
                    <asp:TextBox ID="txtEndDate" runat="server" Style="width: 84%" autocomplete="off" BackColor="AliceBlue"
                                            class="form-control" TabIndex="9"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup" runat="server" 
                                            TargetControlID="txtEndDate" Format="MM/dd/yyyy">
                                        </cc1:CalendarExtender>
                   </td>
                   </tr>
                   <tr> 
                   <td  align="center">
                   
                   <asp:Button ID="btnshow" runat="server" Text="DISPLAY" onclick="btnshow_Click" BorderColor=Red Style="width:30%"></asp:Button></td>
                   </tr>
</table>
      <tr>
    <td width="100%" > &nbsp; </ td>
</tr>
</panel>

</td>
</tr>
      <tr>
    <td width="100%" > &nbsp; </ td>
</tr>
      <tr>
    <td width="100%" > &nbsp; </ td>
</tr>
    <tr>
        <td width="100%">
            <asp:GridView ID="Gvdata" runat="server" AutoGenerateColumns="False" Width="100%"
                CellPadding="3" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
                BorderWidth="1px" CellSpacing="2" PageSize="40" OnPageIndexChanging="Gvdata_PageIndexChanging"
                AllowPaging="True">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblsaleid" runat="server" Text='<%#Eval("VisitId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Visit Date">
                        <ItemTemplate>
                            <asp:Label ID="lblvisitdate" runat="server" Text='<%#Eval("VisitDate","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type Of Visit">
                        <ItemTemplate>
                            <asp:Label ID="lblviittype" runat="server" Text='<%#Eval("VisitType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company Name">
                        <ItemTemplate>
                            <asp:Label ID="lblcomname" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Person Meet">
                        <ItemTemplate>
                            <asp:Label ID="lblpersonmeet" runat="server" Text='<%#Eval("PersonMet") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Discussion">
                        <ItemTemplate>
                            <asp:Label ID="lbldiscussion" runat="server" Text='<%#Eval("Discussion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("Email") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblcontact" runat="server" Text='<%#Eval("ContactNo") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblbu" runat="server" Text='<%#Eval("BU") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblecallstatus" runat="server" Text='<%#Eval("CallStatus") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblfeedback" runat="server" Text='<%#Eval("Feedback") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblforwardcall" runat="server" Text='<%#Eval("ForwardCallToEmail") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblfwdrmk" runat="server" Text='<%#Eval("ForwardRemark") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Priority">
                        <ItemTemplate>
                            <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("Priority") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="ActionDone">
                        <ItemTemplate>
                            <asp:Label ID="lblactiondone" runat="server" Text='<%#Eval("Discussion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--     <asp:TemplateField HeaderText="NextAction">
                                <ItemTemplate>
                                    <asp:Label ID="lblnextaction" runat="server" Text='<%#Eval("NextAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbldraft" runat="server" Text='<%#Eval("IsDraft")%>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblexpebussiness" runat="server" Text='<%#Eval("ExpectedBusinessAmt") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--   <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblnewpartner" runat="server" Text='<%#Eval("NewPartner") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                  <%--  <asp:TemplateField HeaderText="NextReminderDate">
                        <ItemTemplate>
                            <asp:Label ID="lblnextremin" runat="server" Text='<%#Eval("NextReminderDate","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
        </td>
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </tr>
    </br>
</asp:Content>
