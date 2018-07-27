var contentBlock = '#content',
    dialogsBlock = '#dialogs-container',
    dialogBlock = '#dialog-container',
    dialogs = '.dialogs',
    search = '#dialogs-search-input',
    chatBackButton = '#dialog-header-back',
    chatSendButton = '#dialog-footer-send-icon',
    chatTextInput = '#dialog-footer-input-area';

//====================Переход вперёд и назад по кнопкам браузера=======================//
$(window).on('popstate', function () {
    if (location.href.search(/unread/i) != -1) {
        GetUnreadDialogs();
    }
    //else if (location.href.search(/dialogs\/id/i) != -1) {
    //    GetDialog(location.href.substring(location.href.search(/id/i) + 3));
    //}
    else if (location.href.search(/dialogs\/q/i) != -1) {
        GetQRFullPage(location.href.substring(location.href.search(/q/i) + 2));
    }
    else if (location.href.search(/id/i) == -1 && location.href.search(/q/i) == -1) {
        GetDialogs();
    }
});

////====================Открытие диалога по клику на него=======================//
//$(document).on('click', dialogs, function (event) {
//    var dialogid = $(event.currentTarget).attr('dialogId');
//    GetDialog(dialogid);
//    history.pushState(null, null, "/dialogs/id=" + dialogid);
//});

//====================Кнопка назад=======================//
$(document).on('click', chatBackButton, function () {
    history.back();
});

//====================Кнопка "отправить" в чате=======================//
$(document).on('keyup input', chatTextInput, function () {
    if ($(this).text() == '') $(chatSendButton).attr('class', 'fa fa-paper-plane not-active');
    else $(chatSendButton).attr('class', 'fa fa-paper-plane active');
});

//====================Поиск по диалогам=======================//
var start = true, tmp = "";
$(document).on('keyup input', search, function (event) {
    var query = $(this).val();
    var keyCode = event.keyCode;
    //if ((event.keyCode == 8 || (event.keycode >= 32 && event.keycode <= 126) || (event.keycode >= 1040 && event.keycode <= 1105)) && $('#dialog-footer-input-area').is(":focus"))
    if (tmp != query) {
        if (start) {
            GetQueryResult(query);
            history.pushState(null, null, "/dialogs/q=" + $(this).val());
            start = false;
        }
        else if (query != "") {
            fnDelay(GetQueryResult, query);
            history.pushState(null, null, "/dialogs/q=" + $(this).val());
        }
        else if (query == "") {
            history.pushState(null, null, "/dialogs");
            fnDelay(GetDialogs);
            start = true;
        }
        tmp = query;
    }
});

$(document).on('keydown', function (event) {
    if (location.href.search(/dialogs\/id/i) != -1) {
        if (event.keyCode == 13 && $('#dialog-search-input').is(":focus") && $('#dialog-search-input').val() != "") {
            $.ajax({
                type: "POST",
                url: "/Messages/QueryMessages",
                data: "dialogId=" + $("#dialog-body").attr("dialogId") + "&query=" + $('#dialog-search-input').val(),
                success: function (data) {
                    $('#dialog-body').html(data);
                }
            });
        }
    }
});

$(document).on('click', '#dialog-search-contrl-search', function () {
    if (location.href.search(/dialogs\/id/i) != -1) {
        if ($('#dialog-search-input').val() != "") {
            $.ajax({
                type: "POST",
                url: "/Messages/QueryMessages",
                data: "dialogId=" + $("#dialog-body").attr("dialogId") + "&query=" + $('#dialog-search-input').val(),
                success: function (data) {
                    $('#dialog-body').html(data);
                }
            });
        }
    };
});


$(document).on('click', '#dialogs-filter-unread', function () {
    if (location.href.search(/unread/i) == -1)
    {
        GetUnreadDialogs();
        history.pushState(null, null, "/dialogs/section=unread");
    }
});

$(document).on('click', '#dialogs-filter-all', function () {
    if (location.href.search(/unread/i) != -1) {
        GetDialogs();
        history.pushState(null, null, "/dialogs");
    }
});

$(document).on('click', '#dialog-header-controls-search', function (event) {
    $('#dialog-header-search').css('display', 'block');
    $('#dialog-header-std').css('display', 'none');
    $('#dialog-body').attr('part', $('#dialog-body').attr('partcount'));
    $('#dialog-search-input').focus();
    $(window).scrollTop($(document).height());
});

$(document).on('click', '#dialog-search-contrl-cancel', function (event) {
    $('#dialog-header-search').css('display', 'none');
    $('#dialog-header-std').css('display', 'block');
    $.ajax({
        type: "POST",
        url: "/Messages/LoadMessages",
        data: "dialogId=" + $('#dialog-body').attr('dialogId') + "&part=1",
        success: function (data) {
            $('#dialog-body').html(data);
            $('#dialog-body').attr('part', 1);
            window.scrollTo(0, 0);
            $(window).scrollTop($(document).height());
        }
    });
});

var tmp = 599;
$(document).scroll(function () {
    if (location.href.search(/dialogs\/id/i) != -1 && (Number($('#dialog-body').attr('part')) < Number($('#dialog-body').attr('partcount')))) {
        var part = $('#dialog-body').attr('part');
        if ($(document).scrollTop() <= 600 && tmp >= 600) {
            part++;
            $.ajax({
                type: "POST",
                url: "/Messages/LoadMessages",
                data: "dialogId=" + $('#dialog-body').attr('dialogId') + "&part=" + part,
                success: function (data) {
                    $('#dialog-body').attr('part', part);
                    $('#dialog-body').prepend(data);
                }
            });
        }
        tmp = $(document).scrollTop();
    }
});

function GetUnreadDialogs()
{
    $.ajax({
        type: "POST",
        url: "/Messages/DefaultDialogs",
        data: "isUnread=true",
        success: function (data) {
            $(contentBlock).html(data);
            $(dialogsBlock).removeAttr('style');
            $(dialogBlock).css('display', 'none');
            $(dialogBlock).empty();
            $(search).focus();
        }
    });
}

function GetDialogs()
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
            $(search).focus();
        }
    });
}

function GetQueryResult(query)
{
    $.ajax({
        type: "POST",
        url: "/Messages/QueryDialogs",
        data: "id=" + query + "&isAjax=true",
        success: function (data) {
            $(dialogsBlock).html(data);
            $(dialogsBlock).removeAttr('style');
            $(dialogBlock).css('display', 'none');
            $(dialogBlock).empty();
        }
    });
}

function GetQRFullPage(query) {
    $.ajax({
        type: "POST",
        url: "/Messages/QueryDialogs",
        data: "id=" + query + "&isAjax=false",
        success: function (data) {
            $(contentBlock).html(data);
            $(dialogsBlock).removeAttr('style');
            $(dialogBlock).css('display', 'none');
            $(dialogBlock).empty();
        }
    });
}

function GetDialog(id) {
    $.ajax({
        type: "POST",
        url: "/Messages/CurrentDialog",
        data: "id=" + id,
        success: function (data) {
            $(contentBlock).html(data);
            $(dialogsBlock).css('display', 'none');
            $(dialogBlock).removeAttr('style');
            $(dialogsBlock).empty();
            $(window).scrollTop($(document).height());
        }
    });
}

var timer = 0, _SEC = 400;
function fnDelay(func, query) {
    clearTimeout(timer);
    timer = setTimeout(function () {
        func(query);
    }, _SEC);
};