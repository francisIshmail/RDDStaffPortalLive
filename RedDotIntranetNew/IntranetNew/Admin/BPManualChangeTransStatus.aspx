<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="BPManualChangeTransStatus.aspx.cs" Inherits="IntranetNew_Admin_BPManualChangeTransStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>

<script>
    $(document).ready(function () {
        $("#myInput").on("keyup", function () {
            var value = $(this).val().toLowerCase();
            $("#ctl00_ContentPlaceHolder1_GRVManulStatusChange tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            });
        });
    });
</script>--%>

<script type="text/javascript">
    function getConfirmationOnDelete() {
        return confirm("Are you sure you want to Delete this record, alerts will be deleted as well ?");
    }
</script>
<asp:ScriptManager ID="SMManualChange" runat="server" > </asp:ScriptManager>
<asp:UpdatePanel ID="UPManualStatusChange" runat="server">
<ContentTemplate>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Setup - To Manually Change Transactional Status </Lable>
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
<%--<tr>
    <td width="100%" >&nbsp;</td>
</tr>
<tr>
    <td width="100%" align="justify" >
    <input id="myInput" style="height:25px;width:300px;"    type="text" placeholder="Search..">
    </td>
</tr>--%>

<tr>
    <td width="100%" >&nbsp;</td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlManualTransStatus" runat="server" Width="85%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">


        <asp:GridView ID="GRVManulStatusChange" runat="server" 
            AutoGenerateColumns="False" ShowFooter="True" Class="table table-bordered table-condensed table-hover"
            AllowSorting="true"  
            onrowcommand="GRVManulStatusChange_RowCommand" 
            onrowcancelingedit="GRVManulStatusChange_RowCancelingEdit" 
            onrowdeleting="GRVManulStatusChange_RowDeleting" 
            onrowediting="GRVManulStatusChange_RowEditing" 
            onrowupdating="GRVManulStatusChange_RowUpdating" 
            onsorting="GRVManulStatusChange_Sorting" >
         <Columns>

                <asp:TemplateField HeaderText="Id" SortExpression="Id">
                    <ItemTemplate><asp:Label ID="lblID" Text='<%#Eval("Id")%>' runat="server"></asp:Label></ItemTemplate>
                </asp:TemplateField>

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
               </asp:TemplateField>

                <asp:TemplateField HeaderText="From Status" SortExpression="FromStatus" >
                        <ItemTemplate> <asp:Label ID="lblFromStatus" Text='<%#Eval("FromStatus")%>'  runat="server"></asp:Label> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlFromStatus" runat="server" Text='<%#Eval("FromStatus")%>' CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="Active"     Text="Active"></asp:ListItem> 
                                <asp:ListItem Value="Soft hold" Text="Soft hold"></asp:ListItem> 
                                <asp:ListItem Value="Hard hold" Text="Hard hold"></asp:ListItem> 
                                <asp:ListItem Value="Blocked"   Text="Blocked"></asp:ListItem> 
                                <asp:ListItem Value="Dormant"   Text="Dormant"></asp:ListItem> 
                             </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFooterFromStatus" runat="server" CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="Active"    Text="Active"></asp:ListItem> 
                                <asp:ListItem Value="Soft hold" Text="Soft hold"></asp:ListItem> 
                                <asp:ListItem Value="Hard hold" Text="Hard hold"></asp:ListItem> 
                                <asp:ListItem Value="Blocked"   Text="Blocked"></asp:ListItem> 
                                <asp:ListItem Value="Dormant"   Text="Dormant"></asp:ListItem> 
                             </asp:DropDownList>
                        </FooterTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="To Status" SortExpression="ToStatus">
                        <ItemTemplate> <asp:Label ID="lblToStatus" Text='<%#Eval("ToStatus")%>'  runat="server"></asp:Label>  </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlToStatus" runat="server" Text='<%#Eval("ToStatus")%>' CssClass="form-control" >  
                                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                    <asp:ListItem Value="Active"    Text="Active"></asp:ListItem> 
                                    <asp:ListItem Value="Soft hold" Text="Soft hold"></asp:ListItem> 
                                    <asp:ListItem Value="Hard hold" Text="Hard hold"></asp:ListItem> 
                                    <asp:ListItem Value="Blocked"   Text="Blocked"></asp:ListItem> 
                                    <asp:ListItem Value="Dormant"   Text="Dormant"></asp:ListItem> 
                                    <asp:ListItem Value="Closed"    Text="Closed"></asp:ListItem> 
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlFooterToStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                    <asp:ListItem Value="Active"    Text="Active"></asp:ListItem> 
                                    <asp:ListItem Value="Soft hold" Text="Soft hold"></asp:ListItem> 
                                    <asp:ListItem Value="Hard hold" Text="Hard hold"></asp:ListItem> 
                                    <asp:ListItem Value="Blocked"   Text="Blocked"></asp:ListItem> 
                                    <asp:ListItem Value="Dormant"   Text="Dormant"></asp:ListItem> 
                                    <asp:ListItem Value="Closed"    Text="Closed"></asp:ListItem> 
                             </asp:DropDownList>
                        </FooterTemplate>
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Can CA" SortExpression="CanCA" >
                        <ItemTemplate>  <asp:CheckBox Id="chkCanCA" runat="server" Checked='<%#Eval("CanCA")%>'  Enabled="false"  />   </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanCA" runat="server" Checked='<%#Eval("CanCA")%>' CssClass="form-control"/> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanCA" runat="server" CssClass="form-control" /> 
                        </FooterTemplate>
                        <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Can CM" SortExpression="CanCM" >
                        <ItemTemplate> <asp:CheckBox Id="chkCanCM" runat="server" Checked='<%#Eval("CanCM")%>'   Enabled="false" />  </ItemTemplate>
                         <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanCM" runat="server" Checked='<%#Eval("CanCM")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanCM" runat="server" CssClass="form-control" /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Can CR" SortExpression="CanCR">
                        <ItemTemplate> <asp:CheckBox Id="chkCanCR" runat="server" Checked='<%#Eval("CanCR")%>'   Enabled="false" />  </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanCR" runat="server" Checked='<%#Eval("CanCR")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanCR" runat="server" CssClass="form-control"  /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

               <asp:TemplateField HeaderText="Can HOF" SortExpression="CanHOF">
                        <ItemTemplate> <asp:CheckBox Id="chkCanHOF" runat="server" Checked='<%#Eval("CanHOF")%>'  Enabled="false" />  </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanHOF" runat="server" Checked='<%#Eval("CanHOF")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanHOF" runat="server" CssClass="form-control"  /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Can HOIS" SortExpression="CanHOIS">
                        <ItemTemplate> <asp:CheckBox Id="chkCanHOIS" runat="server" Checked='<%#Eval("CanHOIS")%>'  Enabled="false" />  </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanHOIS" runat="server" Checked='<%#Eval("CanHOIS")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanHOIS" runat="server" CssClass="form-control"  /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>
               
               <asp:TemplateField HeaderText="Can COO" SortExpression="CanCOO">
                        <ItemTemplate> <asp:CheckBox Id="chkCanCOO" runat="server" Checked='<%#Eval("CanCOO")%>'  Enabled="false" />  </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanCOO" runat="server" Checked='<%#Eval("CanCOO")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanCOO" runat="server" CssClass="form-control" /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

                <asp:TemplateField HeaderText="Can CEO" SortExpression="CanCEO">
                        <ItemTemplate> <asp:CheckBox Id="chkCanCEO" runat="server" Checked='<%#Eval("CanCEO")%>'  Enabled="false" />  </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:CheckBox Id="chkEditCanCEO" runat="server" Checked='<%#Eval("CanCEO")%>' CssClass="form-control"  /> 
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:CheckBox Id="chkFooterCanCEO" runat="server" CssClass="form-control"  /> 
                        </FooterTemplate>
                         <FooterStyle HorizontalAlign="Center" />
               </asp:TemplateField>

               <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnEdit" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Edit" class="btn btn-info" ></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="BtnDelete" runat="server" CausesValidation="false" 
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

