<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="DealerManagement.aspx.cs" Inherits="Intranet_EVO_DealerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title">Dealer Management for Marketing</div>
                </td>
             </tr>
    </table>

    <div>
      <center>
        <asp:Panel ID="Panel1" runat="server" BorderColor="#006666" BorderStyle="Solid">
            <table width="80%" border="0px">
                <tr>
                    <td colspan="5">
                        <b> Dealer Add/Edit Section</b>
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
                       <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                       <h3> <asp:Label ID="lblAddEdit" runat="server" Text="" Font-Bold="true" ForeColor="#333399"></asp:Label></h3>
                    </td>
                </tr>
            
            
                <tr style="height:30px;">
                    <td valign="top">
                        Country :-
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlCountry" runat="server" Width="100px" BackColor="Gray" >
                        </asp:DropDownList>&nbsp;
                        <asp:Label ID="lblCntryCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Pink"></asp:Label>
                    </td>
                    <td>
                        &nbsp;<asp:LinkButton ID="lnkNewCountry" runat="server" Text="New Country" 
                            ForeColor="#993399" Font-Bold="false" onclick="lnkNewCountry_Click"></asp:LinkButton>
                    </td>
                    <td>
                      <asp:Panel ID="PanelCountry" runat="server" GroupingText="ADD New Country" Font-Bold="true" Visible="false">
                                    <table width="100%">
                                        <tr>
                                            <td align="left" valign="bottom" colspan="2">
                                                <asp:TextBox ID="txtNewCountry" runat="server" Width="150px" BackColor="#d9ffd9"></asp:TextBox>
                                            </td>
                                         </tr>
                                         <tr>
                                            <td>
                                                <asp:Button ID="btnSaveCountry" runat="server" Text="Save" 
                                                    onclick="btnSaveCountry_Click"/>
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
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td colspan="5">
                        &nbsp;
                    </td>
                </tr>

                <tr>
                    <td>
                        Contact Person:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtContactPerson" MaxLength="250" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td>
                        E-mail 1:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail1" MaxLength="250" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td>Dealer ID : &nbsp;<asp:Label ID="lblEditDealerId" runat="server" Text="-1" Visible="true"></asp:Label></td>
                </tr>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <%--<asp:Label ID="lblErr1stEsclate" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                    </td>
                    <td>
                    
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter A Valid Email ID" ControlToValidate="txtEmail1" Font-Bold="True" ForeColor="#3366CC" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                        <asp:Label ID="lblErr1stEmail" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>
                        Company:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtCompany" MaxLength="250" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td>
                        E-mail 2:-
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail2" MaxLength="250" runat="server" Width="95%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <%--<asp:Label ID="lblErr2ndEsclate" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>--%>
                    </td>
                    <td>
                    
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please Enter A Valid Email ID" ControlToValidate="txtEmail2" Font-Bold="True" ForeColor="#3366CC" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                        <asp:Label ID="lblErr2ndEmail" runat="server" ForeColor="#3366CC" Font-Bold="True"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                        <td style="width:10%">Phone :</td>
                        <td style="width:15%"><asp:TextBox ID="txtPh" MaxLength="15" runat="server" Width="95%"></asp:TextBox></td>
                        <td style="width:10%">Cell :</td>
                        <td style="width:20%"><asp:TextBox ID="txtCell" MaxLength="15" runat="server" Width="95%"></asp:TextBox></td>
                        <td style="width:45%">
                        <asp:Button ID="btnSave" runat="server" Text="Save New" width="80px" onclick="btnSave_Click" 
                             />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" width="80px" 
                             Enabled="true" onclick="btnCancel_Click" />
                        </td>
                </tr>
            </table>
        <br />
        
        <table width="80%" border="0px">
                <tr>
                    <td colspan="5">
                        <b> Registered Dealer's List From the database ( <asp:Label ID="lblDealersCnt" runat="server" Text="0" Font-Bold="true" ForeColor="Blue"></asp:Label> ) </b>
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
                                    <asp:Label ID="lblDealerId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DealerId")%>'></asp:Label>&nbsp;
                                    <asp:Label ID="lblfk_CountryID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "fk_CountryID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle  CssClass="lighterData" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Country" SortExpression="Country">
                                <ItemTemplate > 
                                  <asp:Label ID="lblCountry" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Country")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>

                            <asp:TemplateField HeaderText="Contact Person" SortExpression="ContactPerson">
                                <ItemTemplate > 
                                  <asp:Label ID="lblContactPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ContactPerson")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                           </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
                                <ItemTemplate> 
                                  <asp:Label ID="lblCompanyName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Email" SortExpression="Email1">
                                <ItemTemplate> 
                                  <asp:Label ID="lblEmail1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email1")%>'></asp:Label>  
                                  &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Email2")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Call" SortExpression="Phone">
                                <ItemTemplate> 
                                  <asp:Label ID="lblPhone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phone")%>'></asp:Label>  
                                  &nbsp;<asp:Label ID="lblCell" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Cell")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="By User" SortExpression="byUserName">
                                <ItemTemplate> 
                                  <asp:Label ID="lblbyUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "byUserName")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Last Modified" SortExpression="ModifiedDate" Visible="true">
                                <ItemTemplate> 
                                  <asp:Label ID="lblModifiedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ModifiedDate","{0:MMM/dd/yyyy}")%>'></asp:Label>  
                                </ItemTemplate>
                                <ItemStyle CssClass="lighterData" HorizontalAlign="Left"/>
                            </asp:TemplateField>
                            
                           <asp:TemplateField HeaderText="Action" >
                                <ItemTemplate> 
                                    <asp:LinkButton ID="lnkEdit" Text="Edit" runat="server" OnClick="lnkEdit_Click"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="lnkDel" Text="Delete" runat="server" OnClick="lnkDel_Click" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="80px" />
                           </asp:TemplateField>

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
    </asp:UpdatePanel>
</asp:Content>

