var DashBoard = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		DashBoard.Attachevent();
	},
	Attachevent: function () {
		;
		var Cards = [];
		$("#Firstcard").each(function (index, item) {
			
			var url = $(this).find("#hdnurl").val();
			$.ajax({
				async: false,
				cache: false,
				type: "POST",
				url:url,
				dataType: 'Json',
				contentType: "Application/json",
				dataType: 'JSON',
				success: function (response) {
					;
					var i = 0;
					while (i < response.data.length) {
						Cards.push(response.data[i]);
						i++;
					}

				}
			})
		});
		
		$("#Cards #Firstcard").each(function (index, item) {
			
			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val();
			var Noc = $(this).find("#hdnNoofColumns").val();
			var ids = $(this).find(".card")[0].childNodes[1].id;
			var lbl = $(this).find(".ds1").text().split(" ");

			$(this).find(".ds2").text(lbl[0] + " Acheived");

			if ($(this).find(".ds1").text() == "Revenue Target") {
				$(this).find(".A1").text(Cards[0].RevTarget);
				$(this).find(".B1").text(Cards[0].ActualRev);
				$(this).find(".perv").text(Cards[0].RevTrgetAcheivedPercent);
				$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + Cards[0].RevTrgetAcheivedPercent+'')
				

			} else if ($(this).find(".ds1").text() == "Revenue Forecast") {
				$(this).find(".A1").text(Cards[0].RevForecast);
				$(this).find(".B1").text(Cards[0].ActualRev);
				$(this).find(".perv").text(Cards[0].RevForecastAcheivedPercent);
				$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + Cards[0].RevForecastAcheivedPercent + '')
			} else if ($(this).find(".ds1").text()== "GP Target") {
				$(this).find(".A1").text(Cards[0].GPTarget);
				$(this).find(".B1").text(Cards[0].ActualGP);
				$(this).find(".perv").text(Cards[0].GPTrgetAcheivedPercent);
				$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + Cards[0].GPTrgetAcheivedPercent + '')

			} else {
				$(this).find(".A1").text(Cards[0].GPForecast);
				$(this).find(".B1").text(Cards[0].ActualGP);
				$(this).find(".perv").text(Cards[0].GPForecastAcheivedPercent);
				$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + Cards[0].GPForecastAcheivedPercent + '')
			}
			

			
		});
		
		$("#pis  #PiChart").each(function (index, item) {
			
			var url = $(this).find("#hdnurl").val();
			var ids = $(this).find(".chart-container")[0].childNodes[1].id;
			var pts = [];
			var lbl = [];
			var bg = [];
			$.ajax({
				async: false,
				cache: false,
				type: "POST",
				url: '/GetPichart',
				dataType: 'Json',
				contentType: "Application/json",
				dataType: 'JSON',
				success: function (response) {
					var i = 0;
					while (i < response.data.length) {
						pts.push(response.data[i].points);
						lbl.push(response.data[i].lblname);
						bg.push(response.data[i].bgcolrs);
						i++;
					}
				}
			})
			var myPieChart = new Chart(ids, {
				type: 'pie',
				data: {
					datasets: [{
						data: pts,
						backgroundColor: bg,
						borderWidth: 0
					}],
					labels: lbl
				},
				options: {
					responsive: true,
					maintainAspectRatio: false,
					legend: {
						position: 'right',
						labels: {
							fontColor: 'rgb(154, 154, 154)',
							fontSize: 14,
							usePointStyle: false,
							padding: 20
						}
					},
					pieceLabel: {
						render: 'percentage',
						fontColor: 'white',
						fontSize: 14,
					},
					tooltips: {
						bodySpacing: 4,
						mode: "nearest",
						intersect: 0,
						position: "nearest",
						xPadding: 10,
						yPadding: 10,
						caretPadding: 10
					},
					layout: {
						padding: {
							left: 0,
							right: 20,
							top: 20,
							bottom: 20
						}
					}
				}
			})
			myPieChart.render();
		});

		$("#dt #Datatables1").each(function (index, item) {


			var url = $(this).find("#hdnurl").val();
			var Col = $(this).find("#hdnColumns").val().split(",");
			var Noc = $(this).find("#hdnNoofColumns").val();
			var fld = $(this).find("#hdnField").val().split(",");
			var ids = $(this).find(".card-body")[0].childNodes[1].id;
			var i = 0;
			var tr = $('#' + ids + ' thead');

			var colms = [];
			while (i < Col.length) {
				tr.find("tr").append('<th>' + Col[i] + '</th>');
				if (Col[i] == 'Date') {
					colms.push({
						'mDataProp': fld[i] + '',
						"render": function (data) {
							return (RdotdatefrmtRes1(data));
						}
					})
				} else {
					colms.push({ 'mDataProp': fld[i] + '', "sWidth": "40%" })
				}

				i++;
			}
			RdottableDash(ids, url, colms)



		});


		$("#bars #BarChart").each(function (index, item) {
			
			var url1 = $(this).find("#hdnurl").val();
			var lbl2 = $(this).find("#hdnlbl2").val().split(",");
			var lbl1 = $(this).find("#hdnlbl1").val().split(",");
			var bgs = $(this).find("#hdnbgcolors").val().split(",");
			var ids = $(this).find(".chart-container")[0].childNodes[1].id;
			var pts = [];
			var lblarr1 = [];
			var lblarr2 = [];
			var bgarr = [];			
			var i = 0;
			//while (i < lbl1.length) {
			//	lblarr1.push(lbl1[i])
			//	i++;
			//}
			i = 0;
			while (i < lbl2.length) {
				lblarr2.push(lbl2[i])
				i++;
			}
			i = 0;
			while (i < bgs.length) {
				bgarr.push(bgs[i])
				i++;
			}			
			$.ajax({
				async: false,
				cache: false,
				type: "POST",
				url: url1,				
				contentType: "Application/json",
				dataType: 'JSON',
				success: function (response) {	
					
					i = 0;
					while (i < response.data.length) {
						var k = 0
						var tempArray = [];
						while (k<response.data[i].points.length) {
							//pts.push([i,response.data[i].points[k]]);
							
							tempArray.push(response.data[i].points[k]);
							k++;
						}	
						pts.push(tempArray);

						lblarr1=response.data[0].lbls;
						i++;                      						
					}
				}
			})
			i = 0;
			var ds = [];
			debugger;
			while (i < lblarr2.length) {
				ds.push({
					label: lblarr2[i],
					backgroundColor: bgarr[i],
					fill: true,
					data: pts[i]
				});
				i++;
            }
			
			var mySalesAllCountry = new Chart(ids, {
				type: 'bar',
				data: {
					labels: lblarr1,					
					datasets: ds
				},
				options: {
					responsive: true,
					maintainAspectRatio: false,
					legend: {
						position: 'bottom',
					},
					tooltips: {
						bodySpacing: 4,
						mode: "nearest",
						intersect: 0,
						position: "nearest",
						xPadding: 10,
						yPadding: 10,
						caretPadding: 10
					},
					layout: {
						padding: { left: 15, right: 15, top: 15, bottom: 15 }
					}
				}
			});

			mySalesAllCountry.render();


		});


		$("#lins #MultilineChart").each(function (index, item) {
			var url1 = $(this).find("#hdnurl").val();
			var lbl2 = $(this).find("#hdnlbl2").val().split(",");
			var lbl1 = $(this).find("#hdnlbl1").val().split(",");
			var bgs = $(this).find("#hdnbgcolors").val().split(",");
			var ids = $(this).find(".chart-container")[0].childNodes[1].id;
			var pts = [];
			var lblarr1 = [];
			var lblarr2 = [];
			var bgarr = [];
			var i = 0;
			while (i < lbl1.length) {
				lblarr1.push(lbl1[i])
				i++;
			}
			i = 0;
			while (i < lbl2.length) {
				lblarr2.push(lbl2[i])
				i++;
			}
			i = 0;
			while (i < bgs.length) {
				bgarr.push(bgs[i])
				i++;
			}
			$.ajax({
				async: false,
				cache: false,
				type: "POST",
				url: url1,
				contentType: "Application/json",
				dataType: 'JSON',
				success: function (response) {
					i = 0;
					while (i < response.data.length) {
						var k = 0
						var tempArray = [];
						while (k < response.data[i].points.length) {
							//pts.push([i,response.data[i].points[k]]);
							tempArray.push(response.data[i].points[k]);
							k++;
						}
						pts.push(tempArray);
						i++;
					}
				}
			})
			i = 0;
			var ds = [];
			while (i < lblarr2.length) {
				ds.push({					
						label: lblarr2[i],
						borderColor: bgarr[i],
						pointBorderColor: "#FFF",
					pointBackgroundColor: bgarr[i],
						pointBorderWidth: 2,
						pointHoverRadius: 4,
						pointHoverBorderWidth: 1,
						pointRadius: 4,
						backgroundColor: 'transparent',
						fill: true,
						borderWidth: 2,
					data: pts[i]
				});
				i++;
			}

			var myMultipleLineChart = new Chart(ids, {
				type: 'line',
				data: {
					labels:lblarr1,
					datasets: ds
				},
				options: {
					responsive: true,
					maintainAspectRatio: false,
					legend: {
						position: 'bottom',
					},
					tooltips: {
						bodySpacing: 4,
						mode: "nearest",
						intersect: 0,
						position: "nearest",
						xPadding: 10,
						yPadding: 10,
						caretPadding: 10
					},
					layout: {
						padding: { left: 15, right: 15, top: 15, bottom: 15 }
					}
				}
			});
			myMultipleLineChart.render();
			
		});

	}
}