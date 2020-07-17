var IndexPV = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';

		IndexPV.Attachevent();
	},
	Attachevent: function () {

        var tblhead1 = ['PVId', 'Country', 'RefNo', 'DocStatus', 'VType', 'DBName', 'Currency', 'VendorCode', 'VendorEmployee', 'Benificiary', 'RequestedAmt', 'ApprovedAmt', 'BeingPayOf',
            'PayRequestDate', 'BankCode', 'BankName', 'PayMethod', 'PayRefNo', 'PayDate', 'FilePath', 'ClosedDate', 'CAappStatus', 'CAappRemarks', 'CAapprovedBy', 'CAapprovedOn',
            'CMappStatus', 'CMappRemarks', 'CMapprovedBy', 'CMapprovedOn', 'CFOappStatus', 'CFOappRemarks', 'CFOapprovedBy', 'CFOapprovedOn'];
        var tblhide = [];
        var tblhead2 = [];
        var dateCond = ['PayRequestDate', 'PayDate', 'ClosedDate', 'CAapprovedOn', 'CMapprovedOn', 'CFOapprovedOn'];

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
            arr = RedDot_DivTable_Fill("I", "/GETRDDPV", data, dateCond, tblhead1, tblhide, tblhead2);
        }
        //#endregion
        //#region Next Button*/
        $('.next').bind('click', function () {
            $(".loader1").show();
            curPage++;
            var value1 = $("#Search-Forms").val().toLowerCase();
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
            var value1 = $("#Search-Forms").val().toLowerCase();
            curPage--;
            if (curPage < 0)
                curPage = (arr.data[0].TotalCount - 1);
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
            var value1 = $("#Search-Forms").val().toLowerCase();
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
        $("#Search-Forms").on("keyup", function () {
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
        //#region New PV*/
        $("#btnAdd").on("click", function () {
            $.post("/ADDRDDPV", { PVId: -1 }, function (response) {
                
                $("#idCard").html(response);
                RedDot_Button_New_HideShow();
            })
        })
        //#endregion
       //#region Cancel PV*/
        $("#btnCancel").on("click", function () {
            RedDot_Button_Init_HideShow();
            $("#idCard").html("");
        })
        //#endregion
        //#region Edit PV*/
        $("#Ibody").on('dblclick', "#Ist", function (event) {
            var PVId = $(this).closest("Ist").prevObject.find(".Abcd").eq(0).text();
            $.post("/ADDRDDPV", { PVId: PVId }, function (response) {
                
                $("#idCard").html(response);
                RedDot_Button_New_HideShow();
            })
        });
        //#endregion
        //#region Save PV*/
        $("#btnSave").on("click", function () {            
            var RDDPV = {
                PVId: $("#PVId").val(),
                Country: $("#Country").val(),
                RefNo: $("#RefNo").val(),
                DocStatus: $("#DocStatus").val(),
                VType: $("#VType").val(),
                DBName: $("#DBName").val(),
                Currency: $("#Currency").val(),
                VendorCode: $("#VendorCode[type='text']").val(),
                VendorEmployee: $("#VendorEmployee[type='text']").val(),
                Benificiary: $("#Benificiary[type='text']").val(),
                RequestedAmt: $("#RequestedAmt[type='number']").val(),
                ApprovedAmt: $("#ApprovedAmt[type='number']").val(),
                BeingPayOf: $("#BeingPayOf[type='text']").val(),
                PayRequestDate: RedDot_setdtpkdate($("#PayRequestDate1").val()),
                BankCode: $("#BankCode[type='text']").val(),
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
            $.post("/SAVERDDPV", { RDDPV: RDDPV }, function (response) {
                
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