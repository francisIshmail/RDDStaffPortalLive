<%@ Page Title="" Language="C#" MasterPageFile="~/IntranetNew/IntranetNew.master"
    AutoEventWireup="true" CodeFile="DailySalesMaster.aspx.cs" Inherits="IntranetNew_Daily_Sales_Report_DailySalesMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
  



    <script type="text/javascript">
      
     
         $(function () {
          $('[id*=ddlBU]').multiselect({
                 includeSelectAllOption: true,
                 nonSelectedText: 'Select BU',
                 enableFiltering: true,
                 buttonWidth: '165px'

             });
         });

           function getConfirmation() {
        return confirm(' Are you sure, You want to Delete Record?');
    }
             
                    

        $(function () {

            $("[id$=txtCustomerName]").autocomplete({


                source: function (request, response) {

                 if ($("[id$=ddlCountry]").val() == "--SELECT--")  {
                    alert('please select country');
                    $("[id$=txtCustomerName]").val('');
                    return;
                }


                    $.ajax({
                        url: "DailySalesMaster.aspx/GetCustomers",
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
                    getCustomerSummary(i.item.val);
                   // getCustomerSummary1(i.item.val);
                    getCustomerSummary2(i.item.val);
                },
                minLength: 1
            });
        });


         /*to get count As per Visit and ExpctedBussiness*/

        function getCustomerSummary(customercode){
        var s =customercode;

        $.ajax({
            url: "DailySalesMaster.aspx/GetCustomersDinfo",
            data: "{ 'dbcode': '" + s + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                var theString = data.d;

                var arySummary = new Array();
                arySummary = theString.split("#");
              
                //alert(arySummary[0]);


                $("[id$=lblcountvisited]").html(arySummary[0]);

                $("[id$=lblexpbuss]").html(arySummary[1]);
                $("[id$=lblcreatedon]").html(arySummary[2]);

              //  $("[id$=lbldiff]").html(arySummary[3]);
               
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }


        });            /*Ajax End*/  //lblcountvisited     

    }

    /*END*/


    function getCustomerSummary1(customercode) {
        var s = customercode;

        $.ajax({
            url: "DailySalesMaster.aspx/GetCustomerscreated",
            data: "{ 'dbcode': '" + s + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                var theString = data.d;

                var arySummary = new Array();
                arySummary = theString.split("#");

                //alert(arySummary[0]);


                $("[id$=lblcreatedon]").html(arySummary[0]);

              

            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }


        });            /*Ajax End*/  //lblcountvisited     

    }

    function getCustomerSummary2(customercode) {
        var s = customercode;

        $.ajax({
            url: "DailySalesMaster.aspx/visitdiff",
            data: "{ 'dbcode': '" + s + "' }",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {

                var theString = data.d;

                var arySummary = new Array();
                arySummary = theString.split("#");

                //alert(arySummary[0]);


                $("[id$=lbldiff]").html(arySummary[0]);



            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }


        });            /*Ajax End*/  //lblcountvisited     

    }


        $(function () {

            $("[id$=txtprsonmeet]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "DailySalesMaster.aspx/GetPersonMeet",
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
        

         $(function () {

             $("[id$=txtprsonmeet]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: "DailySalesMaster.aspx/GetPersonDetails",
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

                                    email:item.split('#')[1],
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
      

         function ValidateCustomer() {
           

            if ($("[id$=txtCardCode]").val() == ""  && $("[id$=ddlCountry]").val() != "--SELECT--" )
            {
               alert('Please select valid CustomerName');
//               $("[id$=txtCustomerName]").val('');
//                   
               return;

            }
            
        }
      function ResetResultValue() {
        var el = document.getElementById('<%= txtCardCode.ClientID %>');
        el.value = '';
    }
       
   // START : Function to save new Reseller
   function saveReseller() {
          
        var partner = document.getElementById('<%= txtnewpartnername.ClientID %>').value;
        var countryName =  document.getElementById('<%= ddlCountry.ClientID %>').value;

      if(countryName == "--SELECT--")
      {
            alert('Please select Country');
                return false;
      }
      if(partner == "")
      {
         alert('Please Enter New Reseller Name');
                return false;
      }
        $.ajax({
            type: "POST",
            url: "DailySalesMaster.aspx/Addreseller",
            data: "{ 'partnerName': '" + partner + "','country':'" + countryName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSucces,
            error: OnError
        });

        function OnSucces(response) {
            if (response.d == "1") {
                 var txtnewpartnername = document.getElementById("<%= txtnewpartnername.ClientID %>");
                 document.getElementById("<%= txtnewpartnername.ClientID %>").value="";
                 txtnewpartnername.style.display = "none";
                document.getElementById("Button").style.visibility= "hidden";
                alert('Reseller added successfully.');
                
            }
            else {
                alert(response.d);
            }
        }
        function OnError(response) {
            alert(response.d);
        }
    }

// END : Function to save new Reseller


      function newreseller(){
           // alert('In func');
           var ddlnewpartner = document.getElementById('<%= ddlnewpartner.ClientID %>').value;
           var txtnewpartnername = document.getElementById("<%= txtnewpartnername.ClientID %>");
         
         //  alert(txtnewpartnername);
        // alert(btnsavereseller);
          if(ddlnewpartner=="0"){
            txtnewpartnername.style.display = "none";
          document.getElementById("Button").style.visibility= "hidden";
          }
          else{
           txtnewpartnername.style.display = "";
            document.getElementById("Button").style.visibility = "inherit";
        
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

      function ValidateDraft()
        
         {
                 
         var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
            if (ddlCountry == "--SELECT--") {
                alert('Please select Country');
                return false;
            }

           
             var txtCardCode = document.getElementById('<%= txtCardCode.ClientID %>').value;
               
            if (txtCardCode.trim() == "" || txtCardCode.trim() == null) {
                alert('Please select Valid Customer');
              //  document.getElementById('<%= txtCustomerName.ClientID %>').focus();
                return false;
               
            }

//          var txtCustomerName = document.getElementById('<%= txtCustomerName.ClientID %>').value;
//            if (txtCustomerName.trim() == "" || txtCustomerName.trim() == null) {
//                alert('Please select Customer');
//                document.getElementById('<%= txtCustomerName.ClientID %>').focus();
//                return false;
//            }

             var txtvisitDate = document.getElementById('<%= txtvisitDate.ClientID %>').value;
            if (txtvisitDate.trim() == "" || txtvisitDate.trim() == null) {
                alert('Please enter Visit Date');
                document.getElementById('<%= txtvisitDate.ClientID %>').focus();
                return false;
            }

            var ddltypeofvisit = document.getElementById('<%= ddltypeofvisit.ClientID %>').value;
            if (ddltypeofvisit == "--SELECT--") {
                alert('Please select Type Of Visit');
                     document.getElementById('<%= ddltypeofvisit.ClientID %>').focus();
                return false;
            }

              var txtactiondone = document.getElementById('<%= txtactiondone.ClientID %>').value;
            if (txtactiondone.trim() == "" || txtactiondone.trim() == null) {
                alert('Please enter Discussion');
                document.getElementById('<%= txtactiondone.ClientID %>').focus();
                return false;
            }
           
         }


        function Validate() {

         var ddlCountry = document.getElementById('<%= ddlCountry.ClientID %>').value;
            if (ddlCountry == "--SELECT--") {
                alert('Please select Country');
                return false;
            }

//              var txtforwardcallto = document.getElementById('<%= txtforwardcallto.ClientID %>').value;              
//            if (txtforwardcallto.trim() != "" || txtforwardcallto.trim() != null) {
//                alert('Please enter Forward Call Remark');
//                return false;
//            }


   var txtCardCode = document.getElementById('<%= txtCardCode.ClientID %>').value;
               
            if (txtCardCode.trim() == "" || txtCardCode.trim() == null) {
                alert('Please select Valid Customer');
              //  document.getElementById('<%= txtCustomerName.ClientID %>').focus();
                return false;
               
            }

//             var txtCustomerName = document.getElementById('<%= txtCustomerName.ClientID %>').value;
//            if (txtCustomerName.trim() == "" || txtCustomerName.trim() == null) {
//                alert('Please select Customer');
//                document.getElementById('<%= txtCustomerName.ClientID %>').focus();
//                return false;
//            }

            var txtvisitDate = document.getElementById('<%= txtvisitDate.ClientID %>').value;
            if (txtvisitDate.trim() == "" || txtvisitDate.trim() == null) {
                alert('Please enter Visit Date');
                document.getElementById('<%= txtvisitDate.ClientID %>').focus();
                return false;
            }

            var ddltypeofvisit = document.getElementById('<%= ddltypeofvisit.ClientID %>').value;
            if (ddltypeofvisit == "--SELECT--") {
                alert('Please select Type Of Visit');
                document.getElementById('<%= ddltypeofvisit.ClientID %>').focus(); 
                return false;
            }

         

            var txtprsonmeet = document.getElementById('<%= txtprsonmeet.ClientID %>').value;
            if (txtprsonmeet.trim() == "" || txtprsonmeet.trim() == null) {
                alert('Please Enter Contact Person');
                document.getElementById('<%= txtprsonmeet.ClientID %>').focus();
                return false;
            }
                 var txtdesig = document.getElementById('<%= txtdesig.ClientID %>').value;
            if (txtdesig.trim() == "" || txtdesig.trim() == null) {
                alert('Please enter Designation');
                document.getElementById('<%= txtdesig.ClientID %>').focus();
                return false;
            }

             var txtemailid = document.getElementById('<%= txtemailid.ClientID %>').value;
            if (txtemailid.trim() == "" || txtemailid.trim() == null) {
                alert('Please enter emailid');
                document.getElementById('<%= txtemailid.ClientID %>').focus();
                return false;
            }


            
            var txtconnumber = document.getElementById('<%= txtconnumber.ClientID %>').value;
            if (txtconnumber.trim() == "" || txtconnumber.trim() == null) {
                alert('Please enter ContactNo');
                document.getElementById('<%= txtconnumber.ClientID %>').focus();
                return false;
            }  

             var ddlBU = document.getElementById('<%= ddlBU.ClientID %>').value;
        if (ddlBU.trim() == "" || ddlBU.trim() == null) {
            alert('Please select BU');
            return false;
        }
         var txtactiondone = document.getElementById('<%= txtactiondone.ClientID %>').value;
            if (txtactiondone.trim() == "" || txtactiondone.trim() == null) {
                alert('Please enter Discussion');
                document.getElementById('<%= txtactiondone.ClientID %>').focus();
                return false;
            }
           
           
            var txtfeedback = document.getElementById('<%= txtfeedback.ClientID %>').value;
            if (txtfeedback.trim() == "" || txtfeedback.trim() == null) {
                alert('Please enter FeedBack');
                document.getElementById('<%= txtfeedback.ClientID %>').focus();
                return false;
            }  
            var txtexpectedbuss = document.getElementById('<%= txtexpectedbuss.ClientID %>').value;
            if (txtexpectedbuss.trim() == "" || txtexpectedbuss.trim() == null) {
                alert('Please enter Expected Bussiness Amount');
                document.getElementById('<%= txtexpectedbuss.ClientID %>').focus();
                return false;
            }

           var ddlststusofcall = document.getElementById('<%= ddlststusofcall.ClientID %>').value;
            if (ddlststusofcall == "--SELECT--") {
                alert('Please select Call Status');
                return false;
            }
                            
             var ddlpriority = document.getElementById('<%= ddlpriority.ClientID %>').value;
            if (ddlpriority == "--SELECT--") {
                alert('Please select Priority');
                return false;
            }
           
        }

    </script>
     
    
   
    <asp:UpdatePanel ID="UPManualCLStatusChangeAlert" runat="server">
        <ContentTemplate>
            <table width="100%" align="center">
                <tr>
                    <td width="100%" align="center">
                        <lable id="lblformName" runat="server" style="color: #d71313; font-size: x-large;
                            font-weight: bold; font-family: Raleway;"> &nbsp;&nbsp;&nbsp;DAILY SALES REPORT </lable>
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
                        <asp:Label ID="lblMsg" runat="server" Style="padding: 5px 12px; margin: 4px 0; box-sizing: border-box;
                            font-family: Raleway; font-size: 14px; color: Red; font-weight: bold;" />
                        &nbsp;&nbsp;
                    </td>
                </tr>
               

            <tr>
            <td width="100%" align="center">
            <asp:Panel ID="pnlnewDeal" runat="server" Width="100%" Height="25%" BorderWidth="1px"
                BorderColor="Red" EnableTheming="true">
                <table width="95%" align="center" cellpadding="3px" cellspacing="3px">
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                    <tr>
                        <td width="10%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Country &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddlCountry" runat="server" class="form-control"   Width="285px"
                                Style="font-size: medium;" TabIndex="0"  
                              >
                            </asp:DropDownList>
                        </td>
                        <%--  onselectedindexchanged="ddlCountry_SelectedIndexChanged"--%><%--AutoPostBack="true"--%>
                        <td width="10%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Customer &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtCustomerName" runat="server" class="form-control" 
                                Width="285px" autocomplete="off" Style="font-size: medium;" 
                                placeHolder="Enter CustomerName" TabIndex="1"   onfocusout="ValidateCustomer()"
                                onkeyup="javascript:ResetResultValue();" 
                              ></asp:TextBox>
                            <%-- <asp:DropDownList ID="ddlcustomer" runat="server"   AutoPostBack="true"     
                                Width="165px" style="font-size:medium;" class="form-control" TabIndex="1" 
                             >
                            </asp:DropDownList>--%>
                            <%--        <asp:ListBox ID="ddlcustomer" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>
                            <%-- <input type="text" id="ddlcustomer" onkeyup="myFunction()">--%>
                        </td>
                        <td width="12%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Visit Date &nbsp;
                            </label>
                        </td>
                        <td width="22%">
                            <asp:TextBox ID="txtvisitDate" runat="server" Width="185px" class="form-control"
                                Style="font-size: medium;" TabIndex="2" placeholder="mm/dd/yyyy" autocomplete="off">
                            </asp:TextBox>
                            <cc1:CalendarExtender ID="txtvisitDate_CalendarExtender1" runat="server" Enabled="True"
                                TargetControlID="txtvisitDate" DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>
                     <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                    <tr>
                        <td width="10%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Type Of Visit &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddltypeofvisit" runat="server" class="form-control" Style="font-size: medium;"
                                TabIndex="3" Width="285px">
                             </asp:DropDownList>
                        </td>
                        <td width="10%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                New Partner &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                            <asp:DropDownList ID="ddlnewpartner" runat="server" Width="165px" 
                              class="form-control" Style="font-size: medium;" 
                               OnChange="javascript:newreseller();">
                           
                                <asp:ListItem Value="0">NO</asp:ListItem>
                                <asp:ListItem Value="1">YES</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td width="12%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px;  font-weight: normal;">
                                Next Reminder On &nbsp;
                            </label>
                        </td>
                        <td width="22%">
                            <asp:TextBox ID="txtreminderdate" runat="server" Width="185px" class="form-control"
                                Style="font-size: medium;"  placeholder="mm/dd/yyyy" autocomplete="off">
                            </asp:TextBox>
                            <cc1:CalendarExtender ID="Calendarextender1" runat="server" Enabled="True" TargetControlID="txtreminderdate"
                                DaysModeTitleFormat="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                        </td>
                    </tr>

                 <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                  
                    <tr>
                        <td width="10%">
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                               Contact Person &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="txtprsonmeet" runat="server"   Width="285px" class="form-control"
                                TabIndex="4" Style="font-size: medium;" autocomplete="off"
                                placeHolder=" Enter Person Name"  >
                            </asp:TextBox>
                        </td>
                        <td width="10%">
                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                BU Offered &nbsp;
                            </label>
                           
                        </td>
                        <td width="20%">
                        <asp:ListBox ID="ddlBU" runat="server" SelectionMode="Multiple" Width="165px"
                                class="form-control" TabIndex="8"></asp:ListBox>
                         
                        </td>
                        <td width="12%">
                                 <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Status Of Call &nbsp;
                            </label>
                        </td>
                        <td width="22%">
                                <asp:DropDownList ID="ddlststusofcall" runat="server" class="form-control" Width="185px"
                                TabIndex="12" Style="font-size: medium;">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>Funnel</asp:ListItem>
                                <asp:ListItem>Closed</asp:ListItem>
                                <asp:ListItem>Lead</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                    <tr>
                        <td width="10%">
                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Designation &nbsp;
                            </label>
                           
                        </td>
                        <td width="20%">
                        <asp:TextBox ID="txtdesig" runat="server"   Width="285px" class="form-control" Style="font-size: medium;"
                                TabIndex="5" autocomplete="off" placeHolder=" Enter Designation" MaxLength="50">
                            </asp:TextBox>
                            
                        </td>
                        <td width="10%">
                      
                            <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Expected Bussiness($) &nbsp;
                            </label>
                           
                        </td>
                        <td  >
                         
                          <asp:TextBox ID="txtexpectedbuss" runat="server" Width="165px" class="form-control"
                                TabIndex="9" Style="font-size: medium;" autocomplete="off" onkeypress="javascript:return isNumberKey(event);" placeHolder="Expected Bussiness">
                            </asp:TextBox>
                        </td>
                        <td width="12%">
                             <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Priority &nbsp;
                            </label>
                        </td>
                        <td width="22%">
                            <asp:DropDownList ID="ddlpriority" runat="server" class="form-control" Width="185px"
                                TabIndex="13" Style="font-size: medium;">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>High</asp:ListItem>
                                <asp:ListItem>Medium</asp:ListItem>
                                <asp:ListItem>Low</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                            &nbsp;
                            <asp:TextBox ID="txtCardCode" runat="server" Width="150px" class="form-control" TabIndex="12"
                                Style="font-size: medium; display: none;"></asp:TextBox>
                           
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="10%">
                            &nbsp;
                        </td>
                        <td width="20%">
                            &nbsp;
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                    <tr>
                        <td width="10%">
                              <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Email Id &nbsp;
                            </label>
                        </td>
                        <td width="20%">
                      
                            <asp:TextBox ID="txtemailid" runat="server"   Width="285px" class="form-control" Style="font-size: medium;"
                                TabIndex="6" autocomplete="off" placeHolder="Enter Email">
                            </asp:TextBox>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="Please Enter valid EmailID" ControlToValidate="txtemailid" Font-Bold="True" ForeColor=" #ff0000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td width="10%">
                        
                         <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Discussion &nbsp;
                            </label>
                        </td>
                        <td width="20%" colspan="3">
                         
                        <asp:TextBox ID="txtactiondone" runat="server" Width="710px" class="form-control"
                                TabIndex="10" Style="font-size: medium;" autocomplete="off" placeHolder=" Enter Discussion" MaxLength="3000">
                            </asp:TextBox>
                        </td>
                        
                        
                    </tr>
                  
                    
                    <tr>
                        <td width="10%" rowspan="2">
                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Contact Number &nbsp;
                            </label>
                            
                        </td>
                        <td width="20%">
                          <asp:TextBox ID="txtconnumber" runat="server"   Width="285px" class="form-control"
                                Style="font-size: medium;" autocomplete="off" onkeypress="javascript:return isNumberKey(event);"
                                TabIndex="7" placeHolder="Enter Contact No" MaxLength="10">
                          
                            <%-- <asp:DropDownList ID="ddlBU" runat="server" AutoPostBack="true" class="form-control"
                                Width="165px" Style="font-size: medium;">
                            </asp:DropDownList>--%>
                            </asp:TextBox>
                            <%-- <asp:ListBox ID="ddlBU" runat="server" SelectionMode="Multiple" Width="150px" class="form-control" TabIndex="1" >
            </asp:ListBox>--%>
                        </td>
                        <td width="10%">
                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                FeedBack/Action
                            </label>
                           
                        </td>
                        <td width="20%" colspan="3">
                        <asp:TextBox ID="txtfeedback" runat="server" Width="710px" class="form-control" Style="padding: 5px 12px;
                                margin: 3px 0; font-size: medium;" TabIndex="11" autocomplete="off" placeHolder="Enter FeedBack" MaxLength="1000">
                            </asp:TextBox>
                    
                        </td>
                        
                        
                    </tr> 
                    <tr>
                        <td width="100%" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="10%">
                        <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Forward Call To &nbsp;
                            </label>
                         </td>
                        <td width="20%" >
                       <asp:TextBox ID="txtforwardcallto" runat="server" Width="285px" class="form-control"
                                TabIndex="14" Style="font-size: medium;" autocomplete="off">
                            </asp:TextBox>
                  
                           
                         <%-- <asp:Button ID="btnsavereseller" runat="server" Text="Save Reseller"  class="btn btn-info"
                                Font-Bold="true" Font-Size="Medium"   style="font-family: Raleway;height:38px;width:120px;display:none;"  
                              OnClientClick="saveReseller();" />--%>


                        </td>
                        <td width="10%">
                              <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 14px; font-weight: normal;">
                                Forward Call remark &nbsp;
                            </label>
                        </td>
                        <td width="10%" colspan="3">
                             <asp:TextBox ID="txtforwardcallrmk" runat="server" Width="710px" class="form-control"
                                TabIndex="15" Style="font-size: medium;" autocomplete="off">
                            </asp:TextBox>
                        </td>
                        
                        
                    </tr>
                    <tr>
                        <td width="100%" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td width="10%" colspan="1">
                           <asp:TextBox ID="txtnewpartnername" runat="server" class="form-control" Style="font-size: medium;display:none;"
                                TabIndex="20" autocomplete="off"  placeHolder="Enter PartnerName" Width="210px" 
                                Font-Bold="true">
                            </asp:TextBox>

                        </td>
                        <td width="20%">
                         <input type="button" id="Button" value="Save Reseller" onclick="saveReseller();" class="btn btn-info" style="visibility:hidden;"  />
                        </td>
                        <td width="10%">
                            
                        </td>
                        <td width="20%">
                            
                        </td>
                        <td width="12%">
                            &nbsp;
                        </td>
                        <td width="22%">
                            &nbsp;
                        </td>
                        <%--<td width="22%">
        &nbsp;
        </td>--%>
                    </tr>
                   
               <%--    <table>
                    <tr>
                     <td>
                      <asp:Label ID="lblcreatedon" runat="server" Text=""></asp:Label>
                     </td>
                    </tr>
                   </table>--%>

                    <tr>
                    <td>
                    <table style="width:50%; background-color:#ffffcc;border:1px;">
                      <tr>
                        <td width="15%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                      </tr>
                      <tr class="info">   <td  colspan="3" align="center" width="25%">
                 <%--   <b> <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 15px; font-weight: normal;background:silver">
                             CURRENT MONTH SUMMARY
                            </label></b>--%>
                          CURRENT MONTH SUMMARY
                        
                        </td>
                       <td width="5%">
                            &nbsp;
                        </td>

                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                        <td width="5%">
                            &nbsp;
                        </td>
                      </tr>
                      <tr  align="center">
                       <td align="left"> <label style="padding: 5px 12px; margin: 3px 0;box-sizing: border-box; font-family: Raleway;
                                font-size: 15px; font-weight: normal;">
                               Visit Count&nbsp;
                            </label></td><td> <asp:Label ID="lblcountvisited" runat="server" Text=""></asp:Label></td>
                      
                      </tr>
                      <tr  align="center">
                      <td width="5%" align="left"><label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 15px; font-weight: normal;">
                              Total Expected Bussiness&nbsp;
                            </label></td><td><asp:Label ID="lblexpbuss"
                            runat="server" Text=""></asp:Label></td>
                      </tr>
                      <tr  align="center">
                      <td width="25%" align="center" colspan="3" rowspan="2">
                     
                       <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;color:Blue;
                                font-size: 15px; font-weight: normal;">
                              Last Visited &nbsp;
                            </label> <asp:Label ID="lbldiff" ForeColor="Red"
                            runat="server" Text="" Font-Bold="true">&nbsp;&nbsp;
                            </asp:Label> &nbsp; Days Ago.( &nbsp;
                                     <asp:Label   ID="lblcreatedon" ForeColor="Red"
                            runat="server" Text=""></asp:Label> &nbsp;)</td>
                        
                    <%-- <td width="2%">
                      <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 15px; font-weight: normal;">
                            Before &nbsp;
                            </label></td>--%><%--<td width="2%">--%><%--<asp:Label ID="lbldiff"
                            runat="server" Text=""></asp:Label></td> <td width="2%">
                      <label style="padding: 5px 12px; margin: 3px 0; box-sizing: border-box; font-family: Raleway;
                                font-size: 15px; font-weight: normal;">
                            Days Ago &nbsp;
                            </label></td>--%>
                      </tr>
         
                    </table>
                      </td>
                      
                         <td width="100%" colspan="6" align="center" >
                            <asp:Button ID="btnsaveasdraft" Text="Save As Draft" runat="server" class="btn btn-primary" 
                                Font-Bold="true" Font-Size="Medium" Style="font-family: Raleway; height: 38px;
                                width: 150px;" OnClick="btnsaveasdraft_Click1" TabIndex="18" OnClientClick="return ValidateDraft();"/>
                       
                       
                            <%--  <asp:TextBox ID="txtCustomerName" runat="server"  class="form-control" AutoPostBack="true" ></asp:TextBox>--%>
                            <asp:Label ID="lbldraft" runat="server" Text="" Visible="false"></asp:Label><asp:Label
                                ID="lblenablestatus" runat="server" Text="" Visible="false"></asp:Label>
                         &nbsp;&nbsp;
                       
                            <asp:Button ID="btnSave" Text="Save" runat="server" class="btn btn-success" Font-Bold="true"
                                Font-Size="Medium" Style="font-family: Raleway; height: 38px; width: 150px;"
                                OnClientClick="return Validate();" TabIndex="16" OnClick="btnSave_Click" />
                      
                       &nbsp;&nbsp;

                          <asp:Button ID="btndel" runat="server" class="btn btn-danger" Visible="false" Font-Bold="true"
                                Font-Size="Medium"  Style="font-family: Raleway; height: 38px; 
                                width: 150px;" Text="Delete" TabIndex="20" onclick="btndel_Click"   OnClientClick="return getConfirmation();"/>
   &nbsp;&nbsp;

                            <asp:Button ID="btncancel" runat="server" class="btn btn-danger" Font-Bold="true"
                                Font-Size="Medium" OnClick="btncancel_Click" Style="font-family: Raleway; height: 38px;
                                width: 150px;" Text="Cancel" TabIndex="17" />
                       &nbsp;&nbsp;
                      
                            <asp:Button ID="btngotolist" runat="server" class="btn btn-info" Font-Bold="true"
                                Font-Size="Medium" Style="font-family: Raleway; height: 38px; width: 170px;"
                                Text="Go To List" TabIndex="19" OnClick="btngotolist_Click" />
                     
                  
                      
                        </td>
                    </tr>
                 
                    <tr>
                        <td width="100%" colspan="6">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                  </asp:Panel>


                  </td>
                  </tr>
              <%--  </table>--%>
              

              <tr>
    <td width="100%" > &nbsp; </ td>
</tr>
   <%--OnPageIndexChanging="Gvdata_PageIndexChanging"--%>

                <tr>
                    <td width="95%">
                        <asp:GridView ID="Gvdata" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="3" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None"
                            BorderWidth="1px" CellSpacing="2"
                            PageSize="30" OnSelectedIndexChanged="Gvdata_SelectedIndexChanged" 
                            AllowPaging="True" OnRowDataBound="Gvdata_RowDataBound">
                            <Columns>
                               <asp:CommandField ShowSelectButton="True" SelectText="Edit" ControlStyle-ForeColor="Blue"
                                    ControlStyle-Font-Size="Small" ButtonType="Link" ItemStyle-Width="5%" ItemStyle-Font-Size="Medium"
                                    ItemStyle-Height="25px">
                                    <ControlStyle Font-Size="Small" ForeColor="Blue" />
                                    <ItemStyle Font-Size="Medium" Height="25px" Width="5%" />
                                </asp:CommandField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsrno" runat="server" Text='<%#Container.DataItemIndex + 1 %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblsaleid" runat="server" Text='<%#Eval("VisitId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldraft" runat="server" Text='<%#Eval("IsDraft")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="IsDraft">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("STATUS")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcountry" runat="server" Text='<%#Eval("Country") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcompany" runat="server" Text='<%#Eval("Company") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visit Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisitdate" runat="server" Text='<%#Eval("VisitDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Of Visit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblviittype" runat="server" Text='<%#Eval("VisitType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcomname" runat="server" Text='<%#Eval("Company") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpersonmeet" runat="server" Text='<%#Eval("PersonMet") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblemail" runat="server" Text='<%#Eval("Email") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("Designation") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcontact" runat="server" Text='<%#Eval("ContactNo") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblbu" runat="server" Text='<%#Eval("BU") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblecallstatus" runat="server" Text='<%#Eval("CallStatus") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblfeedback" runat="server" Text='<%#Eval("Feedback") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblforwardcall" runat="server" Text='<%#Eval("ForwardCallToEmail") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblfwdrmk" runat="server" Text='<%#Eval("ForwardRemark") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Priority">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("Priority") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ActionDone">
                                    <ItemTemplate>
                                        <asp:Label ID="lblactiondone" runat="server" Text='<%#Eval("Discussion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--     <asp:TemplateField HeaderText="NextAction">
                                <ItemTemplate>
                                    <asp:Label ID="lblnextaction" runat="server" Text='<%#Eval("NextAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblexpebussiness" runat="server" Text='<%#Eval("ExpectedBusinessAmt") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblnewpartner" runat="server" Text='<%#Eval("IsNewPartner") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NextReminderDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblnextremin" runat="server" Text='<%#Eval("NextReminderDate","{0:d}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                              <%--  <asp:TemplateField ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkdelete" runat="server" CommandArgument='<%#Eval("VisitId")%>'
                                            Text="Delete" CommandName="Delete" ForeColor="Red" CausesValidation="false"  OnClientClick="return getConfirmation();"
                                            Height="25px" Font-Size="small"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Center" />
                                    <HeaderStyle Font-Size="Smaller" HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                    </td>
                </tr>
      </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
