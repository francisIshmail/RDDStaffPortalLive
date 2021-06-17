var ItemMaster = function () { };  //Class

var btnType = "";

ItemMaster.prototype = {

    Init: function () {
        debugger;
        ItemMaster.prototype.ControlInit();
        ItemMaster.prototype.ClickEvent();

    },

    ControlInit: function () {
        $('.loader1').hide();
        $('#LblMsg').text('Not Connected to SAP');
        $.ajax({


            async: false,
            cache: false,
            type: "POST",
            url: "/SAP/ItemMaster/Get_BindDDLList",
            data: JSON.stringify({ type: 'MF', value: '0' }),
            dataType: 'Json',
            contentType: "Application/json",

            success: function (value) {
                debugger;
                var jData = value

                var ddlManufacturer = jData.Table;

                $("[id$=cbManufacturer]").empty();
                for (var i = 0; i < ddlManufacturer.length; i++) {

                    $("[id$=cbManufacturer]").append($("<option></option>").val(ddlManufacturer[i].FirmCode).html(ddlManufacturer[i].FirmName));
                }
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    },

    ClearControl: function () {

        $("#chkDB_AE").prop('checked', false);
        $("#chkDB_UG").prop('checked', false);
        $("#chkDB_TZ").prop('checked', false);
        $("#chkDB_KE").prop('checked', false);
        $("#chkDB_ZM").prop('checked', false);
        $("#chkDB_ML").prop('checked', false);
        $("#chkDB_TRI").prop('checked', false);

        $("[id$=cbManufacturer]").val('').trigger('change');
        $("[id$=cbBU]").val('').trigger('change');
        $("[id$=cbPC]").val('').trigger('change');
        $("[id$=cbPL]").val('').trigger('change');
        $("[id$=cbPG]").val('').trigger('change');

        $("[id$=txItemCode]").val('');
        $("[id$=txDescr]").val('');
        $("[id$=txLength]").val('');
        $("[id$=txWidth]").val('');
        $("[id$=txHeight]").val('');
        $("[id$=txWeight]").val('');
    },

    ClickEvent: function () {

        $("[id$=btnConnectToSAP]").click(function () {

            debugger;

            $('.loader1').show();
            $('#LblMsg').text('Not Connected to SAP');
            var dbList = "SAPAE;SAPKE;SAPTZ;SAPUG;SAPZM;SAPML;SAPTRI";
            $.ajax({
                //async: false,
                //cache: false,
                type: "POST",
                url: "/SAP/ItemMaster/Connet_To_SAP",
                data: JSON.stringify({ dbname: dbList }),
                dataType: 'Json',
                contentType: "Application/json",
                success: function (value) {
                    debugger;
                    var jData = value;
                    var ErrorMsg = "";
                    var msg = "";
                    for (var i = 0; i < jData.table.length; i++) {
                        if (jData.table[i].Result == 'True') {
                            $("#" + jData.table[i].Message + "Check").prop('checked', true);
                            var DBCode = jData.table[i].Message.replace('SAP','');
                            $("#chkDB_" + DBCode).prop('checked', true);
                        }
                        else {
                            $("#" + jData.table[i].Message + "Check").prop('checked', false);
                            ErrorMsg = ErrorMsg + ' ; ' + jData.table[i].Message;
                           // $("#chkDB_" + DBCode).prop('checked', false);
                        }
                    }
                    if (ErrorMsg == '') {
                        $('#LblMsg').text('SAP Companies are Connected .... Now Fill in the Form and click "Add Items to selected DBs button" ');
                    }
                    else {
                        $('#LblMsg').text(ErrorMsg);
                    }
                    //$("label[for=BULable]").html("Code : " + str1 + ", BU : " + str2);
                    $('.loader1').hide();
                },
                error: function (response) {
                    RedDotAlert_Error(response.responseText);
                    $('.loader1').hide();
                },
                failure: function (response) {
                    RedDotAlert_Error(response.responseText);
                    $('.loader1').hide();
                }
            });

        });

        $("#cbManufacturer").change(function () {
            try {
                if (($("#cbManufacturer").val() != "--Select--") && ($("#cbManufacturer").val() != "") && ($("#cbManufacturer").val() != "0")) {

                    $.ajax({

                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/ItemMaster/Get_BindDDLList",
                        data: JSON.stringify({ type: 'BU', value: $("#cbManufacturer").val() }),
                        dataType: 'Json',
                        contentType: "Application/json",

                        success: function (value) {
                            debugger;
                            var jData = value

                            var ddlBU = jData.Table;

                            $("#cbBU").empty();
                            for (var i = 0; i < ddlBU.length; i++) {

                                $("#cbBU").append($("<option></option>").val(ddlBU[i].fldValue).html(ddlBU[i].ShowVal));
                            }

                            $("#cbPC").empty();
                            $("#cbPL").empty();
                            $("#cbPG").empty();

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                }

            }
            catch (Error) {
                alert(Error);
            }
        });

        $("#cbBU").change(function () {
            try {
                if (($("#cbBU").val() != "--Select BU--") && ($("#cbBU").val() != "") && ($("#cbBU").val() != "0")) {
                    Get_PC_Values();
                    Get_PG_Values();

                    var str = $("#cbBU option:selected").text()
                    var str_Pos = str.indexOf("[") + 1;
                    var lst_Pos = str.indexOf("]") ;
                    var str1 = str.substring(str_Pos, lst_Pos);
                    var str2 = str.substring(0, str_Pos-2);
                    $("label[for=BULable]").html("Code : "+ str1+", BU : "+str2);
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("#cbPC").change(function () {
            try {
                if (($("#cbPC").val() != "--Select--") && ($("#cbPC").val() != "") && ($("#cbPC").val() != "0")) {
                    Get_PL_Values();
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=btnAddPC]").click(function () {
            debugger;
            if (($("#cbBU").val() != "--Select BU--") && ($("#cbBU").val() != "") && ($("#cbBU").val() != "0")) {
                btnType = "btnAddPC";
                $('#AddNewPopup').modal('show');// data - target="#AddNewPopup"
            }
            else {
                RedDotAlert_Warning("Select BU..");
                return;
            }
        });

        $("[id$=btnAddPL]").click(function () {
            debugger;
            if (($("#cbPC").val() != "--Select--") && ($("#cbPC").val() != "") && ($("#cbPC").val() != "0")) {
                btnType = "btnAddPL";
                $('#AddNewPopup').modal('show');// data - target="#AddNewPopup"
            }
            else {
                RedDotAlert_Warning("Select PC..");
                return;
            }
        });

        $("[id$=btnAddPG]").click(function () {
            if (($("#cbBU").val() != "--Select BU--") && ($("#cbBU").val() != "") && ($("#cbBU").val() != "0")) {
                btnType = "btnAddPG";
                $('#AddNewPopup').modal('show');
            }
            else {
                RedDotAlert_Warning("Select BU..");
                return;
            }
        });

        $("[id$=btnPopCancel]").click(function () {
            $('#AddNewPopup').modal('hide');
            $("[id$=txtPopValue]").val('');
            $("[id$=txtPopDescr]").val('');
        });

        $("[id$=btnPopCross]").click(function () {
            $('#AddNewPopup').modal('hide');
            $("[id$=txtPopValue]").val('');
            $("[id$=txtPopDescr]").val('');
        });

        $("[id$=btnPopSave]").click(function () {
            debugger;
            try {
                if (btnType == "btnAddPC") {
                    if (($("#cbBU").val() != "--Select BU--") && ($("#cbBU").val() != "") && ($("#cbBU").val() != "0")) {

                        $.ajax({

                            async: false,
                            cache: false,
                            type: "POST",
                            url: "/SAP/ItemMaster/Part_ToAddNew_Value",
                            data: JSON.stringify({ insfor: 'PC', value: $("[id$=txtPopValue]").val(), descr: $("[id$=txtPopDescr]").val(), type: $("[id$=cbBU]").val() }),
                            dataType: 'Json',
                            contentType: "Application/json",

                            success: function (value) {
                                debugger;
                                var jData = value

                                if (jData.Table[0].Result == 'True') {
                                    RedDotAlert_Success("Product Category Saved Successfully..");
                                    $('#AddNewPopup').modal('hide');
                                    $("[id$=txtPopValue]").val('');
                                    $("[id$=txtPopDescr]").val('');
                                    btnType = "";
                                    Get_PC_Values();
                                }
                                else
                                    RedDotAlert_Warning(jData.Table[0].Message)
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }
                    else
                        RedDotAlert_Warning("Select BU..")
                }
                else if (btnType == "btnAddPG") {
                    if (($("#cbBU").val() != "--Select BU--") && ($("#cbBU").val() != "") && ($("#cbBU").val() != "0")) {

                        $.ajax({

                            async: false,
                            cache: false,
                            type: "POST",
                            url: "/SAP/ItemMaster/Part_ToAddNew_Value",
                            data: JSON.stringify({ insfor: 'PG', value: $("[id$=txtPopValue]").val(), descr: $("[id$=txtPopDescr]").val(), type: $("[id$=cbBU]").val() }),
                            dataType: 'Json',
                            contentType: "Application/json",

                            success: function (value) {
                                debugger;
                                var jData = value

                                if (jData.Table[0].Result == 'True') {
                                    RedDotAlert_Success("Product Group Saved Successfully..");
                                    $('#AddNewPopup').modal('hide');
                                    $("[id$=txtPopValue]").val('');
                                    $("[id$=txtPopDescr]").val('');
                                    btnType = "";
                                    Get_PG_Values();
                                }
                                else
                                    RedDotAlert_Warning(jData.Table[0].Message)
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }
                    else
                        RedDotAlert_Warning("Select BU..")
                }
                else if (btnType == "btnAddPL") {
                    if (($("#cbPC").val() != "--Select--") && ($("#cbPC").val() != "") && ($("#cbPC").val() != "0")) {

                        $.ajax({

                            async: false,
                            cache: false,
                            type: "POST",
                            url: "/SAP/ItemMaster/Part_ToAddNew_Value",
                            data: JSON.stringify({ insfor: 'PL', value: $("[id$=txtPopValue]").val(), descr: $("[id$=txtPopDescr]").val(), type: $("[id$=cbPC]").val() }),
                            dataType: 'Json',
                            contentType: "Application/json",

                            success: function (value) {
                                debugger;
                                var jData = value

                                if (jData.Table[0].Result == 'True') {
                                    RedDotAlert_Success("Product Line Saved Successfully..");
                                    $('#AddNewPopup').modal('hide');
                                    $("[id$=txtPopValue]").val('');
                                    $("[id$=txtPopDescr]").val('');
                                    btnType = "";
                                    Get_PL_Values();
                                }
                                else
                                    RedDotAlert_Warning(jData.Table[0].Message)
                            },
                            error: function (response) {
                                alert(response.responseText);
                            },
                            failure: function (response) {
                                alert(response.responseText);
                            }
                        });
                    }
                    else
                        RedDotAlert_Warning("Select Product Category..")
                }
            }
            catch (Error) {
                alert(Error);
            }

        });

        $("[id$=btnCancel]").click(function () {
           // ItemMaster.prototype.ClearControl();
            ClearControlAfterSave();
        });

        $("[id$=btnAddItem]").click(function () {
            debugger;
            try {

                if ($('#SAPAECheck').is(":checked") == false || $('#SAPUGCheck').is(":checked") == false || $('#SAPTZCheck').is(":checked") == false || $('#SAPKECheck').is(":checked") == false || $('#SAPZMCheck').is(":checked") == false || $('#SAPMLCheck').is(":checked") == false || $('#SAPTRICheck').is(":checked") == false) {

                    RedDotAlert_Warning("Failed to connect to one of SAP database, please try again...");
                    return false;
                }

                var dbList = "";
                if ($('#chkDB_AE').is(":checked")) {
                    dbList = "SAPAE";
                }
               
                if ($('#chkDB_UG').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPUG"
                    else
                        dbList = "SAPUG";
                }
               
                if ($('#chkDB_TZ').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPTZ"
                    else
                        dbList = "SAPTZ";
                }
               
                if ($('#chkDB_KE').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPKE"
                    else
                        dbList = "SAPKE";
                }
               
                if ($('#chkDB_ZM').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPZM"
                    else
                        dbList = "SAPZM";
                }
               

                if ($('#chkDB_ML').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPML"
                    else
                        dbList = "SAPML";
                }
                
                if ($('#chkDB_TRI').is(":checked")) {
                    if (dbList.length > 0)
                        dbList = dbList + ";SAPTRI"
                    else
                        dbList = "SAPTRI";
                }

                if (dbList == '') {
                    RedDotAlert_Warning("Select atleast one SAP database to add item");
                    return false;
                }

                if (Validate() == false) {
                    return false;
                }

                var itmCode, itmDesc, mfrId, itmGrpId, itmGrpCode, itmBU, itmProductCategory, itmPL, itmProductGrp, Lenght, Width, Height, Weight,HSCode;
                itmCode = $("#txItemCode").val();
                itmDesc = $("#txDescr").val();
                mfrId = $("[id$=cbManufacturer]").val();
                itmGrpId = $("[id$=cbBU]").val();
                HSCode = $("[id$=txHSCode]").val();
                debugger;
                var str = $("#cbBU option:selected").text()
                var str_Pos = str.indexOf("[")+1;
                var lst_Pos = str.indexOf("]");
                var str1 = str.substring(str_Pos, lst_Pos);


                itmGrpCode = str1;//$("#cbBU option:selected").text()
                itmBU = $("[id$=cbBU]").val();
                itmProductCategory = $("#cbPC option:selected").text();
                itmPL = $("[id$=cbPL]").val();
                itmProductGrp = $("#cbPG option:selected").text(); //$("[id$=cbPG]").val()
                Lenght = $("[id$=txLength]").val();
                Width = $("[id$=txWidth]").val();
                Height = $("[id$=txHeight]").val();

                if ($("[id$=txWeight]").val() == "")
                    Weight = 0;
                else
                    Weight = $("[id$=txWeight]").val();

                if (dbList.length > 0) {
                    $.ajax({
                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/ItemMaster/Add_ItemToSAPDB",
                        data: JSON.stringify({ DBList: dbList, itmCode: itmCode, itmDesc: itmDesc, mfrId: mfrId, itmGrpId: itmGrpId, itmGrpCode: itmGrpCode, itmBU: itmBU, itmProductCategory: itmProductCategory, itmPL: itmPL, itmProductGrp: itmProductGrp, Lenght: Lenght, Width: Width, Height: Height, Weight: Weight, HSCode: HSCode}),
                        dataType: 'Json',
                        contentType: "Application/json",
                        success: function (value) {
                            debugger;
                            var jData = value;
                            var _msgTrue = "";
                            var _msgFalse = "";

                            for (var i = 0; i < jData.table.length; i++) {
                                if (jData.table[i].Result == 'True') {
                                    _msgTrue = _msgTrue + "#" + (i + 1).toString() + ". " + jData.table[i].Message;
                                }
                                else
                                    _msgFalse = _msgFalse + "#" + (i + 1).toString() + ". " + jData.table[i].Message;
                            }
                            if (_msgTrue != "")
                                RedDotAlert_Success(_msgTrue);

                            if (_msgFalse != "")
                                RedDotAlert_Error(_msgFalse);

                            ClearControlAfterSave();

                        },
                        error: function (response) {
                            RedDotAlert_Error(response.responseText);
                        },
                        failure: function (response) {
                            RedDotAlert_Error(response.responseText);
                        }
                    });
                }
               
            }
            catch (Error) {
                alert(Error);
                return false;
            }
        })

        //HS Code validation
        $("[id$=txHSCode]").on('blur', function () {           
            var t = true;           
            // before 4 char and  2 char exact
            //8888.88 -abcd.ef  valid             
            var testEmail = /^\w{4}\.\w{2}$|^\d{0,11}$/;// /[0-9a-zA-Z]{4}+.[0-9a-zA-Z]{2}/;
            var t = $(this).val().match(testEmail)
            if (t!==null) {
                $(this).val(t[0]);
                t = false;
            }
            else {
                $(this).val('');
                $(this).attr("placeholder", "Enter HS Code");
                t = true;
            }

        })
    },

}

function ClearControlAfterSave() {
    debugger;
    $("[id$=cbManufacturer]").val('').trigger('change');
    $("[id$=cbBU]").val('').trigger('change');
    $("[id$=cbPC]").val('').trigger('change');
    $("[id$=cbPL]").val('').trigger('change');
    $("[id$=cbPG]").val('').trigger('change');

    $("[id$=txItemCode]").val('');
    $("[id$=txHSCode]").val('');
    $("[id$=txDescr]").val('');
    $("[id$=txLength]").val('');
    $("[id$=txWidth]").val('');
    $("[id$=txHeight]").val('');
    $("[id$=txWeight]").val('');

}

function Validate() {
    debugger;
    try {

        if ($("#txItemCode").val() == '' || $("#txDescr").val() == '') {
            RedDotAlert_Warning("One or more fields left blank!!! Either Item Code or Item Description is left blank. Make sure these 2 textboxes are not blank.");
            return false;
        }

        if ($("#txItemCode").val().indexOf("'") >= 0) {
            RedDotAlert_Warning("Invalid Character occurs ' in field Item Code , Char( ' ) not supported.");
            return false;
        }

        if ($("#txDescr").val().indexOf("'") >= 0) {
            RedDotAlert_Error("Invalid Character occurs ' in field Item Desc , Char( ' ) not supported.");
            return false;
        }

        if ($('#chkDB_AE').is(":checked") == false && $('#chkDB_UG').is(":checked") == false && $('#chkDB_TZ').is(":checked") == false && $('#chkDB_KE').is(":checked") == false && $('#chkDB_ZM').is(":checked") == false && $('#chkDB_ML').is(":checked") == false && $('#chkDB_TRI').is(":checked") == false ) {
            RedDotAlert_Warning("Error !  Please select at least one database to proceed and retry");
            return false;
        }

        if ($("[id$=cbManufacturer]").val() == '0' || $("[id$=cbManufacturer]").val() == '' || $("[id$=cbManufacturer]").val() == '--Select--') {
            RedDotAlert_Warning("Erro ! Field Manufacturer left blank!!!.Make sure you select from the list, and retry");
            return false;
        }

        //if ($("#cbManufacturer option:selected").text().indexOf("'")>= '0' ) {
        //    RedDotAlert_Error("Invalid Character occurs ' in field Manufacturer , Char( ' ) not supported.");
        //    return false;
        //}

        if ($("[id$=cbBU]").val() == '0' || $("[id$=cbBU]").val() == '' || $("[id$=cbBU]").val() == '--Select--') {
            RedDotAlert_Warning("Erro ! Field BU left blank!!!.Make sure you select from the list, and retry");
            return false;
        }
        
       
        //if ($("#cbBU option:selected").text().indexOf("'") >= '0') {
        //    RedDotAlert_Error("Invalid Character occurs ' in field Manufacturer , Char( ' ) not supported.");
        //    return false;
        //}
        

        if ($("[id$=cbPC]").val() == '0' || $("[id$=cbPC]").val() == '' || $("[id$=cbPC]").val() == '--Select--') {
            RedDotAlert_Warning("Erro ! Field  Product Category left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry");
            return false;
        }

        if ($("#cbPC option:selected").text().indexOf("'") >= '0') {
            RedDotAlert_Error("Invalid Character occurs ' in field  Product Category, Char( ' ) not supported.");
            return false;
        }

        if ($("[id$=cbPL]").val() == '0' || $("[id$=cbPL]").val() == '' || $("[id$=cbPL]").val() == '--Select--') {
            RedDotAlert_Warning("Erro ! Field Product Line left blank!!!. Make sure either you select from the list or Enter new value in text field provided for it, and retry");
            return false;
        }

        if ($("#cbPL option:selected").text().indexOf("'") >= '0') {
            RedDotAlert_Error("Invalid Character occurs ' in field Product Line , Char( ' ) not supported.");
            return false;
        }

        if ($("[id$=cbPG]").val() == '0' || $("[id$=cbPG]").val() == '' || $("[id$=cbPG]").val() == '--Select--') {
            RedDotAlert_Warning("Erro! Field  Product Group left blank!!!.Make sure either you select from the list or Enter new value in text field provided for it, and retry");
            return false;
        }

        if ($("#cbPG option:selected").text().indexOf("'") >= '0') {
            RedDotAlert_Error("Invalid Character occurs ' in field  Product Group, Char( ' ) not supported.");
            return false;
        }
        var str2 = "";
        try {
            var str = $("#cbBU option:selected").text()
            var str_Pos = str.indexOf("[") + 1;
            var lst_Pos = str.indexOf("]");
           
             str2 = str.substring(0, str_Pos - 2);
        } catch (e) {

        }
       
        if ($.inArray(str2, ['MS FG', 'MS OEM', 'MS CSP']) == -1 && $("[id$=HSCode]").val() == '') {
            RedDotAlert_Warning("Erro ! Please enter HS Code, and retry");
            return false;
        }
        if ($("[id$=txLength]").val() == '') {
            RedDotAlert_Error("Error ! Please enter Length, and retry");
            return false;
        }

        if (parseFloat($("[id$=txLength]").val().trim()) <= 0) {
            RedDotAlert_Error("Error ! Please enter Length greater than zero, and retry");
            return false;
        }
        if ($("[id$=txWidth]").val() == '') {
            RedDotAlert_Error("Error ! Please enter Width, and retry");
            return false;
        }
        if (parseFloat($("[id$=txWidth]").val().trim()) <= 0) {
            RedDotAlert_Error("Error ! Please enter width greater than zero, and retry");
            return false;
        }
        if ($("[id$=txHeight]").val() == '') {
            RedDotAlert_Error("Error ! Please enter Height, and retry");
            return false;
        }
        if (parseFloat($("[id$=txHeight]").val().trim()) <= 0) {
            RedDotAlert_Error("Error ! Please enter height greater than zero, and retry");
            return false;
        }

        if ($("[id$=txWeight]").val() == '') {
            RedDotAlert_Error("Error ! Please enter Weight, and retry");
            return false;
        }
        if (parseFloat($("[id$=txWeight]").val().trim()) <= 0) {
            RedDotAlert_Error("Error ! Please enter Weight greater than zero, and retry");
            return false;
        }
       
        return true;
    }
    catch (Error) {
        alert(Error);
        return false;
    }
}

function Get_PC_Values() {
    $.ajax({

        async: false,
        cache: false,
        type: "POST",
        url: "/SAP/ItemMaster/Get_BindDDLList",
        data: JSON.stringify({ type: 'PC', value: $("#cbBU").val() }),
        dataType: 'Json',
        contentType: "Application/json",

        success: function (value) {
            debugger;
            var jData = value

            var ddlPC = jData.Table;

            $("#cbPC").empty();
            for (var i = 0; i < ddlPC.length; i++) {

                $("#cbPC").append($("<option></option>").val(ddlPC[i].pCatId).html(ddlPC[i].descrip));
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

function Get_PG_Values() {
    $.ajax({

        async: false,
        cache: false,
        type: "POST",
        url: "/SAP/ItemMaster/Get_BindDDLList",
        data: JSON.stringify({ type: 'PG', value: $("#cbBU").val() }),
        dataType: 'Json',
        contentType: "Application/json",

        success: function (value) {
            debugger;
            var jData = value

            var ddlPG = jData.Table;

            $("#cbPG").empty();
            for (var i = 0; i < ddlPG.length; i++) {

                $("#cbPG").append($("<option></option>").val(ddlPG[i].pGrpId).html(ddlPG[i].descrip));
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

function Get_PL_Values() {
    $.ajax({

        async: false,
        cache: false,
        type: "POST",
        url: "/SAP/ItemMaster/Get_BindDDLList",
        data: JSON.stringify({ type: 'PL', value: $("#cbPC").val() }),
        dataType: 'Json',
        contentType: "Application/json",

        success: function (value) {
            debugger;
            var jData = value

            var ddlPL = jData.Table;

            $("#cbPL").empty();
            for (var i = 0; i < ddlPL.length; i++) {

                $("#cbPL").append($("<option></option>").val(ddlPL[i].plCode).html(ddlPL[i].descrip));
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

$(document).ready(function () {

    var ItemMaster_obj = new ItemMaster();
    ItemMaster_obj.Init();

});