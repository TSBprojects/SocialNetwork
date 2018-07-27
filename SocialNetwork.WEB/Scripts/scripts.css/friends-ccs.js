if (location.href.search(/section=find/i) == -1)
{
    $('#friends-tab-line').css({
        'width': $('#friends-tab-all, #friends-tab-inbox').width() + 20 + 'px',
        'left': $('#friends-tab-all, #friends-tab-inbox').position().left + 15 + 'px'
    });

}
afterLoad();

$('#friends-filter-myfriends').click(function () {
    $('#friends-filter-line').animate({
        top: $('#friends-filter-myfriends').position().top + 'px'
    }, {
        queue: false,
        duration: 200
    });
});
$('#friends-filter-friendsrequests').click(function () {
    $('#friends-filter-line').animate({
        top: $('#friends-filter-friendsrequests').position().top + 'px'
    }, {
        queue: false,
        duration: 200
    });
});
$('#friends-filter-findfriends').click(function () {
    $('#friends-filter-line').animate({
        top: $('#friends-filter-findfriends').position().top + 'px'
    }, {
        queue: false,
        duration: 200
    });
});

$(document).on('click', '#friends-tab-all, #friends-tab-inbox, #friends-tab-online, #friends-tab-outbox', function (e) {
    $('#friends-tab-line').animate({
        width: $(e.currentTarget).width() + 20 + 'px',
        left: $(e.currentTarget).position().left + 15 + 'px'
    }, {
        queue: false,
        duration: 200
    });
});
$(document).on('click', '#friends-filter-myfriends, #friends-filter-friendsrequests, #friends-filter-findfriends', function (e) {
    $('#friends-filter-line').animate({
        top: $(e.currentTarget).position().top + 'px'
    }, {
        queue: false,
        duration: 200
    });
});


$(document).on('click', '#option-title-city-opt', function () {
    $('#friends-search-filter-city-select').addClass('option-title');
});
$(document).on('click', '.option-city-opt', function () {
    $('#friends-search-filter-city-select').removeClass('option-title');
});

$(document).on('click', '#option-title-age-from-opt', function () {
    $('#friends-search-filter-age-from').addClass('option-title');
});
$(document).on('click', '.option-age-from-opt', function () {
    $('#friends-search-filter-age-from').removeClass('option-title');
});

$(document).on('click', '#option-title-age-to-opt', function () {
    $('#friends-search-filter-age-to').addClass('option-title');
});
$(document).on('click', '.option-age-to-opt', function () {
    $('#friends-search-filter-age-to').removeClass('option-title');
});

$(document).on('click', '#friends-search-contrl', function () {
    $('#friends-search-filter').toggleClass('hidden');
});

$(document).mouseup(function (e) {
    if ($('#friends-search-contrl').has(e.target).length === 0 && $('#friends-search-filter').has(e.target).length === 0)
        $('#friends-search-filter').addClass('hidden');
});

$(document).on('mouseenter', '.friends-user-contrl, .friends-user-contrl-div', function (event) {
    if ($(event.currentTarget).hasClass('friends-user-contrl')) {
        $(event.currentTarget).next().removeClass('hidden');
        $(event.currentTarget).addClass('fa-spin');
    }
    else if ($(event.currentTarget).hasClass('friends-user-contrl-div')) {
        $(event.currentTarget).removeClass('hidden');
        $(event.currentTarget).prev().addClass('fa-spin');
    }
});
$(document).on('mouseleave', '.friends-user-contrl, .friends-user-contrl-div', function (event) {
    if ($(event.currentTarget).hasClass('friends-user-contrl')) {
        $(event.currentTarget).next().addClass('hidden');
        $(event.currentTarget).removeClass('fa-spin');
    }
    else if ($(event.currentTarget).hasClass('friends-user-contrl-div')) {
        $(event.currentTarget).addClass('hidden');
        $(event.currentTarget).prev().removeClass('fa-spin');
    }
});