var data;
$(function () {
    var hub = $.connection.workHub;

    //========================================Messages========================================//
    hub.client.showMessage = function (d) {
        data = d;
        //Вывод сообщений в чате
        if (location.href.search(/id/i) != -1) {
            ShowMessage('');
            hub.server.readMessages(data.dialogId);
        }
        //Вывод последнего сообщения в диалогах
        else if (location.href.search(/dialogs/i) != -1) {
            ShowDialogMessage();
        }
    };
    hub.client.showMessageForCaller = function (d) {
        data = d;
        //Вывод сообщений в чате
        if (location.href.search(/id/i) != -1) {
            ShowMessage('unread');
        }
            //Вывод последнего сообщения в диалогах
        else if (location.href.search(/dialogs/i) != -1) {
            ShowDialogMessage();
        }
    };

    hub.client.showReadMessages = function (){
        $('.unread').removeClass('unread');
    }

    //========================================================================================//

    //=========================================Online=========================================//
    hub.client.usersOnline = function(userId){
        if (location.href.search(/friends/i) != -1)
        {
            $(".friend").each(function () {
                if ($(this).attr('userId') == userId) {
                    $(this).children('.friend-img-link').addClass('online');
                    return false;
                }
            });
        }
        else if (location.href.search(/dialogs/i) != -1) {
            $(".dialogs").each(function () {
                if ($(this).children('.dialogs-img-link').attr('userId') == userId) {
                    $(this).children('.dialogs-img-link').addClass('online');
                    return false;
                }
            });
        }
    }
    hub.client.usersOffline = function (userId) {
        if (location.href.search(/friends/i) != -1) {
            $(".friend").each(function () {
                if ($(this).attr('userId') == userId) {
                    $(this).children('.friend-img-link').removeClass('online');
                    return false;
                }
            });
        } else if (location.href.search(/dialogs/i) != -1) {
            $(".dialogs").each(function () {
                var f = $(this).children('.dialogs-img-link').attr('userId');
                if (f== userId) {
                    $(this).children('.dialogs-img-link').removeClass('online');
                    return false;
                }
            });
        }
    }
    //========================================================================================//

    $.connection.hub.start().done(function () {

        //========================================Messages========================================//
        //Отправка сообщения на сервер по нажатию "enter"
        $(document).on('keydown', function (event) {
            if ((!event.ctrlKey && !event.shiftKey)
                    && event.keyCode == 13
                    && $('#dialog-footer-input-area').is(":focus")
                    && $('#dialog-footer-input-area').text() != "") {
                hub.server.sendMessage($("#dialog-body").attr("dialogId"), $("#dialog-footer-input-area").html());
                $('#dialog-footer-send-icon').attr('class', 'fa fa-paper-plane not-active');
                $('#dialog-footer-input-area').text('');
                //$('#dialog-footer-input-area').focus();
            }
        });
        //Отправка сообщения на сервер по кнопке
        $(document).on('click', '#dialog-footer-send-icon', function (event) {
            if ($('#dialog-footer-input-area').text() != "") {
                hub.server.sendMessage($("#dialog-body").attr("dialogId"), $('#dialog-footer-input-area').html());
                $('#dialog-footer-send-icon').attr('class', 'fa fa-paper-plane not-active');
                $('#dialog-footer-input-area').text('');
                $('#dialog-footer-input-area').focus();
            }
        });
        
        $(document).ready(function()
        {
            if (location.href.search(/dialogs\/id/i) != -1) {
                var dialogid = location.href.substring(location.href.search(/id/i) + 3);
                $.ajax({
                    type: "POST",
                    url: "/Messages/CurrentDialog",
                    data: "id=" + dialogid,
                    success: function (data) {
                        $('#content').html(data);
                        $(window).scrollTop($(document).height());
                        hub.server.readMessages(dialogid);
                    }
                });
            }
        });

        $(window).on('popstate', function () {
            if (location.href.search(/dialogs\/id/i) != -1) {
                var dialogid = location.href.substring(location.href.search(/id/i) + 3);
                $.ajax({
                    type: "POST",
                    url: "/Messages/CurrentDialog",
                    data: "id=" + dialogid,
                    success: function (data) {
                        $('#content').html(data);
                        $(window).scrollTop($(document).height());
                        hub.server.readMessages(dialogid);
                    }
                });
            }
        });
        //====================Открытие диалога по клику на него=======================//
        $(document).on('click', '.dialogs', function (event) {
            var dialogid = $(event.currentTarget).attr('dialogId');
            $.ajax({
                type: "POST",
                url: "/Messages/CurrentDialog",
                data: "id=" + dialogid,
                success: function (data) {
                    $('#content').html(data);
                    $(window).scrollTop($(document).height());
                    hub.server.readMessages(dialogid);
                }
            });
            history.pushState(null, null, "/dialogs/id=" + $(event.currentTarget).attr('dialogId'));
        });
        //========================================================================================//

    });
});

function ShowMessage(unread)
{
    var lastMessSec = $('.dialog-nav').last().attr('sec'),
    lastMessUser = $('.dialog-nav').last().attr('userId');

    if (lastMessUser == data.userId && data.DateSec - lastMessSec < 5) {
        $("#dialog-body").append(
            "<div class='dialog-message-min " + unread + "' messId='" + data.messId + "'>" +
                "<div class='dialog-info-min  dialog-nav'  sec='" + data.DateSec + "'  userId='" + data.userId + "'>" +
                    "<div class='dialog-text'>" + htmlEncode(data.Text) + "</div>" +
                "</div>" +
            "</div>"
        );
        $(window).scrollTop($(document).height());
    }
    else {
        if (data.isToday) {
            $("#dialog-body").append(
                "<div class='dialog-date-sep'>Сегодня</div>" +
                "<div class='dialog-message " + unread + "' messId='" + data.messId + "'>" +
                "<div class='dialog-img-link'>" +
                    "<img class='dialog-img' src='" + data.UserProfImage + "' />" +
                "</div><div class='dialog-info  dialog-nav' sec='" + data.DateSec + "'  userId='" + data.userId + "'>" +
                    "<div class='dialog-name'>" + data.FromUserName + "</div>" +
                    "<div class='dialog-last-date'>" + data.Date + "</div>" +
                    "<div class='dialog-text'>" + htmlEncode(data.Text) + "</div>" +
                "</div>" +
            "</div>"
            );
        }
        else {
            $("#dialog-body").append(
                "<div class='dialog-message " + unread + "' messId='" + data.messId + "'>" +
                "<div class='dialog-img-link'>" +
                    "<img class='dialog-img' src='" + data.UserProfImage + "' />" +
                "</div><div class='dialog-info  dialog-nav' sec='" + data.DateSec + "'  userId='" + data.userId + "'>" +
                    "<div class='dialog-name'>" + data.FromUserName + "</div>" +
                    "<div class='dialog-last-date'>" + data.Date + "</div>" +
                    "<div class='dialog-text'>" + htmlEncode(data.Text) + "</div>" +
                "</div>" +
            "</div>"
            );
        }
        $(window).scrollTop($(document).height());
    }
}
function ShowDialogMessage()
{
    var newDial = true, mcount = 0;
    $(".dialogs").each(function () {
        if ($(this).attr('dialogId') == data.dialogId) {
            mcount = Number($(this).children('.dialogs-info').children('.dialogs-unread-count').text());
            $(this).children('.dialogs-info').children('.dialogs-last-date').text(data.Date);
            $(this).children('.dialogs-info').children('.dialogs-last-message-img').attr('src', data.UserProfImage);
            $(this).children('.dialogs-info').children('.dialogs-last-message').children('.dialogs-last-message-mes').html(htmlEncode(data.Text).replace(new RegExp("<br>", "g"), " "));
            $(this).children('.dialogs-info').children('.dialogs-last-message').children('.dialogs-last-message-img').remove();
            $(this).children('.dialogs-info').children('.dialogs-unread-count').text(++mcount); 
            $(this).children('.dialogs-info').children('.dialogs-unread-count').removeAttr('style');
            $(this).addClass('unread');
            $('#dialogs-container>ul').prepend($(this));
            newDial = false;
            return false;
        }
    });
    if (newDial) {
        $('#dialogs-container>ul').prepend(
            "<li class='dialogs button unread' dialogId='" + data.dialogId + "'>" +
                "<div class='dialogs-img-link'>" +
                    "<img class='dialogs-img' src='" + data.DialProfImage + "'/>" +
                "</div><div class='dialogs-info'>" +
                    "<div class='dialogs-name'>" + data.DialogName + "</div>" +
                    "<i class='dialogs-close fa fa-close'></i>" +
                    "<div class='dialogs-last-date'>" + data.Date + "</div>" +
                    "<div class='dialogs-last-message'>" +
                        "<img class='dialogs-last-message-img' src='" + data.UserProfImage + "' /><span class='dialogs-last-message-mes'>" + htmlEncode(data.Text).replace(new RegExp("<br>","g")," ") + "</span>" +
                    "</div>" +
                    "<div class='dialogs-unread-count'>"+1+"</div>"+
                "</div>" +
            "</li>"
        );
    }
}
function htmlEncode(value) {
    return value;
    //var encodedValue = $('<div />').text(value).html();
    //return encodedValue;
}

//var usersOnline

//$(function () {
//    var online = $.connection.onlineHub;
//    online.client.refresh = function (data) {
//        usersOnline = data;
//        for (var i = 0; i < $('.users').get().length; i++) {
//            if (IsUserOnline($('.users:eq(' + i + ')').attr('user'))) {
//                $('.users:eq(' + i + ')').children("div").children("div").attr('class', 'online');
//            }
//            else {
//                $('.users:eq(' + i + ')').children("div").children("div").attr('class', 'offline');
//            }
//        }
//    };
//    $.connection.hub.start().done(function () { /*вызов методов сервера*/ });
//});

//function IsUserOnline(userId) {
//    for (var j = 0; j < usersOnline.length; j++)
//        if (usersOnline[j] == userId)
//            return true;
//    return false;
//}

