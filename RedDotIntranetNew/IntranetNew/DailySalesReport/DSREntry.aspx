<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master" AutoEventWireup="true" CodeFile="DSREntry.aspx.cs" Inherits="IntranetNew_DailySalesReport_DSREntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">



        $(function () {

            $("[id$=txtprsonmeet]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "DailySalesMasterNew.aspx/GetPersonMeet",
                        data: "{ 'prefix': '" + request.term + "','dbcustomer':'" + $("[id$=txtCustomerName").val() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            // alert(data.d);
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('#')[0]//,
                                    //  val: item.split('#')[1]
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
                    //alert(i.item.label);

                    $("[id$=txtprsonmeet]").val(i.item.label);
                    //$("[id$=txtCardCode]").val(i.item.val);

                },
                minLength: 1
            });
        });

        function getConfirmationOnSubmit() {
            return confirm(" You won't be able to make any change and report will be sent to your manager once submitted. Are you sure you want to submit report ? ");
        }

        function ValidateCustomer() {


            //            if ($("[id$=txtCardCode]").val() == "" && $("[id$=ddlCountry]").val() != "--SELECT--") {
            //                alert('Please select valid CustomerName');
            //                //               $("[id$=txtCustomerName]").val('');
            //                //                   
            //                return;

        }

        //        }
        $(function () {
            $('[id*=ddlBU]').multiselect({
                includeSelectAllOption: true,
                nonSelectedText: 'Select BU',
                enableFiltering: true,
                buttonWidth: '150px'

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

            $("[id$=txtCustomerName]").autocomplete({


                source: function (request, response) {

                    if ($("[id$=ddlCountry]").val() == "--SELECT--") {
                        alert('please select country');
                        $("[id$=txtCustomerName]").val('');
                        return;
                    }

                    //debugger;

                    $.ajax({
                        url: "DailySalesMasterNew.aspx/GetCustomers",
                        data: "{ 'prefix': '" + request.term + "','dbcountry':'" + $("[id$=lblCountry").html() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            // alert(data.d);
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
                    //                alert(i.item.label);
                    $("[id$=txtCustomerName]").val(i.item.label);
                    $("[id$=txtCardCode]").val(i.item.val);
                    // getCustomerSummary(i.item.val);
                    GetDistinctOrRepeat(i.item.label);
                    // getCustomerSummary2(i.item.val);
                },
                minLength: 1


            });
        });



        function GetDistinctOrRepeat(customername) {
            var s = customername;
            $.ajax({
                url: "DailySalesMasterNew.aspx/GetCutomerDisRpt",
                data: "{ 'dbcode': '" + s + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                type: "POST",
                success: function (data) {

                    var theString = data.d;

                    var arySummary = new Array();
                    arySummary = theString.split("#");
                    $("[id$=txtdisrptt]").val(arySummary[0]);


                    // $("[id$=txtdisrptt]").html(arySummary[0]);

                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }

            });

        }




        // Auto seach of customer Name in Edit Mode
        $(function () {

            $("[id$=txtcustomernameEdit]").autocomplete({

                source: function (request, response) {

                    if ($("[id$=ddlCountry]").val() == "--SELECT--") {
                        alert('please select country');
                        $("[id$=txtCustomerName]").val('');
                        return;
                    }
                    debugger;

                    $.ajax({
                        url: "DailySalesMasterNew.aspx/GetCustomers",

                        data: "{ 'prefix': '" + request.term + "','dbcountry':'" + $("[id$=ddlCountry").val() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            // alert(data.d);
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
                    //                alert(i.item.label);
                    $("[id$=txtCustomerName]").val(i.item.label);
                    $("[id$=txtCardCode]").val(i.item.val);
                    // getCustomerSummary(i.item.val);

                    // getCustomerSummary2(i.item.val);
                },
                minLength: 1
            });
        });



        $(function () {

            $("[id$=txtprsonmeet]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "DailySalesMasterNew.aspx/GetPersonDetails",
                        data: "{ 'prefix': '" + request.term + "','dbcustomer':'" + $("[id$=txtCustomerName").val() + "','dbpermeet':'" + $("[id$=txtprsonmeet").val() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            //  alert(data.d);
                            response($.map(data.d, function (item) {
                                return {

                                    label: item.split('#')[0],
                                    val: item.split('#')[1],
                                    email: item.split('#')[1],
                                    design: item.split('#')[2],
                                    contact: item.split('#')[3]
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
                    //alert(i.item.label);
                    $("[id$=txtprsonmeet]").val(i.item.label);
                    $("[id$=txtemailid]").val(i.item.email);
                    $("[id$=txtdesig]").val(i.item.design);
                    $("[id$=txtconnumber]").val(i.item.contact);

                },
                minLength: 1
            });
        });


        //        function checkEnteredDate() {
        //            var today = new Date();
        //            var dd = today.getDate();
        //            var mm = today.getMonth() + 1;
        //            var yyyy = today.getFullYear();
        //            var today = dd + '/' + mm + '/' + yyyy;

        //            //  alert(today);

        //            var VisitDate = new Date(document.getElementById('<%= txtvisitDate.ClientID %>').value);
        //            var dd1 = VisitDate.getDate();
        //            var mm1 = VisitDate.getMonth() + 1;
        //            var yyyy1 = VisitDate.getFullYear();
        //            var ff = dd1 + '/' + mm1 + '/' + yyyy1;

        //            if (dd > dd1) {
        //                {
        //                    alert('You Are Submitting Late Report');
        //                    return false;

        //                }
        //            }
        //        }



        function CallForwardValidation() {
            var txtdesc = document.getElementById('<%= txtdesc.ClientID %>').value;
            var txtfrdcall = document.getElementById('<%= txtfrdcall.ClientID %>').value;

            if (txtfrdcall === "") {
                alert('Please enter farward call to Email Id');
                document.getElementById('<%= txtfrdcall.ClientID %>').focus();
                return false;
            }

            if (txtdesc === "") {
                alert('Please enter description to forward call');
                document.getElementById('<%= txtdesc.ClientID %>').focus();
                return false;
            }
        }
        function reminderValidation() {
            var txtreminderdate = document.getElementById('<%= txtreminderdate.ClientID %>').value;
            var txtdescription = document.getElementById('<%= txtdescription.ClientID %>').value;

            if (txtdescription === "") {
                alert('Please enter reminder description');
                document.getElementById('<%= txtdescription.ClientID %>').focus();
                return false;
            }

            if (txtreminderdate === "") {
                alert('Please enter reminder Date');
                document.getElementById('<%= txtreminderdate.ClientID %>').focus();
                return false;
            }
        }
        
      
  
          
    </script>

    <table width="100%" align="center">
        <tr>
            <td width="100%" align="center" class="style1">
                <lable id="lblformName" runat="server" style="color: #d71313; font-size: x-large;
                    font-weight: bold; font-family: Raleway;"> &nbsp;&nbsp;&nbsp;Daily/Weekly Report </lable>
            </td>
        </tr>
        <tr>
            <td width="100%" align="center">
                <asp:Label ID="lblms" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 16px; color: Red; font-weight: bold;" />
                &nbsp;&nbsp;
                 <asp:Label ID="lblErrorMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" />
                
                 <asp:Label ID="lblCountry" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 10px; color: White; font-weight: bold;" />
            </td>
        </tr>
        <tr>
            <td width="100%" align="center">
                <asp:Panel ID="pnlnewDeal" runat="server" Width="100%" Height="550px" BorderWidth="1px"
                    BorderColor="Red" EnableTheming="true">
                    <table width="100%" align="center">
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="vertical-align: baseline;">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td width="100%" rowspan="4" align="right">
                                <asp:GridView ID="GvRptSummary" runat="server" RowStyle-BorderWidth="1" RowStyle-BorderColor="Red"  HeaderStyle-BorderColor="Black"
                                    class="rounded_corners" Font-Bold="True" HeaderStyle-BackColor="#d71313" HeaderStyle-ForeColor="Black"
                                    RowStyle-BackColor="#A1DCF2" RowStyle-ForeColor="#3A3A3A" Width="60%" BackColor="White" OnRowCreated="GvRptSummary_RowCreated"
                                    BorderColor="#CC9966" BorderStyle="None" CellPadding="4">
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#d71313" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />

                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; font-size: small;">
                           <%--
                                <label style="text-align: right; width: 120px;">
                                   Country
                                </label>--%>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCountry" runat="server" class="form-control" Width="105px" Visible="false"
                                    BackColor="AliceBlue" TabIndex="1" Font-Size="Small">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <%--<asp:Label ID="Label2" runat="server" Text="" Visible="false"></asp:Label>--%>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <label style="text-align: right; width: 120px; font-size: small;">
                                    Visit From Date
                                </label>
                            </td>   <%-- onfocusout="javascript:checkEnteredDate();"--%>
                            <td width="2%">
                        
                                <asp:TextBox ID="txtvisitDate" runat="server" Width="105px" class="form-control"
                                    AutoPostBack="true" TabIndex="2" placeholder="dd/mm/yyyy" autocomplete="off"
                                 Font-Size="small" BackColor="AliceBlue"
                                    OnTextChanged="txtvisitDate_TextChanged">
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtvisitDate"
                                    DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                </cc1:CalendarExtender>

                            </td>
                            <td width="2%" style="vertical-align: top; font-size: small;">
                                <asp:Label ID="Label1" runat="server" Text=" To Date" Font-Bold="true" Style="text-align: right"
                                    Width="80px"></asp:Label>
                            </td>
                            <td width="2%">
                                <asp:TextBox ID="txttodate" runat="server" Width="105px" class="form-control" TabIndex="2"
                                    placeholder="dd/mm/yyyy" autocomplete="off" 
                                    Font-Size="small" BackColor="AliceBlue">
                                </asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txttodate"
                                    DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="8">
                               
                                <asp:Button ID="btnsaveasdraft" runat="server" class="btn btn-primary" Font-Bold="true"
                                    ToolTip="Save As Draft" Font-Size="Small" Style="width: 7%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;"
                                    OnClientClick="return ValidateDraft();" Text="DRAFT" TabIndex="15" OnClick="btnsaveasdraft_Click1" />&nbsp;&nbsp;
                                <asp:Button ID="btnSave" runat="server" Text="SUBMIT" class="btn btn-success" Font-Size="Small"
                                    ToolTip="Submit Final Report" Style="width: 7%; padding: 5px 12px; margin: 4px 0;
                                    box-sizing: border-box; font-family: Raleway; font-size: 14px;" OnClientClick="return getConfirmationOnSubmit();"
                                    TabIndex="14" OnClick="btnSave_Click" />
                                &nbsp;
                                <asp:Button ID="btncancel" runat="server" class="btn btn-danger" Font-Bold="true"
                                    Font-Size="Small" OnClick="btncancel_Click" Style="width: 7%; padding: 5px 12px;
                                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;"
                                    Text="CANCEL" TabIndex="15" />&nbsp;&nbsp;
                                      <a href="#">
                                     <asp:Button ID="btnShowModalPopup" runat="server" Text="ADD RESELLER" class="btn btn-success"  ToolTip="Add New Reseller"   OnClick="btnShowModalPopup_Click"   Font-Bold="true"
                                            Font-Size="Small" Style="width: 8%; padding: 5px 12px;margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;" />
                                      </a>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6" width="100%">
                                <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                               <%-- <asp:LinkButton ID="btnShowModalPopup" runat="server" Text="" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="popUpStyle"
                                    DropShadow="false" PopupControlID="divPopUp" PopupDragHandleControlID="panelDragHandle"
                                    TargetControlID="btnShowModalPopup" />--%>

                                <asp:LinkButton ID="btnShowModalPopup1" runat="server" Text="" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="popUpStyle"
                                    DropShadow="false" PopupControlID="divPopUp" PopupDragHandleControlID="panelDragHandle"
                                    TargetControlID="btnShowModalPopup1" />

                                       <asp:LinkButton ID="btnShowModalPopup8" runat="server" Text="" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender8" runat="server" BackgroundCssClass="popUpStyle"
                                    DropShadow="false" PopupControlID="div2" PopupDragHandleControlID="pnlnewreseller"
                                    TargetControlID="btnShowModalPopup" />

                                <asp:LinkButton ID="btnShowModalPopup2" runat="server" Text="" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="popUpStyle"
                                    DropShadow="false" PopupControlID="div1" PopupDragHandleControlID="pnlreminder"
                                    TargetControlID="btnShowModalPopup2" />
                                <br />

                                <div id="div2" class="popUpStyle" style="display: none;">
                                    <asp:Panel ID="pnlnewreseller" runat="Server" BackColor="Beige" BorderColor="Red"
                                        BorderWidth="1px" CssClass="myPanelClass" Height="35%" Width="150%">
                                        <table border="2" width="100%">
                                            <tr class="info">
                                                <td align="center" colspan="2" width="100%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: bold;">
                                                    Add New Reseller &nbsp;
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                       Country &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:DropDownList ID="ddlcountryList" runat="server" class="form-control" Width="80%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                       Reseller Name &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtnewreseller" runat="server" autocomplete="off" class="form-control"
                                                        Font-Size="small" placeholder="Enter NewReseller" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <asp:Label ID="Label2" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:Label ID="Label6" runat="server" Text="0" Visible="false"></asp:Label>
                                             <asp:Label ID="Label7" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:Label ID="Label8" runat="server" Text="0" Visible="false"></asp:Label>
                                            <tr>
                                                <td align="center" colspan="2" width="100%">
                                                    <asp:Button ID="btNewReseller" runat="server" class="btn btn-success" OnClick="btNewReseller_Click"
                                                        OnClientClick="return NewResellerValidation()" Text="Save" />

                                                    <asp:Button ID="btnexit" runat="server" class="btn btn-danger" Text="Close" OnClick="btnexit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--   --%>
                                </div>

                                <div id="divPopUp" class="popUpStyle" style="display: none;">
                                    <asp:Panel ID="panelDragHandle" runat="Server" BackColor="Beige" BorderColor="Red"
                                        BorderWidth="1px" CssClass="myPanelClass" Height="35%" Width="150%">
                                        <table border="2" width="100%">
                                            <tr class="info">
                                                <td align="center" colspan="2" width="100%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: bold;">
                                                        Forward Call Details &nbsp;
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                        Forward Call To &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtfrdcall" runat="server" autocomplete="off" class="form-control"
                                                        Font-Size="small" placeholder="Enter Email Id" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                        Description / Instruction &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtdesc" runat="server" autocomplete="off" class="form-control"
                                                        Font-Size="small" placeholder="Enter description" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <asp:Label ID="LblForwardCallId" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:Label ID="lblForwardDesc" runat="server" Text="0" Visible="false"></asp:Label>
                                             <asp:Label ID="LblreminderDateID" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:Label ID="LblreminderDesc" runat="server" Text="0" Visible="false"></asp:Label>
                                            <tr>
                                                <td align="center" colspan="2" width="100%">
                                                    <asp:Button ID="btSave" runat="server" class="btn btn-success" OnClick="btSave_Click"
                                                        OnClientClick="return CallForwardValidation()" Text="Save" />

                                                    <asp:Button ID="btnClose" runat="server" class="btn btn-danger" Text="Close"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--  --%>
                                </div>
                                <div id="div1" class="popUpStyle" style="display: none;">
                                    <asp:Panel ID="pnlreminder" runat="Server" BackColor="Beige" BorderColor="Red" BorderWidth="1px"
                                        CssClass="myPanelClass" Height="35%" Width="150%">
                                        <table border="2" width="100%">
                                            <tr class="info">
                                                <td align="center" colspan="2" width="100%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: bold;">
                                                        Reminder &nbsp;
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                         Date &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtreminderdate" runat="server" autocomplete="off" class="form-control"
                                                        Font-Size="small" placeholder="Enter Reminder Date" Width="80%"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtreminderdate"
                                                        DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>

                                                     
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="20%">
                                                    <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway font-size:14px;
                                                        font-weight: normal;">
                                                        Description &nbsp;
                                                    </label>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtdescription" runat="server" autocomplete="off" class="form-control" TextMode="MultiLine"
                                                        Font-Size="small" placeholder="Enter description" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <asp:Label ID="Label4" runat="server" Text="0" Visible="false"></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="0" Visible="false"></asp:Label>
                                            <tr>
                                                <td align="center" colspan="2" width="100%">
                                                    <asp:Button ID="btnsavee" runat="server" class="btn btn-success" Text="Save" OnClick="btnsavee_Click"   OnClientClick="return reminderValidation()" />
                                                    <asp:Button ID="Button2" runat="server" class="btn btn-danger" Text="Close"  />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <%--  --%>
                                </div>
                                <asp:GridView ID="grdQuartlyRowDetail" runat="server" AlternatingRowStyle-BackColor="#C5C5C5"
                                    AutoGenerateColumns="false" DataKeyNames="VisitId" ForeColor="#333333" OnDataBound="OnDataBound"
                                    OnRowCancelingEdit="grdQuartlyRowDetail_RowCancelingEdit" OnRowCommand="grdQuartlyRowDetail_RowCommand"
                                    OnRowDataBound="grdQuartlyRowDetail_RowDataBound" OnRowDeleting="grdQuartlyRowDetail_RowDeleting"
                                    OnRowEditing="grdQuartlyRowDetail_RowEditing" OnRowUpdating="grdQuartlyRowDetail_RowUpdating"
                                    ShowFooter="true" ShowHeaderWhenEmpty="true" Width="100%">
                                    <AlternatingRowStyle BackColor="#C5C5C5" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblvisitid" runat="server" Font-Size="Small" 
                                                    Text='<%#Eval("VisitId")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcardcode" runat="server" Font-Size="Small" ForeColor="White" 
                                                    Text='<%#Eval("CardCode")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCardCodeEdit" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    placeHolder="Enter Customer" Text='<%#Eval("CardCode")%>' Visible="false" 
                                                    Width="1%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCardCode" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="white" class="form-control" Font-Size="Small" 
                                                    TabIndex="3" Visible="false"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcountry" runat="server" Font-Size="Small" 
                                                    Text='<%#Eval("country")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCountryEdit" runat="server" 
                                                    AppendDataBoundItems="True" AutoPostBack="True" class="form-control" 
                                                    Font-Size="Small" onselectedindexchanged="ddlCountry_SelectedIndexChanged" 
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlcountryfooter" runat="server" AutoPostBack="True" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    onselectedindexchanged="ddlCountry_SelectedIndexChanged" TabIndex="12" 
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" 
                                            HeaderText="Date &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Font-Size="Small"
                                                    Text='<%#Eval("ActualVisitDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%-- <asp:TextBox ID="txtreminder" runat="server" BackColor="White" BorderColor="Green" class="form-control"  ontextchanged="txtDateEdit_TextChanged"
                                                    Font-Size="Small" MaxLength="50" Text='<%#Eval("ReminderDate","{0:MM/dd/yyyy}")%>' Visible="false"></asp:TextBox>--%>
                                                <asp:TextBox ID="txtDateEdit" runat="server" BackColor="White"  AutoPostBack="true" 
                                                    BorderColor="Green" class="form-control" Font-Size="small"  autocomplete="off" 
                                                    placeholder="dd/mm/yyyy" TabIndex="2" 
                                                    Text='<%#Eval("ActualVisitDate","{0:MM/dd/yyyy}")%>' Width="105px"  ontextchanged="txtDateEdit_TextChanged">
                                </asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" 
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" TargetControlID="txtDateEdit"  
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDateFooter" runat="server" autocomplete="off"  AutoPostBack="true"
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="small" 
                                                    ontextchanged="txtDateFooter_TextChanged" placeholder="dd/mm/yyyy" TabIndex="2" 
                                                    Width="105px">
                                </asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender7" runat="server" 
                                                    DaysModeTitleFormat="dd/MM/yyyy" Enabled="True" TargetControlID="txtDateFooter" 
                                                    TodaysDateFormat="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" 
                                            HeaderText="Call Mode &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcallMode" runat="server" Text='<%#Eval("ModeOfCall")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%-- <asp:TextBox ID="ddlstatuscall" Width="120px" runat="server" Text='<%#Eval("FunnelStatus")%>'  BackColor="White" BorderColor="Brown" class="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlcallmode" runat="server" AppendDataBoundItems="True" 
                                                    class="form-control" Font-Size="Small" Width="100%">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlststusofcallMode" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" TabIndex="12" 
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" 
                                            HeaderText="Call Type &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcallstatus" runat="server" Text='<%#Eval("CallStatus")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%-- <asp:TextBox ID="ddlstatuscall" Width="120px" runat="server" Text='<%#Eval("FunnelStatus")%>'  BackColor="White" BorderColor="Brown" class="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlstatuscall" runat="server" AppendDataBoundItems="True" 
                                                    class="form-control" Font-Size="Small" Width="100%">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlststusofcall" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" TabIndex="12" 
                                                    Width="100%">
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="10%" 
                                            HeaderText="Company &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt; ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcustomername" runat="server" Font-Size="Small" 
                                                    Text='<%#Eval("Company")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtcustomernameEdit" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="255" 
                                                    placeHolder="Enter Customer" Text='<%#Eval("Company")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCustomerName" runat="server" autocomplete="off" 
                                                    AutoPostBack="true" BackColor="White" BorderColor="Green" class="form-control" 
                                                    Font-Size="Small" MaxLength="255" OnTextChanged="txtCustomerName_OnTextChanged" 
                                                    placeHolder="Enter Customer" TabIndex="3" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="8%" 
                                            HeaderText="Contact Person &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPermeet" runat="server" Text='<%#Eval("PersonMet")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPermeet" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="150" 
                                                    Text='<%#Eval("PersonMet")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtprsonmeet" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="150" placeHolder="Enter Person" TabIndex="4" Width="100%">
                                                 
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="7%" HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEmail" runat="server" BackColor="White" BorderColor="Green" 
                                                    class="form-control" Font-Size="Small" MaxLength="100" 
                                                    Text='<%#Eval("Email")%>' Width="100%"></asp:TextBox>
                                                  <asp:RegularExpressionValidator ID="RFVEditEmail" runat="server" 
                                                        ControlToValidate="txtEmail" ErrorMessage="Invalid EmailID" 
                                                        Font-Bold="True" ForeColor=" #ff0000" 
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtemailid" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="100" placeHolder="Enter Email" TabIndex="5" Width="100%">
                                                </asp:TextBox>
                                                 <asp:RegularExpressionValidator ID="RFVFooterEmail" runat="server" 
                                                        ControlToValidate="txtemailid" ErrorMessage="Invalid EmailID" Font-Bold="True" 
                                                        ForeColor=" #ff0000" 
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="7%" HeaderText="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblconno" runat="server" Text='<%#Eval("ContactNo")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtconnumber" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="15" 
                                                    onkeypress="javascript:return isNumberKey(event);" 
                                                    Text='<%#Eval("ContactNo")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtconnumber" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="15" onkeypress="javascript:return isNumberKey(event);" 
                                                    placeHolder="Enter Phoneno" TabIndex="6" Width="100%">
                                                   
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesign" runat="server" Text='<%#Eval("Designation")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdesign" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="50" 
                                                    Text='<%#Eval("Designation")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtdesig" runat="server" autocomplete="off" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="50" 
                                                    placeHolder="Enter Designation" TabIndex="7" Width="100%">
                                                  
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" HeaderText="BU">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBU" runat="server" Text='<%#Eval("BU")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBUUedit" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="3500" Text='<%#Eval("BU")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBU" runat="server" autocomplete="off" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="3500" 
                                                    placeHolder="Enter BU" TabIndex="9" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="4%" 
                                            HeaderText="D/R&lt;span style='Font-Size:15px' &gt; &nbsp;? &lt;/span&gt;">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label3" runat="server" HeaderStyle-Width="5%" Text="D / R" 
                                                    ToolTip="D-Distinct/R-Repeat"></asp:Label>
                                                <asp:ImageButton ID="btn1" runat="server" Enabled="false" 
                                                    ImageUrl="~/outer css-js/images/que1.png" ToolTip="D-Distinct/R-Repeat" 
                                                    Width="20px" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldisrpt" runat="server" Enabled="false" 
                                                    Text='<%#Eval("VisitType")%>' ToolTip="D-Distinct/R-Repeat"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdisrptEdit" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Enabled="false" Font-Size="Small" 
                                                    MaxLength="50" Text='<%#Eval("VisitType")%>' ToolTip="D-Distinct/R-Repeat" 
                                                    Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtdisrptt" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Enabled="false" Font-Size="Small" 
                                                    MaxLength="50" TabIndex="9" ToolTip="D-Distinct/R-Repeat" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="10%" 
                                            HeaderText="Discussion &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldiscussion" runat="server" Text='<%#Eval("Discussion")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtctiondon" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="3000" 
                                                    Text='<%#Eval("Discussion")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtactiondone" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="3000" placeHolder="Enter Discussion" TabIndex="10" Width="100%"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="6%" 
                                            HeaderText="Next Action &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnextaction" runat="server" Text='<%#Eval("NextAction")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlNextActionEdit" runat="server" class="form-control" 
                                                    AppendDataBoundItems="True" 
                                                  Font-Size="Small" Width="100%" >
                                                   
                                                </asp:DropDownList>

                                               

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlnxtactiFooter" runat="server" BorderColor="Green" 
                                                    class="form-control">
                                                   
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="4%" HeaderText="Biz($)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblexpbusAmt" runat="server" ForeColor="Red" 
                                                    Text='<%#Eval("ExpectedBusinessAmt")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtexpbusAmt" runat="server" BackColor="Red" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="6" 
                                                    onkeypress="javascript:return isNumberKey(event);" 
                                                    Text='<%#Eval("ExpectedBusinessAmt")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtexpectedbuss" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="6" onkeypress="javascript:return isNumberKey(event);" 
                                                    placeHolder="Enter Biz($)" TabIndex="11" Text="0" Width="100%"> </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="header-center" 
                                            HeaderStyle-Font-Size="Small" HeaderStyle-Width="18%" 
                                            HeaderText="FeedBack &lt;span style='Font-Size:22px' &gt; &nbsp;* &lt;/span&gt;">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfeedbck" runat="server" Text='<%#Eval("Feedback")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfeedbck" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="1000" 
                                                    Text='<%#Eval("Feedback")%>' Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtfeedback" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="1000" placeHolder="Enter Feedback" TabIndex="13" Width="100%">
                                               
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%">
                                            <EditItemTemplate>
                                                <div class="dropdown">
                                                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown" style="background-color: #00CCFF;  
                                                        width: 10%; height: 20%;" title="Forward Call/Set reminder" type="button">
                                                        <span class="caret"></span>
                                                    </button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:ImageButton ID="btnShowModalPopup1" runat="server" 
                                                                CommandName="Forward CallTo" Height="20px" ImageAlign="Left" 
                                                                ImageUrl="~/outer css-js/images/arr1.png" OnClick="btnShowModalPopup1_Click" 
                                                                ToolTip="Forward CallTo" Width="30px" />
                                                            </a></li>
                                                        <li><a href="#">
                                                            <asp:ImageButton ID="btnShowModalPopup2" runat="server" CommandName="Reminder" 
                                                                Height="20px" ImageAlign="Left" ImageUrl="~/outer css-js/images/reminder1.png" 
                                                                OnClick="btnShowModalPopup2_Click" ToolTip="Reminder" Width="20px" />
                                                            </a></li>
                                                        </a></li>
                                                    </ul>
                                                </div>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <div class="dropdown">
                                                    <button class="btn btn-primary dropdown-toggle" data-toggle="dropdown" style="background-color: #00CCFF;  
                                                        width: 10%; height: 20%;" title="Forward Call/Set reminder" type="button">
                                                        <span class="caret"></span>
                                                    </button>
                                                    <ul class="dropdown-menu">
                                                        <li><a href="#">
                                                            <asp:ImageButton ID="btnShowModalPopup1" runat="server" 
                                                                CommandName="Forward CallTo" Height="20px" ImageAlign="Left" 
                                                                ImageUrl="~/outer css-js/images/arr1.png" OnClick="btnShowModalPopup1_Click" 
                                                                ToolTip="Forward CallTo" Width="30px" />
                                                            </a></li>
                                                        <li><a href="#">
                                                            <asp:ImageButton ID="btnShowModalPopup2" runat="server" CommandName="Reminder" 
                                                                Height="20px" ImageAlign="Left" ImageUrl="~/outer css-js/images/reminder1.png" 
                                                                OnClick="btnShowModalPopup2_Click" ToolTip="Reminder" Width="20px" />
                                                            </a></li>
                                                        </a></li>
                                                    </ul>
                                                </div>
                                            </FooterTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="0%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcall" runat="server" Font-Size="Small" 
                                                    Text='<%#Eval("ForwardCallToEmail")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtcall" runat="server" BackColor="White" BorderColor="Green" 
                                                    class="form-control" Font-Size="Small" MaxLength="50" 
                                                    Text='<%#Eval("ForwardCallToEmail")%>' Visible="false"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtcall" runat="server" autocomplete="off" BackColor="White" 
                                                    BorderColor="Green" class="form-control" MaxLength="50" 
                                                    placeHolder="Enter forwardCall To Email" Visible="false">
                                                   
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="0%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrnk" runat="server" Text='<%#Eval("ForwardRemark")%>' 
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtdescc" runat="server" BackColor="White" BorderColor="Green" 
                                                    class="form-control" Font-Size="Small" MaxLength="1000" 
                                                    Text='<%#Eval("ForwardRemark")%>' Visible="false"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtdescc" runat="server" autocomplete="off" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="1000" 
                                                    placeHolder="Enter Forward remark" Visible="false">
                                                   
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="Small" HeaderStyle-Width="0%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblreminder" runat="server" Font-Size="Small" 
                                                    Text='<%#Eval("ReminderDate","{0:MM/dd/yyyy}")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtreminder" runat="server" BackColor="White"   autocomplete="off"
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="50" 
                                                    Text='<%#Eval("ReminderDate","{0:MM/dd/yyyy}")%>' Visible="false"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtreminder" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" MaxLength="50" 
                                                    placeHolder="Enter forwardCall To Email" Visible="false">
                                                   
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="0%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblremidesc" runat="server" Text='<%#Eval("ReminderDesc")%>' 
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtremdescc" runat="server" BackColor="White" 
                                                    BorderColor="Green" class="form-control" Font-Size="Small" MaxLength="500" 
                                                    Text='<%#Eval("ReminderDesc")%>' Visible="false"></asp:TextBox>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtremdescc" runat="server" autocomplete="off" 
                                                    BackColor="White" BorderColor="Green" class="form-control" Font-Size="Small" 
                                                    MaxLength="500" placeHolder="Enter Forward remark" Visible="false">
                                                   
                                                </asp:TextBox>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="BtnEdit" runat="server" CommandName="Edit" Height="20px" 
                                                    ImageUrl="~/outer css-js/images/editt.png" ToolTip="Edit" Width="20px" />
                                                &nbsp;
                                                <asp:ImageButton ID="BtnDelete" runat="server" 
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                                    CommandName="Delete" Height="20px" 
                                                    ImageUrl="~/outer css-js/images/icons8-trash.png" 
                                                    OnClientClick="return confirm('Are you sure you want delete');" 
                                                    ToolTip="Delete" Width="20px" />
                                                <%--    <asp:ImageButton runat="server" ID="btnShowModalPopup"  OnClick="btnShowModalPopup_Click"  ImageUrl="~/outer css-js/images/arrow.png"  CommandName="Forward CallTo" ToolTip="Forward CallTo" Height="20px" Width="20px"  />--%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="BtnSave" runat="server" CommandName="Update" Height="20px" 
                                                    ImageUrl="~/outer css-js/images/save-icon.png" ToolTip="Update" Width="20px" />
                                                &nbsp;
                                                <asp:ImageButton ID="BtnCancel" runat="server" CommandName="Cancel" 
                                                    Height="20px" ImageUrl="~/outer css-js/images/Cancel-icon.png" ToolTip="Cancel" 
                                                    Width="20px" />
                                                <%-- <asp:ImageButton runat="server" ID="btnShowModalPopup"  OnClick="btnShowModalPopup_Click"  ImageUrl="~/outer css-js/images/arrow.png"  CommandName="Forward CallTo" ToolTip="Forward CallTo" Height="20px" Width="20px"  />--%>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <%--  <asp:ImageButton ID="btnShowModalPopup" runat="server" 
                                                    CommandName="Forward CallTo" Height="20px" 
                                                    ImageUrl="~/outer css-js/images/arr1.png" OnClick="btnShowModalPopup_Click" 
                                                    ToolTip="Forward CallTo" Width="30px" />--%>
                                                <asp:ImageButton ID="BtnAddNew" runat="server" CommandName="AddNew" 
                                                    Height="20px" ImageUrl="~/outer css-js/images/icons8-add-new.png" 
                                                    ToolTip="Add New" Width="20px" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="4%" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#d71313" Font-Bold="True" ForeColor="White" Height="15px" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <%--   RowStyle-BackColor="#A1DCF2"--%>
                                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                    <%--FDF5AC--%>
                                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                    <%--4D0000--%>
                                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                    <%--FCF6C0--%>
                                    <SortedDescendingHeaderStyle BackColor="#820000" />
                                    <%--820000--%>
                                </asp:GridView>
                                <%--   #C70039--%><%--#990000--%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="6">
                                &nbsp;
                            </td>
                        </tr>
        </tr>
    </table>
    </asp:Panel> </td> </tr>
    <tr>
        <td width="80%" align="left">
            <%-- <asp:Button ID="btnsaveasdraft" runat="server" class="btn btn-primary" Font-Bold="true" ToolTip="Save As Draft"
                    Font-Size="Small" Style="width: 10%; padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 14px;" OnClientClick="return ValidateDraft();"
                    Text="DRAFT" TabIndex="15" OnClick="btnsaveasdraft_Click1" />&nbsp;&nbsp;

                <asp:Button ID="btnSave" runat="server" Text="SUBMIT" class="btn btn-success" Font-Size="Small" ToolTip="Submit Final Report"
                    Style="width: 10%; padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                    font-family: Raleway; font-size: 14px;" OnClientClick="return getConfirmationOnSubmit();" TabIndex="14"
                    OnClick="btnSave_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btncancel" runat="server" class="btn btn-danger" Font-Bold="true"
                    Font-Size="Small" OnClick="btncancel_Click" Style="width: 10%; padding: 5px 12px;
                    margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;"
                    Text="CANCEL" TabIndex="15" />&nbsp;&nbsp;--%>
            <asp:Button ID="btngotolist" runat="server" class="btn btn-info" Font-Bold="true"
                Visible="false" Font-Size="Small" align="left" Style="width: 10%; padding: 5px 12px;
                margin: 4px 0; box-sizing: border-box; font-family: Raleway; font-size: 14px;"
                Text="Go To List" TabIndex="16" OnClick="btngotolist_Click" />
        </td>
    </tr>
    <tr>
        <td width="100%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <%--  <asp:GridView ID="GvRptSummary1" runat="server"  class="rounded_corners" 
                Width="30%" HeaderStyle-BackColor="#3AC0F2"  HeaderStyle-ForeColor="Black" 
                RowStyle-BackColor="#A1DCF2" AlternatingRowStyle-BackColor="Gray"  
                RowStyle-ForeColor="#3A3A3A" Font-Bold="True" BackColor="White" 
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
               <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />

<HeaderStyle BackColor="#990000" ForeColor="#FFFFCC" Font-Bold="True"></HeaderStyle>

               <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />

<RowStyle BackColor="White" ForeColor="#330099"></RowStyle>
               <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
               <SortedAscendingCellStyle BackColor="#FEFCEB" />
               <SortedAscendingHeaderStyle BackColor="#AF0101" />
               <SortedDescendingCellStyle BackColor="#F6F0C0" />
               <SortedDescendingHeaderStyle BackColor="#7E0000" />
            </asp:GridView>--%>
        </td>
    </tr>
    <%-- <tr>
        <td>
             <asp:Panel ID="pnlRewardList" runat="server" Width="40%" Height="25%" BorderWidth="1px"
                    BorderColor="Red" EnableTheming="true">
                    <asp:GridView ID="GrdSummery" runat="server" AutoGenerateColumns="False"
                        Width="100%" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                        BorderWidth="1px" CellPadding="3">
                        <Columns>
                        <asp:TemplateField HeaderText="MMM" ControlStyle-BackColor="Brown" ControlStyle-BorderColor="Red" ControlStyle-ForeColor="White">
                                <ItemTemplate>
                                  <asp:Label ID="lbltmm" runat="server" Text='<%#Eval("MMM")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Visits">
                                <ItemTemplate>
                                    <asp:Label ID="lblvis" runat="server" Text='<%#Eval("Visit")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Distinct">
                                <ItemTemplate>
                                    <asp:Label ID="lbldis" runat="server" Text='<%#Eval("Distint")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Repeat">
                                <ItemTemplate>
                                    <asp:Label ID="lblrpt" runat="server" Text='<%#Eval("Repeatt")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="($)Closed">
                                <ItemTemplate>
                                    <asp:Label ID="lblclo" runat="server" Text='<%#Eval("Closed")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Action Result">
                                <ItemTemplate>
                                    <asp:Label ID="lblacfrw" runat="server" Text='<%#Eval("ActionFrw")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#E66F22" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        </asp:Panel>
        </td>
        </tr>--%>
    <%-- <tr>
            <td>
                <asp:Panel ID="pnlRewardList" runat="server" Width="20%" Height="25%" BorderWidth="1px"
                    BorderColor="Red" EnableTheming="true">
                    <asp:GridView ID="GrvListAll" runat="server" AutoGenerateColumns="False" ForeColor="#333333"
                        OnSelectedIndexChanged="GrvListAll_SelectedIndexChanged" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select"> <u> <%# Eval("VisitDate", "{0:d}")%> </u>  </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VisitDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("VisitDate","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No.Of Customers Visited" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcusvis" runat="server" Text='<%#Eval("VIST_COUNT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TotalBizz($)" ItemStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalexp" runat="server" Text='<%#Eval("TOTAL_EXPECTED_AMT")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="lbldatee" runat="server" Text='<%#Eval("VisitDate")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                       
                        </Columns>
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#808080" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#808080" ForeColor="#333333" HorizontalAlign="Center" />
                        <RowStyle BackColor="#C0C0C0" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Blue" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>--%>
    </table>
    <style type="text/css">
        
    
        .header-center
        {
            text-align: center;
        }
        
        .myPanelClass
        {
            display: block;
            min-width: 200px;
            min-height: 200px;
            width: 600px;
            height: 600px;
        }
        
        
        .required
        {
            content: "*";
            font-weight: small;
            color: white;
        }
        
        
        
        .rounded_corners td, .rounded_corners th
        {
         //   border: 1px solid #de2a16;
          border: 1px solid #000000;
            font-family: Arial;
            font-size: 10pt;
            text-align: center;
        }
        .rounded_corners table table td
        {
            border-style: none;
        }
        
        
        .style1
        {
            height: 20px;
        }
    </style>
    <%--<style type="text/css">

.popUpStyle{ position: fixed; text-align: center; height: 50%; width: 50%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:white; opacity: 0.7;}


</style>
    --%>
</asp:Content>
