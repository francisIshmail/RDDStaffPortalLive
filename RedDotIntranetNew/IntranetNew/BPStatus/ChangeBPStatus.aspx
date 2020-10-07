<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ChangeBPStatus.aspx.cs" Inherits="IntranetNew_BPStatus_ChangeBPStatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>

<script type="text/javascript">
        

    $(function () {
        ddl_changed();
        $("[id$=txtCustomerName]").autocomplete({

            source: function (request, response) {

                if (($("[id$=ddlDBName]").val() == "--Select--") || ($("[id$=ddlRegion]").val() == "--Select--")) {
                    alert('please select database & region');
                    $("[id$=txtCustomerName]").val('');
                    return;
                }

                $.ajax({
                    //url: '<%=ResolveUrl("~/BPStatus/BPStatus_ChangeBPStatus.aspx/GetCustomers") %>',
                    url: "ChangeBPStatus.aspx/GetCustomers",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=ddlDBName]").val() + "','region':'" + $("[id$=ddlRegion]").val() + "','transstatus':'" + $("[id$=ddlTransStatus]").val() + "','clstatus':'" + $("[id$=ddlCLStatus]").val() + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('#')[0],
                                val: item.split('#')[1],
                                status: item.split('#')[2],
                                statusremark: item.split('#')[3],
                                clstatus: item.split('#')[4],
                                clremark: item.split('#')[5],
                                acctbal: item.split('#')[6],
                                onetofifteen: item.split('#')[7],
                                sixteentoonefifty: item.split('#')[8],
                                onefiftyplus: item.split('#')[9],
                                paymethod: item.split('#')[10],
                                creditlimit: item.split('#')[11],
                                clexpirydate: item.split('#')[12],
                                clupdatedate: item.split('#')[13],
                                clexpiryextension: item.split('#')[14],
                                tempcl: item.split('#')[15],
                                tempclexpirydate: item.split('#')[16],
                                tempclremark: item.split('#')[17]
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
                //                alert(i.item.label);

                $("[id$=txtCustomerName]").val(i.item.label);
                $("[id$=txtCardCode]").val(i.item.val);
                $("[id$=lblTransStatus]").html(i.item.status);
                $("[id$=LblTransRemarks]").html(i.item.statusremark);
                $("[id$=lblCLStatus]").html(i.item.clstatus);
                $("[id$=lblCLRemarks]").html(i.item.clremark);

                $("[id$=lblAcctBalance]").html(i.item.acctbal);
                $("[id$=lbloneTfifteenDays]").html(i.item.onetofifteen);
                $("[id$=lblSixteenTonefiftyDays]").html(i.item.sixteentoonefifty);
                $("[id$=lblOneFiftyPlusDays]").html(i.item.onefiftyplus);
                $("[id$=lblPayMethod]").html(i.item.paymethod);

                $("[id$=txtCL]").val(i.item.creditlimit);
                $("[id$=txtCLExpDt]").val(i.item.clexpirydate);
                $("[id$=txtCLUpdateDt]").val(i.item.clupdatedate);
                $("[id$=txttempCLAmt]").val(i.item.tempcl);
                $("[id$=txttempCLExpDt]").val(i.item.tempclexpirydate);
                $("[id$=txttempCLRemarks]").val(i.item.tempclremark);

                document.getElementById('<%= ddlCLExpExtn.ClientID %>').value = i.item.clexpiryextension;

                if (i.item.status == "Soft hold" || i.item.status == "Hard hold" || i.item.status == "Blocked" || i.item.status == "Closed") {
                    $("[id$=lblTransStatus]").removeClass("Active");
                    $("[id$=lblTransStatus]").removeClass("Dormant");
                    $("[id$=lblTransStatus]").addClass("SoftHardBlocked");
                }
                else if (i.item.status == "Dormant") {
                    $("[id$=lblTransStatus]").removeClass("Active");
                    $("[id$=lblTransStatus]").removeClass("SoftHardBlocked");
                    $("[id$=lblTransStatus]").addClass("Dormant");
                }
                else if (i.item.status == "Active") {
                    $("[id$=lblTransStatus]").removeClass("Dormant");
                    $("[id$=lblTransStatus]").removeClass("SoftHardBlocked");
                    $("[id$=lblTransStatus]").addClass("Active");
                }

                if (i.item.clstatus == "Limit" || i.item.clstatus == "Expired" || i.item.clstatus == "Blocked" || i.item.clstatus == "Closed") {
                    $("[id$=lblCLStatus]").removeClass("Ok");
                    $("[id$=lblCLStatus]").addClass("LimitExpiredClosed");
                }
                else if (i.item.clstatus == "Ok") {
                    $("[id$=lblCLStatus]").removeClass("LimitExpiredClosed");
                    $("[id$=lblCLStatus]").addClass("Ok");
                }
            },
            minLength: 1
        });
    });

    //// This function is used to bind the NewTrans Status & New CL Status DDL on region selection.
    function ddl_changed() {
        
        var ddl = document.getElementById('<%= ddlRegion.ClientID %>');
        //alert(ddl.value);
        if (ddl.value != "--Select--") {

            var ddlDBName = document.getElementById('<%= ddlDBName.ClientID %>').value;
             // ToBind NewTrans status ddl
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ChangeBPStatus.aspx/GetTransStatus",
                    data: "{'dbname':'" + $("[id$=ddlDBName]").val() + "','region':'" + $("[id$=ddlRegion]").val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        $("[id$=ddlNewTStatus] option").remove();
                        $.each(data.d, function (key, value) {
                            $("[id$=ddlNewTStatus]").append($("<option></option>").val(value.status).html(value.status));
                        });
                    },
                    error: function (result) {
                        alert("Error to bind new trans status");
                    }
                });

                // ToBind NewCL status ddl
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ChangeBPStatus.aspx/GetCLStatus",
                    data: "{'dbname':'" + $("[id$=ddlDBName]").val() + "','region':'" + $("[id$=ddlRegion]").val() + "'}",
                    dataType: "json",
                    success: function (data) {
                        $("[id$=ddlNewCLStatus] option").remove();
                        $.each(data.d, function (key, value) {
                            $("[id$=ddlNewCLStatus]").append($("<option></option>").val(value.status).html(value.status));

                            if (value.status == "Ok" || value.status == "Limit" || value.status == "Blocked" || value.status == "Closed" ) {
                                document.getElementById("btnCLUpdate").disabled = false;
                            }
                            else {
                                document.getElementById("btnCLUpdate").disabled = true;
                            }

                        });

                    },
                    error: function (result) {
                        alert("Error to bind new CL status");
                    }
                });
            //alert(ddl.value);
        }
    }

    /// START : Functions to show and hide loader 
    function ShowProgressBar() {
        document.getElementById('divLoader').style.visibility = 'visible';
    }
    function HideProgressBar() {
        document.getElementById('divLoader').style.visibility = 'hidden';
    }
    /// END : Functions to show and hide loader

    /// START : function to clear controls 
    function reset() {

        document.getElementById('<%= txtCardCode.ClientID %>').value = "";
        document.getElementById('<%= txtCustomerName.ClientID %>').value = "";

        $("[id$=lblTransStatus]").html("");
        $("[id$=LblTransRemarks]").html("");
        document.getElementById('<%= ddlNewTStatus.ClientID %>').value = "--select--";
        document.getElementById('<%= txtNewTStatusRemarks.ClientID %>').value = "";

        $("[id$=lblCLStatus]").html("");
        $("[id$=lblCLRemarks]").html("");
        document.getElementById('<%= ddlNewCLStatus.ClientID %>').value = "--select--";
        document.getElementById('<%= txtNewCLStatusRemarks.ClientID %>').value = "";

        document.getElementById('<%= txtCL.ClientID %>').value = "";
        document.getElementById('<%= txtCLExpDt.ClientID %>').value = "";
        document.getElementById('<%= txtCLUpdateDt.ClientID %>').value = "";
        document.getElementById('<%= ddlCLExpExtn.ClientID %>').value = "--select--";

        document.getElementById('<%= txttempCLAmt.ClientID %>').value = "";
        document.getElementById('<%= txttempCLExpDt.ClientID %>').value = "";
        document.getElementById('<%= txttempCLRemarks.ClientID %>').value = "";

        $("[id$=lblPayMethod]").html("");
        $("[id$=lblAcctBalance]").html("");
        $("[id$=lbloneTfifteenDays]").html("");
        $("[id$=lblSixteenTonefiftyDays]").html("");
        $("[id$=lblOneFiftyPlusDays]").html("");

    }
    /// End : function to clear controls 

    /// START : of function to save CL status , tempCL details

    function saveclstatus() {

        var DBName = document.getElementById('<%= ddlDBName.ClientID %>').value;
        var CardCode = document.getElementById('<%= txtCardCode.ClientID %>').value;
        var OldCLStatus = document.getElementById('<%= lblCLStatus.ClientID %>').innerText;
        var NewCLStatus = document.getElementById('<%= ddlNewCLStatus.ClientID %>').value;
        var CLStatusRemarks = document.getElementById('<%= txtNewCLStatusRemarks.ClientID %>').value;

        var CLAmt = document.getElementById('<%= txtCL.ClientID %>').value;
        var CLExpDt = document.getElementById('<%= txtCLExpDt.ClientID %>').value;
        var CLUpdateDt = document.getElementById('<%= txtCLUpdateDt.ClientID %>').value;

        var ddlCLExpExtn = document.getElementById('<%= ddlCLExpExtn.ClientID %>').value;

        var tempCLAmt = document.getElementById('<%= txttempCLAmt.ClientID %>').value;
        var tempCLExpDt = document.getElementById('<%= txttempCLExpDt.ClientID %>').value;
        var tempCLRemarks = document.getElementById('<%= txttempCLRemarks.ClientID %>').value;

        var PayMethod = document.getElementById('<%= lblPayMethod.ClientID %>').innerText;
        var arryPayMethod = PayMethod.split("[")

        if (arryPayMethod[0].trim() == "CASH") {
            alert('You can not change credit information for CASH customer.');
            return false;
        }

        if (DBName == "--Select--") {
            alert('Please select database');
            return false;
        }
        if (CardCode === "") {
            alert('Please select customer');
            return false;
        }
        if (OldCLStatus == NewCLStatus) {
            alert('New Credit limit status must be different from Old status');
            return false;
        }
        if (CLStatusRemarks === "" && NewCLStatus != "--select--") {
            alert('Please enter credit limit status change remarks');
            return false;
        }
        if (CLStatusRemarks!="" && NewCLStatus == "--select--") {
            alert('Please select new credit limit status');
            return false;
        }

        if (CLAmt === "") {
            CLAmt = 0;
        }
        if (tempCLAmt === "") {
            tempCLAmt = 0;
        }
        /// cl validation
        if (parseFloat(CLAmt) > 0) {
            if (CLExpDt === "") {
                alert('Please enter CL expiry date');
                return false;
            }
            if (CLUpdateDt === "") {
                alert('Please enter CL Update date');
                return false;
            }
        }
        /// temp cl validation
        if (parseFloat(tempCLAmt) > 0) {
            if (tempCLExpDt === "") {
                alert('Please enter Temp CL expiry date');
                return false;
            }
            if (tempCLRemarks === "") {
                alert('Please enter Temp CL Remarks');
                return false;
            }
        }
        ShowProgressBar();
        var data = { dbname: DBName, cardcode: CardCode, clstatus: NewCLStatus, clstatusremarks: CLStatusRemarks, creditlimit: CLAmt, clexpirydate: CLExpDt, clupdatedate: CLUpdateDt, cLexpiryextension: ddlCLExpExtn, tempcl: tempCLAmt, tempclexpirydate: tempCLExpDt, tempclremarks: tempCLRemarks };

        $.ajax({
            type: "POST",
            url: "ChangeBPStatus.aspx/UpdateCLStatus",
            // data: stringData,
            data: '{obj: ' + JSON.stringify(data) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSucces,
            error: OnError
        });

        function OnSucces(response) {
            HideProgressBar();
            if (response.d == "1") {
                alert('Credit Limit detail updated Successfully.');
                reset();
            }
            else {
                alert(response.d);
            }
        }
        function OnError(response) {
            HideProgressBar();
            alert(response.d);
        }

    }
    /// END : of function to save CL status , tempCL details


    ///START : of function to save customer transactional status

    function savetransstatus() {

        var DBName = document.getElementById('<%= ddlDBName.ClientID %>').value;
        var CardCode = document.getElementById('<%= txtCardCode.ClientID %>').value;
        var OldTStatus = document.getElementById('<%= lblTransStatus.ClientID %>').innerText;
        var NewTStatus = document.getElementById('<%= ddlNewTStatus.ClientID %>').value;
        var TStatusRemarks = document.getElementById('<%= txtNewTStatusRemarks.ClientID %>').value;

        if (DBName == "--Select--") {
            alert('Please select database');
            return false;
        }
        if (CardCode === "" ) {
            alert('Please select customer');
            return false;
        }
        if (NewTStatus == "--select--") {
            alert('Please select new transactional status');
            return false;
        }

        if (TStatusRemarks === "") {
            alert('Please enter transactional status change remarks');
            return false;
        }

        if (OldTStatus === NewTStatus) {
            alert('New transactional status must be different from Old status');
            return false;
        }

        ShowProgressBar();

        var data = {dbname:DBName, cardcode: CardCode, tstatus: NewTStatus, tstatusremarks: TStatusRemarks };
       // var stringData = JSON.stringify(data);
        $.ajax({
            type: "POST",
            url: "ChangeBPStatus.aspx/UpdateTransStatus",
            // data: stringData,
            data: '{obj: ' + JSON.stringify(data) + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSucces,
            error: OnError
        });

        function OnSucces(response) {
            HideProgressBar();
            if (response.d == "1") {
                alert('Transactional status updated successfully.');
                reset();
            }
            else {
                alert(response.d);
            }
        }
        function OnError(response) {
            HideProgressBar();
            alert(response.d);
        }

    }

    ///END : of function to save customer transactional status

      
</script>



 <table width="100%" align="center"  > 
<tr>
    <td width="100%" align="center" >
         <Lable style="color: #d71313;font-size:x-large;font-weight: bold;font-family: Raleway;" > &nbsp;&nbsp;&nbsp; Change Customer Status in SAP </Lable>
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
    
<asp:Panel ID="pnlBPStatusChange" runat="server" Width="70%"  Height="25%" BorderWidth="1px" BorderColor="Red" EnableTheming="true">
    
       <table  id="tblCustomers" class="table table-stripped table-condensed" width="100%" >
        <tr class="info">
            <td width="25%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;color:Red">Database &nbsp; *  </label>  </td>
            <td width="25%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold;color:Red">Region  &nbsp; * </label>  </td>
            <td width="25%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; ">Trans Status  &nbsp; (optional) </label>  </td>
            <td width="25%" align="center"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:bold; ">CL Status  &nbsp; (Optional) </label>  </td>
        </tr>

        <tr>
           
            <td width="25%">
                <asp:DropDownList ID="ddlDBName" runat="server" CssClass="form-control" > 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="SAPAE"     Text="SAPAE"></asp:ListItem>
                                <asp:ListItem Value="SAPKE"     Text="SAPKE"></asp:ListItem> 
                                <asp:ListItem Value="SAPKE-TEST" Text="SAPKE-TEST"></asp:ListItem> 
                                <asp:ListItem Value="SAPTZ"     Text="SAPTZ"></asp:ListItem> 
                                <asp:ListItem Value="SAPUG"     Text="SAPUG"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM"     Text="SAPZM"></asp:ListItem> 
                                <asp:ListItem Value="SAPZM-TEST" Text="SAPZM-TEST"></asp:ListItem> 
                 </asp:DropDownList>
            </td>
            <td width="25%">
                 <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control" onchange="ddl_changed()"> 
                 </asp:DropDownList>
                 <%--onselectedindexchanged="ddlRegion_SelectedIndexChanged" AutoPostBack="true"--%>
            </td>
            <td width="25%">
                 <asp:DropDownList ID="ddlTransStatus" runat="server" CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="A"     Text="Active"></asp:ListItem> 
                                <asp:ListItem Value="S"     Text="Soft hold"></asp:ListItem> 
                                <asp:ListItem Value="H"     Text="Hard Hold"></asp:ListItem> 
                                <asp:ListItem Value="B"     Text="Blocked"></asp:ListItem> 
                                <asp:ListItem Value="D"     Text="Dormant"></asp:ListItem> 
                 </asp:DropDownList>
            </td>
            <td width="25%">
                <asp:DropDownList ID="ddlCLStatus" runat="server" CssClass="form-control"> 
                                <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                <asp:ListItem Value="O"     Text="Ok"></asp:ListItem> 
                                <asp:ListItem Value="L"     Text="Limit"></asp:ListItem> 
                                <asp:ListItem Value="E"     Text="Expired"></asp:ListItem> 
                                <asp:ListItem Value="B"     Text="Blocked"></asp:ListItem> 
                 </asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td width="25%">  
                 <asp:TextBox ID="txtCardCode" placeholder="Customer Code" runat="server" Enabled="false"  CssClass="form-control" style="box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; " />
            </td>
            <td colspan="2" width="50%">
                <asp:TextBox ID="txtCustomerName" placeholder="Enter CustomerName" runat="server" Width="70%" CssClass="form-control" style="box-sizing: border-box;font-family: Raleway; font-size:14px; font-weight:normal; " />
            </td>
            <td width="25%" > </td>
        </tr>

        <%-- <tr>
            <td colspan="4" width="100%" >
                
            </td>
        </tr>--%>

       </table>

</asp:Panel>

  </td>
</tr>

<tr>
<td>&nbsp;</td>
</tr>

<tr>
<td align="center">
    
    <table width="100%" align="center" >
        <tr>
            <td width="49%" align="center" >  
                <%-- to show Transactional status  --%>
                <table width="100%" class="table table-bordered">
                    <tr class="info">
                        <td colspan="3" width="100%" align="center" > <b>  TRANSACTIONAL STATUS & REMARKS </b>  </td>
                    </tr>
                    <tr>
                        <td width="18%"> <asp:Label ID="lblCurrStatus" Text="Current Status" runat="server" style="font-size:14px;color:Red" /> </td>
                        <td width="18%"> <asp:Label ID="lblTransStatus" runat="server" Enabled="false"  Height="25px"   /> </td>
                       <td width="64%">  <asp:Label ID="LblTransRemarks" runat="server" Enabled="false"  style="font-size:12px;"  /></td>
                       
                    </tr>
                    <tr>
                        <td width="18%"> <asp:Label ID="lblNewStatus" Text="New Status" runat="server" style="font-size:14px;color:Blue"  /> </td>
                        <td width="18%"> <asp:DropDownList ID="ddlNewTStatus" runat="server" class="form-control"  >
                                            <%-- <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                             <asp:ListItem Value="A" Text="Active"></asp:ListItem> 
                                             <asp:ListItem Value="S" Text="Soft hold"></asp:ListItem> 
                                             <asp:ListItem Value="H" Text="Hard hold"></asp:ListItem> 
                                             <asp:ListItem Value="B" Text="Blocked"></asp:ListItem> 
                                             <asp:ListItem Value="C" Text="Closed"></asp:ListItem> 
                                             <asp:ListItem Value="D" Text="Dormant"></asp:ListItem> --%>
                                        </asp:DropDownList>
                         </td>
                        <td width="64%">  <asp:TextBox ID="txtNewTStatusRemarks" runat="server" placeholder="Enter transactional status change Remarks" style="font-size:14px;" class="form-control" /></td>
                    </tr>

                    <tr>
                       <td width="100%" colspan="3" > 
                       <input type="button" value="UPDATE " onclick="return savetransstatus();" class="btn btn-success"/>
                       <%--<asp:Button id="BtnUpdateTStatus" runat="server" Text="SAVE" class="btn btn-success"  OnClientClick="return savetransstatus();"  />   --%>
                        </td>
                    </tr>

                </table>
                <br />
                <%-- to show aginig  --%>
                <table width="100%" class="table table-bordered">

                    <tr class="info">
                        <td colspan="5" width="100%" align="center" > <b>  AGEING (As per invoice pay term) </b>  </td>
                    </tr>
                    <tr>
                        <td align="center" width="20%" > <span style="font-size:14px;color:Blue"> Pymnt Method </span> </td>
                        <td align="center" width="20%" > <span style="font-size:14px;color:Blue"> Acct Balance </span> </td>
                        <td align="center" width="20%" > <span style="font-size:14px;color:Blue"> [1-15] Days </span></td>
                        <td align="center" width="20%" > <span style="font-size:14px;color:Blue"> [16-150] Days </span></td>
                        <td align="center" width="20%" > <span style="font-size:14px;color:Blue"> [151+] Days </span></td>
                    </tr>
                    <tr>
                        <td align="center" width="20%" ><asp:Label ID="lblPayMethod" runat="server" style="font-size:14px;color:Red"  Height="23px"  /> </td>
                        <td align="center" width="20%" ><asp:Label ID="lblAcctBalance" runat="server" style="font-size:14px;color:Red"  Height="23px"  /> </td>
                        <td align="center" width="20%" ><asp:Label ID="lbloneTfifteenDays" runat="server" style="font-size:14px;color:Red" Height="23px"   /> </td>
                        <td align="center" width="20%" ><asp:Label ID="lblSixteenTonefiftyDays" runat="server" style="font-size:14px;color:Red" Height="23px"  /></td>
                        <td align="center" width="20%" ><asp:Label ID="lblOneFiftyPlusDays" runat="server" style="font-size:14px;color:Red" Height="23px"  /></td>
                    </tr>

                </table>

            </td>
            <td width="2%" >
                &nbsp;
            </td>
            <td width="49%" align="center" >  
                
                <table width="100%" class="table table-bordered">
                        <tr class="info">
                            <td colspan="5" width="100%" align="center" > <b> CREDIT LIMIT STATUS & REMARKS </b> </td>
                        </tr>
                         <tr>
                            <td width="20%">  <asp:Label ID="lblCLCurrStatus" Text="Current Status" runat="server" style="font-size:14px;color:Red"   /> </td>
                            <td width="20%">  <asp:Label ID="lblCLStatus" runat="server" Enabled="false" Height="25px"   /> </td>
                            <td colspan="3" width="60%">  <asp:Label ID="lblCLRemarks" runat="server" Enabled="false"    /> </td>
                        </tr>
                        <tr>
                            <td width="20%">  <asp:Label ID="lblCLNewStatus" Text="New Status" runat="server" style="font-size:14px;color:Blue"  /> </td>
                            <td width="20%"> <asp:DropDownList ID="ddlNewCLStatus" runat="server" class="form-control">
                                           <%--  <asp:ListItem Value="--Select--" Text="--Select--"></asp:ListItem> 
                                             <asp:ListItem Value="O" Text="Ok"></asp:ListItem> 
                                             <asp:ListItem Value="L" Text="Limit"></asp:ListItem> 
                                             <asp:ListItem Value="E" Text="Expired"></asp:ListItem> 
                                             <asp:ListItem Value="B" Text="Blocked"></asp:ListItem> 
                                             <asp:ListItem Value="C" Text="Closed"></asp:ListItem> --%>
                                        </asp:DropDownList>
                            </td>
                           <td colspan="3" width="60%">   <asp:TextBox ID="txtNewCLStatusRemarks" style="font-size:14px;" runat="server" placeholder="Enter CL status change Remarks" class="form-control" /></td>
                        </tr>

                         <tr>
                            <td width="20%">  <asp:Label ID="lblCL" Text="Credit Limit" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="20%">  <asp:TextBox ID="txtCL" style="font-size:14px;" runat="server" placeholder="Enter CL" class="form-control" /> </td>
                            <td width="20%">  <asp:Label ID="lblCLExpDt" Text="CL Expiry Date" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="40%">  <asp:TextBox ID="txtCLExpDt" style="font-size:14px;" width="60%" runat="server" placeholder="MM/DD/YYYY" class="form-control" /> 
                                 <cc1:CalendarExtender ID="txtCLExpDt_CalendarExtender1" runat="server" 
                                        Enabled="True" TargetControlID="txtCLExpDt" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                            </td>
                            <%--<td width="20%">  &nbsp; </td>--%>
                        </tr>

                        <tr>
                            <td width="20%">  <asp:Label ID="lblCLUpdateDt" Text="CL UpdateDt" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="20%">  <asp:TextBox ID="txtCLUpdateDt" style="font-size:14px;" runat="server" MaxLength="10" class="form-control"  placeholder="MM/DD/YYYY"/> 
                                         <cc1:CalendarExtender ID="txtCLUpdateDt_CalendarExtender" runat="server" 
                                        Enabled="True" TargetControlID="txtCLUpdateDt" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                            
                            </td>
                            <td width="20%">  <asp:Label ID="lblCLExpityExtn" Text="CLExpiry Extn" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="40%">   <asp:DropDownList ID="ddlCLExpExtn" runat="server" class="form-control" width="60%">
                                                     <asp:ListItem Value="--select--" Text="--select--"></asp:ListItem> 
                                                     <asp:ListItem Value="0" Text="0"></asp:ListItem> 
                                                     <asp:ListItem Value="1" Text="1"></asp:ListItem> 
                                                     <asp:ListItem Value="2" Text="2"></asp:ListItem> 
                                                     <asp:ListItem Value="3" Text="3"></asp:ListItem> 
                                                </asp:DropDownList>
                            </td>
                            <%--<td width="20%">  &nbsp; </td>--%>
                        </tr>

                        <tr>
                            <td width="20%">  <asp:Label ID="lbltempCLAmt" Text="TempCL Amt" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="20%">  <asp:TextBox ID="txttempCLAmt" style="font-size:14px;" runat="server" placeholder="Enter temp CL " class="form-control" /> </td>
                            <td width="20%">  <asp:Label ID="lbltempCLExpDt" Text="TempCL Expiry Dt" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td width="40%">  <asp:TextBox ID="txttempCLExpDt" style="font-size:14px;" width="60%" runat="server" placeholder="temp CL ExpDt" class="form-control" /> 
                                 <cc1:CalendarExtender ID="txttempCLExpDt_CalendarExtender1" runat="server" 
                                        Enabled="True" TargetControlID="txttempCLExpDt" DaysModeTitleFormat="dd/MM/yyyy" 
                                        TodaysDateFormat="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                            </td>
                            <%--<td width="20%">  &nbsp; </td>--%>
                        </tr>

                         <tr>
                            <td width="20%">  <asp:Label ID="lbltempCLRemarks" Text="TempCL Remrks" runat="server" style="font-size:14px;color:Blue"   /> </td>
                            <td colspan="4" width="80%">  <asp:TextBox ID="txttempCLRemarks" style="font-size:14px;" runat="server" placeholder="temp CL remarks" class="form-control" /> </td>
                        </tr>

                        <%--<tr>
                            <td width="20%">  <asp:Label ID="lblTempCLUsed" Text="Temp CL Used" runat="server" style="font-size:14px;color:Red"   /> </td>
                            <td width="20%">  <asp:TextBox ID="txtTempCLUsed" style="font-size:14px;" runat="server" placeholder="tepl CL used" Enabled="false" class="form-control" /> </td>
                            <td width="20%">  <asp:Label ID="lblTempCLBal" Text="Temp CL Bal" runat="server" style="font-size:14px;color:Red"   /> </td>
                            <td width="20%">  <asp:TextBox ID="txtTempCLBal" style="font-size:14px;" runat="server" Enabled="false" placeholder="BAL temp CL" class="form-control" /> </td>
                            <td width="20%">  &nbsp; </td>
                        </tr>--%>

                         <tr>
                               <td width="100%" colspan="5" >
                                <%--<asp:Button id="BtnUpdateCLStatus" runat="server" Text="SAVE" class="btn btn-success"  />    --%>
                                <input type="button" value="UPDATE" id="btnCLUpdate" onclick="return saveclstatus();" class="btn btn-success"/>
                                </td>
                        </tr>
                
                </table>
               
            </td>

        </tr>
        <tr>
            <td colspan="2" > &nbsp; <%--<asp:Button id="BtnChangeStatus" runat="server" Text="UPDATE STATUS" class="btn btn-success"  />  --%>  </td>
        </tr>
    </table>

    </td>
</tr>


</table>


 <div id="divLoader" style="visibility:hidden; position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                        <span style="border-width: 0px; position: fixed; padding: 20px; background-color: #FFFFFF; font-size: 30px; left: 40%; top: 40%; border-radius: 50px;">Please wait ...</span>
 </div>


<style type="text/css">

.SoftHardBlocked
{
    color: #d71313;
    font-size:large;
    font-weight: bold;
    font-family: Raleway;
}

.Dormant
{
color:#0d05f5;
font-size:large;
font-weight: bold;
font-family: Raleway;
}

.Active{
color:#0db854;
font-size:large;
font-weight: bold;
font-family: Raleway;
}

.Ok{
color:#0db854;
font-size:large;
font-weight: bold;
font-family: Raleway;
}

.LimitExpiredClosed
{
    color: #d71313;
    font-size:large;
    font-weight: bold;
    font-family: Raleway;
}

</style>

</asp:Content>

