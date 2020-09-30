<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="PVNew.aspx.cs" Inherits="IntranetNew_BPStatus_PVNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>

<script type="text/javascript">

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

    function VoucherType_changed() {

        var ddlVoucherType = document.getElementById('<%= ddlVoucherType.ClientID %>').value
        if (ddlVoucherType === "Internal") {
            $("[id$=lblVendorEmploye]").html("Employee");
        }
        else if (ddlVoucherType === "Vendor") {
            $("[id$=lblVendorEmploye]").html("Vendor");
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
        }
        else if (ddlPaymentMethod == "TT") {
            $("[id$=lblChequeNo]").html("TT No");
            $("[id$=lblChequeDate]").html("TT Date");
        }
        else {
            $("[id$=lblChequeNo]").html("Pay Ref No");
            $("[id$=lblChequeDate]").html("Pay Ref Date");
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


        var ddlPaymentMethod = document.getElementById('<%= ddlPaymentMethod.ClientID %>').value
        var txtApprovedAmt = document.getElementById('<%= txtApprovedAmt.ClientID %>').value
        var txtChequeNo = document.getElementById('<%= txtChequeNo.ClientID %>').value
        var txtChequeDate = document.getElementById('<%= txtChequeDate.ClientID %>').value

        if (ddlCountry == "--Select--") {
            alert('Please select country');
            document.getElementById('<%= ddlCountry.ClientID %>').focus();
            return false;
        }

        if (ddlDatabase == "--Select--") {
            alert('Please select database');
            document.getElementById('<%= ddlDatabase.ClientID %>').focus();
            return false;
        }

        if (ddlVoucherType == "--Select--") {
            alert('Please select Voucher Type');
            document.getElementById('<%= ddlVoucherType.ClientID %>').focus();
            return false;
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

    }

</script>

 <asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">
    <ContentTemplate>
<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable id="lblformName" runat="server" style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Payment Voucher  </Lable>
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
    
<asp:Panel ID="pnlPV" runat="server" Width="95%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
<%--class="table table-stripped condensed"  --%>
<table width="95%" align="center" cellpadding="3px" cellspacing="3px" class="table table-stripped condensed" >
    
   <%-- <tr>    
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
    </tr>--%>
    
     <tr>
        <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Country  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlCountry" runat="server"  class="form-control"  
                Width="170px" style="font-size:medium;" TabIndex="0" AutoPostBack="True" 
                onselectedindexchanged="ddlCountry_SelectedIndexChanged"> </asp:DropDownList>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">Database  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlDatabase" runat="server"  class="form-control"  
                Width="220px" style="font-size:medium;" TabIndex="0"> 
                    <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                    <asp:ListItem Value="SAPAE" Text="SAPAE"></asp:ListItem> 
                    <asp:ListItem Value="SAPKE" Text="SAPKE"></asp:ListItem> 
                    <asp:ListItem Value="SAPTZ" Text="SAPTZ"></asp:ListItem>
                    <asp:ListItem Value="SAPUG" Text="SAPUG"></asp:ListItem>
                    <asp:ListItem Value="SAPZM" Text="SAPZM"></asp:ListItem>
             </asp:DropDownList>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Ref No  &nbsp; </label>  
        </td>
        <td width="21%">
              <asp:TextBox ID="txtRefNo" runat="server" MaxLength="10" Width="165px" class="form-control" style="font-size:medium;" TabIndex="99" Enabled="false" >
              </asp:TextBox>
        </td>
    </tr>
     <tr>
        <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Voucher Type  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:DropDownList ID="ddlVoucherType" runat="server"  class="form-control"  onchange="VoucherType_changed()"
                Width="170px" style="font-size:medium;" TabIndex="0"> 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Internal" Text="Internal"></asp:ListItem> 
                <asp:ListItem Value="Vendor" Text="Vendor"></asp:ListItem> 
             </asp:DropDownList>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="lblVendorEmploye" runat="server" >Vendor/Employee  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtVendorEmployee" runat="server" MaxLength="254" placeholder="Enter Name" class="form-control" Width="220px" style="font-size:medium;" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Voucher Status  &nbsp; </label>  
        </td>
        <td width="21%">
               <asp:DropDownList ID="ddlStatus" runat="server"  class="form-control"  
                Width="165px" style="font-size:medium;" TabIndex="0"> 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Open" Text="Open"></asp:ListItem> 
                <asp:ListItem Value="Paid - Closed" Text="Paid - Closed"></asp:ListItem> 
                <asp:ListItem Value="Rejected-Closed" Text="Rejected-Closed"></asp:ListItem> 
             </asp:DropDownList>
        </td>
    </tr>

     <tr>
        <td width="12%">
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
            <asp:TextBox id="txtBenificiary" runat="server" MaxLength="254" class="form-control" placeholder="Enter Benificiary" Width="220px" style="font-size:medium;" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Payment Req Date   &nbsp; </label>  
        </td>
        <td width="21%">
               <asp:TextBox id="txtPaymentReqDate" runat="server" MaxLength="10"  placeholder="Enter payReq date" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="12%">
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
                Width="220px" style="font-size:medium;" TabIndex="0"> 
                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                <asp:ListItem Value="Cheque" Text="Cheque"></asp:ListItem> 
                <asp:ListItem Value="Cash" Text="Cash"></asp:ListItem> 
                <asp:ListItem Value="TT" Text="TT"></asp:ListItem> 
                <asp:ListItem Value="Other" Text="Other"></asp:ListItem> 
             </asp:DropDownList>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Check Image   &nbsp; </label>  
        </td>
        <td width="21%">
               <asp:FileUpload ID="fuploadCheckImage" runat="server" width="165px"  />
               <asp:RegularExpressionValidator   
                        id="FileUpLoadValidator" runat="server"   
                        ErrorMessage="Upload Jpegs and Gifs only."   
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF)$"   
                        ControlToValidate="fuploadCheckImage">  
                </asp:RegularExpressionValidator> 
        </td>
    </tr>

      <tr>
        <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Approved Amt  &nbsp; </label>  
        </td>
        <td width="21%">
           <asp:TextBox id="txtApprovedAmt" runat="server" MaxLength="19" class="form-control" placeholder="Enter approved amt" Width="170px" style="font-size:medium;" onkeypress="javascript:return isNumberKey(event);" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;" id="lblChequeNo" runat="server" >Pay Ref No  &nbsp; </label>  
        </td>
        <td width="21%">
            <asp:TextBox id="txtChequeNo" runat="server" MaxLength="24" placeholder="Enter refNo" class="form-control" Width="220px" style="font-size:medium;" >  </asp:TextBox>
        </td>
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"  id="lblChequeDate" runat="server"   >Pay Ref Date&nbsp; </label>  
        </td>
        <td width="21%">
               <asp:TextBox id="txtChequeDate" runat="server" MaxLength="10" class="form-control" placeholder="Enter date" Width="165px" style="font-size:medium;" >  </asp:TextBox>
        </td>
    </tr>

     <tr>
        <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> Being Payment Of:  &nbsp; </label>  
        </td>
        <td width="54%" colspan="3">
           <asp:TextBox id="txtBeingPayOf" runat="server" MaxLength="500" placeholder="Enter Being payment of " class="form-control" Width="720px" style="font-size:medium;" >  </asp:TextBox>
        </td>
        
         <td width="12%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Created On&nbsp; </label>  
        </td>
        <td width="21%">
               <asp:TextBox id="txtCreatedOn" runat="server" MaxLength="10" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
        </td>
    </tr>

    <tr>
        <td colspan="6" width="100%">
             <asp:Panel ID="pnlApproval" runat="server" Width="100%" Visible="false" >
             <table width="100%">
             
              <tr>
                        <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> CA approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="ddlCAapproval" runat="server"  class="form-control" 
                                Width="170px" style="font-size:medium;" TabIndex="0"> </asp:DropDownList>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">CM approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:DropDownList ID="ddlCMapproval" runat="server"  class="form-control" 
                                Width="170px" style="font-size:medium;" TabIndex="0"> </asp:DropDownList>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">CFO approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                                <asp:DropDownList ID="ddlCFOapproval" runat="server"  class="form-control" 
                                Width="170px" style="font-size:medium;" TabIndex="0"> </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;"> CA approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                             <asp:TextBox id="txtCAapprovalRemarks" runat="server" MaxLength="300" class="form-control" placeholder="Enter CA Remarks" Width="165px" style="font-size:medium;" >  </asp:TextBox>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal;">CM approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                            <asp:TextBox id="txtCMapprovalRemarks" runat="server" class="form-control" MaxLength="300" placeholder="Enter CM Remarks" Width="165px" style="font-size:medium;" >  </asp:TextBox>
                        </td>
                         <td width="12%">
                           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">CFO approval &nbsp; </label>  
                        </td>
                        <td width="21%">
                                <asp:TextBox id="txtCFOapprovalRemarks" runat="server" MaxLength="300" placeholder="Enter CFO Remarks" class="form-control" Width="165px" style="font-size:medium;" >  </asp:TextBox>
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
                OnClientClick="return Validate();" />

          &nbsp;&nbsp;  &nbsp;&nbsp;   
        
        <asp:Button ID="BtnDelete" Text="Delete" runat="server" class="btn btn-danger" 
                Enabled="false"   OnClientClick="return getConfirmation();"
                Font-Bold="true" Font-Size="Medium" 
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="20" 
                  />

         &nbsp;&nbsp;  &nbsp;&nbsp;
        
        <asp:Button ID="BtnAddRow" Text="Add Row" runat="server" 
                class="btn btn-info" Font-Bold="true" Font-Size="Medium"  
                style="font-family: Raleway;height:38px;width:150px;" TabIndex="21" 
                 />
      
        </td>
    
    </tr>
  </table>

</asp:Panel>

  </td>
</tr>
 
 <tr>
 <td width="100%" align="center" >
    
    <asp:GridView ID="gvPVLines" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                    ShowFooter="True"
                    Width="95%" Height="120%" 
                    CellPadding="4" 

                    ForeColor="#333333" GridLines="None" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No" ItemStyle-Width="3%" >
                            <ItemTemplate>
                                    <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                    <asp:Label ID="lblPVLineId" runat="server" Text='<%#Eval("PVLineId") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server"  Font-Size="Medium" Text='<%#Eval("Date","{0:d}")  %>'></asp:Label>
<%--                               <asp:TextBox ID="txtdate" runat="server" Font-Size="Medium"
                                    autocomplete="off" class="form-control" Text='<%#Eval("Date","{0:d}")  %>'></asp:TextBox>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:TextBox ID="txtdateEdit" runat="server" Font-Size="Medium"
                                    autocomplete="off" class="form-control" Text='<%#Eval("Date","{0:d}")  %>'></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateEdit_CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                    TargetControlID="txtdateEdit" Format="MM/dd/yyyy">
                                </cc1:CalendarExtender>
                            </EditItemTemplate>
                            <FooterTemplate>
                                 <asp:TextBox ID="txtdateFooter" runat="server" Font-Size="Medium"
                                        autocomplete="off" class="form-control" Text='<%#Eval("Date","{0:d}")  %>'></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdateFooter_CalendarExtender1" PopupButtonID="imgPopup" runat="server"
                                        TargetControlID="txtdateFooter" Format="MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                            <ItemTemplate>
                                 <asp:Label ID="lblDescription" runat="server"  Font-Size="Medium" Text='<%#Eval("Description") %>'></asp:Label>
                               <%--  <asp:TextBox ID="txtDescription" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Description") %>'></asp:TextBox>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescriptionEdit" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                       Text='<%#Eval("Description") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                 <asp:TextBox ID="txtDescriptionFooter" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Amount" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="lblgvamt" runat="server"  Font-Size="Medium" Text='<%#Eval("Amount") %>'></asp:Label>
                                <%--<asp:TextBox ID="txtgvamt" runat="server" autocomplete="off" Font-Size="Medium"
                                    class="form-control" Text='<%#Eval("Amount") %>'  ></asp:TextBox>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                 <asp:TextBox ID="txtgvamtEdit" runat="server" autocomplete="off" Font-Size="Medium"
                                    class="form-control" Text='<%#Eval("Amount") %>' onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                     <asp:TextBox ID="txtgvamtFooter" runat="server" autocomplete="off" Font-Size="Medium"
                                        class="form-control" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <%-- <asp:TextBox ID="txtRemarks" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Remarks") %>'></asp:TextBox>--%>
                                   <asp:Label ID="lblRemarks" runat="server"  Font-Size="Medium" Text='<%#Eval("Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRemarksEdit" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Remarks") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtRemarksFooter" runat="server" autocomplete="off" class="form-control" Font-Size="Medium"
                                   Text='<%#Eval("Remarks") %>'></asp:TextBox>
                            </FooterTemplate>
                        </asp:TemplateField>
                      
                         <asp:TemplateField HeaderText="View attachment" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <a Id="ancViewAttahcment" runat="server" href='<%#Eval("FilePath") %>' target="_blank" >  </a>
                               <%-- <asp:Label ID="lblFilePath" runat="server" Text='<%#Eval("FilePath") %>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:FileUpload ID="fUploadAttachmentEdit" runat="server" />
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:FileUpload ID="fUploadAttachmentFooter" runat="server"  />
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
                    <%--<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />--%>
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                   <%-- <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />--%>
                </asp:GridView>


 </td>
 </tr>
   
</table>

    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>

