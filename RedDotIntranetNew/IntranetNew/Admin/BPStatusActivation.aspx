<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="BPStatusActivation.aspx.cs" Inherits="IntranetNew_Admin_BPStatusActivation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function getConfirmationOnDelete() {
        return confirm("Are you sure you want to Delete ?");
    }
</script>
<asp:ScriptManager ID="SMBPStatusActivation" runat="server" > </asp:ScriptManager>
<asp:UpdatePanel ID="UPBPStatusActivation" runat="server">
<ContentTemplate>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Activate Customer Status </Lable>
    </td>
</tr>

<tr>
        <td width="100%">
        &nbsp;
        </td>
</tr>
<tr>
        <td width="100%">
        &nbsp;
        </td>
</tr>
<tr>
        <td width="100%" align="center" >
            <asp:Label ID="lblMsg" runat="server" Text="" style="color: #d71313;font-size:14px;font-weight: bold;font-family: Raleway;" ></asp:Label>
        </td>
</tr>
<tr>
    <td width="100%" >&nbsp;</td>
</tr>
<tr>
    <td width="100%" >&nbsp;</td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlBPStatusActivation" runat="server" Width="40%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">


        <asp:GridView ID="GRVBPStatusActivation" runat="server" 
            AutoGenerateColumns="False" ShowFooter="True" Class="table table-bordered table-condensed table-hover"
            AllowSorting="true"  
            onrowcommand="GRVBPStatusActivation_RowCommand" 
            onrowcancelingedit="GRVBPStatusActivation_RowCancelingEdit" 
            onrowdeleting="GRVBPStatusActivation_RowDeleting" 
            onrowediting="GRVBPStatusActivation_RowEditing" 
            onrowupdating="GRVBPStatusActivation_RowUpdating" 
            onsorting="GRVBPStatusActivation_Sorting" >
         <Columns>

                <asp:TemplateField HeaderText="Id" SortExpression="Id"  >
                    <ItemTemplate><asp:Label ID="lblID" Text='<%#Eval("Id")%>' runat="server"></asp:Label></ItemTemplate>
                </asp:TemplateField>
<%--
                 <asp:TemplateField HeaderText="Database" HeaderStyle-HorizontalAlign="Center" SortExpression="DBName" >
                        <ItemTemplate> <asp:Label ID="lblDBName" Text='<%#Eval("DBName")%>' runat="server"></asp:Label> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDBName" runat="server" Text='<%#Eval("DBName")%>' CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE"     Text="SAPAE"></asp:ListItem> 
                                <asp:ListItem Value="SAPKE"     Text="SAPKE"></asp:ListItem> 
                                <asp:ListItem Value="SAPTZ"     Text="SAPTZ"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG"     Text="SAPUG"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM"     Text="SAPZM"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE-TEST"     Text="SAPAE-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPKE-TEST"     Text="SAPKE-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPTZ-TEST"     Text="SAPTZ-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG-TEST"     Text="SAPUG-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM-TEST"     Text="SAPZM-TEST"></asp:ListItem> 
                             </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFooterDBName" runat="server" CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE"     Text="SAPAE"></asp:ListItem> 
                                <asp:ListItem Value="SAPKE"     Text="SAPKE"></asp:ListItem> 
                                <asp:ListItem Value="SAPTZ"     Text="SAPTZ"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG"     Text="SAPUG"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM"     Text="SAPZM"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE-TEST"     Text="SAPAE-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPKE-TEST"     Text="SAPKE-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPTZ-TEST"     Text="SAPTZ-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG-TEST"     Text="SAPUG-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM-TEST"     Text="SAPZM-TEST"></asp:ListItem> 
                             </asp:DropDownList>
                        </FooterTemplate>
               </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Country" SortExpression="Country">
                        <ItemTemplate> <%#Eval("Country")%> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCountry" runat="server" Text='<%#Eval("Country")%>' CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="ANG"     Text="ANG"></asp:ListItem> 
                                <asp:ListItem Value="BOT"     Text="BOT"></asp:ListItem> 
                                <asp:ListItem Value="DXB"     Text="DXB"></asp:ListItem> 
                                <asp:ListItem Value="ET"      Text="ET"></asp:ListItem> 
                                <asp:ListItem Value="KE"      Text="KE"></asp:ListItem> 
                                <asp:ListItem Value="MAL"     Text="MAL"></asp:ListItem> 
                                <asp:ListItem Value="MOZ"     Text="MOZ"></asp:ListItem> 
                                <asp:ListItem Value="NA"      Text="NA"></asp:ListItem> 
                                <asp:ListItem Value="RW"      Text="RW"></asp:ListItem> 
                                <asp:ListItem Value="TZ"      Text="TZ"></asp:ListItem> 
                                <asp:ListItem Value="UG"      Text="UG"></asp:ListItem> 
                                <asp:ListItem Value="ZAM"     Text="ZAM"></asp:ListItem> 
                                <asp:ListItem Value="ZIM"     Text="ZIM"></asp:ListItem> 
                                <asp:ListItem Value="SA"      Text="SA"></asp:ListItem> 
                             </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFooterCountry" runat="server" CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="ANG"     Text="ANG"></asp:ListItem> 
                                <asp:ListItem Value="BOT"     Text="BOT"></asp:ListItem> 
                                <asp:ListItem Value="DXB"     Text="DXB"></asp:ListItem> 
                                <asp:ListItem Value="ET"      Text="ET"></asp:ListItem> 
                                <asp:ListItem Value="KE"      Text="KE"></asp:ListItem> 
                                <asp:ListItem Value="MAL"     Text="MAL"></asp:ListItem> 
                                <asp:ListItem Value="MOZ"     Text="MOZ"></asp:ListItem> 
                                <asp:ListItem Value="NA"      Text="NA"></asp:ListItem> 
                                <asp:ListItem Value="RW"      Text="RW"></asp:ListItem> 
                                <asp:ListItem Value="TZ"      Text="TZ"></asp:ListItem> 
                                <asp:ListItem Value="UG"      Text="UG"></asp:ListItem> 
                                <asp:ListItem Value="ZAM"     Text="ZAM"></asp:ListItem> 
                                <asp:ListItem Value="ZIM"     Text="ZIM"></asp:ListItem> 
                                <asp:ListItem Value="SA"      Text="SA"></asp:ListItem>
                             </asp:DropDownList>
                        </FooterTemplate>
               </asp:TemplateField>
                
               <asp:TemplateField HeaderText="Activate" SortExpression="IsActive"  >
                        <ItemTemplate>  <asp:CheckBox Id="chkActivate" runat="server" Checked='<%#Eval("IsActive")%>'  Enabled="false"  />   </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditActivate" runat="server" Checked='<%#Eval("IsActive")%>' CssClass="form-control"/> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterActivate" runat="server" CssClass="form-control" /> 
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

               <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Edit" class="btn btn-info" ></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="BtnDelete" runat="server" CausesValidation="false" Enabled="false"
                                CommandName="Delete" Text="Delete" class="btn btn-danger"  OnClientClick="return getConfirmationOnDelete();"  ></asp:LinkButton>
                        </ItemTemplate>
                         <EditItemTemplate>
                                     <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="False"
                                         CommandName="Update" Text="Update" class="btn btn-success" ></asp:LinkButton>
                                     &nbsp;<asp:LinkButton ID="BtnCancel" runat="server" CausesValidation="False" 
                                         CommandName="Cancel" Text="Canel" class="btn btn-default" ></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btInsert" runat="server" Text="Add New" Class="btn btn-success" CommandName="Add" />
                        </FooterTemplate>
               </asp:TemplateField>

         </Columns>
        </asp:GridView>

</asp:Panel>

  </td>
</tr>

</table>
       
 </ContentTemplate>
</asp:UpdatePanel>   

 
</asp:Content>





