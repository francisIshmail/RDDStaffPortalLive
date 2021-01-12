var LeaveManage = {
    initialize: function (e) {
        debugger
       

        LeaveManage.Attachevent();
    },
    Attachevent: function () {
       
        $(document).on('apply.daterangepicker','#reportrange', function (ev, picker) {
            debugger;
            var currentleave;
            $("#random").hide();
            $("#Leavedtls").empty();
            var d = new Date();
            var StrtDate = d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate();
            var EndDate = d.getFullYear() + "/" + 12 + "/" + 31;
            var fromdate = $('#reportrange').data('daterangepicker').startDate.format('YYYY-MM-DD');
            var todate = $('#reportrange').data('daterangepicker').endDate.format('YYYY-MM-DD');

            var startMin1 = new Date(moment(fromdate, 'YYYY-MM-DD').add(-1, 'days'));

            var days = daysdifference(fromdate, todate);
            var arr2 = getDateArray(fromdate, todate);
            var startMin = moment(fromdate, 'YYYY-MM-DD');
            startMin = new Date(startMin);
            var startdayMin = ("0" + startMin.getDate()).slice(-2);
            var startmonthMin = ("0" + (startMin.getMonth() + 1)).slice(-2);
            var nowMax = moment(todate, 'YYYY-MM-DD');
            nowMax = new Date(nowMax);
            var dayMax = ("0" + nowMax.getDate()).slice(-2);
            var monthMax = ("0" + (nowMax.getMonth() + 1)).slice(-2);
            $('#reportrange span').html(startdayMin + "/" + startmonthMin + "/" + startMin.getFullYear() + ' - ' + dayMax + "/" + monthMax + "/" + nowMax.getFullYear());
            $(this).val(picker.startDate.format('MM/DD/YYYY') + '-' + picker.endDate.format('MM/DD/YYYY'));
            $("#divDates").empty();
            var weeklyoff = $("#Weeklyoff").val();
            var LeaveRule = $("#LeaveRule").val();
            var weekday = new Array(7);
            weekday[0] = "Sunday";
            weekday[1] = "Monday";
            weekday[2] = "Tuesday";
            weekday[3] = "Wednesday";
            weekday[4] = "Thursday";
            weekday[5] = "Friday";
            weekday[6] = "Saturday";
            if (LeaveRule != "WorkingDays") {
                if (weekday[startMin1.getDay()] == weeklyoff) {
                    $('#reportrange').data('daterangepicker').startDate.add(-1, 'day')
                    html = '<tr class="dttype"><td style="width:50%; float:left">';
                    html += '<div id="divLvDates">' + getMonthDetails((startMin1.getMonth())) + '' + startMin1.getDate() + '</div>';
                    html += '</td><td style="width:50%; float:right">';
                    html += '<select name="daytime" class="custom-select" id="ddlDayType"> <option value="1">All Day </option> <option value="0.5">half day - morning</option> <option value="0.5">half day - afternoon </option> </select >';
                    html += '</td></tr>';
                    $("#divDates").append(html);
                }
            }

            for (var i = 0; i < arr2.length; i++) {

                currentleave = (arr2[i].getMonth()) * 1.75;
                var date1 = arr2[i].getDate();
                var curyear = d.getFullYear();
                var months = getMonthDetails((arr2[i].getMonth()));
                var html = '<tr class="dttype"><td style="width:50%; float:left">';
                html += '<div id="divLvDates">' + months + '' + date1 + '</div>';
                html += '</td><td style="width:50%; float:right">';
                html += '<select name="daytime" class="custom-select" id="ddlDayType"> <option value="1">All Day </option> <option value="0.5">half day - morning</option> <option value="0.5">half day - afternoon </option> </select >';
                html += '</td></tr>';

                if (LeaveRule == "WorkingDays") {

                    if (weekday[arr2[i].getDay()] != weeklyoff) {


                        $("#divDates").append(html);
                    }
                }
                else {


                    $("#divDates").append(html);

                }

            }
            var remainleave = (currentleave - days);
            var Header = '<h5>' + days + ' days of annual leave</h5>';
            Header += '<h5>' + remainleave + ' days remaining</h5>';
            $("#Leavedtls").append(Header);

        });
        var totalday = 0;

        $(document).on('change', '[id^=EmployeeId]', function () {

            var EmployeeId = $("#EmployeeId option:selected").val();
            $("#Leavedtls").empty();
            $("#divDates").empty();
            $("#random").show();
            $.ajax({
                type: "POST",
                url: "/GetWeeklyOffDay",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ EmployeeId: $(this).val() }),


                success: function (data) {

                    arr = data;
                    $("#Weeklyoff").val(arr.data[0].WeeklyOff);
                    $("#LeaveRule").val(arr.data[0].LeaveRules);
                }

            });
        });
        var getDateArray = function (fromdate, todate) {


            var arr = new Array();
            var weekday = new Array(7);
            weekday[0] = "Sunday";
            weekday[1] = "Monday";
            weekday[2] = "Tuesday";
            weekday[3] = "Wednesday";
            weekday[4] = "Thursday";
            weekday[5] = "Friday";
            weekday[6] = "Saturday";
            var arr3 = new Array();
            var dt = new Date(fromdate);
            var fnldt = new Date(todate);
            while (dt <= fnldt) {

                arr.push(new Date(dt));
                arr3.push(weekday[dt.getDay()]);
                dt.setDate(dt.getDate() + 1);
            }

            return arr;

        }

        function daysdifference(FromDate, ToDate) {

            var startDay = new Date(FromDate);
            var endDay = new Date(ToDate);
            var millisBetween = endDay.getTime() - startDay.getTime();
            var days = millisBetween / (1000 * 3600 * 24);
            var finalday = days + 1;
            return finalday;
        }
        function getMonthDetails(month) {
            var monthnm;
            if (month == 0) {
                monthnm = "Jan";
            }
            else if (month == 1) {
                monthnm = "Feb";
            }
            else if (month == 2) {
                monthnm = "Mar";
            }
            else if (month == 3) {
                monthnm = "Apr";
            }
            else if (month == 4) {
                monthnm = "May";
            }
            else if (month == 5) {
                monthnm = "Jun";
            }
            else if (month == 6) {
                monthnm = "July";
            }
            else if (month == 7) {
                monthnm = "Aug";
            }
            else if (month == 8) {
                monthnm = "Sep";
            }
            else if (month == 9) {
                monthnm = "Oct";
            }
            else if (month == 10) {
                monthnm = "Nov";
            }
            else if (month == 11) {
                monthnm = "Dec";
            }
            else {
                monthnm = "undefined";
            }
            return monthnm;

        }
        
        $(document).on('click', '#Btn_Submit', function () {
            $(".loader1").show();
            if ($('#Reason').val().length == 0) {
                RedDotAlert_Error("Please Enter a valid reason");
                $(".loader1").hide();
                return false;
            }
            $(".dttype").each(function () {
                debugger;
                var currow = $(this).closest('tr');
                var LeaveDate = $(this).find("[id^='divLvDates']").html();
                var LeaveDay = $(this).find("[id^='ddlDayType']").val();
                var LeaveDayType = $(this).find("[id^='ddlDayType'] option:selected").text();
                totalday += parseFloat(LeaveDay);
                var LeaveRequestDetails = {
                    LeaveDate: LeaveDate,
                    LeaveDay: LeaveDay,
                    LeaveDayType: LeaveDayType
                };
            });
            var fromdate = $('#reportrange').data('daterangepicker').startDate.format('YYYY-MM-DD');
            var todate = $('#reportrange').data('daterangepicker').endDate.format('YYYY-MM-DD');
            var days = daysdifference(fromdate, todate);
            var day = totalday;
            var RDD_LeaveRequest = {
                EmployeeId: $("#EmployeeId option:selected").val(),
                LeaveTypeId: $("#LeaveTypeId option:selected").val(),
                Reason: $("#Reason").val(),
                FromDate: fromdate,
                ToDate: todate,
                NoOfDays: day,
                LeaveStatus: "Pending",
                ApproverRemarks: "NULL",
                IsPrivateLeave: $('input[name="private"]:checked').val(),
                AttachmentUrl: $("#AttachmentUrl[type='hidden']").val(),
                LeaveRequestDetailsList: []
            };

            $(".dttype").each(function () {
                var currow = $(this).closest('tr');
                var LeaveDate = $(this).find("[id^='divLvDates']").html();
                var LeaveDay = $(this).find("[id^='ddlDayType']").val();
                var LeaveDayType = $(this).find("[id^='ddlDayType'] option:selected").text();
                var LeaveRequestDetails = {
                    LeaveDate: LeaveDate,
                    LeaveDay: LeaveDay,
                    LeaveDayType: LeaveDayType
                };
                RDD_LeaveRequest.LeaveRequestDetailsList.push(LeaveRequestDetails);
            });

            $.post("/SaveLeaveRequest", { RDD_LeaveRequest: RDD_LeaveRequest }, function (response) {
                debugger
                $(".loader1").hide();
                if (response.data.Saveflag == true && response.data.LeaveRequestId != -1) {
                    $("#exampleModal").modal("hide");
                    RedDotAlert_Success(response.data.ErrorMsg);
                    window.location.href = '@Url.Action("Index","RDD_LeaveRequest")';

                }
                else {
                    RedDotAlert_Error(response.data.ErrorMsg);
                }

            });

        });
    }
}