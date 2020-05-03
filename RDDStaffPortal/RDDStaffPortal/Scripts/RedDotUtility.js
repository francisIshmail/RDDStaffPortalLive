

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
