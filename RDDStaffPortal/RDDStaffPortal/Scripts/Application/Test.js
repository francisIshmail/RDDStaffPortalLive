var test = {
    initialize: function () {

        $.fn.dataTable.ext.errorMode = 'none';
        test.Attachevent();
    },
    Attachevent: function () {      
        /*Loading Data*/
        var colms = [
            { "mDataProp": "CODE", "sWidth": "30%" },
            { "mDataProp": "DESCRIPTION", "sWidth": "20%" },
            { "mDataProp": "IsDefault", 
                render: function (data, type, full, meta) {
                    if (full.IsDefault == true) {
                        return '<input id="toggle-demo" class="toggle" type="checkbox" checked data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-offstyle="danger">';
                    } else {
                        return '<input id="toggle-demo" class="toggle" type="checkbox"  data-toggle="toggle" data-on="On" data-off="Off" data-onstyle="success" data-offstyle="danger">';
                    }
                } ,
                "sWidth": "20%"
            },
            {
                "data": null,               
                "defaultContent": "<button type='button' data-toggle='tooltip' title='' name='Edit' class='btn btn-primary btn-xs mar-1 padd-l7 padd-r7 edit' data-original-title='Edit Task'><i class='fa fa-edit'></i></button><button type='button' name='Delete' data-toggle='tooltip' title='' class='btn btn-danger btn-xs mar-1 padd-l7 padd-r7 delete' data-original-title='Remove'><i class='fa fa-trash-alt'></i></button><input type='hidden' id='EditMode' value='false' ></input>",
                "targets": -1, "sWidth": "30%"
            }
        ];
        RdottableNDW1('tblUnits', '/GetTestList', colms);
        /*Status Change on & Off*/
        $('#tblUnits').on('click', '.toggle', function () {           
            if ($(this).find("#toggle-demo").is(":Checked") == false) {
                $(this).find("#toggle-demo").prop("checked", true)
            } else {
                $(this).find("#toggle-demo").prop("checked", false)
            }
            var k = $(this).closest('td').find("input").is(":Checked");
            var row = $(this).parent('td')[0]._DT_CellIndex.row;
            var CellNo = $(this).parent('td')[0]._DT_CellIndex.column;
            var CODE = $("#tblUnits tbody tr:eq(" + row + ") td:eq(0)").text();
            var Rtest = {
                CODE: CODE,
                CellNo: CellNo,
                SingleVal: k,
                EditFlag: true
            };
            const swalWithBootstrapButtons = Swal.mixin({
                confirmButtonClass: 'btn btn-success',
                cancelButtonClass: 'btn btn-danger',
                buttonsStyling: false,
            })
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, Change it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    CODE = $(tr).find("td:eq(0)").text().trim();
                    $.post("/SaveTest", Rtest).done(function (data) {
                        if (data.ErrorMessage == "" && data.SaveFlag == true) {
                            $('#btnrelod').trigger('click');
                        } else {
                            RedDotAlert_Error(CODE);
                        }
                    });
                    swalWithBootstrapButtons.fire(
                        'Status',
                        'Your Data has been Chaged.',
                        'success'
                    )
                } else if (
                    result.dismiss === Swal.DismissReason.cancel
                ) {
                    $('#btnrelod').trigger('click');
                    swalWithBootstrapButtons.fire(
                        'Cancelled',
                        'Your Data is safe :)',
                        'error'
                    )
                }
            })                                      
        });
        /*Poppu close time refresh */
        $("#btnmodelcls").on("click", function () {
            $("#tblUnits1 tbody tr").css("background", "");
            $("#tblUnits1  thead tr").find("th:eq(3)").show();
            $("#tblUnits1").find('[id*=CODE]').removeAttr("disabled");
            $("#hdnOperationMode").val(false);
            if ($("#tblUnits1 > tbody > tr").length > 1) {
                $("#tblUnits1 tbody tr td:last-child").remove();
            }
            $("#tblUnits1 tbody").find("tr:gt(0)").remove();
            var abk1 = [];
            abk1.push("[name*='txtCODE']", "[name*='txtDESCRIPTION']");
            Rdotsettxtclr(abk1);
           
        })             
        /*Code Tab Event*/
        RdottableTabEve("#tblUnits1", "[name*='txtCODE']", "[name *= 'txtDESCRIPTION']", "Please Enter CODE", 'T', "");
        /*Enter Event*/
        RdottableLstEnt("#tblUnits1", "[name*='txtDESCRIPTION']", "[name *= 'txtCODE']", "T", "");             
         /*Add Table Change Event*/
        var counter = parseInt($("#Counter").val())+1;
        $("#tblUnits1").on("change", "[name*='txtCODE']", function () {
            
            var tr = $(this).closest('tr');
            drec = [];
            $('#tblUnits1 tbody tr td:nth-child(1)').each(function () {
                //add item to array
                var ab = $(this).find("[name *= 'txtCODE']").val();
                drec.push(ab);
            });
            drec.splice($.inArray(tr.find("[name *= 'txtCODE']").val(), drec), 1);
            if ($.inArray(tr.find("[name *= 'txtCODE']").val(), drec) >= 0) {
                var dr = tr.find("[name *= 'txtCODE']").val();
                tr.css("background", "red");
                tr.find("[name *= 'txtCODE']").focus();
                RedDotAlert_Error("Already Exist Code '" + dr + "'");
                tr.find("[name *= 'txtCODE']").val('');
            }
            else {
                tr.css("background", "");
                if (tr.is(":last-child")) {
                    debugger;
                  
                    //  n1 = parseInt(n1) + 1;
                    $('#tblUnits1 tbody tr:last').clone().insertAfter('#tblUnits1 tbody tr:last');
                    $("<td><button type='button' class='btn btn-danger' name='btnDelete" + counter + "')'><span class='fa fa-trash-alt'></span></button></td>")
                        .appendTo("#tblUnits1 tbody tr:nth-last-child(2)");
                    $(this).closest('tr').next('tr').find("[name*='txtCODE']").val('');

                    var trLast = $("#tblUnits1 tbody tr:last");
                    txtarry = [];
                    txtarry.push('txtCODE', 'txtDESCRIPTION','toggle-demo');
                    RdotTbltxtdrpidchanged(txtarry, counter, "txt", trLast);
                   // $("#toggle-demo" + counter + "").bootstrapToggle('destroy');	
                    //$("#toggle-demo" + counter + "").bootstrapToggle('destroy')	
                    //$("#toggle-demo"+counter+"").bootstrapToggle();
                    //trLast.find("#toggle-demo" + counter + "").bootstrapToggle('destroy');	
                    //trLast.find("#toggle-demo" + counter + "").bootstrapToggle();
                    trLast.find("[name*='txtDESCRIPTION']").val('');
                }
            }
            counter++;
        });              
        /*Save Unit Table */
        $("#btnSave").click(function () {
            debugger;
            var Rtest = {
                EditFlag: $("#hdnOperationMode").val(),
                RDD_TestDetailnew: []
            };
            $("#tblUnits1 tbody tr").each(function (index, item) {
                var CODE = $(this).find("[name*='txtCODE']").val();
                var DESCRIPTION = $(this).find("[name*='txtDESCRIPTION']").val();
                var IsDefault = $(this).find("[id*='toggle-demo']").is(":Checked");
                if ((CODE != "") && (DESCRIPTION != "")) {
                    var RDD_TestDetail = {
                        CODE: CODE,
                        DESCRIPTION: DESCRIPTION,
                        IsDefault: IsDefault
                    };
                    Rtest.RDD_TestDetailnew.push(RDD_TestDetail);
                }
            });
            var validationResult = ValidateForm(Rtest);
            if (validationResult.formValid) {
                $.post("/SaveTest", Rtest).done(function (response) {
                    debugger;
                    if (response.Drecord.length == 0) {    
                        $("#hdnOperationMode").val(false);
                        $('#btnmodelcls').trigger("click");
                        response.Drecord = 0;                        
                        if ($("#tblUnits1 > tbody > tr").length > 1) {
                            $("#tblUnits1 tbody tr td:last-child").remove();
                        }
                        $("#tblUnits1 tbody").find("tr:gt(0)").remove();
                        if (response.EditFlag == true) {
                            RdotAlertUpd("Succesfully");
                        } else {
                            RedDotAlert_Success("Save Succcesfully");

                        }                        
                        $('#btnrelod').trigger('click');
                        var abk1 = [];
                        abk1.push("[name*='txtCODE']", "[name*='txtDESCRIPTION']");
                        Rdotsettxtclr(abk1);                       
                    } else {
                        $("#tblUnits1 tbody tr").each(function (index, item) {
                            var tr = $(this).closest("tr");
                            var CODE = tr.find("[name*='txtCODE']").val();
                            var DESCRIPTION = tr.find("[name*='txtDESCRIPTION']").val();
                            if (response.Drecord.length > 0) {
                                var ary1 = [];
                                ary1 = response.Drecord;
                                if ($.inArray(CODE, ary1) < 0 && CODE != '' && DESCRIPTION != '') {
                                    $(this).closest('tr').remove();                                   
                                    $('#btnrelod').trigger('click');                                   
                                }
                                else if (CODE != '') {
                                    $(this).closest('tr').css("background", "red");
                                    RedDotAlert_Error("Already Exists Code");
                                }
                                else {
                                    $(this).closest('tr').css("background", "");
                                }
                            }
                            else {
                                $("#tblUnits1 tbody").find("tr:not(:last)").remove();
                                $("[name*='txtCODE']").val();                              
                                if (CODE != '' && DESCRIPTION != '') {                                  
                                    $('#btnrelod').trigger('click');                                  
                                }                               
                                RedDotAlert_Success("Save successfully");
                                $(".dataTables_empty").replaceWith("");
                            }
                        });
                    }
                   
                });
            }
            else {
                RdotAlerterr();
            }
        });
        function ValidateForm(Rtest) {
            var response = {
                ErrorMessage: "",
                formValid: false
            };

            if (Rtest.RDD_TestDetailnew.length == 0) {
                response.ErrorMessage += "Please Enter atleast One Branch code Details";
            }
            if (response.ErrorMessage.length == 0) {
                response.formValid = true;
            }

            return response;
        }
        /*Popup Base Delete Operation*/
        $("#tblUnits1").on("click", "[name*='btnDelete']", function () {
            $(this).closest("tr").remove();
        });
        $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
            console.log(thrownError);
        });
       /*Cell Base Edit & Save on Table*/
        $('#tblUnits').on('click', 'tbody td:not(:first-child,:last-child,:nth-last-child(2))', function () {           
                if ($(this).find('input').is(':focus')) return this;
                var cell = $(this);
                var content = $(this).html();
                $(this).html('<input type="text" value="' + $(this).html() + '" />')
                    .find('input')
                    .trigger('focus')
                    .on({
                        'blur': function () {
                            $(this).trigger('closeEditable');
                        },
                        'keyup': function (e) {
                            if (e.which == '13') { // enter                                
                                $(this).trigger('saveEditable');
                            } else if (e.which == '27') { // escape
                                $(this).trigger('closeEditable');
                            }
                        },
                        'closeEditable': function () {
                            cell.html(content);
                        },
                        'saveEditable': function (e) { 
                            debugger;
                            var k = $(this).closest('td').find("input").val();   
                            var row = cell[0]._DT_CellIndex.row;
                            var CellNo = cell[0]._DT_CellIndex.column;
                            var CODE = $("#tblUnits tbody tr:eq(" + row + ") td:eq(0)").text();
                var Rtest = {
                    CODE: CODE,
                    CellNo: CellNo,
                    SingleVal: k,
                    EditFlag: true
                };               
                    $.post("/SaveTest", Rtest).done(function (response) {                        
                        if (response.ErrorMessage == "" && response.SaveFlag == true) { 
                            RedDotAlert_Success("Update successfully");
                            $('#btnrelod').trigger('click');                                                                                                                                  
                        }
                    });                                         
                        }
                    });
            });
        /*Edit Mode Opertion Popup Open*/
        $("#tblUnits").on("click", "[name^='Edit']", function () {               
            var tr = $(this).closest("tr"); 
            $("#tblUnits1  thead tr").find("th:eq(3)").hide();
                    $('#ModulesPopUp').modal("show");
                    var DESCRIPTION = $(tr).find("td:eq(1)").text();
                    var Code = $(tr).find("td:eq(0)").text();                   
                    $("#tblUnits1  tbody tr").find('[id*=txtCODE]').val(Code);
                    $("#tblUnits1  tbody tr").find('[id*=txtCODE]').attr("disabled", true);
                    $("#tblUnits1  tbody tr").find('[id*=txtDESCRIPTION]').val(DESCRIPTION);
                    $("#hdnOperationMode").val(true);                                                                 
        })
        /*Delete Mode Operation*/
        $("#tblUnits").on("click", "[name^='Delete']", function () {
                    var tr = $(this).closest("tr");               
                    const swalWithBootstrapButtons = Swal.mixin({
                        confirmButtonClass: 'btn btn-success',
                        cancelButtonClass: 'btn btn-danger',
                        buttonsStyling: false,
                    })
                    swalWithBootstrapButtons.fire({
                        title: 'Are you sure?',
                        text: "You won't be able to revert this!",
                        type: 'warning',
                        showCancelButton: true,
                        confirmButtonText: 'Yes, delete it!',
                        cancelButtonText: 'No, cancel!',
                        reverseButtons: true
                    }).then((result) => {
                        if (result.value) {
                            CODE = $(tr).find("td:eq(0)").text().trim();
                            $.getJSON("/DeleteFlag", { Code: CODE }).done(function (data) {
                                if (data.deleteFlag) {
                                    $('#btnrelod').trigger('click');                                    
                                } else {
                                    RdotAlertdele(doccode);
                                }
                            });
                            swalWithBootstrapButtons.fire(
                                'Deleted!',
                                'Your Data has been deleted.',
                                'success'
                            )
                        } else if (                          
                            result.dismiss === Swal.DismissReason.cancel
                        ) {
                            swalWithBootstrapButtons.fire(
                                'Cancelled',
                                'Your Data is safe :)',
                                'error'
                            )
                        }
                    })                                    
        });      
    }
}