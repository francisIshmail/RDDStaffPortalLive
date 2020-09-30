<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="salesemployeemaster.aspx.cs" Inherits="IntranetNew_Targets_SalesEmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Sales Employee Master  </Lable>
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
    
<asp:Panel ID="pnlForms" runat="server" Width="95%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="95%" align="center" cellpadding="3px" cellspacing="3px" >
    
   
    
 
      <tr>
        
        <td width="30%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">ID  &nbsp; </label>  
        </td>
        <td width="40%" >
               <asp:TextBox ID="txtid" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " Enabled="false" ></asp:TextBox> 
       
        </td>
        <td width="30%">
            <asp:Label ID="lblMenuID" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
        
    </tr>

      <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Name<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
            <asp:TextBox ID="txtname" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
         </td>
         <td width="30%" align="left"> <asp:RequiredFieldValidator ID="reqname" runat="server" ControlToValidate="txtname" ErrorMessage="Enter Name" ></asp:RequiredFieldValidator>
       
        </td>
        
    </tr>

     <tr>
     
        <td width="30%" align="left">
            &nbsp;&nbsp; Short Name<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
            <asp:TextBox ID="txtshortname" runat="server" 
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  AutoPostBack="true"
                ontextchanged="txtshortname_TextChanged"></asp:TextBox> 
         
         </td>
          <td width="30%" align="left"> <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtname" ErrorMessage="Enter Short Name" ></asp:RequiredFieldValidator>
       
        </td>
        
    </tr>

      <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Email<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%" >
            <asp:TextBox ID="txtemail" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
         </td>
           <td width="30%" align="left"> <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtemail" ErrorMessage="Enter Email Id" ></asp:RequiredFieldValidator>
       
        </td>
        
    </tr>

      <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Designation<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
            <asp:DropDownList ID="ddlDesignation" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "   > </asp:DropDownList>
        
         </td>
         <td width="30%" align="left">  <asp:RequiredFieldValidator ID="reqdesignation" InitialValue="0"  runat="server" ControlToValidate="ddlDesignation" ErrorMessage="Select Designation" ></asp:RequiredFieldValidator>
       
        </td>
         
    </tr>

      <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Manager<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
          <asp:DropDownList ID="ddlManager" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  > </asp:DropDownList> </td>
         <td width="30%" align="left">
         <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlManager" ErrorMessage="Select Manage" ></asp:RequiredFieldValidator>--%>
       
        </td>
        
    </tr>
     <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Forecast From<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
         <asp:TextBox ID="txtforecastfrm" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;  " ></asp:TextBox> 
      
        </td>
         <td width="30%" align="left">
        </td>
        
    </tr>
     <tr>
      
        <td width="30%" align="left">
            &nbsp;&nbsp; Intranet Login Username<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
        <td width="40%"  >
         <asp:DropDownList ID="ddlMemUser" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " > </asp:DropDownList>
        
            <%-- <asp:TextBox ID="txtmembershipunm" runat="server" style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;  " Enabled="false"></asp:TextBox> 
       --%>
        </td>
         <td width="30%" align="left">  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0"  runat="server" ControlToValidate="ddlMemUser" ErrorMessage="Select MembershipUser" ></asp:RequiredFieldValidator>
       
        </td>
        
    </tr>
     <tr >
     <td width="30%" align="left">
            &nbsp;&nbsp;Country<label 
                style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> 
            &nbsp;
            </label>
        </td>
     <td colspan="2" >
                               <asp:CheckBoxList ID="chkListCountries" runat="server" 
                                   RepeatDirection="Horizontal"  RepeatColumns="6"  Font-Size="Small" onselectedindexchanged="chkListCountries_SelectedIndexChanged" 
                                   ></asp:CheckBoxList>
                              </td>
     </tr>

      <tr>
     
        <td width="100%" align="center" colspan="3"> &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save"  OnClick="BtnSave_Click" style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " />  &nbsp;&nbsp; 
            <asp:Button ID="BtnCancel" runat="server" Text="Cancel" OnClick="BtnCancel_Click" style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " CausesValidation="false" />
        </td>
      
    </tr>
    
   
    

    <tr>
        
        <td align="left" colspan="3">
          <asp:HiddenField ID="selcountry" runat="server" />
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

<tr>
    <td> &nbsp;</td>
</tr>





</table>
<table width="100%" align="center">
 <tr>
    <td width="100%" align="center" >
        <asp:Panel ID="pnlFormList" runat="server" Width="99%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="false" PageSize="20"
                ForeColor="#333333"  Width="100%" AllowPaging="True" 
                onselectedindexchanged="Gridview1_SelectedIndexChanged" 
                OnRowCommand="GridView1_RowCommand" OnRowDeleting="Griview1_RowDeleting" OnPageIndexChanging="Griview1_PageIndexChanging"
                >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="10%" HeaderText="ID">

                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Height="25px" Font-Size="Smaller"
                                  Text='<%#Eval("Id")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="20%"  HeaderText="Name"  >
                            <ItemTemplate>
                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("alias")%>' Height="25px" Font-Size="small"></asp:Label>
                            </ItemTemplate>
                             <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Left"/>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Sales Person" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblshortnm" runat="server" Text='<%#Eval("salesperson")%>' Height="25px" Font-Size="small"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                       
                         <asp:TemplateField ItemStyle-Width="20%"  HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("Email")%>' Height="25px" Font-Size="small"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Left"/>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Designation" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbldesignationid" runat="server" Text='<%#Eval("desid")%>' Height="25px" Font-Size="small"></asp:Label>
                            </ItemTemplate>
                             <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                       <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Designation">
                            <ItemTemplate>
                                <asp:Label ID="lbldesignation" runat="server" Text='<%#Eval("Designation")%>' Height="25px" Font-Size="small"></asp:Label>
                            </ItemTemplate>
                             <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Left"/>
                        </asp:TemplateField>


                         <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Manager" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblmanagerid" runat="server" Text='<%#Eval("manid")%>' Height="25px"  Font-Size="small"></asp:Label>
                            </ItemTemplate>
                             <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>
                        </asp:TemplateField>


                       <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Manager">
                            <ItemTemplate>
                                <asp:Label ID="lblmanager" runat="server" Text='<%#Eval("Manager")%>' Height="25px"  Font-Size="small"></asp:Label>
                            </ItemTemplate>
                             <ItemStyle Font-Size="Smaller" HorizontalAlign="Left" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Left"/>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Membership User">
                            <ItemTemplate>
                                <asp:Label ID="lblmembuser" runat="server" Text='<%#Eval("membershipuser")%>' Height="25px"  Font-Size="small"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                       
                       <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Forecast From" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblforecastfrom" runat="server" Text='<%#Eval("forecast_from")%>' Height="25px"  Font-Size="small"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>
                        </asp:TemplateField>
                       
                        

                        <asp:CommandField ShowSelectButton="True" SelectText="Edit" ControlStyle-ForeColor="Blue" ControlStyle-Font-Size="Small"  ButtonType="Link" ItemStyle-Width="5%"  ItemStyle-Font-Size="Medium" ItemStyle-Height="25px"  />

                         <asp:TemplateField ItemStyle-Width="5%" >

                            <ItemTemplate>
                                <asp:LinkButton  ID="lnkdelete" runat="server"  
                                  CommandArgument='<%#Eval("Id")%>' Text="Delete" CommandName="Delete" ForeColor="Blue"   CausesValidation="false" OnClientClick="return <b> confirm('Are you sure  to delete this entry') </b>"  Height="25px" Font-Size="small"  ></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                            <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center"/>

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

