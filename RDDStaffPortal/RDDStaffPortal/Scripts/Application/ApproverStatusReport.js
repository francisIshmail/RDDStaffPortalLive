var ApproverStatusReport = function () { };

var tblhead1 = ['SRNO', 'OBJTYPE', 'DocumentName', 'DOC_ID', 'DOC_DATE', 'CARDNAME', 'DocTotal', 'ORIGINATOR', 'ORG_Remark', 'APPROVER', 'APPROVAL_DECISION', 'APPROVAL_DATE'];
var tblhide = [];
var tblhead2 = [];
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
       
        $("select").not("#cbAction").select2({
            theme: "bootstrap",
            allowClear: true,

        });
        //$(".datepicker").datetimepicker({

        //    format: 'DD/MM/YYYY'
        //});

        //RedDot_NewDate(".datepicker");
       

        $('.loader1').hide();
        Get_DocumentApprove_List();
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

        $(document).on("click", "#IIst button[id^='btnAction']", function (e) {
            debugger;
            var tr = $(this).closest("#IIst");
           
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
                    var actiontblValue = ['ID','TEMPLATE_ID', 'OBJTYPE', 'APPROVER', 'APPROVAL_DECISION', 'APPROVAL_Remark', 'APPROVAL_DATE'];                 
                    $(".ApproverAction").each(function () {  
                        if (jData[count1 - 1].Flag == 'Disable') {

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

function Get_DocumentApprove_List() {
    debugger;
    //$("[id$=pgHeader]").html('<h4 class="page-title">Sales Order List</h4>');
    var value1 = '';//$("#Search-Forms").val().toLowerCase();
    if ($("input[name='ApprovalStatus']:checked").attr('id') == 'SalesOrder')
        value1 = 'T0.OBJTYPE=17';

    if ($("#txtFrmDate").val() != '' && $("#txtToDate").val() != '')
        value1 = value1 + " And T1.PostingDate BetWeen $" + GetSqlDateformat($("[id$=txtFrmDate]").val()) + "$ And $" + GetSqlDateformat($("[id$=txtToDate]").val())+"$"

    if ($("#cbOriginator").val() != '')
        value1 = value1 + " And ORIGINATOR=$" + $("#cbOriginator").val() + "$";

    if ($("#txtApprover").val() != '')
        value1 = value1 + " And APPROVER=$" + $("#txtApprover").val()+"$";

    if ($("#cbStatus").val() != '' && $("#cbStatus").val() !='-- Select --')
        value1 = value1 + " And APPROVAL_DECISION=$" + $("#cbStatus").val()+"$";

    //GetSqlDateformat($("[id$=txtChkDate2]").val());, ,APPROVAL_DECISION
    var DBName = 'All';
    var UserName = $("#txtApprover").val();

    $('.loader1').show();
    var data = JSON.stringify({
        pagesize: 50,
        pageno: curPage,
        psearch: value1,
        DBName: DBName,
        UserName: UserName
    });
    arr = RedDot_DivTable_Fill("II", "/Get_ApprovalDoc_List", data, dateCond, tblhead1, tblhide, tblhead2);
    debugger;
    var k = 1;
    $(".approvallist").each(function () {
      
        $(this).find('.Abcd1 button[id^="btnAction1"]').attr("id", 'btnAction1' + k);
        
        
        k++;
    });


}