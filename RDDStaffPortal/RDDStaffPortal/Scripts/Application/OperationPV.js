var OperationPV = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';

		OperationPV.Attachevent();
	},
	Attachevent: function () {
        tblDetails = ['LineRefNo', 'Date1', 'Description', 'Amount', 'Remarks', 'FilePathInput', 'hdnFilePathInput'];
        //#region  Add Row
        /*Remark Change Event Add Row*/
        $(document).on("change", "#IIst input[id^='Remarks']", function () {
            RedDot_tbldtpicker();
            $(".datepicker").datetimepicker("destroy");
            var tr = $("#IIst").clone(true);
            var tr2 = $(this).closest("#IIst");
            var count2 = parseInt($("#hdncount").val())
            var m = tr2.find("input[id^='LineRefNo']").val();
            if (m < count2) {
                return;
            }
            var count1 = parseInt(count2) + 1;
            RedDot_Table_Attribute(tr, tblDetails, count1, ".PVLines");
            tr.find(".fa-eye").removeAttr("href");
            tr.find("#lblDocname").text('Document Name');
            tr.find("input[id ^= 'FilePathInput']").attr("title", "No file chosen...");
            $("#IIbody").append(tr);
        })
        //#endregion
        $('body').on('focusin', '.datepicker', function () {
            $('.datepicker').datetimepicker({              
                format: 'DD/MM/YYYY',
                showClose: true,
                showClear: true,

            });

        });
        $('body').on('focusin', '.datepickerH', function () {
            $('.datepickerH').datetimepicker({               
                format: 'DD/MM/YYYY',
                showClose: true,
                showClear: true,

            });
        });  
        //#region EditMode
        if ($("#EditFlag").val() == "True") {
            /*Edit Mode show Date (DD-MM-yyyy)*/
            RedDot_dateEdit("#PayRequestDate1[type='text']", "#PayRequestDate[type='hidden']");
            RedDot_dateEdit("#PayDate1[type='text']", "#PayDate[type='hidden']");
            var k1 = 1;
            /*Edit Mode Table show Date(DD-MM-yyyy) & file path*/
            $(".PVLines").each(function () {
                if ($(".PVLines").length != k1) {
                    $(this).find("[name^='Date1']").val(RedDot_DateTblEdit($(this).find("[name^='hdnDate1']").val()));
                    var k = $(this).find("[name^='hdnFilePathInput']").val().split("/").length;
                    $(this).find(".fa-eye").attr("href", $(this).find("input[id ^= 'hdnFilePathInput']").val());
                    $(this).find(".fa-eye").attr("target", "_blank");
                    $(this).find("#lblDocname").text($(this).find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
                    $(this).find("input[id ^= 'FilePathInput']").attr("title", $(this).find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
                }
                k1++;
            });
            $("#FilePathview").attr("href", $("input[id='FilePath'][type='hidden']").val());
        } else {
            /*New show Date Current date (DD-MM-yyyy)*/
            RedDot_NewDate(".datepicker,.datepickerH");
        }
        //#endregion
        //#region focus new row column
        RedDot_tableLstEnt("#IIst", "input[id^='Remarks']", "input[id ^= 'Date1']", "Please Enter Remark", "T", "");
        //#endregion
        //#region Tab Event
        /*Tab Event then focus new Column */
        RedDot_tableTabEve("#IIst", "input[id^='Date1']", "input[id^='Description']", "", "T", "");
        RedDot_tableTabEve("#IIst", "input[id^='Description']", "input[id^='Amount']", "Please Enter Description", "T", "");
        RedDot_tableTabEve("#IIst", "input[id^='Amount']", "input[id^='Remarks']", "Please Enter Amount", "N", "");
        RedDot_tableTabEve("#IIst", "input[id^='Remarks']", "input[id ^= 'rowchooseFile']", "Please Enter Remark", "T", "");
      //#endregion    
        //#region SearchBox
        $('#Country').select2({

            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select--'
        });
        $('#DBName').select2({
            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select--',

        });
        $('#Currency').select2({
            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select--',

        });
        $('#PayMethod').select2({
            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select --',

        });
        $('#VType').select2({
            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select --',

        });
        $('#DocStatus').select2({
            theme: "bootstrap",
            allowClear: true,
            placeholder: '--Select --',

        });
        //#endregion
        //#region File Upload Header
        /*Header File Uplaod*/
        $("#FilePath").on("change", function () {
            var data = new FormData();
            var files = $("input[id = 'FilePath']").get(0).files;
            if (files.length > 0) {
                data.append("Files", files[0]);
                data.append("type", "Header");
            }
            $.ajax({
                url: "/SAP/RDD_PV/UploadDoc",
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {
                    if (response == "InvalidError") {
                        // RdotAlerterrtxt("Invalid Image Formate Supported Formate .jpeg/.jpg/.png /.bmp");
                    }
                    else {
                        $("input[id='FilePath'][type='hidden']").val(response);
                        $("#FilePathview").attr("href", $("input[id='FilePath'][type='hidden']").val());
                        $("#FilePathview").attr("target", "_blank");
                    }
                },
                error: function (er) {
                    RedDotAlert_Error(er);
                }
            });
        });
        //#endregion
        //#region PVLines File uplaod
        /* Details PVLines File uplaod*/
        $(document).on("change", "#IIst input[id^='FilePathInput']", function (event) {            
            var tr = $(this).closest("#IIst");
            var data = new FormData();
            var files = tr.find("input[id ^= 'FilePathInput']").get(0).files;
            if (files.length > 0) {
                data.append("Files", files[0]);
                data.append("type", "Details");
            }
            $.ajax({
                url: "/SAP/RDD_PV/UploadDoc",
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {                    
                    if (response == "InvalidError") {
                        // RdotAlerterrtxt("Invalid Image Formate Supported Formate .jpeg/.jpg/.png /.bmp");
                    }
                    else {                       
                        tr.find("input[id^='hdnFilePathInput']").val(response);
                        tr.find(".fa-eye").attr("href", tr.find("input[id ^= 'hdnFilePathInput']").val());
                        tr.find(".fa-eye").attr("target", "_blank");
                        var k = tr.find("input[id ^= 'hdnFilePathInput']").val().split("/").length;
                        tr.find("#lblDocname").text(tr.find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
                        tr.find("input[id ^= 'FilePathInput']").attr("title", tr.find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
                    }
                },
                error: function (er) {
                    RedDotAlert_Error(er);
                }
            });
        });
         //#endregion
        //#region Remove PVLines
        /*Details PVLines Remove Records*/
        $(document).on("click", "#IIst button[id^='btntblDel']", function () {            
            var count2 = parseInt($("#hdncount").val())
            var tr = $(this).closest("#IIst");
            var m = tr.find("input[id^='LineRefNo']").val();
            if (count2 == 1 || m == count2) {
                return
            }
            RedDot_Table_DeleteActivity(tr, tblDetails, ".PVLines");
        })
        //#endregion
	}
}