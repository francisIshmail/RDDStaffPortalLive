var UserRole = {
	initialize: function () {

		$.fn.dataTable.ext.errorMode = 'none';
		UserRole.Attachevent();
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





        var tf = true;
        $("#Userid").on("change", function () {

            var Userid = $('#Userid Option:selected').val();
            if (Userid == '0' && tf == true) {
                RdotAlerterrtxt("Please select  User");
                return false

            }
            tf = true;
            // RdottableNDWPara1("tblReports", "/GetWebReportMapData", colms, Userid);
            $(".basket_list  ul").empty();
            $("#product  li").draggable({

                // brings the item back to its place when dragging is over
                revert: true,

                // once the dragging starts, we decrease the opactiy of other items
                // Appending a class as we do that with CSS
                drag: function (event, ui) {

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

        // jQuery UI Draggable
        $("#product  li").draggable({

            // brings the item back to its place when dragging is over
            revert: true,

            // once the dragging starts, we decrease the opactiy of other items
            // Appending a class as we do that with CSS
            drag: function (event, ui) {

                ui.helper.data('dropped', false);
                $(this).addClass("active");
                $(this).closest("#product").addClass("active");

            },

            // removing the CSS classes once dragging is over.
            stop: function (event, ui) {
                //  alert('stop: dropped=' + ui.helper.data('dropped'));
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
        });

        function AddSecondary(CustCode1) {
            debugger
            $.ajax({
                type: "POST",
                url: "/UserRoles/GetRolesForUser",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    Username: CustCode1
                }),
                success: function (response) {
                    debugger
                    console.log(response);
                    var i = 0;
                    while (i < response.length) {
                        $("#pri1").find("ul").append('<li data-id="' + response[i] + '">'
                            + '<span class="name"  >' + response[i] + '</span>'
                            + '<input  type="hidden" id="hdntyp" value="O"/>'
                            + '<button class="delete">&#10005;</button>');


                        $('#product ul li:contains(' + response[i] + ')').css("background-color", "blue");

                        $('#product ul li:contains(' + response[i] + ')').find("input[id='hdnuse']").val('true');

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
                + '<span class="name">' + move.find("a").html() + '</span>'
                + '<input  type="hidden" id="hdntyp" value="N"/>'
                + '<button class="delete">&#10005;</button>');
        }

        // The function that is triggered once delete button is pressed
        $(".basket_list").on("click", "ul li .delete", function () {
            debugger
            var tr = $(this).closest("li");
            var Role = tr.attr("data-id");
            var Username = $('#Userid Option:selected').val();
            var typ1 = tr.find("input[id='hdntyp']").val();
            // var k = $(this).closest("ul").attr("id");
            //if (k == "pr1" && $("#hdnflag").val() == "true") {
            //    typ1 = "O";
            //}
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
                            url: "/UserRoles/RemoveUserFromRole",
                            contentType: "application/json",
                            dataType: "json",
                            data: JSON.stringify({
                                Username: Username,
                                Role: Role
                            }),
                            success: function (response) {
                                debugger
                                if (response.Success == true) {
                                    //$(".reloadcss").trigger("click");
                                    // $('#txtsearch').trigger("keyup");
                                    $('#product ul li:contains(' + Role + ')').css("background-color", "");

                                    $('#product ul li:contains(' + Role + ')').find("input[id='hdnuse']").val('false');
                                    tr.remove();
                                } else {
                                    RdotAlerterrtxt(Role);
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
                        // $('#txtsearch').trigger("keyup");
                        $('#product ul li:contains(' + Role + ')').css("background-color", "");

                        $('#product ul li:contains(' + Role + ')').find("input[id='hdnuse']").val('false');
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
                    if (kl == Role) {
                        $(this).css("background-color", "");
                    }
                })
                $(this).closest("li").remove();
            }


        });


        //Clear
        $("#btnclear").on("click", function () {
            tf = false;
            $(".basket_list  ul li").each(function () {
             var tr=   $(this).closest("li");
                tr.remove();
                $('#product ul li:contains(' + tr.attr("data-id") + ')').css("background-color", "");

                $('#product ul li:contains(' + tr.attr("data-id") + ')').find("input[id='hdnuse']").val('false');

            });



            $('#Userid').val("0").trigger("change");

        })
        //Save 
        $("#btnsave").on("click", function () {
            debugger
            var UserName = $('#Userid Option:selected').val();
            var WebRepLists = [];
            $("#pri1  ul li").each(function () {
                debugger
                var k = $(this).text();
                var l = $(this).attr("data-id");
                t1 = $(this).find("input[id='hdntyp']").val();
                if (t1 == "N") {
                    WebRepLists.push(l);
                }

            });
            if (UserName == '') {
                RdotAlerterrtxt('Please Select User Name');
                return
            }
            if (WebRepLists.length == 0) {
                RdotAlerterrtxt('Please Add Report');
                return
            }
            $.post("/UserRoles/AddUserToRole", { Username: UserName, Role: WebRepLists }).done(function (response) {
                debugger
                if (response.Success == true) {
                    RdotAlertSucesstxt(response.Message);
                    $("#btnclear").trigger("click");
                    tf = false;


                    //$(".reloadcss").trigger("click");
                    // $('#txtsearch').trigger("keyup");
                    var i = 0
                    while (WebRepLists.length > i) {
                        $('#product ul li:contains(' + WebRepLists[i] + ')').css("background-color", "");

                        $('#product ul li:contains(' + WebRepLists[i]+ ')').find("input[id='hdnuse']").val('false');
                        i++;
                    }
                   
                    $('#Userid').val("0").trigger("change");

                } else {
                    RdotAlerterr(response.Message);
                }
            })



        })

	}
}