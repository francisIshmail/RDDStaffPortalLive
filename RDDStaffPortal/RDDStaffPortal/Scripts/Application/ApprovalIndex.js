var ApprovalIndex = {
    initialize: function (e) {
        debugger
        //$.fn.dataTable.ext.errorMode = 'none';

        ApprovalIndex.Attachevent();
    },
    Attachevent: function () {

        var tblhead1 = ['Template_Id', 'ObjType', 'DocumentName', 'Description', 'Status', 'no_of_approvals', 'Condition', 'Condition_Text', 'CreatedOn', 'CreatedBy', 'LastUpdatedOn', 'LastUpdatedBy'];
        var tblhide = [];
        var tblhead2 = [];
        var dateCond = ['CreatedOn', 'LastUpdatedOn'];

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
        RedDot_Button_Init_HideShow();
        Getdata();
        //#region Load Data
        function Getdata() {
            var value1 = $("#Search-Forms").val().toLowerCase();
            $('.loader1').show();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETAPPROVAL", data, dateCond, tblhead1, tblhide, tblhead2);
        }
        //#endregion
        //#region Next Button*/
        $('.next').bind('click', function (e) {
            e.preventDefault();
            $(".loader1").show();
            if (arr.data[0].TotalCount < 50) {
                $(".loader1").hide();
                return;
            }
            curPage++;
            var value1 = $("#Search-Forms").val().toLowerCase();
           
            if (curPage > arr.data[0].TotalCount)
                curPage = 0;
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETAPPROVAL", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region Prev Button*/
        $('.prev').bind('click', function (e) {
            e.preventDefault();
            $(".loader1").show();
            var value1 = $("#Search-Forms").val().toLowerCase();
            if (arr.data[0].TotalCount < 50) {
                $(".loader1").hide();
                return;
            }
            curPage--;
            
            if (curPage < 0)
                curPage = (arr.data[0].TotalCount - 1);
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETAPPROVAL", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region Apply Button*/
        $("#btnApply").on("click", function (e) {
            e.preventDefault();
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
            var value1 = $("#Search-Forms").val().toLowerCase();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETAPPROVAL", data, dateCond, tblhead1, tblhide, tblhead2);
            $('.close').trigger("click");
        })
        //#endregion
        //#region Search Textbox*/
        $("#Search-Forms").on("keyup", function (e) {
            e.preventDefault();
            $(".loader1").show();
            var value1 = $(this).val().toLowerCase();
            var data = JSON.stringify({
                pagesize: 50,
                pageno: curPage,
                psearch: value1,
            });
            arr = RedDot_DivTable_Fill("I", "/GETAPPROVAL", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region New PV*/
        $("#btnAdd").on("click", function (e) {
            
                e.preventDefault();
                $.post("/ADDAPPROVAL", { TEMPId: -1 }, function (response) {

                    $("#idCard").html(response);
                    RedDot_Button_New_HideShow();
                    
                })

           
           
        })
        //#endregion
        //#region Cancel PV*/
        $("#btnCancel").on("click", function (e) {
            e.preventDefault();
            RedDot_Button_Init_HideShow();
            $("#idCard").html("");
        })
        //#endregion
        //#region Edit PV*/
        $("#Ibody").on('dblclick', "#Ist", function (e) {
           

                e.preventDefault();
                var TEMPId = $(this).closest("Ist").prevObject.find(".Abcd").eq(0).text();
              
                $.post("/ADDAPPROVAL", { TEMPId: TEMPId }, function (response) {

                    $("#idCard").html(response);

                    RedDot_Button_Edit_HideShow();

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


                    
                })
           
           
        });
        //$("#Ibody").on('click', "#Ist", function (e) {

        //    $("#ApprovalDecisionPopup").modal("show");

        //})
        //#endregion
        //#region Save PV*/
        $("#btnSave").on("click", function (e) {
            e.preventDefault();
            var t = false;
            if ($("input[name='optionsRadios']:checked").attr("id") == "IIndCondition") {
                t = true;
            }
            var RDD_Approval = {
                Template_Id: $("#Template_Id").val(),
                ObjType: $("#ObjType").val(),
                DocumentName: $("#ObjType option:selected").text(),
                Description: $("#Description").val(),
                Status: $("#TempStatus").is(":checked"),
                no_of_approvals: $("#no_of_approvals").val(),
                Condition: t,
                Condition_Text: $("#Condition_Text").val(),               
                EditFlag: $("#EditFlag").val(),
                CreatedBy: $("#CreatedBy").val(),
                CreatedOn: $("#CreatedOn").val(),
                RDD_Approval_ApproversList: [],
                RDD_Approval_OriginatorsList:[]

            };
            $(".ApproverDet").each(function () {                          
                var IsApproval_Mandatory = $(this).find(".colorinput-input").is(":checked"); 
                var Approval_Sequence = $(this).find("[name^='SeqSrno']").val(); 
                var Approver = $(this).find("[name^='hdnApprover']").val();               
                if (Approver != '' && Approver!='-1') {
                    var RDD_Approval_Approvers = {
                        Approval_Sequence: Approval_Sequence,
                        IsApproval_Mandatory: IsApproval_Mandatory,
                        Approver: Approver                       
                    };
                    RDD_Approval.RDD_Approval_ApproversList.push(RDD_Approval_Approvers);
                }
            });
            $(".OriginatorDet").each(function () {
                var OriginatorName = $(this).find("[name^='Originator']").val();
                var Originator = $(this).find("[name^='hdnOriginator']").val();
                if (Originator != '' && Originator != '-1') {
                    var RDD_Approval_Originators = {
                        OriginatorName: OriginatorName,
                        Originator: Originator
                    };
                    RDD_Approval.RDD_Approval_OriginatorsList.push(RDD_Approval_Originators);
                }

            });


            var ValidateFormCheck = ValidateForm(RDD_Approval);
            if (ValidateFormCheck.formValid == false) {
                RedDotAlert_Error(ValidateFormCheck.ErrorMessage);
                return;
            }
            $.post("/SAVEAPPROVAL", { RDD_Approval: RDD_Approval }, function (response) {

                if (response[0].Outtf == true) {
                    RedDotAlert_Success(response[0].Responsemsg);
                    RedDot_Button_Init_HideShow();
                    Getdata();
                    $("#idCard").html("");
                } else {
                    RedDotAlert_Error(response[0].Responsemsg);
                }

            })


        })
        //#endregion
        //#region Validation PV*/
        function ValidateForm(RDD_Approval) {
            var response = {
                ErrorMessage: "",
                formValid: false
            };
            if (RDD_Approval.EditFlag == "False") {
                if (RDD_Approval.DocumentName == "0" || RDD_Approval.DocumentName == "-1") {
                    response.ErrorMessage += "DocumentName,";
                }
            } else if (RDD_Approval.EditFlag == "True") {
                if (RDD_Approval.DocumentName == "0" || RDD_Approval.DocumentName == "-1") {
                    response.ErrorMessage += "DocumentName,";
                }
            }

            if (RDD_Approval.RDD_Approval_OriginatorsList.length == 0) {
                response.ErrorMessage += "Plase Enter Originator Data";
            }
            if (RDD_Approval.RDD_Approval_ApproversList.length == 0) {
                response.ErrorMessage += "Plase Enter Approve Data";
            }
            if (RDD_Approval.no_of_approvals > RDD_Approval.RDD_Approval_ApproversList.length) {
                response.ErrorMessage += "Plase Enter Approve Data";
            }

            if (RDD_Approval.Condition_Text.indexOf('truncat') >= 0) {
                response.ErrorMessage += "Plase Do not Write Truncat";
                
            }
            if (RDD_Approval.Condition_Text.indexOf('delete') >= 0) {
                response.ErrorMessage += "Plase Do not Write delete";

            }
            if (RDD_Approval.Condition_Text.indexOf('insert') >= 0) {
                response.ErrorMessage += "Plase Do not Write insert";

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