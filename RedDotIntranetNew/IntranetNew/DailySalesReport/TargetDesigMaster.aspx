<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="TargetDesigMaster.aspx.cs" Inherits="IntranetNew_DesignationsTarget_TargetDesigMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
    function isNumberKey(evt) {
            debugger;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
                return true;
            } else {
                alert("Enter only numbers");
                return false;
            }
        }
//        function getConfirmationOnDelete() {
//            return confirm("Are you sure you want to Delete ?");
//        }
    </script>
    <asp:ScriptManager ID="SMAutoCLChangeAlert" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UPAutoCLStatusChangeAlert" runat="server">
        <ContentTemplate>
            <table width="100%" align="center">
                <caption>
                   -
                    <tr>
                        <td align="center" width="100%">
                            <lable style="color: #d71313; font-size: x-large; font-weight: bold; font-family: Raleway;">
                    &nbsp;&nbsp;&nbsp;  SETUP - Reporting Frequency & Targets
                </lable>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%">
                            <asp:Label ID="lblMsg" runat="server" Style="color: #d71313; font-size: 14px; font-weight: bold;
                                font-family: Raleway;" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%">
                            &nbsp;
                        </td>
                    </tr>
                    <%--        onsorting="GRVAutoCLStatusChange_Sorting"--%>
                    <tr>
                        <td align="center" width="80%">
                            <asp:Panel ID="pnlAutoCLStatus" runat="server" BorderColor="Red" BorderWidth="1px" DataKeys="TargetId"
                                EnableTheming="true" Height="25%" Width="70%">
                                <asp:GridView ID="GRVAutoCLStatusChange" runat="server" AllowSorting="true" OnDataBound="OnDataBound"  
                                 onrowediting="GRVAutoCLStatusChange_RowEditing"  
                                   onrowcancelingedit="GRVAutoCLStatusChange_RowCancelingEdit"
                                   onrowdeleting="GRVAutoCLStatusChange_RowDeleting" 
                                   onsorting="GRVAutoCLStatusChange_Sorting"
                                    AutoGenerateColumns="False" 
                                    OnRowCommand="GRVAutoCLStatusChange_RowCommand" Class="table table-bordered table-condensed table-hover"
                                    ShowFooter="True" onrowdatabound="GRVAutoCLStatusChange_RowDataBound">
                                  
                                    <Columns>
                                     
                              <asp:TemplateField HeaderText="SR.NO" SortExpression="TargetId" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="Small" >
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("TargetId")%>' Visible="false"></asp:Label>
                                     <%--  <asp:Label ID="lbldesigid" runat="server" Text='<%#Eval("DesigId")%>' Visible="false" ></asp:Label>--%>
                                    <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField  Visible="false" ItemStyle-HorizontalAlign="center" >
                                            <ItemTemplate >
                                                <asp:Label ID="lbldesigid" runat="server" Text='<%#Eval("DesigId")%>' Visible="true"  ></asp:Label>
                                               
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="ddldesiidEdit" runat="server"  Visible="true" Text='<%#Eval("DesigId")%>'></asp:TextBox>
                                                   
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                             <asp:TextBox ID="ddldesiid" runat="server"  Visible="false"></asp:TextBox>
                                             
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="DESIGNATION" SortExpression="DESIGNATION" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("DesigName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:Label ID="lbldesigName" runat="server" Text='<%#Eval("DesigName")%>' Visible="false"></asp:Label>
                                                     <asp:DropDownList ID="ddldesiEdit" runat="server" BackColor="White" BorderColor="Brown"  
                                                    class="form-control" Style="font-size: small;" Width="101%"></asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                    <asp:DropDownList ID="ddldesi" runat="server" BackColor="White" BorderColor="Brown" TabIndex="1"
                                                    class="form-control" Style="font-size: small;" Width="101%"></asp:DropDownList>                                   
                                                
                                            </FooterTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>


                                             <asp:TemplateField HeaderText="CANVIEW SCORE" SortExpression="VIEW SCORE" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkcanview" runat="server"  Style="font-size: small;" Enabled="false"  Checked='<%#Eval("ViewScore")%>'   />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:CheckBox ID="chkedit" runat="server"  Style="font-size: small;"  Width="80%"   class="form-control"  Checked='<%#Eval("ViewScore")%>'   BackColor="White" BorderColor="Brown"  />      
                                            </EditItemTemplate>
                                           <FooterTemplate>
                                                   <asp:CheckBox ID="chkfooter" runat="server"  Style="font-size: small;"   Width="80%"  class="form-control"    BackColor="White" BorderColor="Brown" TabIndex="2" />                                                                   
                                            </FooterTemplate>
                                          <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderStyle-Width="15%" HeaderText="FREQ OF RPT" SortExpression="FREQ OF RPT" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfreqRpt" runat="server" Text='<%#Eval("FreqOfRpt")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlFrqRPtEdit" runat="server" BackColor="White" BorderColor="Brown" Text='<%#Eval("FreqOfRpt")%>' 
                                                    class="form-control" Style="font-size: small;" Width="90%">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>DAILY</asp:ListItem>
                                                    <asp:ListItem>WEEKLY</asp:ListItem>
                                                    <%--<asp:ListItem>FORTHNIGHT</asp:ListItem>--%>
                                                    <asp:ListItem>MONTHLY</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlFreqRpt" runat="server" BackColor="White" BorderColor="Brown"  TabIndex="3"
                                                    class="form-control" Style="font-size: small;" Width="90%">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                    <asp:ListItem>DAILY</asp:ListItem>
                                                    <asp:ListItem>WEEKLY</asp:ListItem>
                                                    <%--<asp:ListItem>FORTHNIGHT</asp:ListItem>--%>
                                                    <asp:ListItem>MONTHLY</asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="8%" HeaderText="DAILY" SortExpression="MIN PER DAY"  HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblminperday" runat="server" Text='<%#Eval("DailyTarget")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtminperdayEdit" runat="server" Text='<%#Eval("DailyTarget")%>' Width="80%"  MaxLength="2"
                                                    BackColor="White" BorderColor="Brown" class="form-control" Style="font-size: small;"  
                                                    onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtminperday" runat="server" BackColor="White" BorderColor="Brown"  MaxLength="2"
                                                   Width="80%" class="form-control" autocomplete="off" Style="font-size: small;"   placeHolder="0"  TabIndex="4"
                                                    onkeypress="javascript:return isNumberKey(event);">                                          
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="WEEK" SortExpression="WEEKLY" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblweek" runat="server" Text='<%#Eval("WeeklyTarget")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtweekEdit" runat="server" Text='<%#Eval("WeeklyTarget")%>' MaxLength="2"
                                                    BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"
                                                    class="form-control" Style="font-size: small;" Width="80%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtweek" runat="server" class="form-control" MaxLength="2"  TabIndex="5" Width="80%"
                                                    onkeypress="javascript:return isNumberKey(event);" BackColor="White" BorderColor="Brown"   placeHolder="0" 
                                                    Style="font-size: small;" autocomplete="off">
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="MONTHLY" SortExpression="MONTHLY TARGET" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthly" runat="server" Text='<%#Eval("MonthlyTarget")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtmonEdit" Width="80%" runat="server" Text='<%#Eval("MonthlyTarget")%>' MaxLength="3"
                                                    BackColor="White" BorderColor="Brown" class="form-control" Style="font-size: small;"  onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtmon" runat="server" Width="80%" BackColor="White" BorderColor="Brown"   placeHolder="0"  MaxLength="3" 
                                                    TabIndex="6" autocomplete="off" class="form-control" Style="font-size: small;"  onkeypress="javascript:return isNumberKey(event);">
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="DISTINCT" SortExpression="DISTINCT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBU" runat="server" Text='<%#Eval("DistinctTarget")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDistinctedit" Width="80%" runat="server" Text='<%#Eval("DistinctTarget")%>' MaxLength="3"
                                                    BackColor="White" BorderColor="Brown" class="form-control" Style="font-size: small;"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDistinct" Width="80%" runat="server" BackColor="White" BorderColor="Brown" MaxLength="3"  placeHolder="0" TabIndex="7"
                                                    class="form-control" Style="font-size: small;" ></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="6%" HeaderText="REPEAT" SortExpression="REPEAT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Small">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRptEdit" runat="server" Text='<%#Eval("RepeatTarget")%>' Enabled="false" ></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRptEdit" Width="80%" runat="server" Text='<%#Eval("RepeatTarget")%>' MaxLength="3" 
                                                    BackColor="White" BorderColor="Brown" class="form-control" Style="font-size: small;"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRpt" Width="80%" runat="server" BackColor="White" BorderColor="Brown" MaxLength="3"   placeHolder="0"
                                                    class="form-control" Style="font-size: small;" TabIndex="8"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Width="7%" HeaderText="NEW" SortExpression="NEW CUSTOMER" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="Small">
                                           <HeaderTemplate>
            <asp:Label ID="lblcustomer1"  runat="server"></asp:Label> <asp:ImageButton ID="btn1" runat="server" ImageUrl="~/outer css-js/images/customer.png"  Enabled="false" Width="20px" ToolTip="NEW CUSTOMER"/>
              <asp:Label ID="Label1"  runat="server"></asp:Label> <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/outer css-js/images/cutomer1.png"  Enabled="false" Width="22px" Height="20px" ToolTip="NEW CUSTOMER"/>
        </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcustomer" runat="server" Text='<%#Eval("NewCustomers")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtcusedit" Width="85%" runat="server" Text='<%#Eval("NewCustomers")%>' MaxLength="2"
                                                 BackColor="White" BorderColor="Brown" class="form-control" Style="font-size: small;"  onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtcus" runat="server" Width="85%" class="form-control" TabIndex="9"   placeHolder="0"  MaxLength="2"
                                                    BackColor="White" BorderColor="Brown" autocomplete="off" Style="font-size: small;"  onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField SortExpression="ACTION" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                       
                                                <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" class="btn btn-info" ></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="BtnDelete" runat="server" CausesValidation="false" CommandName="Delete"     OnClientClick="return confirm('Are you sure you want delete');"
                                                    Text="Delete" class="btn btn-danger" ></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="False" CommandName="Update"
                                                    Text="Update" class="btn btn-success"></asp:LinkButton>
                                                &nbsp;<asp:LinkButton ID="BtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancel" class="btn btn-default"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btInsert" runat="server" Text="Add New" Class="btn btn-success" CommandName="Add"  TabIndex="10"/>
                                            </FooterTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </caption>
  
           </table>
 </ContentTemplate>
</asp:UpdatePanel>   
</asp:Content>
