<%@ Page Title="" Language="C#" MasterPageFile="~/Intranet/MasterIntern_1.master" AutoEventWireup="true" CodeFile="DownloadRepsNew.aspx.cs" Inherits="Intranet_webReporting_DownloadRepsNew" %>

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
        imgBtn.style.height = '30px';

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
        <td class="style1">
            Get Report for 
            &nbsp;
            <asp:RadioButton ID="radEVO" runat="server" AutoPostBack="True" Checked="True" 
                Font-Bold="True" GroupName="rep" Text="EVOLUTION" 
                oncheckedchanged="radEVO_CheckedChanged" />
            <asp:RadioButton ID="radSap" runat="server" AutoPostBack="True" 
                Font-Bold="True" GroupName="rep" Text="SAP (Testing)" 
                oncheckedchanged="radSap_CheckedChanged" />
        </td>
    </tr>
    <tr style="height:50px">
        <td align="center" style="width:100%;color:#797979;font-weight:bolder;font-size:x-large">
            <asp:Label ID="lblTitle" runat="server" Text="" Font-Bold="true"></asp:Label>
        </td>
    </tr>
      <tr style="height:20px">
        <td>
            &nbsp;
        </td>
    </tr>
    <tr style="height:40px">
        <td>
           <asp:Panel ID="panelWorkingUsers" runat="server"  class="POPanel1"  GroupingText="You can work as follwing Users ( Work As selected one)">
                                <table width="100%" >
                                    <tr>
                                      <td style="width:70%">
                                       <asp:RadioButtonList ID="RadWorkingUsers" runat="server" 
                                            RepeatDirection="Horizontal" 
                                            onselectedindexchanged="RadWorkingUsers_SelectedIndexChanged" AutoPostBack="true" Font-Bold="true">
                                        </asp:RadioButtonList>
                                         
                                       </td>
                                      <td style="width:30%">
                                         <asp:Label ID="lblworkForUser" runat="server" Text="." Font-Size="16px" Font-Bold="True" Visible="false"></asp:Label>

                                         Work As : &nbsp;<asp:Label ID="lblWorkingAs" runat="server" Text="." Font-Size="14px" ForeColor="Blue" Font-Bold="True"></asp:Label>
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
          <asp:Panel ID="PanelFolderList" runat="server" BorderWidth="0px" Height="30px" Visible="true">
              <table style="width:100%;background-color:#c7c7c7;color:Black;padding-top:10px;padding-left:25px" border="0px">
                <tr style="height:10px">
                    <td > &nbsp;</td>
                </tr>
                <tr style="height:30px">
                 <td style="width:20%;font-size:16px;font-weight:bold" valign="baseline">
                   Select Report Date
                 </td>
                 <td style="width:25%"valign="baseline">
                  <asp:DropDownList ID="ddlFoldersList" runat="server" Width="220px" BackColor="#cfcfcf" ForeColor="Black" Font-Bold="true">
                  </asp:DropDownList>
                 </td>
                 <td style="width:55%;font-size:13px;font-weight:bold" valign="baseline" >
                  ( Latest&nbsp;<asp:Label ID="lblnoOfReps" runat="server" Text="0" Font-Bold="true" ForeColor="Black" ></asp:Label>&nbspReports Listed )
                 </td>
                </tr>
                 <tr style="height:10px">
                    <td> &nbsp;</td>
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
        <td align="center">
            <asp:GridView ID="grdWeeklyFiles" runat="server" AutoGenerateColumns="False" 
                Width="100%" CellPadding="3" GridLines="Vertical" ForeColor="Black" 
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
                BorderWidth="1px" onrowdatabound="grdWeeklyFiles_RowDataBound">
                <AlternatingRowStyle BackColor="#c7c7c7" />
            <Columns>
                <asp:TemplateField HeaderText="Sr. No">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%#Container.DataItemIndex+1 %>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Detail / BU">
                    <ItemTemplate>
                        <asp:Label ID="lblBU" runat="server" Text='<%#Eval("BU")%>' Font-Bold="true"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="30%" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>
                            <asp:ImageButton ID="btnDownload" runat="server" Width="100px" Height="25px" ImageUrl="~/Intranet/images/download_button.png" OnClick="btnDownload_Click" ToolTip="Click to download file!" OnClientClick="clearMsg();" />
                    </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Height="40px" Width="20%" />
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
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="#c7c7c7" Font-Bold="True" ForeColor="black" Height="40px" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
             
            </asp:GridView>
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
</asp:Content>
