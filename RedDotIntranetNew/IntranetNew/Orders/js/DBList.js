var DBList = function () { };

DBList.prototype = {

    Init: function () {

        DBList.prototype.ControlInit();


    },

    ControlInit: function () {

        $("[id$=pgHeader]").html('<span>User Wise DataBase SetUp</span>');
        debugger;
        $.ajax({
            url: "SalesOrderList.aspx/GetUserList",
            data: "{'prefix':'1'}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (value) {
                var jData = eval('(' + value.d + ')');
                debugger;

                DBList.prototype.BindGrid(jData.table.rows);
            },
            error: function (response) {
                alert(response.responseText);
            },
            failure: function (response) {
                alert(response.responseText);
            }
        });
    },

    BindGrid: function (jdata) {
        //Create a HTML Table element.
        var table = $("#gvDB");
        table.find("tr:not(:first)").remove();
        //Get the count of columns.
        var columnCount = jdata.length;
        //Add the data rows.

        for (var i = 0; i < jdata.length; i++) {
            debugger;
            row = $(table[0].insertRow(-1));
            var cell = $("<td />"); cell.html(i + 1); row.append(cell);
            //            var cell = $("<td />"); cell.html("<a href=" + (i + 1) + ">Edit</a>"); row.append(cell);
            var cell = $("<td />"); cell.html(jdata[i].usercode); row.append(cell);
            var cell = $("<td />"); cell.html("<input type='checkbox' checked='false'/>"); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].description); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].qty); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].price); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].dis); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].taxcode); row.append(cell);
            //            var cell = $("<td style='display:none;' />"); cell.html(jdata[i].taxrate); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].total); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].whscode); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].qtyinwhs); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].qtyaval); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].opgrefalpha); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].gp); row.append(cell);
            //            var cell = $("<td />"); cell.html(jdata[i].gpper); row.append(cell);


        }

    }
}

$(document).ready(function () {

    var DBList_obj = new DBList();
    DBList_obj.Init();

});