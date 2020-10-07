<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="PV.aspx.cs" Inherits="IntranetNew_BPStatus_PV" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>

<script type="text/javascript">

    $(function () {
        $("#btnShowAgeing").click(function () {

            if ($("[id$=ddlVoucherType]").val() == 'Vendor') {
                if ($("[id$=ddlDatabase]").val() != '--Select--' && $("[id$=lblVendEmployeeCode]").val() != "") {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "PV.aspx/GetAgeing",
                        data: "{'dbName':'" + $("[id$=ddlDatabase]").val() + "','cardcode':'" + $("[id$=lblVendEmployeeCode]").val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            $("[id$=lblBalance]").html(data.d.Balance);
                            $("[id$=lbl0_30]").html(data.d.zeroTothirty);
                            $("[id$=lbl31_45]").html(data.d.thirtyoneTofourtyfive);
                            $("[id$=lbl46_60]").html(data.d.fourtyfiveTosixty);
                            $("[id$=lbl61_90]").html(data.d.sixtyoneToninty);
                            $("[id$=lbl91_120]").html(data.d.nintyoneToonetwenty);
                            $("[id$=lbl120Plus]").html(data.d.onetwentyplus);
                        },
                        error: function (result) {
                            $("[id$=lblBalance]").html('');
                            $("[id$=lbl0_30]").html('');
                            $("[id$=lbl31_45]").html('');
                            $("[id$=lbl46_60]").html('');
                            $("[id$=lbl61_90]").html('');
                            $("[id$=lbl91_120]").html('');
                            $("[id$=lbl120Plus]").html('');
                            alert("Error to get Vendor ageing report");
                        }
                    });
                    $('#VendorAgeing').modal('show');

                }
                else {
                    alert(' Please select database and vendor ');
                }
            }
            else {
                alert(' You can view ageing for vendors only ');
            }

        });
    });

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


    $(function () {

        $("[id$=txtVendorEmployee]").autocomplete({

            source: function (request, response) {

                if (($("[id$=ddlVoucherType]").val() == "--Select--") || ($("[id$=ddlCountry]").val() == "--Select--") || ($("[id$=ddlDatabase]").val() == "--Select--")) {
                    alert('please select country, database & Voucher Type');
                    $("[id$=txtVendorEmployee]").val('');
                    return;
                }
                $("[id$=lblVendEmployeeCode]").val('');
                $.ajax({
                    url: "PV.aspx/GetVendors",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=ddlDatabase]").val() + "','emporvend':'" + $("[id$=ddlVoucherType]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('#')[0],
                                val: item.split('#')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=txtVendorEmployee]").val(i.item.label);
                $("[id$=lblVendEmployeeCode]").val(i.item.val);
                $("[id$=txtBenificiary]").val(i.item.label);
            },
            minLength: 1
        });
    });

    $(function () {

        $("[id$=txtBankName]").autocomplete({

            source: function (request, response) {

                if (($("[id$=ddlVoucherType]").val() == "--Select--") || ($("[id$=ddlCountry]").val() == "--Select--") || ($("[id$=ddlDatabase]").val() == "--Select--")) {
                    alert('please select country, database & Voucher Type');
                    $("[id$=txtBankName]").val('');
                    return;
                }

                $.ajax({
                    url: "PV.aspx/GetBanks",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=ddlDatabase]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('#')[0],
                                val: item.split('#')[1]
                            }
                        }))
                    },
                    error: function (response) {
                        alert(response.responseText);

                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                $("[id$=txtBankName]").val(i.item.label);
                $("[id$=lblBankCode]").val(i.item.val);
            },
            minLength: 1
        });
    });


    function VoucherType_changed() {

        var ddlVoucherType = document.getElementById('<%= ddlVoucherType.ClientID %>').value
        

        if (ddlVoucherType === "Internal") {
            $("[id$=lblVendorEmploye]").html("Employee");
            var EmpName = document.getElementById('<%= lblUserDisplayName.ClientID %>').innerText
            $("[id$=txtVendorEmployee]").val(EmpName);
            $("[id$=txtBenificiary]").val(EmpName);
            $('#btnShowAgeing').attr("disabled", true);
        }
        else if (ddlVoucherType === "Vendor") {
            $("[id$=lblVendorEmploye]").html("Vendor");
            $("[id$=txtVendorEmployee]").val("");
            $("[id$=txtBenificiary]").val("");
            $('#btnShowAgeing').attr("disabled", false);
           // $('#btnShowAgeing').removeAttr("disabled");
        }
        else {
            $("[id$=lblVendorEmploye]").html("Vendor/Employee");
        }

    }

    function PaymentMethod_changed() {
        var ddlPaymentMethod = document.getElementById('<%= ddlPaymentMethod.ClientID %>').value
        if (ddlPaymentMethod == "Cheque") {
            $("[id$=lblChequeNo]").html("Cheque No");
            $("[id$=lblChequeDate]").html("Cheque Date");
            $("[id$=lblChequeImage]").html("Cheque Image");
            document.getElementById('<%= txtBankName.ClientID %>').disabled = false;
        }
        else if (ddlPaymentMethod == "TT") {
            $("[id$=lblChequeNo]").html("TT No");
            $("[id$=lblChequeDate]").html("TT Date");
            $("[id$=lblChequeImage]").html("TT Image");
            document.getElementById('<%= txtBankName.ClientID %>').disabled = false;
        }
        else {
            $("[id$=lblChequeNo]").html("Pay Ref No");
            $("[id$=lblChequeDate]").html("Pay Ref Date");
            $("[id$=lblChequeImage]").html("Pay Ref Image");
            document.getElementById('<%= txtBankName.ClientID %>').disabled = true;
        }
    }

    function getConfirmation() {
        return confirm(' Are you sure, You want to Delete this PV ?');
    }

    function getConfirmationRowDelete() {

        var gridViewRowCount = document.getElementById("<%= gvPVLines.ClientID %>").rows.length;
        if (gridViewRowCount == 2) {
            alert('One row is mandatory, you can not delete this');
            return false;
        }
        else {
            return confirm(' Are you sure, You want to Delete this row ?');
        }
    }

    function Validate() {

        var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value
        var ddlDatabase = document.getElementById('<%= ddlDatabase.ClientID %>').value
        var ddlVoucherType = document.getElementById('<%= ddlVoucherType.ClientID %>').value
        var ddlStatus = document.getElementById('<%= ddlStatus.ClientID %>').value
        var txtVendorEmployee = document.getElementById('<%= txtVendorEmployee.ClientID %>').value
        var ddlCurrency = document.getElementById('<%= ddlCurrency.ClientID %>').value
        var txtBenificiary = document.getElementById('<%= txtBenificiary.ClientID %>').value
        var txtPaymentReqDate = document.getElementById('<%= txtPaymentReqDate.ClientID %>').value
        var txtRequestedAmount = document.getElementById('<%= txtRequestedAmount.ClientID %>').value
        var txtBeingPayOf = document.getElementById('<%= txtBeingPayOf.ClientID %>').value
        var txtBankName = document.getElementById('<%= txtBankName.ClientID %>').value

        var lblVendEmployeeCode = document.getElementById('<%= lblVendEmployeeCode.ClientID %>').value
        var lblBankCode = document.getElementById('<%= lblBankCode.ClientID %>').value
        //var fimage = document.getElementById('<%= fuploadCheckImage.ClientID %>').value
        //alert(fimage);

        var ddlPaymentMethod = document.getElementById('<%= ddlPaymentMethod.ClientID %>').value
        var txtApprovedAmt = document.getElementById('<%= txtApprovedAmt.ClientID %>').value
        var txtChequeNo = document.getElementById('<%= txtChequeNo.ClientID %>').value
        var txtChequeDate = document.getElementById('<%= txtChequeDate.ClientID %>').value

        if (ddlCountry == "--Select--") {
            alert('Please select country');
            document.getElementById('<%= ddlCountry.ClientID %>').focus();
            return false;
        }
       
        if (ddlVoucherType == "--Select--") {
            alert('Please select Voucher Type');
            document.getElementById('<%= ddlVoucherType.ClientID %>').focus();
            return false;
        }

        if (ddlVoucherType == "Vendor") {
            if (ddlDatabase == "--Select--") {
                alert('Please select database');
                document.getElementById('<%= ddlDatabase.ClientID %>').focus();
                return false;
            }
        }

        if (ddlStatus == "--Select--") {
            alert('Please select status');
            return false;
        }
        if (txtVendorEmployee === "") {
            if (ddlVoucherType == "Vendor") {
                alert('Please enter vendor name');
                document.getElementById('<%= txtVendorEmployee.ClientID %>').focus();
                return false;
            }
            else {
                alert('Please enter employee name');
                document.getElementById('<%= txtVendorEmployee.ClientID %>').focus();
                return false;
            }
        }

        if (ddlVoucherType == "Vendor") {
            if (lblVendEmployeeCode === "") {
                alert('Invalid vendor, Please select vendor name from auto search list');
                document.getElementById('<%= txtVendorEmployee.ClientID %>').focus();
                return false;
            }
        }

        if (ddlCurrency == "--Select--") {
            alert('Please select currency');
            document.getElementById('<%= ddlCurrency.ClientID %>').focus();
            return false;
        }

        if (txtBenificiary === "") {
            alert('Please enter Benificiary detail');
            document.getElementById('<%= txtBenificiary.ClientID %>').focus();
            return false;
        }
        if (txtPaymentReqDate === "") {
            alert('Please enter payment request date');
            document.getElementById('<%= txtPaymentReqDate.ClientID %>').focus();
            return false;
        }
        if (txtRequestedAmount === "") {
            alert('Please enter requested amount');
            document.getElementById('<%= txtRequestedAmount.ClientID %>').focus();
            return false;
        }
        if (txtBeingPayOf === "") {
            alert('Please enter Being Payment Of');
            document.getElementById('<%= txtBeingPayOf.ClientID %>').focus();
            return false;
        }

        if (ddlPaymentMethod == "Cheque" || ddlPaymentMethod == "TT") {
            if (txtBankName === "") {
                alert('Please enter Bank Name');
                document.getElementById('<%= txtBankName.ClientID %>').focus();
                return false;
            }

            if (lblBankCode === "") {
                alert('Invalid Bank, Please select correct BankName From auto search List');
                document.getElementById('<%= txtBankName.ClientID %>').focus();
                return false;
            }
        }

    }

</script>

 <%--<asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">--%>

<%--<ContentTemplate>--%>
<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable id="lblformName" runat="server" style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Payment Voucher  </Lable>
    </td>
</tr>

<tr>
        <td width="50%">
           &nbsp;
           <asp:Label ID="lblUserPVRole" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblUserPVCountry" runat="server" Visible="false"></asp:Label>
           <asp:Label ID="lblUserDisplayName" runat="server" Visible="true" ForeColor="White" ></asp:Label>

        </td>
        <td width="50%">
        &nbsp;
        </td>
</tr>

<tr>
        <td width="100%" align="center">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                 <ContentTemplate>
                        <asp:Label ID="lblMsg" runat="server" style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />  &nbsp;&nbsp; 
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
</tr>

<tr>

    <td width="100%" align="center" >
    
<asp:Panel ID="pnlPV" runat="server" Width="95%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<%--class="table table-stripped condensed"  --%>
<table width="97%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>    
        <td width="12%">
           &nbsp;
        </td>
        <td width="21%">
        &nbsp;
        </td>
         <td width="12%">
           &nbsp;
        </td>
        <td width="21%">
        &nbsp;
        </td>
         <td width="12%">
           &nbsp;
        </td>
        <td width="21%">
        &nbsp;
        </td>
    </tr>
    
     <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Country  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">
            <ContentTemplate>
            <asp:DropDownList ID="ddlCountry" runat="server"  class="form-control"  
                Width="170px" style="font-size:medium;" TabIndex="0" AutoPostBack="true"
                onselectedindexchanged="ddlCountry_SelectedIndexChanged" > </asp:DropDownList>
                </ContentTemplate>
           </asp:UpdatePanel>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Database  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlDatabase" runat="server"  class="form-control"  
                Width="180px" style="font-size:medium;" TabIndex="0"> 
                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                    <asp:ListItem Value="SAPAE" Text="SAPAE"></asp:ListItem> 
                    <asp:ListItem Value="SAPKE" Text="SAPKE"></asp:ListItem> 
                    <asp:ListItem Value="SAPTZ" Text="SAPTZ"></asp:ListItem>
                    <asp:ListItem Value="SAPUG" Text="SAPUG"></asp:ListItem>
                    <asp:ListItem Value="SAPZM" Text="SAPZM"></asp:ListItem>
                    <asp:ListItem Value="SAPML" Text="SAPML"></asp:ListItem>
                    <asp:ListItem Value="SAPTRI" Text="SAPTRI"></asp:ListItem>
             </asp:DropDownList>
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Ref No  &nbsp; </label>  
        </td>
        <td width="18%">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <asp:TextBox ID="txtRefNo" runat="server" MaxLength="10" Width="165px" class="form-control" style="font-size:medium;" TabIndex="99" Enabled="false" >
              </asp:TextBox>
            </ContentTemplate>
            </asp:UpdatePanel>
              <asp:Label ID="lblPVId" runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
     <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Voucher Type  &nbsp; </label>  
        </td>  
        <td width="21%">
           
            <asp:DropDownList ID="ddlVoucherType" runat="server"  class="form-control"  onchange="VoucherType_changed()" 
                Width="170px" style="font-size:medium;" TabIndex="0" > 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Internal" Text="Internal"></asp:ListItem> 
                <asp:ListItem Value="Vendor" Text="Vendor"></asp:ListItem> 
             </asp:DropDownList>
             
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="lblVendorEmploye" runat="server" >Vendor/Employee  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtVendorEmployee" runat="server" MaxLength="254" placeholder="Enter Name" class="form-control" Width="300px" style="font-size:medium;" >  </asp:TextBox>
             
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Voucher Status  &nbsp; </label>  
        </td>
        <td width="18%">
               <asp:DropDownList ID="ddlStatus" runat="server"  class="form-control"  Enabled="false"
                Width="165px" style="font-size:medium;" TabIndex="0"> 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Open" Text="Open"></asp:ListItem> 
                <asp:ListItem Value="Paid-Closed" Text="Paid - Closed"></asp:ListItem> 
                <asp:ListItem Value="Rejected-Closed" Text="Rejected-Closed"></asp:ListItem> 
             </asp:DropDownList>
        </td>
    </tr>

     <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Currency  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlCurrency" runat="server"  class="form-control"  
                Width="170px" style="font-size:medium;" TabIndex="0"> 
             </asp:DropDownList>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Benificiary  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtBenificiary" runat="server" MaxLength="254" class="form-control" placeholder="Enter Benificiary" Width="300px" style="font-size:medium;" >  </asp:TextBox>
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Pymnt Req Date   &nbsp; </label>  
        </td>
        <td width="18%">
               <asp:TextBox id="txtPaymentReqDate" runat="server" MaxLength="10"  placeholder="Enter payReq date" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
                 <cc1:CalendarExtender ID="txtPaymentReqDate_CalendarExtender1" runat="server" 
                                        Enabled="True" TargetControlID="txtPaymentReqDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
        </td>
    </tr>

     <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Requested Amt  &nbsp; </label>  
        </td>
        <td width="21%">
           <asp:TextBox id="txtRequestedAmount" runat="server" MaxLength="19" class="form-control" placeholder="Enter requested amt" Width="170px" style="font-size:medium;" onkeypress="javascript:return isNumberKey(event);" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">pay Method  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlPaymentMethod" runat="server"  class="form-control"  onchange="PaymentMethod_changed()"
                Width="180px" style="font-size:medium;" TabIndex="0"> 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Cheque" Text="Cheque"></asp:ListItem> 
                <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem> 
                <asp:ListItem Value="TT" Text="TT"></asp:ListItem> 
                <asp:ListItem Value="Other" Text="Other"></asp:ListItem> 
             </asp:DropDownList>
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="lblChequeImage" runat="server"  >Pay Ref Image   &nbsp; </label>  
        </td>
        <td width="18%">
               <asp:FileUpload ID="fuploadCheckImage" runat="server" width="165px"  />
                <a id="ancChequeImage" runat="server" target="_blank"  visible="false" > <b> View image </b> </a>
               <%--<asp:RegularExpressionValidator   
                        id="FileUpLoadValidator" runat="server"   
                        ErrorMessage="Upload Jpegs and Gifs only."   
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF)$"   
                        ControlToValidate="fuploadCheckImage">  
                </asp:RegularExpressionValidator> --%>
        </td>
    </tr>

      <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Approved Amt  &nbsp; </label>  
        </td>
        <td width="21%">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:TextBox id="txtApprovedAmt" runat="server" MaxLength="19" Enabled="false" class="form-control" placeholder="Enter approved amt" Width="170px" style="font-size:medium;" onkeypress="javascript:return isNumberKey(event);" >  </asp:TextBox>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="lblChequeNo" runat="server" >Pay Ref No  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtChequeNo" runat="server" MaxLength="24" placeholder="Enter refNo" class="form-control" Width="180px" style="font-size:medium;" >  </asp:TextBox>
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  id="lblChequeDate" runat="server"   >Pay Ref Date&nbsp; </label>  
        </td>
        <td width="18%">
               <asp:TextBox id="txtChequeDate" runat="server" MaxLength="10" class="form-control" placeholder="Enter date" Width="165px" style="font-size:medium;" >  </asp:TextBox>
               <cc1:CalendarExtender ID="txtChequeDate_CalendarExtender2" runat="server" 
                                        Enabled="True" TargetControlID="txtChequeDate" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
        </td>
    </tr>

     <tr>
        <td width="14%">
            <asp:TextBox id="lblVendEmployeeCode" runat="server" BorderColor="White" MaxLength="50" ForeColor="White"  Width="2px" style="font-size:medium;" TabIndex="1000"  >  </asp:TextBox>
        </td>
        <td width="21%">
           <asp:TextBox id="lblBankCode" runat="server" BorderColor="White" ForeColor="White" MaxLength="120"  Width="2px" style="font-size:medium;" TabIndex="999" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="Label1" runat="server" >Bank Name&nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtBankName" runat="server" MaxLength="255" placeholder="Enter Bank" class="form-control" Width="300px" style="font-size:medium;" >  </asp:TextBox>
            
        </td>
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  id="Label2" runat="server"   >CreatedBy&nbsp; </label>  
        </td>
        <td width="18%">
               <asp:TextBox id="txtCreatedBy" runat="server" MaxLength="50" class="form-control" Enabled="false" Width="165px" style="font-size:medium;" >  </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Being Payment Of  &nbsp; </label>  
        </td>
        <td width="54%" colspan="3">
           <asp:TextBox id="txtBeingPayOf" runat="server" MaxLength="520" placeholder="Enter Being payment of " class="form-control" Width="700px" style="font-size:medium;" >  </asp:TextBox>
        </td>
        
         <td width="14%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Created On&nbsp; </label>  
        </td>
        <td width="18%">
               <asp:TextBox id="txtCreatedOn" runat="server" MaxLength="10" class="form-control" Width="165px" style="font-size:medium;" Enabled="false" >  </asp:TextBox>
        </td>
    </tr>

    <tr>
        <td colspan="6" width="100%">
             <asp:Panel ID="pnlPVApproval" runat="server" Width="100%" Visible="false" >
             <table width="100%">
             
              <tr>
                        <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> CA approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="ddlCAapproval" runat="server"  class="form-control" 
                                Width="230px" style="font-size:medium;" TabIndex="0"> 
                                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem> 
                                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Open" Text="Rejected-Open"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Closed" Text="Rejected-Closed"></asp:ListItem> 
                             </asp:DropDownList>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">CM approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="ddlCMapproval" runat="server"  class="form-control" 
                                Width="230px" style="font-size:medium;" TabIndex="0"> 
                                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem> 
                                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Open" Text="Rejected-Open"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Closed" Text="Rejected-Closed"></asp:ListItem> 
                            </asp:DropDownList>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">CFO approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                                <asp:DropDownList ID="ddlCFOapproval" runat="server"  class="form-control" 
                                Width="230px" style="font-size:medium;" TabIndex="0"> 
                                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                    <asp:ListItem Value="Pending" Text="Pending"></asp:ListItem> 
                                    <asp:ListItem Value="Approved" Text="Approved"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Open" Text="Rejected-Open"></asp:ListItem> 
                                    <asp:ListItem Value="Rejected-Closed" Text="Rejected-Closed"></asp:ListItem> 
                                </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> CA Remarks &nbsp; </label>  
                        </td>
                        <td width="21%">
                             <asp:TextBox id="txtCAapprovalRemarks" runat="server" MaxLength="300" class="form-control"  placeholder="Enter CA Remarks" Width="230px" style="font-size:medium;" >  </asp:TextBox>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">CM Remarks &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:TextBox id="txtCMapprovalRemarks" runat="server" class="form-control" MaxLength="300"  placeholder="Enter CM Remarks" Width="230px" style="font-size:medium;" >  </asp:TextBox>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">CFO Remarks &nbsp; </label>  
                        </td>
                        <td width="21%">
                                <asp:TextBox id="txtCFOapprovalRemarks" runat="server" MaxLength="300" placeholder="Enter CFO Remarks" class="form-control" Width="230px" style="font-size:medium;" >  </asp:TextBox>
                        </td>
                    </tr>

             </table>
             </asp:Panel>
        </td>
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
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="20" onclick="BtnDelete_Click1" 
                  />

         &nbsp;&nbsp;  &nbsp;&nbsp;
        
        <asp:Button ID="BtnAddRow" Text="Add Row" runat="server" 
                class="btn btn-info" Font-Bold="true" Font-Size="Medium"  
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="21" onclick="BtnAddRow_Click" 
                 />

        &nbsp;&nbsp;  &nbsp;&nbsp;
      
        <asp:Button ID="BtnExportToPDF" Text="Download PV" runat="server" 
                class="btn btn-info" Font-Bold="true" Font-Size="Medium"  
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="21" onclick="BtnExportToPDF_Click" />
        &nbsp;&nbsp;&nbsp;

        <input type = "button" id="btnShowAgeing" class="btn btn-success" value = "View Ageing" />


        &nbsp;&nbsp;  &nbsp;&nbsp;
        
        <asp:Button ID="BtnSendMailToSignatories" Text="Send Mail To Signatories" runat="server" 
                class="btn btn-info" Font-Bold="true" Font-Size="Small"  
                style="font-family: Raleway;height:38px;width:250px;" TabIndex="21" onclick="BtnSendMailToSignatories_Click" 
                 />

        </td>
    
    </tr>
    <tr>
    <td width="100%" colspan="6" > &nbsp;
    
    <div id="dvExportPVToPDF" runat="server">
    
    </div>

    </td>
    </tr>
  </table>
  
</asp:Panel>

  </td>
</tr>
 <tr>
 <td width="100%" align="center" > &nbsp; </td>
 </tr>
 <tr>
 <td width="100%" align="center" >
    
    <asp:GridView ID="gvPVLines" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    Width="95%" Height="120%" 
                    CellPadding="4" 
                    ForeColor="#333333" GridLines="None" 
         onrowdeleting="gvPVLines_RowDeleting" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="3%" >
                            <ItemTemplate>
                                <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                <asp:Label ID="lblPVLineId" runat="server" Text='<%#Eval("PVLineId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%" >
                            <ItemTemplate>
                               <asp:TextBox ID="txtdate" runat="server" Font-Size="Medium" placeholder="Select Date"
                                    autocomplete="off" class="form-control" Text='<%#Eval("Date","{0:d}")  %>'></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                    TargetControlID="txtdate" Format="MM/dd/yyyy">
                                </cc1:CalendarExtender>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="22%" >
                            <ItemTemplate>
                                 <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description" autocomplete="off" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Description") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="8%">
                            <ItemTemplate>
                              <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtgvamt" runat="server" autocomplete="off" Font-Size="Medium" AutoPostBack="true" placeholder="Enter amount" OnTextChanged="txtgvamt_TextChanged"
                                        class="form-control" Text='<%#Eval("Amount") %>' onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                                </ContentTemplate>
                               </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="22%">
                            <ItemTemplate>
                                 <asp:TextBox ID="txtRemarks" runat="server" autocomplete="off" placeholder="Enter Remarks" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Remarks") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                         <asp:TemplateField HeaderText="View" ItemStyle-Width="10%"  >
                            <ItemTemplate>
                               &nbsp;&nbsp;  <a id="ancViewAttahcment" runat="server" href='<%#Eval("FilePath") %>' target="_blank"  ><b> View doc </b> </a>
                               <%-- <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath") %>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%" Visible="false" >
                            <ItemTemplate>
                                <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Add attachment" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:FileUpload ID="fUploadAddattachment" runat="server" />
                                <%--<asp:Label ID="lblLineRefNo" runat="server" Text='<%#Eval("LineRefNo") %>' Visible="true"></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="" ItemStyle-Width="7%"  >
                            <ItemTemplate>
                                <asp:Button ID="btnDel" runat="server" Text="Delete" Width="80%" onclick="BtnDelete_Click"  CommandName="Delete" class="btn btn-danger"  OnClientClick="return getConfirmationRowDelete();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                   
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>

 </td>
 </tr>
   
</table>

   <%-- </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="BtnAddRow" /> 
        <asp:PostBackTrigger ControlID="btnSave" /> 
    </Triggers>
</asp:UpdatePanel>--%>


<div class="modal fade" id="VendorAgeing" tabindex="-1" role="dialog" aria-labelledby="ModalTitle"
    aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                    &times;</button>
                <h4 class="modal-title" id="ModalTitle">
                    Vendor Ageing Report</h4>
            </div>
            <div class="modal-body">
                
                <table class="table table-bordered">
                   <tr class="info">
                    <th>Balance</th>
                    <th>0-30</th>
                    <th>31-45</th>
                    <th>46-60</th>
                    <th>61-90</th>
                    <th>91-120</th>
                    <th>121+</th>
                   </tr>
                   <tr>
                    <td>  <asp:Label ID="lblBalance" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl0_30" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl31_45" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl46_60" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl61_90" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl91_120" runat="server" />  </td>
                    <td>  <asp:Label ID="lbl120Plus" runat="server" />  </td>
                   </tr>
                </table>

                <div id="dvMessage" runat="server" visible="false" class="alert alert-danger">
                    <strong>Error!</strong>
                    <asp:Label ID="lblMessage" runat="server" />
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">
                    Close</button>
            </div>
        </div>
    </div>
</div>


</asp:Content>

