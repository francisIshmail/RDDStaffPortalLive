<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="DSR_RPT.aspx.cs" Inherits="IntranetNew_DailySalesReport_DSR_RPT" %>

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
                                <%--     <label> Select</label> --%>
                            </td>
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlMonth" runat="server" SelectionMode="Multiple" Width="50%"></asp:ListBox>
                            </td>
                            <td align="left" width="10%">
                                <asp:ListBox ID="ddlyear" runat="server" SelectionMode="Multiple" Width="120%"></asp:ListBox>
                                <%--  <asp:DropDownList ID="ddlyear" runat="server" class="form-control" Width="98%">
                                            </asp:DropDownList>--%>
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
                                    OnClick="btnSave_Click" Text="GET SCORE" Width="100px" />
                            </td>
                            <td width="5%">
                            </td>
                            <td width="35%">
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/outer css-js/images/Black.png" ToolTip="Above 80%"
                                    Enabled="false" Width="20px" Height="18px" />
                                <asp:Label ID="Label5" runat="server" Text="Good" style="vertical-align: Top;" ToolTip="Above 80%"></asp:Label>
                               <%-- <asp:ImageButton ID="btn1" runat="server" ImageUrl="~/outer css-js/images/green1.png" ToolTip="Equal To 100%"
                                    Enabled="false" Width="20px" Height="18px" />
                                <asp:Label ID="Label2" runat="server" Text="Good" style="vertical-align: Top;" ToolTip="Equal To 100%"></asp:Label>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/outer css-js/images/orange.png" ToolTip="Above 75% and Below 99%"
                                    Enabled="false" Width="20px" Height="18px" />
                                <asp:Label ID="Label3" runat="server" Text="Average" style="vertical-align: Top;" ToolTip="Above 75% and Below 99%"></asp:Label>--%>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/outer css-js/images/Red1.png" ToolTip="Below 80%"
                                    Enabled="false" Width="20px" Height="18px" />
                                <asp:Label ID="Label4" runat="server" Text="Poor" style="vertical-align: Top;" ToolTip="Below 75%"></asp:Label>
                            </td>
                        </tr>
                    </caption>
                </table>
            </asp:Panel>
        </td>
    </tr><%--663399--%>
    <table border="1px">
        <tr>
            <td align="center" width="120%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:GridView ID="GVScoreCard" runat="server" RowStyle-BorderWidth="1" RowStyle-BorderColor="Red" Visible=false
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
                        <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Font-Size="Small" Text='<%#Eval("Employee")%>'></asp:Label>
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

     <table border="1px">
        <tr>
            <td align="center" width="120%">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:GridView ID="GVScoreCard1" runat="server" RowStyle-BorderWidth="1" RowStyle-BorderColor="Red" OnRowCreated="GVScoreCard1_RowCreated" OnRowDataBound="GVScoreCard1_RowDataBound"
                    AutoGenerateColumns="false"  class="rounded_corners"
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
                        <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate>
                                <asp:Label ID="lblemp" runat="server" Font-Size="Small" Text='<%#Eval("Employee")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <asp:TemplateField HeaderText=" Monthly ">
                            <ItemTemplate>
                                <asp:Label ID="lblmon" runat="server" Font-Size="Small" Text='<%#Eval("Monthly")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>


                         <asp:TemplateField HeaderText=" TARGET ">
                            <ItemTemplate>
                                <asp:Label ID="lbltarget" runat="server" Font-Size="Small" Text='<%#Eval("TotalTarget")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                          <asp:TemplateField HeaderText=" VISIT ">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalvisit" runat="server" Font-Size="Small" Text='<%#Eval("TotalVisit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=" DIST ">
                            <ItemTemplate>
                                <asp:Label ID="lbldivisit" runat="server" Font-Size="Small" Text='<%#Eval("DistinctVisit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="  REPT  ">
                            <ItemTemplate>
                                <asp:Label ID="lblrpevisit" runat="server" Font-Size="Small" Text='<%#Eval("RepeatVisit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="  NEW  ">
                            <ItemTemplate>
                                <asp:Label ID="lblnewvisit" runat="server" Font-Size="Small" Text='<%#Eval("NewVisit")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="   OPEN   ">
                            <ItemTemplate>
                                <asp:Label ID="lblopenitems" runat="server" Font-Size="Small" Text='<%#Eval("OpenItem")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                          <asp:TemplateField HeaderText="  CLOSED  ">
                            <ItemTemplate>
                                <asp:Label ID="lblclosed" runat="server" Font-Size="Small" Text='<%#Eval("Closed")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText=" NO # ">
                            <ItemTemplate>
                                <asp:Label ID="lblprospect" runat="server" Font-Size="Small" Text='<%#Eval("NO#")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText=" USD ">
                            <ItemTemplate>
                                <asp:Label ID="lblusd" runat="server" Font-Size="Small" Text='<%#Eval("USD")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                           <asp:TemplateField HeaderText="  ">
                            <ItemTemplate>
                                <asp:Label ID="lblfrwdcall" runat="server" Font-Size="Small" Text='<%#Eval("ForwardEmail")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="  ">
                            <ItemTemplate>
                                <asp:Label ID="lbltargetpoint" runat="server" Font-Size="Small" Text='<%#Eval("Targetpoint")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="  ">
                            <ItemTemplate>
                                <asp:Label ID="lbltargetacheve" runat="server" Font-Size="Small" Text='<%#Eval("TargetAchieve")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText=" ">
                            <ItemTemplate>
                                <asp:Label ID="lblnewcustomer" runat="server" Font-Size="Small" Text='<%#Eval("NewCustomers")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         
                        
                         <asp:TemplateField HeaderText=" " >
                            <ItemTemplate>
                                <asp:Label ID="lbltotalpoint" runat="server" Font-Size="Small" Text='<%#Eval("TotalPoint")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

            
                    </Columns>
                </asp:GridView>
            </td>
            <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
            <td>
            </td>
        </tr>
        </table>
    <%--  
    <asp:chart id="Chart1" runat="server"
            Width="1000px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  <legends>
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="xxcvxcv" 
                LegendStyle="Row" />
  </legends>
  <series>
    <asp:Series Name="DAILY VISIT" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart>



    <asp:chart id="Chart2" runat="server"
             Height="300px" Width="1000px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  
  <legends>
 
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="WEEKLY SCORE"
                LegendStyle="Row" />
  </legends>
  <series>
    <asp:Series Name="WEEKLY VISIT" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart>

<br />--%>
    <asp:Chart ID="Chart3" runat="server" Height="500px" Width="1000px">
        <Titles>
            <asp:Title ShadowOffset="3" Name="Title1" />
        </Titles>
      
        <Legends>
            <asp:Legend Alignment="Center" Docking="Bottom" BackColor="White" IsTextAutoFit="False"
                Name="MONTHLY SCORE" LegendStyle="Row" />
        </Legends>
         <Series>
            <asp:Series Name="TARGET" YValuesPerPoint="2" BorderColor="Red"  Color="Red"  Label="#VALY%" />
        </Series>
        <Series>
            <asp:Series Name="ACHIEVED" YValuesPerPoint="2"   BorderColor="MediumSeaGreen" Color="MediumSeaGreen" Label="#VALY%"  />
        </Series>
        
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
        </ChartAreas>
    </asp:Chart>
    <%--<br />
<asp:chart id="Chart2" runat="server"
             Height="300px" Width="400px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  <legends>
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="WEEKLY"
                LegendStyle="Row" />
  </legends>
  <series>
    <asp:Series Name="WEEKLY" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart>
<br />
<asp:chart id="Chart3" runat="server"
             Height="300px" Width="400px">
  <titles>
    <asp:Title ShadowOffset="3" Name="Title1" />
  </titles>
  <legends>
    <asp:Legend Alignment="Center" Docking="Bottom"
                IsTextAutoFit="False" Name="MONTHLY"
                LegendStyle="Row" />
  </legends>
  <series>
    <asp:Series Name="MONTHLY" />
  </series>
  <chartareas>
    <asp:ChartArea Name="ChartArea1"
                     BorderWidth="0" />
  </chartareas>
</asp:chart>--%>
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
