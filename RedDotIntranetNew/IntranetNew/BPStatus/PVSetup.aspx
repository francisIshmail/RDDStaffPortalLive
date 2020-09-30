<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="PVSetup.aspx.cs" Inherits="IntranetNew_BPStatus_PVSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>


<script type="text/javascript">

    $(document).ready(function () {
        $(function () {
            $('[id*=ddlCountry]').multiselect({
                includeSelectAllOption: true,
                buttonWidth: '230px'
            });
        });
    });

</script>


<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" colspan="2"  >
         <Lable id="lblformName" runat="server" style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Payment Voucher - Setup  </Lable>
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
        <td width="100%" align="center" colspan="2" >
              <asp:Label ID="lblMsg" runat="server" style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

 <tr>
    <td width="100%" align="center" >
    
    <asp:Panel ID="pnlPVsetup" runat="server" Width="60%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
    <%--class="table table-stripped condensed"  --%>
        <table width="97%" align="center" cellpadding="3px" cellspacing="3px" class="table table-stripped condensed" >
            <tr>
                <td width="20%" >  <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Employee  &nbsp; </label>   </td>
                <td width="30%"> 
                    <asp:DropDownList ID="ddlEmployees" runat="server" class="form-control" AutoPostBack="true"
                        onselectedindexchanged="ddlEmployees_SelectedIndexChanged" ></asp:DropDownList>
                 </td>
                <td width="20%"></td>
                <td width="30%"></td>
            </tr>

            <tr>
                <td width="20%" >  <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Employee Name  &nbsp; </label>   </td>
                <td width="30%"> 
                    <asp:TextBox ID="txtEmpDisplayName" runat="server" class="form-control" Font-Size="Medium" ></asp:TextBox>
                 </td>
                <td width="20%"></td>
                <td width="30%"></td>
            </tr>

            <tr>
                <td width="20%" >  <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Role  &nbsp; </label>   </td>
                <td width="80%" colspan="3"> 
                    <asp:RadioButton ID="rbCA" runat="server" GroupName="UserRole" Text="CA"  /> &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbCM" runat="server" GroupName="UserRole" Text="CM" /> &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbCFO" runat="server" GroupName="UserRole" Text="CFO" /> &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbEmp" runat="server" GroupName="UserRole" Text="Employee" /> 
                 </td>
            </tr>

            <tr>
                <td width="20%" >  <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Country  &nbsp; </label>   </td>
                <td width="30%"> 
                     <asp:ListBox ID="ddlCountry" runat="server" SelectionMode="Multiple" 
                        class="input-control" style="height:25px" Width="150px"  >
                    </asp:ListBox>
                 </td>
                <td width="20%"></td>
                <td width="30%"></td>
            </tr>

             <tr>
                <td width="100%" colspan="4">  
                    <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" 
                        Font-Bold="true" Font-Size="Medium" 
                            style="font-family: Raleway;height:38px;width:150px;" TabIndex="19" 
                        OnClientClick="return Validate();" onclick="btnSave_Click"  />
                </td>
            </tr>


        </table>
    </asp:Panel>
    </td>
 </tr>



</table>


</asp:Content>

