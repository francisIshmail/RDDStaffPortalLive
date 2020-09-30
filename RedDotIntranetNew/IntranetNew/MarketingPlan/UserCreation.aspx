<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="UserCreation.aspx.cs" Inherits="IntranetNew_MarketingPlan_UserCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<table>
<tr>

<td>
 <asp:Panel ID="pnlForms" runat="server" Width="140%" Height="25%" BorderWidth="1px"
                    BorderColor="Red" EnableTheming="true">
    <table class="table table-stripped" width="100%" align="center" cellpadding="3px"
                        cellspacing="3px">
        <tr>
            <td >
                Originator
            </td>
            <td width="20%">
                <asp:DropDownList ID="ddlorignator" runat="server">
                </asp:DropDownList>
             <%--   <asp:DropDownList ID="ddlorignator" TabIndex="1" AutoPostBack="true" runat="server"
                    Style="width: 77%; padding: 5px 12px; margin: 3px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 14px;" class="form-control">
                </asp:DropDownList>--%>
            </td>
            <td  width="10%">
                Approver
            </td>
            <td  width="10%">
               <%-- <asp:CheckBoxList ID="ddlapprover" runat="server" AutoPostBack="true" Style="width: 77%;
                    padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                    font-size: 14px;" class="form-control" OnSelectedIndexChanged="ddlapprover_SelectedIndexChanged"
                    Font-Size="Small">
                </asp:CheckBoxList>--%>

                <asp:ListBox ID="ddlapprover" runat="server" SelectionMode="Multiple" Width="130px"
                    onselectedindexchanged="ddlapprover_SelectedIndexChanged1" Height="240px"></asp:ListBox>
            </td>
           
                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
          
        </tr>
        <tr>
            <td  width="10%">
                <asp:Button ID="btnsave" runat="server" Text="SAVE" OnClick="btnsave_Click" />
                
                
            </td>
        </tr>
    </table>
    </asp:Panel>

</td>
</tr>
    
 
      
        <tr>
        
     <td align="center">
         <asp:GridView ID="GvData" runat="server" AutoGenerateColumns="False" 
             CellPadding="4" ForeColor="#333333" GridLines="None"  Width="140%">
             <AlternatingRowStyle BackColor="White" />
         <Columns>
         
         <asp:TemplateField HeaderText="Sr.No">
          <ItemTemplate>
              <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
               <asp:Label ID="lblauthid" runat="server" Text='<%#Eval("Authentication_ID")%>' Visible="false"></asp:Label>
          </ItemTemplate>
         </asp:TemplateField>


         <asp:TemplateField HeaderText="Originator">
          <ItemTemplate>
              <asp:Label ID="lblori" runat="server" Text='<%#Eval("originator")%>'></asp:Label>
            
          </ItemTemplate>
         </asp:TemplateField>

          <asp:TemplateField HeaderText="Approver">
          <ItemTemplate>
              <asp:Label ID="lblapp" runat="server" Text='<%#Eval("Approver")%>'></asp:Label>
            
          </ItemTemplate>
         </asp:TemplateField>
           <asp:TemplateField HeaderText="Action">
          <ItemTemplate>
              <asp:LinkButton ID="lnkdelete" runat="server" onclick="lnkdelete_Click">Delete</asp:LinkButton>
               <%-- <asp:LinkButton ID="lnkEdit" runat="server" onclick="lnkEdit_Click" >Edit</asp:LinkButton>--%>
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
       </td>
    </tr>
     
   </table>
</asp:Content>
