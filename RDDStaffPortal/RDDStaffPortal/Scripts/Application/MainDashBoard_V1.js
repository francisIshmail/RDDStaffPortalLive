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
		var chartfunnel, chartbudget, chartBreadth, chartDebator;
		var Inventory_Data = '', Stock_Data0 = '', Stock_Data0I = '', Stock_Data0II = '', Stock_Data0III = '';
		var Stock_Data1 = '', Stock_Data1I = '', Stock_Data1II = '', Stock_Data1III = '';
		var Inventory_bgs, Inventory_lbl1, Inventory_lbl2;
		var Stock1_country = [], chartxStockBar1, chartxStockBar0, Stock_country = [];
		var StockGit_country = [], chartxStockBarGit, lblStockGit1 = [], lblStockGit2 = [], lblStockGit3 = [], ds_Git = [], ds_Git1 = [];
		var Stock_DataGit = '', Stock_DataGitI = '', Stock_DataGitII = '', Stock_DataGitIII = '';
		var StockGitdata = [], StockGitdata1 = [];


		var ds_Budget = [];
		var ds_funnel = [];
		var lbl_funnel = [];
		var ds_Debtor = [];
		var lbl_Debtor = [];
		var ds_Breadth = [];
		var lbl_Breadth = [];
		var lbl_budget = [];
		var lbl_points = [];
		var points_Graph = [];
		var StockAgeTemparr = [],StockAgelbl=[];
		var pointsMonth = [];
		var now = new Date();
		var future = now.setMonth(now.getMonth(), 1);
		var past = now.setMonth(now.getMonth() - 5, 1);
		while (new Date(future) >= new Date(past)) {
			points_Graph.push(RdotdatefrmtMonthYear(past))
			lbl_points.push(RdotdatefrmtMonthYear(past))
			var now = new Date(past);
			pointsMonth.push(now.getMonth() + 1)
			past = now.setMonth(now.getMonth() + 1, 1);
		}

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
				$("#GPCheck").hide();				
				try {
					$("#MainChart #MainChart-V1").each(function (index, item) {						
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val();
						var lbl2 = $(this).find("#hdnlbl2").val().split(",");
						var lbl1 = $(this).find("#hdnlbl1").val().split(",");
						var bgs = $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".ABC").attr("id");
						var Line_data = ''
						data_return(Noc, Maindata)
						Line_data = Maindata_result;
						var k3 = 0;
						var totalbalance = 0;
						while (Line_data.length > k3) {
							var tr = $("#Head-" + ids).clone();
							tr.find("#Country-" + ids).html(Line_data[k3][lbl2[0]])
							tr.find("#CountryAmt-" + ids).html("$ " + RedDot_NumberFormat(Line_data[k3][lbl2[1]]))
							$("#footer-" + ids).append(tr);
							totalbalance += parseFloat(Line_data[k3][lbl2[1]]);
							k3++;
						}
						if (k3 > 0) {
							$(".country-amount")[0].remove()
						}
						$("#balance-" + ids).html("$ " + RedDot_NumberFormat(totalbalance))
					})

				} catch (e) {
					console.log(e)
				}
				try {
					$("#lins #BarChart-V1").each(function (index, item) {						
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
						if ($.inArray(TodayMonth, [0, 1, 2])>0) {
							i = 3;
							k = 0;

						} else if ($.inArray(TodayMonth, [3, 4, 5]) > 0) {
							i = 6;
							k = 3;
						}
						else if ($.inArray(TodayMonth, [6, 7, 8]) > 0) {
							i = 9;
							k = 6;
						}
						else if (($.inArray(TodayMonth, [9, 10, 11]) > 0)) {
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
												tr.find(".Abcd")[k].children[0].textContent = "$ " + arr[i][fld[k]].toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
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
						var ds_graph = [];
						if (ids == 'DASH030') {						
							var x = document.getElementById("smallSelect-DASH030");
							var txt = "All options: ";
							for (i = 0; i < x.length; i++) {
								if (x.options[i].text !== 'ALL') {
									lbltxt.push(x.options[i].text.toString().toUpperCase());
									lbl_funnel.push(x.options[i].text.toString().toUpperCase());
								}
								
								var found_names = $.grep(Line_data, function (v) {
									return v.Status.toUpperCase() === x.options[i].text.toString().toUpperCase();
								});
								var k = 0
								var points1 = [];
								while (k < points_Graph.length && found_names.length > 0) {
									var Monthsplit = points_Graph[k].split('-');
									var found_names1 = $.grep(found_names, function (v) {
										return v.quoteMonthMMM.toUpperCase() === Monthsplit[0].toUpperCase() && v.QuoteYear == Monthsplit[1];
									});

									if (found_names1.length > 0) {
										points1.push(found_names1[0].TotalAmount)
									}
									k++;
								}
								if (points1.length > 0) {
									ds.push(points1);
									ds_funnel.push(points1);
								}									
							}							
							i = 0;
							while (lbltxt.length > i) {
								ds_graph.push({
									name: lbltxt[i],
									data: ds[i]
								});
								i++;
							}
						} else if (ids == 'DASH037') {
							$("#smallSelect-DASH037").empty('');
							var b = [];							
							$.each(Line_data, function (index, event) {
								var events = $.grep(b, function (e) {
									return event.Project === e.Project;
								});								
								if (events.length === 0) {
									b.push(event);									
								}
							});
							$.each(b, function (index, event) {
								$('#smallSelect-DASH037').append('<option value=' + index + ' selected="">' + b[index].Project + '</option>');
							});
							
							$('#smallSelect-DASH037').val('0');

							var x = document.getElementById("smallSelect-DASH037");
							var txt = "All options: ";
							
							for (i = 0; i < x.length; i++) {
								if (x.options[i].text !== 'ALL') {
									lbltxt.push(x.options[i].text.toString().toUpperCase());
									
								}
									
								var found_names = $.grep(Line_data, function (v) {
									return v.Project.toUpperCase() === x.options[i].text.toString().toUpperCase();
								});
								var k = 0
								var points1 = [], points2 = [];
								
								while (k < points_Graph.length && found_names.length > 0) {
									var Monthsplit = points_Graph[k].split('-');
									var found_names1 = $.grep(found_names, function (v) {
										return v.BudgetMonth.toUpperCase() === pointsMonth[k].toString() && v.BudgetYear == Monthsplit[1];
									});

									if (found_names1.length > 0) {
										points1.push(found_names1[0].TotalBudget)
										points2.push(found_names1[0].TotalExpense)
									}
									k++;

								}


								if (points1.length > 0) {
									ds_Budget.push(points1);
									ds_Budget.push(points2);
									ds.push(points1);
									ds.push(points2);

								}
									

							}


							i = 0;
							lbl_budget = $(this).find("#hdnlbl2").val().split(",");
							while (lbl_budget.length > i) {
								ds_graph.push({
									name: lbl_budget[i],
									data: ds[i]
								});
								i++;
							}

						} else if (ids == 'DASH038') {

							$("#Three_month-DASH038").hide();
							$("#six_months-DASH038").hide();
							$("#smallSelect-DASH038").empty('');
							var b = [];
							
							$('#smallSelect-DASH038').append('<option value="All" selected="">ALL</option>');
							$.each(Line_data, function (index, event) {
								var events = $.grep(b, function (e) {
									return event.Project === e.Project;
								});
								if (events.length === 0 && event.Project!=="") {
									b.push(event);
								}
							});

							$.each(b, function (index, event) {								
								$('#smallSelect-DASH038').append('<option value=' + b[index].Project+ ' selected="">' + b[index].Project + '</option>');
							});

							$('#smallSelect-DASH038').val('All');

							var x = document.getElementById("smallSelect-DASH038");
							var txt = "All options: ";
							
							for (i = 0; i < x.length; i++) {
								if (x.options[i].text !== 'ALL') {
									lbl_Debtor.push(x.options[i].text.toString().toUpperCase());

								}

								var found_names = $.grep(Line_data, function (v) {
									return v.Project.toUpperCase() === x.options[i].text.toString().toUpperCase();
								});
								
								var points1 = [];
								
								//while if
								if(found_names.length > 0) {
									//var Monthsplit = points_Graph[k].split('-');
									//var found_names1 = $.grep(found_names, function (v) {
									//	return v.BudgetMonth.toUpperCase() === pointsMonth[k].toString() && v.BudgetYear == Monthsplit[1];
									//});

									
									var k = 0
										while (lbl2.length > k) {
											points1.push(found_names[0][lbl2[k]])

											k++;
										}
										
								
									//sk++;

							}


								if (points1.length > 0) {
									ds_Debtor.push(points1);
									
									ds.push(points1);
									

								}







							}


							i = 0;
							
							while (lbl_Debtor.length > i) {
								ds_graph.push({
									name: lbl_Debtor[i],
									data: ds[i]
								});
								i++;
							}
							var options = {
								series: ds_graph,
								chart: {
									type: 'bar',
									height: 210,
									stacked: true,
									
									//stackType: '100%',
									toolbar: {
										show: true
									},
									zoom: {
										enabled: true
									}
								},
								responsive: [{
									breakpoint: 480,
									options: {
										legend: {
											position: 'bottom',
											offsetX: -10,
											offsetY: 0
										}
									}
								}],
								plotOptions: {
									bar: {
										borderRadius: 2,
										horizontal: false,
									},
								},
								dataLabels: {
									
									
									formatter: function (value, opt) {
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

										return abr + values;
									},
									
								},
								xaxis: {

									
									categories: lbl2,
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

											return abr + values;
										}
									}
								},
								legend: {
									position: 'right',
									offsetY: 0
								},
								fill: {
									opacity: 1
								}
							};
							chartDebator = new ApexCharts(document.querySelector("#"+ids+""), options);
							chartDebator.render();
							return;


						}else {
							
							$("#smallSelect-DASH031").hide();
							lbl_Breadth = $(this).find("#hdnlbl2").val().split(",");
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
							ds_Breadth.push(points1);
							ds_Breadth.push(points2);


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
								id:ids,
								height: 180,
								type: 'area',
								zoom: {
									autoScaleYaxis: true
								}
							},
							dataLabels: {
								enabled: false
							},
							stroke: {
								curve: 'smooth'
							},
							xaxis: {

								categories: points_Graph,
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
							chartfunnel = new ApexCharts(document.querySelector("#" + ids + ""), options);
						chartfunnel.render();
						} else if (ids == 'DASH037') {
							chartbudget = new ApexCharts(document.querySelector("#" + ids + ""), options);
							chartbudget.render();
						}
						else{
							 chartBreadth = new ApexCharts(document.querySelector("#" + ids + ""), options);
							chartBreadth.render();
					}

						

					})

				} catch (e) {
					console.log(e)
				}

				
				var Line_data = ''
				data_return(1, Maindata)
				Line_data = Maindata_result;
				if (Line_data.length>0)
				$("#daysLeft").text(Line_data[0]["daysLeft"]);

				$("#Target").text(Quoter_arr[1]);
				$("#Actual").text(Quoter_arr[3]);
				$("#Forecast").text(Quoter_arr[2]);
				$("#Delta").text(Quoter_arr[4]);
				$("#pills-Quarter-tab").trigger('click');

				
				try {
					$("#ThirdCard #ThirdCard-V1").each(function (index, item) {
						$("#smallSelect-DASH040").hide();
						$("#smallSelect-DASH039").hide();
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val().split(",");
						var lbl2 = $(this).find("#hdnlbl2").val().split(",");

						var lbl1 = $(this).find("#hdnlbl1").val().split(",");
						var bgs = $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".ABC")[0].childNodes[1].id;
						var Line_data = ''
						debugger
						data_return(Noc + 4, Maindata)
						var Line_data1 = Maindata_result;
						data_return(Noc + 5, Maindata)
						var Line_data2 = Maindata_result;
						if (ids =="DASH040") {
							$("#exampleModal-I-" + ids).find(".modal-dialog").addClass("modal-lg");
							$("#exampleModal-II-" + ids).find(".modal-dialog").addClass("modal-lg");
						}

						$("#btn-I-" + ids).html(lbl2[0] + "<i class='fas fa-paperclip'></i>");
						$("#btn-II-" + ids).html(lbl2[1] + "<i class='fas fa-paperclip'></i>");
						data_return(Noc, Maindata)
						Line_data = Maindata_result;
						
						if (Line_data.length == 0) {
							return
						}
						$("#lblamt1-I-" + ids).html(Line_data[0][url1[0]]);
						$("#lblamt2-I-" + ids).html("$ "+Line_data[0][url1[1]].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						data_return(Noc + 1, Maindata)
						Line_data = Maindata_result;
						$('tr#tbl-body-I-' + ids).not(':first').remove();
						
						
						if (Line_data != null && Line_data.length != 0) {
							var i1 = 0;
							$("#tbl-body-I-" + ids).show();
							var Idata = Line_data1[0]["I"];
							console.log(Idata)
							Idata = Idata.split(",");
							console.log(Idata)
							
							while (Line_data.length > i1) {
								var tr = $("#tbl-body-I-" + ids).clone();
								var l1 = Idata.length;
								var k = 0;

								
								while (l1 >= k ) {

									tr.find(".Abcd").eq(k).text(Line_data[i1][Idata[k]]);

									k++;
								}
								$("#tbl-Ibody-" + ids).append(tr);
								i1++;
							}
							$("#tbl-body-I-" + ids)[0].remove();
						} else {

							$("#tbl-body-I-" + ids).hide();

						}
						data_return(Noc + 2, Maindata)
						Line_data = Maindata_result;

						debugger
						$("#lblamt1-II-" + ids).html(Line_data[0][url1[2]]);
						$("#lblamt2-II-" + ids).html("$ " +Line_data[0][url1[3]].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
						data_return(Noc + 3, Maindata)
						Line_data = Maindata_result;
						$('tr#tbl-body-II-' + ids).not(':first').remove();
						if (Line_data != null && Line_data.length != 0) {
							var i1 = 0;
							var Idata = Line_data2[0]["II"];
							console.log(Idata)
							Idata = Idata.split(",");
							console.log(Idata)

							
							$("#tbl-body-II-" + ids).show();
							while (Line_data.length > i1) {
								var tr = $("#tbl-body-II-" + ids).clone();

								var k = 0;

								var l1 = Idata.length;
								while (l1 >= k ) {

									tr.find(".Abcd").eq(k).text(Line_data[i1][Idata[k]]);

									k++;
								}
								$("#tbl-IIbody-" + ids).append(tr);
								i1++;
							}
							$("#tbl-body-II-" + ids)[0].remove();
						} else {

							$("#tbl-body-II-" + ids).hide();

						}

						
						data_return(Noc + 6, Maindata)
						tblheader = Maindata_result;
						debugger
						var tblheader1 = tblheader[0]["III"].split(",");
						var tblheader2 = tblheader[0]["IV"].split(",");
						
						
						//tblheader = ['DB Name', 'Customer Name', 'Cheque No', 'Cheque Date', ' Cheque Amount'];
						//
							var k1 = 0;
							
							while (tblheader1.length > k1) {
								$("#tbl-IIHead-" + ids).find(".Abcd").eq(k1).text(tblheader2[k1]);
								$("#tbl-IHead-" + ids).find(".Abcd").eq(k1).text(tblheader1[k1]);
								k1++;
						}
						var t = $("#tbl-IIHead-DASH039").find(".Abcd").length;
						var k2 = tblheader1.length;
						while(k2 < t) {
							$("#tbl-IIHead-" + ids).find(".Abcd")[k2-1].remove()
							$("#tbl-IHead-" + ids).find(".Abcd")[k2 - 1].remove();
							$("#tbl-body-I-" + ids).find(".Abcd")[k2 - 1].remove();
							$("#tbl-body-II-" + ids).find(".Abcd")[k2 - 1].remove();
							k2++;
						}


						
						$("#exampleModalLabel-I-" + ids).html(lbl1[0]);
						$("#exampleModalLabel-II-" + ids).html(lbl1[1]);


					})
				} catch (e) {
					console.log(e)
				}


				try {
					debugger
					$("#InventoryChart #InventoryChart-V1").each(function (index, item) {
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val().split(",");
						Inventory_lbl2 = $(this).find("#hdnlbl2").val().split(",");

						Inventory_lbl1 = $(this).find("#hdnlbl1").val().split(",");
						Inventory_bgs= $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".card-light")[0].childNodes[1].id;
						Inventory_Data= ''
						debugger
						data_return(Noc, Maindata)
						Inventory_Data= Maindata_result;
						$("#smallSelect-"+ids).empty('');
						
						
						$.each(Inventory_Data	, function (index, event) {
							$('#smallSelect-' + ids).append('<option value=' + index + ' selected="">' + Inventory_Data[index].Country + '</option>');
						});
						var i2 = 1;
						while (Inventory_lbl1.length > i2) {
							var tr = $("#tr-" + ids).clone();
							if (i2 != 1) {
								tr.removeClass("border-right");
								tr.removeClass("col-md-4");
								tr.addClass("col-md-2");
							}
							tr.find("#Amount-" + ids).addClass(Inventory_bgs[i2]);
							tr.find("#Amount-" + ids).html(RedDot_NumberFormat(Inventory_Data[0][Inventory_lbl1[i2]].toFixed(2)));
							tr.find("#Amountlbl-" + ids).html([Inventory_lbl2[i2]]);
							$("#tbody-"+ids).append(tr);
							i2++;
						}
						if (Inventory_Data.length > 0) {
							$("#tbody-" + ids).find(".ABC")[0].remove();
						}
						


						$('#smallSelect-' + ids).val('0');

					});

				} catch (e) {
				console.log(e)
				}

				try {
					$("#InventoryBarChart #Inventory_BarChart-V1").each(function (index, item) {
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url1 = $(this).find("#hdnurl").val().split(",");
						
						if (index == 0) {
							Stock_country = [];

							Stock_lbl02 = $(this).find("#hdnlbl2").val().split(",");

							Stock_lbl01 = $(this).find("#hdnlbl1").val().split(",");
							Stock_bgs0 = $(this).find("#hdnbgcolors").val().split(",");
							var ids = $(this).find(".chart-container")[0].childNodes[1].id;
							Stock_Data0 = ''
							debugger
							data_return(Noc, Maindata)
							Stock_Data0 = Maindata_result;
							data_return(Noc + 1, Maindata)
							Stock_Data0I = Maindata_result;
							data_return(Noc + 2, Maindata)
							Stock_Data0II = Maindata_result;
							data_return(Noc + 3, Maindata)
							Stock_Data0III = Maindata_result;
							$("#smallSelectI-" + ids).empty('');
							$("#smallSelectII-" + ids).empty('');


							var country = [];
							var Stock_whsstatus = [];


							$.each(Stock_Data0II, function (index, event) {

								var events1 = $.grep(Stock_whsstatus, function (e) {
									return event.WhsStatus === e.WhsStatus;
								});

								if (events1.length === 0) {
									Stock_whsstatus.push(event);
								}
							});

							debugger
							$('#smallSelectI-' + ids).append('<option value=0 selected="">ALL</option>');
							$.each(Stock_whsstatus, function (index, event) {
								$('#smallSelectI-' + ids).append('<option value=' + index + 1 + ' >' + Stock_whsstatus[index].WhsStatus + '</option>');
							});




							var Stock_BU = [];

							$('#smallSelectII-' + ids).append('<option value=0 selected="">ALL</option>');
							$.each(Stock_Data0II, function (index, event) {
								var events3 = $.grep(Stock_BU, function (e) {
									return event.BU === e.BU;
								});
								if (events3.length === 0) {
									Stock_BU.push(event);
								}
							})



							$.each(Stock_BU, function (index, event) {
								$('#smallSelectII-DASH043').append('<option value=' + index + 1 + ' >' + Stock_BU[index].BU + '</option>');
							});


							$('#smallSelectI-' + ids).val('0');

							var events2 = $.grep(Stock_Data0, function (e) {
								return e.WhsStatus === "ALL";
							});

							var i2 = 0;
							var data = [];
							var data1 = [];
							while (events2.length > i2) {
								data.push(events2[i2].TotalStockValue);
								data1.push(events2[i2].QtyOnHand);
								Stock_country.push(events2[i2].Country)
								i2++;
							}



							var options = {
								series: [{
									name: 'Total Stock Value',

									data: data
								}, {
									name: 'Qty On Hand',

									data: data1
								}],
								chart: {
									type: 'bar',
									height: 240
								},
								plotOptions: {
									bar: {
										borderRadius: 4,
										horizontal: true,
									}
								},
								dataLabels: {
									enabled: false
								},
								xaxis: {
									categories: Stock_country,
									labels: {
										formatter: function (value, index, values) {
											return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

										}
									}
								},
								yaxis: {
									labels: {
										formatter: function (value, index, values) {
											return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

										}
									}
								}
							};

							chartxStockBar0 = new ApexCharts(document.querySelector("#" + ids + ""), options);
							chartxStockBar0.render();
							$("input[name=inlineRadioOptionsInv-" + ids + "][value='Total Stock Value']").attr('checked', 'checked');



							chartxStockBar0.hideSeries("Qty On Hand");
							chartxStockBar0.showSeries("Total Stock Value");



							$('#smallSelectII-' + ids).val('0');
						} else {
							Stock1_country = [];
							StockAgelbl = points_Graph[3] + ',' + points_Graph[4] + ',' + points_Graph[5];
							StockAgelbl = StockAgelbl.split(",");
							Stock_lbl12 = $(this).find("#hdnlbl2").val().split(",");

							Stock_lbl11 = $(this).find("#hdnlbl1").val().split(",");
							Stock_bgs1 = $(this).find("#hdnbgcolors").val().split(",");
							var ids = $(this).find(".chart-container")[0].childNodes[1].id;
							Stock_Data1 = ''
							debugger
							data_return(Noc, Maindata)
							Stock_Data1 = Maindata_result;
							data_return(Noc + 1, Maindata)
							Stock_Data1I = Maindata_result;
							data_return(Noc + 2, Maindata)
							Stock_Data1II = Maindata_result;
							data_return(Noc + 3, Maindata)
							Stock_Data1III = Maindata_result;
							$("#smallSelectI-" + ids).empty('');
							$("#smallSelectII-" + ids).empty('');


							var country = [];
							var Stock_whsstatus = [];


							$.each(Stock_Data1II, function (index, event) {

								var events1 = $.grep(Stock_whsstatus, function (e) {
									return event.WhsStatus === e.WhsStatus;
								});

								if (events1.length === 0) {
									Stock_whsstatus.push(event);
								}
							});

							debugger
							$('#smallSelectI-' + ids).append('<option value=0 selected="">ALL</option>');
							$.each(Stock_whsstatus, function (index, event) {
								$('#smallSelectI-' + ids).append('<option value=' + index + 1 + ' >' + Stock_whsstatus[index].WhsStatus + '</option>');
							});




							var Stock_BU = [];

							$('#smallSelectII-' + ids).append('<option value=0 selected="">ALL</option>');
							$.each(Stock_Data1II, function (index, event) {
								var events3 = $.grep(Stock_BU, function (e) {
									return event.BU === e.BU;
								});
								if (events3.length === 0) {
									Stock_BU.push(event);
								}
							})



							$.each(Stock_BU, function (index, event) {
								$('#smallSelectII-'+ids).append('<option value=' + index + 1 + ' >' + Stock_BU[index].BU + '</option>');
							});

							$.each(Stock_Data1, function (index, event) {
								var events4 = $.grep(country, function (e) {
									return event.Country === e.Country;
								});
								if (events4.length === 0) {
									country.push(event);
								}
							})
							$.each(country, function (index, event) {
								Stock1_country.push(country[index].Country);
							});

							$('#smallSelectI-' + ids).val('0');
							var k = 0;
							
							var events2 = $.grep(Stock_Data1, function (e) {
								return e.WhsStatus === "ALL";
							});
							StockAgeTemparr = [];
							while (k < StockAgelbl.length && events2.length > 0) {
								var points1 = [], points2 = [];
								var Monthsplit = StockAgelbl[k].split('-');
								var found_names1 = $.grep(events2, function (v) {
									return v.Month.toString() === pointsMonth[k+3].toString() && v.Year == Monthsplit[1].toString();
								});
								//,
								var i2 = 0;
								while (found_names1.length > i2) {
									points1.push(found_names1[i2].TotalStockValue);
									points2.push(found_names1[i2].QtyOnHand);
									i2++;
								}
								StockAgeTemparr.push(points1);
								StockAgeTemparr.push(points2);
								k++;

							}
							debugger
							



							var options = {
								series: [{
									name: StockAgelbl[0],
									data: StockAgeTemparr[0],

								}, {
										name: StockAgelbl[1],
										data: StockAgeTemparr[2],
								}, {
										name: StockAgelbl[2],
										data: StockAgeTemparr[4],
								}],
								chart: {
									type: 'bar',
									height: 240
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
									categories: Stock1_country,
									labels: {
										formatter: function (value, index, values) {
											return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

										}
									}
								},
								yaxis: {
									labels: {
										formatter: function (value, index, values) {
											return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

										}
									}
								}
							};

							chartxStockBar1 = new ApexCharts(document.querySelector("#" + ids + ""), options);
							chartxStockBar1.render();
							$("input[name=inlineRadioOptionsInv-" + ids + "][value='Total Stock Value']").attr('checked', 'checked');



							



							$('#smallSelectII-' + ids).val('0');
						}

					});

				} catch (e) {
					console.log(e)
				}
				try {
					$("#InventoryMainBarChart #Inventory_MainBarChart-V1").each(function (index, item) {
						debugger
						

						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						lblStockGit3 = $(this).find("#hdnurl").val().split(",");
						lblStockGit2 = $(this).find("#hdnlbl2").val().split(",");

						lblStockGit1  = $(this).find("#hdnlbl1").val().split(",");
						//Inventory_bgs = $(this).find("#hdnbgcolors").val().split(",");
						var ids = $(this).find(".chart-container")[0].childNodes[1].id;
						
						
						data_return(Noc, Maindata)
						Stock_DataGit = Maindata_result;
						data_return(Noc+1, Maindata)
						Stock_DataGitI = Maindata_result;
						data_return(Noc + 2, Maindata)
						Stock_DataGitII = Maindata_result;
						data_return(Noc + 3, Maindata)
						Stock_DataGitIII = Maindata_result;
						$('#smallSelectI-' + ids).empty('');
						$('#smallSelectII-' + ids).empty('');
						$('#smallSelectIII-' + ids).empty('');

						var country = [];
						var Stock_whsstatus = [];


						$.each(Stock_DataGitII, function (index, event) {

							var events1 = $.grep(Stock_whsstatus, function (e) {
								return event.WhsStatus === e.WhsStatus;
							});

							if (events1.length === 0) {
								Stock_whsstatus.push(event);
							}
						});

						debugger
						$('#smallSelectII-' + ids).append('<option value=0 selected="">ALL</option>');
						$.each(Stock_whsstatus, function (index, event) {
							$('#smallSelectII-' + ids).append('<option value=' + index + 1 + ' >' + Stock_whsstatus[index].WhsStatus + '</option>');
						});




						var Stock_BU = [];

						$('#smallSelectIII-' + ids).append('<option value=0 selected="">ALL</option>');
						$.each(Stock_DataGitII, function (index, event) {
							var events3 = $.grep(Stock_BU, function (e) {
								return event.BU === e.BU;
							});
							if (events3.length === 0) {
								Stock_BU.push(event);
							}
						})



						$.each(Stock_BU, function (index, event) {
							$('#smallSelectIII-' + ids).append('<option value=' + index + 1 + ' >' + Stock_BU[index].BU + '</option>');
						});
						$('#smallSelectI-' + ids).append('<option value=0 selected="">ALL</option>');
						//var StockGitdata = [], StockGitdata1 = [];
						$.each(Stock_DataGit, function (index, event) {
							$('#smallSelectI-' + ids).append('<option value=' + index + 1 + ' >' + Stock_DataGit[index].Country + '</option>');
							StockGit_country.push(Stock_DataGit[index].Country)
							var i = 0
							var data = [], data1 = [];
							while (lblStockGit3.length > i) {
								data1.push(Stock_DataGit[index][lblStockGit2[i]])
								data.push(Stock_DataGit[index][lblStockGit3[i]])
								i++;
							}
							StockGitdata.push(data);
							StockGitdata1.push(data1);
							
						})

						
						var i1 = 0;
						while (i1 < StockGit_country.length) {
							ds_Git.push({

								name: StockGit_country[i1],

								data: StockGitdata[i1]
							});
							ds_Git1.push({

								name: StockGit_country[i1],

								data: StockGitdata1[i1]
							});
							i1++;
						}
						
						

						var options = {
							series: ds_Git,
							chart: {
								type: 'bar',
								height: 350,
								stacked: true,

							},
							responsive: [{
								breakpoint: 480,
								options: {
									legend: {
										position: 'bottom',
										offsetX: -10,
										offsetY: 0
									}
								}
							}],
							xaxis: {

								categories: lblStockGit1,

							}, dataLabels: {


								formatter: function (value, opt) {
									var abr = "";
									if (value < 0) {
										value = -(value);
										abr = '-';
									} else if (value == 0) {
										return  Math.trunc(value).toString();
									}
									if (value >= 1000000000) {
										values =  Math.trunc(value / 1000000000) + 'b';
									} else if (value >= 1000000) {
										values = Math.trunc(value / 1000000) + 'm';
									} else if (value >= 1000) {
										values =  Math.trunc(value / 1000) + 'k';
									} else {
										values = value;
									}

									return abr + values;
								},

							},
							
							fill: {
								opacity: 1
							},
							yaxis: {
								labels: {
									formatter: function (value, index, values) {
										return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");

									}
								}
							},
							legend: {
								position: 'right',
								offsetX: 0,
								offsetY: 50
							},
						};

						chartxStockBarGit = new ApexCharts(document.querySelector("#"+ids+""), options);
						chartxStockBarGit.render();
						$("input[name=inlineRadioOptionsInvMain-" + ids + "][value='Total Stock Value']").attr('checked', 'checked');
						
						
					})

                } catch (e) {
					console.log(e)
                }
			}
		})

		$("#smallSelectI-DASH045").on('change', function () {
			debugger
			$(".loader1").show();
			var t = $("#smallSelectI-DASH045 option:selected").text();

			var i = 0;

			while (StockGit_country.length > i) {
				if (t == "ALL") {
					chartxStockBarGit.showSeries(StockGit_country[i])
				} else
					if (t !== StockGit_country[i]) {
						//$(".apexcharts-legend-text").text(arr[i]).trigger('click');
						chartxStockBarGit.hideSeries(StockGit_country[i])
					} else {
						chartxStockBarGit.showSeries(StockGit_country[i])
					}
				i++;
			}

			$(".loader1").hide();

		})

		$("input[name ='inlineRadioOptionsInvMain-DASH045']").on("change", function () {
			debugger
			var Country_Drop = $("#smallSelectI-DASH045 option:selected").text();
			if (Country_Drop !== 'ALL') {
				$.each(StockGit_country, function (index, event) {
					chartxStockBarGit.showSeries(StockGit_country[index]);
				})
			}
			//if (Country_Drop !== 'ALL') {
			//	$("#smallSelectI-DASH045").val("0").trigger("change");
			//}
			var t = $('input[name="inlineRadioOptionsInvMain-DASH045"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBarGit.updateOptions({
					series: ds_Git
				});
			} else {
				chartxStockBarGit.updateOptions({
					series: ds_Git1
				});
			}

			$.each(StockGit_country, function (index, event) {
				if (Country_Drop !== StockGit_country[index]) {
					chartxStockBarGit.hideSeries(StockGit_country[index])
				} else {
					chartxStockBarGit.showSeries(StockGit_country[index])
				}

			})

		})

		$("#smallSelectII-DASH045").on('change', function () {
			debugger
			$(".loader1").show();
			

			var Country_Drop = $("#smallSelectI-DASH045 option:selected").text();
			if (Country_Drop !== 'ALL') {
			$.each(StockGit_country, function (index, event) {
				chartxStockBarGit.showSeries(StockGit_country[index]);
			})
				}
			var WareHouse_Drop = $("#smallSelectII-DASH045 option:selected").text();
			var BU_Drop = $("#smallSelectIII-DASH045 option:selected").text();
			debugger
			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_DataGit, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_DataGitI, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_DataGitII, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_DataGitIII, function (e) {
					return e.BU === BU_Drop;
				});

			}

			StockGit_country = [];
			ds_Git = [], ds_Git1 = [];
			StockGitdata = [], StockGitdata1 = [];
			//$('#smallSelectI-DASH045').empty('');
			//$('#smallSelectI-DASH045').append('<option value="0" selected="">ALL</option>');
			var k = 1;
			$.each(events2, function (index, event) {		
				var i = 0
				var data = [], data1 = [];
				while (lblStockGit3.length > i) {
					data1.push(events2[index][lblStockGit2[i]])
					data.push(events2[index][lblStockGit3[i]])
					i++;
				}
				StockGit_country.push(events2[index].Country)

			//	$('#smallSelectI-DASH045').append('<option value="'+k+'" selected="">'+events2[index].Country+'</option>');
				StockGitdata.push(data);
				StockGitdata1.push(data1);
				k++;
			})


			var i1 = 0;
			while (i1 < StockGit_country.length) {
				ds_Git.push({

					name: StockGit_country[i1],

					data: StockGitdata[i1]
				});
				ds_Git1.push({

					name: StockGit_country[i1],

					data: StockGitdata1[i1]
				});
				i1++;
			}
			
			var t = $('input[name="inlineRadioOptionsInvMain-DASH045"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBarGit.updateOptions({
					series: ds_Git
				});
			} else {
				chartxStockBarGit.updateOptions({
					series: ds_Git1
				});
			}
			if (Country_Drop !== 'ALL') {
				$.each(StockGit_country, function (index, event) {
					if (Country_Drop !== StockGit_country[index]) {
						chartxStockBarGit.hideSeries(StockGit_country[index])
					} else {
						chartxStockBarGit.showSeries(StockGit_country[index])
					}

				})
			}
				//$("#smallSelectI-DASH045").val("0").trigger("change");
			
			$(".loader1").hide();

		})


		$("#smallSelectIII-DASH045").on('change', function () {
			$(".loader1").show();
			var Country_Drop = $("#smallSelectI-DASH045 option:selected").text();
			if (Country_Drop !== 'ALL') {
				$.each(StockGit_country, function (index, event) {
					chartxStockBarGit.showSeries(StockGit_country[index]);
				})
			}
			//if (Country_Drop!=='ALL') {
			//	$("#smallSelectI-DASH045").val("0").trigger("change");
   //         }
			
			var WareHouse_Drop = $("#smallSelectII-DASH045 option:selected").text();
			var BU_Drop = $("#smallSelectIII-DASH045 option:selected").text();
			debugger
			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_DataGit, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_DataGitI, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_DataGitII, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_DataGitIII, function (e) {
					return e.BU === BU_Drop;
				});

			}

			StockGit_country = [];
			StockGitdata = [], StockGitdata1 = [];
			ds_Git = [], ds_Git1 = []; 
			//	$('#smallSelectI-DASH045').empty('');
			//$('#smallSelectI-DASH045').append('<option value="0" selected="">ALL</option>');
			var k=1;
			$.each(events2, function (index, event) {
				var i = 0
				var data = [], data1 = [];
				while (lblStockGit3.length > i) {
					data1.push(events2[index][lblStockGit2[i]])
					data.push(events2[index][lblStockGit3[i]])
					i++;
				}
				StockGit_country.push(events2[index].Country)

				//$('#smallSelectI-DASH045').append('<option value="' + k + '" selected="">' + events2[index].Country + '</option>');
				StockGitdata.push(data);
				StockGitdata1.push(data1);
				k++;
			})


			var i1 = 0;
			while (i1 < StockGit_country.length) {
				ds_Git.push({

					name: StockGit_country[i1],

					data: StockGitdata[i1]
				});
				ds_Git1.push({

					name: StockGit_country[i1],

					data: StockGitdata1[i1]
				});
				i1++;
			}

			var t = $('input[name="inlineRadioOptionsInvMain-DASH045"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBarGit.updateOptions({
					series: ds_Git
				});
			} else {
				chartxStockBarGit.updateOptions({
					series: ds_Git1
				});
			}
			//$("#smallSelectI-DASH045").val("0").trigger("change");
			if (Country_Drop !== 'ALL') {
				$.each(StockGit_country, function (index, event) {
					if (Country_Drop !== StockGit_country[index]) {
						chartxStockBarGit.hideSeries(StockGit_country[index])
					} else {
						chartxStockBarGit.showSeries(StockGit_country[index])
					}

				})
			}
			$(".loader1").hide();

		})

		$("input[name='inlineRadioOptionsInv-DASH043']").on('change', function () {
			var t = $('input[name="inlineRadioOptionsInv-DASH043"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar0.hideSeries("Qty On Hand");
				chartxStockBar0.showSeries("Total Stock Value");
			} else {
				chartxStockBar0.showSeries("Qty On Hand");
				chartxStockBar0.hideSeries("Total Stock Value");
			}



		})
		$("#smallSelectI-DASH043").on('change', function () {
			var WareHouse_Drop = $("#smallSelectI-DASH043 option:selected").text();
			var BU_Drop = $("#smallSelectII-DASH043 option:selected").text();
			debugger
			Stock_country = [];
			chartxStockBar0.showSeries("Qty On Hand");
			chartxStockBar0.showSeries("Total Stock Value");


			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_Data0, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});

			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {

				events2 = $.grep(Stock_Data0I, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data0II, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_Data0III, function (e) {
					return e.BU === BU_Drop;
				});

			}



			//--------------

			//if (t1 !== "ALL" && t == "ALL") {
			//	events2 = $.grep(Stock_DataIII, function (e) {
			//		return e.BU === t1;
			//	});

			//}
			//
			//else if (t1 !== "ALL" && t !== "ALL") {
			//	events2 = $.grep(Stock_DataII, function (e) {
			//		return e.WhsStatus === t && e.BU === t1;
			//	});
			//}
			//else if (t1 == "ALL" && t == "ALL") {
			//	events2 = $.grep(Stock_Data, function (e) {
			//		return e.WhsStatus === t && e.BU === t1;
			//	});
			//}
			//else if (t1 == "ALL" && t !== "ALL") {
			//	events2 = $.grep(Stock_DataI, function (e) {
			//		return e.WhsStatus === t && e.BU === t1;
			//	});
			//}
			//else {

			//	events2 = $.grep(Stock_Data, function (e) {
			//		return e.WhsStatus === t;
			//	});							
			//         }


			var i2 = 0;
			var data = [];
			var data1 = [];
			while (events2.length > i2) {
				data.push(events2[i2].TotalStockValue);
				data1.push(events2[i2].QtyOnHand);
				Stock_country.push(events2[i2].Country)
				i2++;
			}
			chartxStockBar0.updateOptions({
				series: [{
					name: 'Total Stock Value',

					data: data
				}, {
					name: 'Qty On Hand',

					data: data1
				}], xaxis: {

					categories: Stock_country,



				}
			});

			var t = $('input[name="inlineRadioOptionsInv-DASH043"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar0.hideSeries("Qty On Hand");
				chartxStockBar0.showSeries("Total Stock Value");
			} else {
				chartxStockBar0.showSeries("Qty On Hand");
				chartxStockBar0.hideSeries("Total Stock Value");
			}
		})
		$("#smallSelectII-DASH043").on('change', function () {
			var WareHouse_Drop = $("#smallSelectI-DASH043 option:selected").text();
			var BU_Drop = $("#smallSelectII-DASH043 option:selected").text();
			debugger
			Stock_country = [];
			chartxStockBar0.showSeries("Qty On Hand");
			chartxStockBar0.showSeries("Total Stock Value");


			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_Data0, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});

			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {

				events2 = $.grep(Stock_Data0I, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data0II, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_Data0III, function (e) {
					return e.BU === BU_Drop;
				});

			}
			//var t = $("#smallSelectI-DASH043 option:selected").text();
			//var t1 = $("#smallSelectII-DASH043 option:selected").text();
			//debugger
			//Stock_country = [];
			//var events2 = '';
			//chartxStockBar0.showSeries("Qty On Hand");
			//chartxStockBar0.showSeries("Total Stock Value");

			//if (t1 !== "ALL" && t == "ALL") {
			//	events2 = $.grep(Stock_DataIII, function (e) {
			//		return e.BU === t1;
			//	});


			//} else if (t1 == "ALL" && t == "ALL") {
			//	events2 = $.grep(Stock_Data, function (e) {
			//		return e.WhsStatus === t && e.BU === t1;
			//	});
			//}else if ( t1 !=="ALL") {
			//	events2 = $.grep(Stock_DataII, function (e) {
			//		return e.WhsStatus === t && e.BU === t1;
			//	});
			//         }
			//else {

			//	events2 = $.grep(Stock_DataI, function (e) {
			//		return e.WhsStatus === t;
			//	});
			//}


			var i2 = 0;
			var data = [];
			var data1 = [];
			while (events2.length > i2) {
				data.push(events2[i2].TotalStockValue);
				data1.push(events2[i2].QtyOnHand);
				Stock_country.push(events2[i2].Country)
				i2++;
			}
			chartxStockBar0.updateOptions({
				series: [{
					name: 'Total Stock Value',

					data: data
				}, {
					name: 'Qty On Hand',

					data: data1
				}], xaxis: {

					categories: Stock_country,



				}
			});
			var t = $('input[name="inlineRadioOptionsInv-DASH043"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar0.hideSeries("Qty On Hand");
				chartxStockBar0.showSeries("Total Stock Value");
			} else {
				chartxStockBar0.showSeries("Qty On Hand");
				chartxStockBar0.hideSeries("Total Stock Value");
			}
		})

		$("#smallSelectI-DASH044").on('change', function () {
			var WareHouse_Drop = $("#smallSelectI-DASH044 option:selected").text();
			var BU_Drop = $("#smallSelectII-DASH044 option:selected").text();
			debugger
			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_Data1, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data1I, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data1II, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_Data1III, function (e) {
					return e.BU === BU_Drop;
				});

			}

			StockAgeTemparr = [];
			var k = 0;
			while (k < StockAgelbl.length && events2.length > 0) {
				Stock1_country = [];
				var points1 = [], points2 = [];
				var Monthsplit = StockAgelbl[k].split('-');
				var found_names1 = $.grep(events2, function (v) {
					return v.Month.toString() === pointsMonth[k + 3].toString() && v.Year == Monthsplit[1].toString();
				});
				//,
				var i2 = 0;
				while (found_names1.length > i2) {
					points1.push(found_names1[i2].TotalStockValue);
					points2.push(found_names1[i2].QtyOnHand);
					Stock1_country.push(found_names1[i2].Country);
					i2++;
				}
				StockAgeTemparr.push(points1);
				StockAgeTemparr.push(points2);
				k++;

			}
			var t = $('input[name="inlineRadioOptionsInv-DASH044"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[0],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[2],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[4],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			} else {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[1],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[3],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[5],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			}

		})



		$("#smallSelectII-DASH044").on('change', function () {
			var WareHouse_Drop = $("#smallSelectI-DASH044 option:selected").text();
			var BU_Drop = $("#smallSelectII-DASH044 option:selected").text();
			debugger
			var events2 = '';
			if (BU_Drop == "ALL" && WareHouse_Drop == "ALL") {
				events2 = $.grep(Stock_Data1, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else if (BU_Drop == "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data1I, function (e) {
					return e.WhsStatus === WareHouse_Drop;
				});
			}
			else if (BU_Drop !== "ALL" && WareHouse_Drop !== "ALL") {
				events2 = $.grep(Stock_Data1II, function (e) {
					return e.WhsStatus === WareHouse_Drop && e.BU === BU_Drop;
				});
			}
			else {
				events2 = $.grep(Stock_Data1III, function (e) {
					return e.BU === BU_Drop;
				});

			}

			StockAgeTemparr = [];
			var k = 0;
			while (k < StockAgelbl.length && events2.length > 0) {
				Stock1_country = [];
				var points1 = [], points2 = [];
				var Monthsplit = StockAgelbl[k].split('-');
				var found_names1 = $.grep(events2, function (v) {
					return v.Month.toString() === pointsMonth[k + 3].toString() && v.Year == Monthsplit[1].toString();
				});
				//,
				var i2 = 0;
				while (found_names1.length > i2) {
					points1.push(found_names1[i2].TotalStockValue);
					points2.push(found_names1[i2].QtyOnHand);
					Stock1_country.push(found_names1[i2].Country);
					i2++;
				}
				StockAgeTemparr.push(points1);
				StockAgeTemparr.push(points2);
				k++;

			}
			var t = $('input[name="inlineRadioOptionsInv-DASH044"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[0],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[2],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[4],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			} else {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[1],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[3],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[5],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			}

		})
		$("input[name='inlineRadioOptionsInv-DASH044']").on('change', function () {
			var t = $('input[name="inlineRadioOptionsInv-DASH044"]:checked').val();
			if (t == "Total Stock Value") {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[0],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[2],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[4],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			} else {
				chartxStockBar1.updateOptions({
					series: [{
						name: StockAgelbl[0],
						data: StockAgeTemparr[1],

					}, {
						name: StockAgelbl[1],
						data: StockAgeTemparr[3],
					}, {
						name: StockAgelbl[2],
						data: StockAgeTemparr[5],
					}], xaxis: {

						categories: Stock1_country,



					}
				});
			}


		})
		
		$("#smallSelect-DASH042").on('change', function () {
			var t = $(this).val();
			var i2 = 1;
			$("#tbody-DASH042").find(".ABC").not(':first').remove();
			$("#tr-DASH042").find("#Amount-DASH042").removeClass("text-danger");
			while (Inventory_lbl1.length > i2) {
				var tr = $("#tr-DASH042").clone();
				if (i2 != 1) {
					tr.removeClass("border-right");
					tr.removeClass("col-md-4");
					tr.addClass("col-md-2");
				}
				tr.find("#Amount-DASH042").addClass(Inventory_bgs[i2]);
				tr.find("#Amount-DASH042").html(RedDot_NumberFormat(Inventory_Data[t][Inventory_lbl1[i2]].toFixed(2)));
				tr.find("#Amountlbl-DASH042").html([Inventory_lbl2[i2]]);
				$("#tbody-DASH042").append(tr);
				i2++;
			}
			if (Inventory_Data.length > 0) {
				$("#tbody-DASH042").find(".ABC")[0].remove();
			}
		});
		$("#smallSelect-DASH030").on('change', function () {
			$("#six_months-DASH030").trigger('click');
			var t = $(this).val();
			var arr = ['OPEN', 'WON OPG', 'WON-R OPG', 'LOST-CLOSED', 'EXPECTED CLOSE'];
			var i = 0;
			
			while (arr.length > i) {
				if (t == "ALL") {
					chartfunnel.showSeries(arr[i])
				}else
				if (t !== arr[i] ) {
					//$(".apexcharts-legend-text").text(arr[i]).trigger('click');
					chartfunnel.hideSeries(arr[i])
				} else {
					chartfunnel.showSeries(arr[i])
				}
				i++;
			}
			

			
		})

		$("#smallSelect-DASH038").on('change', function () {
			
			var t = $(this).val();
			
			var i = 0;

			while (lbl_Debtor.length > i) {
				if (t == "All") {
					chartDebator.showSeries(lbl_Debtor[i])
				} else
					if (t !== lbl_Debtor[i]) {
						//$(".apexcharts-legend-text").text(arr[i]).trigger('click');
						chartDebator.hideSeries(lbl_Debtor[i])
					} else {
						chartDebator.showSeries(lbl_Debtor[i])
					}
				i++;
			}



		})
		$("#smallSelect-DASH037").on('change', function () {
			
			$("#six_months-DASH037").trigger('click');
			var t = parseInt($(this).val())*2;
			i = 0;
			
			chartbudget.updateSeries([{ 
				name: lbl_budget[i],
				data: ds_Budget[t]
			},
			{
				name: lbl_budget[i+1],
				data: ds_Budget[t+1]
			} ]);
		});
		var resetCssClasses = function (activeEl) {
			var els = document.querySelectorAll('button')
			Array.prototype.forEach.call(els, function (el) {
				el.classList.remove('active')
			})

			activeEl.target.classList.add('active')
		}

		$('#Three_month-DASH030').on('click', function (e) {
				resetCssClasses(e)
				t = 0
				var k = parseInt(ds_funnel[0].length)
				var k1 = 0;
				if (k <= 5) {
					k = 0;
					k1 = 0
					k2 = 0;
					k3 = 0;
					k4 = 0;
				} else {
					k = ds_funnel[t][5];
					k1 = ds_funnel[t + 1][5];
					k2 = ds_funnel[t + 2][5];
					k3 = ds_funnel[t + 3][5]
					k4 = ds_funnel[t + 4][5]
				}
				var lbl = points_Graph[3] + ',' + points_Graph[4] + ',' + points_Graph[5];
				lbl = lbl.split(",");
				var t = 0;
				var d = ds_funnel[t][3] + "," + ds_funnel[t][4] + "," + k.toString();
				d = d.split(",")
				var d1 = ds_funnel[t + 1][3] + "," + ds_funnel[t + 1][4] + "," + k1.toString();
				d1 = d1.split(",")
				var d2 = ds_funnel[t + 2][3] + "," + ds_funnel[t + 2][4] + "," + k2.toString();
				d2 = d2.split(",")
				var d3 = ds_funnel[t + 3][3] + "," + ds_funnel[t + 3][4] + "," + k3.toString();
				d3 = d3.split(",")
				var d4 = ds_funnel[t + 4][3] + "," + ds_funnel[t + 4][4] + "," + k4.toString();
				d4 = d4.split(",")

				chartfunnel.updateOptions({
					series: [{
						name: lbl_funnel[0],
						data: d
					}, {
						name: lbl_funnel[1],
						data: d1
						}, {
							name: lbl_funnel[2],
							data: d2
						}, {
							name: lbl_funnel[3],
							data: d3
						}, {
						name: lbl_funnel[4],
						data: d4
						}],

					xaxis: {

						categories: lbl,



					}, yaxis: {
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
								} else if (value == undefined) {
									values = 0;
								}
									else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})
				
			})

		$('#six_months-DASH030').on('click', function (e) {
				resetCssClasses(e)
				var t = 0;
				
				chartfunnel.updateOptions({
					series: [{
						name: lbl_funnel[0],
						data: ds_funnel[t]
					}, {
						name: lbl_funnel[1],
							data: ds_funnel[t+1]
					}, {
						name: lbl_funnel[2],
							data: ds_funnel[t+2]
					}, {
						name: lbl_funnel[3],
							data: ds_funnel[t+3]
					}, {
						name: lbl_funnel[4],
							data: ds_funnel[t+4]
						}],

					xaxis: {

						categories: points_Graph,



					}, yaxis: {
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
								} else if (value == undefined) {
									values = 0;
								} else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})
				
			})

		$('#Three_month-DASH037').on('click', function (e) {
				
				resetCssClasses(e);	
				var lbl = points_Graph[3] + ',' + points_Graph[4] + ',' + points_Graph[5];
				lbl = lbl.split(",");
				var t = parseInt($("#smallSelect-DASH037").val())*2;
				var d = ds_Budget[t][3] + "," + ds_Budget[t][4] + "," + ds_Budget[t][5];
				d=d.split(",")
				var d1 = ds_Budget[t + 1][3] + "," + ds_Budget[t + 1][4] + "," + ds_Budget[t + 1][5];
				d1 = d1.split(",")

				chartbudget.updateOptions({
					series: [{						
							name: lbl_budget[0],
							data: d
						}, {
								name: lbl_budget[1],
								data: d1						
					}],
					
					xaxis: {

						categories: lbl,
						


					}, yaxis: {
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
								} else if (value == undefined) {
									values = 0;
								} else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})


			})

		$('#six_months-DASH037').on('click', function (e) {
				resetCssClasses(e)
				var t = parseInt($("#smallSelect-DASH037").val()) *2;				
				chartbudget.updateOptions({
					series: [{
						name: lbl_budget[0],
						data: ds_Budget[t]
					}, {
						name: lbl_budget[1],
							data: ds_Budget[t+1]
					}],

					xaxis: {

						categories: points_Graph,



					}, yaxis: {
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
								} else if (value == undefined) {
									values = 0;
								} else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})
			})
		$('#Three_month-DASH031').on('click', function (e) {
				
				resetCssClasses(e)
				t = 0
				var k = parseInt(ds_Breadth[0].length)
				var k1 = 0;
				if (k <=5) {
					k = 0;
				} else {
					k = ds_Breadth[t + 1][5];
					k1 = 0;
				}
				var lbl = points_Graph[3] + ',' + points_Graph[4] + ',' + points_Graph[5];
				lbl = lbl.split(",");
				var t =  0;
				var d = ds_Breadth[t][3] + "," + ds_Breadth[t][4] + "," + k.toString();
				d = d.split(",")
				var d1 = ds_Breadth[t + 1][3] + "," + ds_Breadth[t + 1][4] + "," + k.toString();
				d1 = d1.split(",")

				chartBreadth.updateOptions({
					series: [{
						name: lbl_budget[0],
						data: d
					}, {
						name: lbl_budget[1],
						data: d1
					}],

					xaxis: {

						categories: lbl,



					}, yaxis: {
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
								}else if (value == undefined) {
									values = 0;
								} else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})


			})

		$('#six_months-DASH031').on('click', function (e) {
				resetCssClasses(e)
				var t = 0;
				chartBreadth.updateOptions({
					series: [{
						name: lbl_Breadth[0],
						data: ds_Breadth[t]
					}, {
						name: lbl_Breadth[1],
						data: ds_Breadth[t + 1]
					}],

					xaxis: {

						categories: points_Graph,



					}, yaxis: {
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
								} else if (value == undefined) {
									values = 0;
								}else {
									values = value;
								}

								return abr + values;
							}
						}
					},
				})

			})

		$("#pills-Month-tab").on('click', function () {
			var t = $('input[name="inlineRadioOptions"]:checked').val();
			
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
		$("input[name='inlineRadioOptions']").on('change', function () {
			
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
				case 19:
					Maindata_result = Maindata.Table19;
					break;
				case 20:
					Maindata_result = Maindata.Table20;
					break;
				case 21:
					Maindata_result = Maindata.Table21;
					break;
				case 22:
					Maindata_result = Maindata.Table22;
					break;
				case 23:
					Maindata_result = Maindata.Table23;
					break;
				case 24:
					Maindata_result = Maindata.Table24;
					break;
				case 25:
					Maindata_result = Maindata.Table25;
					break;
				case 26:
					Maindata_result = Maindata.Table26;
					break;
				case 27:
					Maindata_result = Maindata.Table27;
					break;
				case 28:
					Maindata_result = Maindata.Table28;
					break;
				case 29:
					Maindata_result = Maindata.Table29;
					break;
				case 30:
					Maindata_result = Maindata.Table30;
					break;
				case 31:
					Maindata_result = Maindata.Table31;
					break;
				case 32:
					Maindata_result = Maindata.Table32;
					break;
				case 33:
					Maindata_result = Maindata.Table33;
					break;
				case 34:
					Maindata_result = Maindata.Table34;
					break;
				case 35:
					Maindata_result = Maindata.Table35;
					break;
				case 36:
					Maindata_result = Maindata.Table36;
					break;
				case 37:
					Maindata_result = Maindata.Table37;
					break;
				case 38:
					Maindata_result = Maindata.Table38;
					break;
				case 39:
					Maindata_result = Maindata.Table39;
					break;
				case 40:
					Maindata_result = Maindata.Table40;
					break;
				case 41:
					Maindata_result = Maindata.Table41;
					break;
				case 42:
					Maindata_result = Maindata.Table42;
					break;
				case 43:
					Maindata_result = Maindata.Table43;
					break;
				case 44:
					Maindata_result = Maindata.Table44;
					break;
				case 45:
					Maindata_result = Maindata.Table45;
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


		var tf1 = true;
		var UsersWidget = {
			UserDashWidgets: []
		};
		$("#btnsave").on("click", function () {


			$(".mar-b10").each(function (index, item) {

				var DashidTxt = $(this).find("[id='Inphdn']").val();
				var IsActiveTxt = $(this).find("[id='ChkDash']").is(":checked");

				var UserDashWidget = {
					DashId: DashidTxt,
					IsActive: IsActiveTxt,


				};
				if (DashidTxt != undefined)
					UsersWidget.UserDashWidgets.push(UserDashWidget);
			});
			$.post("/SaveUserDash", UsersWidget).done(function (response) {
				if (response.SaveFlag == true) {
					tf = false;

					RedDotAlert_Success('Save Succcesfully');
					//$(".leftwidgets").load('/Account/GetDashBoardView');
					//var newUrl = '/Dashboard/Index';
					//	window.location.href = newUrl;
					$('.close').trigger("click");

				} else {
					//	RedDotAlert_Error('Error Occur');
				}

			});

		})
       
	}
}