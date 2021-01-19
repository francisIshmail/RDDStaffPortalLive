var ApproverStatusReport = function () { };

var tblhead1 = ['SRNO', 'OBJTYPE', 'DocumentName', 'DOC_ID', 'Country', 'Refno', 'DOC_DATE', 'CARDNAME','Currency', 'DocTotal', 'ORIGINATOR', 'ORG_Remark', 'APPROVER', 'APPROVAL_DECISION', 'APPROVAL_DATE'];
var tblhide = [];
var tblhead2 = ['DocTotal'];
var dateCond = ['DOC_DATE', 'APPROVAL_DATE'];
var arr = [];
var curPage = 1;
var _DocKey = 0;

ApproverStatusReport.prototype = {

    Init: function () {
        debugger;
        ApproverStatusReport.prototype.ControlInit();
        ApproverStatusReport.prototype.ClickEvent();

    },

    ControlInit: function () {

       
        debugger;
        var arr3 =""
        //$("select").not("#cbAction").select2({
        //    theme: "bootstrap",
        //    allowClear: true,

        //});
        //$(".datepicker").datetimepicker({

        //    format: 'DD/MM/YYYY'
        //});

        //RedDot_NewDate(".datepicker");
       

        $('.loader1').hide();
        GetFillRadioButton();
      // Get_DocumentApprove_List();
        $('#IIst').hide();
    },

    ClickEvent: function () {

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

        $("#btnApply").click(function () {
            Get_DocumentApprove_List();
        });

        $("#FilterBtn").click(function () {
            $("#FilterSection").slideToggle("slow");
        });

        $("#btnActionCancel").click(function () {
            $('#ApprovalDecisionPopup').modal('hide');
        });

        $("#btnActionSave").click(function () {
            debugger;
            var k = 1;
            var _LoginUser = $('#txtApprover').val();
            $(".ApproverAction").each(function () {
                var _ID = $(this).find(".Abcd [id^='txtID']").val();
                var _Template_ID = $(this).find(".Abcd [id^='txtTemplate_ID']").val();
                var _ObjectType = $(this).find(".Abcd [id^='txtObjType']").val();
                
                var _Approver = $(this).find(".Abcd [id^='txtApprover']").val();
                var _Action = $(this).find(".Abcd [id^='cbAction']").val();
                var _Remark = $(this).find(".Abcd [id^='txtApprRemark']").val();
                var _ApprovalDate =GetSqlDateformat( $(this).find(".Abcd [id^='txtApprovedDate']").val());

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
                            var jData = value.Table;

                            $('#ApprovalDecisionPopup').modal('hide');
                            Get_DocumentApprove_List();
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

        function Getgrid(tr, len) {
            var i1 = 0
            while (len > i1) {
                $("#IIIBody").append(tr);
                i1++
            }
           $("#IIIst")[0].remove();

        }

        $(document).on("click", "#IIst button[id^='btnView']", function (e) {
            debugger;
            var tr = $(this).closest("#IIst");
            var Pvid = tr.find(".Abcd").eq(3).text();
            //var _ObjType = tr.find(".Abcd").eq(1).text();
           
            location.href = $("input[name='ApprovalStatus']:checked").labels(0).attr("alt")+""+Pvid;
           
            
           
        })

        $(document).on("click", "#IIst button[id^='btnAction']", function (e) {
            debugger;
            var tr = $(this).closest("#IIst");
            var doc = tr.find(".Abcd").eq(10).text().trim();
            _DocKey = tr.find(".Abcd").eq(3).text();
            var _ObjType = tr.find(".Abcd").eq(1).text();
           
            var tr1 = $("#IIIst").clone(true);
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/Get_Doc_ApproverList",
                data: JSON.stringify({ ObjectType: _ObjType, DocKey: _DocKey,LoginUser:$('#txtApprover').val()}),
                dataType: 'Json',
                contentType: "Application/json",
                success: function (value) {  
                    debugger;
                    $("div#IIIst").not(':first').remove(); 
                    //var tr1 = $("#IIIst").clone(true); 
                    var jData = value.Table;
                    var i1 = 0;
                    while (jData.length > i1) {
                       // $("#IIIBody").append(tr1[0]);
                        $("#IIIst").clone().insertAfter("#IIIBody");

                        i1++;
                    }
                    $("#IIIst")[0].remove();
                  var Approvername=   $("input[id='txtApprover']").val(function () {
                        return this.value.toUpperCase();
                    })
                 //   $("div#IIIst:first").remove(); 
                   // Getgrid(tr, jData.length);                   
                    var count1 = 1;                    
                    var actiontblValue = ['ID', 'TEMPLATE_ID', 'OBJTYPE', 'APPROVER', 'APPROVAL_DECISION', 'APPROVAL_Remark', 'APPROVAL_DATE'];                 


                    
                    if (doc == "Paid - Closed" || doc == "Rejected-Closed") {
                        $("#btnActionSave").hide();
                        $("#ApprovalDecisionPopup").find("#exampleModalLabel").text("Approval Decision   [ You can not take action on Rejected-Closed/Paid-Closed vourcher ]");
                    } else {
                        $("#ApprovalDecisionPopup").find("#exampleModalLabel").text("Approval Decision");
                    }

                    $(".ApproverAction").each(function () {  
                        if (jData[count1 - 1].Flag == 'Disable') {
                            if ($('#txtApprover').val().toLowerCase() == jData[count1 - 1][actiontblValue[3]].toLowerCase()) {
                               $("#btnActionSave").prop('disabled', true);
                            }
                           // 
                            $(this).find('.Abcd input[id^="txtID"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtTemplate_ID"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtObjType"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprover"]').prop('disabled', true);
                            $(this).find('.Abcd [id^="cbAction"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprRemark"]').prop('disabled', true);
                            $(this).find('.Abcd input[id^="txtApprovedDate"]').prop('disabled', true);

                        }
                        else {
                            if ($('#txtApprover').val().toLowerCase() == jData[count1 - 1][actiontblValue[3]].toLowerCase()) {
                                $("#btnActionSave").removeAttr('disabled');
                            }
                           // 
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
                        $(this).find('.Abcd input[id^="txtObjType"]').val(jData[count1-1][actiontblValue[2]]);                       
                        $(this).find('.Abcd input[id^="txtApprover"]').val(jData[count1-1][actiontblValue[3]]);                       
                        $(this).find('.Abcd [id^="cbAction"]').val(jData[count1-1][actiontblValue[4]]);                       
                        $(this).find('.Abcd input[id^="txtApprRemark"]').val(jData[count1-1][actiontblValue[5]]);                       
                        $(this).find('.Abcd input[id^="txtApprovedDate"]').val(RedDot_dateEditFormat(jData[count1-1][actiontblValue[6]]));                      
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
            
           

        });
        
    },


}



$(document).ready(function () {

    var ApproverStatusReport_obj = new ApproverStatusReport();
    ApproverStatusReport_obj.Init();

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

function RedDot_dateEditFormat(dtval) {
    if (dtval != null || dtval == '') {
        var now = new Date(dtval);
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = (day) + "/" + (month) + "/" + now.getFullYear();
        return today;
    }

}

$(document).on('change', 'input[type=radio][name=ApprovalStatus]', function () {
    debugger
    FillOriginator();

});

function FillOriginator() {
    $('#IIst').hide();
    $('div#IIst').not(':first').remove();
    $("#cbOriginator").empty();
   // $('#ORIGINATOR').append('<option value="0" selected="">-Select-</option>');
    debugger
    var i1 = 0;
    var k = $("input[name='ApprovalStatus']:checked").attr('id');
    var found_names = $.grep(arr3.Table1, function (v) {
        return v.ObjType === k;
    });

    while (i1 < found_names.length) {
        //,
        $('#cbOriginator').append('<option value=' + found_names[i1]['Originator'] + ' selected="">' + found_names[i1]['OriginatorName']+'</option>');
       i1 ++;
    }
    $('#cbOriginator').val('All')
    var found_names1 = $.grep(arr3.Table2, function (v) {
        return v.Action === k;
    });    
        
    RedDot_DivTable_Header_Fill("II", found_names1);



}
function Get_DocumentApprove_List() {
    debugger;
    //$("[id$=pgHeader]").html('<h4 class="page-title">Sales Order List</h4>');
    var value1 = 'T0.OBJTYPE='+$("input[name='ApprovalStatus']:checked").attr('id')+'';//$("#Search-Forms").val().toLowerCase();
    //if ($("input[name='ApprovalStatus']:checked").attr('id') == 'SalesOrder')
    //    value1 = 'T0.OBJTYPE=17';
    //if ($("input[name='ApprovalStatus']:checked").attr('id') == 'PaymentVoucher')
    //    value1 = 'T0.OBJTYPE=18';

    if ($("#txtFrmDate").val() != '' && $("#txtToDate").val() != '')
        value1 = value1 + " And T1." + $("input[name='ApprovalStatus']:checked").attr("alt")+" BetWeen $" + GetSqlDateformat($("[id$=txtFrmDate]").val()) + "$ And $" + GetSqlDateformat($("[id$=txtToDate]").val())+"$"

    if ($("#cbOriginator").val() != '' && $("#cbOriginator").val()!='All')
        value1 = value1 + " And ORIGINATOR=$" + $("#cbOriginator").val() + "$";

    if ($("#txtApprover").val() != '')
        value1 = value1 + " And APPROVER=$" + $("#txtApprover").val()+"$";

    if ($("#cbStatus").val() != '' && $("#cbStatus").val() !='-- Select --')
        value1 = value1 + " And APPROVAL_DECISION=$" + $("#cbStatus").val() + "$";
    var Objtype = $("input[name='ApprovalStatus']:checked").attr('id');

    //GetSqlDateformat($("[id$=txtChkDate2]").val());, ,APPROVAL_DECISION
    var DBName = 'All';
    var UserName = $("#txtApprover").val();

    $('.loader1').show();
    var data = JSON.stringify({
        pagesize: 50,
        pageno: curPage,
        psearch: value1,
        DBName: DBName,
        UserName: UserName,
        Objtype: Objtype,
    });
    arr = RedDot_DivTable_Fill("II", "/Get_ApprovalDoc_List", data, dateCond, tblhead1, tblhide, tblhead2);
    debugger;
    var k = 1;
    $(".approvallist").each(function () {
      
        $(this).find('.Abcd1 button[id^="btnAction1"]').attr("id", 'btnAction1' + k);
        
        
        k++;
    });


}


function GetFillRadioButton() {
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: "/GetApprovalFill",
        contentType: "application/json",
        dataType: "json",
       
        success: function (data) {
             arr3 = data;
            var i = 0;
            var rdbtn = "";
            while (i < arr3.Table.length) {
                debugger
                var chk = '';
                if (i == 0) {
                    chk = 'checked';
                } 
                rdbtn = rdbtn+ "<div class='col-xl-6 col-md-6 col-6'>" +
                    "<div class='form-check form-check-inline align-items'>" +
                    "<div class='custom-control custom-radio'>" +
                    " <input type='radio' alt='" + arr3.Table[i]['DocDateColumn'] +"' id='" + arr3.Table[i]['ObjType'] +"' name='ApprovalStatus' class='custom-control-input' "+chk+">" +
                    " <label class='custom-control-label' alt='" + arr3.Table[i]['RedirectURL'] +"' for='" + arr3.Table[i]['ObjType'] +"'>" + arr3.Table[i]['DocumentName'] +"</label>" +
                    "</div>" +
                    "</div>" +
                    "</div>";
               
                i++;
            }
            $("#rdbtn").html(rdbtn);
           

            FillOriginator();
        }
    });
}