<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="DownloadReports.aspx.cs" Inherits="IntranetNew_Reports_DownloadReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">

    function btnOverEffect(rw) {

        var grid = document.getElementById('<%=grdWeeklyFiles.ClientID %>');
        var imgBtn = grid.rows[rw + 1].cells[2].children[0];
        imgBtn.style.width = '120px';
        imgBtn.style.height = '25px';

    }
    function btnOutEffect(rw) {

        var grid = document.getElementById('<%=grdWeeklyFiles.ClientID %>');
        var imgBtn = grid.rows[rw + 1].cells[2].children[0];
        imgBtn.style.width = '100px';
        imgBtn.style.height = '25px';

    }

    function clearMsg() {
        var lb = document.getElementById('<%=lblMsg.ClientID %>');
        lb.innerHTML = "";
    }

</script>
<center>

<table width="80%">
    <tr>
        <td class="style1" >
            <%--Get Report for --%>
            &nbsp;
            <asp:RadioButton ID="radEVO" runat="server" AutoPostBack="True" Checked="True" 
                Font-Bold="True" GroupName="rep" Text="EVOLUTION" 
                oncheckedchanged="radEVO_CheckedChanged" Visible="false"/>
            <asp:RadioButton ID="radSap" runat="server" AutoPostBack="True" 
                Font-Bold="True" GroupName="rep" Text="SAP (Testing)" 
                oncheckedchanged="radSap_CheckedChanged" Visible="false"/>
        </td>
    </tr>
    <tr style="height:50px">
        <td align="center" style="width:100%;color:#797979;font-weight:bolder;font-size:x-large">
            <asp:Label ID="lblTitle" runat="server" Text="" Font-Bold="true"></asp:Label>
        </td>
    </tr>
    <tr style="height:40px">
        <td>
           <asp:Panel ID="panelWorkingUsers" runat="server" GroupingText="You can work as follwing Users ( Work As selected one)">
                                <table width="100%" style="">
                                    <tr>
                                      <td style="width:70%">
                                       <asp:RadioButtonList ID="RadWorkingUsers" runat="server" 
                                            RepeatDirection="Horizontal" 
                                            onselectedindexchanged="RadWorkingUsers_SelectedIndexChanged" AutoPostBack="true" Font-Bold="false">
                                        </asp:RadioButtonList>
                                         
                                       </td>
                                      <td style="width:30%">
                                         <asp:Label ID="lblworkForUser" runat="server" Text="." Font-Size="16px" Font-Bold="true" Visible="false"></asp:Label>

                                         Work As : &nbsp;<asp:Label ID="lblWorkingAs" runat="server" Text="." Font-Size="14px" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                      </td>
                                    </tr>
                                      
                                </table>
               </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblCurrentReportBasePath" runat="server" Text="" Visible="false" ></asp:Label><br />
            <asp:Label ID="lblDwnldFilePathnow" runat="server" Text="" Visible="false"> </asp:Label>
        </td>
    </tr>
    <tr>
        <td>
          <asp:Panel ID="PanelFolderList" runat="server" BorderWidth="1px" BorderColor="Red" EnableTheming="true" Visible="true">
              <table style="width:100%;  color:Black;padding-top:10px;padding-left:25px;" border="0px">
                <tr >
                 <td style="width:20%;" >
                 &nbsp;&nbsp;   <asp:Label ID="LblSelectDate" Text="  Select Report Date" runat="server"  > </asp:Label>
                 </td>
                 <td style="width:25%" >
                  <asp:DropDownList ID="ddlFoldersList" runat="server" Width="220px"  ForeColor="Black"  Font-Size="13px" Font-Bold="true">
                  </asp:DropDownList>
                 </td>
                 <td style="width:50%;" >
                  ( Latest&nbsp;<asp:Label ID="lblnoOfReps" runat="server" Text="0" Font-Bold="false" ForeColor="Black" ></asp:Label>&nbspReports Listed )
                 </td>
                </tr>
              </table>
          </asp:Panel>
        </td>
    </tr>
    <tr style="height:20px">
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" >
            <asp:Panel ID="Panel1" runat="server" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
            <asp:GridView ID="grdWeeklyFiles" runat="server" AutoGenerateColumns="False" 
                Width="100%" CellPadding="4" GridLines="None" ForeColor="#333333" 
                onrowdatabound="grdWeeklyFiles_RowDataBound">
                <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' Font-Bold="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle  Width="10%" CssClass="gvItemCenter" Height="29px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Detail / BU">
                    <ItemTemplate>
                        <asp:Label ID="lblBU" runat="server" Text='<%#Eval("BU")%>' Font-Bold="false"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="gvItemCenter" Width="30%" Height="29px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Download">
                    <ItemTemplate>
                            <asp:ImageButton ID="btnDownload" runat="server" Width="100px" Height="25px" ImageUrl="~/images/Download_Red1.jpg" OnClick="btnDownload_Click" ToolTip="Click to download file!" OnClientClick="clearMsg();" />
                    </ItemTemplate>
                        <%--<ItemStyle HorizontalAlign="Center" Height="40px"  />--%>
                         <ItemStyle CssClass="gvItemCenter" Width="30%" Height="29px"/>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="File" Visible="false">
                        <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("FileName")%>' Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblPth" runat="server" Text='<%#Eval("reportFilePath")%>' Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblintimationMailId" runat="server" Text='<%#Eval("intimationMailId")%>' Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblsendDownloadIntimation" runat="server" Text='<%#Eval("sendDownloadIntimation")%>' Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblsendUploadIntimation" runat="server" Text='<%#Eval("sendUploadIntimation")%>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="40%" />
                </asp:TemplateField>
            </Columns>
                <FooterStyle BackColor="#d71313" Font-Bold="false" ForeColor="White" />
                <HeaderStyle BackColor="#d71313" Font-Bold="false" ForeColor="White" 
                    Height="30px" />
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
    </tr>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblPath" runat="server" Text="xxx" Visible="false"></asp:Label>
        </td>
    </tr>    
</table>
</center>

<style type="text/css">

.gvItemCenter { text-align: center; }
.gvItemHeight { height:30px }

</style>

</asp:Content>

