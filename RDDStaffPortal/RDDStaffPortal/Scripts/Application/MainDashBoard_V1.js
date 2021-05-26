var MainDashBoard_V1 = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		var Maindata;
		var Maindata_result = '';
		MainDashBoard_V1.Attachevent();
	},
	Attachevent: function () {
		var Month_arr = [];
		var Quoter_arr = [];
		var Half_arr = [];
		var Year_arr = [];
		var ChartSalesCondition = true;
		var chartb;
		$.ajax({
			async: false,
			cache: false,
			type: "POST",
			url: "/Get_MainDashBoard_V1",
			contentType: "Application/json",
			dataType: 'JSON',
			success: function (response) {
				
				
				$(".loader1").show();
				Maindata = response;
				console.log(Maindata);
				$("#SalesChecklbl").hide();
				$("#GPChecklbl").hide();
				$("#SalesCheck").hide();
				$("#GPCheck").hide()
				try {
					$("#lins #BarChart-V1").each(function (index, item) {
						debugger
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val();
						var lbl2 = $(this).find("#hdnlbl2").val().split(",");
						var lbl1 = $(this).find("#hdnlbl1").val().split(",");
						var bgs = $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".chart-container")[0].childNodes[1].id;
						var pts = [];
						var lblarr1 = [];
						var lblarr2 = [];
						var bgarr = [];
						
						if (ids == 'DASH022') {

							ids = 'chartSales';
							$('#first_tab,#pills-tabContent-Sales,#SalesCheck,#SalesChecklbl').show();
							$("#SalesChecklbl").show();
							$("#SalesCheck").show();
							$("input[name=inlineRadioOptions][value='chartSales']").attr('checked', 'checked');
							ChartSalesCondition = false;

						} else if (ids == 'DASH023') {
							ids = 'chartGP';

							if (ChartSalesCondition == true) {

								$('#first_tab,#pills-tabContent-Sales,#SalesCheck,#SalesChecklbl').hide();
								$("input[name=inlineRadioOptions][value='chartGP']").attr('checked', 'checked');
								$('#first_tab,#pills-tabContent-GP,#GPCheck').show();
								$("#pills-tabContent-GP").show();
								$("#pills-Month-tab").attr("href", "#pills-Month-GP");
								$("#pills-Quarter-tab").attr("href", "#pills-Quarter-GP");
								$("#pills-Half-tab").attr("href", "#pills-Half-GP");
								$("#pills-Year-tab").attr("href", "#pills-Year-GP");
								//$("#SalesChecklbl").hide();

							} else {
								$('#first_tab,#pills-tabContent-GP,#GPCheck').show();
								$("#pills-tabContent-GP").hide();
							}
							$("#GPChecklbl").show();



						}

						var i = 0;
						while (i < lbl1.length) {
							lblarr1.push(lbl1[i].replace(' ', '').trim(''))
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

						var points = [];
						var points1 = [];
						var points2 = [];
						var points3 = [];
						var tempArray = [];

						var Line_data = ''
						data_return(Noc, Maindata)
						Line_data = Maindata_result;

						/*STatrt Month wise*/
						var lblarr1_Month = [];
						var lblarr1_Half = [];
						var lblarr1_Quoter = [];
						var pts_Month = [];
						var pts_Half = [];
						var pts_Quoter = [];
						var TodayMonth = (new Date().getMonth());




						lblarr1_Month.push(lblarr1[TodayMonth]);
						points.push(Line_data[TodayMonth][lblarr2[0].replace(' ', '').trim('')]);
						points1.push(Line_data[TodayMonth][lblarr2[1].replace(' ', '').trim('')]);
						points2.push(Line_data[TodayMonth][lblarr2[2].replace(' ', '').trim('')]);
						points3.push(Line_data[TodayMonth][lblarr2[3].replace(' ', '').trim('')]);

						Month_arr.push(ids);
						Month_arr.push(Line_data[TodayMonth][lblarr2[0].replace(' ', '').trim('')].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Month_arr.push(Line_data[TodayMonth][lblarr2[1].replace(' ', '').trim('')].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Month_arr.push(Line_data[TodayMonth][lblarr2[2].replace(' ', '').trim('')].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Month_arr.push(Line_data[TodayMonth][lblarr2[3].replace(' ', '').trim('')].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));


						tempArray.push(points);
						tempArray.push(points1);
						tempArray.push(points2);
						tempArray.push(points3);

						pts_Month.push(tempArray);
						/*End Month*/
						/*Start Quoter */
						var k = 0;
						if ($.inArray(TodayMonth, [0, 1, 2]) == 1) {
							i = 3;
							k = 0;

						} else if ($.inArray(TodayMonth, [3, 4, 5]) == 1) {
							i = 6;
							k = 3;
						}
						else if ($.inArray(TodayMonth, [6, 7, 8]) == 1) {
							i = 9;
							k = 6;
						}
						else if (($.inArray(TodayMonth, [9, 10, 11]) == 1)) {
							i = 12;
							k = 9;
						}
						points = [];
						points1 = [];
						points2 = [];
						points3 = [];
						var Target = 0
						var Actual = 0
						var Forecast = 0
						var Delta = 0

						while (i > k) {
							lblarr1_Quoter.push(lbl1[k].replace(' ', '').trim(''))
							points.push(Line_data[k][lblarr2[0].replace(' ', '').trim('')]);
							points1.push(Line_data[k][lblarr2[1].replace(' ', '').trim('')]);
							points2.push(Line_data[k][lblarr2[2].replace(' ', '').trim('')]);
							points3.push(Line_data[k][lblarr2[3].replace(' ', '').trim('')]);
							Target = parseFloat(Line_data[k][lblarr2[0].replace(' ', '').trim('')]) + parseFloat(Target);
							Actual = parseFloat(Line_data[k][lblarr2[1].replace(' ', '').trim('')]) + parseFloat(Actual);
							Forecast = parseFloat(Line_data[k][lblarr2[2].replace(' ', '').trim('')]) + parseFloat(Forecast);
							Delta = parseFloat(Line_data[k][lblarr2[3].replace(' ', '').trim('')]) + parseFloat(Delta);
							k++;
						}
						debugger
						Quoter_arr.push(ids);
						Quoter_arr.push(Target.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Quoter_arr.push(Actual.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Quoter_arr.push(Forecast.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Quoter_arr.push(Delta.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						tempArray = [];

						tempArray.push(points);
						tempArray.push(points1);
						tempArray.push(points2);
						tempArray.push(points3);

						pts_Quoter.push(tempArray);
						/*End Quoter*/

						/*start Half Month*/
						if (TodayMonth > 5) {
							i = 6;
							TodayMonth = 12;
						} else {
							i = 0;
							TodayMonth = 6;
						}
						points = [];
						points1 = [];
						points2 = [];
						points3 = [];
						Target = 0
						Actual = 0
						Forecast = 0
						Delta = 0
						while (i < TodayMonth) {
							lblarr1_Half.push(lbl1[i].replace(' ', '').trim(''));
							points.push(Line_data[i][lblarr2[0].replace(' ', '').trim('')]);
							points1.push(Line_data[i][lblarr2[1].replace(' ', '').trim('')]);
							points2.push(Line_data[i][lblarr2[2].replace(' ', '').trim('')]);
							points3.push(Line_data[i][lblarr2[3].replace(' ', '').trim('')]);

							Target = parseFloat(Line_data[i][lblarr2[0].replace(' ', '').trim('')]) + parseFloat(Target);
							Actual = parseFloat(Line_data[i][lblarr2[1].replace(' ', '').trim('')]) + parseFloat(Actual);
							Forecast = parseFloat(Line_data[i][lblarr2[2].replace(' ', '').trim('')]) + parseFloat(Forecast);
							Delta = parseFloat(Line_data[i][lblarr2[3].replace(' ', '').trim('')]) + parseFloat(Delta);
							i++;
						}

						Half_arr.push(ids);
						Half_arr.push(Target.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Half_arr.push(Actual.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Half_arr.push(Forecast.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Half_arr.push(Delta.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

						tempArray = [];

						tempArray.push(points);
						tempArray.push(points1);
						tempArray.push(points2);
						tempArray.push(points3);

						pts_Half.push(tempArray);

						/*END Half Month*/
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

						//switch (Noc) {
						//	case 9:
						//		Line_data = Maindata.Table9;
						//		break;

						//}



						points = [];
						points1 = [];
						points2 = [];
						points3 = [];

						Target = 0
						Actual = 0
						Forecast = 0
						Delta = 0
						i = 0;
						while (i < Line_data.length) {
							//lblarr1.push(Line_data[i]["BU"]);
							points.push(Line_data[i][lblarr2[0].replace(' ', '').trim('')]);
							points1.push(Line_data[i][lblarr2[1].replace(' ', '').trim('')]);
							points2.push(Line_data[i][lblarr2[2].replace(' ', '').trim('')]);
							points3.push(Line_data[i][lblarr2[3].replace(' ', '').trim('')]);
							Target = parseFloat(Line_data[i][lblarr2[0].replace(' ', '').trim('')]) + parseFloat(Target);
							Actual = parseFloat(Line_data[i][lblarr2[1].replace(' ', '').trim('')]) + parseFloat(Actual);
							Forecast = parseFloat(Line_data[i][lblarr2[2].replace(' ', '').trim('')]) + parseFloat(Forecast);
							Delta = parseFloat(Line_data[i][lblarr2[3].replace(' ', '').trim('')]) + parseFloat(Delta);
							i++;
						}

						Year_arr.push(ids);
						Year_arr.push(Target.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Year_arr.push(Actual.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Year_arr.push(Forecast.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						Year_arr.push(Delta.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));

						tempArray = [];


						tempArray.push(points);
						tempArray.push(points1);
						tempArray.push(points2);
						tempArray.push(points3);

						pts.push(tempArray);




						var ds = [];
						var i = 0;
						while (i < lblarr2.length) {
							ds.push({

								name: lblarr2[i],

								data: pts[0][i]
							});
							i++;
						}
						var ds_Month = [];
						var i = 0;
						while (i < lblarr2.length) {
							ds_Month.push({
								name: lblarr2[i],
								data: pts_Month[0][i]
							});
							i++;
						}

						var ds_Quoter = [];
						var i = 0;
						while (i < lblarr2.length) {
							ds_Quoter.push({
								name: lblarr2[i],
								data: pts_Quoter[0][i]
							});
							i++;
						}
						var ds_Half = [];
						var i = 0;
						while (i < lblarr2.length) {
							ds_Half.push({
								name: lblarr2[i],
								data: pts_Half[0][i]
							});
							i++;
						}

						var options = {
							series: ds,
							chart: {
								type: 'bar',
								height: 250
							},
							plotOptions: {
								bar: {
									horizontal: false,
									columnWidth: '55%',
									endingShape: 'rounded'
								},
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								show: true,
								width: 2,
								colors: ['transparent']
							},
							xaxis: {
								categories: lblarr1,
							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										var abr = "";
										if (value < 0) {
											value = -(value);
											abr = '-';
										} else if (value == 0) {
											return '$ ' + value.toString();
										}
										if (value >= 1000000000) {
											values = (value / 1000000000) + 'b';
										} else if (value >= 1000000) {
											values = (value / 1000000) + 'm';
										} else if (value >= 1000) {
											values = (value / 1000) + 'k';
										}

										return '$ ' + abr + values;
									}
								}
							},
							fill: {
								opacity: 1
							},
							tooltip: {
								y: {
									formatter: function (value) {


										return '$ ' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
									}
								}
							}
						};

						var options_Month = {
							series: ds_Month,
							chart: {
								type: 'bar',
								height: 250
							},
							plotOptions: {
								bar: {
									horizontal: false,
									columnWidth: '55%',
									endingShape: 'rounded'
								},
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								show: true,
								width: 2,
								colors: ['transparent']
							},
							xaxis: {
								categories: lblarr1_Month,
							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										var abr = "";
										if (value < 0) {
											value = -(value);
											abr = '-';
										} else if (value == 0) {
											return '$ ' + value.toString();
										}
										if (value >= 1000000000) {
											values = (value / 1000000000) + 'b';
										} else if (value >= 1000000) {
											values = (value / 1000000) + 'm';
										} else if (value >= 1000) {
											values = (value / 1000) + 'k';
										}

										return '$ ' + abr + values;
									}
								}
							},
							fill: {
								opacity: 1
							},
							tooltip: {
								y: {
									formatter: function (value) {
										return '$ ' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
									}
								}
							}
						};
						var options_Half = {
							series: ds_Half,
							chart: {
								type: 'bar',
								height: 250
							},
							plotOptions: {
								bar: {
									horizontal: false,
									columnWidth: '55%',
									endingShape: 'rounded'
								},
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								show: true,
								width: 2,
								colors: ['transparent']
							},
							xaxis: {
								categories: lblarr1_Half,
							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										var abr = "";
										if (value < 0) {
											value = -(value);
											abr = '-';
										} else if (value == 0) {
											return '$ ' + value.toString();
										}
										if (value >= 1000000000) {
											values = (value / 1000000000) + 'b';
										} else if (value >= 1000000) {
											values = (value / 1000000) + 'm';
										} else if (value >= 1000) {
											values = (value / 1000) + 'k';
										}

										return '$ ' + abr + values;


									}
								}
							},
							fill: {
								opacity: 1
							},
							tooltip: {
								y: {
									formatter: function (value) {
										return '$ ' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
									}
								}
							}
						};

						var options_Quoter = {
							series: ds_Quoter,
							chart: {
								type: 'bar',
								height: 250
							},
							plotOptions: {
								bar: {
									horizontal: false,
									columnWidth: '55%',
									endingShape: 'rounded'
								},
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								show: true,
								width: 2,
								colors: ['transparent']
							},
							xaxis: {
								categories: lblarr1_Quoter,
							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										var abr = "";
										if (value < 0) {
											value = -(value);
											abr = '-';
										} else if (value == 0) {
											return '$ ' + value.toString();
										}
										if (value >= 1000000000) {
											values = (value / 1000000000) + 'b';
										} else if (value >= 1000000) {
											values = (value / 1000000) + 'm';
										} else if (value >= 1000) {
											values = (value / 1000) + 'k';
										}

										return '$ ' + abr + values;
									}
								}
							},
							fill: {
								opacity: 1
							},
							tooltip: {
								y: {
									formatter: function (value) {
										return '$ ' + value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
									}
								}
							}
						};

						//$("#first_tab").find(".card-title").text($("#lins").find(".card-title").text());


						$('#lins,#lins').hide();
						var chartx = new ApexCharts(document.querySelector('#' + ids + '-Year'), options);
						chartx.render();

						chartx = new ApexCharts(document.querySelector('#' + ids + '-Month'), options_Month);
						chartx.render();

						chartx = new ApexCharts(document.querySelector('#' + ids + '-Quarter'), options_Quoter);
						chartx.render();

						chartx = new ApexCharts(document.querySelector('#' + ids + '-Half'), options_Half);
						chartx.render();
						//var myMultipleLineChart = new Chart(ids, chartoption);
						//myMultipleLineChart.render();

						//if (tf1 == true) {
						//	var myMultipleLineChart1 = new Chart("#multipleLineChart", chartoption);
						//	myMultipleLineChart1.render();
						//	tf1 = false;
						//         }
						try {
							$("#dt #Datatables1-V1").each(function (index, item) {

								var url = ['', '1', '2', '3', '4', '5'];
								var Col = $(this).find("#hdnColumns").val().split(",");
								var Noc = parseInt($(this).find("#hdnNoofColumns").val());
								var fld = $(this).find("#hdnField").val().split(",");
								var ids = $(this).find(".card-body")[0].childNodes[1].id;
								var i = 0;
								var tr = $('#' + ids + ' thead');



								var tblhead1 = ['CompanyName', 'Date', 'Amount'];
								var tblhide = ['Date'];
								if (fld[1] == 'Date') {
									tblhide = [];
								}


								var tr2 = $('#' + ids);

								tr2.find("#Ist").show();
								tr2.find("div#Ist").not(':first').remove();
								RedDot_DivTable_Header_Fill_Dashboard(ids, Col);
								var dtdat = '';
								data_return(Noc, Maindata)
								dtdat = Maindata_result;
								//switch (Noc) {
								//		case 0:
								//			dtdat = Maindata.Table
								//			break;
								//		case 1:
								//			dtdat = Maindata.Table1
								//			break;
								//		case 2:
								//			dtdat = Maindata.Table2
								//			break;
								//		case 3:
								//			dtdat = Maindata.Table3
								//			break;
								//		case 4:
								//			dtdat = Maindata.Table4
								//			break;
								//		case 5:
								//			dtdat = Maindata.Table5
								//			break;
								//		//put your cases here
								//	}

								var arr = dtdat;
								if (arr != null && arr.length != 0) {
									var i = 0;
									while (arr.length > i) {
										var tr = tr2.find("#Ist").clone();
										var tr1 = tr2.find("#Ind").closest();
										var k = 0;
										var l1 = fld.length;
										while (l1 > k) {
											if (fld[k] == 'Date') {
												tr.find(".Abcd")[k].children[0].textContent = DateToStringformat(arr[i][fld[k]]);
											} else if (fld[k] == 'Amount') {
												tr.find(".Abcd")[k].children[0].textContent = "$ " + arr[i][fld[k]].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
											}
											else {
												tr.find(".Abcd")[k].children[0].textContent = arr[i][fld[k]];
											}
											var t = tblhead1[k];
											if (jQuery.inArray(t, tblhide) !== -1) {
												tr.find(".Abcd").eq(k).addClass("Abc")
												tr1.prevObject.find(".reddotTableHead").eq(k).addClass("Abc")
											}
											k++;
										}
										tr2.find("#Ibody").append(tr[0]);
										i++;
									}
									tr2.find("#Ist")[0].remove();
								} else {
									tr2.find("#Ist").hide();
									//RedDotAlert_Error("No Record Found");
								}







							});
						} catch (e) {
							console.log(e);
						}



					});

				} catch (e) {
					console.log(e)
				}
				try {
					$("#GP #GraphChart-V1").each(function (index, item) {
						
						var now = new Date();
						var future = now.setMonth(now.getMonth(), 1);
						var past = now.setMonth(now.getMonth() - 5, 1);
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val();
						var lbl2 = $(this).find("#hdnlbl2").val().split(",");
						var lbl1 = $(this).find("#hdnlbl1").val().split(",");
						var bgs = $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".chart-container")[0].childNodes[1].id;
						var Line_data = ''
						data_return(Noc, Maindata)
						Line_data = Maindata_result;



						var i;
						var lbltxt = [];
						var ds = [];
						var ds1 = [];
						var points = [];
						var ds_graph = [];
						while (new Date(future) >= new Date(past)) {
							points.push(RdotdatefrmtMonthYear(past))
							var now = new Date(past);
							past = now.setMonth(now.getMonth() + 1, 1);
						}

						if (ids == 'DASH030') {
							var x = document.getElementById("smallSelect-DASH030");
							var txt = "All options: ";


							for (i = 0; i < x.length; i++) {
								if (x.options[i].text !== 'ALL')
									lbltxt.push(x.options[i].text.toString().toUpperCase());
								var found_names = $.grep(Line_data, function (v) {
									return v.Status.toUpperCase() === x.options[i].text.toString().toUpperCase();
								});
								var k = 0
								var points1 = [];
								while (k < points.length && found_names.length > 0) {
									var Monthsplit = points[k].split('-');
									var found_names1 = $.grep(found_names, function (v) {
										return v.quoteMonthMMM === Monthsplit[0] && v.QuoteYear == Monthsplit[1];
									});

									if (found_names1.length > 0) {
										points1.push(found_names1[0].TotalAmount)
									}
									k++;

								}


								if (points1.length > 0)
									ds.push(points1);






							}

							
							i = 0;
							while (lbltxt.length > i) {
								ds_graph.push({
									name: lbltxt[i],
									data: ds[i]
								});
								i++;
							}
						} else {
							$("#smallSelect-DASH031").hide();
							var k = 0
							var points1 = [];
							var points2 = [];
							var temparr = [];
							while (Line_data.length > k) {
								points1.push(Line_data[k].Breadth)
								points2.push(Line_data[k].NewCustomers)

								k++;

							}
							
						if (points1.length > 0) {
								ds.push(points1);
								ds.push(points2);


							}



							i = 0;
							while (lbl2.length > i) {
								ds_graph.push({
									name: lbl2[i],
									data: ds[i]
								});

								i++;
							}


						}
						i = 0;

						
						var options = {
							series: ds_graph,
							chart: {
								height: 180,
								type: 'area',
								//zoom: {
								//	autoScaleYaxis: true
								//}
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								curve: 'smooth'
							},
							xaxis: {

								categories: points,
								tickPlacement: 'on',


							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										var abr = "";
										if (value < 0) {
											value = -(value);
											abr = '-';
										} else if (value == 0) {
											return '$ ' + Math.trunc(value).toString();
										}
										if (value >= 1000000000) {
											values = '$ ' + Math.trunc(value / 1000000000) + 'b';
										} else if (value >= 1000000) {
											values = '$ ' + Math.trunc(value / 1000000) + 'm';
										} else if (value >= 1000) {
											values = '$ ' + Math.trunc(value / 1000) + 'k';
										} else {
											values = value;
										}

										return  abr + values;
									}
								}
							},

						};
						if (ids == 'DASH030') {
							chartb = new ApexCharts(document.querySelector("#" + ids + ""), options);
						chartb.render();
					}
						else{
			var	chartb1 = new ApexCharts(document.querySelector("#" + ids + ""), options);
				chartb1.render();
                    }

						

					})

                } catch (e) {
					console.log(e)
                }

				
				var Line_data = ''
				data_return(1, Maindata)
				Line_data = Maindata_result;
				
				$("#daysLeft").text(Line_data[0]["daysLeft"]);

				$("#Target").text(Quoter_arr[1]);
				$("#Actual").text(Quoter_arr[3]);
				$("#Forecast").text(Quoter_arr[2]);
				$("#Delta").text(Quoter_arr[4]);
				$("#pills-Quarter-tab").trigger('click');

				try {
					$("#pis  #PiChart-V1").each(function (index, item) {
						
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url = $(this).find("#hdnurl").val();
						var ids = $(this).find(".chart-container")[0].childNodes[1].id;
						var pts = [];
						var lbl = [];
						var bg = [];
						var Pi_data;
						data_return(Noc, Maindata)
						Pi_data = Maindata_result;
						//switch (Noc) {
						//	case 10:
						//		Pi_data = Maindata.Table10
						//		break;
						//	case 11:
						//		Pi_data = Maindata.Table11
						//		break;

						//}

						var i = 0;
						while (i < Pi_data.length) {
							pts.push(Pi_data[i].Percentage);
							lbl.push(Pi_data[i].Status);
							bg.push(Pi_data[i].BgColor);
							i++;
						}


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
				} catch (e) {
					console.log(e);
				}
				try {
					$("#SecCard-V1 #SecondCard-V1").each(function (index, item) {
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url = $(this).find("#hdnurl").val().split(",");
						var lblarr = $(this).find("#hdnlbl2").val().split(",");
						
						var Secondcard_data = Maindata.Table15;

						if (Secondcard_data.length > 0) {
							if ($(this).find(".card-category").text() == "Total Receivable") {
								$(this).find(".card-title").text(" " + RedDot_NumberFormat(Secondcard_data[0].TotalReceivable));
							} else if ($(this).find(".card-category").text() == "Total Payable") {
								$(this).find(".card-title").text(" " + RedDot_NumberFormat(Secondcard_data[0].TotalPayable));

							} else {
								$(this).find(".card-title").text(" " + RedDot_NumberFormat(Secondcard_data[0].BankBalance));
							}
						} else {
							$(this).find(".card-title").text(" " + RedDot_NumberFormat(0));
						}


						var tblhead1 = ['Country', 'days_0_30', 'days_31_37', 'days_38_45', 'days_46_60', 'days_61_90', 'days_91_120', 'days_121_150', 'days_151_180', 'days_181plus'];
						var ids = $(this).find("#hdnDashid").val();
						var arr = [];
						
						if (ids !== 'DASH036') {
							var tr2 = $('#' + ids + '-Model');


							tr2.find("#Ist").show();
							tr2.find("div#Ist").not(':first').remove();

							data_return(Noc, Maindata)
							arr = Maindata_result;
							//switch (Noc) {
							//	case 16:
							//		arr = Maindata.Table16;
							//		break;
							//	case 17:
							//		arr = Maindata.Table17;
							//		break;
							//}

							if (arr != null && arr.length != 0) {
								var i = 0;
								while (arr.length > i) {
									var tr = tr2.find("#Ist").clone();
									var tr1 = tr2.find("#Ind").closest();
									var k = 0;
									var l1 = tblhead1.length;
									while (l1 > k) {
										if (tblhead1[k] !== 'Country') {
											tr.find(".Abcd")[k].children[0].textContent = Math.round(arr[i][tblhead1[k]]).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
										} else {
											var Country = "";
											switch (index) {
												case 0:
													Country = 'Area';
													break;
												case 1:
													Country = 'DBName';
													break;
											}
											tr.find(".Abcd")[k].children[0].textContent = arr[i][Country];
										}


										k++;
									}
									if (ids == 'DASH035') {
										tr1.prevObject.find(".reddotTableHead").eq(0)[0].children[0].textContent = 'DBName';
									}
									tr2.find("#Ibody").append(tr[0]);
									i++;
								}
								tr2.find("#Ist")[0].remove();
							} else {
								tr2.find("#Ist").hide();
								//RedDotAlert_Error("No Record Found");
							}


						} else {
							
							ids = $('#' + ids + '-chart');;
							var pts = [];
							var lbl = [];
							var bg = [];
							var Bank_Data = '';
							switch (Noc) {
								case 16:
									Bank_Data = Maindata.Table16;
									break;

							}



							var i = 0;
							while (i < Bank_Data.length) {
								pts.push(Bank_Data[i][lblarr[1]]);
								lbl.push(Bank_Data[i][lblarr[0]]);
								bg.push(Bank_Data[i][lblarr[2]]);
								i++;
							}


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
						}







					});

				} catch (e) {
					console.log(e)
				}
			}
		})
		$("#smallSelect-DASH030").on('change', function () {
			
			var t = $(this).val();
			var arr = ['OPEN', 'WON OPG', 'WON-R OPG', 'LOST-CLOSED', 'EXPECTED CLOSE'];
			var i = 0;
			
			while (arr.length > i) {
				if (t == "ALL") {
					chartb.showSeries(arr[i])
                }else
				if (t !== arr[i] ) {
					//$(".apexcharts-legend-text").text(arr[i]).trigger('click');
					chartb.hideSeries(arr[i])
				} else {
					chartb.showSeries(arr[i])
                }
				i++;
            }
			

			
		})
		//var resetCssClasses = function (activeEl) {
		//	var els = document.querySelectorAll('button')
		//	Array.prototype.forEach.call(els, function (el) {
		//		el.classList.remove('active')
		//	})

		//	activeEl.target.classList.add('active')
		//}

		//document
		//	.querySelector('#Three_month')
		//	.addEventListener('click', function (e) {
		//		resetCssClasses(e)
		//		
				
		//	})

		//document
		//	.querySelector('#six_months')
		//	.addEventListener('click', function (e) {
		//		resetCssClasses(e)

		//		chartb.zoomX(
		//			'Dec-2020',
		//			'May-2021'
		//		)
		//	})

		$("#pills-Month-tab").on('click', function () {
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			debugger
			if (t == 'chartSales') {
				$("#Target").text(Month_arr[1]);
				$("#Actual").text(Month_arr[3]);
				$("#Forecast").text(Month_arr[2]);
				$("#Delta").text(Month_arr[4]);
			} else if (t == 'chartGP' && Month_arr.length > 5) {
				$("#Target").text(Month_arr[6]);
				$("#Actual").text(Month_arr[8]);
				$("#Forecast").text(Month_arr[7]);
				$("#Delta").text(Month_arr[9]);
			} else {
				$("#Target").text(Month_arr[1]);
				$("#Actual").text(Month_arr[3]);
				$("#Forecast").text(Month_arr[2]);
				$("#Delta").text(Month_arr[4]);
			}

		})
		$("#pills-Year-tab").on('click', function () {
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			debugger
			
			if (t == 'chartSales') {
				$("#Target").text(Year_arr[1]);
				$("#Actual").text(Year_arr[3]);
				$("#Forecast").text(Year_arr[2]);
				$("#Delta").text(Year_arr[4]);
			} else if (t == 'chartGP' && Year_arr.length > 5) {
				$("#Target").text(Year_arr[6]);
				$("#Actual").text(Year_arr[8]);
				$("#Forecast").text(Year_arr[7]);
				$("#Delta").text(Year_arr[9]);
			} else {
				$("#Target").text(Year_arr[1]);
				$("#Actual").text(Year_arr[3]);
				$("#Forecast").text(Year_arr[2]);
				$("#Delta").text(Year_arr[4]);
			}
		})
		$("#pills-Quarter-tab").on('click', function () {
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			debugger
			if (t == 'chartSales') {
				$("#Target").text(Quoter_arr[1]);
				$("#Actual").text(Quoter_arr[3]);
				$("#Forecast").text(Quoter_arr[2]);
				$("#Delta").text(Quoter_arr[4]);
			} else if (t == 'chartGP' && Quoter_arr.length > 5) {
				$("#Target").text(Quoter_arr[6]);
				$("#Actual").text(Quoter_arr[8]);
				$("#Forecast").text(Quoter_arr[7]);
				$("#Delta").text(Quoter_arr[9]);
			} else {
				$("#Target").text(Quoter_arr[1]);
				$("#Actual").text(Quoter_arr[3]);
				$("#Forecast").text(Quoter_arr[2]);
				$("#Delta").text(Quoter_arr[4]);
            }
		})
		$("#pills-Half-tab").on('click', function () {
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			debugger
			
			if (t == 'chartSales') {
				$("#Target").text(Half_arr[1]);
				$("#Actual").text(Half_arr[3]);
				$("#Forecast").text(Half_arr[2]);
				$("#Delta").text(Half_arr[4]);
			}
			else if (t == 'chartGP' && Half_arr.length > 5) {
				$("#Target").text(Half_arr[6]);
				$("#Actual").text(Half_arr[8]);
				$("#Forecast").text(Half_arr[7]);
				$("#Delta").text(Half_arr[9]);
			} else {
				$("#Target").text(Half_arr[1]);
				$("#Actual").text(Half_arr[3]);
				$("#Forecast").text(Half_arr[2]);
				$("#Delta").text(Half_arr[4]);
			}
		})
		//$("input[name=inlineRadioOptions]").trigger('chnage');
		$("input[name=inlineRadioOptions]").on('change', function () {
			
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			if (t == 'chartSales') {
				$('#pills-tabContent-Sales').show();
				$('#pills-tabContent-GP').hide();
				$("#pills-Month-tab").attr("href", "#pills-Month-Sales");
				$("#pills-Quarter-tab").attr("href", "#pills-Quarter-Sales");
				$("#pills-Half-tab").attr("href", "#pills-Half-Sales");
				$("#pills-Year-tab").attr("href", "#pills-Year-Sales");
				$("#pills-Quarter-Sales").addClass("show active");
				$("#pills-Half-Sales").removeClass("show active");
				$("#pills-Month-Sales").removeClass("show active");
				$("#pills-Year-Sales").removeClass("show active");
			} else {
				
				$('#pills-tabContent-Sales').hide();
				$('#pills-tabContent-GP').show();
				$("#pills-Month-tab").attr("href", "#pills-Month-GP");
				$("#pills-Quarter-tab").attr("href", "#pills-Quarter-GP");
				$("#pills-Half-tab").attr("href", "#pills-Half-GP");
				$("#pills-Year-tab").attr("href", "#pills-Year-GP");
				$("#pills-Quarter-tab").trigger('click');
				$("#pills-Quarter-GP").addClass("show active");
				$("#pills-Half-GP").removeClass("show active");
				$("#pills-Month-GP").removeClass("show active");
				$("#pills-Year-GP").removeClass("show active");
			}
			
			

			
		})
		function data_return(index, Maindata) {
			switch (index) {
				case 0:
					Maindata_result = Maindata.Table;
					break;
				case 1:
					Maindata_result = Maindata.Table1;
					break;
				case 2:
					Maindata_result = Maindata.Table2;
					break;
				case 3:
					Maindata_result = Maindata.Table3;
					break;
				case 4:
					Maindata_result = Maindata.Table4;
					break;
				case 5:
					Maindata_result = Maindata.Table5;
					break;
				case 7:
					Maindata_result = Maindata.Table7;
					break;
				case 9:
					Maindata_result = Maindata.Table9;
					break;
				case 10:
					Maindata_result = Maindata.Table10;
					break;
				case 11:
					Maindata_result = Maindata.Table11;
					break;
				case 12:
					Maindata_result = Maindata.Table12;
					break;
				case 13:
					Maindata_result = Maindata.Table13;
					break;
				case 14:
					Maindata_result = Maindata.Table14;
					break;
				case 15:
					Maindata_result = Maindata.Table15;
					break;
				case 16:
					Maindata_result = Maindata.Table16;
					break;
				case 17:
					Maindata_result = Maindata.Table17;
					break;
				case 18:
					Maindata_result = Maindata.Table18;
					break;
			}
		}

		function DateToStringformat(obj) {
			try {
				;
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
	}
}