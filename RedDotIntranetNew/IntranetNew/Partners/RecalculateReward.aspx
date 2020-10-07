<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="RecalculateReward.aspx.cs" Inherits="IntranetNew_Partners_RecalculateReward" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">

    function confirmIsEligible() {
        var ddlIsEligible = document.getElementById("<%= ddlIsEligible.ClientID %>");
        var OldVal = document.getElementById("<%= LblIsEligibleStatus.ClientID %>").innerHTML;
        var NewVal = ddlIsEligible.options[ddlIsEligible.selectedIndex].text;
        
        if (NewVal != "--Select--")
        {
            if(NewVal != OldVal)
            {
                alert("Confirmation ! You have changed the Reward Eligibilty for document from " + OldVal +" to " + NewVal + ". If you want to continue click on Recalculate & Post Button."); 
            }
        }
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

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" colspan="2">
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Recalculate Reward Points </Lable>
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
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:#be19c1; font-weight:bold; " />  &nbsp;&nbsp; 
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlRecalculateReward" runat="server" Width="50%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
     
    <tr>
        <td width="30%">
           &nbsp;
        </td>
        <td width="35%">
        &nbsp;
        </td>
        <td width="35%">
       <asp:Label ID="LblIsEligibleStatus" runat="server"  Visible="true" Width="0px"
                
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:White; font-weight:bold; " />
        </td>
    </tr>

    <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> SAP Database   &nbsp;  </label>
        </td>
        <td width="35%" >
           
                <asp:DropDownList ID="ddlDatabase" runat="server" AutoPostBack="true"
                    
                    style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  >  </asp:DropDownList>
           
        </td>
         <td width="35%" >
        </td>
    </tr>

      <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Document Type   &nbsp;  </label>
        </td>
        <td width="70%" colspan="2" align="left" >
                 <asp:RadioButton ID="rdARInvoice" runat="server"  
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                GroupName="rdDocType" Text="AR Invoice"></asp:RadioButton> 
                &nbsp;
            <asp:RadioButton ID="rdARCreditNote" runat="server"  
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                 GroupName="rdDocType" Text="AR Credit Note"></asp:RadioButton> 
        </td>
         <td width="35%" >
        </td>
    </tr>

      <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Document No.   &nbsp;  </label>
        </td>
        <td width="35%" >
               <asp:TextBox ID="txtDocumentNo" runat="server" onkeypress="javascript:return isNumberKey(event);"
                   style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
         <td width="35%" >
             <asp:Button ID="BtnGetInvForRecalculate0" runat="server" 
                 onclick="BtnGetInvForRecalculate_Click" 
                 style="width:60%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                 Text="Get Data" />
        </td>
    </tr>


    <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> SAP Customer   &nbsp;  </label>
        </td>
        <td width="35%" >

         <asp:TextBox ID="txtCardName" runat="server" Enabled="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
               
        </td>
         <td width="35%" >
                <asp:TextBox ID="txtCardCode" runat="server" Visible="false" Enabled="false"
                   style="width:50%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
    </tr>

      <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Document Date   &nbsp;  </label>
        </td>
        <td width="35%" >
         <asp:TextBox ID="txtDocDate" runat="server" Enabled="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
               
        </td>
         <td width="35%" >
              <asp:TextBox ID="txtDocEntry" runat="server" Enabled="false" Visible="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
    </tr>
     
      <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Document Status   &nbsp;  </label>
        </td>
        <td width="35%" >
         <asp:TextBox ID="txtDocStatus" runat="server" Enabled="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
               
        </td>
         <td width="35%" >
              &nbsp;
        </td>
    </tr>

       <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Document Total   &nbsp;  </label>
        </td>
        <td width="35%" >
         <asp:TextBox ID="txtDocTotal" runat="server" Enabled="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
               
        </td>
         <td width="35%" >
              &nbsp;
        </td>
    </tr>

      <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward Points   &nbsp;  </label>
        </td>
        <td width="35%" >
         <asp:TextBox ID="txtRewardPoints" runat="server" Enabled="false"
                   style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
               
        </td>
         <td width="35%" >
              &nbsp;
        </td>
    </tr>

     <tr >
        <td width="30%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Eligible for Reward   &nbsp;  </label>
        </td>
        <td width="35%" >
            <asp:DropDownList ID="ddlIsEligible" runat="server"  
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
                <asp:ListItem>YES</asp:ListItem>
                <asp:ListItem>NO</asp:ListItem>
            </asp:DropDownList>
               
        </td>
         <td width="35%" >
              &nbsp;
        </td>
    </tr>

    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>


     <tr>
        <td width="80%" align="left" colspan="2">
      
            &nbsp;&nbsp; &nbsp;&nbsp; 
             <asp:Button ID="BtnRecalculate" runat="server" Text="Recalculate & Post"  Enabled="false"
                    
                style="width:40%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnRecalculate_Click" 
                     />  &nbsp;&nbsp; 
           <asp:Button ID="BtnCancel" runat="server" Text="Cancel" 
                
                style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                onclick="BtnCancel_Click" />
       
        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

     <tr>
        <td colspan="3">
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
        &nbsp;
    </td>
</tr>


</table>



</asp:Content>

