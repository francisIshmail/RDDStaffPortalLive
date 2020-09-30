var SalesOrder = function () { };  //Class

var ItemDetails = new Array();
var _SO_ID = 0;
var _Post_To_SAP = 'N';
var _LoginUser = '';

SalesOrder.prototype = {

    Init: function () {

        SalesOrder.prototype.ControlInit();
        SalesOrder.prototype.BindAutoCompleteDDL();
        // SalesOrder.prototype.BindItems({});
        SalesOrder.prototype.ClickEvent();
        //        AssetWarranty.prototype.BindItems2({});
        //        AssetWarranty.prototype.PickerEvent();
        //        AssetWarranty.prototype.BindAutoCompleteDDL();
        //        AssetWarranty.prototype.ClickEvent();
        //AssetCapitalization.prototype.BindItems({});
        //SalesOrder.prototype.ClearControls();
        SalesOrder.prototype.AddItemClearControls();
        //AssetMaster.prototype.BindItemsSeq({});


    },
    BindGrid: function (jdata) {
        //Create a HTML Table element.
        var table = $("#gvItem");
        table.find("tr:not(:first)").remove();
        //Get the count of columns.
        var columnCount = jdata.length;
        //Add the data rows.

        for (var i = 0; i < jdata.length; i++) {
            debugger;
            row = $(table[0].insertRow(-1));
            var cell = $("<td />"); cell.html(i + 1); row.append(cell);
            var cell = $("<td />"); cell.html("<a href=" + (i + 1) + "  class='linkTest1'>Edit</a>"); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].itemcode); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].description); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].qty); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].price); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].dis); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].taxcode); row.append(cell);
            var cell = $("<td style='display:none;' />"); cell.html(jdata[i].taxrate); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].total); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].whscode); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].qtyinwhs); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].qtyaval); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].opgrefalpha); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].gp); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].gpper); row.append(cell);


        }

        //         $("#classTable tr").on('click', '.linkTest', function (e) {
        //            debugger;

        //            _SO_ID = $(this).closest('tr').find('td').eq(2).text();

        //        $('#gvItem tr td:eq(1)').click(function () {
        $("#gvItem tr").on('click', '.linkTest1', function (e) {
            debugger;
           // var href = $(this).find("a").attr("href");
            var href = $(this).closest('tr').find('td').eq(1).text();

            if (href) {

//                $("[id$=uxTrowindex]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(0)").html()));
//                $("[id$=txtItem]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(2)").html()));
//                $("[id$=txtDescr]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(3)").html()));
//                $("[id$=txtQt]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(4)").html()));
//                $("[id$=txtUnitPrice]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(5)").html()));
//                $("[id$=txtDisc]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(6)").html()));
//                $("[id$=cbTax]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(7)").html()));
//                $("[id$=txtTaxRate]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(8)").html()));
//                $("[id$=txtTot]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(9)").html()));
//                $("[id$=cbWhs]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(10)").html()));
//                $("[id$=txtQtyWhs]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(11)").html()));
//                $("[id$=txtQtAval]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(12)").html()));
//                $("[id$=txt_GP]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(14)").html()));
//                $("[id$=txt_GPPer]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(15)").html()));

                $("[id$=uxTrowindex]").val($(this).closest('tr').find('td').eq(0).text());
                $("[id$=txtItem]").val($(this).closest('tr').find('td').eq(2).text());
                $("[id$=txtDescr]").val($(this).closest('tr').find('td').eq(3).text());
                $("[id$=txtQt]").val($(this).closest('tr').find('td').eq(4).text());
                $("[id$=txtUnitPrice]").val($(this).closest('tr').find('td').eq(5).text());
                $("[id$=txtDisc]").val($(this).closest('tr').find('td').eq(6).text());
                $("[id$=cbTax]").val($(this).closest('tr').find('td').eq(7).text());
                $("[id$=txtTaxRate]").val($(this).closest('tr').find('td').eq(8).text());
                $("[id$=txtTot]").val($(this).closest('tr').find('td').eq(9).text());
                $("[id$=cbWhs]").val($(this).closest('tr').find('td').eq(10).text());
                $("[id$=txtQtyWhs]").val($(this).closest('tr').find('td').eq(11).text());
                $("[id$=txtQtAval]").val($(this).closest('tr').find('td').eq(12).text());
                $("[id$=txt_GP]").val($(this).closest('tr').find('td').eq(14).text());
                $("[id$=txt_GPPer]").val($(this).closest('tr').find('td').eq(15).text());

                $.ajax({
                    url: "SalesOrder.aspx/Get_ActiveOPGSelloutList",
                    data: "{'basedb':'SAPAE','rebatedb':'" + $("[id$=cbDataBase]").val() + "','itemcode':'" + $("[id$=txtItem]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (value) {
                        var jData = eval('(' + value.d + ')');
                        debugger;

                        var ddlopgSelloutList = jData.table;

                        $("[id$=cbopg]").empty();
                        for (var i = 0; i < ddlopgSelloutList.rows.length; i++) {

                            $("[id$=cbopg]").append($("<option></option>").val(ddlopgSelloutList.rows[i].opgid).html(ddlopgSelloutList.rows[i].opgid));
                        }

                       // $("[id$=cbopg]").val(($('#gvItem ').find("tr:eq(" + href + ")").find("td:eq(13)").html()));
                        $("[id$=cbopg]").val($(this).closest('tr').find('td').eq(13).text());
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
                $("[id$=btn_DelRow]").attr("disabled", false);
                $("[id$=btn_DelRow]").removeClass("disabled");

                $("[id$=btn_AddRow]").text("Update");
                return false;
            }
        });


    },

    BindGridSearch: function (jdata) {
        debugger;
        //Create a HTML Table element.
        var table = $("#classTable");
        table.find("tr:not(:first)").remove();
        //Get the count of columns.
        var columnCount = jdata.rows.length;
        //Add the data rows.

        for (var i = 0; i < jdata.rows.length; i++) {
            row = $(table[0].insertRow(-1));

            var cell = $("<td />"); cell.html("<a href=" + (i + 1) + "  class='linkTest'><b>Select</b></a>"); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].src); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].so_id); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].refno); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].sapdocnum); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].postdate); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].cardcode); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].cardname); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].project); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].businestype); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].tax); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].doctotal); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].docstatus); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].gp); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].apprstatus); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].aprovedby); row.append(cell);
            var cell = $("<td />"); cell.html(jdata.rows[i].remarks); row.append(cell);

        }
        $("#classTable tr").on('click', '.linkTest', function (e) {
            debugger;

            _SO_ID = $(this).closest('tr').find('td').eq(2).text();

            $('#classModal').modal('hide');

            getId(_SO_ID);


            return false;

            //            var href = $(this).find("a").attr("href");
            //            if (href) {
            //                //$(this).closest('tr').find('td').eq(2).text();

            //                //$("[id$=uxTrowindex]").val(($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(0)").html()));


            //                //alert(($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(0)").html()));
            //                //_SO_ID = ($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(2)").html());
            //                _SO_ID = $(this).closest('tr').find('td').eq(2).text();

            //                $('#classModal').modal('hide');

            //                getId(_SO_ID);


            //                return false;


            //            }
            //            return false;
        });

        //        $('#classTable tr td:eq(0)').click(function () {
        //            return false;
        //            debugger;
        //            var href = $(this).find("a").attr("href");
        //            if (href) {

        //                //$("[id$=uxTrowindex]").val(($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(0)").html()));


        //                //alert(($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(0)").html()));
        //                _SO_ID = ($('#classTable ').find("tr:eq(" + href + ")").find("td:eq(2)").html());

        //                $('#classModal').modal('hide');

        //                getId(_SO_ID);


        //                return false;


        //            }
        //        });
    },

    ControlInit: function () {

        $("[id$=pgHeader]").html('<span>Sales Order</span>');

        $("#tabs").tabs();

        $("[id$=txtTotBefTax]").val('0.00');
        $("[id$=txtTotalTax]").val('0.00');
        $("[id$=txtTotal]").val('0.00');
        $("[id$=txtGP]").val('0.00');
        $("[id$=txtGPPer]").val('0.00');
        //$("[id$=txtCreatedBy]").val('ABC')
        $('#btnExcelImport').change(handleFile);

        $.ajax({
            url: "SalesOrder.aspx/Get_CurrentLoginUser",
            data: "{ 'dbname': ''}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                debugger;
                var theString = data.d;

                var aryloginUser = new Array();
                aryloginUser = theString[0].split("#");

                _LoginUser = aryloginUser[0];

                $("[id$=txtCreatedBy]").val(aryloginUser[0]);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }


        });


    },

    SearchControlClear: function () {

        $("[id$=txtSerFrmDate]").val('');
        $("[id$=txtSerToDate]").val('');
        $("[id$=cbSerProject]").val('');
        $("[id$=txtSerCardCode]").val('');
        $("[id$=txtUnitPrice]").val('');
        $("[id$=txtSerCardName]").val('');
        $("[id$=cbSerDocStatus]").val('');
        $("[id$=txtSerRefNum]").val('');
        $("[id$=cbSerSalesEmp]").val('');
        $("[id$=cbSerApprStatus]").val('');

    },

    AddItemClearControls: function () {

        $("[id$=uxTrowindex]").val('');
        $("[id$=txtItem]").val('');
        $("[id$=txtDescr]").val('');
        $("[id$=txtQt]").val('');
        $("[id$=txtUnitPrice]").val('');
        $("[id$=cbTax]").val('');
        $("[id$=txtTaxRate]").val('');
        $("[id$=txtDisc]").val('0.00');
        $("[id$=txtTot]").val('');

        $("[id$=cbWhs]").val('');
        $("[id$=txtQtyWhs]").val('');
        $("[id$=txtQtAval]").val('');
        $("[id$=cbopg]").empty();
        $("[id$=cbopg]").val('');
        $("[id$=txt_GP]").val('');
        $("[id$=txt_GPPer]").val('');

        $("[id$=btn_AddRow]").text("Add Row");
        $("[id$=btn_DelRow]").attr("disabled", true);
        $("[id$=btn_DelRow]").addClass("disabled");
    },

    BindAutoCompleteDDL: function () {
        try {
            var usercode = $("[id$=txtCreatedBy]").val();
            $.ajax({
                url: "SalesOrder.aspx/GetUserDataBase",
                data: "{'prefix':'tejSAP', 'usercode':'" + usercode + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (value) {
                    var jData = eval('(' + value.d + ')');
                    debugger;

                    $("[id$=cbDataBase]").empty();
                    var dbList = jData;
                    for (var i = 0; i < dbList.rows.length; i++) {

                        $("[id$=cbDataBase]").append($("<option></option>").val(dbList.rows[i].code).html(dbList.rows[i].descr));
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
        catch (Error) {
            alert(Error);
        }
    },

    ClickEvent: function () {

        $('#btnGetTemplate').click(function () {
            debugger;
            var templeteFileCSV = '../Orders/Template/SalesOrder_Items.xlsx'; /// <reference path="../Template/SalesOrder_Items.xlsx" />

            window.open(templeteFileCSV, '_blank');
        });

        $('#btnRefresh').click(function () {
            debugger;
            try {
                if ($("[id$=txtItem]").val() != '' && $("[id$=cbWhs]").val() != '' && $("[id$=cbWhs]").val() != '0' && $("[id$=cbopg]").val() != '' && $("[id$=txtQt]").val() != '' && $("[id$=txtUnitPrice]").val() != '') {
                    Get_GP_And_GPPer();
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $('#btnCancel').click(function () {
            debugger;
            try {
                ClearControls();
            }
            catch (Error) {
                alert(Error);
            }
        });
        $('#btnSAPPost').click(function () {
            debugger;
            try {
                $('#divLoader').show();
                if (_Post_To_SAP == "N") {
                    if (($("[id$=txtCLStatus]").val() == 'Ok' || $("[id$=txtCLStatus]").val() == 'Expired' || $("[id$=txtCLStatus]").val() == 'Limit') && $("[id$=txtTrnStatus]").val() == 'Active') {
                        $.ajax({
                            url: "SalesOrder.aspx/Post_SalesOrder_InTo_SAP",
                            data: "{'dbname':'" + $("[id$=cbDataBase]").val() + "','_so_id':'" + _SO_ID.toString() + "'} ",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (value) {
                                debugger;
                                var jData = eval('(' + value.d + ')');

                                if (jData.rows[0].result == 'True') {
                                    alert(jData.rows[0].message + '. DocNum - ' + jData.rows[1].message);
                                    ClearControls();
                                }
                                else {
                                    alert(jData.rows[1].message);
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
                    else {
                        alert("Selected " + $("[id$=txtCardCode]").val() + " Customer Transaction Status is : " + $("[id$=txtTrnStatus]").val() + " And Credit Limit Status is : " + $("[id$=txtCLStatus]").val());
                        return;
                    }
                }
                else {
                    alert("This Document Already Posted in to SAP B1..");
                    return;
                }
            }
            catch (Error) {
                alert(Error);
            }

        });


        $('#btnSearch,#btnSer').click(function () {
            debugger;
            try {
                if (($("[id$=cbDataBase]").val() != "Select DB") && ($("[id$=cbDataBase]").val() != "0")) {

                    $('#classModal').modal('show');
                    var Parameters = ' And T0.CreatedBy=$' + _LoginUser + '$';


                    var db = $("[id$=cbDataBase]").val();
                    Get_Search_SalesOrderList(db, Parameters);


                }
            }
            catch (Error) {
                alert(Error);
            }

        });

        $("[id$=btnSave]").click(function () {
            try {
                debugger
                if (_Post_To_SAP == 'N') {
                    if (Validate() == true) {
                        var SalesOrder = new Array();
                        var SalesOrderDetail = new Array();
                        var SalesOrder_Obj = new Object();


                        SalesOrder_Obj['SO_ID'] = _SO_ID;
                        SalesOrder_Obj['DBName'] = $("[id$=cbDataBase]").val();
                        SalesOrder_Obj['PostingDate'] = GetSqlDateformat($("[id$=txtPostingDate]").val())
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
                        //                    SalesOrder_Obj['Credit_Limit'] = $("[id$=txtCreditLimit]").val();
                        //                    SalesOrder_Obj['Aging_0_30'] = $("[id$=txt_0_30]").val();
                        //                    SalesOrder_Obj['Aging_31_45'] = $("[id$=txt_31_45]").val();
                        //                    SalesOrder_Obj['Aging_46_60'] = $("[id$=txt_46_60]").val();
                        //                    SalesOrder_Obj['Aging_61_90'] = $("[id$=txt_61_90]").val();
                        //                    SalesOrder_Obj['Aging_91_Abv'] = $("[id$=txt_91_Above]").val();
                        //                    SalesOrder_Obj['TRNS_Status'] = $("[id$=txtTrnStatus]").val();
                        //                    SalesOrder_Obj['CL_Status'] = $("[id$=txtCLStatus]").val();

                        SalesOrder_Obj['Pay_Method_1'] = $("[id$=cbPayMth1]").val();
                        SalesOrder_Obj['Rcpt_check_No_1'] = $("[id$=txtCheck1]").val();

                        if ($("[id$=txtChkDate1]").val() == '')
                            SalesOrder_Obj['Rcpt_check_Date_1'] = '';
                        else
                            SalesOrder_Obj['Rcpt_check_Date_1'] = GetSqlDateformat($("[id$=txtChkDate1]").val());

                        SalesOrder_Obj['Remarks_1'] = $("[id$=txtRemarks1]").val();
                        SalesOrder_Obj['Curr_1'] = $("[id$=cbCur1]").val();
                        SalesOrder_Obj['Amount_1'] = $("[id$=txtAmount1]").val();
                        SalesOrder_Obj['Pay_Method_2'] = $("[id$=cbPayMth2]").val();
                        SalesOrder_Obj['Rcpt_check_No_2'] = $("[id$=txtCheck2]").val();

                        if ($("[id$=txtChkDate2]").val() == '')
                            SalesOrder_Obj['Rcpt_check_Date_2'] = '';
                        else
                            SalesOrder_Obj['Rcpt_check_Date_2'] = GetSqlDateformat($("[id$=txtChkDate2]").val());

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
                            SalesOrder_Obj['LastUpdatedBy'] = $("[id$=txtCreatedBy]").val(); ;
                        }


                        SalesOrder.push(SalesOrder_Obj);



                        $('#gvItem tr').each(function () {
                            if (!this.rowIndex) return; // skip first row
                            debugger;
                            var SalesOrderDetail_Obj = new Object();
                            SalesOrderDetail_Obj['ItemCode'] = (this.cells[2].innerHTML);
                            SalesOrderDetail_Obj['Description'] = (this.cells[3].innerHTML);
                            SalesOrderDetail_Obj['Quantity'] = (this.cells[4].innerHTML);
                            SalesOrderDetail_Obj['UnitPrice'] = (this.cells[5].innerHTML);
                            SalesOrderDetail_Obj['DiscPer'] = (this.cells[6].innerHTML);
                            SalesOrderDetail_Obj['TaxCode'] = (this.cells[7].innerHTML);
                            SalesOrderDetail_Obj['TaxRate'] = (this.cells[8].innerHTML);
                            SalesOrderDetail_Obj['LineTotal'] = (this.cells[9].innerHTML);
                            SalesOrderDetail_Obj['WhsCode'] = (this.cells[10].innerHTML);
                            SalesOrderDetail_Obj['QtyInWhs'] = (this.cells[11].innerHTML);
                            SalesOrderDetail_Obj['QtyAval'] = (this.cells[12].innerHTML);
                            SalesOrderDetail_Obj['OpgRefAlpha'] = (this.cells[13].innerHTML);

                            if (this.cells[14].innerHTML == '') {
                                SalesOrderDetail_Obj['GP'] = 0.00;
                                SalesOrderDetail_Obj['GPPer'] = 0.00;
                            }
                            else {
                                SalesOrderDetail_Obj['GP'] = (this.cells[14].innerHTML);
                                SalesOrderDetail_Obj['GPPer'] = (this.cells[15].innerHTML);
                            }

                            SalesOrderDetail.push(SalesOrderDetail_Obj);
                        });



                        $.ajax({
                            url: "SalesOrder.aspx/Save_SO",
                            data: "{'model':'" + JSON.stringify(SalesOrder) + "','model1':'" + JSON.stringify(SalesOrderDetail) + "','dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                            dataType: "json",
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            success: function (value) {
                                debugger;
                                var jData = eval('(' + value.d + ')');

                                if (jData.rows[0].result == 'True') {

                                    ClearControls();
                                    alert(jData.rows[0].message + " Trans ID-" + jData.rows[1].message);

                                }
                                else {
                                    alert(jData.rows[0].message);
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
                else {
                    alert('This Document already Posted into SAP B1...Can not Update.');
                    return;
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=cbDataBase]").change(function () {
            try {
                debugger;
                if (($("[id$=cbDataBase]").val() != "Select DB") && ($("[id$=cbDataBase]").val() != "")) {
                    $.ajax({
                        url: "SalesOrder.aspx/Get_BindDLList",
                        data: "{'dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (value) {
                            var jData = eval('(' + value.d + ')');
                            debugger;


                            var ddlRDDProject = jData.table;
                            var ddlBusinesType = jData.table1;
                            var ddlInvPayTerm = jData.table2;
                            var ddlCustPayTerm = jData.table3;
                            var ddlSalesEmp = jData.table4;
                            var ddlPayMethod = jData.table5;
                            var ddlCurrency = jData.table6;
                            var ddlWhsCode = jData.table7;
                            var ddlTaxCode = jData.table8;
                            var ddlDocStatus = jData.table9;
                            var ddlAprStatus = jData.table10;

                            //--This is for serch window
                            $("[id$=cbSerProject]").empty();
                            for (var i = 0; i < ddlRDDProject.rows.length; i++) {

                                $("[id$=cbSerProject]").append($("<option></option>").val(ddlRDDProject.rows[i].code).html(ddlRDDProject.rows[i].descr));
                            }

                            $("[id$=cbSerSalesEmp]").empty();
                            for (var i = 0; i < ddlSalesEmp.rows.length; i++) {

                                $("[id$=cbSerSalesEmp]").append($("<option></option>").val(ddlSalesEmp.rows[i].code).html(ddlSalesEmp.rows[i].descr));
                            }

                            $("[id$=cbSerDocStatus]").empty();
                            for (var i = 0; i < ddlDocStatus.rows.length; i++) {

                                $("[id$=cbSerDocStatus]").append($("<option></option>").val(ddlDocStatus.rows[i].code).html(ddlDocStatus.rows[i].descr));
                            }

                            $("[id$=cbSerApprStatus]").empty();
                            for (var i = 0; i < ddlAprStatus.rows.length; i++) {

                                $("[id$=cbSerApprStatus]").append($("<option></option>").val(ddlAprStatus.rows[i].code).html(ddlAprStatus.rows[i].descr));
                            }
                            //---Thi is for Main window

                            $("[id$=cbRDDProject]").empty();
                            for (var i = 0; i < ddlRDDProject.rows.length; i++) {

                                $("[id$=cbRDDProject]").append($("<option></option>").val(ddlRDDProject.rows[i].code).html(ddlRDDProject.rows[i].descr));
                            }

                            $("[id$=cbBusinessType]").empty();
                            for (var i = 0; i < ddlBusinesType.rows.length; i++) {

                                $("[id$=cbBusinessType]").append($("<option></option>").val(ddlBusinesType.rows[i].code).html(ddlBusinesType.rows[i].descr));
                            }

                            $("[id$=cbInvPayTerm]").empty();
                            for (var i = 0; i < ddlInvPayTerm.rows.length; i++) {

                                $("[id$=cbInvPayTerm]").append($("<option></option>").val(ddlInvPayTerm.rows[i].code).html(ddlInvPayTerm.rows[i].descr));
                            }

                            $("[id$=cbCustPayTerm]").empty();
                            for (var i = 0; i < ddlCustPayTerm.rows.length; i++) {

                                $("[id$=cbCustPayTerm]").append($("<option></option>").val(ddlCustPayTerm.rows[i].code).html(ddlCustPayTerm.rows[i].descr));
                            }

                            $("[id$=cbSalesEmp]").empty();
                            for (var i = 0; i < ddlSalesEmp.rows.length; i++) {

                                $("[id$=cbSalesEmp]").append($("<option></option>").val(ddlSalesEmp.rows[i].code).html(ddlSalesEmp.rows[i].descr));
                            }

                            $("[id$=cbPayMth1]").empty();
                            for (var i = 0; i < ddlPayMethod.rows.length; i++) {

                                $("[id$=cbPayMth1]").append($("<option></option>").val(ddlPayMethod.rows[i].code).html(ddlPayMethod.rows[i].descr));
                            }

                            $("[id$=cbPayMth2]").empty();
                            for (var i = 0; i < ddlPayMethod.rows.length; i++) {

                                $("[id$=cbPayMth2]").append($("<option></option>").val(ddlPayMethod.rows[i].code).html(ddlPayMethod.rows[i].descr));
                            }

                            $("[id$=cbCur1]").empty();
                            for (var i = 0; i < ddlCurrency.rows.length; i++) {

                                $("[id$=cbCur1]").append($("<option></option>").val(ddlCurrency.rows[i].code).html(ddlCurrency.rows[i].descr));
                            }

                            $("[id$=cbCur2]").empty();
                            for (var i = 0; i < ddlCurrency.rows.length; i++) {

                                $("[id$=cbCur2]").append($("<option></option>").val(ddlCurrency.rows[i].code).html(ddlCurrency.rows[i].descr));
                            }

                            $("[id$=cbWhs]").empty();
                            for (var i = 0; i < ddlWhsCode.rows.length; i++) {

                                $("[id$=cbWhs]").append($("<option></option>").val(ddlWhsCode.rows[i].code).html(ddlWhsCode.rows[i].descr));
                            }

                            $("[id$=cbTax]").empty();
                            for (var i = 0; i < ddlTaxCode.rows.length; i++) {

                                $("[id$=cbTax]").append($("<option></option>").val(ddlTaxCode.rows[i].code).html(ddlTaxCode.rows[i].descr));
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

        $("[id$=cbCustPayTerm],[id$=txtPostingDate]").change(function () {
            try {
                debugger;
                //$("[id$=txtPostingDate]").val()
                if ($("[id$=cbCustPayTerm]").val() != '' && $("[id$=cbCustPayTerm]").val() != 'Select PayTerms' && $("[id$=txtPostingDate]").val() != '') {

                    var groupnum = $("[id$=cbCustPayTerm]").val();
                    var db = $("[id$=cbDataBase]").val();

                    //Get_DeliveryDate(groupnum, db);
                }
                else {
                    PopUpMessage("Select Asset Group And Put to use date...!");
                }
            }
            catch (Error) {
                alert(Error);
            }
        });

        $("[id$=txtCardName]").autocomplete({

            source: function (request, response) {

                debugger;

                if ($("[id$=cbDataBase]").val() == "Select DB" || $("[id$=cbDataBase]").val() == "" || $("[id$=cbDataBase]").val() == "0") {
                    alert('Please Select Company DataBase ');
                    $("[id$=txtCardName]").val('');
                    $("[id$=txtCardCode]").val('');
                    return;
                }
                $.ajax({
                    url: "SalesOrder.aspx/GetCustomers",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=cbDataBase]").val() + "','field':'cardname'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
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
                debugger;
                $("[id$=txtCardName]").val(i.item.label);
                $("[id$=txtCardCode]").val(i.item.val);

                get_Customer_Due(i.item.val)


            },
            minLength: 1
        });

        $("[id$=txtUnitPrice],[id$=txtQt]").change(function () {
            debugger;
            try {
                if ($("[id$=txtQt]").val() != '' && $("[id$=txtUnitPrice]").val() != '' && $("[id$=cbDataBase]").val() != "Select DB" && $("[id$=cbDataBase]").val() != "0") {

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

        $("[id$=cbWhs]").change(function () {
            try {
                if ($("[id$=cbWhs]").val() != '' && $("[id$=cbDataBase]").val() != "Select DB" && $("[id$=cbDataBase]").val() != "0") {
                    debugger;
                    $.ajax({
                        url: "SalesOrder.aspx/Get_WarehouseQty",
                        data: "{'itemcode':'" + $("[id$=txtItem]").val() + "','whscode':'" + $("[id$=cbWhs]").val() + "','dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (value) {
                            debugger;
                            var jData = eval('(' + value.d + ')');
                            var QtyInWhs = jData.rows[0].onhand;
                            var QtyAvl = jData.rows[0].actalqty;

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
                if ($("[id$=cbTax]").val() != '' && $("[id$=cbDataBase]").val() != "Select DB" && $("[id$=cbDataBase]").val() != "0") {
                    $.ajax({
                        url: "SalesOrder.aspx/Get_TaxCodeRate",
                        data: "{'taxcode':'" + $("[id$=cbTax]").val() + "','dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (value) {
                            debugger;
                            var jData = eval('(' + value.d + ')');
                            var TaxRate = jData.rows[0].rate;

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

        $("[id$=txtItem]").autocomplete({


            source: function (request, response) {

                debugger;

                if (($("[id$=cbDataBase]").val() == "Select DB") || ($("[id$=cbDataBase]").val() == "") || ($("[id$=cbDataBase]").val() == "0")) {
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
                    url: "SalesOrder.aspx/GetItemList",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
                            return {
                                label: item.split('$')[0],
                                val: item.split('$')[1],
                                whs: item.split('$')[2],
                                taxcode: item.split('$')[3],
                                whsqty: item.split('$')[4],
                                actualqty: item.split('$')[5],
                                taxrate: item.split('$')[6]
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
                $("[id$=cbWhs]").val(i.item.whs);
                $("[id$=cbTax]").val(i.item.taxcode);
                $("[id$=txtQtyWhs]").val(i.item.whsqty);
                $("[id$=txtQtAval]").val(i.item.actualqty);
                $("[id$=txtTaxRate]").val(i.item.taxrate);

                $.ajax({
                    url: "SalesOrder.aspx/Get_ActiveOPGSelloutList",
                    data: "{'basedb':'SAPAE','rebatedb':'" + $("[id$=cbDataBase]").val() + "','itemcode':'" + $("[id$=txtItem]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (value) {
                        var jData = eval('(' + value.d + ')');
                        debugger;

                        var ddlopgSelloutList = jData.table;

                        $("[id$=cbopg]").empty();
                        for (var i = 0; i < ddlopgSelloutList.rows.length; i++) {

                            $("[id$=cbopg]").append($("<option></option>").val(ddlopgSelloutList.rows[i].opgid).html(ddlopgSelloutList.rows[i].opgid));
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

        $("[id$=btn_AddRow]").click(function () {
            try {

                debugger;
                var _SOID = 0;
                if (Validate_AddRow() == true) {


                    if ($("[id$=btn_AddRow]").text() == 'Add Row') {
                        var _ItemDetails = {};

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
                        SalesOrder.prototype.BindGrid(ItemDetails);
                    }
                    else {

                        var index = $("[id$=uxTrowindex]").val();
                        var parameter = ItemDetails[index - 1];

                        $("[id$=uxTrowindex]").val(index);

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

                        SalesOrder.prototype.BindGrid(ItemDetails);
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

        $("[id$=btn_DelRow]").click(function () {
            debugger;
            var index = $('#uxTrowindex').val();
            ItemDetails.splice(index - 1, 1);

            SalesOrder.prototype.BindGrid(ItemDetails);
            SalesOrder.prototype.AddItemClearControls();

            var _TotBefTax = 0.00;
            var _TotTax = 0.00;
            var _Total = 0.00;

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

            $("[id$=btn_DelRow]").attr("disabled", true);
            $("[id$=btn_DelRow]").addClass("disabled");

            if (_SO_ID != 0) {
                //                $("#uxSaveMain").text("Update");
                //                $('#uxSaveMain').prepend('<span class="k-icon k-i-plus"></span>');
            }
        });

        $("[id$=btn_clear]").click(function () {
            try {

                SalesOrder.prototype.AddItemClearControls();
            }
            catch (Error) {
                alert(Error);
            }

        });


        //-------This is for Serch Window----
        $('#btnClearFilter').click(function () {
            debugger;
            try {
                SalesOrder.prototype.SearchControlClear();
            }
            catch (Error) {
                alert(Error);
            }

        });


        $('#btnApplyFilter').click(function () {
            debugger;
            try {
                if (($("[id$=cbDataBase]").val() != "Select DB") && ($("[id$=cbDataBase]").val() != "0")) {

                    var db = $("[id$=cbDataBase]").val();
                    var searchcriteria = ' And T0.CreatedBy=$' + _LoginUser + '$';

                    if ($("[id$=txtSerCardCode]").val() != '')
                        searchcriteria = ' And T0.CardCode=$' + $("[id$=txtSerCardCode]").val() + '$';

                    if ($("[id$=cbSerProject]").val() != '0' && $("[id$=cbSerProject]").val() != '' && $("[id$=cbSerProject]").val() != 'Select RDD Project')
                        searchcriteria = searchcriteria + ' And RDD_Project=$' + $("[id$=cbSerProject]").val() + '$';

                    if ($("[id$=txtSerFrmDate]").val() != '' && $("[id$=txtSerToDate]").val() != '')
                        searchcriteria = searchcriteria + ' And PostingDate BetWeen $' + GetSqlDateformat($("[id$=txtSerFrmDate]").val()) + '$ And $' + GetSqlDateformat($("[id$=txtSerToDate]").val()) + '$';

                    if ($("[id$=txtSerRefNum]").val() != '')
                        searchcriteria = searchcriteria + ' And RefNo=$' + $("[id$=txtSerRefNum]").val() + '$';

                    if ($("[id$=cbSerSalesEmp]").val() != '-1' && $("[id$=cbSerSalesEmp]").val() != '' && $("[id$=cbSerSalesEmp]").val() != '-No Sales Employee-')
                        searchcriteria = searchcriteria + ' And SalesEmp=$' + $("[id$=cbSerSalesEmp]").val() + '$';

                    if ($("[id$=cbSerDocStatus]").val() != '0' && $("[id$=cbSerDocStatus]").val() != '' && $("[id$=cbSerDocStatus]").val() != '--Select--')
                        searchcriteria = searchcriteria + ' And DocStatus=$' + $("[id$=cbSerDocStatus]").val() + '$';

                    if ($("[id$=cbSerApprStatus]").val() != '0' && $("[id$=cbSerApprStatus]").val() != '' && $("[id$=cbSerApprStatus]").val() != '--Select--')
                        searchcriteria = searchcriteria + ' And AprovalStatus=$' + $("[id$=cbSerApprStatus]").val() + '$';

                    Get_Search_SalesOrderList(db, searchcriteria);

                }
            }
            catch (Error) {
                alert(Error);
            }

        });

        $("[id$=txtSerCardName]").autocomplete({

            source: function (request, response) {

                debugger;

                if ($("[id$=cbDataBase]").val() == "Select DB" || $("[id$=cbDataBase]").val() == "" || $("[id$=cbDataBase]").val() == "0") {
                    alert('Please Select Company DataBase ');
                    $("[id$=txtSerCardName]").val('');
                    $("[id$=txtSerCardCode]").val('');
                    return;
                }
                $.ajax({
                    url: "SalesOrder.aspx/GetCustomers",
                    data: "{ 'prefix': '" + request.term + "','dbname':'" + $("[id$=cbDataBase]").val() + "','field':'cardname'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        //alert(data.d);
                        response($.map(data.d, function (item) {
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
                debugger;
                $("[id$=txtSerCardName]").val(i.item.label);
                $("[id$=txtSerCardCode]").val(i.item.val);


            },
            minLength: 1
        });
    }
}


$(document).ready(function () {

    var SalesOrder_obj = new SalesOrder();
    SalesOrder_obj.Init();

});

function isNumberKey(evt) {
    debugger;
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
        return true;
    } else {
        alert("Enter only numbers");
        return false;
    }
}

function Validate_AddRow() {
    try {

        debugger;

        if ($("[id$=txtItem]").val() == '') {
            alert("Select Item...");
            return false;
        }

        if ($("[id$=cbWhs]").val() == '0' || $("[id$=cbWhs]").val() == '') {
            alert("Select Warehouse...");
            return false;
        }

        if ($("[id$=txtQtAval]").val() != '') {
            var QtAval = parseInt($("[id$=txtQtAval]").val());
            if (QtAval <= 0) {
                alert("Available Quantity should be greater than zero in Selected Warehouse...");
                return false;
            }
        }
        else {
            alert("Available Quantity should be greater than zero in Selected Warehouse...");
            return false;
        }

        if ($("[id$=txtQt]").val() != '') {
            var Qty = parseInt($("[id$=txtQt]").val());
            var AvalQty = parseInt($("[id$=txtQtAval]").val());
            if (Qty <= 0 || Qty > AvalQty) {
                alert("Quantity should be greater than zero And less than or equal to Available quantity...");
                return false;
            }

        }
        else {
            alert("Quantity should be greater than zero And less than or equal to Available quantity...");
            return false;
        }

        if ($("[id$=cbTax]").val() == '0' || $("[id$=cbTax]").val() == '') {
            alert("Select Tax Code...");
            return false;
        }

        if ($("[id$=txtUnitPrice]").val() == '') {
            alert("Enter Unit Price...");
            return false;
        }

        if ($("[id$=cbopg]").val() == '0' || $("[id$=cbopg]").val() == '') {
            alert("Select OPG...");
            return false;
        }


        return true;
    }
    catch (Error) {
        alert(Error);
        return false;
    }
}

function Validate() {
    debugger;
    try {

        if ($("[id$=cbDataBase]").val() == '0' || $("[id$=cbDataBase]").val() == '' || $("[id$=cbDataBase]").val() == '--Select DB--') {
            alert("Select SAP Database...");
            return false;
        }

        if ($("[id$=txtCardCode]").val() == '') {
            alert("Select CardName...");
            return false;
        }

        if ($("[id$=txtRefNum]").val() == '') {
            alert("Enter Reference No....");
            return false;
        }

        if ($("[id$=cbRDDProject]").val() == '0' || $("[id$=cbRDDProject]").val() == '' || $("[id$=cbRDDProject]").val() == 'Select RDD Project') {
            alert("Select Project...");
            return false;
        }

        if ($("[id$=cbBusinessType]").val() == '0' || $("[id$=cbBusinessType]").val() == '' || $("[id$=cbBusinessType]").val() == 'Select Business Type') {
            alert("Select Business Type...");
            return false;
        }

        if ($("[id$=cbInvPayTerm]").val() == '0' || $("[id$=cbInvPayTerm]").val() == '' || $("[id$=cbInvPayTerm]").val() == 'Select Inv PayTerm') {
            alert("Select Invoice Payment Terms...");
            return false;
        }

        if ($("[id$=cbCustPayTerm]").val() == '0' || $("[id$=cbCustPayTerm]").val() == '' || $("[id$=cbCustPayTerm]").val() == 'Select PayTerms') {
            alert("Select Customer Payment Terms...");
            return false;
        }

        if ($("[id$=cbSalesEmp]").val() == '-1' || $("[id$=cbCustPayTerm]").val() == '' || $("[id$=cbCustPayTerm]").val() == '-No Sales Employee-') {
            alert("Select Sales Employee...");
            return false;
        }

        //---This is for payment method 1

        if ($("[id$=cbPayMth1]").val() == '0' || $("[id$=cbPayMth1]").val() == '' || $("[id$=cbPayMth1]").val() == '--Select--') {
            alert("Select Payment Method 1...");
            return false;
        }

        if ($("[id$=txtCheck1]").val() == '') {
            alert("Enter Check/Rceipt No 1...");
            return false;
        }

        if ($("[id$=txtChkDate1]").val() == '') {
            alert("Select Check/Rceipt Date 1...");
            return false;
        }

        if ($("[id$=txtRemarks1]").val() == '') {
            alert("Enter Remark 1...");
            return false;
        }

        if ($("[id$=cbCur1]").val() == '0' || $("[id$=cbCur1]").val() == '' || $("[id$=cbCur1]").val() == '--Select--') {
            alert("Select Currency 1...");
            return false;
        }

        if ($("[id$=txtAmount1]").val() == '') {
            alert("Enter Amount 1...");
            return false;
        }

        //----This is for Grid
        if (ItemDetails.length <= 0) {
            alert("Add Items in Grid...");
            return false;
        }

        //---This is for payment method 2

        if ($("[id$=cbPayMth2]").val() != '0' && $("[id$=cbPayMth2]").val() != '' && $("[id$=cbPayMth2]").val() != '--Select--') {

            if ($("[id$=txtCheck2]").val() == '') {
                alert("Enter Check/Rceipt No 2...");
                return false;
            }

            if ($("[id$=txtChkDate2]").val() == '') {
                alert("Select Check/Rceipt Date 2...");
                return false;
            }

            if ($("[id$=txtRemarks2]").val() == '') {
                alert("Enter Remark 2...");
                return false;
            }

            if ($("[id$=cbCur2").val() == '0' || $("[id$=cbCur2]").val() == '' || $("[id$=cbCur2]").val() == '--Select--') {
                alert("Select Currency 2...");
                return false;
            }

            if ($("[id$=txtAmount2]").val() == '') {
                alert("Enter Amount 2...");
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


function get_Customer_Due(customercode) {
    var s = customercode;

    $.ajax({
        url: "SalesOrder.aspx/Get_CustomersDue_Info",
        data: "{ 'dbname': '" + $("[id$=cbDataBase]").val() + "','cardcode':'" + s + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        success: function (data) {
            debugger;
            var theString = data.d;

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
            $("[id$=cbCustPayTerm]").val(arySummary[8]);

            var db = $("[id$=cbDataBase]").val();

            Get_DeliveryDate(arySummary[8], db);

            if (arySummary[6] == "Active")
            { $("[id$=txtTrnStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:Green;color:White;'); }
            else {
                $("[id$=txtTrnStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:red;color:White;');
            }

            if (arySummary[7] == "Ok")
            { $("[id$=txtCLStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:Green;color:white;'); }
            else { $("[id$=txtCLStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:red;color:white;'); }


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
            url: "SalesOrder.aspx/Get_PayTerms_Days",
            data: "{'dbname': '" + dbname + "','groupnum':'" + groupnum + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST",
            success: function (data) {
                debugger;
                var theString = data.d;

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

function ClearControls() {
    debugger;
    _SO_ID = 0;
    _Post_To_SAP = 'N';

    $("[id$=pgHeader]").html('<span>Sales Order</span>');
    $("[id$=cbDataBase]").val('0');

    var Dt = new Date();

    var CurDate = DateToStringformat(Dt);
    $("[id$=txtPostingDate]").val(CurDate);

    Dt.setDate(Dt.getDate() + 30);
    CurDate = DateToStringformat(Dt);
    $("[id$=txtDelDate]").val(CurDate);
    $("[id$=txtDocStatus]").val('Draft');
    $("[id$=txtApprvBy]").val('');

    $("[id$=txtCreatedBy]").val('');
    $("[id$=txtCardCode]").val('');
    $("[id$=txtCardName]").val('');
    $("[id$=txtRefNum]").val('');

    $("[id$=cbRDDProject]").val('');
    $("[id$=cbBusinessType]").val('');
    $("[id$=cbInvPayTerm]").val('');
    $("[id$=cbCustPayTerm]").val('');
    $("[id$=cbSalesEmp]").val('');

    $("[id$=txt_ForworderDet]").val('');

    $("[id$=txtCreditLimit]").val('0.00');
    $("[id$=txt_0_30]").val('0.00');
    $("[id$=txt_31_45]").val('0.00');
    $("[id$=txt_46_60]").val('0.00');
    $("[id$=txt_61_90]").val('0.00');
    $("[id$=txt_91_Above]").val('0.00');
    $("[id$=txtTrnStatus]").val('');
    $("[id$=txtCLStatus]").val('');


    $("[id$=txtTrnStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:none;color:none;');
    $("[id$=txtCLStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:none;color:none;');


    $("[id$=cbPayMth1]").val('');
    $("[id$=txtCheck1]").val('');
    $("[id$=txtChkDate1]").val('');
    $("[id$=txtRemarks1]").val('');
    $("[id$=cbCur1]").val('');
    $("[id$=txtAmount1]").val('');

    $("[id$=cbPayMth2]").val();
    $("[id$=txtCheck2]").val();
    $("[id$=txtChkDate2]").val('');
    $("[id$=txtRemarks2]").val('');
    $("[id$=cbCur2]").val('');
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

    $("#btnSave").text("Save");
}

function Get_GP_And_GPPer() {
    try {

        $.ajax({
            url: "SalesOrder.aspx/Get_GPAndGPPer",
            data: "{'dbname':'" + $("[id$=cbDataBase]").val() + "','itemcode':'" + $("[id$=txtItem]").val() + "','warehouse':'" + $("[id$=cbWhs]").val() + "','qtysell':'" + $("[id$=txtQt]").val() + "','pricesell':'" + $("[id$=txtUnitPrice]").val() + "','curr':'USD','opgrebateid':'" + $("[id$=cbopg]").val() + "'} ",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (value) {
                debugger;
                var jData = eval('(' + value.d + ')');
                var GP = jData.rows[0].gpvalrowusd;
                var GPPer = jData.rows[0].gppercrowusd;

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

function getId(e) {
    try {
        debugger;

        _SO_ID = e;

        //===========================================
        $("#btnSave").text("Update");
        $('#btnSave').prepend('<span class="k-icon k-i-plus"></span>');
        //===========================================

        $.ajax({
            url: "SalesOrder.aspx/Get_Rec_SalesOrder",
            data: "{'dbname':'" + $("[id$=cbDataBase]").val() + "','so_id':'" + _SO_ID + "'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (value) {
                var jData = eval('(' + value.d + ')');
                debugger;

                ItemDetails = new Array();

                var SO_Header = jData.table.rows[0];
                var SO_Details = jData.table1.rows;


                _SO_ID = SO_Header.so_id;
                _Post_To_SAP = SO_Header.post_sap;

                if (SO_Header.apprstatus != '')
                    $("[id$=pgHeader]").html('<span>Sales Order [' + SO_Header.apprstatus + ']</span>');
                else
                    $("[id$=pgHeader]").html('<span>Sales Order</span>');

                $("[id$=cbDataBase]").val(SO_Header.dbname);
                $("[id$=txtCardCode]").val(SO_Header.cardcode);
                $("[id$=txtCardName]").val(SO_Header.cardname);
                $("[id$=txtRefNum]").val(SO_Header.refno);
                $("[id$=txtPostingDate]").val(SO_Header.postingdate);
                $("[id$=txtDelDate]").val(SO_Header.deliverydate);
                $("[id$=txtDocStatus]").val(SO_Header.docstatus);
                $("[id$=txtApprvBy]").val(SO_Header.aprovedby);
                $("[id$=txtCreatedBy]").val(SO_Header.createdby);
                $("[id$=cbRDDProject]").val(SO_Header.rdd_project);
                $("[id$=cbBusinessType]").val(SO_Header.businestype);
                $("[id$=cbInvPayTerm]").val(SO_Header.invpayterms);
                $("[id$=cbCustPayTerm]").val(SO_Header.custpayterms);
                $("[id$=txt_ForworderDet]").val(SO_Header.forwarder);
                $("[id$=cbSalesEmp]").val(SO_Header.salesemp);

                //                $("[id$=txtCreditLimit]").val(SO_Header.credit_limit);
                //                $("[id$=txt_0_30]").val(SO_Header.aging_0_30);
                //                $("[id$=txt_31_45]").val(SO_Header.aging_31_45);
                //                $("[id$=txt_46_60]").val(SO_Header.aging_46_60);
                //                $("[id$=txt_61_90]").val(SO_Header.aging_61_90);
                //                $("[id$=txt_91_Above]").val(SO_Header.aging_91_abv);
                //                $("[id$=txtTrnStatus]").val(SO_Header.trns_status);
                //                $("[id$=txtCLStatus]").val(SO_Header.cl_status);

                if (SO_Header.trns_status == "Active")
                { $("[id$=txtTrnStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:Green;color:White;'); }
                else {
                    $("[id$=txtTrnStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:red;color:White;');
                }

                if (SO_Header.cl_status == "Ok")
                { $("[id$=txtCLStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:Green;color:white;'); }
                else { $("[id$=txtCLStatus]").attr('style', 'Width:130px; Height:33px;font-family: Raleway; font-size: 14px; font-weight: bold; text-align:center;background-color:red;color:white;'); }

                $("[id$=cbPayMth1]").val(SO_Header.pay_method_1);
                $("[id$=txtCheck1]").val(SO_Header.rcpt_check_no_1);
                $("[id$=txtChkDate1]").val(SO_Header.rcpt_check_date_1);
                $("[id$=txtRemarks1]").val(SO_Header.remarks_1);
                $("[id$=cbCur1]").val(SO_Header.curr_1);
                $("[id$=txtAmount1]").val(SO_Header.amount_1);

                $("[id$=cbPayMth2]").val(SO_Header.pay_method_2);
                $("[id$=txtCheck2]").val(SO_Header.rcpt_check_no_2);
                $("[id$=txtChkDate2]").val(SO_Header.rcpt_check_date_2);
                $("[id$=txtRemarks2]").val(SO_Header.remarks_2);
                $("[id$=cbCur2]").val(SO_Header.curr_2);
                $("[id$=txtAmount2]").val(SO_Header.amount_2);

                $("[id$=txtTotBefTax]").val(SO_Header.total_bef_tax);
                $("[id$=txtTotalTax]").val(SO_Header.total_tx);
                $("[id$=txtTotal]").val(SO_Header.doctotal);
                $("[id$=txtGP]").val(SO_Header.gp);
                $("[id$=txtGPPer]").val(SO_Header.gp_per);
                $("[id$=txtRemarks]").val(SO_Header.remarks);

                ItemDetails = jData.table1.rows;
                SalesOrder.prototype.BindGrid(jData.table1.rows);

                get_Customer_Due(SO_Header.cardcode.toString());

            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });


    } catch (n) {
        alert(n)
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

function Get_Search_SalesOrderList(dbname, searchcriteria) {
    try {
        debugger;

        $.ajax({
            url: "SalesOrder.aspx/Get_SalesOrder_List",
            data: "{'dbname':'" + dbname + "','searchcriteria':'" + searchcriteria + "'} ",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (value) {
                var jData = eval('(' + value.d + ')');
                debugger;
                SalesOrder.prototype.BindGridSearch(jData);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });

    }
    catch (ex) {
        log(ex);
    }

}

function handleFile(e) {
    debugger;
    //Get the files from Upload control
    var files = e.target.files;
    var i, f;
    //Loop through files
    for (i = 0, f = files[i]; i != files.length; ++i) {
        var reader = new FileReader();
        var name = f.name;
        reader.onload = function (e) {
            var data = e.target.result;

            var result;
            var workbook = XLSX.read(data, { type: 'binary' });

            var sheet_name_list = workbook.SheetNames;
            sheet_name_list.forEach(function (y) { /* iterate through sheets */
                //Convert the cell value to Json
                var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                if (roa.length > 0) {
                    result = roa;
                }
            });
            //Get the first column first cell value
            //alert(result[0].Column1);
            Get_DataFromExcel(result);
        };
        reader.readAsArrayBuffer(f);
    }
}


function Get_DataFromExcel(result) {
    debugger;
    try {

        if (result.length > 0) {

            var _ItemDetails = new Array();

            for (var i = 0; i < result.length; i++) {
                var _ItemDetails_Obj = {};

                _ItemDetails_Obj["pvlineid"] = i;
                _ItemDetails_Obj["itemcode"] = result[i].ItemCode;
                _ItemDetails_Obj["description"] = result[i].Description;
                _ItemDetails_Obj["quantity"] = result[i].Quantity;
                _ItemDetails_Obj["unitprice"] = result[i].UnitPrice;
                _ItemDetails_Obj["taxcode"] = result[i].TaxCode;
                _ItemDetails_Obj["whscode"] = result[i].Warehouse;

                if (result[i].opgRefAlpha != undefined && result[i].opgRefAlpha != '')
                    _ItemDetails_Obj["opgrefalpha"] = result[i].opgRefAlpha;
                else
                    _ItemDetails_Obj["opgrefalpha"] = "NA";

                _ItemDetails.push(_ItemDetails_Obj);

            }

            $.ajax({
                url: "SalesOrder.aspx/To_GetData_From_Excel",
                data: "{'model1':'" + JSON.stringify(_ItemDetails) + "','dbname':'" + $("[id$=cbDataBase]").val() + "'} ",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (value) {
                    debugger;
                    var jData = eval('(' + value.d + ')');

                    ItemDetails = jData.rows;
                    var messg = '';

                    if (ItemDetails[0].result == "False") {
                        for (var i = 0; i < ItemDetails.length; i++) {
                            messg = messg + ItemDetails[i].rowno + " - " + ItemDetails[i].msg +"\n";
                        }
                        alert(messg);
                    }
                    else {
                        SalesOrder.prototype.BindGrid(jData.rows);

                        Get_Calculation();
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
    catch (ex) {
        log(ex);
    }
}