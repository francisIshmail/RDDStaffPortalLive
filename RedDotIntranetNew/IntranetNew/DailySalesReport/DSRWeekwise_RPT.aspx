<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="DSRWeekwise_RPT.aspx.cs" Inherits="IntranetNew_DailySalesReport_DSR_RPT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">


        $(function () {
            $('[id*=ddlMonth]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select',
                enableFiltering: true,
                buttonWidth: '110px'

            });
        });


        $(function () {
            $('[id*=ddlyear]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select',
                enableFiltering: false,
                buttonWidth: '110px'

            });
        });

        $(function () {
            $('[id*=ddlcountry]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select',
                enableFiltering: true,
                buttonWidth: '110px'

            });
        });

        $(function () {
            $('[id*=ddlemp]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select',
                enableFiltering: true,
                buttonWidth: '110px'

            });
        });


    </script>
    <tr>
        <td width="100%" align="center">
            <asp:Panel ID="pnlForms" runat="server" Width="65%" Height="60px" BorderWidth="1px"
                BorderColor="Red" EnableTheming="true">
                <table width="100%" align="center" cellpadding="3px" cellspacing="3px">
                    <caption>
                        <tr>
                            <td width="11%">
                            </td>
                           
                            <td align="center" style="color: #d71313;" width="10%">
                                <b>MM</b>
                            </td>
                            <td align="center" style="color: #d71313;" width="10%">
                                <b>YYYY</b>
                            </td>
                            <td align="left" style="color: #d71313;" width="10%">
                                <b>Select Country</b>
                            </td>
                            <td align="left" style="color: #d71313;" width="10%">
                                <b>Select Person</b>
                            </td>
                            <td align="center" width="9%">
                            </td>
                            <td width="5%">
                            </td>
                            <td align="center" width="35%">
                            </td>
                        </tr>
                    

                        <tr>
                            <td align="center" style="vertical-align: Top;" width="11%">
                                <b>
                                    <asp:Label ID="Label1" runat="server" Text="Select" Width="60%"></asp:Label>
                                </b>
                            </td>
                          
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlMonth" runat="server" SelectionMode="Multiple" Width="50%"></asp:ListBox>
                            </td>
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlyear" runat="server" SelectionMode="Multiple" Width="120%"></asp:ListBox>
                            </td>
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlcountry" runat="server" class="form-control" AutoPostBack="true"
                                    SelectionMode="Multiple" Width="40%" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged">
                                </asp:ListBox>
                            </td>
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlemp" runat="server" class="form-control" SelectionMode="Multiple"
                                    Width="45%"></asp:ListBox>
                            </td>
                            <td align="left" style="vertical-align: top;" width="9%">
                                <asp:Button ID="btnSave" runat="server" class="btn btn-success" Font-Size="Small"
                                    OnClick="btnSave_Click" Text="GET DATA" Width="100px" />
                            </td>
                            <td colspan="2" width="40%">
                         &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnexporttoex" runat="server" Text="EXPORT TO EXCEL" class="btn btn-danger"
                            Font-Bold="true" OnClick="btnexporttoex_Click" />
                                       
                            </td>
                            
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
        </td>
    </tr><%--663399--%>
    <b><asp:Label ID="lblsummaryname" runat="server" Text="VISIT SUMMARY OF MODE OF CALL" ForeColor="Red" Visible="false" CssClass="text-align:center;"></asp:Label></b>
        <br />
    <table border="1px">
        <tr>
            <td align="center" width="150%">
               
                <asp:GridView ID="GVScoreCard" runat="server" RowStyle-BorderWidth="1" RowStyle-BorderColor="Red" Visible="true"   OnRowCreated="GVScoreCard_RowCreated"
                    AutoGenerateColumns="false" OnRowDataBound="GVScoreCard_RowDataBound" class="rounded_corners"
                    Font-Bold="True" HeaderStyle-BackColor="#d71313" HeaderStyle-ForeColor="Black"
                    RowStyle-BackColor="#A1DCF2" RowStyle-ForeColor="#3A3A3A" Width="150%" BackColor="White"
                    BorderColor="#CC9966" BorderStyle="None" CellPadding="4">
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#d71313" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                    <Columns>
                        <asp:TemplateField HeaderText=" Employee ">
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Font-Size="Small" Text='<%#Eval("Employee")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:TemplateField HeaderText=" WkNo " >
                            <ItemTemplate>
                                <asp:Label ID="lblwkly" runat="server" Font-Size="Small" Text='<%#Eval("WeekNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText=" Country ">
                            <ItemTemplate>
                                <asp:Label ID="lblcountry" runat="server" Font-Size="Small" Text='<%#Eval("Country")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText=" Visit Date ">
                            <ItemTemplate>
                                <asp:Label ID="lblactualVdate" runat="server" Font-Size="Small" Text='<%#Eval("VisitDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        
                            <asp:TemplateField HeaderText="  Call Mode ">
                            <ItemTemplate>
                                <asp:Label ID="lblmodeofcall" runat="server" Font-Size="Small" Text='<%#Eval("ModeOfCall")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                            <asp:TemplateField HeaderText="  Call Status  ">
                            <ItemTemplate>
                                <asp:Label ID="lblcallstatus" runat="server" Font-Size="Small" Text='<%#Eval("CallStatus")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        
                           <asp:TemplateField HeaderText="  Company   ">
                            <ItemTemplate>
                                <asp:Label ID="lblcallstatus" runat="server" Font-Size="Small" Text='<%#Eval("Company")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="  Contact Details ">
                            <ItemTemplate>
                                <asp:Label ID="lblprsonmet" runat="server" Font-Size="Small" Text='<%#Eval("PersonMet")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText= "  Email  ">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" runat="server" Font-Size="Small" Text='<%#Eval("Email")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="  Discussion  ">
                            <ItemTemplate>
                                <asp:Label ID="lbldisc" runat="server" Font-Size="Small" Text='<%#Eval("Discussion")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                      
                             <asp:TemplateField HeaderText="  Next Action  ">
                            <ItemTemplate>
                                <asp:Label ID="lblnxtaction" runat="server" Font-Size="Small" Text='<%#Eval("NextAction")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        

                        
                          <asp:TemplateField HeaderText="  Biz($)  ">
                            <ItemTemplate>
                                <asp:Label ID="lblbiz" runat="server" Font-Size="Small" Text='<%#Eval("ExpectedBusinessAmt")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText=" Feedback  ">
                            <ItemTemplate>
                                <asp:Label ID="lblfeedback" runat="server" Font-Size="Small" Text='<%#Eval("FeedBack")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>




                         <asp:TemplateField HeaderText="  HOD Comments  ">
                            <ItemTemplate>
                                <asp:Label ID="lblnxtaction" runat="server" Font-Size="Small" Text='<%#Eval("Comments")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <td>
            </td>
        </tr>
    </table>
   

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
            width: 600px;
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
          
              border: 1px solid #000000;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        .rounded_corners table table td
        {
            border-style: none;
        }
        
        
        .style2
        {
            height: 45px;
        }
    </style>
    <br></br>
</asp:Content>
