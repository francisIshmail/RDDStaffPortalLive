var MainDashBoard = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		var Maindata;
		var Maindata_result = '';
		MainDashBoard.Attachevent();
	},
	Attachevent: function () {
		
		$.ajax({
			async: false,
			cache: false,
			type: "POST",
			url: "/Get_MainDashBoard",
			
			contentType: "Application/json",
			dataType: 'JSON',
			success: function (response) {
				$(".loader1").show();
				Maindata = response;
				console.log(Maindata);
				
				try {
					$("#Cards #Firstcard").each(function (index, item) {
						var url = $(this).find("#hdnurl").val();
						var Col = $(this).find("#hdnColumns").val();
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var ids = $(this).find(".card")[0].childNodes[1].id;
						var lbl = $(this).find(".ds1").text().split(" ");
						var Card_data = '';
						//switch (Noc) {
						//	case 13:
						//		Card_data = Maindata.Table13
						//		break;
						//}
						data_return(Noc, Maindata)
						Card_data = Maindata_result;
						$(this).find(".ds2").text(lbl[0] + " Achieved");

						if ($(this).find(".ds1").text() == "Rev Target") {
							$(this).find(".A1").text("$" + Card_data[0].RevTarget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".B1").text("$" + Card_data[0].ActualRev.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".perv").text(Card_data[0].RevTrgetAcheivedPercent);
							var newClass = (Card_data[0].RevTrgetAcheivedPercent > 100) ? 100 :
								Card_data[0].RevTrgetAcheivedPercent;
							$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + newClass + '')


						} else if ($(this).find(".ds1").text() == "Rev Forecast") {
							$(this).find(".A1").text("$" + Card_data[0].RevForecast.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".B1").text("$" + Card_data[0].ActualRev.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".perv").text(Card_data[0].RevForecastAcheivedPercent);
							var newClass = (Card_data[0].RevForecastAcheivedPercent > 100) ? 100 :
								Card_data[0].RevForecastAcheivedPercent;
							$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + newClass + '')
						} else if ($(this).find(".ds1").text() == "GP Target") {
							$(this).find(".A1").text("$" + Card_data[0].GPTarget.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".B1").text("$" + Card_data[0].ActualGP.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".perv").text(Card_data[0].GPTrgetAcheivedPercent);
							var newClass = (Card_data[0].GPTrgetAcheivedPercent > 100) ? 100 :
								Card_data[0].GPTrgetAcheivedPercent;
							$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + newClass + '')

						} else {
							$(this).find(".A1").text("$" + Card_data[0].GPForecast.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".B1").text("$" + Card_data[0].ActualGP.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
							$(this).find(".perv").text(Card_data[0].GPForecastAcheivedPercent);
							var newClass = (Card_data[0].GPForecastAcheivedPercent > 100) ? 100 :
								Card_data[0].GPForecastAcheivedPercent;
							$(this).find(".progress-bar").removeClass("w-75").addClass('w-' + newClass + '')
						}



					});
				} catch (e) {
					console.log(e);
				}

				try {
					$("#dt #Datatables1").each(function (index, item) {
						
						var url = ['','1','2','3','4','5'];
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
                try {
					$("#pis  #PiChart").each(function (index, item) {
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url = $(this).find("#hdnurl").val();
						var ids = $(this).find(".chart-container")[0].childNodes[1].id;
						var pts = [];
						var lbl = [];
						var bg = [];
						var Pi_data;
						data_return(Noc, Maindata)
						Pi_data	 = Maindata_result;
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
					$("#bars,#bars1 #BarChart,#BarChart1").each(function (index, item) {
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
						data_return(Noc, Maindata)
						Bardata = Maindata_result;
						//switch (Noc) {
						//	case 12:
						//		Bardata = Maindata.Table12;
						//		break;
						//	case 9:
						//		Bardata = Maindata.Table9;
						//		break;
						//}
						if (item.id == 'bars') {
							lblarr1 = [];
							
                        } 
						
						
						var ds = [];
						var points = [];
						var points1 = [];
						var points2 = [];
								i = 0;
						while (i < Bardata.length) {
							if (item.id == 'bars') {
								lblarr1.push(Bardata[i]["BU"]);
								points.push(Bardata[i][lblarr2[0].replace(' ', '').trim('')]);
								points1.push(Bardata[i][lblarr2[1].replace(' ', '').trim('')]);
							} else {
								debugger
								points.push(Bardata[i][lblarr2[0].replace(' ', '').trim('')]);
								points1.push(Bardata[i][lblarr2[1].replace(' ', '').trim('')]);
								points2.push(Bardata[i][lblarr2[2].replace(' ', '').trim('')]);
                            }
							

									var k = 0
									
								
								
									i++;
						}
						var tempArray = [];


						tempArray.push(points);
						tempArray.push(points1);
						if (item.id !== 'bars') {
							tempArray.push(points2);
						}
						pts.push(tempArray);
						
						
						
						i = 0;
						while (i < lblarr2.length) {
						
							
							ds.push({
								label: lblarr2[i],
								backgroundColor: bgarr[i],
								fill: true,
								data: pts[0][i]
							});
							
							i++;
						}

						$("#second_tab").find(".card-title").text($("#bars1").find(".card-title").text());
						if (ids == 'DASH017') {
							ids = 'multipleLineChart1';
							$('#second_tab,#pills-Month-tab1').show();
						} else if (ids == 'DASH018') {
							ids = 'SalesAllCountry1';
							$('#second_tab,#pills-Quarter-tab1').show();

						}
						$("#bars1,#bars1").hide();
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
								scales: {
									xAxes: [{
										ticks: {}
									}],
									yAxes: [{
										ticks: {
											beginAtZero: true,
											userCallback: function (value, index, values) {
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


												return '$ ' + abr + values.toString();// value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
											}
										}
									}]
								},
								layout: {
									padding: { left: 15, right: 15, top: 15, bottom: 15 }
								}
							}
						});

						mySalesAllCountry.render();


					});

                } catch (e) {
					console.log(e);
				}
				try {
					$("#lins #MultilineChart").each(function (index, item) {
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
						var Line_data = '' 
						data_return(Noc, Maindata)
						Line_data= Maindata_result;
						//switch (Noc) {
						//	case 9:
						//		Line_data = Maindata.Table9;
						//		break;
							
						//}
						

						
						var points = [];
						var points1 = [];
						var points2 = [];
						i = 0;
						while (i < Line_data.length) {
							//lblarr1.push(Line_data[i]["BU"]);
							points.push(Line_data[i][lblarr2[0].replace(' ', '').trim('')]);
							points1.push(Line_data[i][lblarr2[1].replace(' ', '').trim('')]);
							points2.push(Line_data[i][lblarr2[2].replace(' ', '').trim('')]);

							



							i++;
						}
						var tempArray = [];


						tempArray.push(points);
						tempArray.push(points1);
						tempArray.push(points2);

						pts.push(tempArray);



						

						var ds = [];
						var i = 0;
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
								data: pts[0][i]
							});
							i++;
						}
						
						var chartoption = {
							type: 'line',
							data: {
								labels: lblarr1,
								datasets: ds,
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
								scales: {
									xAxes: [{
										ticks: {}
									}],
									yAxes: [{
										ticks: {
											beginAtZero: true,
											userCallback: function (value, index, values) {

												if (value > 0) {
													if (value >= 1000000000) {
														values = (value / 1000000000) + 'b';
													} else if (value >= 1000000) {
														values = (value / 1000000) + 'm';
													} else if (value >= 1000) {
														values = (value / 1000) + 'k';
													} else {
														values = value;
													}
												} else {
													if (value <= 1000) {
														values = (value / 1000) + 'k';
													} else if (value <= 1000000) {
														values = (value / 1000000) + 'm';
													} else if (value <= 1000000000) {
														values = (value / 1000000000) + 'b';
													}
												}

												return '$ ' + values.toString();// value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
											}
										}
									}]
								},
								layout: {
									padding: { left: 15, right: 15, top: 15, bottom: 15 }
								}
							}
						};
						$("#first_tab").find(".card-title").text($("#lins").find(".card-title").text());
						if (ids == 'DASH005') {
							ids = 'multipleLineChart';
							$('#first_tab,#pills-Month-tab').show();

						} else if (ids == 'DASH016') {
							ids = 'SalesAllCountry';
							$('#first_tab,#pills-Quarter-tab').show();

						}
						$('#lins,#lins').hide();
						var myMultipleLineChart = new Chart(ids, chartoption);
						myMultipleLineChart.render();

						//if (tf1 == true) {
						//	var myMultipleLineChart1 = new Chart("#multipleLineChart", chartoption);
						//	myMultipleLineChart1.render();
						//	tf1 = false;
						//         }



					});

                } catch (e) {
					console.log(e)
				}
				try {
					$("#SecCard #SecondCard").each(function (index, item) {
						var Noc = parseInt($(this).find("#hdnNoofColumns").val());
						var url = $(this).find("#hdnurl").val().split(",");
						var lblarr = $(this).find("#hdnlbl2").val().split(",");

						var Secondcard_data = Maindata.Table14;

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

						if (ids !== 'DASH021') {
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
									if (ids == 'DASH020') {
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
								case 15:
									Bank_Data = Maindata.Table15;
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

			}, complete: function () {
                
				$(".loader1").hide();
				
			}
		})

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
				case 15:
					Maindata_result = Maindata.Table15;
					break;
				case 16:
					Maindata_result = Maindata.Table16;
					break;
				case 17:
					Maindata_result = Maindata.Table17;
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