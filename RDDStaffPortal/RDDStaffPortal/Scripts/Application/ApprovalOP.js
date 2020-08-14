var ApprovalOP = {
    initialize: function (e) {
      
       // $.fn.dataTable.ext.errorMode = 'none';
        
       

            ApprovalOP.Attachevent();
       
    },
    Attachevent: function () {
        var k1 = 1;
        var k2 = 1;
        if ($("#EditFlag").val() == "True") {

            if ($("input[id='Condition']").val() == 'True') {
                $("#IIndCondition").trigger("click");
            }
            if ($("input[id='Status']:hidden").val() == 'True') {
                $("#TempStatus").trigger('click');
            }
            $(".ApproverDet").each(function () {
                applyAutoComplete2("#Approver" + k1, "#hdnApprover" + k1, "/GetUserListAuto");


                if ($(this).find("input[id^='hdnMandtory']").val() == 'True') {
                    $(this).find(".colorinput-input").trigger('click');
                }

                k1++;
            });
            $(".OriginatorDet").each(function () {
                applyAutoComplete2("#Originator" + k2, "#hdnOriginator" + k2, "/GetUserListAuto");
                k2++;
            });

        } else {
            $("#TempStatus").trigger('click');
            applyAutoComplete2("#Originator1", "#hdnOriginator1", "/GetUserListAuto");
            applyAutoComplete2("#Approver1", "#hdnApprover1", "/GetUserListAuto");
        } 


        var tblDetails = ['OriSrno', 'Originator', 'hdnOriginator'];
        var tblDetails1 = ['SeqSrno', 'Approver', 'hdnApprover'];
        //#region Originator Add  & Remove Row
        $(document).on("focusout", "#IIst input[id^='Originator']", function (e) {
            
            e.preventDefault();
            var tr = $("#IIst").clone(true);
            var tr2 = $(this).closest("#IIst");
            var count2 = parseInt($("#hdncount").val());

            var m = tr2.find("input[id^='OriSrno']").val();
            var k = tr2.find("input[id^='hdnOriginator']").val();
            if (k == '-1' || k == '') {
                tr2.find("input[id^='Originator']").val('');
                return;
            }

            drec = [];
            $('.OriginatorDet').each(function () {
                //add item to array
                var ab = $(this).find(".Abcd input[id^='hdnOriginator']").val();
                drec.push(ab);
            });
            drec.splice($.inArray(tr2.find(".Abcd input[id^='hdnOriginator']").val(), drec), 1);
            if ($.inArray(tr2.find(".Abcd input[id^='hdnOriginator']").val(), drec) >= 0) {
                tr2.css("background", "red");
                tr2.find(".Abcd input[id^='Originator']").focus();
                tr2.find(".Abcd input[id^='Originator']").val('');
                tr2.find(".Abcd input[id^='hdnOriginator']").val('-1');
                RedDotAlert_Error("Already Exist Code '" + k + "'");
                return;
            } else {

                tr2.css("background", "");
                if (m < count2) {
                    return;
                }
                var count1 = parseInt(count2) + 1;
                RedDot_Table_Attribute(tr, tblDetails, count1, ".OriginatorDet", "hdncount");

                $("#IIbody").append(tr);


                if ($("#Originator" + count1).data('uiAutocomplete')) {
                    $("#Originator" + count1).autocomplete("destroy");
                    $("#Originator" + count1).removeData('uiAutocomplete');
                    $("#Originator" + count1).removeClass("ui-autocomplete-input");
                    applyAutoComplete2("#Originator1", "#hdnOriginator1", "/GetUserListAuto");
                }

                applyAutoComplete2("#Originator" + count1, "#hdnOriginator" + count1, "/GetUserListAuto");



            }



        })

        $(document).on("click", "#IIst button[id^='btntblDel']", function (e) {
            e.preventDefault();
            var count2 = parseInt($("#hdncount").val())
            

            var tr = $(this).closest("#IIst");
            var m = tr.find("input[id^='OriSrno']").val();
            if (count2 == 1 || m == count2) {
                return
            }

            RedDot_Table_DeleteActivity(tr, tblDetails, ".OriginatorDet", "hdncount");
            var t = 1;
            $(".OriginatorDet").each(function () {

                if ($("#Originator" + t).data('uiAutocomplete')) {
                    $("#Originator" + t).autocomplete("destroy");
                    $("#Originator" + t).removeData('uiAutocomplete');

                }
                $("#Originator" + t).removeClass("ui-autocomplete-input");
                applyAutoComplete2("#Originator" + t, "#hdnOriginator" + t, "/GetUserListAuto");
                t++;


            });



        })
        //#endregion
        //#region Approver Add & Remove Row
        $(document).on("focusout", "#IIIst input[id^='Approver']", function () {            
            var tr = $("#IIIst").clone(true);
            var tr2 = $(this).closest("#IIIst");
            var count2 = parseInt($("#hdncount1").val())
            var m = tr2.find("input[id^='SeqSrno']").val();
            var k = tr2.find("input[id^='hdnApprover']").val();
            if (k == '-1' || k == '') {
                tr2.find("input[id^='Approver']").val('');
                return;
            }

            drec = [];
            $('.ApproverDet').each(function () {
                //add item to array
                var ab = $(this).find(".Abcd input[id^='hdnApprover']").val();
                drec.push(ab);
            });
            drec.splice($.inArray(tr2.find(".Abcd input[id^='hdnApprover']").val(), drec), 1);
            if ($.inArray(tr2.find(".Abcd input[id^='hdnApprover']").val(), drec) >= 0) {
                tr2.css("background", "red");
                tr2.find(".Abcd input[id^='Approver']").focus();
                tr2.find(".Abcd input[id^='Approver']").val('');
                tr2.find(".Abcd input[id^='hdnApprover']").val('-1');
                RedDotAlert_Error("Already Exist Code '" + k + "'");
                return;
            } else {

                tr2.css("background", "");
                if (m < count2) {
                    return;
                }
                var count1 = parseInt(count2) + 1;
                RedDot_Table_Attribute(tr, tblDetails1, count1, ".ApproverDet", "hdncount1");

                $("#IIIbody").append(tr);


                if ($("#Approver" + count1).data('uiAutocomplete')) {
                    $("#Approver" + count1).autocomplete("destroy");
                    $("#Approver" + count1).removeData('uiAutocomplete');
                    $("#Approver" + count1).removeClass("ui-autocomplete-input");
                    applyAutoComplete2("#Approver1", "#hdnApprover1", "/GetUserListAuto");
                }



                applyAutoComplete2("#Approver" + count1, "#hdnApprover" + count1, "/GetUserListAuto");
            }

        })

        $(document).on("click", "#IIIst button[id^='btntblDel']", function () {
            var count2 = parseInt($("#hdncount1").val())
            
            var tr = $(this).closest("#IIIst");
            var m = tr.find("input[id^='SeqSrno']").val();
            if (count2 == 1 || m == count2) {
                return
            }
            RedDot_Table_DeleteActivity(tr, tblDetails1, ".ApproverDet", "hdncount1");

            var t = 1;
            $(".ApproverDet").each(function () {

                if ($("#Approver" + t).data('uiAutocomplete')) {
                    $("#Approver" + t).autocomplete("destroy");
                    $("#Approver" + t).removeData('uiAutocomplete');

                }
                $("#Approver" + t).removeClass("ui-autocomplete-input");
                applyAutoComplete2("#Approver" + t, "#hdnApprover" + t, "/GetUserListAuto");
                t++;


            });

        })
        //#endregion
        //#region Condiotion text
        $('#Condition_Text').on("focusout", function () {
            if ($("#Condition_Text").val().indexOf('truncat') >= 0) {
                RedDotAlert_Warning("Do not write " + $("#Condition_Text").val());
                $("#Condition_Text").val('');
                $("#Condition_Text").focus();

            } else if ($("#Condition_Text").val().indexOf('insert') >= 0) {
                RedDotAlert_Warning("Do not write " + $("#Condition_Text").val())
                $("#Condition_Text").val('');
                $("#Condition_Text").focus();

            } else if ($("#Condition_Text").val().indexOf('delete') >= 0) {
                RedDotAlert_Warning("Do not write " + $("#Condition_Text").val())
                $("#Condition_Text").val('');
                $("#Condition_Text").focus();
            }
        })
        //#endregion
    }
}