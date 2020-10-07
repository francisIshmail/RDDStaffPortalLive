<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="NewFunnelDeal.aspx.cs" Inherits="IntranetNew_Funnel_NewFunnelDeal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.3.min.js"></script>

<script type="text/javascript">

    $(document).ready(function () {

//        $(function () {
//            $.ajax({
//                type: "POST", url: "NewFunnelDeal.aspx/GetCustomers", dataType: "json", contentType: "application/json", success: function (res) {
//                    alert(res);
//                    $.each(res.d, function (data, value) {
//                        alert(value.);
//                        $("#ddlNationality").append($("<option></option>").val(value.CardCode).html(value.CardName));
//                    })
//                }
//            });
//        });


        $(function () {
            $('[id*=ddlcustomer]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select Customer',
                maxHeight: 350,

                dropDown: true,
                enableFiltering: true,
                buttonWidth: '230px'
            });
        });

    });
</script>

 

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

    // Function to calculate Margin $ if Margin % is entered
    function calculateMargin() {
        var Landed = document.getElementById('<%= txtLanded.ClientID %>').value;
        var Marginpercent = document.getElementById('<%= txtMarginPercent.ClientID %>').value;

        if (Landed.trim() == "" || Landed.trim() == null) {
            Landed = 0;
            document.getElementById('<%= txtLanded.ClientID %>').value = 0;
        }
        if (Marginpercent.trim() == "" || Marginpercent.trim() == null) {
            Marginpercent = 0;
            document.getElementById('<%= txtMarginPercent.ClientID %>').value = 0;
        }

        if (Landed != null && Landed != NaN && Marginpercent != null && Marginpercent!=NaN) {
            document.getElementById('<%= txtMargin.ClientID %>').value = (Landed * (Marginpercent / 100)).toFixed(2)
            document.getElementById('<%= txtTotalSell.ClientID %>').value = (Number(Landed) + Number(Landed * (Marginpercent / 100))).toFixed(2)
        }
    }

    // Function to calculate Margin % if Margin $ is entered
    function calculateMarginPercent() {
        var Landed = document.getElementById('<%= txtLanded.ClientID %>').value;
        var Margindoller = document.getElementById('<%= txtMargin.ClientID %>').value;

        if (Landed.trim() == "" || Landed.trim() == null) {
            Landed = 0;
            document.getElementById('<%= txtLanded.ClientID %>').value = 0;
        }
        if (Margindoller.trim() == "" || Margindoller.trim() == null) {
            Margindoller = 0;
            document.getElementById('<%= txtMargin.ClientID %>').value = 0;
        }

        if (Landed != null && Landed != NaN && Margindoller != null && Margindoller != NaN) {
            document.getElementById('<%= txtMarginPercent.ClientID %>').value = (( Number(Margindoller)/Number(Landed)) * 100).toFixed(2)
            document.getElementById('<%= txtTotalSell.ClientID %>').value = (Number(Landed) + Number(Margindoller)).toFixed(2)
        }
    }

    // Function to calculate Margin % & Margin $ if Landed is changed after calculation
    function calculateMarginAndPercent() {

        var Landed = document.getElementById('<%= txtLanded.ClientID %>').value;
        var Margindoller = document.getElementById('<%= txtMargin.ClientID %>').value;
        var Marginpercent = document.getElementById('<%= txtMarginPercent.ClientID %>').value;

        if (Landed.trim() == "" || Landed.trim() == null) {
            Landed = 0;
        }

        if (Margindoller.trim() == "" || Margindoller.trim() == null) {
            Margindoller = 0;
        }
        if (Marginpercent.trim() == "" || Marginpercent.trim() == null) {
            Marginpercent = 0;
        }

        if (Landed != null && Landed != NaN && Marginpercent != null && Marginpercent != NaN) {
            calculateMargin();
        }
        else if (Landed != null && Landed != NaN && Margindoller != null && Margindoller != NaN) {
                calculateMarginPercent();
        }
    }

    // Function to calculate Margin % & Margin $ if Landed and Total Sell Amount is Entered
    function calcMarginAndPercentBasedOnTotalSell() {

        var Landed = document.getElementById('<%= txtLanded.ClientID %>').value;
        var TotalSell = document.getElementById('<%= txtTotalSell.ClientID %>').value;

        if (Landed != null && Landed != NaN && TotalSell != null && TotalSell != NaN) {
            document.getElementById('<%= txtMargin.ClientID %>').value = Number(TotalSell) - Number(Landed)
            var Margindoller = document.getElementById('<%= txtMargin.ClientID %>').value;
            if (Landed == 0) {
                document.getElementById('<%= txtMarginPercent.ClientID %>').value = 0;
            } else {
                document.getElementById('<%= txtMarginPercent.ClientID %>').value = ((Number(Margindoller) / Number(Landed)) * 100).toFixed(2)
            }
        }
    }

    function getConfirmation() {
        return confirm(' Are you sure, You want to Delete Funnel Deal ?');
    }

    // Tfunction is used to show and hide the reseller textbox and button
    function NewReseller() {
        var txtNewReseller = document.getElementById("<%= txtNewReseller.ClientID %>");
        var BtnSaveReseller = document.getElementById("<%= BtnSaveReseller.ClientID %>");
        if (document.getElementById('<%= chkNewReseller.ClientID %>').checked == true) {
            txtNewReseller.style.display = "";
            BtnSaveReseller.style.display = "";
        }
        else {
            txtNewReseller.style.display = "none";
            BtnSaveReseller.style.display = "none";
        }
    }

    function ValidateReseller() {

        var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
        if (ddlCountry == "--SELECT--") {
            alert('Please select country');
            return false;
        }

        var txtNewReseller = document.getElementById('<%= txtNewReseller.ClientID %>').value;
        if (txtNewReseller == "" || txtNewReseller==null) {
            alert('Please enter Reseller Name');
            return false;
        }
    }


    // funtion to perform validation before save/update
    function Validate() {
        
        var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
        if (ddlCountry == "--SELECT--") {
            alert('Please select country'); 
            return false;
        }

        var ddlBU = document.getElementById('<%= ddlBU.ClientID %>').value;
        if (ddlBU == "--SELECT--") {
            alert('Please select BU');
            return false;
        }

        var txtBDM = document.getElementById('<%= txtBDM.ClientID %>').value;
        if (txtBDM.trim() == "" || txtBDM.trim() == null) {
            alert('Please enter BDM');
            document.getElementById('<%= txtBDM.ClientID %>').focus();
            return false;
        }

        var txtEndUser = document.getElementById('<%= txtEndUser.ClientID %>').value;
        if (txtEndUser.trim() == "" || txtEndUser.trim() == null) {
            alert('Please enter End User');
            document.getElementById('<%= txtEndUser.ClientID %>').focus();
            return false;
        }

        var ddlcustomer = document.getElementById('<%= ddlcustomer.ClientID %>').value;
        if (ddlcustomer.trim() == "" || ddlcustomer.trim() == null) {
            alert('Please enter customer');
            return false;
        }

        var txtGoodsDesc = document.getElementById('<%= txtGoodsDesc.ClientID %>').value;
        if (txtGoodsDesc.trim() == "" || txtGoodsDesc.trim() == null) {
            alert('Please enter Goods Description');
            document.getElementById('<%= txtGoodsDesc.ClientID %>').focus();
            return false;
        }

        var txtRemark1 = document.getElementById('<%= txtRemark1.ClientID %>').value;
        if (txtRemark1.trim() == "" || txtRemark1.trim() == null) {
            alert('Please enter Remark 1');
            document.getElementById('<%= txtRemark1.ClientID %>').focus();
            return false;
        }

        var txtQuoteDate = document.getElementById('<%= txtQuoteDate.ClientID %>').value;
        if (txtQuoteDate.trim() == "" || txtQuoteDate.trim() == null) {
            alert('Please enter Quote Date');
            document.getElementById('<%= txtQuoteDate.ClientID %>').focus();
            return false;
        }

        var txtCost = document.getElementById('<%= txtCost.ClientID %>').value;
        if (txtCost.trim() == "" || txtCost.trim() == null) {
            document.getElementById('<%= txtCost.ClientID %>').value = "0";
            //alert('Please enter Cost');
            //document.getElementById('<%= txtCost.ClientID %>').focus();
            //return false;
        }

        var txtLanded = document.getElementById('<%= txtLanded.ClientID %>').value;
        if (txtLanded.trim() == "" || txtLanded.trim() == null) {
            document.getElementById('<%= txtLanded.ClientID %>').value = "0";
            txtLanded = 0;
           // alert('Please enter Landed');
           // document.getElementById('<%= txtLanded.ClientID %>').focus();
           // return false;
        }

//        if (Number(txtLanded) < Number(txtCost)) {
//            alert('Landed must be greater than or equal to Cost');
//            return false;
//        }

        var txtTotalSell = document.getElementById('<%= txtTotalSell.ClientID %>').value;
        if (txtTotalSell.trim() == "" || txtTotalSell.trim() == null) {
            alert('Please enter Total Sell Amount');
            document.getElementById('<%= txtTotalSell.ClientID %>').focus();
            return false;
        }

        if (Number(txtTotalSell) < Number(txtLanded)) {
            alert('Total Sell Amount must be greater than or equal to Landed');
            return false;
        }

        var txtMarginPercent = document.getElementById('<%= txtMarginPercent.ClientID %>').value;
        var txtMargin = document.getElementById('<%= txtMargin.ClientID %>').value;
        if (txtMarginPercent.trim() == "" || txtMarginPercent.trim() == null || txtMargin.trim() == "" || txtMargin.trim() == null) {
            document.getElementById('<%= txtMarginPercent.ClientID %>').value = 0;
            //alert('Please enter either margin $ or margin %');
            //return false;
        }

        var txtClodingDate = document.getElementById('<%= txtClodingDate.ClientID %>').value;
        if (txtClodingDate.trim() == "" || txtClodingDate.trim() == null) {
            alert('Please enter expected Closing Date');
            document.getElementById('<%= txtClodingDate.ClientID %>').focus();
            return false;
        }

        var qtdate = new Date(txtQuoteDate);
        var closingDt = new Date(txtClodingDate);

        if (qtdate > closingDt) {
            alert('Expected Closing Date must be greater than quote date');
            document.getElementById('<%= txtClodingDate.ClientID %>').focus();
            return false;
        }

        var ddlStatus = document.getElementById('<%= ddlStatus.ClientID %>').value;
        if (ddlStatus.trim() == "WON-R OPG" || ddlStatus.trim() == "WON OPG") {

            var txtOrderDate = document.getElementById('<%= txtOrderDate.ClientID %>').value;
            var OrderDt = new Date(txtOrderDate);

            if (txtOrderDate.trim() == "" || txtOrderDate.trim() == null) {
                alert('Please enter Order Date');
                document.getElementById('<%= txtOrderDate.ClientID %>').focus();
                return false;
            }

            if (closingDt > OrderDt) {
                alert('Order date must be greater than Expected Closing Date');
                document.getElementById('<%= txtClodingDate.ClientID %>').focus();
                return false;
            }
        }
        else if (ddlStatus.trim() == "LOST-CLOSED") {

            var txtOrderDate = document.getElementById('<%= txtOrderDate.ClientID %>').value;
            if (txtOrderDate.trim() != "" && txtOrderDate.trim() != null) {
                alert('You can not enter the Order Date for Lost deal.');
                document.getElementById('<%= txtOrderDate.ClientID %>').focus();
                return false;
            }

            var txtInvoiceDate = document.getElementById('<%= txtInvoiceDate.ClientID %>').value;
            if (txtInvoiceDate.trim() != "" && txtInvoiceDate.trim() != null) {
                alert('You can not enter the Invoice Date for Lost deal.');
                document.getElementById('<%= txtInvoiceDate.ClientID %>').focus();
                return false;
            }
        }

               var txtReminderOn = document.getElementById('<%= txtReminderOn.ClientID %>').value;
               var ddlStatus = document.getElementById('<%= ddlStatus.ClientID %>').value;
               if ((txtReminderOn.trim() == "" || txtReminderOn.trim() == null) && ddlStatus=="OPEN") {
                    return confirm('You have not set the next reminder date. Are you sure, You want to Save Funnel Deal ?');
               }

    }

    </script>

<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable id="lblformName" runat="server" style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Funnel New Deal  </Lable>
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
    
<asp:Panel ID="pnlnewDeal" runat="server" Width="95%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="95%" align="center" cellpadding="3px" cellspacing="3px"  >
    
    <tr>    
        <td width="10%">
           &nbsp;
        </td>
        <td width="23%">
        &nbsp;
        </td>
         <td width="10%">
           &nbsp;
        </td>
        <td width="23%">
        &nbsp;
        </td>
         <td width="12%">
           &nbsp;
        </td>
        <td width="22%">
        &nbsp;
        </td>
    </tr>
    
     <tr>
        <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Country  &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:DropDownList ID="ddlCountry" runat="server"  class="form-control"  AutoPostBack="true"
                Width="220px" style="font-size:medium;" TabIndex="0" 
                onselectedindexchanged="ddlCountry_SelectedIndexChanged" > </asp:DropDownList>
        </td>
         <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Customer  &nbsp; </label>  
        </td>
        <td width="23%">
             <asp:ListBox ID="ddlcustomer" runat="server" SelectionMode="Multiple" Width="150px" class="form-control" TabIndex="1" >
            </asp:ListBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Quote ID  &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtQuoteID" runat="server" MaxLength="250" Width="165px" class="form-control" style="font-size:medium;" TabIndex="99" Enabled="false" >
              </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> BU  &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:DropDownList ID="ddlBU" runat="server"  Width="220px"   class="form-control" style="font-size:medium;" TabIndex="2" >
            </asp:DropDownList>
        </td>
         <td width="10%" >
            <asp:Label ID="lblFid" runat="server" Visible="false" Text=""></asp:Label>
        </td>
        <td width="23%" >
            <asp:Label ID="lblChangeCount" runat="server" Visible="false" Text=""></asp:Label>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Quote Date  &nbsp; </label>  
        </td>
        <td width="22%">
                <asp:TextBox ID="txtQuoteDate" runat="server"  Width="165px" class="form-control"  style="font-size:medium;" TabIndex="8" placeholder="mm/dd/yyyy" >
                </asp:TextBox>
                   <cc1:CalendarExtender ID="_txtQuoteDateCalendarExtender" runat="server"  Enabled="True" TargetControlID="txtQuoteDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>

     <tr>
        <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> BDM  &nbsp; </label>  
        </td>
        <td width="23%">
           <asp:TextBox ID="txtBDM" runat="server"  Width="220px" MaxLength="250" class="form-control" style="font-size:medium;" placeholder="Enter BDM" TabIndex="3" ></asp:TextBox>
        </td>
         <td width="10%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> End User  &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:TextBox ID="txtEndUser" runat="server" Width="220px"  MaxLength="250" class="form-control" style="font-size:medium;" placeholder="Enter EndUser" TabIndex="4"></asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Cost &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtCost" runat="server" Width="165px" MaxLength="9"  class="form-control" TabIndex="9"  style="font-size:medium;" placeholder="Enter Cost" onkeypress="javascript:return isNumberKey(event);" > </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="10%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Goods Descr &nbsp; </label>  
        </td>
        <td width="56%" colspan="3">
           <asp:TextBox ID="txtGoodsDesc" runat="server" Width="665px" MaxLength="1020" class="form-control" TabIndex="5" style="font-size:medium;" placeholder="Enter Goods Description" > </asp:TextBox>
        </td>
        
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Landed &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtLanded" runat="server" Width="165px" MaxLength="9"  class="form-control" TabIndex="10"  style="font-size:medium;" placeholder="Enter Landed" onkeypress="javascript:return isNumberKey(event);" onkeyup="javascript:calculateMarginAndPercent()"  > </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="10%">
                <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Remark 1 &nbsp; </label>  
        </td>
        <td width="56%" colspan="3">
            <asp:TextBox ID="txtRemark1" runat="server" Width="665px" MaxLength="250" class="form-control" TabIndex="6" style="font-size:medium;" placeholder="Enter Remark" > </asp:TextBox>
        </td>
         
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Margin % &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtMarginPercent" runat="server" Width="165px"  MaxLength="10"  class="form-control" TabIndex="11"  style="font-size:medium;" placeholder="Enter Margin %" onkeypress="javascript:return isNumberKey(event);" onkeyup="javascript:calculateMargin()" > </asp:TextBox>
        </td>
    </tr>

      <tr>
        <td width="10%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Remark 2 &nbsp; </label>  
        </td>
        <td width="56%" colspan="3">
              <asp:TextBox ID="txtRemark2" runat="server" Width="665px" Enabled="false" class="form-control" TabIndex="6" style="font-size:medium;" placeholder="Enter Remark" > </asp:TextBox>
        </td>
         
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Margin $ &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtMargin" runat="server" Width="165px" MaxLength="10" class="form-control" TabIndex="12"  style="font-size:medium;" placeholder="Enter Margin $" onkeypress="javascript:return isNumberKey(event);" onkeyup="javascript:calculateMarginPercent()" > </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="10%">
             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Remark 3 &nbsp; </label>  
        </td>
        <td width="56%" colspan="3">
              <asp:TextBox ID="txtRemark3" runat="server" Enabled="false" MaxLength="250" Width="665px" class="form-control" TabIndex="6" style="font-size:medium;" placeholder="Enter Remark" > </asp:TextBox>
        </td>
         
         <td width="12%">
          <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Sell Amount &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:TextBox ID="txtTotalSell" runat="server" Width="165px" MaxLength="19" class="form-control" TabIndex="13"  style="font-size:medium;" placeholder="Enter Sell Amount" onkeypress="javascript:return isNumberKey(event);" onkeyup="javascript:calcMarginAndPercentBasedOnTotalSell();" > </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Created By  &nbsp; </label>  
        </td>
        <td width="23%">
           <asp:TextBox ID="txtCreatedBy" runat="server" Width="220px"  class="form-control" style="font-size:medium;" Enabled="false" ></asp:TextBox>
        </td>
         <td width="10%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Updated By &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:TextBox ID="txtUpdatedBy" runat="server" Width="220px"  class="form-control" style="font-size:medium;" Enabled="false" ></asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Status &nbsp; </label>  
        </td>
        <td width="22%">
              <asp:DropDownList ID="ddlStatus" Width="165px" runat="server" TabIndex="14"  class="form-control" style="font-size:medium;" ></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td width="10%">
            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Created Date  &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:TextBox ID="txtCreatedDate" runat="server" Width="220px"  class="form-control" style="font-size:medium;" Enabled="false"></asp:TextBox>
        </td>
         <td width="10%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Updated Date &nbsp; </label>  
        </td>
        <td width="23%">
            <asp:TextBox ID="txtUpdatedDt" runat="server" Width="220px"  class="form-control" style="font-size:medium;" Enabled="false" ></asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Closing Date &nbsp; </label>  
        </td>
        <td width="22%">
               <asp:TextBox ID="txtClodingDate" runat="server"  Width="165px" class="form-control"  style="font-size:medium;" TabIndex="15" placeholder="mm/dd/yyyy" >
                </asp:TextBox>
                   <cc1:CalendarExtender ID="txtClodingDate_CalendarExtender1" runat="server"  Enabled="True" TargetControlID="txtClodingDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>

     <tr>
        <td width="10%">
           &nbsp;
        </td>
        <td width="33%" colspan="2" align="right">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;color:Blue   "> <b> Next Reminder On </b> </label>  
        </td>
        <td width="23%">
             <asp:TextBox ID="txtReminderOn" runat="server"  Width="220px" class="form-control"  style="font-size:medium;" TabIndex="18" placeholder="mm/dd/yyyy" >
                </asp:TextBox>
                   <cc1:CalendarExtender ID="txtReminderOn_CalendarExtender1" runat="server"  Enabled="True" TargetControlID="txtReminderOn" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Order Date &nbsp; </label>  
        </td>
        <td width="22%">
               <asp:TextBox ID="txtOrderDate" runat="server"  Width="165px" class="form-control"  style="font-size:medium;" TabIndex="16" placeholder="mm/dd/yyyy" >
                </asp:TextBox>
                   <cc1:CalendarExtender ID="txtOrderDate_CalendarExtender1" runat="server"  Enabled="True" TargetControlID="txtOrderDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>

     <tr>
        <td width="10%">
          &nbsp;&nbsp;  <asp:CheckBox ID="chkNewReseller" runat="server" Visible="false" Text="New Reseller" TabIndex="100" onClick="javascript:NewReseller(event);"   />
        </td>
        <td width="23%">
                <asp:TextBox ID="txtNewReseller" placeholder="Enter Reseller Name" TabIndex="101"  runat="server"  Width="220px" class="form-control" style="font-size:medium;display:none;" > </asp:TextBox>
        </td>
         <td width="33%" colspan="2">
                <asp:Button ID="BtnSaveReseller" Text="Save Reseller" runat="server" OnClientClick="return ValidateReseller();"
                    class="btn btn-warning"  Font-Bold="true" Font-Size="Small" 
                style="font-family: Raleway;height:38px;width:120px;display:none;" 
                    TabIndex="102" onclick="BtnSaveReseller_Click"  />
        </td>
        
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Invoice Date &nbsp; </label>  
        </td>
        <td width="22%">
            <asp:TextBox ID="txtInvoiceDate" runat="server"  Width="165px" class="form-control"  style="font-size:medium;" TabIndex="17" placeholder="mm/dd/yyyy" >
                </asp:TextBox>
                   <cc1:CalendarExtender ID="txtInvoiceDate_CalendarExtender1" runat="server"  Enabled="True" TargetControlID="txtInvoiceDate" DaysModeTitleFormat="dd/MM/yyyy" 
                            TodaysDateFormat="dd/MM/yyyy">
                        </cc1:CalendarExtender>
        </td>
    </tr>

    <tr>
    <td width="100%" colspan="6"> &nbsp;</td>
    </tr>
     <tr>
        <td width="100%" colspan="6" >

         &nbsp;&nbsp;  &nbsp; 
        
        <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-primary" 
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="19" 
                OnClientClick="return Validate();" onclick="btnSave_Click" />

          &nbsp;&nbsp;  &nbsp;&nbsp;   
        
        <asp:Button ID="BtnDelete" Text="Delete" runat="server" class="btn btn-danger" 
                Enabled="false"   OnClientClick="return getConfirmation();"
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="20" 
                onclick="BtnDelete_Click"   />

         &nbsp;&nbsp;  &nbsp;&nbsp;
        
        <asp:Button ID="BtnGoBack" Text="Go To List" runat="server" 
                class="btn btn-info" Font-Bold="true" Font-Size="Medium"  
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="21" 
                onclick="BtnGoBack_Click" />
      
        </td>
    
    </tr>
    <tr>
    <td width="100%" colspan="6" > &nbsp;</td>
    </tr>
  </table>

</asp:Panel>

  </td>
</tr>
   
</table>
</asp:Content>

