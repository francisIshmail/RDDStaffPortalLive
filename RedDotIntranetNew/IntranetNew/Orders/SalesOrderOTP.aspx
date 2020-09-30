<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="SalesOrderOTP.aspx.cs" Inherits="IntranetNew_Orders_SalesOrderOTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script src="https://code.jquery.com/jquery-3.3.1.js" type="text/javascript"></script>
    <script src="../Orders/js/SOROTP.js" type="text/javascript"></script>
<table width="100%" align="center"> 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; SOR - Code Generator </Lable>
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
    
<asp:Panel ID="pnlForms" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table width="90%" align="center" cellpadding="3px" cellspacing="3px" >
    
    <tr>
        <td width="20%">
           &nbsp;
        </td>
        <td width="30%">
           &nbsp;
        </td>
        <td width="20%">
        &nbsp;
        </td>
        <td width="30%">
        <input type="hidden" id="lblDocEntry" />
        </td>
    </tr>
    
 
      <tr>

        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Database  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:DropDownList ID="ddlDatabase" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>
        </td>

        <td width="20%">
            <asp:Label ID="lbl" runat="server"  Visible="false"
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " />
                <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Date  &nbsp; </label>  
        </td>
        <td width="30%">
           <asp:TextBox ID="txtDate" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Calibri; font-size:14px;" Enabled="false"></asp:TextBox> 
        </td>

    </tr>

     <tr>

        <td width="20%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Draft SOR Num  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:TextBox ID="txtDraftSORNum" runat="server" onkeypress="javascript:return isNumberKey(event);" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Calibri; font-size:14px; "></asp:TextBox> 
        </td>

        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Country/Project  &nbsp; </label>  
        </td>
        <td width="30%">
<%--            <asp:DropDownList ID="ddlCountry" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>--%>
            <asp:TextBox ID="txtCountry" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px;" Enabled="false"></asp:TextBox> 
        </td>

    </tr>

     <tr>
        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">BU  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:DropDownList ID="ddlBU" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>
        </td>

        <td width="20%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">SalesPerson  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:TextBox ID="txtSalesPerson" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " Enabled="false"></asp:TextBox> 
        </td>
        
    </tr>

    <tr>
        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Requested By  &nbsp; </label>  
        </td>
        <td width="30%">
            <asp:DropDownList ID="ddlRequestedBy" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>
        </td>

        <td width="20%">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">CA/CM  &nbsp; </label>  
        </td>
        <td width="30%">
             <asp:DropDownList ID="ddlCACM" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; ">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Approval Code  &nbsp; </label>  
        </td>
        <td width="30%">
           
            <asp:TextBox ID="txtApprovalCode" runat="server" style="width:90%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Calibri; font-size:14px; " Font-Bold="true" Enabled="false"></asp:TextBox> 
        </td>

        <td width="20%">
            <button id="BtnGenerateOTP" type="button" style="font-weight: bold; font-family: 
                                    height: 33px; width: 150px;" class="btn btn-primary">
                                    <span></span><b>Generate Code</b>
                           </button>
        </td>
        <td width="30%">
            &nbsp;
        </td>
        
    </tr>
    <tr>
        <td width="20%" align="left">
           <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; ">Remarks  &nbsp; </label>  
        </td>
        <td width="80%" colspan="3">
             <asp:TextBox ID="txtRemarks" runat="server" style="width:96%;padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; "></asp:TextBox> 
        </td>
    </tr>
     

     <tr>
        <td width="80%" align="left" colspan="3">
          &nbsp;&nbsp;   <button id="btnSave" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 250px;" class="btn btn-primary">
                                    <span></span><b>Save & Send Email</b>
                           </button> &nbsp;&nbsp;
                           
                            <button id="BtnView" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 250px;" class="btn btn-primary">
                                    <span></span><b>View Order</b>
                           </button> &nbsp;&nbsp; 
      
                        <button id="btnCancel" type="button" style="font-weight: bold; font-family: Verdana;
                                    height: 33px; width: 150px;" class="btn btn-danger">
                                    <span></span><b>Cancel</b>
                           </button>

        </td>
        <td width="20%">
        &nbsp;
        </td>
    </tr>

    <tr>
        
        <td align="left" colspan="2">
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

<%--<tr>
    <td width="100%" align="center">
        <asp:Panel ID="pnlFormList" runat="server" Width="90%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
        </asp:Panel>
    </td>
</tr>--%>


</table>





<div id="classModal" class="modal fade large bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="classInfo" aria-hidden="true">
        <div class="modal-dialog modal-lg" style="width: 70%; height: 50%;">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ffc34d;">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×
                    </button>
                    <h5 class="modal-title" id="classModalLabel" style="color: White; font-weight: bold;
                        font-family: Raleway;">
                        Draft Sales Order List
                    </h5>
                </div>
                <div>
                   
                </div>
                <div class="modal-body" style="overflow: auto; height: 50%;">
                    <table id="classTable" class="table table-bordered" style="padding-left: 0.5%; padding-right: 0.5%;">
                        <thead>
                        </thead>
                        <tbody>
                            <tr style="background-color: #ffe6e6; font-weight: bold">
                                <td style="text-align: center;">
                                    #
                                </td>
                                <td style="text-align: center;">
                                    DocEntry
                                </td>
                                <td style="text-align: center;">
                                    SAP DocNum
                                </td>
                                <td style="text-align: center;">
                                    Ref. No.
                                </td>
                                <td>
                                    Posting Date
                                </td>
                                <td>
                                    Customer
                                </td>
                                <td>
                                    Order Total
                                </td>
                                <td>
                                    Total Sell
                                </td>
                                <td>
                                    Total Cost
                                </td>
                                <td>
                                    Total Rebate
                                </td>
                                <td>
                                    Total GP
                                </td>
                                <td>
                                    SalesPerson
                                </td>
                                <td>
                                    Project
                                </td>

                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">
                        Close
                    </button>
                </div>
            </div>
        </div>
    </div>



<div id="divLoaderSOROTP" style="visibility:hidden; position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                      <%--  <span style="border-width: 0px; position: fixed; padding: 20px; background-color: #FFFFFF; font-size: 30px; left: 40%; top: 40%; border-radius: 50px;">Generating code ...</span>--%>
                      <span class="loader" >please wait...</span>
</div>


<style>
.loader {
  border: 16px solid #f3f3f3;
  border-radius: 50%;
  border-top: 16px solid blue;
  border-bottom: 16px solid blue;
  width: 120px;
  height: 120px;
  -webkit-animation: spin 2s linear infinite;
  animation: spin 2s linear infinite;
  padding: 15px; 
  font-size:larger;
  font-weight:bold;
  color:White;
  position: fixed;
  left: 40%; top: 40%; 
  align:center;
}

@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>

</asp:Content>

