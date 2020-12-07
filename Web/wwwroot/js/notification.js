$(document).ready(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl('/NotificationHub').build();
    connection.on('notify', function (message)
    {
        alert(message);
    });
    connection.start();
});