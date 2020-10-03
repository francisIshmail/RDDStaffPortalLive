<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="MarketingListData.aspx.cs" Inherits="IntranetNew_MarketingPlan_MarketingListData"
    %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>
    <asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">
        <ContentTemplate>--%>
        <panel>
            <table width="70%" align="center" cellpadding="3px" cellspacing="3px">
                <tr>
                    <td align="center" width="15%">
                        <label>
                            Country Name:
                        </label>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" BackColor="AliceBlue"
                            class="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="center" width="15%">
                        <label>
                            Status:</label>
                    </td>
                    <td width="20%">
                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" BackColor="AliceBlue"
                            OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged" class="form-control">
                            <asp:ListItem>--SELECT--</asp:ListItem>
                            <asp:ListItem>Pending</asp:ListItem>
                            <asp:ListItem>Approved</asp:ListItem>
                            <asp:ListItem>Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <%-- <asp:Button ID="btnsearch" runat="server" Text="SEARCH" class="btn btn-danger" 
                    onclick="btnsearch_Click" />--%>
                    </td>
                </tr>
                <asp:Label ID="lblMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" />
            </table></panel>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GvList" runat="server" AutoGenerateColumns="False" BorderWidth="1px"
                        Width="100%" CellPadding="5" CellSpacing="8" ForeColor="#333333" AutoGenerateSelectButton="True"
                        AllowPaging="True" PageSize="30" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                        OnPageIndexChanging="GvList_PageIndexChanging">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblplanid" runat="server" Text='<%#Eval("PlanId") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SRC fund">
                                <ItemTemplate>
                                    <asp:Label ID="lblsourceoffund" runat="server" Text='<%#Eval("SourceOfFund") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Country">
                                <ItemTemplate>
                                    <asp:Label ID="lblcountry" runat="server" Text='<%#Eval("CountryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblstartdate" runat="server" Text='<%#Eval("StartDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblenddate" runat="server" Text='<%#Eval("EndDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vendor/BU">
                                <ItemTemplate>
                                    <asp:Label ID="lblbu" runat="server" Text='<%#Eval("Vendor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="App Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblappamt" runat="server" Text='<%#Eval("VendorApprovedAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RDD App Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblrddappamt" runat="server" Text='<%#Eval("RDDApprovedAmt") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Used Amt">
                                <ItemTemplate>
                                    <asp:Label ID="lblusedamt" runat="server" Text='<%#Eval("UsedAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RDD BalAmt">
                                <ItemTemplate>
                                    <asp:Label ID="lanlbalamt" runat="server" Text='<%#Eval("BalanceAmount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="App BalAmt">
                                <ItemTemplate>
                                    <asp:Label ID="lblappbalamt" runat="server" Text='<%#Eval("BalanceFromApp") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Balance On Approved">
                        <ItemTemplate>
                            <asp:Label ID="lblbalonapp" runat="server" Text='<%#Eval("UsedAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Desc">
                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  <asp:TemplateField HeaderText="Approval Remark">
                        <ItemTemplate>
                            <asp:Label ID="lblapprmk" runat="server" Text='<%#Eval("ApproverRemark") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="AppBy">
                                <ItemTemplate>
                                    <asp:Label ID="lblappby" runat="server" Text='<%#Eval("ApprovedBy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PlanStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblplanstat" runat="server" Text='<%#Eval("planStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AppStatus">
                                <ItemTemplate>
                                    <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("ApprovalStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Create Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblcreatedate" runat="server" Text='<%#Eval("CreatedOn","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkedit" runat="server" onclick="lnkedit_Click">Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <table>
                <tr>
                    <td>
                        CLICK HERE:
                        <asp:Button ID="btnexporttoex" runat="server" Text="EXPORT TO EXCEL" class="btn btn-success"
                            Font-Bold="true" OnClick="btnexporttoex_Click" />
                       
                    </td>
                </tr>
            </table>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
