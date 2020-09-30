<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="BPWithholdingTaxSlab.aspx.cs" Inherits="IntranetNew_Admin_BPWithholdingTaxSlab" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
    function getConfirmationOnDelete() {
        return confirm("Are you sure you want to Delete this record, alerts will be deleted as well ?");
    }
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
<asp:ScriptManager ID="SMWithholdingTax" runat="server" > </asp:ScriptManager>
<asp:UpdatePanel ID="UPWithholdingTax" runat="server">
<ContentTemplate>

<table width="90%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Setup - Withholding Tax  </Lable>
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
    
<asp:Panel ID="pnlwithholdingTax" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">


        <asp:GridView ID="GRVWithholdingTax" runat="server" 
            AutoGenerateColumns="False" ShowFooter="True" Class="table table-bordered table-condensed table-hover"
            AllowSorting="true"  
            onrowcommand="GRVWithholdingTax_RowCommand" 
            onrowcancelingedit="GRVWithholdingTax_RowCancelingEdit" 
            onrowdeleting="GRVWithholdingTax_RowDeleting" 
            onrowediting="GRVWithholdingTax_RowEditing" 
            onrowupdating="GRVWithholdingTax_RowUpdating" 
            onsorting="GRVWithholdingTax_Sorting" >
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

                <asp:TemplateField HeaderText="From Date" SortExpression="FromDate" >
                        <ItemTemplate> <asp:Label ID="lblFromDate" Text='<%#Eval("FromDate")%>'  runat="server"></asp:Label> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFromDate" runat="server" Text='<%#Eval("FromDate")%>' placeholder="mm/dd/yyyy" CssClass="form-control" ></asp:TextBox>
                             <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtFromDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFromDateFooter" runat="server" Text='<%#Eval("FromDate")%>' placeholder="mm/dd/yyyy" CssClass="form-control" ></asp:TextBox>
                            <cc1:CalendarExtender ID="txtFromDateFooter_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtFromDateFooter" DaysModeTitleFormat="dd/MM/yyyy" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                        </FooterTemplate>
               </asp:TemplateField>

               <asp:TemplateField HeaderText="To Date" SortExpression="ToDate" >
                        <ItemTemplate> <asp:Label ID="lblToDate" Text='<%#Eval("ToDate")%>'  runat="server"></asp:Label> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtToDate" runat="server" Text='<%#Eval("ToDate")%>' placeholder="mm/dd/yyyy" CssClass="form-control" ></asp:TextBox>
                            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtToDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtToDateFooter" runat="server" Text='<%#Eval("ToDate")%>' placeholder="mm/dd/yyyy" CssClass="form-control" ></asp:TextBox>
                            <cc1:CalendarExtender ID="txtToDateFooter_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtToDateFooter" DaysModeTitleFormat="dd/MM/yyyy" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                        </FooterTemplate>
               </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="Withholding Tax %" SortExpression="WTaxPercentage" >
                        <ItemTemplate> <asp:Label ID="lblWTaxPercentage" Text='<%#Eval("WTaxPercentage")%>'   runat="server"></asp:Label> </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtWTaxPercentage" runat="server" onkeypress="javascript:return isNumberKey(event);" Text='<%#Eval("WTaxPercentage")%>' placeholder="0.0" CssClass="form-control" ></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtWTaxPercentageFooter" runat="server" onkeypress="javascript:return isNumberKey(event);" Text='<%#Eval("WTaxPercentage")%>' placeholder="0.0" CssClass="form-control" ></asp:TextBox>
                        </FooterTemplate>
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

