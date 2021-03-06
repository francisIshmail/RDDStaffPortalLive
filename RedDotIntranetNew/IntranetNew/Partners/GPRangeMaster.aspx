<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="GPRangeMaster.aspx.cs" Inherits="IntranetNew_Partners_GPRangeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; GP % Range Master </Lable>
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
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlGPRangeMaster" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="90%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="30%">
           &nbsp;
        </td>
        <td width="35%">
        &nbsp;
        </td>
        <td width="35%">
       <asp:Label ID="lblGPRangeID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>
    
 
      <tr>
        
        <td width="30%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">GP % Range  &nbsp; </label>  
        </td>
        <td width="35%">
            <asp:TextBox ID="txtGPFrom" runat="server" placeholder="0.00" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
            
        </td>
        <td width="35%">
            <asp:TextBox ID="txtGPTo" runat="server"  placeholder="0.00" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
            
        </td>
    </tr>


    <tr >
       
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Remark   &nbsp;  </label>
        </td>
        <td width="35%" colspan="2">
            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" 
                style="width:82%; padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox>
       
          
        </td>
    </tr>
    <asp:Panel id="pnlInactiveRemark" runat="server" Visible="false" > 
        <tr >
       
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Remark to inactive   &nbsp;  </label>
        </td>
        <td width="35%" colspan="2">
            <asp:TextBox ID="txtRemarkInactive" runat="server"  TextMode="MultiLine" style="width:82%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox>
        </td>
    </tr>
    </asp:Panel>
      <tr>
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> IsActive   &nbsp;  </label>
        </td>
        <td width="35%">
            <asp:CheckBox ID="chkActive" runat="server" />
        </td>
        <td width="35%">
        &nbsp;
        </td>
    </tr>

     <tr>
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                 />  &nbsp;&nbsp; 
       <%-- </td>
        <td align="left" width="45%">--%>
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnCancel_Click" 
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

<tr>
    <td> &nbsp;</td>
</tr>

<tr>
    <td width="100%" align="center">
        <asp:Panel ID="pnlFormList" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="100%" AllowPaging="True" onselectedindexchanged="Gridview1_SelectedIndexChanged" 
                >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"
                                  Text='<%#Eval("GPRangeId")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenterID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="From GP %">
                            <ItemTemplate>
                                <asp:Label ID="lblGPFrom" runat="server" Text='<%#Eval("GPPercentageFrom")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="20%" HeaderText="To GP %">
                            <ItemTemplate >
                                <asp:Label ID="lblGPTo" runat="server" Text='<%#Eval("GPPercentageTo")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="20%" HeaderText="Remark" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%#Eval("Remark")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="InactiveRemark" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblInactiveRemark" runat="server" Text='<%#Eval("InactiveRemark")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%" HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:CheckBox align="center" ID="chkIsActive" runat="server" Checked='<%#Eval("IsActive")%>' Enabled="false"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:CommandField ShowSelectButton="True" SelectText="Edit" ControlStyle-ForeColor="Blue" ButtonType="Link" ItemStyle-Width="8%" />

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
</tr>



</table>

<style type="text/css">

.gvItemCenter { text-align: left; }
.gvHeaderCenter {  text-align: left; }
.gvSelectButton { ForeColor: Blue}
.gvHeaderCenterID { text-align: center; }
.gvItemCenterID { text-align: center; }

</style>

</asp:Content>


