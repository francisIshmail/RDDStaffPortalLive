var CustomerMapping = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		CustomerMapping.Attachevent();
	},
	Attachevent: function () {

		
		var colms = [
			//{ "mDataProp": "CardCode", "sWidth": "30%" },
			{
				"mDataProp": "CardName", render: function (data, type, full) {

					return "<li data-id='" + full.CardCode + "' class='ui-draggable ui-draggable-handle' style='background-color:" + full.bgcolor + "'><a href = '#' style=''> " + full.CardName + " </a><input type='hidden' id='hdndbname' value='" + full.DBName + "'><input type='hidden' id='hdnuse' value='" + full.IsAlreadyMapped + "'/><input type='hidden' id='hdnCustyp' value='" + full.CustTyp +"'/></li>";

				}, "sWidth": "30%" }

			


		];
		//SAPAE   SAPKE	 SAPTZ	 SAPUG	SAPZM	SAPML	SAPTRI
		RdottableNDWPara1("tblSAPAE", "/GetCustMapData", colms, 0);
		RdottableNDWPara1("tblSAPKE", "/GetCustMapData", colms, 1);
		RdottableNDWPara1("tblSAPTZ", "/GetCustMapData", colms, 2);
		RdottableNDWPara1("tblSAPUG", "/GetCustMapData", colms, 3);
		RdottableNDWPara1("tblSAPZM", "/GetCustMapData", colms, 4);
		RdottableNDWPara1("tblSAPML", "/GetCustMapData", colms, 5);
		RdottableNDWPara1("tblSAPTRI", "/GetCustMapData", colms, 6);
		
		debugger
		// jQuery UI Draggable
		$("#product  li").draggable({
			
			// brings the item back to its place when dragging is over
			revert: true,

			// once the dragging starts, we decrease the opactiy of other items
			// Appending a class as we do that with CSS
			drag: function () {
				$(this).addClass("active");
				$(this).closest("#product").addClass("active");
			},

			// removing the CSS classes once dragging is over.
			stop: function () {
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

					if ($("#pri1").find("ul li ").length == 1 && basket.find(".basket_list").attr("id") == 'pri1') {
						RdotAlerterrtxt('You can add only one primary account');
						return
					}
					if (move.find("input[id='hdnCustyp']").val() == 'P') {
						if (basket.find(".basket_list").attr("id") == 'sec1') {
							RdotAlerterrtxt('You can add only primary account');
							return
                        }

						
						if (k1 == 0) {
							addBasket(basket, move);
							k1 = 1;
                        }
						
						move.css("background-color", "orange");
						move.css("left", "0px");
						move.css("top", "0px");						
						$("#hdnflag").val(true);
						
						var ParentCode = move.attr("data-id");
						var ParentDb =move.find("input[id='hdndbname']").val();
						//$.getJSON("/GetCustMapParentData", { ParentCode=ParentCode, ParentDb=ParentDb }).done(function (data) {
						//	debugger
						//	var k = data;
						//});

						AddSecondary(ParentCode, ParentDb);

						
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

						if (basket.find(".basket_list").attr("id") == 'pri1') {
							//$("#hdntitle").text(move.find("input[id='hdndbname']").val());
							move.css("background-color", "hotpink");
						} else {//
							move.css("background-color", "orange");
						}
						
						//var k1 = 0;
						if (k1 == 0) {
							addBasket(basket, move);
							k1 = 1;
						}
						move.css("left", "0px");
						move.css("top", "0px");
						//$("#hdnflag").val(false);
						
						
					} else {
						move.css("left", "0px");
						move.css("top", "0px");
						RdotAlerterrtxt('Alredy use');
                    }
					

					// Updating the quantity by +1" rather than adding it to the basket
					//move.find("input").val(parseInt(move.find("input").val()) + 1);
				}
				
			}
		});

		function AddSecondary(ParentCode1, ParentDb1) {
			$.ajax({
				type: "POST",
				url: "/GetCustMapParentData",
				contentType: "application/json",
				dataType: "json",
				data: JSON.stringify({
					ParentCode:ParentCode1,
					ParentDb:ParentDb1
				}),
				success: function (response) {
					debugger
					console.log(response);
					var i = 0;
					while (i < response.length) {
						$("#sec1").find("ul").append('<li data-id="' + response[i].CardCode + '">'
							+ '<span class="name"  title="' + response[i].DBName +'">' + response[i].CardName + '</span>'
							+ '<input  value="' + response[i].DBName + '" type="hidden" id="hdndbtyp"><input  type="hidden" id="hdntyp" value="O"/>'
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
				+ '<span class="name"  title="' + move.find("input[id='hdndbname']").val() +'">' + move.find("a").html() + '</span>'
				+ '<input  value="' + move.find("input[id='hdndbname']").val() +'" type="hidden" id="hdndbtyp" ><input  type="hidden" id="hdntyp" value="N"/>'
				+ '<button class="delete">&#10005;</button>');
		}

		
			// The function that is triggered once delete button is pressed
			$(".basket_list").on("click","ul li .delete", function () {
				debugger	
				var tr = $(this).closest("li");
				var vt = tr.attr("data-id");
				var dbname = tr.find("input[id='hdndbtyp']").val();
				var typ1 = tr.find("input[id='hdntyp']").val();
				var k = $(this).closest("ul").attr("id");
				if (k == "pr1" && $("#hdnflag").val() == "true") {
					typ1 = "O";
                }
				if ( typ1=="O" ) {				
					const swalWithBootstrapButtons = Swal.mixin({
						confirmButtonClass: 'btn btn-success',
						cancelButtonClass: 'btn btn-danger',
						buttonsStyling: false,
					})
					swalWithBootstrapButtons.fire({
						title: 'Are you sure?',
						text: "You won't be able to revert this!",
						type: 'warning',
						showCancelButton: true,
						confirmButtonText: 'Yes, Change it!',
						cancelButtonText: 'No, cancel!',
						reverseButtons: true
					}).then((result) => {
						if (result.value) {
							$.ajax({
								type: "POST",
								url: "/DeleteCustMap",
								contentType: "application/json",
								dataType: "json",
								data: JSON.stringify({
									code: vt,
									dbname: dbname,
									typ: k
								}),
								success: function (response) {
									debugger
									if (response.data == true) {
										//$(".reloadcss").trigger("click");
										$('#txtsearch').trigger("keyup");
										tr.remove();
									} else {
										RdotAlerterrtxt(vt);
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
								'Your Code has been Chaged.',
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

					if (k == "pr1") {
						$("#btnclear").trigger("click");
                    }
					//if (k == "se1") {

					//} else {

					//}

				} else {
					$("#product  li").each(function () {
						var kl = $(this).attr("data-id");
						if (kl == vt) {
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
		$("#hdnflag").val(false);
		})

		//$(".basket_list").on("hover","ul li .name", function () {
		//debugger
		//})
		

		//$(".basket_list").hover(function () {
		//	debugger
		//$(this).css('cursor', 'pointer').attr('title', 'This is a hover text.');
		//});

		$("#btnmerge").on("click", function () {
			debugger
			var Parent_DBNametxt = '';
			var Parent_CardCodetxt = '';
			var CustomerNametxt = '';
			var t1 = 'N';
			$("#pri1  ul li").each(function () {
				debugger
				 t1 = $(this).find("input[id='hdntyp']").val();
				if (t1 == "N") {
					CustomerNametxt = $(this).text();
					Parent_CardCodetxt = $(this).attr("data-id");
					Parent_DBNametxt = $(this).find("input[id='hdndbtyp']").val();
				}
			});
			var Cust = {
				Parent_DBName: Parent_DBNametxt,
				Parent_CardCode: Parent_CardCodetxt,
				CustomerName: CustomerNametxt,
				EditFlag:$("#hdnflag").val(),
				ChildLists:[]
            }			
			$("#sec1  ul li").each(function () {
				debugger
				var k = $(this).text();
				var l = $(this).attr("data-id");
				var t = $(this).find("input[id='hdndbtyp']").val();
				 t1 = $(this).find("input[id='hdntyp']").val();
				if (t1 == "N") {
					var ChildList = {
						Child_CardCode: l,
						Child_CardName: k,
						Child_DBName: t,
					}

					Cust.ChildLists.push(ChildList);
                }
				
			});
			if (Cust.CustomerName == '') {
				RdotAlerterrtxt('Please Add Primary Account');
				return
            }
			if (Cust.ChildLists.length == 0) {
				RdotAlerterrtxt('Please Add Secondary Account');
				return
            }
			$.post("/SaveCustMap", Cust).done(function (response) {
				debugger
				if (response.saveflag == true) {
					RdotAlertSucesstxt(response.errormsg);
					$("#btnclear").trigger("click");
					//$(".reloadcss").trigger("click");
					$('#txtsearch').trigger("keyup");
				} else {
					RdotAlerterr(response.errormsg);
                }
            })



		})
		

	}
}