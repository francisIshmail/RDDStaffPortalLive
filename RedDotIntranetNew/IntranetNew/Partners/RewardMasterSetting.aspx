<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="RewardMasterSetting.aspx.cs" Inherits="IntranetNew_Partners_RewardMasterSetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Reward Master Setting </Lable></td>
</tr>

<tr>
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
    
<asp:Panel ID="pnlRewardMastersetting" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="100%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="40%">
           &nbsp;
        </td>
        <td width="30%">
        &nbsp;
        </td>
        <td width="30%">
       <asp:Label ID="lblMasterSettingId" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
        </td>
    </tr>
    
    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward setup auto renewal &nbsp;  </label>
        </td>
        <td width="30%" align="left" >
          <asp:CheckBox ID="chkAutoRenewal" runat="server"  style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:CheckBox> 
        </td>
         <td width="30%" >
        </td>
    </tr>
 
      <tr>
        
        <td width="40%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">
            Reminder day of quarter for Reward setup &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:TextBox ID="txtRewardReminderdt" runat="server" 
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="2" onkeypress="javascript:return isNumberKey(event);"  ></asp:TextBox> 
        </td>
        <td width="30%">
            &nbsp;
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reminder email Id for Reward setup   &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" >
             <asp:TextBox ID="txtReminderemialId" runat="server" TextMode="MultiLine" style="width:80%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
         
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward eligibility minimun Invoice value &nbsp;  </label>
        </td>
        <td width="30%" >
           <asp:TextBox ID="txtMinInvoiceRewardValue" runat="server"  placeholder="0.00" 
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="10" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox> 
          
        </td>
         <td width="30%" >
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points multiplication factor &nbsp;  </label>
        </td>
        <td width="30%" >
           <asp:TextBox ID="txtMultiplicationFactor" runat="server"  placeholder="0" 
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="3" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
          
        </td>
         <td width="30%" >
        </td>
    </tr>
     <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points rounding to &nbsp;  </label>
        </td>
        <td width="30%" colspan="2" align="left">
           <asp:RadioButton ID="rdceiling" runat="server"  
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                GroupName="rdRounding" Text="Ceiling"></asp:RadioButton> 
        &nbsp;
            <asp:RadioButton ID="rdFloor" runat="server"  
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                 GroupName="rdRounding" Text="Floor"></asp:RadioButton> 
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points calculation based on&nbsp;  </label>
        </td>
        <td width="30%" colspan="2" align="left">
           <asp:RadioButton ID="rdcost" runat="server"  
                style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                GroupName="rdRewardCalcBasedOn" Text="Cost"></asp:RadioButton> 
        &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rdsales" runat="server"  
                 style="width:65%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                 GroupName="rdRewardCalcBasedOn" Text="Sales"></asp:RadioButton> 
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward confirm based on Payment Term &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
          <asp:CheckBox ID="chkPointConfirmOnPayTerm" runat="server"  style="width:5%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:CheckBox> 
      
           
        </td>
    </tr>
    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points confirm after days &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
           <asp:TextBox ID="txtPointConfirmDays" runat="server"  placeholder="days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="3" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
           
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Use INVOICE OR SEUTP days to confirm points &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
            
                <asp:DropDownList ID="ddlUseINVORSETUP" runat="server" 
                        style="width:30%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  >  
                        <asp:ListItem>--Select--</asp:ListItem>
                        <asp:ListItem>INVOICE</asp:ListItem>
                        <asp:ListItem>SETUP</asp:ListItem>
                </asp:DropDownList>
           
        </td>
    </tr>


    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Finance grace period to enter payment &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
           <asp:TextBox ID="txtFinanceGraceDays" runat="server"  placeholder="days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="2" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
           
        </td>
    </tr>

     <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Customer grace period to make payment &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
           <asp:TextBox ID="txtcustGracePeriod" runat="server"  placeholder="days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="2" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
           
        </td>
    </tr>

     <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward subscription bonus points &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
           <asp:TextBox ID="txtRewardSubscriptionBonus" runat="server"  placeholder="days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="4" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
           
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points expires after Months &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
           <asp:TextBox ID="txtRewardPointsExpiresAfterMonth" runat="server"  placeholder="days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="4" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> 
           
        </td>
    </tr>

    <tr >
        <td width="40%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points expires after every  &nbsp;  </label>
        </td>
        <td width="60%" colspan="2" align="left">
          <%-- <asp:TextBox ID="txtPointsExpiresAftrMonth" runat="server"  placeholder="Month" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="2" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> --%>

                <asp:DropDownList ID="ddlRewardExpiry" runat="server" 
                    style="width:30%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  >  
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>QUARTERLY</asp:ListItem>
                    <asp:ListItem>HALF YEARLY</asp:ListItem>
                    <asp:ListItem>YEARLY</asp:ListItem>
            </asp:DropDownList>
           
        </td>
    </tr>

    <tr >
        <td width="50%" align="left">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; "> Reward points expiry reminder frequency &nbsp;  </label>
        </td>
        <td width="50%" colspan="2" align="left">
           <%--<asp:TextBox ID="txtsendRewardPtsexpiryday" runat="server"  placeholder="Days" 
                style="width:15%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                MaxLength="2" onkeypress="javascript:return isNumberKey(event);"></asp:TextBox> --%>
            <asp:DropDownList ID="ddlRewardPointExpiryFrequency" runat="server" 
                    style="width:30%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "  >  
                    <asp:ListItem>--Select--</asp:ListItem>
                    <asp:ListItem>Daily</asp:ListItem>
                    <asp:ListItem>Weekly</asp:ListItem>
                    <asp:ListItem> Fortnight </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
    
        <td align="left" colspan="3" width="80%">
            &nbsp;
        </td>
        <tr>
            <td align="left" colspan="2" width="80%">
                &nbsp;&nbsp;
                <asp:Button ID="BtnSave" runat="server" 
                    style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;" 
                    Text="Save" onclick="BtnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" 
                    style="width:20%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " 
                    Text="Go Back" onclick="BtnCancel_Click" />
            </td>
            <td width="20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3">
              
                  &nbsp;

            </td>
        </tr>
    
    </tr>

</table>

</asp:Panel>

  </td>
</tr>

<tr>
    <td> &nbsp;</td>
</tr>

</table>

</asp:Content>

