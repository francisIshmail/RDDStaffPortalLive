
// GET NOTIFICATIONS AND SETUP SIGNALR
debugger
console.log("signalr");
// Click on notification icon to show notification
$('span.notification').click(function (e) {
    e.stopPropagation();
    $('.noti-content').show();
    var count = 0;
    count = parseInt($('span.count').html()) || 0;
    count++;
    // Only load notification if not already loaded
    if (count > 0) {
        updateNotification();
    }
    $('span.count', this).html('&nbsp;');
});

// Hide notifications
$('html').click(function () {
    $('.noti-content').hide();
});

// Update notifications
function updateNotification() {
    $('#notiContent').empty();
    $('#notiContent').append($('<li>Loading...</li>'));
    $.ajax({
        async: false,
        cache: false,
        type: "POST",
        url: '/Dashboard/GetNotificationContacts',
        contentType: "application/json",
        dataType: "json",        
        success: function (response) {
            debugger
            $('#notiContent').empty();
            if (response.Table.length === 0) {
                $('#notiContent').append($('<li>No data available</li>'));
            }
            $(".notification").html(response.Table.length);
            $("#dropdown-title").html("You have " +response.Table.length +" new notification");
            $.each(response.Table, function (index, value) {
                //$('#notiContent').append($('<li>New contact : ' + value.DocumentName + ' (' + value.Description + ') added</li>'));
                $('#notiContent').append($(' <a href="' + value.PVURL + '">' +
                    '<div class= "notif-icon notif-primary" > <i class="fa fa-user-plus"></i> </div >' +
                    '<div class="notif-content">' +
                    '<span class="block">' +
                    ''+ value.DocumentName + '(' + value.Description + ')' +
                    ' </span>' +
                    ' <span class="time">1 day ago</span>' +
                    '</div>' +
                    ' </a >'));
            });
        },
        error: function (error) {
            console.log(error);
        }
    });
}

// Update notifications count
function updateNotificationCount() {
    var count = 0;
    count = parseInt($('span.count').html()) || 0;
    count++;
    $('span.count').html(count);
}

// SignalR js code for start hub and send receive notification(s) from the server side
var notificationHub = $.connection.notificationHub;
$.connection.hub.start().done(function () {
    console.log('Notification hub started');
});

// SignalR method for push server message to client
notificationHub.client.notify = function (message) {
    if (message && message.toLowerCase() === "added") {
        debugger
        updateNotificationCount();
    }
};


