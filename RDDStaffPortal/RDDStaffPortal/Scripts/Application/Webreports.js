var Webreports = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		Webreports.Attachevent();
	},
	Attachevent: function () {

		$.getJSON("/GetUserList").done(function (data) {
			$("#Userid").empty();
			$('#Userid').append('<option value="0" selected="">-Select-</option>');
			var ary = [];
			ary = data;
			for (var i = 0; i < ary.length; i++) {
				$('#Userid').append('<option value="' + ary[i].Code + '" selected="">' + ary[i].CodeName + '</option>');
			}
			$('#Userid').val(0);
			// $('#Userid').selectpicker('refresh');
		});
		$('#Userid').select2({
			theme: "bootstrap"
		});

		var colms = [
			//{ "mDataProp": "CardCode", "sWidth": "30%" },
			{
				"mDataProp": "reportTitle", render: function (data, type, full) {

					return "<li data-id='" + full.repTypeId + "' class='ui-draggable ui-draggable-handle' style='background-color:" + full.bgcolor + "'><a href = '#' style=''> " + full.reportTitle + " </a><input type='hidden' id='hdnuse' value='" + full.IsAlreadyMapped + "'/></li>";

				}
			}




		];
		//SAPAE   SAPKE	 SAPTZ	 SAPUG	SAPZM	SAPML	SAPTRI""
		RdottableNDWPara1("tblReports", "/GetWebReportMapData", colms,"");


		var tf = true;
		$("#Userid").on("change", function () {

			var Userid = $('#Userid Option:selected').val();
			if (Userid == '0' && tf == true) {
				RdotAlerterrtxt("Please select  User");
				return false

			}
			tf = true;
			RdottableNDWPara1("tblReports", "/GetWebReportMapData", colms, Userid);
			$(".basket_list  ul").empty();
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
			debugger
			AddSecondary(Userid);
		});
		debugger
		// jQuery UI Draggable
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




		// jQuery Ui Droppable
		$(".basket").droppable({

			// The class that will be appended to the to-be-dropped-element (basket)
			activeClass: "active",

			// The class that will be appended once we are hovering the to-be-dropped-element (basket)
			hoverClass: "hover",

			// The acceptance of the item once it touches the to-be-dropped-element basket
			// For different values http://api.jqueryui.com/droppable/#option-tolerance
			tolerance: "touch",
			drop: function (event, ui) {
				debugger
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
					// Add the dragged item to the basket))

					//if ($("#pri1").find("ul li ").length == 1 && basket.find(".basket_list").attr("id") == 'pri1') {

					//	RdotAlerterrtxt('You can add only one primary account');
					//	return
					//}
					if (move.find("input[id='hdnCustyp']").val() == 'P') {
						//if (basket.find(".basket_list").attr("id") == 'sec1') {

						//	RdotAlerterrtxt('You can add only primary account');
						//	return
						//}


						if (k1 == 0) {
							addBasket(basket, move);
							k1 = 1;
						}

						move.css("background-color", "orange");

						$("#hdnflag").val(true);

						//var ParentCode = move.attr("data-id");
						//var ParentDb = move.find("input[id='hdndbname']").val();


						//AddSecondary(ParentCode, ParentDb);


						return
					}
					if (move.find("input[id='hdnuse']").val() == "false") {
						debugger
						var arr1 = [];
						arr1.push(move.attr("data-id"));
						$(".basket_list  ul li").each(function () {
							arr1.push($(this).attr("data-id"));
						});
						var recipientsArray = arr1.sort();
						for (var i = 0; i < recipientsArray.length - 1; i++) {
							if (recipientsArray[i + 1] == recipientsArray[i]) {
								return
							}
						}

						//if (basket.find(".basket_list").attr("id") == 'pri1') {
						//	//$("#hdntitle").text(move.find("input[id='hdndbname']").val());
						//	move.css("background-color", "hotpink");
						//} else {//
						//	move.css("background-color", "orange");
						//}
						move.css("background-color", "orange");
						//var k1 = 0;
						if (k1 == 0) {
							addBasket(basket, move);
							k1 = 1;
						}



					} else {

						RdotAlerterrtxt('Alredy use');
					}


					// Updating the quantity by +1" rather than adding it to the basket
					//move.find("input").val(parseInt(move.find("input").val()) + 1);
				}

			}
		});

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
			basket.find("ul").append('<li data-id="' + move.attr("data-id") + '">'
				+ '<span class="name"  title="' + move.find("input[id='hdndbname']").val() + '">' + move.find("a").html() + '</span>'
				+ '<input  value="' + move.find("input[id='hdndbname']").val() + '" type="hidden" id="hdndbtyp" ><input  type="hidden" id="hdntyp" value="N"/>'
				+ '<button class="delete">&#10005;</button>');
		}


		// The function that is triggered once delete button is pressed
		$(".basket_list").on("click", "ul li .delete", function () {
			debugger
			var tr = $(this).closest("li");
			var Code = tr.attr("data-id");
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
									$('#txtsearch').trigger("keyup");
									tr.remove();
								} else {
									RdotAlerterrtxt(Code);
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
						$('#txtsearch').trigger("keyup");
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
						$(this).css("background-color", "");
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
				RdotAlerterrtxt('Please Select User Name');
				return
			}
			if (WURep.WebRepLists.length == 0) {
				RdotAlerterrtxt('Please Add Report');
				return
			}
			$.post("/SaveWebRep", WURep).done(function (response) {
				debugger
				if (response.saveflag == true) {
					RdotAlertSucesstxt(response.errormsg);
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


	}
}