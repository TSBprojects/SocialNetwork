$(document).ready(function () {
    if (location.href.search(/id/i) != -1) $(window).scrollTop($(document).height());
});

afterLoad();

$('#dialogs-filter-all').click(function () {
    $('#dialogs-filter-line').animate({
        top: $('#dialogs-filter-all').position().top + 'px'
    }, {
            queue: false,
            duration: 200
        });
});
$('#dialogs-filter-unread').click(function () {
    $('#dialogs-filter-line').animate({
        top: $('#dialogs-filter-unread').position().top + 'px'
    }, {
            queue: false,
            duration: 200
        });
});

$(document).on('mouseenter', '#dialog-header-controls-contrl, #dialog-header-controls-contrl-div', function (event) {
    if ($(event.currentTarget).attr('id') == 'dialog-header-controls-contrl') {
        $(event.currentTarget).next().removeClass('hidden');
        $(event.currentTarget).addClass('fa-spin');
    }
    else if ($(event.currentTarget).attr('id') == 'dialog-header-controls-contrl-div') {
        $(event.currentTarget).removeClass('hidden');
        $(event.currentTarget).prev().addClass('fa-spin');
    }
});
$(document).on('mouseleave', '#dialog-header-controls-contrl, #dialog-header-controls-contrl-div', function (event) {
    if ($(event.currentTarget).attr('id') == 'dialog-header-controls-contrl') {
        $(event.currentTarget).next().addClass('hidden');
        $(event.currentTarget).removeClass('fa-spin');
    }
    else if ($(event.currentTarget).attr('id') == 'dialog-header-controls-contrl-div') {
        $(event.currentTarget).addClass('hidden');
        $(event.currentTarget).prev().removeClass('fa-spin');
    }
});

$(document).on('mouseenter', '#dialog-footer-clip-icon, #dialog-footer-contrl-div', function (event) {
    if ($(event.currentTarget).attr('id') == 'dialog-footer-clip-icon') {
        $(event.currentTarget).next().removeClass('hidden');
        $(event.currentTarget).addClass('fa-spin');
    }
    else if ($(event.currentTarget).attr('id') == 'dialog-footer-contrl-div') {
        $(event.currentTarget).removeClass('hidden');
        $(event.currentTarget).prev().addClass('fa-spin');
    }
});
$(document).on('mouseleave', '#dialog-footer-clip-icon, #dialog-footer-contrl-div', function (event) {
    if ($(event.currentTarget).attr('id') == 'dialog-footer-clip-icon') {
        $(event.currentTarget).next().addClass('hidden');
        $(event.currentTarget).removeClass('fa-spin');
    }
    else if ($(event.currentTarget).attr('id') == 'dialog-footer-contrl-div') {
        $(event.currentTarget).addClass('hidden');
        $(event.currentTarget).prev().removeClass('fa-spin');
    }
});

if (location.href.search(/id/i) != -1)
{
    frame.onresize = function () {
        if ($(document).height() - ($(window).scrollTop() + $(window).height()) < 260) {
            $(window).scrollTop($(document).height());
        }
    }
}

$(document).on('keypress', '#dialog-footer-input-area', function (event) {
    if (event.which == 13) {
        if (!event.shiftKey && !event.ctrlKey) return false;
    }
});

$(document).on('click', '#show-bymessage-only-button', function () {
    $('#show-bymessage-only-button').css('display', 'none');
    $('#show-ByDialogName').css('display', 'none');
    $('#dialogs-empty').css('display', 'block');
});