<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="ViewReportNew.aspx.cs" Inherits="IntranetNew_DailySalesReport_ViewReportNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.2.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.0/js/bootstrap.min.js"
        type="text/javascript"></script>
    <script src='https://kit.fontawesome.com/a076d05399.js' type="text/javascript"></script>
    <style type="text/css">
        .Background
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .Popup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 1500px;
            height: 700px;
        }
        .lbl
        {
            font-size: 16px;
            font-style: italic;
            font-weight: bold;
        }
    </style>
    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
    <tr>
        <td>
            <asp:RadioButton ID="rdbUnread" runat="server" Checked="True" ForeColor="Red" AutoPostBack="true"
                GroupName="record" />
            <b>
                <asp:Label ID="Label1" runat="server" Text="UNREAD RECORDS" ForeColor="Red"></asp:Label>
            </b>(<asp:Label ID="lblunread" runat="server" Text="" Font-Size="Large" ForeColor="Red"
                Checked="True"></asp:Label>)
            <asp:RadioButton ID="rdbRead" runat="server" GroupName="record" AutoPostBack="true" />
            <b>
                <asp:Label ID="Label2" runat="server" Text="READ RECORDS" ForeColor="green"></asp:Label>
            </b>
        </td>
    </tr>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:GridView ID="GvdisplayRecord" runat="server" AutoGenerateColumns="false" RowStyle-BorderColor="Red"
                RowStyle-BorderWidth="1" class="rounded_corners" Font-Bold="True" HeaderStyle-BackColor="#d71313"
                HeaderStyle-ForeColor="Black" RowStyle-BackColor="#A1DCF2" RowStyle-ForeColor="#3A3A3A"
                Width="20%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" 
                CellPadding="4" PageSize="25">
                <Columns>
                    <asp:BoundField DataField="alias" HeaderText="Employee" HtmlEncode="true" 
                        ItemStyle-Width="2%" >
                    <ItemStyle Width="2%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="VisitDate">
                        <ItemTemplate>
                            <%--   <asp:Label ID="lblid" runat="server" Text='<%#Eval("VisitId")%>' Visible="false"></asp:Label>--%>
                            <%-- <asp:Label ID="lblalias" runat="server" Text='<%#Eval("alias")%>' Visible="false"></asp:Label>--%>
                            <asp:Label ID="lblvisitdate" runat="server" Text='<%#Eval("VisitDate","{0:MM/dd/yyyy}")%>'
                                Visible="false"></asp:Label>
                            <asp:LinkButton ID="lnkEdit" runat="server" Text='<%#Eval("VisitDate","{0:dd/MM/yyyy}")%>'
                                OnClick="Edit"></asp:LinkButton>
                            <asp:Label ID="lblcreatedby" runat="server" Text='<%#Eval("CreatedBy")%>' Visible="false"></asp:Label>
                            <asp:Label ID="lblisread" runat="server" Text='<%#Eval("IsRead")%>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="2%" />
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <HeaderStyle BackColor="#d71313" Font-Bold="True" ForeColor="#FFFFCC" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#330099" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>
            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="Popup" Style="display: none">
                <tr>
                    <td align="center" width="100%">
                        <asp:Label Font-Bold="true" ID="Label4" runat="server" Text="Company Wise Report"
                            Font-Size="Large" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <br />
                <table align="center">
                    <tr>
                        <td>
                            <asp:Label ID="lblmessage" runat="server" Text=""></asp:Label>
                            <asp:Panel ID="panel1" runat="server" Height="600px" Width="1450px" ScrollBars="Horizontal">
                                <asp:GridView ID="GvEmpDatewiseRpt" runat="server" AutoGenerateColumns="False" class="rounded_corners"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcountry" runat="server" Text='<%#Eval("Country")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%#Eval("ActualVisitDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call Mode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmodeofcall" runat="server" Text='<%#Eval("ModeOfCall")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Call Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcallstatus" runat="server" Text='<%#Eval("CallStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Company">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVisitId" runat="server" Text='<%#Eval("VisitId")%>' Visible="false"></asp:Label>
                                                <asp:Label ID="lblcom" runat="server" Text='<%#Eval("Company")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpersonmet" runat="server" Text='<%#Eval("PersonMet")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblphone" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BU">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbu" runat="server" Text='<%#Eval("BU")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="D/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDisRpt" runat="server" Text='<%#Eval("VisitType")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discussion">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldissc" runat="server" Text='<%#Eval("Discussion")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Biz($)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblbiz" runat="server" Text='<%#Eval("ExpectedBusinessAmt")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FeedBack">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfeedback" runat="server" Text='<%#Eval("feedback")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Forward call To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfrdcall" runat="server" Text='<%#Eval("ForwardCallToEmail")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reminder Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremidate" runat="server" Text='<%#Eval("ReminderDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reminder Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremidesc" runat="server" Text='<%#Eval("ForwardCallToEmail")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Next Action">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnxtaction" runat="server" Text='<%#Eval("ReminderDesc")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HOD Comments"  HeaderStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcomments" runat="server" class="form-control" Font-Size="Small" TextMode="MultiLine"
                                                    MaxLength="500" Text='<%#Eval("Comments")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="PM Comments"  HeaderStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPMcomments" runat="server" class="form-control" Font-Size="Small" TextMode="MultiLine"
                                                    MaxLength="500" Text='<%#Eval("PM_Comments")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                            </asp:Panel>
                        </td>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" runat="server" Text="Mark As Read" OnClick="Save" class="btn btn-success" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return Hidepopup()"
                                    class="btn btn-danger" />
                            </td>
                        </tr>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
            <cc1:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                BackgroundCssClass="Background" TargetControlID="lnkFake">
            </cc1:ModalPopupExtender>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GvdisplayRecord" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
    <style type="text/css">
        .header-center
        {
            text-align: center;
        }
        
        .myPanelClass
        {
            display: block;
            min-width: 200px;
            min-height: 200px;
            width: 800px;
            height: 600px;
        }
        
        
        .required
        {
            content: "*";
            font-weight: small;
            color: white;
        }
        
        
        
        .rounded_corners td, .rounded_corners th
        {
            border: 1px solid #de2a16;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        .rounded_corners table table td
        {
            border-style: none;
        }
        
        
        .style1
        {
            height: 20px;
        }
    </style>
</asp:Content>
