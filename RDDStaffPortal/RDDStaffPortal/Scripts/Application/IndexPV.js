var IndexPV = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
        //$("#btnAction").hide();
		IndexPV.Attachevent();
	},
    Attachevent: function () {

        $("#FilterBtn").click(function () {
            $("#FilterSection").slideToggle("slow");
        });

        $('body').on('focusin', '.datepicker', function () {

            if ($('.datepicker').data('DateTimePicker') != undefined) {
                $('.datepicker').datetimepicker('destroy');
            }
            $('.datepicker').datetimepicker({

                format: 'DD/MM/YYYY',
                showClose: true,
                showClear: true,

            });
        });
        
        var tblhead1 = ['PVId', 'Country', 'RefNo', 'ApprovalStatus', 'VType', 'VendorEmployee', 'Currency', 'RequestedAmt', 'ApprovedAmt', 'Benificiary', 'BeingPayOf',  'CreatedOn']
        //var tblhead1 = ['PVId', 'Country', 'RefNo', 'DocStatus', 'VType', 'DBName', 'Currency', 'VendorCode', 'VendorEmployee', 'Benificiary', 'RequestedAmt', 'ApprovedAmt', 'BeingPayOf',
          //  'PayRequestDate', 'BankCode', 'BankName', 'PayMethod', 'PayRefNo', 'PayDate', 'FilePath', 'ClosedDate', 'CAappStatus', 'CAappRemarks', 'CAapprovedBy', 'CAapprovedOn',
            //'CMappStatus', 'CMappRemarks', 'CMapprovedBy', 'CMapprovedOn', 'CFOappStatus', 'CFOappRemarks', 'CFOapprovedBy', 'CFOapprovedOn'];
        var tblhide = ['PVId'];
        var tblhead2 = ['RequestedAmt','ApprovedAmt'];
        var dateCond = ['PayRequestDate', 'PayDate', 'ClosedDate',  'CreatedOn'];

        $('.loader1').show();
        var k1 = 0;
        var arr = [];
        var curPage = 1;
        //#region checkbox hide/show
        while (tblhead1.length > k1) {
            if (jQuery.inArray(tblhead1[k1], tblhide) !== -1) {
                $('div#' + tblhead1[k1] + '1').addClass("Abc");
                $("#idchk").append("<div class='col-md-3 mar-b10'><div class='panel'><div class='row'><div class='col-sm-10 col-md-10'><div class='widget-content padd-0'><span class='widget-name'> " + tblhead1[k1] + "</span></div></div><div class='col-sm-2 col-md-2 ml-auto'><label class='colorinput mar-t6 mar-b0'><input id='" + tblhead1[k1] + "' type='checkbox' class='colorinput-input'><span class='colorinput-color bg-secondary'></span><div> </label></div></div></div></div></div>");
            } else {
                if (jQuery.inArray(tblhead1[k1], tblhead2) == -1) {
                    $('div#' + tblhead1[k1] + '1').removeClass("Abc");
                    $("#idchk").append("<div class='col-md-3 mar-b10'><div class='panel'><div class='row'><div class='col-sm-10 col-md-10'><div class='widget-content padd-0'><span class='widget-name'> " + tblhead1[k1] + "</span></div></div><div class='col-sm-2 col-md-2 ml-auto'><label class='colorinput mar-t6 mar-b0'><input id='" + tblhead1[k1] + "' type='checkbox' class='colorinput-input' checked><span class='colorinput-color bg-secondary'></span><div> </label></div></div></div></div></div>");
                }
            }
            k1++;
        }
        //#endregion
        $('.loader1').hide();
        $("#btnAction").hide();
        RedDot_Button_Init_HideShow();
        var me = getUrlVars()["PVId"];

        if (me != undefined) {
            debugger
            $("#btnAction").show();
            viewmode(me);
           
        } else {
            Getdata();
        }
        
        $("#btnAdvApply").click(function () {
            Getdata();
        });
        //#region Load Data
        function Getdata() {
            debugger
            var value1 = $("#txtPV").val().toLowerCase();
            var SearchCon = "";
            if ($("#txtFrmDate").val() != '' && $("#txtToDate").val() != '')
                SearchCon = SearchCon + " And CreatedOn BetWeen $" + GetSqlDateformat($("[id$=txtFrmDate]").val()) + "$ And $" + GetSqlDateformat($("[id$=txtToDate]").val()) + "$"

            if ($("#AdvCountry").val() != "0" && $("#AdvCountry").val() != "-1")
                SearchCon = SearchCon + " And Country=$" + $("#AdvCountry").val() + "$";

            if ($("#AdvCurrency").val() != "0" && $("#AdvCurrency").val() != "-1")
                SearchCon = SearchCon + " And Currency=$" + $("#AdvCurrency").val() + "$";

            if ($("#AdvPayMeth").val() != "0" && $("#AdvPayMeth").val() != "-1")
                SearchCon = SearchCon + " And PayMethod=$" + $("#AdvPayMeth").val() + "$";
            if ($("#AdvDBName").val() != "0" && $("#AdvDBName").val() != "-1")
                SearchCon = SearchCon + " And DBName=$" + $("#AdvDBName").val() + "$";

            if (SearchCon != "") {
                SearchCon =" "+ SearchCon + ""
            }

            $('.loader1').show();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
                SearchCon: SearchCon
            });
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
        }
        //#endregion
        //#region Next Button*/
        $('.next').bind('click', function () {
            $(".loader1").show();            
            if (arr.data.length > 0) {
                if (arr.data[0].TotalCount < 50) {
                    $(".loader1").hide();
                    return;
                }                
            } else {
                $(".loader1").hide();
                RedDotAlert_Error('No Record Found');
               return
            }
            curPage++;
            var value1 = $("#txtPV").val().toLowerCase();
            if (curPage > arr.data[0].TotalCount)
                curPage = 0;
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region Prev Button*/
        $('.prev').bind('click', function () {
            $(".loader1").show();
            var value1 = $("#txtPV").val().toLowerCase();
            if (arr.data.length > 0) {
                if (arr.data[0].TotalCount < 50) {
                    $(".loader1").hide();
                    return;
                }
                curPage--;
                if (curPage < 0)
                    curPage = (arr.data[0].TotalCount - 1);
            } else {
                curPage--;
            }
            
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region Apply Button*/
        $("#btnApply").on("click", function () {
            $(".loader1").show();
            tblhide = [];
            $('input:checkbox').each(function () {
                var IsActiveTxt = $(this).is(":checked");
                if (IsActiveTxt == false && $(this).attr("id") != undefined) {

                    tblhide.push($(this).attr("id"));
                    $('div#' + $(this).attr("id") + '1').addClass("Abc");
                } else {
                    $('div#' + $(this).attr("id") + '1').removeClass("Abc");
                }
                $("select").val('').trigger('change');
            });
            var value1 = $("#txtPV").val().toLowerCase();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
            $('.close').trigger("click");
        })
        //#endregion
       //#region Search Textbox*/
        $("#txtPV").on("keyup", function () {
            $(".loader1").show();
            var value1 = $(this).val().toLowerCase();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        $("#btnDelete").on("click", function () {
            const swalWithBootstrapButtons = Swal.mixin({
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger',
                buttonsStyling: false,
            })
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    $.getJSON("/DeleteRDDPV", { PVId: $("#PVId").val() }).done(function (data) {
                        if (data.data[0].Outtf == false) {
                            RedDotAlert_Error(data.data[0].Responsemsg);
                            return

                        }
                        if (data.data[0].Outtf == true) {
                            RedDot_Button_Init_HideShow();
                            Getdata();
                            $("#idCard").html("");
                            $(".required-label").text("");
                        }
                    });
                    swalWithBootstrapButtons.fire(
                        'Deleted!',
                        'Your Data has been deleted.',
                        'success'
                    )

                } else if (
                    // Read more about handling dismissals
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Your Data is safe :)',
                        'error'
                    )
                }
            })
        })
        //#region New PV*/
        $("#btnAdd").on("click", function () {
            $("#FilterSection").hide();
            $.post("/ADDRDDPV", { PVId: -1 }, function (response) {

                $("#idCard").html(response);
                $('#Country').val($('#Country option:eq(1)').val()).trigger('change');
                $('#DocStatus').val($('#DocStatus option:eq(1)').val()).trigger('change');
                
                if ($("#VType").val() == "Vendor") {

                    $("#lblemp").text('Vendor :');
                } else {
                    $("#lblemp").text('Employee :');
                }
                $(".required-label").text("");
                $("#Div1-Approval").hide();
                $("#Div1-ApprovedBy").hide();
                
                RedDot_Button_New_HideShow();
                $("#btnSave").show();
                $("#btnDelete").hide();
                $("#btnPrint").hide();
            })
        })

        //#endregion
        $("#btnPrint").on("click", function () {

            window.location.href = "/SAP/RDD_PV/DownloadPdf?p_pvid=" + $("#PVId").val()+"";  
            

        })
       //#region Cancel PV*/
        $("#btnCancel").on("click", function () {
            var me = getUrlVars()["PVId"];

            if (me != undefined) {
                location.href = "/Admin/ApprovalStatusReport/Index";
                return;
            }
            RedDot_Button_Init_HideShow();
            $("#btnAction").hide();
            $("#idCard").html("");
            $(".required-label").text("");
        })
        //#endregion
        $("#btnSendMail").on("click", function () {
            debugger
            _DocKey = $("#PVId").val();
            var _ObjType = $("#Doc_Object").val();
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/PV_SendMailToSignatories",
                data: JSON.stringify({ ObjectType: _ObjType, PVID: _DocKey }),
                dataType: 'Json',
                contentType: "Application/json",
                success: function (value) {
                    
                    var t = value;
                    if (t == false) {
                        RdotAlerterrtxt('Already Mail sent');
                    } else {
                        RdotAlertSucesstxt('Mail Sent Successfully');
                    }
                    
                }
            });
        })
        $(document).on("click","#btnActionSave",function () {
            debugger;
            var k = 1;
            var _LoginUser = $('#LastUpdatedBy').val();
            $(".ApproverAction").each(function () {
                var _ID = $(this).find(".Abcd [id^='txtID']").val();
                var _Template_ID = $(this).find(".Abcd [id^='txtTemplate_ID']").val();
                var _ObjectType = $(this).find(".Abcd [id^='txtObjType']").val();

                var _Approver = $(this).find(".Abcd [id^='txtApprover']").val();
                var _Action = $(this).find(".Abcd [id^='cbAction']").val();
                var _Remark = $(this).find(".Abcd [id^='txtApprRemark']").val();
                var _ApprovalDate = GetSqlDateformat($(this).find(".Abcd [id^='txtApprovedDate']").val());

                if (_LoginUser.toUpperCase() == _Approver.toUpperCase()) {

                    $.ajax({
                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/Get_Doc_ApproverAction",
                        data: JSON.stringify({ ID: _ID, Template_ID: _Template_ID, ObjectType: _ObjectType, DocKey: _DocKey, Approver: _Approver, Action: _Action, Remark: _Remark, ApprovalDate: _ApprovalDate }),
                        dataType: 'Json',
                        contentType: "Application/json",
                        success: function (value) {
                            debugger;
                            var jData = value.Table1;
                            if (jData.length > 0) {
                                if (jData[0].Result == 'True') {
                                    if (value.Table.length > 0) {
                                        $(".required-label").text("[ " + value.Table[0].ApprovalStatus + " ]");
                                        $("#ApprovalStatus").val(value.Table[0].ApprovalStatus);
                                    }
                                       
                                    $('#ApprovalDecisionPopup').modal('hide');
                                    RedDotAlert_Success("Save Succesfully");
                                } else {
                                    RedDotAlert_Error("Error Occur");
                                }
                                
                            }
                           
                            
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                }
            });
        });
        var SqlDate;
        function GetSqlDateformat(obj) {
            try {
                if (obj != undefined && obj != null) {
                    SqlDate = obj.toString().split('/')[1] + '/';
                    SqlDate += obj.toString().split('/')[0] + '/';
                    SqlDate += obj.toString().split('/')[2];
                    return SqlDate;
                }
            }
            catch (ex) {
                log(ex);
            }
        }
        
        $("#btnAction").on("click", function () {
            debugger
            _DocKey = $("#PVId").val();
            var _ObjType = $("#Doc_Object").val();
            var tr1 = $("#IIIIst").clone(true);
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/Get_Doc_ApproverList",
                data: JSON.stringify({ ObjectType: _ObjType, DocKey: _DocKey, LoginUser: $('#LastUpdatedBy').val() }),
                dataType: 'Json',
                contentType: "Application/json",
                success: function (value) {
                    debugger;
                    $("div#IIIIst").not(':first').remove();
                    //var tr1 = $("#IIIst").clone(true); 
                    var jData = value.Table;
                    var i1 = 0;
                    while (jData.length > i1) {
                        // $("#IIIBody").append(tr1[0]);
                        $("#IIIIst").clone().insertAfter("#IIIIBody");

                        i1++;
                    }
                    $("#IIIIst")[0].remove();
                    var Approvername = $("input[id='txtApprover']").val(function () {
                        return this.value.toUpperCase();
                    })
                      //$("div#IIIst:first").remove(); 
                    // Getgrid(tr, jData.length);                   
                    var count1 = 1;
                    var actiontblValue = ['ID', 'TEMPLATE_ID', 'OBJTYPE', 'APPROVER', 'APPROVAL_DECISION', 'APPROVAL_Remark', 'APPROVAL_DATE'];
                    var doc = $("#DocStatus").val();
                    $("#btnActionSave").show();
                    //if ($('#LastUpdatedBy').val().toLowerCase() == $('#CreatedBy').val().toLowerCase() && $("#VType").val() !='VType') {
                    //    $("#btnActionSave").hide();
                    //}
                    //else
                        if (doc == "Paid - Closed" || doc == "Rejected-Closed") {
                        $("#btnActionSave").hide();
                        $("#ApprovalDecisionPopup").find("#exampleModalLabel").text("Approval Decision   [ You can not take action on Rejected-Closed/Paid-Closed vourcher ]");
                    } else {
                        $("#ApprovalDecisionPopup").find("#exampleModalLabel").text("Approval Decision");
                    }

                    
                    $(".ApproverAction").each(function () {
                        
                        
                        if (jData[count1 - 1].Flag == 'Disable') {//|| $("#EditFlag").val() == "True"
                            if (jData[count1 - 1][actiontblValue[3]].toLowerCase() == $('#LastUpdatedBy').val().toLowerCase()) {
                                $("#btnActionSave").hide();
                            }
                            $(this).find('.Abcd input[id^="txtID"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtTemplate_ID"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtObjType"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprover"]').prop('disabled', true);
                            $(this).find('.Abcd [id^="cbAction"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprRemark"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprovedDate"]').prop('disabled', true);

                        }
                        else {
                           
                            $(this).find('.Abcd input[id^="txtID"]').removeAttr('disabled');
                            $(this).find('.Abcd input[id^="txtTemplate_ID"]').removeAttr('disabled');
                            $(this).find('.Abcd input[id^="txtObjType"]').removeAttr('disabled');
                            $(this).find('.Abcd input[id^="txtApprover"]').removeAttr('disabled');
                            $(this).find('.Abcd [id^="cbAction"]').removeAttr('disabled');
                            $(this).find('.Abcd input[id^="txtApprRemark"]').removeAttr('disabled');
                            $(this).find('.Abcd input[id^="txtApprovedDate"]').removeAttr('disabled');
                        }
                        $(this).find('.Abcd input[id^="txtID"]').attr("id", 'txtID' + count1);
                        $(this).find('.Abcd input[id^="txtID"]').attr("name", 'txtID' + count1);
                        $(this).find('.Abcd input[id^="txtTemplate_ID"]').attr("id", 'txtTemplate_ID' + count1);
                        $(this).find('.Abcd input[id^="txtTemplate_ID"]').attr("name", 'txtTemplate_ID' + count1);
                        $(this).find('.Abcd input[id^="txtObjType"]').attr("id", 'txtObjType' + count1);
                        $(this).find('.Abcd input[id^="txtObjType"]').attr("name", 'txtObjType' + count1);
                        $(this).find('.Abcd input[id^="txtApprover"]').attr("id", 'txtApprover' + count1);
                        $(this).find('.Abcd input[id^="txtApprover"]').attr("name", 'txtApprover' + count1);
                        $(this).find('.Abcd input[id^="cbAction"]').attr("id", 'cbAction' + count1);
                        $(this).find('.Abcd input[id^="cbAction"]').attr("name", 'cbAction' + count1);
                        $(this).find('.Abcd input[id^="txtApprRemark"]').attr("id", 'txtApprRemark' + count1);
                        $(this).find('.Abcd input[id^="txtApprRemark"]').attr("name", 'txtApprRemark' + count1);
                        $(this).find('.Abcd input[id^="txtApprovedDate"]').attr("id", 'txtApprovedDate' + count1);
                        $(this).find('.Abcd input[id^="txtApprovedDate"]').attr("name", 'txtApprovedDate' + count1);

                        $(this).find('.Abcd input[id^="txtID"]').val(jData[count1 - 1][actiontblValue[0]]);
                        $(this).find('.Abcd input[id^="txtTemplate_ID"]').val(jData[count1 - 1][actiontblValue[1]]);
                        $(this).find('.Abcd input[id^="txtObjType"]').val(jData[count1 - 1][actiontblValue[2]]);
                        $(this).find('.Abcd input[id^="txtApprover"]').val(jData[count1 - 1][actiontblValue[3]]);
                        $(this).find('.Abcd [id^="cbAction"]').val(jData[count1 - 1][actiontblValue[4]]);
                        $(this).find('.Abcd input[id^="txtApprRemark"]').val(jData[count1 - 1][actiontblValue[5]]);
                        $(this).find('.Abcd input[id^="txtApprovedDate"]').val(RedDot_dateEditFormat(jData[count1 - 1][actiontblValue[6]]));
                        count1++;

                    });
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });

            $("#ApprovalDecisionPopup").modal("show");
        })
      
        function viewmode(PVId) {
            $.post("/VIEWRDDPV", { PVId: PVId }, function (response) {
                debugger
                $("#idCard").html(response);
                RedDot_Button_New_HideShow();
                $("#RequestedAmt").val($("#RequestedAmt").val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                $("#ApprovedAmt").val($("#ApprovedAmt").val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                $(".txtcheck").each(function (index) {
                    if ($("#" + $(this).attr("id") + "").val() !== '') {
                        $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');
                    }
                });
                $(".dropcheck").each(function (index) {
                    if ($("#" + $(this).attr("id") + "").val() !== '0') {
                        $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');
                    }
                });
                if ($("#IsDraft").val() == "True") {
                    $(".required-label").text("[ Draft ]");
                    $("#btnSave").show();
                    $("#btnDelete").show();
                    $("#Div1-Approval").hide();
                    $("#Div1-ApprovedBy").hide();
                } else {
                    $("#btnSave").hide();
                    $("#btnDelete").hide();
                    $(".required-label").text("[ " + $("#ApprovalStatus").val() + " ]");
                    $("#Div1-Approval").show();
                    $("#Div1-ApprovedBy").show();
                    if ($("#ApprovalStatus").val() == "Approved") {
                        $("#btnPrint").show();
                        $("#btnSendMail").show();
                    }
                }

                $("#btnSave").hide();
                $("#btnDelete").hide();
                $("#btnCancel").text("Back");

                VendorAging($("#DBName").val(), $("#VendorCode").val());
                RedDot_Notification_Status_change(me, $("#ApprovalStatus").val(), 'PV');
               
            })
        }


        function VendorAging(DBName, BP) {
            debugger
            var V_AGE = ["0-30", "31-45", "46-60", "61-90", "91-120", "121+", "Balance"];
            if ($("#VType").val() == "Vendor" && DBName !="" && BP!="") {
                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "/GetVendorAgeing",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        DBName: DBName,
                        BP: BP
                    }),
                    success: function (data) {
                        var arr3 = data;
                        $("#txtbal").val(arr3.Table[0][V_AGE[6]] == null ? 0 : arr3.Table[0][V_AGE[6]]);
                        $("#txt0").val(arr3.Table[0][V_AGE[0]] == null ? 0 : arr3.Table[0][V_AGE[0]])
                        $("#txt31").val(arr3.Table[0][V_AGE[1]] == null ? 0 : arr3.Table[0][V_AGE[1]])
                        $("#txt46").val(arr3.Table[0][V_AGE[2]] == null ? 0 : arr3.Table[0][V_AGE[2]])
                        $("#txt61").val(arr3.Table[0][V_AGE[3]] == null ? 0 : arr3.Table[0][V_AGE[3]])
                        $("#txt91").val(arr3.Table[0][V_AGE[4]] == null ? 0 : arr3.Table[0][V_AGE[4]])
                        $("#txt121").val(arr3.Table[0][V_AGE[5]] == null ? 0 : arr3.Table[0][V_AGE[5]])



                    }
                });
            }
        }
        //#region Edit PV*/
        $("#Ibody").on('dblclick', "#Ist", function (event) {
            $("#FilterSection").hide();
            var PVId = $(this).closest("Ist").prevObject.find(".Abcd").eq(0).text();
            $.post("/ADDRDDPV", { PVId: PVId }, function (response) {
                
                $("#idCard").html(response);
                $("#RequestedAmt").val($("#RequestedAmt").val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                $("#ApprovedAmt").val($("#ApprovedAmt").val().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                RedDot_Button_Edit_HideShow();
                debugger
                $(".txtcheck").each(function (index) {
                    if ($("#" + $(this).attr("id") + "").val() !== '') {
                        $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');
                    }
                });
                $(".dropcheck").each(function (index) {
                    if ($("#" + $(this).attr("id") + "").val() !== '0') {
                        $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');
                    }
                });
                if ($("#IsDraft").val() == "True") {
                    $(".required-label").text("[ Draft ]");
                    $("#btnSave").show();
                    $("#btnDelete").show();
                    $("#Div1-Approval").hide();
                    $("#Div1-ApprovedBy").hide();
                } else if ($("#ApprovalStatus").val() == "Rejected-Open") {
                    $(".required-label").text("[ " + $("#ApprovalStatus").val() + " ]");
                    $("#btnAction").show();
                    $("#btnSave").show();
                    $("#btnDelete").hide();
                    $("#Div1-Approval").show();
                    $("#Div1-ApprovedBy").show();
                }
                else {
                    if ($("#ApprovalStatus").val() == "Approved") {
                        $("#btnPrint").show();
                    }
                    $("#btnAction").show();
                    $("#btnSave").hide();
                    $("#btnDelete").hide();
                    $(".required-label").text("[ "+$("#ApprovalStatus").val()+" ]");
                    $("#Div1-Approval").show();
                    $("#Div1-ApprovedBy").show();
                }
                VendorAging($("select[id^=DBName]").val(), $("#VendorCode").val());
                //if ($("#Currency").val() !== '0') {
                //    $("#div-Currency").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#VendorEmployee").val() !== '') {
                //    $("#div-VendorEmployee").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#Benificiary").val() !== '') {
                //    $("#div-Benificiary").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#RequestedAmt").val() !== '') {
                //    $("#div-RequestedAmt").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#ApprovedAmt").val() !== '') {
                //    $("#div-ApprovedAmt").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#PayRefNo").val() !== '') {
                //    $("#div-PayRefNo").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#BeingPayOf").val() !== '') {
                //    $("#div-BeingPayOf").removeClass('has-error1').addClass('has-success1');
                //}
                //if ($("#PayMethod").val() !== '0') {
                //    $("#div-PayMethod").removeClass('has-error1').addClass('has-success1');
                //}

                //if ($("#DBName").val() !== '0') {
                //    $("#div-DBName").removeClass('has-error1').addClass('has-success1');
                //}
            })
        });
        //#endregion
        //#region Save PV*/
        var arr4 = "";
        $("#btnSave").on("click", function () {
            debugger
            if ($("#IsDraft").val() == "False" && $("#ApprovalStatus").val()!== "Rejected-Open") {
                RedDotAlert_Error("Can not Modify");
                return;
            }
            $("#RefNo").removeAttr('disabled');
            var RDDPV = {
                PVId: $("#PVId").val(),
                Country: $("#Country").val(),
                RefNo: $("#RefNo").val(),
                DocStatus: $("#DocStatus").val(),
                VType: $("#VType").val(),
                DBName: $("#DBName").val(),
                Currency: $("#Currency").val(),
                VendorCode: $("#VendorCode[type='hidden']").val(),
                VendorEmployee: $("#VendorEmployee[type='text']").val(),
                Benificiary:$("#Benificiary[type='text']").val(),
                RequestedAmt: parseFloat($("#RequestedAmt").val()),
                ApprovedAmt: parseFloat($("#ApprovedAmt[type='number']").val()),
                BeingPayOf: $("#BeingPayOf[type='text']").val(),
                PayRequestDate: RedDot_setdtpkdate($("#PayRequestDate1").val()),
                BankCode: $("#BankCode[type='hidden']").val(),
                BankName: $("#BankName[type='text']").val(),
                PayMethod: $("#PayMethod").val(),
                PayRefNo: $("#PayRefNo[type='text']").val(),
                PayDate: RedDot_setdtpkdate($("#PayDate1").val()),
                FilePath: $("#FilePath[type='hidden']").val(),
                ClosedDate: RedDot_setdtpkdate($("#ClosedDate1").val()),
                CAappStatus: $("#CAappStatus[type='text']").val(),
                CAappRemarks: $("#CAappRemarks[type='text']").val(),
                CAapprovedBy: $("#CAapprovedBy[type='text']").val(),
                CAapprovedOn: RedDot_setdtpkdate($("#CAapprovedOn1").val()),
                CMappStatus: $("#CMappStatus[type='text']").val(),
                CMappRemarks: $("#CMappRemarks[type='text']").val(),
                CMapprovedBy: $("#CMapprovedBy[type='text']").val(),
                CMapprovedOn: RedDot_setdtpkdate($("#CMapprovedOn1").val()),
                CFOappStatus: $("#CFOappStatus[type='text']").val(),
                CFOappRemarks: $("#CFOappRemarks[type='text']").val(),
                CFOapprovedBy: $("#CFOapprovedBy[type='text']").val(),
                CFOapprovedOn: RedDot_setdtpkdate($("#CFOapprovedOn1").val()),
                EditFlag: $("#EditFlag").val(),
                RDD_PVLinesDetails: [],

            };
            $("#RefNo").attr('disabled',true);
            $(".PVLines").each(function () {
                var Date = RedDot_setdtpkdate($(this).find("[name^='Date1']").val());
                var Description = $(this).find("[name^='Description']").val();
                var Amount = $(this).find("[name^='Amount']").val();
                var Remarks = $(this).find("[name^='Remarks']").val();
                var FilePath = $(this).find("[name^='hdnFilePathInput']").val();
                var LineRefNo = $(this).find("[name^='LineRefNo']").val();
                if (Description != '' && parseInt(Amount) != 0 && Remarks != '') {
                    var RDD_PVLines = {
                        Date: Date,
                        Description: Description,
                        Amount: Amount,
                        Remarks: Remarks,
                        FilePath: FilePath,
                        LineRefNo: LineRefNo
                    };
                    RDDPV.RDD_PVLinesDetails.push(RDD_PVLines);
                }

            });
           
            var ValidateFormCheck = ValidateForm(RDDPV);
            if (ValidateFormCheck.formValid == false) {
                RedDotAlert_Error(ValidateFormCheck.ErrorMessage);
                return;
            }
            var data = JSON.stringify({ RDDPV: RDDPV });
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/SAVERDDPV",
                data: data,
                dataType: 'Json',
                contentType: "Application/json",

                success: function (response) {
                    //  $.post("/SAVERDDPV", data, function (response) {
                    debugger
                    arr4= response;
                    if (arr4[0].Outtf == true) {
                        var data = JSON.stringify({
                            Object_Type: '18',
                            Originator: $("#CreatedBy[type='hidden']").val(),
                            DocKey: arr4[0].Id,

                        });
                        var CheckApproval = Red_Dot_Model_Popup("#Divid", "ApprovalModal", data);

                        if (CheckApproval == true) {
                            var a = ConfirmYesNo("ApprovalModal");
                           // $('.modal-backdrop').remove();
                            a.then(function (b) {
                                //debugger
                                if (b == 1) {
                                    $.ajax({
                                        async: false,
                                        cache: false,
                                        type: "POST",
                                        data: JSON.stringify({ Object_Type: '18', Originator: $("#CreatedBy[type='hidden']").val(), DocKey: response[0].Id, OriginatorRemark: $("[id$=MRemark]").val() }),
                                        url: "/RDD_Approver_Insert_Records",
                                        dataType: 'Json',
                                        contentType: "Application/json",

                                        success: function (response1) {
                                            debugger
                                            if (response1.Table.length != 0) {

                                                if (response1.Table[0].Result == 'True') {
                                                    $(".required-label").text("");
                                                    $('.modal-backdrop').remove();
                                                    $("#idCard").html("");
                                                    $("#txtFrmDate").val('');
                                                    $("#txtToDate").val('');
                                                    Getdata();
                                                    RedDot_Button_Init_HideShow();
                                                    RedDotAlert_Success(arr4[0].Responsemsg + " Trans ID-" + arr4[0].Id + ' And Document send for Approval');
                                                    $(".modal-backdrop fade").removeClass("show");
                                                    $("body").css("padding-right", "");
                                                    
                                                }

                                            }

                                        }
                                    });

                                }
                                else {
                                    $(".required-label").text("");
                                    $('.modal-backdrop').remove();
                                    $("#idCard").html("");
                                    $("#txtFrmDate").val('');
                                    $("#txtToDate").val('');
                                    Getdata();
                                    $("#btnAction").hide();
                                    RedDot_Button_Init_HideShow();
                                    RedDotAlert_Success(arr4[0].Responsemsg + ' As Draft. ' + " Trans ID-" + arr4[0].Id);
                                    $(".modal-backdrop fade").removeClass("show");
                                    $("body").css("padding-right", "");
                                }
                            })
                        }
                        else {
                            $(".required-label").text("");
                          //  RedDotAlert_Success(response[0].Responsemsg);
                            $('.modal-backdrop').remove();
                            $("#idCard").html("");
                            $("#txtFrmDate").val('');
                            $("#txtToDate").val('');
                            Getdata();
                            RedDot_Button_Init_HideShow();
                            $("#btnAction").hide();
                            RedDotAlert_Success(arr4[0].Responsemsg + " Trans ID-" + arr4[0].Id);
                            $(".modal-backdrop fade").removeClass("show");
                            $("body").css("padding-right", "");
                           
                        }
                        
                    } else {
                        $(".required-label").text("");
                        $('.modal-backdrop').remove();
                        RedDotAlert_Error(arr4[0].Responsemsg);
                        $(".modal-backdrop fade").removeClass("show");
                        $("body").css("padding-right", "");
                    }

                }//)
            })



        })

        function ConfirmYesNo(ModelId) {
            debugger
           var dfd = jQuery.Deferred();
            var $confirm = $('#' + ModelId + '');
            $confirm.modal('show');

            $('#btnApproval').off('click').click(function () {

                $confirm.modal('hide');
                dfd.resolve(1);
                return 1;
            });
            $('#btnAppCancel').off('click').click(function () {
                $confirm.modal('hide');
               dfd.resolve(0);
                return 0;
            });
            return dfd.promise();
        }
        //#endregion
        //#region Validation PV*/
        function ValidateForm(RDD_PV) {
            var response = {
                ErrorMessage: "",
                formValid: false
            };
            if (RDD_PV.EditFlag == "False") {
                if (RDD_PV.Country == "0" || RDD_PV.Country == "-1") {
                    response.ErrorMessage += "Country code,";
                }
            } else if (RDD_PV.EditFlag == "True") {
                if (RDD_PV.Country == "0" || RDD_PV.Country == "-1") {
                    response.ErrorMessage += "Country code,";
                }
            }

            if (RDD_PV.DBName == "0" || RDD_PV.DBName == "-1") {
                response.ErrorMessage += "DBName ,";
            }

            if (RDD_PV.RefNo == "" ) {
                response.ErrorMessage += "Ref No ,";
            }

            if (RDD_PV.DBName == "Vendor") {
                if (RDD_PV.VendorCode == "") {
                    response.ErrorMessage += "Vendor Code  ,";
                }
                
                
            }

            if (RDD_PV.VendorEmployee == "") {
                response.ErrorMessage += "Name  ,";
            }
            if (RDD_PV.Benificiary == "") {
                response.ErrorMessage += "Benificiary  ,";
            }

            if (RDD_PV.DocStatus == "0" || RDD_PV.DocStatus == "-1") {
                response.ErrorMessage += "Doc Status ,";
            }
            if (RDD_PV.Currency == "0" || RDD_PV.Currency == "-1") {
                response.ErrorMessage += "Currency ,";
            }

            if (RDD_PV.RequestedAmt == "0" || RDD_PV.RequestedAmt == "") {
                response.ErrorMessage += "Request Amount  ,";
            }

            if (RDD_PV.PayMethod == "0" || RDD_PV.PayMethod == "-1") {
                response.ErrorMessage += "Request Amount  ,";
            }
            if ((RDD_PV.PayMethod == "Cheque" || RDD_PV.PayMethod == "TT") && (RDD_PV.BankName == "" || RDD_PV.BankCode=="")) {
                response.ErrorMessage += "Bank Name  ,";
            }
            if (RDD_PV.FilePath == "" && RDD_PV.PayMethod == "Cheque") {
                response.ErrorMessage += "if Pay Method is cheque then cheque image is mandatory ,";
            }
            if (RDD_PV.PayRefNo == "") {
                response.ErrorMessage += "Pay Ref No  ,";
            }
            if (RDD_PV.ApprovedAmt == "0" || RDD_PV.PayMethod == "") {
                response.ErrorMessage += "Approve Amount  ,";
            }

            if (RDD_PV.RDD_PVLinesDetails.length == 0) {
                response.ErrorMessage += "Plase Enter Details Data";
            }


            if (response.ErrorMessage.length == 0) {
                response.formValid = true;
            }
            else {
                response.ErrorMessage = "Enter Mandatory Fields " + response.ErrorMessage + "."
            }

            return response;
        }
        //#endregion
	}
}