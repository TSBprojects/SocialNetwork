$('#user-main-head>.user-light-button').click(function (event) {
    $.ajax({
        type: "POST",
        url: "/Friends/SendFriendRequest",
        data: "friendId=" + $('#user-main').attr('userId'),
    });
    history.pushState(null, null, "/friends");
});
$('#user-main-head>.user-dark-button').click(function (event) {
    $.ajax({
        type: "POST",
        url: "/Messages/StartDialog",
        data: "userId=" + $('#user-main').attr('userId'),
    });
});