<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="designationmaster.aspx.cs" Inherits="IntranetNew_Targets_designationmaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Designation Master  </Lable>
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


    <asp:Panel ID="pnlForms" runat="server" Width="55%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="90%" align="center" cellpadding="2px" cellspacing="2px" >
   
      <tr>
        
        <td width="30%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">ID  &nbsp; </label>  
        </td>
        <td width="40%">
               <asp:TextBox ID="txtid" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " Enabled="false"></asp:TextBox> 
        
        </td>
        <td width="30%">
            <asp:Label ID="lblMenuID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>

      <tr>
        <td width="30%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Designation   &nbsp;  </label>
        </td>
        <td width="70%" colspan="2" >
            <asp:TextBox ID="txtdesignation" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;  " CausesValidation="true" ></asp:TextBox> 
         
          <asp:RequiredFieldValidator id="reqval1" runat="server" ControlToValidate="txtdesignation" ErrorMessage="Enter Designation"></asp:RequiredFieldValidator>
       
        </td>
    </tr>
     <tr>
        <td width="80%" align="center" colspan="3"> &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save"  OnClick="BtnSave_Click" style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " />  &nbsp;&nbsp; 
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  />
        </td>
       
    </tr>
   
</table>

</asp:Panel>


  </td>
</tr>

<tr>
<td>&nbsp; </td>
</tr>


<tr>
    <td width="100%" align="center">
        <asp:Panel ID="pnlFormList" runat="server" Width="55%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
        
        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="100%" AllowPaging="True" 
                onselectedindexchanged="Gridview1_SelectedIndexChanged" 
                OnRowCommand="GridView1_RowCommand" OnRowDeleting="Griview1_RowDeleting" PageSize="10" OnPageIndexChanging="Griview1_PageIndexChanging"
                >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"
                                  Text='<%#Eval("Id")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenterID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="60%"  HeaderText="Designation">
                            <ItemTemplate>
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenter" />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:CommandField ShowSelectButton="True" SelectText="Edit"  ControlStyle-ForeColor="Blue" ButtonType="Link" ItemStyle-Width="15%"   />
                    
                      <asp:TemplateField ItemStyle-Width="15%" >

                            <ItemTemplate>
                                <asp:LinkButton  ID="lnkdelete" runat="server" 
                                  CommandArgument='<%#Eval("Id")%>' Text="Delete" CommandName="Delete" ForeColor="Blue"  CausesValidation="false" OnClientClick="return <b> confirm('Are you sure  to delete this entry') </b>"   ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle CssClass="gvItemCenterID" />
                            <HeaderStyle CssClass="gvHeaderCenterID" />

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

