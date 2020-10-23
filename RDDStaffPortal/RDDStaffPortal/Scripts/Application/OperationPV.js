var OperationPV = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';

		OperationPV.Attachevent();
	},
	Attachevent: function () {
        tblDetails = ['LineRefNo', 'Date1', 'Description', 'Amount', 'Remarks', 'FilePathInput', 'hdnFilePathInput'];
        //#region  Add Row
        var t = false;
        $(document).on("change", "#IIst input[id^='Amount']", function () {
            debugger
            Calculation();
            var sum1 = parseFloat($("#ApprovedAmt[type='number']").val());
            if (parseFloat($("#RequestedAmt").val()) < sum1) {
                $("#ApprovedAmt[type='number']").val($("#ApprovedAmt[type='number']").val() - $(this).val());
                $(this).val(0);               
               RedDotAlert_Error("Total amount of all rows can not be greater than requested amount.");

                return;
            }
           
           
        })
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
            RedDot_Table_Attribute(tr, tblDetails, count1, ".PVLines", "hdncount");
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
            PaymentMethod_changed();
            var ddlPaymentMethod = document.getElementById('PayMethod').value
            if (ddlPaymentMethod !== "Cash") {
                $("#div-BankName").removeClass('has-error1').addClass('has-success1');
                

            }

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
                    var str=$(this).find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1];
                    if (str.length > 15)
                        str = str.substring(0, 15)+"...";

                    $(this).find("#lblDocname").text(str);
                    $(this).find("input[id ^= 'FilePathInput']").attr("title", $(this).find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
                }
                k1++;
            });
            $("#FilePathview").attr("href", $("input[id='FilePath'][type='hidden']").val());
            $("#FilePathview").attr("target", "_blank");
        } else {
            /*New show Date Current date (DD-MM-yyyy)*/
            RedDot_NewDate(".datepicker,.datepickerH");
        }
        //#endregion
        debugger

        
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
        //$('#Country').select2({

        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select--'
        //});
        //$('#DBName').select2({
        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select--',

        //});
        //$('#Currency').select2({
        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select--',

        //});
        //$('#PayMethod').select2({
        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select --',

        //});
        //$('#VType').select2({
        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select --',

        //});
        //$('#DocStatus').select2({
        //    theme: "bootstrap",
        //    allowClear: true,
        //    placeholder: '--Select --',

        //});
        //#endregion
        //#region File Upload Header
        /*Header File Uplaod*/
        $("#FilePath").on("change", function () {
            var data = new FormData();
            var files = $("input[id = 'FilePath']").get(0).files;
            var val = $(this).val();
            switch (val.substring(val.lastIndexOf('.') + 1).toLowerCase()) {
                case 'gif': case 'jpg': case 'png': case 'pdf':                    
                    break;                
                default:
                    $(this).val('');
                    $("input[id='FilePath'][type='hidden']").val('');
                    $("#FilePathview").removeAttr("href");
                    $("#FilePathview").removeAttr("target");
                    // error message here
                    RedDotAlert_Error("Only image & pdf");                    
                   return;
                    break;
            }
            debugger
            $("#RefNo").removeAttr('disabled');
            if (files.length > 0) {
                data.append("Files", files[0]);
                data.append("type", "Header");
                data.append("Refno", $("#RefNo").val());
            }
            $("#RefNo").attr('disabled',true);
            $.ajax({
                url: "/SAP/RDD_PV/UploadDoc",
                type: "POST",
                processData: false,
                contentType: false,
                data: data,
                success: function (response) {
                    
                    if (response.indexOf("Error occurred") == 0) {
                        $("input[id='FilePath'][type='hidden']").val('');
                    $("#FilePathview").removeAttr("href");
                    $("#FilePathview").removeAttr("target");
                        RedDotAlert_Error(response);
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
            $("#RefNo").removeAttr('disabled');
            if (files.length > 0) {
                data.append("Files", files[0]);
                data.append("type", "Details");
                data.append("Refno", $("#RefNo").val());
            }
            $("#RefNo").attr('disabled', true);
            
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
                        var str = tr.find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1];
                        if (str.length > 15)
                            str = str.substring(0, 15) + "...";

                        tr.find("#lblDocname").text(str);
                       // tr.find("#lblDocname").text(tr.find("input[id ^= 'hdnFilePathInput']").val().split("/")[parseInt(k) - 1]);
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
            RedDot_Table_DeleteActivity(tr, tblDetails, ".PVLines", "hdncount");
            Calculation();
        })
        //#endregion
        var arr1 = "";
        var arr2 = "";
       
        $(document).on("change", "select[id^='VType']", function () {  
            arr1 = "";
            $('#VendorEmployee').val('');
            $("#VendorCode").val('');
            $("#Benificiary").val('');
            $("#div-Benificiary").removeClass('has-success1').addClass('has-error1');
            $("#div-VendorEmployee").removeClass('has-success1').addClass('has-error1');
            if ($(this).val() == "Vendor") {
                $("#lblemp").text('Vendor :');
               
                $(".creditlimit").show();
                var DBName = $("select[id^='DBName']").val();
                var Vtype = $("select[id^='VType']").val();
                GetVendor(DBName, Vtype);
            } else {
                if (jQuery('#VendorEmployee').data('autocomplete')) {
                    jQuery('#VendorEmployee').autocomplete("destroy");
                    jQuery('#VendorEmployee').removeData('autocomplete');
                }
                var EmpName = $(".u-text").find("B").text().replace("Welcome", '').trim();
                $("[id$=VendorEmployee]").val(EmpName);
                $("[id$=Benificiary]").val(EmpName);
                $("#div-VendorEmployee").removeClass('has-error1').addClass('has-success1');
                $("#div-Benificiary").removeClass('has-error1').addClass('has-success1');
                $("#lblemp").text('Employee :');
                $(".creditlimit").hide();
            }
            
        })
        if ($("select[id ^= 'VType']").val() == "Internal") {
            var EmpName = $(".u-text").find("B").text().replace("Welcome", '').trim();
            $("[id$=VendorEmployee]").val(EmpName);
            $("[id$=Benificiary]").val(EmpName);
            $("#div-VendorEmployee").removeClass('has-error1').addClass('has-success1');
            $("#div-Benificiary").removeClass('has-error1').addClass('has-success1');
            $("#lblemp").text('Employee :');
            $(".creditlimit").hide();
            $("select[id ^= 'VType']").attr("disabled", true);
        }
        $(document).on("change", "select[id^='DBName']", function () { 
            arr1 = "";
            arr2 = "";
            debugger
            if ($("select[id ^= 'VType']").val() != "Internal") {
                $('#VendorEmployee').val('');
                $("#VendorCode").val('');
                $("#Benificiary").val('');
                $("#div-Benificiary").removeClass('has-success1').addClass('has-error1');
                $("#div-VendorEmployee").removeClass('has-success1').addClass('has-error1');
            }

            var DBName = $("select[id^='DBName']").val();
            var Vtype = $("select[id^='VType']").val();
            GetVendor(DBName, Vtype);
            GetBank(DBName);
        })
        function GetVendor(DBName,Vtype) {
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/GetVendor",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    DBName: DBName,
                    Vtype: Vtype
                }),
                success: function (data) {

                    arr1 = data;
                    
                }
            });
            
        }

        function GetBank(DBName) {
            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/GetBank",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    DBName: DBName,
                    
                }),
                success: function (data) {
                    debugger
                    arr2 = data;

                }
            });
            $("#BankName").val('');
            $("#BankCode").val('');
            
        }
        var doc = $("#DocStatus").val();
        $(document).on("change", "select[id^='DocStatus']", function () {
            debugger
            if ($("#DocStatus").val() == "0" || $("#EditFlag").val() == "False") {
                return;
            }
            if (doc == "Paid - Closed" || doc == "Rejected-Closed") {
               // $("#DocStatus").val(doc).trigger('change');
                RdotAlerterrtxt("You can not change voucher status from Rejected to Open..");
                return;   
            }
            var data = JSON.stringify({
                DOC_Status: $("#DocStatus").val(),
                PVID: parseInt($("#PVId").val())
            });
            $.ajax({                          
                async: false,
                cache: false,
                type: "POST",
                url: "/ChangeVoucherStatus",
                contentType: "application/json",
                dataType: "json",
                data: data,
                success: function (response) {
                    if (response[0].Outtf == true) {
                        doc = $("#DocStatus").val();
                        RedDotAlert_Success(response[0].Responsemsg);
                    } else {
                        RedDotAlert_Error(response[0].Responsemsg);
                    }
                    
                },
                error: function (er) {
                    RedDotAlert_Error(er);
                }
            });
        })

        $(document).on("keydown keyup change", ".txtmax", function () {
            var value = $(this).val();

            var t = parseInt($(this).attr("data-val-length-max"));
            if (t < $(this).val().length) {
                $(this).val(value.substr(0, t));
                $("[data-valmsg-for='" + $(this).attr("id") + "']").text($(this).attr("data-val-length"));
            } else {
                $("[data-valmsg-for='" + $(this).attr("id") + "']").text('');
            }
        })
        $(document).on("blur", ".txtcheck", function () {
            
            if ($(this).val() !== '') {
                $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');

            }
            else {
                $("#div-" + $(this).attr("id") + "").removeClass('has-success1').addClass('has-error1');
                $(this).val('');

            }
        });
        if ($("#Country").val() !== '0') {
            $("#div-Country").removeClass('has-error1').addClass('has-success1');
        }
        if ($("#DocStatus").val() !== '0') {
            $("#div-DocStatus").removeClass('has-error1').addClass('has-success1');
        }
        $(document).on("blur", ".dropcheck", function () {
            debugger
            if ($(this).val() !== '0') {
                $("#div-" + $(this).attr("id") + "").removeClass('has-error1').addClass('has-success1');
            }
            else {
                $("#div-" + $(this).attr("id") + "").removeClass('has-success1').addClass('has-error1');
                $(this).val('0').trigger('change');
            }
        });

        $(document).on("change", "select[id^='PayMethod']", function () {
            PaymentMethod_changed();
            $("#BankName").val('');
            $("#BankCode").val(''); 
        })

        function PaymentMethod_changed() {
            var ddlPaymentMethod = document.getElementById('PayMethod').value
            if (ddlPaymentMethod == "Cheque") {
                $("[id$=lblChequeNo]").html("Cheque No");
                $("[id$=lblChequeDate]").html("Cheque Date");
                $("[id$=lblChequeImage]").html("Cheque Image");
                if ($("#ViewMode").val() != "True") {
                    $("#div-BankName").removeClass('has-success1').addClass('has-error1');
                    document.getElementById('BankName').disabled = false;
                } else {
                    $("#div-BankName").removeClass('has-error1');
                    document.getElementById('BankName').disabled = true;
                }
                
            }
            else if (ddlPaymentMethod == "TT") {
                $("[id$=lblChequeNo]").html("TT No");
                $("[id$=lblChequeDate]").html("TT Date");
                $("[id$=lblChequeImage]").html("TT Image");
                
                if ($("#ViewMode").val() != "True") {
                    $("#div-BankName").removeClass('has-success1').addClass('has-error1');
                    document.getElementById('BankName').disabled = false;
                } else {
                    $("#div-BankName").removeClass('has-error1');
                    document.getElementById('BankName').disabled = true;
                }
              
            }
            else {
                $("[id$=lblChequeNo]").html("Pay Ref No");
                $("[id$=lblChequeDate]").html("Pay Ref Date");
                $("[id$=lblChequeImage]").html("Pay Ref Image");
                $("#div-BankName").removeClass('has-success1');
                $("#div-BankName").removeClass('has-error1');
                document.getElementById('BankName').disabled = true;
            }
        }

        $('#VendorEmployee').autocomplete({

            source: function (request, response) {

                try {
                    if (arr1 != "") {
                        debugger
                        var k = $('#VendorEmployee').val().toLowerCase();
                        var results = arr1.Table.filter(function (elem) {
                            return elem.CardName.toLowerCase().indexOf(k) > -1;
                        });
                        if (results.length > 0) {
                            response($.map(results, function (value, key) {
                                return {
                                    label: value.CardName,
                                    value: value.CardName,
                                    val1: value.CardCode,
                                    
                                };

                            }));
                        }
                        else {
                            $('#VendorEmployee').val('');
                            $("#VendorCode").val('');
                            $("#Benificiary").val('');
                            $("#div-VendorEmployee").removeClass('has-success1').addClass('has-error1');
                            $("#div-Benificiary").removeClass('has-success1').addClass('has-error1');
                            response([{ label: 'No results found.', value: 'No results found.' }]);
                        }

                    }
                } catch (e) {

                }


            },
            select: function (event, u) {
                try {

                    $("#VendorEmployee").val(u.item.value);
                    $("#div-Benificiary").removeClass('has-error1').addClass('has-success1');
                    $("#Benificiary").val(u.item.value);
                    $("#VendorCode").val(u.item.val1);                                                    
                } catch (e) {

                }


            },
            minLength: 1,
            delay: 100
        }).focus(function (e, u) {
            $(this).autocomplete("search", "");
        });
        $(document).on("focusout", "#VendorEmployee", function () {
            debugger
            var V_AGE = ["0-30", "31-45", "46-60", "61-90", "91-120", "121+", "Balance"];
            if ($("#VType").val() == "Vendor") {
                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "/GetVendorAgeing",
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        DBName: $("select[id^='DBName']").val(),
                        BP: $("#VendorCode").val()
                    }),
                    success: function (data) {
                        var arr3 = data;
                        $("#txtbal").val(arr3.Table[0][V_AGE[6]] == null ? 0 : arr3.Table[0][V_AGE[6]]);
                        $("#txt0").val(arr3.Table[0][V_AGE[0]] == null ? 0 : arr3.Table[0][V_AGE[0]])
                        $("#txt31").val(arr3.Table[0][V_AGE[1]] == null ? 0 :arr3.Table[0][V_AGE[1]])
                        $("#txt46").val(arr3.Table[0][V_AGE[2]] == null ? 0 :arr3.Table[0][V_AGE[2]])
                        $("#txt61").val(arr3.Table[0][V_AGE[3]] == null ? 0 :arr3.Table[0][V_AGE[3]])
                        $("#txt91").val(arr3.Table[0][V_AGE[4]] == null ? 0 :arr3.Table[0][V_AGE[4]])
                        $("#txt121").val(arr3.Table[0][V_AGE[5]] == null ? 0 :arr3.Table[0][V_AGE[5]])



                    }
                });
            }
        });
        $('#BankName').autocomplete({

            source: function (request, response) {

                try {
                    debugger
                    if (arr2 != "") {

                        var k = $('#BankName').val().toLowerCase();
                        var results = arr2.Table.filter(function (elem) {
                            return elem.AcctName.toLowerCase().indexOf(k) > -1;
                        });
                        if (results.length > 0) {
                            response($.map(results, function (value, key) {
                                return {
                                    label: value.AcctName,
                                    value: value.AcctName,
                                    val1: value.AcctCode,

                                };

                            }));
                        }
                        else {
                            $("#BankName").val('');
                            $("#BankCode").val('');
                            response([{ label: 'No results found.', value: 'No results found.' }]);
                        }

                    }
                    else {

                        $("#BankName").val('');
                        $("#BankCode").val('');
                        
                        response([{ label: 'No results found.', value: 'No results found.' }]);
                    }
                } catch (e) {

                }


            },
            select: function (event, u) {
                try {
                    $("#BankName").val(u.item.value);
                    $("#BankCode").val(u.item.val1);                   
                } catch (e) {

                }
            },
            minLength: 1,
            delay: 100
        }).focus(function (e, u) {
            $(this).autocomplete("search", "");
        });

        function Calculation() {
            var sum = 0.00;
            debugger
            $(".PVLines").each(function () {
                var a = $(this).find('.Abcd input[id^="Amount"]').val();
                a = a || 0
                sum = parseFloat(sum) +  parseFloat(a);
            })
            
           
            $("#ApprovedAmt").val(sum.toFixed(2));
        }


        $('.number').keypress(function (event) {
            var $this = $(this);
            if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
                ((event.which < 48 || event.which > 57) &&
                    (event.which != 0 && event.which != 8))) {
                event.preventDefault();
            }

            var text = $(this).val();
            if ((event.which == 46) && (text.indexOf('.') == -1)) {
                setTimeout(function () {
                    if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                        $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                    }
                }, 1);
            }

            if ((text.indexOf('.') != -1) &&
                (text.substring(text.indexOf('.')).length > 2) &&
                (event.which != 0 && event.which != 8) &&
                ($(this)[0].selectionStart >= text.length - 2)) {
                event.preventDefault();
            }
        });

        $('.number').bind("paste", function (e) {
            var text = e.originalEvent.clipboardData.getData('Text');
            if ($.isNumeric(text)) {
                if ((text.substring(text.indexOf('.')).length > 3) && (text.indexOf('.') > -1)) {
                    e.preventDefault();
                    $(this).val(text.substring(0, text.indexOf('.') + 3));
                }
            }
            else {
                e.preventDefault();
            }
        });
	}
}