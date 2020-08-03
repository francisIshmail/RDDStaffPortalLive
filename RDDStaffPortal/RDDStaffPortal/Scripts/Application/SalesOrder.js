﻿var SalesOrder = function () { };  //Class

var ItemDetails = new Array();
var update_Row_Flag = false;
var _Post_To_SAP = 'N';
var _SO_ID = 0;

SalesOrder.prototype = {

    Init: function () {

        SalesOrder.prototype.ControlInit();
        SalesOrder.prototype.ClickEvent();

    },

    ControlInit: function () {

        $('#DBName,#cbRDDProject,#cbBusinessType,#cbInvPayTerm,#cbCustPayTerm,#cbSalesEmp,#cbPayMth1,#cbPayMth2,#cbCur1,#cbCur2,#cbWhs,#cbTax,#cbopg').select2({
            theme: "bootstrap",
            allowClear: true,
            


        });
        $(".datepicker").datetimepicker({

            format: 'DD/MM/YYYY'
        });

        RedDot_NewDate("#txtPostingDate");

        this.BindGrid();
    },

    BindGrid: function (_ItemDetails, ItemDetails) {
        debugger;
        FieldHide = ['pvlineid', 'taxrate'];
        FieldName = ['SrNo', 'pvlineid', 'itemcode', 'description', 'qty', 'price', 'dis', 'taxcode', 'taxrate', 'total', 'whscode', 'qtyinwhs', 'qtyaval', 'opgrefalpha', 'gp', 'gpper'];

        if (update_Row_Flag == true) {

            arr = _ItemDetails;
            if (arr != null && arr.length != 0) {
                var i = 0;
                //while (arr.length > i) {
                var Row_Index_Sr_No = parseInt(arr[0][FieldName[0]]) - 1;
                var tr = $("#Ist").clone();
                var k = 0;
                var l1 = tr.find(".Abcd").length;
                while (l1 > k) {
                    $("#Ibody").find(".reddotTableRow")[Row_Index_Sr_No].children[k].textContent = arr[0][FieldName[k]];


                    k++;
                }
            }
            update_Row_Flag = false;

            return;
        }

        $("#Ist").show();

        //$("div#Ist").not(':first').remove();
        arr = _ItemDetails;
        if (arr != null && arr.length != 0) {
            var i = 0;
            //while (arr.length > i) {
            var tr = $("#Ist").clone();

            var k = 0;
            var l1 = tr.find(".Abcd").length;
            while (l1 > k) {
                tr.find(".Abcd")[k].textContent = arr[FieldName[k]];


                k++;
            }
            tr.find(".Abcd")[0].textContent = ItemDetails.length;
            $("#Ibody").append(tr);
            // i++;
            // }

            if (ItemDetails.length == 1) {
                $("#Ist")[0].remove();
            }

        } else {
            $("#Ist").hide();
            // RedDotAlert_Error("No Record Found");
        }



    },

    AddItemClearControls: function () {

        $("[id$=uxTrowindex]").val('');
        $("[id$=txtItem]").val('');
        $("[id$=txtDescr]").val('');
        $("[id$=txtQt]").val('');
        $("[id$=txtUnitPrice]").val('');
        $("[id$=cbTax]").val('').trigger('change');
        $("[id$=txtTaxRate]").val('');
        $("[id$=txtDisc]").val('0.00');
        $("[id$=txtTot]").val('');

        $("[id$=cbWhs]").val('').trigger('change');
        $("[id$=txtQtyWhs]").val('');
        $("[id$=txtQtAval]").val('');
        $("[id$=cbopg]").empty();
        $("[id$=cbopg]").val('').trigger('change');
        $("[id$=txt_GP]").val('');
        $("[id$=txt_GPPer]").val('');

        $("[id$=btn_AddRow]").text("Add");
        //$("[id$=btn_DelRow]").attr("disabled", true);
        //$("[id$=btn_DelRow]").addClass("disabled");
    },

    ClickEvent: function () {

        $("[id$=btn_Cancel]").click(function () {
            $('#ItemDetailModalPopPup').modal('hide');
        });

        $("[id$=btn_Clear]").click(function () {
            SalesOrder.prototype.AddItemClearControls();
        });

        $('#Ibody').on('click', "[id$=Grid_Edit]", function (event) {
            debugger;
            var tr = $(this).closest("#Ist");

            //FieldName = ['SrNo', 'pvlineid', 'itemcode', 'description', 'qty', 'price', 'dis', 'taxcode', 'taxrate', 'total', 'whscode', 'qtyinwhs', 'qtyaval', 'opgrefalpha', 'gp', 'gpper'];

            $("[id$=uxTrowindex]").val(tr.find(".Abcd").eq(0).text());
            $("[id$=txtItem]").val(tr.find(".Abcd").eq(2).text());
            $("[id$=txtDescr]").val(tr.find(".Abcd").eq(3).text());
            $("[id$=txtQt]").val(tr.find(".Abcd").eq(4).text());
            $("[id$=txtUnitPrice]").val(tr.find(".Abcd").eq(5).text());
            $("[id$=cbTax]").val(tr.find(".Abcd").eq(7).text());
            $("[id$=txtTaxRate]").val(tr.find(".Abcd").eq(8).text());
            $("[id$=txtDisc]").val(tr.find(".Abcd").eq(6).text());
            $("[id$=txtTot]").val(tr.find(".Abcd").eq(9).text());

            $("[id$=cbWhs]").val(tr.find(".Abcd").eq(10).text());
            $("[id$=txtQtyWhs]").val(tr.find(".Abcd").eq(11).text());
            $("[id$=txtQtAval]").val(tr.find(".Abcd").eq(12).text());

            $("[id$=txt_GP]").val(tr.find(".Abcd").eq(14).text());
            $("[id$=txt_GPPer]").val(tr.find(".Abcd").eq(15).text());

            $.ajax({
                async: false,
                cache: false,
                type: "POST",
                url: "/SAP/SalesOrder/Get_ActiveOPGSelloutList",
                data: JSON.stringify({ basedb: 'SAPAE', rebatedb: $("[id$=DBName]").val(), itemcode: tr.find(".Abcd").eq(2).text() }),
                dataType: 'Json',
                contentType: "Application/json",
                success: function (value) {
                    var jData = value;
                    debugger;

                    var ddlopgSelloutList = jData.Table;

                    $("[id$=cbopg]").empty();
                    for (var i = 0; i < ddlopgSelloutList.length; i++) {

                        $("[id$=cbopg]").append($("<option></option>").val(ddlopgSelloutList[i].OPGID).html(ddlopgSelloutList[i].OPGID));
                    }

                    $("[id$=cbopg]").val(tr.find(".Abcd").eq(13).text()).trigger('change');

                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });

            $("[id$=btn_AddRow]").text("Update");
            $('#ItemDetailModalPopPup').modal('show');
        });

        $('#Ibody').on('click', "[id$=Grid_Delete]", function (event) {
            debugger;
            var tr = $(this).closest("#Ist");
            var Row_Index = tr.find(".Abcd").eq(0).text();

            ItemDetails.splice(Row_Index - 1, 1);
            tr.remove()

            var k = 1;
            $(".SalesDetail").each(function () {
               
                $(this).find(".Abcd").eq(0).text(k);
                k++;

            });

            if (ItemDetails.length > 0) {

                Get_Calculation();
            }
            else {
                $("[id$=txtTotBefTax]").val('0.00');
                $("[id$=txtTotalTax]").val('0.00');
                $("[id$=txtTotal]").val('0.00');
                $("[id$=txtGP]").val('0.00');
                $("[id$=txtGPPer]").val('0.00');
            }

            $('#uxTrowindex').val('');
        });

        $("[id$=DBName]").change(function () {
            try {
                // debugger;
                if (($("[id$=DBName]").val() != "Select DB") && ($("[id$=DBName]").val() != "") && ($("[id$=DBName]").val() != "0")) {
                    $.ajax({


                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/SalesOrder/Get_BindDDLList",
                        data: JSON.stringify({ dbname: $("#DBName").val() }),
                        dataType: 'Json',
                        contentType: "Application/json",

                        success: function (value) {
                            //debugger;
                            var jData = value

                            var ddlRDDProject = jData.Table;
                            var ddlBusinesType = jData.Table1;
                            var ddlInvPayTerm = jData.Table2;
                            var ddlCustPayTerm = jData.Table3;
                            var ddlSalesEmp = jData.Table4;
                            var ddlPayMethod = jData.Table5;
                            var ddlCurrency = jData.Table6;
                            var ddlWhsCode = jData.Table7;
                            var ddlTaxCode = jData.Table8;
                            var ddlDocStatus = jData.Table9;
                            var ddlAprStatus = jData.Table10;

                            $("[id$=cbRDDProject]").empty();
                            for (var i = 0; i < ddlRDDProject.length; i++) {

                                $("[id$=cbRDDProject]").append($("<option></option>").val(ddlRDDProject[i].Code).html(ddlRDDProject[i].Descr));
                            }

                            $("[id$=cbBusinessType]").empty();
                            for (var i = 0; i < ddlBusinesType.length; i++) {

                                $("[id$=cbBusinessType]").append($("<option></option>").val(ddlBusinesType[i].Code).html(ddlBusinesType[i].Descr));
                            }

                            $("[id$=cbInvPayTerm]").empty();
                            for (var i = 0; i < ddlInvPayTerm.length; i++) {

                                $("[id$=cbInvPayTerm]").append($("<option></option>").val(ddlInvPayTerm[i].Code).html(ddlInvPayTerm[i].Descr));
                            }
                            $("[id$=cbCustPayTerm]").empty();
                            for (var i = 0; i < ddlCustPayTerm.length; i++) {

                                $("[id$=cbCustPayTerm]").append($("<option></option>").val(ddlCustPayTerm[i].Code).html(ddlCustPayTerm[i].Descr));
                            }
                            $("[id$=cbSalesEmp]").empty();
                            for (var i = 0; i < ddlSalesEmp.length; i++) {

                                $("[id$=cbSalesEmp]").append($("<option></option>").val(ddlSalesEmp[i].Code).html(ddlSalesEmp[i].Descr));
                            }
                            $("[id$=cbPayMth1]").empty();
                            for (var i = 0; i < ddlPayMethod.length; i++) {

                                $("[id$=cbPayMth1]").append($("<option></option>").val(ddlPayMethod[i].Code).html(ddlPayMethod[i].Descr));
                            }
                            $("[id$=cbPayMth2]").empty();
                            for (var i = 0; i < ddlPayMethod.length; i++) {

                                $("[id$=cbPayMth2]").append($("<option></option>").val(ddlPayMethod[i].Code).html(ddlPayMethod[i].Descr));
                            }
                            $("[id$=cbCur1]").empty();
                            for (var i = 0; i < ddlCurrency.length; i++) {

                                $("[id$=cbCur1]").append($("<option></option>").val(ddlCurrency[i].Code).html(ddlCurrency[i].Descr));
                            }
                            $("[id$=cbCur2]").empty();
                            for (var i = 0; i < ddlCurrency.length; i++) {

                                $("[id$=cbCur2]").append($("<option></option>").val(ddlCurrency[i].Code).html(ddlCurrency[i].Descr));
                            }
                            $("[id$=cbCur2]").empty();
                            for (var i = 0; i < ddlCurrency.length; i++) {

                                $("[id$=cbCur2]").append($("<option></option>").val(ddlCurrency[i].Code).html(ddlCurrency[i].Descr));
                            }

                            $("[id$=cbWhs]").empty();
                            for (var i = 0; i < ddlWhsCode.length; i++) {

                                $("[id$=cbWhs]").append($("<option></option>").val(ddlWhsCode[i].Code).html(ddlWhsCode[i].Descr));
                            }
                            $("[id$=cbTax]").empty();
                            for (var i = 0; i < ddlTaxCode.length; i++) {

                                $("[id$=cbTax]").append($("<option></option>").val(ddlTaxCode[i].Code).html(ddlTaxCode[i].Descr));
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
            }
            catch (Error) {
                alert(Error);
            }

        });

        $("[id$=txtCardName]").autocomplete({

            source: function (request, response) {

                //debugger;

                if ($("[id$=DBName]").val() == "Select DB" || $("[id$=DBName]").val() == "" || $("[id$=DBName]").val() == "0") {
                    alert('Please Select Company DataBase ');
                    $("[id$=txtCardName]").val('');
                    $("[id$=txtCardCode]").val('');
                    return;
                }
                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "/SAP/SalesOrder/GetCustomers",
                    data: JSON.stringify({ prefix: request.term, dbname: $("#DBName").val(), field: 'cardname' }),
                    dataType: 'Json',
                    contentType: "Application/json",
                    success: function (data) {
                        //debugger;

                        response($.map(data, function (item) {
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
                //debugger;
                $("[id$=txtCardName]").val(i.item.label);
                $("[id$=txtCardCode]").val(i.item.val);

                get_Customer_Due(i.item.val)


            },
            minLength: 1
        });

        $("[id$=txtItem]").autocomplete({


            source: function (request, response) {

                debugger;

                if (($("[id$=DBName]").val() == "Select DB") || ($("[id$=DBName]").val() == "") || ($("[id$=DBName]").val() == "0")) {
                    alert('Please Select Company DataBase ');
                    $("[id$=txtItem]").val('');
                    $("[id$=txtDescr]").val('');
                    return;
                }

                if (($("[id$=txtCardCode]").val() == "")) {
                    alert('Please Select Customer... ');
                    $("[id$=txtItem]").val('');
                    $("[id$=txtDescr]").val('');
                    return;
                }

                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "/SAP/SalesOrder/GetItemList",
                    data: JSON.stringify({ prefix: request.term, dbname: $("#DBName").val() }),
                    dataType: 'Json',
                    contentType: "Application/json",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data, function (item) {
                            return {
                                label: item.split('#')[0],
                                val: item.split('#')[1],
                                whs: item.split('#')[2],
                                taxcode: item.split('#')[3],
                                whsqty: item.split('#')[4],
                                actualqty: item.split('#')[5],
                                taxrate: item.split('#')[6]
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
                debugger;

                $("[id$=txtItem]").val(i.item.label);
                $("[id$=txtDescr]").val(i.item.val);
                $("[id$=cbWhs]").val(i.item.whs).trigger('change');
                $("[id$=cbTax]").val(i.item.taxcode).trigger('change');
                $("[id$=txtQtyWhs]").val(i.item.whsqty);
                $("[id$=txtQtAval]").val(i.item.actualqty);
                $("[id$=txtTaxRate]").val(i.item.taxrate);

                $.ajax({
                    async: false,
                    cache: false,
                    type: "POST",
                    url: "/SAP/SalesOrder/Get_ActiveOPGSelloutList",
                    data: JSON.stringify({ basedb: 'SAPAE', rebatedb: $("[id$=DBName]").val(), itemcode: $("[id$=txtItem]").val() }),
                    dataType: 'Json',
                    contentType: "Application/json",
                    success: function (value) {
                        var jData = value;
                        debugger;

                        var ddlopgSelloutList = jData.Table;

                        $("[id$=cbopg]").empty();
                        for (var i = 0; i < ddlopgSelloutList.length; i++) {

                            $("[id$=cbopg]").append($("<option></option>").val(ddlopgSelloutList[i].OPGID).html(ddlopgSelloutList[i].OPGID));
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
            minLength: 1
        });

        $("[id$=cbWhs]").change(function () {
            debugger;
            try {
                if ($("[id$=cbWhs]").val() != '' && $("[id$=DBName]").val() != "Select DB" && $("[id$=DBName]").val() != "0") {
                    debugger;
                    $.ajax({
                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/SalesOrder/Get_WarehouseQty",
                        data: JSON.stringify({ itemcode: $("[id$=txtItem]").val(), whscode: $("[id$=cbWhs]").val(), dbname: $("[id$=DBName]").val() }),
                        dataType: 'Json',
                        contentType: "Application/json",
                        success: function (value) {
                            debugger;
                            var jData = value.Table;
                            var QtyInWhs = jData[0].OnHand;
                            var QtyAvl = jData[0].ActalQty;

                            $("[id$=txtQtyWhs]").val(QtyInWhs);
                            $("[id$=txtQtAval]").val(QtyAvl);

                            if ($("[id$=txtItem]").val() != '' && $("[id$=cbWhs]").val() != '' && $("[id$=cbWhs]").val() != '0' && $("[id$=cbopg]").val() != '' && $("[id$=txtQt]").val() != '' && $("[id$=txtUnitPrice]").val() != '') {
                                Get_GP_And_GPPer();
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
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=cbTax]").change(function () {
            try {
                if ($("[id$=cbTax]").val() != '' && $("[id$=DBName]").val() != "Select DB" && $("[id$=DBName]").val() != "0") {
                    $.ajax({
                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/SalesOrder/Get_TaxCodeRate",
                        data: JSON.stringify({ taxcode: $("[id$=cbTax]").val(), dbname: $("[id$=DBName]").val() }),
                        dataType: 'Json',
                        contentType: "Application/json",

                        success: function (value) {
                            debugger;
                            var jData = value.Table;
                            var TaxRate = jData[0].rate;

                            $("[id$=txtTaxRate]").val(TaxRate);

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

        $("[id$=txtUnitPrice],[id$=txtQt],[id$=txtDisc]").change(function () {
            debugger;
            try {
                if ($("[id$=txtQt]").val() != '' && $("[id$=txtUnitPrice]").val() != '' && $("[id$=DBName]").val() != "Select DB" && $("[id$=DBName]").val() != "0") {

                    var Qty = parseFloat($("[id$=txtQt]").val());
                    var UnitPrice = parseFloat($("[id$=txtUnitPrice]").val());
                    var DisPer = 0.00;
                    var Total = 0.00;

                    Total = Qty * UnitPrice;

                    if ($("[id$=txtDisc]").val() != '') {
                        DisPer = parseFloat($("[id$=txtDisc]").val());
                    }

                    Total = parseFloat(Total - (Total * DisPer / 100));
                    $("[id$=txtTot]").val(Total);

                    if ($("[id$=txtItem]").val() != '' && $("[id$=cbWhs]").val() != '' && $("[id$=cbWhs]").val() != '0' && $("[id$=cbopg]").val() != '' && $("[id$=txtQt]").val() != '' && $("[id$=txtUnitPrice]").val() != '') {
                        Get_GP_And_GPPer();
                    }
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=btn_AddRow]").click(function () {
            try {

                debugger;
                var _SOID = 0;
                if (Validate_AddRow() == true) {


                    if ($("[id$=btn_AddRow]").text() == 'Add') {
                        var _ItemDetails = {};

                        _ItemDetails["SrNo"] = "0";
                        _ItemDetails["pvlineid"] = "0";
                        _ItemDetails["itemcode"] = $("[id$=txtItem]").val();
                        _ItemDetails["description"] = $("[id$=txtDescr]").val();
                        _ItemDetails["qty"] = $("[id$=txtQt]").val();
                        _ItemDetails["price"] = $("[id$=txtUnitPrice]").val();
                        _ItemDetails["taxcode"] = $("[id$=cbTax]").val();
                        _ItemDetails["taxrate"] = $("[id$=txtTaxRate]").val();

                        if ($("[id$=txtDisc]").val() == '')
                            _ItemDetails["dis"] = '0.00';
                        else
                            _ItemDetails["dis"] = $("[id$=txtDisc]").val();

                        _ItemDetails["total"] = $("[id$=txtTot]").val();
                        _ItemDetails["whscode"] = $("[id$=cbWhs]").val();
                        _ItemDetails["qtyinwhs"] = $("[id$=txtQtyWhs]").val();
                        _ItemDetails["qtyaval"] = $("[id$=txtQtAval]").val();
                        _ItemDetails["opgrefalpha"] = $("[id$=cbopg]").val();

                        if ($("[id$=txt_GP]").val() == '') {
                            _ItemDetails["gp"] = '0.00';
                            _ItemDetails["gpper"] = '0.00';
                        }
                        else {
                            _ItemDetails["gp"] = $("[id$=txt_GP]").val();
                            _ItemDetails["gpper"] = $("[id$=txt_GPPer]").val();
                        }

                        ItemDetails.push(_ItemDetails);
                        SalesOrder.prototype.BindGrid(_ItemDetails, ItemDetails);
                    }
                    else {

                        update_Row_Flag = true;
                        var _ItemDetails = {};

                        var index = $("[id$=uxTrowindex]").val();
                        var parameter = ItemDetails[index - 1];

                        $("[id$=uxTrowindex]").val(index);


                        parameter.SrNo = index;
                        parameter.pvlineid = index - 1;
                        parameter.itemcode = $("[id$=txtItem]").val();
                        parameter.description = $("[id$=txtDescr]").val();
                        parameter.qty = $("[id$=txtQt]").val();
                        parameter.price = $("[id$=txtUnitPrice]").val();
                        parameter.taxcode = $("[id$=cbTax]").val();
                        parameter.taxrate = $("[id$=txtTaxRate]").val();

                        if ($("[id$=txtDisc]").val() == '')
                            parameter.dis = '0.00';
                        else
                            parameter.dis = $("[id$=txtDisc]").val();

                        parameter.total = $("[id$=txtTot]").val();
                        parameter.whscode = $("[id$=cbWhs]").val();
                        parameter.qtyinwhs = $("[id$=txtQtyWhs]").val();
                        parameter.qtyaval = $("[id$=txtQtAval]").val();
                        parameter.opgrefalpha = $("[id$=cbopg]").val();

                        if ($("[id$=txt_GP]").val() == '') {
                            parameter.gp = '0.00';
                            parameter.gpper = '0.00';
                        }
                        else {
                            parameter.gp = $("[id$=txt_GP]").val();
                            parameter.gpper = $("[id$=txt_GPPer]").val();
                        }

                        ItemDetails[index - 1] = parameter;
                        _ItemDetails[0] = parameter;

                        SalesOrder.prototype.BindGrid(_ItemDetails, ItemDetails);
                        SalesOrder.prototype.AddItemClearControls();

                        $("[id$=btn_AddRow]").text('Add Row');

                        $("[id$=btn_DelRow]").attr("disabled", true);
                        $("[id$=btn_DelRow]").addClass("disabled");

                    }

                    if (ItemDetails.length > 0) {

                        Get_Calculation();
                    }
                    SalesOrder.prototype.AddItemClearControls();

                }
            }
            catch (Error) {
                alert(Error);
            }

        });

        $("[id$=btn_MainSave1]").click(function () {
            try {
                debugger;
                if (_Post_To_SAP == 'N') {
                    if (Validate() == true) {

                        var RDD_OSOR = {

                            SO_ID: _SO_ID,
                            Doc_Object:17 ,
                            Base_Obj: 0,
                            Base_ID: 0,
                            DBName: $("[id$=DBName]").val(),
                            PostingDate: GetSqlDateformat($("[id$=txtPostingDate]").val()),
                            DeliveryDate: GetSqlDateformat($("[id$=txtDelDate]").val()),
                            DocStatus: $("[id$=txtDocStatus]").val(),
                            AprovedBy: $("[id$=txtApprvBy]").val(),
                            CreatedBy: $("[id$=txtCreatedBy]").val(),
                            CardCode: $("[id$=txtCardCode]").val(),
                            CardName: $("[id$=txtCardName]").val(),
                            RefNo: $("[id$=txtRefNum]").val(),
                            RDD_Project: $("[id$=cbRDDProject]").val(),
                            BusinesType: $("[id$=cbBusinessType]").val(),
                            InvPayTerms: $("[id$=cbInvPayTerm]").val(),
                            CustPayTerms: $("[id$=cbCustPayTerm]").val(),
                            Forwarder: $("[id$=txt_ForworderDet]").val(),
                            SalesEmp: $("[id$=cbSalesEmp]").val(),
                            Pay_Method_1: $("[id$=cbPayMth1]").val(),
                            Rcpt_check_No_1: $("[id$=txtCheck1]").val(),
                            Rcpt_check_Date_1: $("[id$=txtChkDate1]").val() != '' ? GetSqlDateformat($("[id$=txtChkDate1]").val()): '' ,
                            Remarks_1: $("[id$=txtRemarks1]").val(),
                            Curr_1: $("[id$=cbCur1]").val(),
                            Amount_1: $("[id$=txtAmount1]").val(),
                            Pay_Method_2: $("[id$=cbPayMth2]").val(),
                            Rcpt_check_No_2: $("[id$=txtCheck2]").val(),
                            Rcpt_check_Date_2: GetSqlDateformat($("[id$=txtChkDate2]").val()),
                            Remarks_2: $("[id$=txtRemarks2]").val(),
                            Curr_2: $("[id$=cbCur2]").val(),
                            Amount_2: $("[id$=txtAmount2]").val(),

                            Total_Bef_Tax: $("[id$=txtTotBefTax]").val(),
                            Total_Tx: $("[id$=txtTotalTax]").val(),
                            DocTotal: $("[id$=txtTotal]").val(),
                            GP: $("[id$=txtGP]").val(),
                            GP_Per: $("[id$=txtGPPer]").val(),
                            Remarks: $("[id$=txtRemarks]").val(),
                            Validate_Status: 'No' ,
                            Post_SAP: 'N'
                           
                              
                        };
                    }
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=btn_MainSave]").click(function () {
            try {
                debugger
                if (_Post_To_SAP == 'N') {
                    if (Validate() == true) {
                        var SalesOrder = new Array();
                        var SalesOrderDetail = new Array();
                        var SalesOrder_Obj = new Object();

                        SalesOrder_Obj['SO_ID'] = _SO_ID;
                        SalesOrder_Obj['Doc_Object'] = 17;
                        SalesOrder_Obj['Base_Obj'] = 0;
                        SalesOrder_Obj['Base_ID'] = 0;
                        SalesOrder_Obj['DBName'] = $("[id$=DBName]").val();
                        SalesOrder_Obj['PostingDate'] = GetSqlDateformat($("[id$=txtPostingDate]").val());
                        SalesOrder_Obj['DeliveryDate'] = GetSqlDateformat($("[id$=txtDelDate]").val());
                        SalesOrder_Obj['DocStatus'] = $("[id$=txtDocStatus]").val();

                        if ($("[id$=txtApprvBy]").val() == '')
                            SalesOrder_Obj['AprovedBy'] = '';
                        else
                            SalesOrder_Obj['AprovedBy'] = $("[id$=txtApprvBy]").val();

                        SalesOrder_Obj['CreatedBy'] = $("[id$=txtCreatedBy]").val();
                        SalesOrder_Obj['CardCode'] = $("[id$=txtCardCode]").val();
                        SalesOrder_Obj['CardName'] = $("[id$=txtCardName]").val();
                        SalesOrder_Obj['RefNo'] = $("[id$=txtRefNum]").val();
                        SalesOrder_Obj['RDD_Project'] = $("[id$=cbRDDProject]").val();
                        SalesOrder_Obj['BusinesType'] = $("[id$=cbBusinessType]").val();
                        SalesOrder_Obj['InvPayTerms'] = $("[id$=cbInvPayTerm]").val();
                        SalesOrder_Obj['CustPayTerms'] = $("[id$=cbCustPayTerm]").val();
                        SalesOrder_Obj['Forwarder'] = $("[id$=txt_ForworderDet]").val();
                        SalesOrder_Obj['SalesEmp'] = $("[id$=cbSalesEmp]").val();
                       
                        SalesOrder_Obj['Pay_Method_1'] = $("[id$=cbPayMth1]").val();
                        SalesOrder_Obj['Rcpt_check_No_1'] = $("[id$=txtCheck1]").val();

                        if ($("[id$=txtChkDate1]").val() != '')
                            SalesOrder_Obj['Rcpt_check_Date_1'] = GetSqlDateformat($("[id$=txtChkDate1]").val());
                        //else
                        //    SalesOrder_Obj['Rcpt_check_Date_1'] = GetSqlDateformat($("[id$=txtChkDate1]").val());

                        SalesOrder_Obj['Remarks_1'] = $("[id$=txtRemarks1]").val();
                        SalesOrder_Obj['Curr_1'] = $("[id$=cbCur1]").val();
                        SalesOrder_Obj['Amount_1'] = $("[id$=txtAmount1]").val();
                        SalesOrder_Obj['Pay_Method_2'] = $("[id$=cbPayMth2]").val();
                        SalesOrder_Obj['Rcpt_check_No_2'] = $("[id$=txtCheck2]").val();

                        if ($("[id$=txtChkDate2]").val() != '')
                            SalesOrder_Obj['Rcpt_check_Date_2'] = GetSqlDateformat($("[id$=txtChkDate2]").val());
                        //else
                        //    SalesOrder_Obj['Rcpt_check_Date_2'] = GetSqlDateformat($("[id$=txtChkDate2]").val());

                        SalesOrder_Obj['Remarks_2'] = $("[id$=txtRemarks2]").val();
                        SalesOrder_Obj['Curr_2'] = $("[id$=cbCur2]").val();
                        if ($("[id$=txtAmount2]").val() == '')
                            SalesOrder_Obj['Amount_2'] = '0.00';
                        else
                            SalesOrder_Obj['Amount_2'] = $("[id$=txtAmount2]").val();

                        SalesOrder_Obj['Total_Bef_Tax'] = $("[id$=txtTotBefTax]").val();
                        SalesOrder_Obj['Total_Tx'] = $("[id$=txtTotalTax]").val();
                        SalesOrder_Obj['DocTotal'] = $("[id$=txtTotal]").val();
                        SalesOrder_Obj['GP'] = $("[id$=txtGP]").val();
                        SalesOrder_Obj['GP_Per'] = $("[id$=txtGPPer]").val();
                        SalesOrder_Obj['Remarks'] = $("[id$=txtRemarks]").val();
                        SalesOrder_Obj['Validate_Status'] = 'No';
                        SalesOrder_Obj['Post_SAP'] = 'N';

                        if (_SO_ID == 0) {
                            SalesOrder_Obj['LastUpdatedOn'] = '';
                            SalesOrder_Obj['LastUpdatedBy'] = '';
                        }
                        else {
                            var Dt = new Date();
                            var Dt1 = Dt.getDate();
                            var upDt = GetSqlDateformat(Dt1);

                            SalesOrder_Obj['LastUpdatedOn'] = upDt;
                            SalesOrder_Obj['LastUpdatedBy'] = $("[id$=txtCreatedBy]").val();;
                        }


                        SalesOrder.push(SalesOrder_Obj);
                         //FieldName = [0'SrNo', 1'pvlineid', 2'itemcode', 3'description', 4'qty', 5'price', 6'dis', 7'taxcode', 8'taxrate', 9'total', 10'whscode', 11'qtyinwhs', 12'qtyaval', 13'opgrefalpha', 14'gp', 15'gpper'];

                        var k = 1;
                        $(".SalesDetail").each(function () {
                            //if (!this.rowIndex) return; // skip first row
                            debugger;
                            var SalesOrderDetail_Obj = new Object();
                            SalesOrderDetail_Obj['ItemCode'] = $(this).find(".Abcd").eq(2).text();
                            SalesOrderDetail_Obj['Description'] = $(this).find(".Abcd").eq(3).text();
                            SalesOrderDetail_Obj['Quantity'] = $(this).find(".Abcd").eq(4).text();
                            SalesOrderDetail_Obj['UnitPrice'] = $(this).find(".Abcd").eq(5).text();
                            SalesOrderDetail_Obj['DiscPer'] = $(this).find(".Abcd").eq(6).text();
                            SalesOrderDetail_Obj['TaxCode'] = $(this).find(".Abcd").eq(7).text();
                            SalesOrderDetail_Obj['TaxRate'] = $(this).find(".Abcd").eq(8).text();
                            SalesOrderDetail_Obj['LineTotal'] = $(this).find(".Abcd").eq(9).text();
                            SalesOrderDetail_Obj['WhsCode'] = $(this).find(".Abcd").eq(10).text();
                            SalesOrderDetail_Obj['QtyInWhs'] = $(this).find(".Abcd").eq(11).text();
                            SalesOrderDetail_Obj['QtyAval'] = $(this).find(".Abcd").eq(12).text();
                            SalesOrderDetail_Obj['OpgRefAlpha'] = $(this).find(".Abcd").eq(13).text();

                            if ($(this).find(".Abcd").eq(14).text() == '') {
                                SalesOrderDetail_Obj['GP'] = 0.00;
                                SalesOrderDetail_Obj['GPPer'] = 0.00;
                            }
                            else {
                                SalesOrderDetail_Obj['GP'] = ($(this).find(".Abcd").eq(14).text());
                                SalesOrderDetail_Obj['GPPer'] = ($(this).find(".Abcd").eq(15).text());
                            }

                            SalesOrderDetail_Obj['Base_Obj'] = 0;
                            SalesOrderDetail_Obj['Base_Id'] = 0;
                            SalesOrderDetail_Obj['Base_LinId'] = 0;
                          
                            SalesOrderDetail.push(SalesOrderDetail_Obj);

                           
                        });


                        $.ajax({
                            async: false,
                            cache: false,
                            type: "POST",
                            url: "/SAP/SalesOrder/Save_SalesOrder",
                            data: JSON.stringify({ model: JSON.stringify(SalesOrder), model1: JSON.stringify(SalesOrderDetail), dbname: $("[id$=cbDataBase]").val() }),
                            dataType: 'Json',
                            contentType: "Application/json",

                            success: function (value) {
                                debugger;
                                var jData = value;

                                if (jData.table[0].Result == 'True') {

                                    RedDotAlert_Success(jData.table[0].Message + " Trans ID-" + jData.table[1].Message);
                                    ClearControls();
                                   

                                }
                                else {
                                    RedDotAlert_Error(jData.table[0].Message);
                                }
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
                else {
                    alert('This Document already Posted into SAP B1...Can not Update.');
                    return;
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=btn_MainClear]").click(function () {
            ClearControls();
        });

        $("[id$=btn_MainSearch]").click(function () {
            $('#SORSearchModalPopPup').modal('show');
        });

        
    },
}

$(document).ready(function () {

    var SalesOrder_obj = new SalesOrder();
    SalesOrder_obj.Init();

});

function get_Customer_Due(customercode) {
    var s = customercode;

    $.ajax({

        async: false,
        cache: false,
        type: "POST",
        url: "/SAP/SalesOrder/Get_CustomersDue_Info",
        data: JSON.stringify({ dbname: $("#DBName").val(), cardcode: s }),
        dataType: 'Json',
        contentType: "Application/json",

        success: function (data) {
            debugger;
            var theString = data;

            var arySummary = new Array();
            arySummary = theString[0].split("#");

            //alert(arySummary[0]);

            $("[id$=txtCreditLimit]").val(arySummary[0]);
            $("[id$=txt_0_30]").val(arySummary[1]);

            $("[id$=txt_31_45]").val(arySummary[2]);
            $("[id$=txt_46_60]").val(arySummary[3]);
            $("[id$=txt_61_90]").val(arySummary[4]);
            $("[id$=txt_91_Above]").val(arySummary[5]);
            $("[id$=txtTrnStatus]").val(arySummary[6]);
            $("[id$=txtCLStatus]").val(arySummary[7]);
            $("[id$=cbCustPayTerm]").val(arySummary[8]).trigger('change');

            var db = $("[id$=cbDataBase]").val();

            Get_DeliveryDate(arySummary[8], db);

            if (arySummary[6] == "Active") {
                $("[id$=txtTrnStatus]").attr('style', 'font-weight: bold; text-align:center;color:green;');
                //$("[id$=txtTrnStatus]").attr('style', 'font-size: 14px; font-weight: bold; text-align:center;background-color:Green;color:White;');
            }
            else {
                $("[id$=txtTrnStatus]").attr('style', 'font-weight: bold; text-align:center;color:red;');
                //    $("[id$=txtTrnStatus]").attr('style', 'font-size: 14px; font-weight: bold; text-align:center;background-color:red;color:White;');
            }

            if (arySummary[7] == "Ok") {
                $("[id$=txtCLStatus]").attr('style', 'font-weight: bold; text-align:center;color:green;');
            }
            else {
                $("[id$=txtCLStatus]").attr('style', 'font-weight: bold; text-align:center;color:red;');
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

function Get_DeliveryDate(groupnum, dbname) {
    try {

        var obj = GetSqlDateformat($("[id$=txtPostingDate]").val());

        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "/SAP/SalesOrder/Get_PayTerms_Days",
            data: JSON.stringify({ dbname: $("#DBName").val(), groupnum: groupnum }),
            dataType: 'Json',
            contentType: "Application/json",
            success: function (data) {
                debugger;
                var theString = data;

                var ExtrraDays = new Array();
                ExtrraDays = theString[0].split("#");

                var ExtraDays = parseInt(ExtrraDays[0]);

                var date = new Date(obj);
                var DeliveryDate = new Date(date);

                DeliveryDate.setDate(DeliveryDate.getDate() + ExtraDays);

                $("[id$=txtDelDate]").val(DateToStringformat(DeliveryDate));



            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }


        });

    }
    catch (Error) {
        alert(Error);
    }

}

function Get_GP_And_GPPer() {
    try {

        $.ajax({
            async: false,
            cache: false,
            type: "POST",
            url: "/SAP/SalesOrder/Get_GPAndGPPer",
            data: JSON.stringify({ dbname: $("#DBName").val(), itemcode: $("[id$=txtItem]").val(), warehouse: $("[id$=cbWhs]").val(), qtysell: $("[id$=txtQt]").val(), pricesell: $("[id$=txtUnitPrice]").val(), curr: 'USD', opgrebateid: $("[id$=cbopg]").val() }),
            dataType: 'Json',
            contentType: "Application/json",

            success: function (value) {
                debugger;
                var jData = value.Table;
                var GP = jData[0].gpvalrowusd;
                var GPPer = jData[0].gppercrowusd;

                $("[id$=txt_GP]").val(GP);
                $("[id$=txt_GPPer]").val(GPPer);

            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });

    }
    catch (Error) {
        alert(Error);
    }
}

function Validate() {
    debugger;
    try {

        if ($("[id$=DBName]").val() == '0' || $("[id$=DBName]").val() == '' || $("[id$=DBName]").val() == '--Select DB--') {
            RedDotAlert_Error("Select SAP Database...");
            return false;
        }

        if ($("[id$=txtCardCode]").val() == '') {
            RedDotAlert_Error("Select CardName...");
            return false;
        }

        if ($("[id$=txtRefNum]").val() == '') {
            RedDotAlert_Error("Enter Reference No....");
            return false;
        }

        if ($("[id$=cbRDDProject]").val() == '0' || $("[id$=cbRDDProject]").val() == '' || $("[id$=cbRDDProject]").val() == 'Select RDD Project') {
            RedDotAlert_Error("Select Project...");
            return false;
        }

        if ($("[id$=cbBusinessType]").val() == '0' || $("[id$=cbBusinessType]").val() == '' || $("[id$=cbBusinessType]").val() == 'Select Business Type') {
            RedDotAlert_Error("Select Business Type...");
            return false;
        }

        if ($("[id$=cbInvPayTerm]").val() == '0' || $("[id$=cbInvPayTerm]").val() == '' || $("[id$=cbInvPayTerm]").val() == 'Select Inv PayTerm') {
            RedDotAlert_Error("Select Invoice Payment Terms...");
            return false;
        }

        if ($("[id$=cbCustPayTerm]").val() == '0' || $("[id$=cbCustPayTerm]").val() == '' || $("[id$=cbCustPayTerm]").val() == 'Select PayTerms') {
            RedDotAlert_Error("Select Customer Payment Terms...");
            return false;
        }

        if ($("[id$=cbSalesEmp]").val() == '-1' || $("[id$=cbCustPayTerm]").val() == '' || $("[id$=cbCustPayTerm]").val() == '-No Sales Employee-') {
            RedDotAlert_Error("Select Sales Employee...");
            return false;
        }

        //---This is for payment method 1

        if ($("[id$=cbPayMth1]").val() == '0' || $("[id$=cbPayMth1]").val() == '' || $("[id$=cbPayMth1]").val() == '--Select--') {
            RedDotAlert_Error("Select Payment Method 1...");
            return false;
        }

        if ($("[id$=txtCheck1]").val() == '') {
            RedDotAlert_Error("Enter Check/Rceipt No 1...");
            return false;
        }

        if ($("[id$=txtChkDate1]").val() == '') {
            RedDotAlert_Error("Select Check/Rceipt Date 1...");
            return false;
        }

        if ($("[id$=txtRemarks1]").val() == '') {
            RedDotAlert_Error("Enter Remark 1...");
            return false;
        }

        if ($("[id$=cbCur1]").val() == '0' || $("[id$=cbCur1]").val() == '' || $("[id$=cbCur1]").val() == '--Select--') {
            RedDotAlert_Error("Select Currency 1...");
            return false;
        }

        if ($("[id$=txtAmount1]").val() == '') {
            RedDotAlert_Error("Enter Amount 1...");
            return false;
        }

        //----This is for Grid
        if (ItemDetails.length <= 0) {
            RedDotAlert_Error("Add Items in Grid...");
            return false;
        }

        //---This is for payment method 2

        if ($("[id$=cbPayMth2]").val() != '0' && $("[id$=cbPayMth2]").val() != '' && $("[id$=cbPayMth2]").val() != '--Select--') {

            if ($("[id$=txtCheck2]").val() == '') {
                RedDotAlert_Error("Enter Check/Rceipt No 2...");
                return false;
            }

            if ($("[id$=txtChkDate2]").val() == '') {
                RedDotAlert_Error("Select Check/Rceipt Date 2...");
                return false;
            }

            if ($("[id$=txtRemarks2]").val() == '') {
                RedDotAlert_Error("Enter Remark 2...");
                return false;
            }

            if ($("[id$=cbCur2").val() == '0' || $("[id$=cbCur2]").val() == '' || $("[id$=cbCur2]").val() == '--Select--') {
                RedDotAlert_Error("Select Currency 2...");
                return false;
            }

            if ($("[id$=txtAmount2]").val() == '') {
                RedDotAlert_Error("Enter Amount 2...");
                return false;
            }

        }
        return true;
    }
    catch (Error) {
        alert(Error);
        return false;
    }
}

function Validate_AddRow() {
    try {

        debugger;

        if ($("[id$=txtItem]").val() == '') {
            //alert("Select Item...");
            RedDotAlert_Error("Select Item...");
            return false;
        }

        if ($("[id$=cbWhs]").val() == '0' || $("[id$=cbWhs]").val() == '') {
            RedDotAlert_Error("Select Warehouse...");
            return false;
        }

        if ($("[id$=txtQtAval]").val() != '') {
            var QtAval = parseInt($("[id$=txtQtAval]").val());
            if (QtAval <= 0) {
                RedDotAlert_Error("Available Quantity should be greater than zero in Selected Warehouse...");
                return false;
            }
        }
        else {
            RedDotAlert_Error("Available Quantity should be greater than zero in Selected Warehouse...");
            return false;
        }

        if ($("[id$=txtQt]").val() != '') {
            var Qty = parseInt($("[id$=txtQt]").val());
            var AvalQty = parseInt($("[id$=txtQtAval]").val());
            if (Qty <= 0 || Qty > AvalQty) {
                RedDotAlert_Error("Quantity should be greater than zero And less than or equal to Available quantity...");
                return false;
            }

        }
        else {
            RedDotAlert_Error("Quantity should be greater than zero And less than or equal to Available quantity...");
            return false;
        }

        if ($("[id$=cbTax]").val() == '0' || $("[id$=cbTax]").val() == '') {
            RedDotAlert_Error("Select Tax Code...");
            return false;
        }

        if ($("[id$=txtUnitPrice]").val() == '') {
            RedDotAlert_Error("Enter Unit Price...");
            return false;
        }

        if ($("[id$=cbopg]").val() == '0' || $("[id$=cbopg]").val() == '') {
            RedDotAlert_Error("Select OPG...");
            return false;
        }


        return true;
    }
    catch (Error) {
        alert(Error);
        return false;
    }
}

function Get_Calculation() {
    try {
        var _TotBefTax = 0.00;
        var _TotTax = 0.00;
        var _Total = 0.00;
        var _GP = 0.00;
        var _GPPer = 0.00;

        for (var i = 0; i < ItemDetails.length; i++) {
            _TotBefTax = _TotBefTax + parseFloat(ItemDetails[i].total);
            _TotTax = _TotTax + (parseFloat(ItemDetails[i].total) * parseFloat(ItemDetails[i].taxrate) / 100);
            _GP = _GP + parseFloat(ItemDetails[i].gp);
            _GPPer = _GPPer + parseFloat(ItemDetails[i].gpper);
        }

        _Total = parseFloat(_TotBefTax) + parseFloat(_TotTax);
        _GPPer = parseFloat(_GPPer) / parseInt(ItemDetails.length);

        $("[id$=txtTotBefTax]").val(parseFloat(_TotBefTax).toFixed(2));
        $("[id$=txtTotalTax]").val(parseFloat(_TotTax).toFixed(2));
        $("[id$=txtTotal]").val(parseFloat(_Total).toFixed(2));
        $("[id$=txtGP]").val(parseFloat(_GP).toFixed(2));
        $("[id$=txtGPPer]").val(parseFloat(_GPPer).toFixed(2));
    }
    catch (n) {
        alert(n)
    }
}

function ClearControls() {
    debugger;
    _SO_ID = 0;
    _Post_To_SAP = 'N';

    //$("[id$=pgHeader]").html('<span>Sales Order</span>');
    $("[id$=DBName]").val('0').trigger('change');

    var Dt = new Date();

    var CurDate = DateToStringformat(Dt);
    $("[id$=txtPostingDate]").val(CurDate);

    Dt.setDate(Dt.getDate() + 30);
    CurDate = DateToStringformat(Dt);
    $("[id$=txtDelDate]").val(CurDate);
    $("[id$=txtDocStatus]").val('Draft');
    $("[id$=txtApprvBy]").val('');

    //$("[id$=txtCreatedBy]").val();
    $("[id$=txtCardCode]").val('');
    $("[id$=txtCardName]").val('');
    $("[id$=txtRefNum]").val('');

    $('#cbRDDProject').html('').select2({ theme: "bootstrap",
        allowClear: true,
        data: [{id: '', text: 'Select RDD Project' }] }).trigger('change');

    $('#cbBusinessType').html('').select2({ theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: 'Select Business Type'}]}).trigger('change');

    $('#cbInvPayTerm').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: 'Select Inv PayTerm' }]
    }).trigger('change');

    $('#cbCustPayTerm').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: 'Select PayTerms' }]
    }).trigger('change');

    $('#cbSalesEmp').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: '-No Sales Employee-' }]
    }).trigger('change');

    $("[id$=txt_ForworderDet]").val('');

    $("[id$=txtCreditLimit]").val('0.00');
    $("[id$=txt_0_30]").val('0.00');
    $("[id$=txt_31_45]").val('0.00');
    $("[id$=txt_46_60]").val('0.00');
    $("[id$=txt_61_90]").val('0.00');
    $("[id$=txt_91_Above]").val('0.00');
    $("[id$=txtTrnStatus]").val('');
    $("[id$=txtCLStatus]").val('');

    $("[id$=txtTrnStatus]").attr('style', 'font-weight: bold; text-align:center;color:none;');
    $("[id$=txtCLStatus]").attr('style', 'font-weight: bold; text-align:center;color:none;');

    $('#cbPayMth1').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: '--Select--' }]
    }).trigger('change');

    $("[id$=txtCheck1]").val('');
    $("[id$=txtChkDate1]").val('');
    $("[id$=txtRemarks1]").val('');
    $('#cbCur1').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: '--Select--' }]
    }).trigger('change');
  
    $("[id$=txtAmount1]").val('');

    $('#cbPayMth2').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: '--Select--' }]
    }).trigger('change');

    $("[id$=txtCheck2]").val();
    $("[id$=txtChkDate2]").val('');
    $("[id$=txtRemarks2]").val('');

    $('#cbCur2').html('').select2({
        theme: "bootstrap",
        allowClear: true,
        data: [{ id: '', text: '--Select--' }]
    }).trigger('change');

    $("[id$=txtAmount2]").val('');

    $("[id$=txtTotBefTax]").val('');
    $("[id$=txtTotalTax]").val('');
    $("[id$=txtTotal]").val('');
    $("[id$=txtGP]").val('');
    $("[id$=txtGPPer]").val('');
    $("[id$=txtRemarks]").val('');

    SalesOrder.prototype.BindGrid({});
    ItemDetails = new Array();

    SalesOrder.prototype.AddItemClearControls();

    $("#btn_MainSave").text("Save");
}

function DateToStringformat(obj) {
    try {
        debugger;
        if (obj != undefined && obj != null) {
            var dt = new Date(obj);
            var _date = dt.getDate();
            var _Month = dt.getMonth();
            if (parseInt(_date) < 10) {
                _date = '0' + _date;
            }
            if (parseInt(_Month) + 1 < 10) {
                _Month = '0' + (parseInt(_Month) + 1);
            }
            else {
                _Month = (parseInt(_Month) + 1)
            }
            SqlDate = _date + '/';
            SqlDate += _Month + '/';
            SqlDate += dt.getFullYear();
            return SqlDate;
        }
    }
    catch (ex) {
        log(ex);
    }
}

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