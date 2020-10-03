<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="Authorization.aspx.cs" Inherits="IntranetNew_Admin_Authorization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; User Authorization </Lable>
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
    
<asp:Panel ID="pnlForms" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="90%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="35%">
           &nbsp;
        </td>
        <td width="45%">
        &nbsp;
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>
    
 
      <tr>
        
        <td width="35%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">User  &nbsp; </label>  
        </td>
        <td width="45%">
            <asp:DropDownList ID="ddlUser" runat="server" 
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                AutoPostBack="True" onselectedindexchanged="ddlUser_SelectedIndexChanged">
            </asp:DropDownList>
            
        </td>
        <td width="20%">
            <asp:Label ID="lblMenuID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>

        <tr>
        <td width="35%">
           &nbsp;
        </td>
        <td width="45%">
        &nbsp;
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

    <tr>
    <td width="100%" align="center" colspan="3">
<%--        <asp:Panel ID="pnlFormList" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">--%>

        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="100%" >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="ID">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"
                                  Text='<%#Eval("MenuId")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenterID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="Menu">
                            <ItemTemplate>
                                <asp:Label ID="lblMenu" runat="server" Text='<%#Eval("MenuName")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="30%" HeaderText="Form Name">
                            <ItemTemplate >
                                <asp:Label ID="lblFormName" runat="server" Text='<%#Eval("FormName")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="5%" HeaderText="IsActive">
                            <ItemTemplate>
                                <asp:CheckBox align="center" ID="chkIsActive" runat="server" Checked='<%#Eval("IsActive")%>' ></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
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

 <%--       </asp:Panel>--%>
    </td>
</tr>

     <tr>
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click1" 
                 />  &nbsp;&nbsp; 
       <%-- </td>
        <td align="left" width="45%">--%>
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
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

<style type="text/css">

.gvItemCenter { text-align: left; }
.gvHeaderCenter {  text-align: left; }
.gvSelectButton { ForeColor: Blue}
.gvHeaderCenterID { text-align: center; }
.gvItemCenterID { text-align: center; }

</style>

</asp:Content>

