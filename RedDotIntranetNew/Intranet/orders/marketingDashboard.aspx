<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern.master" AutoEventWireup="true" CodeFile="marketingDashboard.aspx.cs" Inherits="Intranet_orders_marketingDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
#tblFields td
{
    border-bottom-style:dotted;
    border-bottom-width:1px;
}

#tblFields tr
{
    height:30px;
}
</style>

     <table width="100%" style="border-color:#00A9F5;">
              <tr style="height:50px;background-color:#507CD1"> <%--row 1--%>
                <td>
                    <div class="Page-Title">Marketing Plans' Dashboard</div>
                </td>
             </tr>
    </table>

    <table width="100%" cellspacing="3px" style="border: medium solid #B9B9B9">
    <tr>
        <%--summary--%>
        <td style="height:auto;width:100%; background-color: #ECECF2; color: #000000;">
            <table width="100%" cellspacing="3px">
                <tr>
                    <td>
                        <asp:Panel ID="pnlSummary" runat="server" GroupingText="Summary" BackColor="#CECEDF">
                            <table id="tblFields" style="width:40%; color: #666666">
                                <tr style="background-color:White;height:35px">
                                    <td style="width:75%;color:black;font-weight:bold">Select Year</td>
                                    <td style="width:25%;color:black;font-weight:bold"><asp:DropDownList ID="ddlYear" 
                                            runat="server" Width="100px" AutoPostBack="True" onselectedindexchanged="ddlYear_SelectedIndexChanged" Font-Bold="True"></asp:DropDownList>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>Funds in pipeline</td>
                                    <td>
                                        <asp:Label ID="lblFundsPipeline" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Accrued approved amount</td>
                                     <td>
                                        <asp:Label ID="lblAccruedApprovedAmt" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Total expenses</td>
                                     <td>
                                        <asp:Label ID="lblExpenses" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Total claimed amount</td>
                                    <td>
                                        <asp:Label ID="lblClaimAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Total unclaimed amount</td>
                                    <td>
                                        <asp:Label ID="lblUnclaimAmount" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Funds available for MKT</td>
                                    <td>
                                        <asp:Label ID="lblFundsAvailableForMKT" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>
                                 <tr>
                                    <td>Amount in MB account should be</td>
                                    <td>
                                        <asp:Label ID="lblMBAccountAmt" runat="server" Text=""></asp:Label>
                                    </td>
                                 </tr>                                
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>

                        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="2" 
                            Width="100%">
                            <asp:TabPanel ID="tabAccruedFunds" runat="server">
                                <HeaderTemplate>Accrued Funds</HeaderTemplate>
                                <ContentTemplate>
                                   <asp:Label ID="lblMsgChartAccruedFunds" runat="server" Text=""></asp:Label><br />
                                        <asp:Chart ID="chartAccruedFunds" runat="server" Height="400px" Width="450px" 
                                                Palette="Light">
                                                <Series>
                                                    <asp:Series ChartType="Pie" Name="Series1" Palette="SemiTransparent">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Area3DStyle-Enable3D="True" Name="ChartArea1">
                                                        <Area3DStyle Enable3D="True" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend Docking="Bottom" LegendStyle="row">
                                                        <CellColumns>
                                                            <asp:LegendCellColumn ColumnType="SeriesSymbol">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn>
                                                            <asp:LegendCellColumn Text="#PERCENT" Alignment="MiddleLeft">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn> 
                                                            
                                                        </CellColumns>
                                                    </asp:Legend>
                                                </Legends>
                                            </asp:Chart>
                                </ContentTemplate>
                            </asp:TabPanel>    
                                

                            <asp:TabPanel ID="tabClaims" runat="server">
                                <HeaderTemplate>Claims</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Label ID="lblMsgChartClaims" runat="server" Text=""></asp:Label><br />
                                   <asp:Chart ID="chartClaims" runat="server" Height="400px" Width="450px" 
                                                Palette="Light">
                                                <Series>
                                                    <asp:Series ChartType="Pie" Name="Series1" Palette="SemiTransparent">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Area3DStyle-Enable3D="True" Name="ChartArea1">
                                                        <Area3DStyle Enable3D="True" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend Docking="Bottom" LegendStyle="row">
                                                        <CellColumns>
                                                            <asp:LegendCellColumn ColumnType="SeriesSymbol">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn>
                                                            <asp:LegendCellColumn Text="#PERCENT" Alignment="MiddleLeft">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn> 
                                                            
                                                        </CellColumns>
                                                    </asp:Legend>
                                                </Legends>
                                            </asp:Chart>
                                </ContentTemplate>
                            </asp:TabPanel>  
                            
                                  
                            <asp:TabPanel ID="tabPMMarketing" runat="server">


                                <HeaderTemplate>PM v/s Marketing</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Label ID="lblMsgChartPMMarketing" runat="server" Text=""></asp:Label><br />
                                    <asp:Chart ID="chartPMMarketing" runat="server" Height="400px" Width="450px" 
                                                Palette="Light">
                                                <Series>
                                                    <asp:Series ChartType="Pie" Name="Series1" Palette="SemiTransparent">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Area3DStyle-Enable3D="True" Name="ChartArea1">
                                                        <Area3DStyle Enable3D="True" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend Docking="Bottom" LegendStyle="row">
                                                        <CellColumns>
                                                            <asp:LegendCellColumn ColumnType="SeriesSymbol">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn>
                                                            <asp:LegendCellColumn Text="#PERCENT" Alignment="MiddleLeft">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn> 
                                                            
                                                        </CellColumns>
                                                    </asp:Legend>
                                                </Legends>
                                            </asp:Chart>
                                </ContentTemplate>
                            </asp:TabPanel>     
                            
                               
                            <asp:TabPanel ID="tabActivityType" runat="server">
                                <HeaderTemplate>Activity Type</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Label ID="lblMsgChartActivityType" runat="server" Text=""></asp:Label><br />
                                    <asp:Chart ID="chartActivityType" runat="server" Height="400px" Width="450px" 
                                                Palette="Light">
                                                <Series>
                                                    <asp:Series ChartType="Pie" Name="Series1" Palette="SemiTransparent">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Area3DStyle-Enable3D="True" Name="ChartArea1">
                                                        <Area3DStyle Enable3D="True" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend Docking="Bottom" LegendStyle="row">
                                                        <CellColumns>
                                                            <asp:LegendCellColumn ColumnType="SeriesSymbol">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn>
                                                            <asp:LegendCellColumn Text="#PERCENT" Alignment="MiddleLeft">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn> 
                                                            
                                                        </CellColumns>
                                                    </asp:Legend>
                                                </Legends>
                                            </asp:Chart>
                                </ContentTemplate>
                            </asp:TabPanel>   
                            
                                 
                            <asp:TabPanel ID="tabLocation" runat="server">
                                <HeaderTemplate>Location Wise</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:Label ID="lblMsgChartLocation" runat="server" Text=""></asp:Label><br />
                                    <asp:Chart ID="chartLocation" runat="server" Height="400px" Width="450px" 
                                                Palette="Light">
                                                <Series>
                                                    <asp:Series ChartType="Pie" Name="Series1" Palette="SemiTransparent">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Area3DStyle-Enable3D="True" Name="ChartArea1">
                                                        <Area3DStyle Enable3D="True" />
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                                <Legends>
                                                    <asp:Legend Docking="Bottom" LegendStyle="row">
                                                        <CellColumns>
                                                            <asp:LegendCellColumn ColumnType="SeriesSymbol">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn>
                                                            <asp:LegendCellColumn Text="#PERCENT" Alignment="MiddleLeft">
                                                                <Margins Left="15" Right="15" />
                                                            </asp:LegendCellColumn> 
                                                            
                                                        </CellColumns>
                                                    </asp:Legend>
                                                </Legends>
                                            </asp:Chart>
                                </ContentTemplate>
                            </asp:TabPanel> 
                        </asp:TabContainer>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
       




</asp:Content>

