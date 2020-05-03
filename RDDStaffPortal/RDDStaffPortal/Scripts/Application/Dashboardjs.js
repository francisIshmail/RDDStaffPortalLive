var DashBoard = {
    initialize: function () {

        $.fn.dataTable.ext.errorMode = 'none';
        DashBoard.Attachevent();
    },
	Attachevent: function () {
		
		$("#Firstcard").each(function (index, item) {
			debugger
			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val();
			var Noc = $(this).find("#hdnNoofColumns").val();
			var ids = $(this).find(".card")[0].childNodes[1].id;



		});
		$("#MultilineChart").each(function (index, item) {
			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val();
			var Noc = $(this).find("#hdnNoofColumns").val();
			var ids = $(this).find(".card")[0].childNodes[1].id;

		});
		$("#PiChart").each(function (index, item) {
			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val();
			var Noc = $(this).find("#hdnNoofColumns").val();
			var ids = $(this).find(".card")[0].childNodes[1].id;

		});

		$("#dt #Datatables1").each(function (index, item) {
			debugger;
			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val().split(",");
			var Noc = $(this).find("#hdnNoofColumns").val();
			var ids = $(this).find(".card-body")[0].childNodes[1].id;
			var i = 0;
			var tr = $('#' + ids + ' thead');
			while (i < Col.length) {				
				tr.find("tr").append('<td>' + Col[i] +'<td>');
				i++;
			}

			
		});

		
		



		

    }
}