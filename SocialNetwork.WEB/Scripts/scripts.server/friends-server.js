$(window).on('popstate', function () {
    if (location.href.search(/online/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=online&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
    } else if (location.href.search(/in_requests/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=in_requests&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
    } else if (location.href.search(/out_requests/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=out_requests&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
    }
    else if (location.href.search(/find/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=find&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
    }
    else if (location.href.search(/friends/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=all&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
    }
});

$(document).on('click', '#find-friends-button', function (event) {
    $.ajax({
        type: "POST",
        url: "/Friends/Handler",
        data: "section=find&isList=false",
        success: function (data) {
            $('#content').html(data);
        }
    });
    history.pushState(null, null, "/friends/section=find");
});
$(document).on('click', '#friends-tab-all', function (event) {
    if (location.href.search(/section=online/i) != -1) 
    {
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
});
$(document).on('click', '#friends-tab-online', function (event) {
    if (location.href.search(/section=online/i) == -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=online&isList=true",
            success: function (data) {
                $('#friends-list').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=online");
    }
});
$(document).on('click', '#friends-tab-inbox', function (event) {
    if (location.href.search(/section=in_requests/i) == -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=in_requests&isList=true",
            success: function (data) {
                $('#requests-list').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=in_requests");
    }
});
$(document).on('click', '#friends-tab-outbox', function (event) {
    if (location.href.search(/section=out_requests/i) == -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=out_requests&isList=true",
            success: function (data) {
                $('#requests-list').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=out_requests");
    }
});
$(document).on('click', '#friends-filter-myfriends', function (event) {
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
    else if (location.href.search(/section=in_requests/i) != -1 || location.href.search(/section=out_requests/i) != -1 || location.href.search(/section=find/i) != -1) {
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
});
$(document).on('click', '#friends-filter-friendsrequests', function (event) {
    if (location.href.search(/section=out_requests/i) != -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=in_requests&isList=true",
            success: function (data) {
                $('#requests-list').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=in_requests");
    }
    else if (location.href.search(/section=in_requests/i) != -1) {}
    else 
    {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=in_requests&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=in_requests");
    
    }
});
$(document).on('click', '#friends-filter-findfriends', function (event) {
    if (location.href.search(/find/i) == -1) {
        $.ajax({
            type: "POST",
            url: "/Friends/Handler",
            data: "section=find&isList=false",
            success: function (data) {
                $('#content').html(data);
            }
        });
        history.pushState(null, null, "/friends/section=find");
    }
});

$(document).on('click', '.friends-user-dialog', function (event) {
    var f = "/dialogs/id=" + $(event.currentTarget).attr('dialogId');
    window.location.href = f;
});

$(document).on('click', '.friends-user-add', function (event) {
    var count;
    $.ajax({
        type: "POST",
        url: "/Friends/AcceptFriendRequest",
        data: "friendId=" + $(event.currentTarget).parent().parent().attr('userId'),
        success: function (data) {
            count = Number($('#friends-tab-inbox>.friends-count').text());
            $('#friends-tab-inbox>.friends-count').text(--count)
            $(event.currentTarget).parent().parent().remove();
        }
    });
});
