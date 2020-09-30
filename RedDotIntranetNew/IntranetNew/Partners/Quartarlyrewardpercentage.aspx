<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="Quartarlyrewardpercentage.aspx.cs" Inherits="IntranetNew_Partners_Quartarlyrewardpercentage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script language="Javascript" type="text/javascript">
    function isNumberKey(evt) {
        debugger;
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
            return true;
        } else {
            alert("Enter only numbers");
            return false;
        }
    }
 </script>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Quarterly Reward Percentage </Lable>
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
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
        <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:#be19c1; font-weight:bold; " />  &nbsp;&nbsp; 
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlQuarterlyRewardPercent" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="30%">
           &nbsp;
        </td>
        <td width="35%">
        &nbsp;
        </td>
        <td width="35%">
       <asp:Label ID="RewardSettingID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>

    <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Country   &nbsp;  </label>
        </td>
        <td width="35%" >
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
           <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true"
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">  </asp:DropDownList>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
         <td width="35%" >
        </td>
    </tr>

     <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Quarter   &nbsp;  </label>
        </td>
        <td width="35%" >
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
           <asp:DropDownList ID="ddlQuarter" runat="server"  AutoPostBack="true"
                
                    style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;" 
                    onselectedindexchanged="ddlQuarter_SelectedIndexChanged">  
             <%--  <asp:ListItem>--Select--</asp:ListItem>
               <asp:ListItem>Q1</asp:ListItem>
               <asp:ListItem>Q2</asp:ListItem>
               <asp:ListItem>Q3</asp:ListItem>
               <asp:ListItem>Q4</asp:ListItem>--%>
            </asp:DropDownList>
          </ContentTemplate>
         </asp:UpdatePanel>
          
        </td>
         <td width="35%" >
         <asp:TextBox ID="txtYear" runat="server" Enabled="false" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
    </tr>
    
    <tr>
        <td colspan="3"> &nbsp;   </td>
    </tr>

     
     <tr >
        <td width="100%" colspan="3" align="center" >
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <%--<asp:Panel ID="pnlQrterlyRow" runat="server" Width="90%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">--%>
                   <asp:GridView ID="grdQuartlyRowDetail" runat="server" 
                       AutoGenerateColumns="false" ShowFooter="true" 
                ShowHeaderWhenEmpty="true" DataKeyNames="RewardSettingLineID"
                        ForeColor="#333333"  Width="90%" 
                        onrowcommand="grdQuartlyRowDetail_RowCommand" 
                onrowcancelingedit="grdQuartlyRowDetail_RowCancelingEdit" 
                onrowediting="grdQuartlyRowDetail_RowEditing" 
                onrowupdating="grdQuartlyRowDetail_RowUpdating"  >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="25%"  HeaderText="From GP %">
                            <ItemTemplate>
                                <asp:Label ID="lblGPPercentageFrom" runat="server" Text='<%#Eval("GPPercentageFrom")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:TextBox ID="txtGPPercentageFrom" Width="120px" runat="server" Text='<%#Eval("GPPercentageFrom")%>' MaxLength="8" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                             </EditItemTemplate>
                             <FooterTemplate>
                                <asp:TextBox ID="txtGPPercentageFromFooter" Width="120px" runat="server" MaxLength="8" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                             </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="25%" HeaderText="To GP % ">
                            <ItemTemplate >
                                <asp:Label ID="lblGPPercentageTo" runat="server" Text='<%#Eval("GPPercentageTo")%>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:TextBox ID="txtGPPercentageTo" Width="120px" runat="server" Text='<%#Eval("GPPercentageTo")%>' MaxLength="8" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                             </EditItemTemplate>
                             <FooterTemplate>
                                <asp:TextBox ID="txtGPPercentageToFooter" Width="120px" runat="server" MaxLength="8" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                             </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right"  />
                            <HeaderStyle HorizontalAlign="Center"  />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="15%" HeaderText="Reward %">
                            <ItemTemplate>
                                <asp:Label ID="lblRewardPercentage" runat="server" Text='<%#Eval("RewardPercentage")%>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                  <asp:TextBox ID="txtRewardPercentage" Width="120px" runat="server" Text='<%#Eval("RewardPercentage")%>' MaxLength="5" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                             </EditItemTemplate>
                              <FooterTemplate>
                                <asp:TextBox ID="txtRewardPercentageFooter" Width="120px" runat="server" MaxLength="5" BackColor="White" BorderColor="Brown" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox>
                             </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <FooterStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="25%"> 
                                <ItemTemplate>
                                     <asp:ImageButton runat="server" ID="BtnEdit" ImageUrl="~/outer css-js/images/icons8-edit.png" CommandName="Edit" ToolTip="Edit" Height="20px" Width="20px" />
                                  &nbsp;   <asp:ImageButton runat="server" ID="BtnDelete" ImageUrl="~/outer css-js/images/icons8-trash.png" CommandName="Delete" ToolTip="Delete" Height="20px" Width="20px" />
                                 </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:ImageButton runat="server" ID="BtnSave" ImageUrl="~/outer css-js/images/save-icon.png" CommandName="Update" ToolTip="Update" Height="20px" Width="20px" />
                                   &nbsp;  <asp:ImageButton runat="server" ID="BtnCancel" ImageUrl="~/outer css-js/images/Cancel-icon.png" CommandName="Cancel" ToolTip="Cancel" Height="20px" Width="20px" />
                                 </EditItemTemplate>
                                
                                 <FooterTemplate>
                                    <asp:ImageButton runat="server" ID="BtnAddNew" ImageUrl="~/outer css-js/images/icons8-add-new.png" CommandName="AddNew" ToolTip="Add New" Height="20px" Width="20px"  />
                                 </FooterTemplate>
                                 <ItemStyle HorizontalAlign="Center" />

                                 <%--<ItemStyle HorizontalAlign="Center" Width="60px" />--%>
                              </asp:TemplateField>

                    </Columns>

                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
           
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td  colspan="3" > &nbsp; </td>
    </tr>
     <tr>
        <td width="80%" align="left" colspan="2">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" 
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                 />  &nbsp;&nbsp; 
         
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
        <asp:Panel ID="pnlRewardList" runat="server" Width="60%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

        <asp:GridView ID="GrvListAll" runat="server" AutoGenerateColumns="false" 
                ForeColor="#333333"  Width="100%" AllowPaging="True" 
                PageSize="20" onpageindexchanging="GrvListAll_PageIndexChanging" 
                onselectedindexchanged="GrvListAll_SelectedIndexChanged" >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="ID" Visible="false" >

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server"
                                  Text='<%#Eval("RewardSettingID")%>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="5%"  HeaderText="CountryCode" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCountryCode" runat="server" Text='<%#Eval("Countrycode")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Country" >
                            <ItemTemplate>
                                <asp:Label ID="lblCountry" runat="server" Text='<%#Eval("Country")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="5%"  HeaderText="Quarter" >
                            <ItemTemplate>
                                <asp:Label ID="lblQuarter" runat="server" Text='<%#Eval("Quarter")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="5%"  HeaderText="Year" >
                            <ItemTemplate>
                                <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="From GP %" >
                            <ItemTemplate>
                                <asp:Label ID="lblGPPercentageFrom" runat="server" Text='<%#Eval("GPPercentageFrom")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="To GP %" >
                            <ItemTemplate>
                                <asp:Label ID="lblGPPercentageTo" runat="server" Text='<%#Eval("GPPercentageTo")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="Reward %" >
                            <ItemTemplate>
                                <asp:Label ID="lblRewardPercentage" runat="server" Text='<%#Eval("RewardPercentage")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
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


</asp:Content>

