<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="SAPCredentials.aspx.cs" Inherits="IntranetNew_BPStatus_SAPCredentials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">

    function saveSAPCredentials() {

        var ddlDBName = document.getElementById('<%= ddlDBName.ClientID %>').value;
        var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
        var txtSAPUserName = document.getElementById('<%= txtSAPLoginName.ClientID %>').value;
        var txtSAPAEpwd = document.getElementById('<%= txtSAPAEpwd.ClientID %>').value;
        var txtSAPKEpwd = document.getElementById('<%= txtSAPKEpwd.ClientID %>').value;
        var txtSAPTZpwd = document.getElementById('<%= txtSAPTZpwd.ClientID %>').value;
        var txtSAPUGpwd = document.getElementById('<%= txtSAPUGpwd.ClientID %>').value;
        var txtSAPZMpwd = document.getElementById('<%= txtSAPZMpwd.ClientID %>').value;

        if (ddlDBName == "--Select--") {
            alert(" Please select default database ");
            return false;
        }

        if (ddlCountry == "--Select--") {
            alert(" Please select default country ");
            return false;
        }

        if (txtSAPUserName === "") {
            alert("Please enter SAP login user name");
            return false;
        }

        if (txtSAPAEpwd === "" && txtSAPKEpwd === "" && txtSAPTZpwd === "" && txtSAPUGpwd === "" && txtSAPZMpwd === "") {
            alert("Please enter password for atleast one database")
            return false;
        }

        $.ajax({
            type: "POST",
            url: "SAPCredentials.aspx/SaveData",
            // data: stringData,
            data: "{ 'defaultDB':'" + ddlDBName + "','defaultCountry':'"+ddlCountry+"','SAPUserName': '" + txtSAPUserName + "','SAPAEpwd':'" + txtSAPAEpwd + "','SAPKEpwd':'" + txtSAPKEpwd + "','SAPTZpwd':'" + txtSAPTZpwd + "','SAPUGpwd':'" + txtSAPUGpwd + "','SAPZMpwd':'" + txtSAPZMpwd + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSucces,
            error: OnError
        });

        function OnSucces(response) {
            if (response.d == "1") {
                alert('SAP credentials saved successfully');
                //reset();
            }
            else {
                alert(response.d);
            }
        }
        function OnError(response) {
            alert(response.d);
        }


    }

</script>

<table width="100%" align="center"  > 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; SAP Credentials </Lable>
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
    
<asp:Panel ID="pnlSAPCredentials" runat="server" Width="90%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">


<table  id="tblCustomers" class="table table-stripped table-condensed" width="100%" >
        <tr class="info">
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;">Default database </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;">Default Country </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;">SAP Login UserName  </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;"> SAPAE password </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; "> SAPKE password </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; "> SAPTZ password </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; "> SAPUG password </label>  </td>
            <td width="12%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; "> SAPZM password </label>  </td>
        </tr>
        <tr >
            <td width="12%" >   
                     <asp:DropDownList ID="ddlDBName" runat="server" CssClass="form-control" > 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE"     Text="SAPAE"></asp:ListItem>
                                <asp:ListItem Value="SAPKE"     Text="SAPKE"></asp:ListItem> 
                                <%--<asp:ListItem Value="SAPKE-TEST"     Text="SAPKE-TEST"></asp:ListItem> --%>
                                <asp:ListItem Value="SAPTZ"     Text="SAPTZ"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG"     Text="SAPUG"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM"     Text="SAPZM"></asp:ListItem> 
                    </asp:DropDownList>
            </td>
            <td  width="12%">
                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" > </asp:DropDownList>
            </td>
            <td width="12%"> <asp:TextBox ID="txtSAPLoginName" style="font-size:14px;" runat="server" placeholder="SAP UserName" class="form-control" /> </td>
            <td width="12%"> <asp:TextBox ID="txtSAPAEpwd" style="font-size:14px;" runat="server" placeholder="SAPAE password" class="form-control" /> </td>
            <td width="12%"> <asp:TextBox ID="txtSAPKEpwd" style="font-size:14px;" runat="server" placeholder="SAPKE password" class="form-control" /> </td>
            <td width="12%"> <asp:TextBox ID="txtSAPTZpwd" style="font-size:14px;" runat="server" placeholder="SAPTZ password" class="form-control" /> </td>
            <td width="12%"> <asp:TextBox ID="txtSAPUGpwd" style="font-size:14px;" runat="server" placeholder="SAPUG password" class="form-control" /> </td>
            <td width="12%"> <asp:TextBox ID="txtSAPZMpwd" style="font-size:14px;" runat="server" placeholder="SAPZM password" class="form-control" /> </td>
        </tr>
        <tr>
        <td width="96%" align="left" colspan="6" >  <input type="button" value="Save" style="width:16%" onclick="return saveSAPCredentials();" class="btn btn-success"/>  </td>
        </tr>

        <tr>
            <td width="96%" colspan="6" > &nbsp; </td>
        </tr>

        <tr>
            <td width="96%" colspan="6"> Kindly enter your SAP login credentials above,You can not use this to change SAP credentials. Website will use these credentials to connect to SAP.  </td>
        </tr>

</table>

</asp:Panel>

</td>
</tr>

</table>

</asp:Content>

