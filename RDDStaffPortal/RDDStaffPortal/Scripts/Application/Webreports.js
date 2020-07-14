var Webreports = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		Webreports.Attachevent();
	},
	Attachevent: function () {
		var selectedObjs;
		$(".loader1").show();
		// drop down image fill with name 
		RdotDropimg("Userid", "/GetUserList");
		var colms = [
			//{ "mDataProp": "CardCode", "sWidth": "30%" },
			{
				"mDataProp": "reportTitle", render: function (data, type, full) {
					if (full.IsAlreadyMapped == true) {
						//style='background-color:" + full.bgcolor + "
						return "<li data-id='" + full.repTypeId + "' class='ui-draggable ui-draggable-handle disabled' '><a href = '#' style=''> " + full.reportTitle + " </a><input type='hidden' id='hdnuse' value='" + full.IsAlreadyMapped + "'/></li>";
					} else {
						return "<li data-id='" + full.repTypeId + "' class='ui-draggable ui-draggable-handle''><a href = '#' style=''> " + full.reportTitle + " </a><input type='hidden' id='hdnuse' value='" + full.IsAlreadyMapped + "'/></li>";
                    }
					

				}
			}




		];
		//SAPAE   SAPKE	 SAPTZ	 SAPUG	SAPZM	SAPML	SAPTRI""
		RdottableNDWPara1("tblReports", "/GetWebReportMapData", colms,"",15);


		var tf = true;
		$("#Userid").on("change", function () {

			var Userid = $('#Userid Option:selected').val();
			if (Userid == '0' && tf == true) {
				RedDotAlert_Error("Please select  User");
				return false

			}
			tf = true;
			RdottableNDWPara1("tblReports", "/GetWebReportMapData", colms, Userid,15);
			$(".basket_list  ul").empty();
			$('#product  li').draggable({
				revert: true,
				start: function (event, ui) {
					//get all selected...
					if (ui.helper.hasClass('selected')) selectedObjs = $('li.selected');
					else {
						selectedObjs = $(ui.helper);
						$('li.selected').removeClass('selected')
					}
				},
				drag: function (event, ui) {
					selectedObjs.each(function () {
						ui.helper.data('dropped', false);
						$(this).addClass("selected");
						$(this).closest("#product").addClass("selected");
					})

				},
				stop: function (event, ui) {
					//alert('stop: dropped=' + ui.helper.data('dropped'));
					selectedObjs.each(function () {
						$(this).removeClass("selected").closest("#product").removeClass("selected");
					})

				}
			}

			).click(function (e) {
				debugger
				if ($(this).find("input[id='hdnuse']").val() == "true") {
					RedDotAlert_Error('Alredy use');
					return
				}
				$(this).toggleClass('selected')
			});
			//$("#product  li").draggable({

			//	// brings the item back to its place when dragging is over
			//	revert: true,

			//	// once the dragging starts, we decrease the opactiy of other items
			//	// Appending a class as we do that with CSS
			//	drag: function (event, ui) {
			//		debugger
			//		ui.helper.data('dropped', false);
			//		$(this).addClass("active");
			//		$(this).closest("#product").addClass("active");

			//	},

			//	// removing the CSS classes once dragging is over.
			//	stop: function (event, ui) {
			//		//alert('stop: dropped=' + ui.helper.data('dropped'));
			//		$(this).removeClass("active").closest("#product").removeClass("active");

			//	}
			//});
			debugger
			AddSecondary(Userid);
		});
		debugger
		// jQuery UI Draggable

		$('#product  li').draggable({
			revert: true,
			start: function (event, ui) {
				//get all selected...
				if (ui.helper.hasClass('selected')) selectedObjs = $('li.selected');
				else {
					selectedObjs = $(ui.helper);
					$('li.selected').removeClass('selected')
				}
			},
			drag: function (event, ui) {
				selectedObjs.each(function () {
					ui.helper.data('dropped', false);
					$(this).addClass("selected");
					$(this).closest("#product").addClass("selected");
				})

			},
			stop: function (event, ui) {
				//alert('stop: dropped=' + ui.helper.data('dropped'));
				selectedObjs.each(function () {
					$(this).removeClass("selected").closest("#product").removeClass("selected");
				})

			}
		}

		).click(function (e) {
			debugger
			if ($(this).find("input[id='hdnuse']").val() == "true") {
				RedDotAlert_Error('Alredy use');
				return
			}
			$(this).toggleClass('selected')
		});
		//$("#product  li").draggable({

		//	// brings the item back to its place when dragging is over
		//	revert: true,

		//	// once the dragging starts, we decrease the opactiy of other items
		//	// Appending a class as we do that with CSS
		//	drag: function (event, ui) {
		//		debugger
		//		ui.helper.data('dropped', false);
		//		$(this).addClass("active");
		//		$(this).closest("#product").addClass("active");

		//	},

		//	// removing the CSS classes once dragging is over.
		//	stop: function (event, ui) {
		//		//alert('stop: dropped=' + ui.helper.data('dropped'));
		//		$(this).removeClass("active").closest("#product").removeClass("active");

		//	}
		//});



		// jQuery Ui Droppable
		$(".basket").droppable({

			// The class that will be appended to the to-be-dropped-element (basket)
			activeClass: "selected",

			// The class that will be appended once we are hovering the to-be-dropped-element (basket)
			hoverClass: "hover",

			// The acceptance of the item once it touches the to-be-dropped-element basket
			// For different values http://api.jqueryui.com/droppable/#option-tolerance
			tolerance: "touch",
			drop: function (event, ui) {
				debugger
				selectedObjs.each(function (e, k) {
					ui.helper.data('dropped', true);
					var k1 = 0;
					var basket = $(this),
						move = ui.draggable,
						itemId = basket.find("ul li[data-id='" + move.attr("data-id") + "']");

					// To increase the value by +1 if the same item is already in the basket
					if (itemId.html() != null) {
						//itemId.find("input").val(parseInt(itemId.find("input").val()) + 1);
					}
					else {

						if (k.children[1].attributes["value"].value == "false") {
							debugger
							var arr1 = [];
							arr1.push(k.attributes["data-id"].value);
							$(".basket_list  ul li").each(function () {
								arr1.push($(this).attr("data-id"));
							});
							var recipientsArray = arr1.sort();
							for (var i = 0; i < recipientsArray.length - 1; i++) {
								if (recipientsArray[i + 1] == recipientsArray[i]) {
									return
								}
							}

							$("#product").find(selectedObjs[e]).removeClass('selected');
							$("#product").find(selectedObjs[e]).find("input[id='hdnuse']").val('true');
							//$("#product").find(selectedObjs[e]).css("background-color", "orange");
							$("#product").find(selectedObjs[e]).addClass("disabled");
							//move.css("background-color", "orange");

							addBasket(basket, k);




						} else {

							RedDotAlert_Error('Alredy use');
						}


						// Updating the quantity by +1" rather than adding it to the basket
						//move.find("input").val(parseInt(move.find("input").val()) + 1);
					}
				});

			}
		});
		//// jQuery Ui Droppable
		//$(".basket").droppable({

		//	// The class that will be appended to the to-be-dropped-element (basket)
		//	activeClass: "active",

		//	// The class that will be appended once we are hovering the to-be-dropped-element (basket)
		//	hoverClass: "hover",

		//	// The acceptance of the item once it touches the to-be-dropped-element basket
		//	// For different values http://api.jqueryui.com/droppable/#option-tolerance
		//	tolerance: "touch",
		//	drop: function (event, ui) {
		//		debugger
		//		ui.helper.data('dropped', true);
		//		var k1 = 0;
		//		var basket = $(this),
		//			move = ui.draggable,
		//			itemId = basket.find("ul li[data-id='" + move.attr("data-id") + "']");

		//		// To increase the value by +1 if the same item is already in the basket
		//		if (itemId.html() != null) {
		//			//itemId.find("input").val(parseInt(itemId.find("input").val()) + 1);
		//		}
		//		else {
					
		//			if (move.find("input[id='hdnuse']").val() == "false") {
		//				debugger
		//				var arr1 = [];
		//				arr1.push(move.attr("data-id"));
		//				$(".basket_list  ul li").each(function () {
		//					arr1.push($(this).attr("data-id"));
		//				});
		//				var recipientsArray = arr1.sort();
		//				for (var i = 0; i < recipientsArray.length - 1; i++) {
		//					if (recipientsArray[i + 1] == recipientsArray[i]) {
		//						return
		//					}
		//				}

						
		//				move.css("background-color", "orange");
						
		//					addBasket(basket, move);
							



		//			} else {

		//				RdotAlerterrtxt('Alredy use');
		//			}


		//			// Updating the quantity by +1" rather than adding it to the basket
		//			//move.find("input").val(parseInt(move.find("input").val()) + 1);
		//		}

		//	}
		//});

		function AddSecondary(CustCode1) {
			debugger
			$.ajax({
				type: "POST",
				url: "/GetCustMapWeb",
				contentType: "application/json",
				dataType: "json",
				data: JSON.stringify({
					CustCode: CustCode1
				}),
				success: function (response) {
					debugger
					console.log(response);
					var i = 0;
					while (i < response.length) {
						$("#pri1").find("ul").append('<li data-id="' + response[i].repTypeId + '">'
							+ '<span class="name"  >' + response[i].reportTitle + '</span>'
							+ '<input  type="hidden" id="hdntyp" value="O"/>'
							+ '<button class="delete">&#10005;</button>');
						i++;
					}

				},
				error: function (response) {
					console.log(response);
				}
			});
		}
		// This function runs onc ean item is added to the basket
		function addBasket(basket, move) {
			debugger
			$(".basket_list").find("ul").append('<li data-id="' + move.attributes["data-id"].value + '">'
				+ '<span class="name" >' + move.innerText + '</span>'
				+ '<input  type="hidden" id="hdntyp" value="N"/>'
				+ '<button class="delete">&#10005;</button>');
		}


		// The function that is triggered once delete button is pressed
		$(".basket_list").on("click", "ul li .delete", function () {
			debugger
			var tr = $(this).closest("li");
			var Code = tr.attr("data-id");
			var Role = tr.find(".name").text();
			var Username = $('#Userid Option:selected').val();
			var typ1 = tr.find("input[id='hdntyp']").val();
			var k = $(this).closest("ul").attr("id");
			if (k == "pr1" && $("#hdnflag").val() == "true") {
				typ1 = "O";
			}
			if (typ1 == "O") {
				const swalWithBootstrapButtons = Swal.mixin({
					confirmButtonClass: 'btn btn-success',
					cancelButtonClass: 'btn btn-danger',
					buttonsStyling: false,
				})
				swalWithBootstrapButtons.fire({
					title: 'Are you sure?',
					text: "You want to remove access of this report ?",
					type: 'warning',
					showCancelButton: true,
					confirmButtonText: 'Yes, Change it!',
					cancelButtonText: 'No, cancel!',
					reverseButtons: true
				}).then((result) => {
					if (result.value) {
						$.ajax({
							type: "POST",
							url: "/DeleteActivityWebReport",
							contentType: "application/json",
							dataType: "json",
							data: JSON.stringify({
								Username: Username,
								Code: Code								
							}),
							success: function (response) {
								debugger
								if (response.data == true) {
									//$(".reloadcss").trigger("click");
									//$('#txtsearch').trigger("keyup");
									//$('#tblReports tbody td li:contains(' + Role + ')').css("background-color", "");
									$('#tblReports tbody td li:contains(' + Role + ')').removeClass("disabled");
									$('#tblReports tbody td li:contains(' + Role + ')').find("input[id='hdnuse']").val('false');
									tr.remove();
								} else {
									RedDotAlert_Error(Code);
								}
								return

							},
							error: function (response) {
								console.log(response);
								return
							}
						});
						swalWithBootstrapButtons.fire(
							'Status',
							'Access has been removed.',
							'success'
						)
					} else if (
						result.dismiss === Swal.DismissReason.cancel
					) {
						//$(".reloadcss").trigger("click");
						//$('#txtsearch').trigger("keyup");
						//$('#tblReports td li:contains(' + Code + ')').css("background-color", "");

						//$('#tblReports td li:contains(' + Code + ')').find("input[id='hdnuse']").val('false');
						swalWithBootstrapButtons.fire(
							'Cancelled',
							'Your Code is safe :)',
							'error'
						)
					}
				})

				

				

			} else {
				$("#product  li").each(function () {
					var kl = $(this).attr("data-id");
					if (kl == Code) {
						//$(this).css("background-color", "");
						$(this).removeClass("disabled");
						$(this).find("input[id='hdnuse']").val('false');
					}
				})
				$(this).closest("li").remove();
			}


		});

		$("#btnclear").on("click", function () {
			$(".basket_list  ul li").each(function () {
				$(this).closest("li").remove();
			});
			tf = false;
			$('#Userid').val("0").trigger("change");
			
		})

		//$(".basket_list").on("hover","ul li .name", function () {
		//debugger
		//})


		//$(".basket_list").hover(function () {
		//	debugger
		//$(this).css('cursor', 'pointer').attr('title', 'This is a hover text.');
		//});

		$("#btnsave").on("click", function () {
			debugger
			
			
			var WURep = {
				userName: $('#Userid Option:selected').val(),
				
				WebRepLists: []
			}
			$("#pri1  ul li").each(function () {
				debugger
				var k = $(this).text();
				var l = $(this).attr("data-id");				
				t1 = $(this).find("input[id='hdntyp']").val();
				if (t1 == "N") {
					var WebRepList = {
						fk_repTypeId:l
					}
					WURep.WebRepLists.push(WebRepList);
				}

			});
			if (WURep.Username == '') {
				RedDotAlert_Error('Please Select User Name');
				return
			}
			if (WURep.WebRepLists.length == 0) {
				RedDotAlert_Error('Please Add Report');
				return
			}
			$.post("/SaveWebRep", WURep).done(function (response) {
				debugger
				if (response.saveflag == true) {
					RedDotAlert_Success(response.errormsg);
					$("#btnclear").trigger("click");
					tf = false;
					$('#Userid').val("0").trigger("change");
					
					//$(".reloadcss").trigger("click");
					$('#txtsearch').trigger("keyup");
					
				} else {
					RdotAlerterr(response.errormsg);
				}
			})



		})

		$(".loader1").hide();
	}
}