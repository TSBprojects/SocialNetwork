var contentBlock = '#content',
    dialogsBlock = '#dialogs-container',
    dialogBlock = '#dialog-container';

$('#nav-message').click(function (event) {
    if (location.href.search(/dialogs\/id/i) != -1 || location.href.search(/q/i) != -1 || location.href.search(/section/i) != -1 )
    {
        $.ajax({
            type: "POST",
            url: "/Messages/DefaultDialogs",
            data: "isUnread=false",
            success: function (data) {
                $(contentBlock).html(data);
                $(dialogsBlock).removeAttr('style');
                $(dialogBlock).css('display', 'none');
                $(dialogBlock).empty();
                $(window).scrollTop(0);
            }
        });
        history.pushState(null, null, "/dialogs");
    }
    else if (location.href.search(/dialogs/i) != -1){}
    else window.location.href = "/dialogs";
});

$('#nav-friends').click(function (event) {
    if (location.href.search(/section=online/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=all&isList=true",
            success: function (data) {
                $('#friends-list').html(data);
            }
        });
        history.pushState(null, null, "/friends");
    }
    else if (location.href.search(/section=in_requests/i) != -1 || location.href.search(/section=out_requests/i) != -1 || location.href.search(/section=find/i) != -1)
    {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=all&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
        history.pushState(null, null, "/friends");
    }
    else if (location.href.search(/find/i) != -1 || location.href.search(/friends/i) == -1) { window.location.href = "/friends"; }
});
