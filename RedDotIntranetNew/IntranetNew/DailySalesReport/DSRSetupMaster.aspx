<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="DSRSetupMaster.aspx.cs" Inherits="IntranetNew_DailySalesReport_FunnelSetupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function getConfirmationOnDelete() {
        return confirm("Are you sure you want to Delete ?");
    }
</script>
<asp:ScriptManager ID="SMAutoCLChangeAlert" runat="server" > </asp:ScriptManager>
<asp:UpdatePanel ID="UPAutoCLStatusChangeAlert" runat="server">
<ContentTemplate>

<table width="100%" align="center"> 
    <caption>
        -
        <tr>
            <td align="center" width="100%">
                <lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;">
                    &nbsp;&nbsp;&nbsp;  SETUP - Call Type
                </lable>
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
            <td align="center" width="100%">
                <asp:Label ID="lblMsg" runat="server" 
                    style="color: #d71313;font-size:14px;font-weight: bold;font-family: Raleway;" 
                    Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="100%">
                &nbsp;</td>
        </tr>      <%--        onsorting="GRVAutoCLStatusChange_Sorting"--%>
        <tr>
            <td align="center" width="100%">
                <asp:Panel ID="pnlAutoCLStatus" runat="server" BorderColor="Red" 
                    BorderWidth="1px" EnableTheming="true" Height="25%" Width="40%" >
                    <asp:GridView ID="GRVAutoCLStatusChange" runat="server" AllowSorting="true"    DataKeyNames="Id"
                        AutoGenerateColumns="False"   
                        Class="table table-bordered table-condensed table-hover" 
                        onrowcancelingedit="GRVAutoCLStatusChange_RowCancelingEdit" 
                        onrowcommand="GRVAutoCLStatusChange_RowCommand" 
                        onrowdeleting="GRVAutoCLStatusChange_RowDeleting" 
                        onrowediting="GRVAutoCLStatusChange_RowEditing" 
                        onrowupdating="GRVAutoCLStatusChange_RowUpdating" 
                       
                      ShowFooter="True" >
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No" SortExpression="Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("Id")%>' Visible="false"></asp:Label>
                                      <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CALL TYPE"   SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblfunnelst" runat="server" Text='<%#Eval("CallStatus")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtfunnelstedit" runat="server" class="form-control" autocomplete="off"  Style="font-size: medium;"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtfunnelFooter" runat="server" class="form-control" autocomplete="off"  Style="font-size: medium;"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateField>

                                <asp:TemplateField HeaderText="ACTION" SortExpression="ACTION">
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
    </caption>

</table>
       
 </ContentTemplate>
</asp:UpdatePanel>   

</asp:Content>

