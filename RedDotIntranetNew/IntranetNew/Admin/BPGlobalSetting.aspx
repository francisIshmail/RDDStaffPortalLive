<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="BPGlobalSetting.aspx.cs" Inherits="IntranetNew_Admin_BPGlobalSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script language="Javascript" type="text/javascript">

    function isNumberKey(evt) {
        debugger;
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) ) {
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
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Setup - Customer Status Global Settings </Lable>
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
    
<asp:Panel ID="pnlBPStatusGS" runat="server" Width="60%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">

<table class="table table-stripped" width="60%" align="center" cellpadding="3px" cellspacing="3px" >
    <tr>
        <td width="100%" colspan="2" align="center">
          <asp:Label ID="lblMsg" runat="server" 
                style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; color:Red; font-weight:bold; " /> 
        </td>
    </tr>
    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Grace Period for <b>CASH</b> customers in days  &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtGracePforCashBP" CssClass="form-control" width="100px" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  </td>
    </tr>
    
    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Grace Period to auto change customer from <b>Soft hold to Active </b> in days   &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtGraceForSoftToActive" CssClass="form-control" width="100px" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  </td>
    </tr>

    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Grace Period for <b>LC</b> customers in days  &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtGracePeriodforLCBP" CssClass="form-control" width="100px" runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-size:12px; "></asp:TextBox>  </td>
    </tr>   

    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Grace Period for <b>PDC</b> customers to enter PDC in system in days &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtGracePeriodforPDCBP" CssClass="form-control" width="100px"  runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>  </td>
    </tr>  

    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Maximum allowable days for <b> temp Credit Limit </b> expiry date &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtMaxAllowableDaysForTempCL" CssClass="form-control" width="100px"  runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>  </td>
    </tr>  

    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> <b>CA</b> Maximum allowable days for <b> temp Credit Limit </b> expiry date &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtCAMaxAllowableDaysForTempCL" CssClass="form-control" width="100px"  runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>  </td>
    </tr>  

    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> <b>CM</b> Maximum allowable days for <b> temp Credit Limit </b> expiry date &nbsp; </label>   </td>
        <td width="40%"> <asp:TextBox ID="txtCMMaxAllowableDaysForTempCL" CssClass="form-control" width="100px"  runat="server" MaxLength="3" onkeypress="javascript:return isNumberKey(event);" ></asp:TextBox>  </td>
    </tr>  

     <tr>
        <td width="60%"> <%--<label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> Allowable extension in months for CL Expiry ( After or Before CL is expired ) &nbsp; </label>  --%>
        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; "> For how many month(s) CL expiry can be extended &nbsp; </label>  
         </td>
        <td width="40%"> 
                            <asp:DropDownList ID="ddlCLExpiryExtnInMonth" runat="server" CssClass="form-control"  > 
                                                    <asp:ListItem Value="--SELECT--" Text="--SELECT--"></asp:ListItem> 
                                                    <asp:ListItem Value="0" Text="0"></asp:ListItem> 
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem> 
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem> 
                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem> 
                            </asp:DropDownList> 
        </td>
    </tr>  
    <tr>
        <td width="60%"> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:12px; font-weight:normal; ">  CL Expiry extension can be set before how many month(s) &nbsp; </label>   </td>
        <td width="40%"> 
                            <asp:DropDownList ID="ddlAllowToSetCLExtensionBeforeMonths" runat="server" CssClass="form-control" > 
                                                     <asp:ListItem Value="--SELECT--" Text="--SELECT--"></asp:ListItem> 
                                                    <asp:ListItem Value="0" Text="0"></asp:ListItem> 
                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem> 
                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem> 
                            </asp:DropDownList> 
        </td>
    </tr>  

     <tr>
        <td width="100%" align="left" >
          &nbsp;&nbsp;  <asp:Button ID="BtnSave" runat="server" Text="Save" class="btn btn-success"
                
                
                style="width:30%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " onclick="BtnSave_Click" 
                />  &nbsp;&nbsp; 
     
        <%--   <asp:Button ID="BtnCancel" runat="server" Text="Cancel"  class="btn btn-default"
                style="width:30%;padding: 5px 12px; margin: 4px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; " />--%>
        </td>
       
    </tr>

</table>

</asp:Panel>

  </td>
</tr>


</table>

</asp:Content>

