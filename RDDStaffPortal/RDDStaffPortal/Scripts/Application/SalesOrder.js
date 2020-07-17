var SalesOrder = function () { };  //Class

SalesOrder.prototype = {

    Init: function () {

        SalesOrder.prototype.ControlInit();
        SalesOrder.prototype.ClickEvent();
        
    },

    ControlInit: function () {

        $('#DBName,#cbRDDProject,#cbBusinessType,#cbInvPayTerm,#cbCustPayTerm,#cbSalesEmp,#cbPayMth1,#cbPayMth2,#cbCur1,#cbCur2').select2({
            theme: "bootstrap",
            allowClear: true

        });
        $(".datepicker").datetimepicker({
            format: 'DD/MM/YYYY'
        });
    },

    ClickEvent: function () {

        $("[id$=DBName]").change(function () {
            try {
                debugger;
                if (($("[id$=DBName]").val() != "Select DB") && ($("[id$=DBName]").val() != "")) {
                    $.ajax({


                        async: false,
                        cache: false,
                        type: "POST",
                        url: "/SAP/SalesOrder/Get_BindDDLList",
                        data: JSON.stringify({ dbname: $("#DBName").val() }),
                        dataType: 'Json',
                        contentType: "Application/json",

                        success: function (value) {
                            debugger;
                            var jData = value

                            var ddlRDDProject = jData.Table;
                            var ddlBusinesType = jData.Table1;
                            var ddlInvPayTerm = jData.Table2;
                            var ddlCustPayTerm = jData.Table3;
                            var ddlSalesEmp = jData.Table4;
                            var ddlPayMethod = jData.Table5;
                            var ddlCurrency = jData.Table6;
                            //var ddlWhsCode = jData.Table7;
                            //var ddlTaxCode = jData.Table8;
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
        //var k = $.noConflict(true);
        $("[id$=txtCardName]").autocomplete({

            source: function (request, response) {

                debugger;

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
                        debugger;
                        alert(data);
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
                debugger;
                $("[id$=txtCardName]").val(i.item.label);
                $("[id$=txtCardCode]").val(i.item.val);

               // get_Customer_Due(i.item.val)


            },
            minLength: 1
        });
    },
}   

$(document).ready(function () {

    var SalesOrder_obj =  new SalesOrder();
    SalesOrder_obj.Init();

});