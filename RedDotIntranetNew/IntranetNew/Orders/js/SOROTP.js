
$(document).ready(function () {

    var SalesOrder_obj = new SalesOrderOTP();
    SalesOrder_obj.Init();
    $('#BtnGenerateOTP').attr('disabled', 'disabled');
    $('#lblDocEntry').val('');
});


var SalesOrderOTP = function () { };  //Class

SalesOrderOTP.prototype = {

    Init: function () {
        SalesOrderOTP.prototype.BindDDL();
        // SalesOrderOTP.prototype.ClickEvent();

    },


    BindDDL: function () {
        $("[id$=ddlDatabase] option").remove();
        $("[id$=ddlBU] option").remove();
        $("[id$=ddlRequestedBy] option").remove();
       
        try {
            $.ajax({
                url: "SalesOrderOTP.aspx/Get_BindDLList",
                data: "{}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (value) {
                    var jData = eval('(' + value.d + ')');
                    debugger;

                    for (var i = 0; i < jData.table.rows.length; i++) {
                        $("[id$=ddlDatabase]").append($("<option></option>").val(jData.table.rows[i].dbname).html(jData.table.rows[i].dbname));
                    }

                    //                    for (var i = 0; i < jData.table1.rows.length; i++) {
                    //                        $("[id$=ddlCountry]").append($("<option></option>").val(jData.table1.rows[i].countrycode).html(jData.table1.rows[i].country));
                    //                    }

                    for (var i = 0; i < jData.table2.rows.length; i++) {
                        $("[id$=ddlBU]").append($("<option></option>").val(jData.table2.rows[i].bucode).html(jData.table2.rows[i].bu));
                    }

                    for (var i = 0; i < jData.table3.rows.length; i++) {
                        $("[id$=ddlRequestedBy]").append($("<option></option>").val(jData.table2.rows[i].email).html(jData.table3.rows[i].employee));
                    }

                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        }
        catch (Error) {
            alert(Error);
        }
    }

};

function isNumberKey(evt) {
    debugger;
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || charCode == 46) {
        return true;
    } else {
        alert("Enter only numbers");
        return false;
    }
}


function Validate() {

    try {

        debugger;

        if ($("[id$=ddlDatabase]").val() == '--Select--' || $("[id$=ddlDatabase]").val() == '') {
            alert("Select Databse...");
            return false;
        }
        if ($("[id$=ddlCountry]").val() == '--Select--' || $("[id$=ddlDatabase]").val() == '' || $("[id$=ddlDatabase]").val() == '0') {
            alert("Select Country...");
            return false;
        }
        if ($("[id$=ddlBU]").val() == '--Select--' || $("[id$=ddlBU]").val() == '') {
            alert("Select BU...");
            return false;
        }

        if ($("[id$=txtDraftSORNum]").val() == '' || $("[id$=ddlBU]").val() == '0') {
            alert("Please enter valid SAP Draft SOR Number");
            return false;
        }

        if ($("[id$=txtSORApprovalCode]").val() == '' ) {
            alert("SOR Approval code can't be empty, please retry..");
            return false;
        }

        if ($("[id$=txtRemarks]").val() == '' ) {
            alert("Please enter remarks");
            return false;
        }

        return true;
    }
    catch (Error) {
        alert(Error);
        return false;
    }

};

// Code to Save data
$(document).on("click", "#btnSave", function () {
    try {
        debugger;
        if (Validate() == true) {


           
        }
    }
    catch (Error) {
        alert(Error);
    }
});


// Code to Generate OTP
$(document).on("click", "#BtnGenerateOTP", function () {
    $("[id$=txtApprovalCode]").val('');
    try {
        debugger;
        ShowProgressBar();
        setTimeout(function () {
            HideProgressBar();

        }, 700);

        try {
            $.ajax({
                url: "SalesOrderOTP.aspx/GenerateOTP",
                data: "{'BU':'" + $("[id$=ddlBU]").val() + "','Project':'" + $("[id$=txtCountry]").val() + "','DraftSORDocEntry':'" + $("[id$=lblDocEntry]").val() + "'} ",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (value) {
                    debugger;
                    $("[id$=txtApprovalCode]").val(value.d);
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });



        }
        catch (Error) {
            alert(Error);
        }

    }
    catch (Error) {
        alert(Error);
    }
});



// Code to check if Draft SOR Number is Valid or not
$(document).on("focusout", "[id$=txtDraftSORNum]", function () {
    GetSORList();
});

// Code to View the SOR Details by SOR No.
$(document).on("click", "#BtnView", function () {
    try {
        debugger;
        GetSORList();
    }
    catch (Error) {
        alert(Error);
    }
});


// Start : Get SOR List by Draft SOR Num
function GetSORList() {
    try {
        debugger;

        if ($("[id$=txtDraftSORNum]").val() != "" && $("[id$=ddlDatabase]").val() != "--Select--") {

            try {
                $.ajax({
                    url: "SalesOrderOTP.aspx/Validate_DraftSORNum",
                    data: "{'DraftSORNum':'" + $("[id$=txtDraftSORNum]").val() + "','DBName':'" + $("[id$=ddlDatabase]").val() + "'} ",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (value) {
                        var jData = eval('(' + value.d + ')');
                        debugger;

                        if (jData.table.rows.length > 0) {  // Start : if to show List of Draft SOR in popup

                            $('#classModal').modal('show');

                            var table = $("#classTable");
                            table.find("tr:not(:first)").remove();
                            //Get the count of columns.
                            var columnCount = jData.table.rows.length;

                            for (var i = 0; i < jData.table.rows.length; i++) {
                                row = $(table[0].insertRow(-1));
                                var cell = $("<td />"); cell.html("<a  class='linkTest'><b> <span style='color:blue'> Select </span></b></a>"); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].docentry); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].docnum); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].refno); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].docdate); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].customer); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].doctotal); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].totalsell); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].totalcost); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].totalrebate); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].gp); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].slpname); row.append(cell);
                                var cell = $("<td />"); cell.html(jData.table.rows[i].project); row.append(cell);
                            }

                            $("#classTable tr").on('click', '.linkTest', function (e) {
                                debugger;

                                var DocEntry = $(this).closest('tr').find('td').eq(1).text();
                                var DocNum = $(this).closest('tr').find('td').eq(2).text();
                                var Salesperson = $(this).closest('tr').find('td').eq(11).text();
                                var Project = $(this).closest('tr').find('td').eq(12).text();
                                $("[id$=lblDocEntry]").val(DocEntry);
                                $("[id$=txtCountry]").val(Project);
                                $("[id$=txtSalesPerson]").val(Salesperson);

                                $('#classModal').modal('hide');
                                var table = $("#classTable");
                                table.find("tr:not(:first)").remove();

                                Get_DraftSORApproverList($("[id$=ddlDatabase]").val(), DocEntry);

                            });

                            $('#BtnGenerateOTP').removeAttr('disabled');
                        }
                        else { // if Draft SOR Number is not valid
                            $('#BtnGenerateOTP').attr('disabled', 'disabled');
                            $("[id$=txtApprovalCode]").val('');
                            alert('Invalid Draft SOR Number');
                        }

                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });

            }
            catch (Error) {
                alert(Error);
            }

        } // End of if to check SOR DOCNUM is not empty
        else {
            $('#BtnGenerateOTP').attr('disabled', 'disabled');
        }

    }
    catch (Error) {
        alert(Error);
    }
} // End of GetSORList


/// START : Functions to show and hide loader 
function ShowProgressBar() {
    $('#divLoaderSOROTP').css('visibility', '');
}
function HideProgressBar() {
    $('#divLoaderSOROTP').css('visibility', 'hidden');
}
/// END : Functions to show and hide loader


// This function is to bind the Approver's 
function Get_DraftSORApproverList(DBName,DraftSORDocEntry) {
        $("[id$=ddlCACM] option").remove();
        try {
            $.ajax({
                url: "SalesOrderOTP.aspx/Get_DraftSORApproverList",
                data: "{'DBName':'" + DBName + "','DraftSORDocEntry':'" +DraftSORDocEntry +"'} ",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (value) {
                    debugger;
                    var jData = eval('(' + value.d + ')');
                    debugger;

                    for (var i = 0; i < jData.table.rows.length; i++) {
                        $("[id$=ddlCACM]").append($("<option></option>").val(jData.table.rows[i].email).html(jData.table.rows[i].approver));
                    }
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });

        }
        catch (Error) {
            alert(Error);
        }
    }

