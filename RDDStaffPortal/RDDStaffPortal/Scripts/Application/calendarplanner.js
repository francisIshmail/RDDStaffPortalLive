var CalendarplannerIndex = {
    initialize: function (e) {
        debugger
        //$.fn.dataTable.ext.errorMode = 'none';
       
        CalendarplannerIndex.CalendarLoad();
        CalendarplannerIndex.Attachevent();
    },
    CalendarLoad: function () {
       
    },
    Attachevent: function () {
        var tblhead1 = ["FullName"];
        var tblhide = [];
        var tblhead2 = ['EmployeeId'];
        var dateCond = [];
        $(".loader1").hide();
        var seletedDate = moment(new Date());
        var firstDay = new Date(seletedDate.year(), seletedDate.month(), 1);
        var lastDay = new Date(seletedDate.year(), seletedDate.month() + 1, 0);
        var getDaysArray = function (year, month) {
            var monthIndex = month - 1; // 0..11 instead of 1..12
            var names = ['sun', 'mon', 'tue', 'wed', 'thu', 'fri', 'sat'];
            var date = new Date(year, monthIndex, 1);
            var result = [];
            while (date.getMonth() == monthIndex) {
               
                result.push(date.getDate() + '-' + names[date.getDay()]);
                date.setDate(date.getDate() + 1);
            }
            return result;
        }
        debugger
        var dayt = getDaysArray(seletedDate.year(), seletedDate.month() + 1);
        
       
        RedDot_DivTable_Header_Fill_Append("I", dayt);
        var curPage = 1;
        var arr = "";
        Getdata();
        //#region Load Data
        function Getdata() {
            var value1 = '';
            $('.loader1').show();
            var data = JSON.stringify({
                pagesize: 20,
                pageno: 1,
                psearch: value1,
                fromdate: firstDay,
                todate:lastDay
            });

            arr = RedDot_DivTable_Fill_Table("I", "/GetAnnualLeaveCalendra", data, dateCond, tblhead1, tblhide, tblhead2);
            debugger
        }

        //#region Next Button*/
        $('.next2').bind('click', function (e) {
            e.preventDefault();
            $(".loader1").show();
            if (arr.Table.length > 0) {
                if (arr.Table[0].TotalCount < 20) {
                    $(".loader1").hide();
                    return;
                }
            } else {
                $(".loader1").hide();
                RedDotAlert_Error('No Record Found');
                return
            }
            curPage++;
            var value1 = '';//$("#Search-Forms").val().toLowerCase();

            if (curPage > arr.Table[0].TotalCount)
                curPage = 0;


            firstDay = new Date(seletedDate.year(), seletedDate.month(), 1);
            lastDay = new Date(seletedDate.year(), seletedDate.month() + 1, 0);

            var data = JSON.stringify({
                pagesize: 20,
                pageno: curPage,
                psearch: value1,
                fromdate: firstDay,
                todate: lastDay
            });
            arr = RedDot_DivTable_Fill_Table("I", "/GetAnnualLeaveCalendra", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion
        //#region Prev Button*/
        $('.prev').bind('click', function (e) {
            e.preventDefault();
            $(".loader1").show();
            var value1 = '';//$("#Search-Forms").val().toLowerCase();
            if (arr.Table.length > 0) {
                if (arr.Table[0].TotalCount < 20) {
                    $(".loader1").hide();
                    return;
                }
                curPage--;
                if (curPage < 0)
                    curPage = (arr.Table[0].TotalCount - 1);
            } else {
                curPage--;
            }

            firstDay = new Date(seletedDate.year(), seletedDate.month(), 1);
            lastDay = new Date(seletedDate.year(), seletedDate.month() + 1, 0);

            var data = JSON.stringify({
                pagesize: 20,
                pageno: curPage,
                psearch: value1,
                fromdate: firstDay,
                todate: lastDay
            });
            arr = RedDot_DivTable_Fill_Table("I", "/GetAnnualLeaveCalendra", data, dateCond, tblhead1, tblhide, tblhead2);
        });
        //#endregion

        $(document).on('click','.next1',function () {
            debugger
            var lastDayOfNextMonth = moment(seletedDate.add(1, "M")).endOf('month');
            seletedDate = lastDayOfNextMonth;
            // Update selected date
            var seletedDate1 = lastDayOfNextMonth.startOf("month").format('MMMM');
            //alert(seletedDate1);
            $("#calendarid").html(seletedDate1 + " " + seletedDate.year());
            var dayt = getDaysArray(seletedDate.year(), seletedDate.month() + 1);


            RedDot_DivTable_Header_Fill_Append("I", dayt);

            firstDay = new Date(seletedDate.year(), seletedDate.month(), 1);
            lastDay = new Date(seletedDate.year(), seletedDate.month() + 1, 0);
            curPage = 0;
            value1 = '';
            var data = JSON.stringify({
                pagesize: 20,
                pageno: curPage,
                psearch: value1,
                fromdate: firstDay,
                todate: lastDay
            });
            arr = RedDot_DivTable_Fill_Table("I", "/GetAnnualLeaveCalendra", data, dateCond, tblhead1, tblhide, tblhead2);

        });

        $(document).on('click','.previous',function () {
            debugger
            var lastDayOfPreviousMonth = moment(seletedDate.add(-1, "M")).endOf('month');
            seletedDate = lastDayOfPreviousMonth;
            var seletedDate1 = lastDayOfPreviousMonth.startOf("month").format('MMMM');
           // alert(seletedDate1);
            $("#calendarid").html(seletedDate1 + " " + seletedDate.year());
            var dayt = getDaysArray(seletedDate.year(), seletedDate.month() + 1);


            RedDot_DivTable_Header_Fill_Append("I", dayt);
            firstDay = new Date(seletedDate.year(), seletedDate.month(), 1);
            lastDay = new Date(seletedDate.year(), seletedDate.month() + 1, 0);
            curPage = 0;
            value1 = '';
            var data = JSON.stringify({
                pagesize: 20,
                pageno: curPage,
                psearch: value1,
                fromdate: firstDay,
                todate: lastDay
            });
            arr = RedDot_DivTable_Fill_Table("I", "/GetAnnualLeaveCalendra", data, dateCond, tblhead1, tblhide, tblhead2);

        })



       
    }
}