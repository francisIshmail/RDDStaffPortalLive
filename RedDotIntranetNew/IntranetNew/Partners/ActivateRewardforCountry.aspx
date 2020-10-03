<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="ActivateRewardforCountry.aspx.cs" Inherits="IntranetNew_Partners_ActivateRewardforCountry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Activate Reward For Country </Lable>
    </td>
</tr>

<tr>
        <td width="50%">
           &nbsp;
        </td>
        <td width="50%">
        &nbsp;
        </td>
</tr>

<tr>
        <td width="100%" align="center">
        
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:#be19c1; font-weight:bold; " />  &nbsp;&nbsp; 
               
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlQuarterlyRewardPercent" runat="server" Width="30%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
    <tr>
    <td colspan="3" > &nbsp;   </td>
    </tr>
        
     <tr >
        <td width="100%" colspan="3" align="center" >
         
            <%--<asp:Panel ID="pnlQrterlyRow" runat="server" Width="90%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">--%>
                   <asp:GridView ID="grvActivateCountry" runat="server" 
                       AutoGenerateColumns="False" ShowFooter="false" 
                ShowHeaderWhenEmpty="True" DataKeyNames="ActivateRewardID"
                        ForeColor="#333333"  Width="90%" CellPadding="4" 
                GridLines="None" onrowdatabound="grvActivateCountry_RowDataBound"   >
                       <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lblCountryCode" runat="server" Text='<%#Eval("CountryCode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="8%"  HeaderText="Country">
                            <ItemTemplate>
                                <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Country")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="5%"  HeaderText="IsRewardActive">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%#Eval("IsRewardActive")%>'></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>

                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" 
                           HorizontalAlign="Center" />
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
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                
                style="width:30%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                 />  &nbsp;&nbsp; 
         
           <asp:Button ID="BtnCancel" runat="server" Text="Go Back" 
                
                
                style="width:30%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnCancel_Click"  
                  />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

    <tr>
        
        <td align="left" colspan="2">
           &nbsp; 
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

</table>


</asp:Content>

