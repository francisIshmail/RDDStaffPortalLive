

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

    swal("Warning",'<p style="font-size: 12px;text-align:center">' + txt + '</p>', "warning")

    //    title: "Are you sure?",
    //    text: "Your will not be able to recover this imaginary file!",
    //    type: "warning",
    //    showCancelButton: true,
    //    confirmButtonClass: "btn-danger",
    //    confirmButtonText: "Yes, delete it!",
    //    closeOnConfirm: false
    //});
}


function  RdotAlerterrtxt(txt) {
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
    });

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

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
    $('#' + tblid+'').DataTable({
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
                RdotAlerterrtxt(errmsg);
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
                RdotAlerterrtxt(errmsg);
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
                RdotAlerterrtxt(errmsg);
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
                RdotAlerterrtxt(errmsg);
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
                    RdotAlerterrDt();
                }
                else {
                    t = false;
                }
            } else {
                if ((t == false) && (new Date(Rdotsetdtpkdate(tr.find(ide).val())) <= new Date(Rdotsetdtpkdate(tr.find(idf).val())))) {//compare end <=, not >=
                    tr.find(ide).val(tr.find(idf).val());
                    t = true;
                    RdotAlerterrDt();
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
