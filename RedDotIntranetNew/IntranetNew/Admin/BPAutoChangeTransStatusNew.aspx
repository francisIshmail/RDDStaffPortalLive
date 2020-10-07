<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="BPAutoChangeTransStatusNew.aspx.cs" Inherits="IntranetNew_Admin_BPAutoChangeTransStatusNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Setup - Auto Change Transactional Status </Lable>
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
        &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlTransStatus" runat="server" Width="100%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table class="table table-stripped" width="100%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="100%" colspan="9" align="center">
          <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " /> 
        </td>
    </tr>
    
        <tr>
            <td width="24%">
               <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Database  &nbsp; </label>  
            </td>
            <td width="76%" colspan="9">
                <asp:DropDownList ID="ddlDatabase" CssClass="form-control"  runat="server" style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="24%">
               <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Make customer <b>Dormant</b> if not transacted for  &nbsp; </label>  
            </td>
            <td width="7%">
                <asp:TextBox ID="txtDormantDays" CssClass="form-control"  runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
            </td>
            <td width="69%" colspan="7"  >
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Days &nbsp; </label>  
            </td>
       </tr>
        <tr>
        <td width="24%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Change status to <b>Soft hold</b> if duedays between &nbsp; </label>  
        </td>
        <td width="7%">
            <asp:TextBox ID="txtFromSoftholdDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="3%">
          <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>TO</b> &nbsp; </label>  
        </td>
         <td width="7%">
            <asp:TextBox ID="txtToSoftholdDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="20%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">after INV pay terms <b>AND</b> Due Amount &nbsp; </label>  
        </td>
        <td width="8%">
            <asp:TextBox ID="txtSoftholdDueAmount"  CssClass="form-control" runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="5%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>OR</b>&nbsp; </label>  
        </td>
        <td width="6%">
            <asp:TextBox ID="txtSoftholdDuePercentage"  CssClass="form-control" runat="server" MaxLength="4" onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="22%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> <b>% </b>of credit limit, whichever is greater &nbsp; </label>  
        </td>
    </tr>

        <tr>
        <td width="24%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Change status to <b>Hard hold</b> if duedays between &nbsp; </label>  
        </td>
        <td width="7%">
            <asp:TextBox ID="txtFromHardHoldDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="3%">
          <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>TO</b> &nbsp; </label>  
        </td>
         <td width="7%">
            <asp:TextBox ID="txtToHardHoldDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="20%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">after INV pay terms <b>AND</b> Due Amount &nbsp; </label>  
        </td>
        <td width="8%">
            <asp:TextBox ID="txtHardholdDueAmount" CssClass="form-control"  runat="server" MaxLength="6"  onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="5%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>OR</b> &nbsp; </label>  
        </td>
        <td width="6%">
            <asp:TextBox ID="txtHardholdDuePercentage" CssClass="form-control"  runat="server" MaxLength="4" onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="22%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> <b>% </b>of credit limit, whichever is greater &nbsp; </label>  
        </td>
    </tr>

        <tr>
        <td width="24%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">Change status to <b>Blocked</b> if duedays between &nbsp; </label>  
        </td>
        <td width="7%">
            <asp:TextBox ID="txtFromBlockedDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="3%">
          <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>TO</b> &nbsp; </label>  
        </td>
         <td width="7%">
            <asp:TextBox ID="txtToBlockedDueDays" CssClass="form-control"  placeholder="0" runat="server" MaxLength="5" onkeypress="javascript:return isNumberKey(event);" style="width:100%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="20%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">after INV pay terms <b>AND</b> Due Amount &nbsp; </label>  
        </td>
        <td width="8%">
            <asp:TextBox ID="txtBlockedDueAmount" CssClass="form-control"  runat="server" MaxLength="6" onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-size:12px; "></asp:TextBox>  
        </td>
        <td width="5%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "><b>OR</b>  &nbsp; </label>  
        </td>
        <td width="6%">
            <asp:TextBox ID="txtBlockedDuePercentage" CssClass="form-control"  runat="server" MaxLength="4"  onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  
        </td>
        <td width="22%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> <b>% </b>of credit limit, whichever is greater &nbsp; </label>  
        </td>
    </tr>

     <tr>
        <td width="100%" align="left" colspan="9">
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" class="btn btn-success"
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnSave_Click" />  &nbsp;&nbsp; 
     
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel"  class="btn btn-default"
                
                style="width:10%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnCancel_Click" />
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

</table>

</asp:Panel>

  </td>
</tr>
<tr>
<td> &nbsp; </td>
</tr>
<tr>
    <td align="center"> 
    
        <asp:Panel ID="pnlTransStatusList" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

        <asp:GridView ID="GRVTransStatusList" runat="server" AutoGenerateColumns="False"  Class="table table-bordered table-condensed table-hover"
                Width="90%" onselectedindexchanged="GRVTransStatusList_SelectedIndexChanged" >
                    <Columns>
                        
                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="DB Name" SortExpression="DBName" >
                            <ItemTemplate>
                                <asp:Label ID="lblDBName" runat="server" Text='<%#Eval("DBName")%>'></asp:Label>
                            </ItemTemplate>
                           <ItemStyle HorizontalAlign="Center"  Width="15%"   />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="15%" HeaderText="Transactional Status" SortExpression="Status">
                            <ItemTemplate >
                                <asp:Label ID="lblTransStatus" runat="server" Text='<%#Eval("Status")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  Width="15%"   />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%" HeaderText="Due Days" SortExpression="DueDays">
                            <ItemTemplate>
                                <asp:Label ID="lblDueDays" runat="server" Text='<%#Eval("DueDays")%>' ></asp:Label>
                            </ItemTemplate>
                             <ItemStyle HorizontalAlign="Center"  Width="10%"   />
                             <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                         <asp:TemplateField ItemStyle-Width="10%" HeaderText="Due Amount" SortExpression="DueAmount" >
                            <ItemTemplate>
                                <asp:Label ID="lblDueAmount" runat="server" Text='<%#Eval("DueAmount")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  Width="10%"   />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>
                       
                       <asp:TemplateField ItemStyle-Width="7%" HeaderText="CL %" SortExpression="CLPercentage">
                            <ItemTemplate>
                                <asp:Label ID="lblCLPercentage" runat="server" Text='<%#Eval("CLPercentage")%>' ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  Width="7%"   />
                            <HeaderStyle CssClass="gvHeaderCenter" />
                        </asp:TemplateField>

                        <asp:CommandField ShowSelectButton="True" SelectText="Edit" 
                            ControlStyle-ForeColor="Blue" ButtonType="Link" ItemStyle-Width="8%" >

                        <ControlStyle ForeColor="Blue" />
                        <ItemStyle Width="8%" />
                        </asp:CommandField>

                    </Columns>

                </asp:GridView>

        </asp:Panel>

    </td>
</tr>

</table>

<style type="text/css">

.gvHeaderCenter { text-align: center; }

</style>

</asp:Content>

