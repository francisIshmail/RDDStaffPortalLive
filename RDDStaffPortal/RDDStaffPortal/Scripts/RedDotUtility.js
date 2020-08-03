

function RdotAlertdele(code) {
    Swal.queue([{
        type: 'error',
        title: 'Oops...',
        text: 'Already use ' + code + ' code!',
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}
function RdotAlerterr() {
    Swal.queue([{
        type: 'error',
        title: 'Oops...',
        text: 'Enter Valid Data!',
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}
function RdotAlertUpd(code) {
    Swal.queue([{
        type: 'success',
        title: 'Success..',
        text: 'Updated ' + code + ' code!',
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}
function RdotAlerterrDt() {
    Swal.queue([{
        type: 'error',
        title: 'Oops...',
        text: 'Enter Valid Date!',
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}

function RdotAlerterrtxt1(txt) {
    // swal({

    swal("Warning", '<p style="font-size: 12px;text-align:center">' + txt + '</p>', "warning")

    //    title: "Are you sure?",
    //    text: "Your will not be able to recover this imaginary file!",
    //    type: "warning",
    //    showCancelButton: true,
    //    confirmButtonClass: "btn-danger",
    //    confirmButtonText: "Yes, delete it!",
    //    closeOnConfirm: false
    //});
}


function RdotAlerterrtxt(txt) {
    Swal.queue([{
        type: 'error',
        title: 'Oops...',
        html: '<p style="font-size: 12px;text-align:center">' + txt + '</p>',
        // text:txt,        
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}
function RdotAlertSucesstxt(txt) {
    Swal.queue([{
        type: 'success',
        title: 'Success..',
        html: '<p style="font-size: 12px;text-align:center">' + txt + '</p>',
        // text:txt,        
        allowOutsideClick: false,
        showLoaderOnConfirm: true,
    }])
}

function RdotTbltxtdrpidchanged(txtid, counter, typ, trLast) {

    var n = txtid.length;

    while (n > 0) {

        trLast.find("[name *= '" + txtid[n - 1] + "']").attr(
            "name", "" + txtid[n - 1] + "" + counter
        );
        trLast.find("[id *= '" + txtid[n - 1] + "']").attr(
            "id", "" + txtid[n - 1] + "" + counter
        );
        if (typ == "drp") {
            trLast.find("[data-id *= '" + txtid[n - 1] + "']").attr(
                "data-id", "" + txtid[n - 1] + "" + counter
            );
            //trLast.find("[id *= '" + txtid[n - 1] + "']").attr(
            //       "data-width", "fit"
            //   );


        }
        n--;
    }

}


function Rdotprop(Ideary, propvalue, tf) {

    var n = Ideary.length;
    while (n > 0) {
        $(Ideary[n - 1]).prop(propvalue, tf);
        n--;
    }
}
function Rdotsettxtclr(Ideary) {
    var n = Ideary.length;
    while (n > 0) {
        $(Ideary[n - 1]).val('');
        n--;
    }
}
function RdottableNDW(tblid, url1, colms) {
    //$('#' + tblid + ' tfoot th').each(function () {
    //    var title = $('#' + tblid + ' tfoot th').eq($(this).index()).text();
    //    if (title) {
    //        $(this).html('<input type="text" class="grid-control"  placeholder="Search"  />');
    //    }
    //});
    var table = $('#' + tblid + '').DataTable({
        "ColumnDefs": [{ "searchable": false, "orderable": true, "targets": [0] }],
        "order": [[0, 'asc']],
        //"scrollY": true,
        //  "scrollX": true,
        "filter": true,
        "paging": true,
        "pageLength": 20,
        "ordering": true,
        "info": true,
        "dataSrc": "",
        "language":
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
        },
        "dom": 'lBfrtip',
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "buttons": [
            'excel',
            // 'pdf',
            {
                text: 'Reload Data',
                attr: {
                    id: 'btnreload'
                },
                action: function () {
                    table.ajax.reload();
                },
            }
        ],
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ajax":
        {
            "async": false,
            "cache": false,
            "type": "POST",
            "url": url1,
            //  "data": newrow,
            "dataType": "json",
        },

        "aoColumns": colms
        //, "fnDrawCallback": function () {
        //    $("input[id='chkIsDefault']").bootstrapToggle();
        //}
    });

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();
    $.fn.dataTable.ext.errMode = function (settings, helpPage, message) {
        debugger

        RedDotAlert_Error(message);

        location.reload("/login/login");
    };
    table.columns().eq(0).each(function (colIdx) {
        $('input', $('#' + tblid + ' tfoot th')[colIdx]).bind('keyup', function () {
            debugger;
            var coltext = this.value; // typed value in the search column
            var colindex = colIdx; // column index
            delay(function () {
                table
                    .column(colindex)
                    .search(coltext)
                    .draw();
            }, 500);
        });
    });

    var r = $('#' + tblid + ' tfoot th');
    r.find('th').each(function () {
        jq(this).css('padding', 8);
    });
    $('#' + tblid + ' thead').append(r);
    $('#search_0').css('text-align', 'center');

    $('div.dataTables_filter input').addClass('form-control input-sm');

    $('#' + tblid + '_length').hide();

    // $("div.dataTables_filter").append($("<button  id='capture' value='true' class='hb2Smf'><i style='font-size:20px;color:#4285F4' class='fa fa-microphone'></i></button>"));
}

function RdottableNDW1(tblid, url1, colms) {

    var table = $('#' + tblid + '').DataTable({
        "ColumnDefs": [{ "searchable": true, "orderable": true, "targets": [0] }],

        "order": [[0, 'asc']],
        //"scrollY": true,
        //  "scrollX": true,
        "filter": true,
        "paging": true,
        "ordering": true,
        "info": true,
        "dataSrc": "",

        "language":
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
        },
        "dom": 'lBfrtip',
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]],
        "buttons": [
            'excel',
            // 'pdf',
            {
                text: '<i class="fa fa-fa fa-recycle"></i>',

                attr: {
                    id: 'btnrelod'

                },
                action: function () {
                    table.ajax.reload();
                },
            }
        ],
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ajax":
        {
            "async": false,
            "cache": false,
            "type": "POST",
            "url": url1,
            //  "data": newrow,
            "dataType": "json",
        },

        "aoColumns": colms,
        rowCallback: function (row, data) {
            $('input.toggle', row).prop('checked', data.IsDefault == 1).bootstrapToggle({ size: 'mini' });
        }
    });



    $('div.dataTables_filter input').addClass('form-control input-sm');

    $('#' + tblid + '_length').hide();

    $("div.dataTables_filter").append($("<button  id='capture' value='true' class='hb2Smf'><i style='font-size:20px;color:#4285F4' class='fa fa-microphone'></i></button>"));

}

function RdottableDash(tblid, url1, colms) {
    $('#' + tblid + '').DataTable({
        "searching": false,
        "processing": true,
        "serverSide": true,
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "ajax": url1,
        "columns": colms
    });
}



function RdottableNDWPara1(tblid, url1, colms, Code, pageL) {
    var newrow = {
        Code: Code
    }
    var table = $('#' + tblid + '').DataTable({
        "ColumnDefs": [{ "searchable": false, "orderable": true, "targets": [0] }],
        "order": [[0, 'asc']],
        //"scrollY": true,
        //  "scrollX": true,
        "filter": true,
        // "paging": true,
        "pageLength": pageL,
        "ordering": true,
        "info": false,
        "rowReorder": {
            dataSrc: 'id',
            selector: 'tr'
        },
        "language":
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
            //"paginate": {
            //    next: '&#8594;', // or '→'
            //    previous: '&#8592;' // or '←' 
            //}
        },
        "dom": 'lBfrtip',
        "pagingType": "simple",
        // "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "All"]], 
        "buttons": [

            {
                text: 'Reload Data',
                attr: {
                    id: 'btnreload',
                    style: 'display:none;'

                },
                className: 'reloadcss',
                action: function () {
                    table.ajax.reload();
                },
            }
        ],
        "processing": true,
        "serverSide": true,
        "destroy": true,
        "ajax":
        {
            "async": false,
            "cache": false,
            "type": "POST",
            "url": url1,
            "data": newrow,
            "dataType": "json",
        },

        "aoColumns": colms
    });


    $('#txtsearch').keyup(function () {
        table.search($(this).val()).draw();
        $("#product  li").draggable({

            // brings the item back to its place when dragging is over
            revert: true,

            // once the dragging starts, we decrease the opactiy of other items
            // Appending a class as we do that with CSS
            drag: function (event, ui) {
                debugger
                ui.helper.data('dropped', false);
                $(this).addClass("active");
                $(this).closest("#product").addClass("active");

            },

            // removing the CSS classes once dragging is over.
            stop: function (event, ui) {
                //alert('stop: dropped=' + ui.helper.data('dropped'));
                $(this).removeClass("active").closest("#product").removeClass("active");

            }
        });


    })


    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();



    $(".dataTables_filter").hide();

    $('#' + tblid + '_length').hide();


}
var RdotMMNames1 = ["01", "02", "03", "04", "05", "06",
    "07", "08", "09", "10", "11", "12"];
/*json date format dd-MMM-yyyy*/
function RdotdatefrmtRes2(dte) {
    var now = new Date(parseInt(dte.substr(6)));
    var now = new Date(now);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + RdotMMNames1[month - 1] + "-" + now.getFullYear();
    return today;
}

var RdotMMNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
    "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
/*json date format dd-MMM-yyyy*/
function RdotdatefrmtRes1(dte) {
    var now = new Date(parseInt(dte.substr(6)));
    var now = new Date(now);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + RdotMMNames[month - 1] + "-" + now.getFullYear();
    return today;
}

function RedDot_setdtpkdate(date1) {
    if (date1 !== undefined ) {
        var d = new Date(date1.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;
        return newdate;
    }
   
}

//Table Number  N & Text T Tab Event
function RdottableTabEve(tbl, ide, idf, errmsg, typ, vtyp) {
    $(tbl).on("keydown", ide, function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            var tr = $(this).closest('tr');
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab != vtyp) {
                tr.find(idf).focus();
                tr.css("background", "");
            }
            else {
                tr.css("background", "red");
                tr.find(ide).focus();
                RedDotAlert_Error(errmsg);
            }
        }
    });
}
/*Lenght decide table inside input */
function RdottableTabEveOne(tbl, ide, idf, errmsg, typ, vtyp, lent) {
    $(tbl).on("keydown", ide, function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            var tr = $(this).closest('tr');
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab.length <= lent) {
                tr.find(idf).focus();
                tr.css("background", "");
            }
            else {
                tr.find(ide).val(ab.slice(0, 1 - ab.length));
                tr.css("background", "red");
                tr.find(ide).focus();
                RedDotAlert_Error(errmsg);
            }
        }
    });
}

/*Last Column Enter Event*/
function RdottableLstEnt(tbl, ide, idf, errmsg, typ, vtyp) {
    $(tbl).on("keypress", ide, function (e) {

        if (e.keyCode == 13) {
            var tr = $(this).closest('tr');
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab != vtyp) {
                tr.find(idf).focus();
                tr.css("background", "");
                $(this).closest('tr').next('tr').find(idf).focus();
            }
            else {
                tr.css("background", "red");
                tr.find(ide).focus();
                RedDotAlert_Error(errmsg);
            }
            return false;
        }
    });
}

function RdottableLstTabBlk(tbl, ide, idf, errmsg, typ, vtyp) {

    $(tbl).on("keydown", ide, function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            var tr = $(this).closest('tr');
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab != "") {
                tr.find(idf).focus();
                tr.css("background", "");
                $(this).closest('tr').next('tr').find(idf).focus();
            }
            else {
                tr.css("background", "red");
                tr.find(ide).focus();
                RedDotAlert_Error(errmsg);
            }

        }
    });
}
/*Last Column Tab Event*/
function RdottableLstTab(tbl, ide, idf) {
    $(tbl).on("keypress", ide, function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            $(this).closest('tr').next('tr').find(idf).focus();
        }
    });
}

var t = false;
function RdottableDateCondion(tbl, ide, idf) {
    $(tbl).on('dp.change', ide, function (e) {

        var fstyp = "";
        if (e.oldDate !== null) {
            var tr = $(this).closest("tr");
            var Edt = new Date(Rdotsetdtpkdate(tr.find(ide).val()));
            var Effdt = new Date(Rdotsetdtpkdate(tr.find(idf).val()));
            if (fstyp = 'E') {
                if ((t == false) && (new Date(Rdotsetdtpkdate(tr.find(ide).val())) >= new Date(Rdotsetdtpkdate(tr.find(idf).val())))) {//compare end <=, not >=
                    tr.find(ide).val(tr.find(idf).val());
                    t = true;
                    RedDotAlert_InvalidDate();
                }
                else {
                    t = false;
                }
            } else {
                if ((t == false) && (new Date(Rdotsetdtpkdate(tr.find(ide).val())) <= new Date(Rdotsetdtpkdate(tr.find(idf).val())))) {//compare end <=, not >=
                    tr.find(ide).val(tr.find(idf).val());
                    t = true;
                    RedDotAlert_InvalidDate();
                }
                else {
                    t = false;
                }
            }


        }

    });
}
/* date formate yy/mm/dd */
function Rdotsetdtpkdate(date1) {

    var d = new Date(date1.split("/").reverse().join("-"));
    var dd = d.getDate();
    var mm = d.getMonth() + 1;
    var yy = d.getFullYear();
    var newdate = yy + "/" + mm + "/" + dd;
    return newdate;
}

/* delete grid record */
function RdotGriddel(tbl, btndel) {
    $(tbl).on("click", btndel, function () {
        //  drec.pop($(this).find(ide).val());
        // counter1--;
        $(this).closest("tr").remove();
    });
}
function RdotTableRowDel(tbl, btndel) {
    $(tbl).on("click", btndel, function () {
        $(this).closest("tr").remove();
    });
}
function RdotDropimg(ids, url) {

    $.getJSON(url).done(function (data) {
        $('#' + ids + '').empty();
        $('#' + ids + '').append('<option value="0" selected="">-Select-</option>');
        var ary = [];
        ary = data;
        for (var i = 0; i < ary.length; i++) {
            $('#' + ids + '').append('<option value="' + ary[i].Code + '" selected="" data-class="avatar" data-style="data:image/jpeg;base64,' + ary[i].imagepath + '" >' + ary[i].CodeName + '</option>');
        }
        $('#' + ids + '').val(0);
        // $('#Userid').selectpicker('refresh');
    });
    $('#' + ids + '').select2({
        theme: "bootstrap",
        templateSelection: formatState,
        templateResult: formatState
    });

    function formatState(state) {
        if (!state.id) { return state.text; }

        var optimage = $(state.element).attr('data-style');
        var $state = '';
        if (optimage != undefined) {
            $state = $(
                '<span ><img sytle="display: inline-block;" src="' + optimage + '" width="30px" />' + state.text + '</span>'
            );
        }
        return $state;
    }
}



function RdotDropimg1(ids, url, path) {
    $.getJSON(url).done(function (data) {
        $('' + ids + '').empty();
        $('' + ids + '').append('<option value="0" selected="">-Select-</option>');
        var ary = [];
        ary = data;
        for (var i = 0; i < ary.length; i++) {
            $('' + ids + '').append('<option value="' + ary[i].CodeName + '" text="' + ary[i].CodeName + '"  data-class="avatar" data-image="' + path + '' + ary[i].Code + '.png" >' + ary[i].CodeName + '</option>');
        }
        $('' + ids + '').val(0);
        // $('#Userid').selectpicker('refresh');
    });
    $('' + ids + '').select2({
        theme: "bootstrap",
        templateSelection: formatState,
        templateResult: formatState
    });

    function formatState(state) {
        if (!state.id) { return state.text; }

        var optimage = $(state.element).attr('data-image');
        var $state = '';
        if (optimage != undefined) {
            $state = $(
                '<span ><img sytle="display: inline-block;" src="' + optimage + '" width="30px" />' + state.text + '</span>'
            );
        }
        return $state;
    }
}

// This function is to show sucess message

function RedDotAlert_Success(message) {

    Swal.queue([{

        type: 'success',

        title: 'Success..',

        html: '<p style="font-size: 12px;text-align:center">' + message + '</p>',

        allowOutsideClick: false,

        showLoaderOnConfirm: true,

    }])

}



// This function is to show error message

function RedDotAlert_Error(message) {

    Swal.queue([{

        type: 'error',

        title: 'Oops...',

        html: '<p style="font-size: 12px;text-align:center">' + message + '</p>',

        allowOutsideClick: false,

        showLoaderOnConfirm: true,

    }])

}



// This function is to shoq Warning error message

function RedDotAlert_Warning(message) {

    swal("Warning", '<p style="font-size: 12px;text-align:center">' + message + '</p>', "warning")

}



// This function is to show Invalid date message

function RedDotAlert_InvalidDate(message) {

    Swal.queue([{

        type: 'error',

        title: 'Oops...',

        text: message,

        allowOutsideClick: false,

        showLoaderOnConfirm: true,

    }])

}

function RedDot_NumberFormat(value) {
    debugger
    var values = 0;
    var abr = "";
    if (value < 0) {
        value = -(value);
        abr = '-';
    } else if (value == 0) {
        return '$ ' + value.toString();
    }
    if (value >= 1000000000) {
        values = (value / 1000000000).toFixed(2) + ' B';
    } else if (value >= 1000000) {
        values = (value / 1000000).toFixed(2) + ' M';
    } else if (value >= 1000) {
        values = (value / 1000).toFixed(2) + ' K';
    }
    
    return abr + '' + values.toString();
}


function RedtDot_CheckAuthorization(url) {
    var t = false;
    var data = JSON.stringify({
        url: url,
    });
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        data: data,
        url: "/CheckAuthorization",
        dataType: 'Json',
        contentType: "Application/json",
        dataType: 'JSON',
        success: function (response) {
            debugger
            if (response.data > 0) {
                t = true;
            } else {
                window.location.href = "/Dashboard/ErrorPage";
            }
        }
    })
    return t;
}


function RedDot_Button_Init_HideShow() {
    $("#btnAdd").show();
    $("#btnSave").hide();
    $("#btnCancel").hide();
    $("#btnSendMail").hide();
    $("#tblid").show();
    $("#tblid1").show();
    
}
function RedDot_Button_New_HideShow() {
    $("#btnAdd").hide();
    $("#btnSave").show();
    $("#btnCancel").show();
    $("#btnSendMail").show();
    $("#tblid").hide();
    $("#tblid1").hide();

}


function RedDot_Table_Attribute(tr,tblDt,count1,tblclass) {
    var i = 0;    
    $(tblclass).each(function () {
        while (tblDt.length > i) {
            tr.find('.Abcd input[id^="' + tblDt[i] + '"]').attr("id", '' + tblDt[i] + '' + count1);
            tr.find('.Abcd input[id^="' + tblDt[i] + '"]').attr("name", '' + tblDt[i] + '' + count1);
            tr.find('.Abcd input[id^="' + tblDt[i] + '"]').val('');
            i++;
        }
        tr.find('.Abcd input[id^="' + tblDt[0] + '"]').val(count1);
       
    });
    $('#' + hdnid+'').val(count1);
   

    
}

function RedDot_Table_HiddenAttribute(tr, tblhidden, k, tblclass) {
    debugger
    var i = 0;   
    $(tblclass).each(function () {
        while (tblhidden.length > i) {
            tr.find('.Abcd input[id^="' + tblhidden[i] + '"]').attr("id", '' + tblhidden[i] + '' + k);
            tr.find('.Abcd input[id^="' + tblhidden[i] + '"]').attr("name", '' + tblhidden[i] + '' + k);
            tr.find('.Abcd input[id^="' + tblhidden[i] + '"]').val('');
            tr.find('.Abcd input[id^="' + tblhidden[i] + '"]').removeClass('ui-autocomplete-input');
            i++;
        }       
    });
}

function applyAutoComplete2(ids, hdnid,url) {
    $(ids).autocomplete({
        source: function (request, response) {
            $.ajax({
                async: false,
                cache: false,
                url: url,
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    $(hdnid).val(-1);
                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            return {
                                label: item.CodeName,
                                value: item.CodeName,
                                val1: item.Code

                            };
                        }))
                    } else {

                        response([{ label: 'No results found.', value: 'No results found.' }]);
                    }
                }
            });
        },
        // autoFocus: true,
        select: function (event, u) {
            event.preventDefault();
            debugger
            var v = u.item.val1;
            if (u.item.val1 == -1 || u.item.val1 == '') {
                $(hdnid).val(-1);
                return false;
            } else {
                $(ids).val(u.item.value);
                $(hdnid).val(u.item.val1);

            }
        },
        minLength: 1
    });
    $(ids).on("change", function () {
        if ($(hdnid).val() == -1) {
            $(this).val('');
        }
    })
}

function applyAutoComplete2(ids, hdnid) {
    $(ids).autocomplete({
        source: function (request, response) {
            $.ajax({
                async: false,
                cache: false,
                url: "/GetUserListAuto",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    if (data.length > 0) {
                        response($.map(data, function (item) {
                            return {
                                label: item.CodeName,
                                value: item.CodeName,
                                val1: item.Code

                            };
                        }))
                    } else {
                        $(ids).val('');
                        $(hdnid).val(-1);
                        response([{ label: 'No results found.', value: 'No results found.' }]);
                    }
                }
            });
        },
        // autoFocus: true,
        select: function (event, u) {
            debugger
            var v = u.item.val1;
            if (u.item.val1 == -1 || u.item.val1 == '') {
                $(hdnid).val(-1);
                return false;
            } else {
                $(hdnid).val(u.item.val1);

            }
        },
        minLength: 1
    })
}
function RedDot_Table_DeleteActivity(tr, tblDt, tblclass, hdnid) {
    tr.remove();
    var k = 1;
    $(tblclass).each(function () {         
        var i = 0;  
        while (tblDt.length > i) {           
            $(this).find('.Abcd input[id^="' + tblDt[i]+ '"]').attr("id", '' + tblDt[i] + '' + k);
            $(this).find('.Abcd input[id^="' + tblDt[i] + '"]').attr("name", '' + tblDt[i] + '' + k);                 
            i++;
        }
        $(this).find('.Abcd input[id^="' + tblDt[0] + '"]').val(k)             
        k++;
    });
      
   
    $('#' + hdnid + '').val(k-1);
    

}

function RedDot_Table_DeleteHiddenActivity(tblhidden, tblclass) {       
    var i = 0;
    var k = 1;
    $(tblclass).each(function () {
        while (tblhidden.length > i) {
            $(this).find('.Abcd input[id^="' + tblhidden[i] + '"]').attr("id", '' + tblhidden[i] + '' + k);
            $(this).find('.Abcd input[id^="' + tblhidden[i] + '"]').attr("name", '' + tblhidden[i] + '' + k);
            i++;
        }      
        k++;
    });
}

/*tbl class date formate DD/MM/YYYY get input */
function RedDot_tbldtpicker() {
    $('.datepicker').datetimepicker({
        defaultDate: new Date(),
        format: 'DD/MM/YYYY',
        showClose: true,
        showClear: true,
        // minDate: new Date(),

    });
}

/* Table date formate dd-MM-yyyy get input date */
function RedDot_Table_dateEdit(tr,date1, dtval) {
    debugger
    var now = new Date(tr.find(dtval).val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "/" + (month) + "/" + now.getFullYear();
    tr.find(date1).val(today);
}
/* date formate dd-MM-yyyy get input date */
function RedDot_dateEdit(date1, dtval) { 
   
    var now = new Date($(dtval).val());
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "/" + (month) + "/" + now.getFullYear();
    $(date1).val(today);
}
function RedDot_NewDate(dteclass) {
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "/" + (month) + "/" + now.getFullYear();
    $(dteclass).val(today);
}
/*New Date Range*/
function RedDot_DateRange(id) {
    var start = moment().subtract(29, 'days');
    var end = moment();


    $('#'+ id +'').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);
    
}
/* date formate dd/MM/yyyy set Table*/
function RedDot_DateTblEdit(dtval) {

    var now = new Date(dtval);
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "/" + (month) + "/" + now.getFullYear();
    return today;
}


/*Last Column Enter Event*/
function RedDot_tableLstEnt(tbl, ide, idf, errmsg, typ, vtyp) {
    $(tbl).on("keypress", ide, function (e) {
        debugger
        if (e.keyCode == 13) {
            var tr = $(this).closest(tbl);
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab != vtyp) {
                tr.find(idf).focus();
                tr.css("background", "");
                $(this).closest(tbl).next(tbl).find(idf).focus();
            }
            else {
                tr.css("background", "red");
                RedDotAlert_Error(errmsg);
                tr.find(ide).focus();
                
               
            }
            return false;
        }
    });
}

//Table Number  N & Text T Tab Event
function RedDot_tableTabEve(tbl, ide, idf, errmsg, typ, vtyp) {
    $(tbl).on("keydown", ide, function (e) {

        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            var tr = $(this).closest(tbl);
            var ab = '';
            if (typ == "N") {
                ab = parseInt(tr.find(ide).val()) || 0;
            } else {
                ab = tr.find(ide).val();
            }
            if (ab != vtyp) {
                tr.find(idf).focus();
                tr.css("background", "");
            }
            else {
                tr.css("background", "red");
                tr.find(ide).focus();
                RedDotAlert_Error(errmsg);
            }
        }
    });
}


function RedDot_DivTable_Fill(Ids,url, data, dateCond, tblhead1, tblhide, tblhead2) {
    var arr = [];
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: url,
        contentType: "application/json",
        dataType: "json",
        data: data,
        success: function (data) {
            debugger
            $('#'+Ids+'st').show();
            $('div#' + Ids +'st').not(':first').remove();
            arr = data;
            if (arr.data != null && arr.data.length != 0) {
                var i = 0;
                while (arr.data.length > i) {
                    var tr = $('#' + Ids + 'st').clone();
                    var tr1 = $('#' + Ids + 'nd').closest();
                    var k = 0;
                    var l1 = tr.find(".Abcd").length;
                    while (l1 > k) {
                        var t = tblhead1[k];
                        if (jQuery.inArray(t, dateCond) !== -1) {
                           
                            tr.find(".Abcd")[k].children[0].textContent = RdotdatefrmtRes1(arr.data[i][tblhead1[k]]);
                        } else {
                            tr.find(".Abcd")[k].children[0].textContent = arr.data[i][tblhead1[k]];
                        }                                              
                        if (jQuery.inArray(t, tblhide) !== -1) {
                            tr.find(".Abcd").eq(k).addClass("Abc")
                            tr1.prevObject.find(".reddotTableHead").eq(k).addClass("Abc")
                        }
                        k++;
                    }
                    $('#' + Ids + 'body').append(tr);
                    i++;
                }
                $('#' + Ids + 'st')[0].remove();
            } else {
                $('#' + Ids + 'st').hide();
                RedDotAlert_Error("No Record Found");
            }
        }, complete: function () {
            $(".loader1").hide();            
        }
    });
    return arr;    
}

function RedDot_AutotxtEventTbl1(Ids, EveNames, inpid, inphid, urls, txtsno) {
   
    $(document).on(EveNames, Ids+"[name^='" + inpid + "']", function () {
        debugger
        var tr = $(this).closest(Ids);
        var inp1 = tr.find(txtsno).val();                   
        $("#" + inpid + "" + inp1).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: urls,
                    type: "POST",
                    dataType: "json",
                    data: { Prefix: request.term },
                    success: function (data) {
                        if (data.length > 0) {
                            response($.map(data, function (item) {
                                return {
                                    label: item.CodeName,
                                    value: item.CodeName,
                                    val1:item.Code
                                  
                                };
                            }))
                        } else {
                            tr.find("#" + inpid + "" + inp1).val('');
                            tr.find(inphid).val(-1);
                            response([{ label: 'No results found.', value: 'No results found.' }]);
                        }
                    }
                });
            },
            // autoFocus: true,
            select: function (event, u) {
                var v = u.item.val1;
                if (u.item.val1 == -1 || u.item.val1 == '') {
                    tr.find(inphid).val(-1);                    
                    return false;
                } else {
                    tr.find(inphid).val(u.item.val1);
                   
                }                
            },
            minLength: 1
        }).focus(function (e, u) {
            tr.find(this).autocomplete("search", "");
        });
      
    })

}