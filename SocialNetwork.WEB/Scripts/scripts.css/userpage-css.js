var um = $('#user-main');
var scroll = $(window).scrollTop();

if ($(window).height() < um.height() + 40) {
    if ($(window).scrollTop() > um.height() - $(window).height() - 20) {
        um.css({
            left: $('#content').offset().left + 612 + 'px',
            marginTop: '0'
        });
        um.addClass('fixed');
        um.removeClass('top70');
        um.addClass('bottom20');
    }
}
else {
    um.css({
        left: $('#content').offset().left + 612 + 'px',
        marginTop: '0',
    });
    um.addClass('fixed');
    um.addClass('top70');
    um.removeClass('bottom20');
}

afterLoad();

$(window).scroll(function () {
    if ($(window).height() < um.height() + 40) {

        var a = $(window).scrollTop();
        var topOffset = um.offset().top - $(window).scrollTop();
        var botOffset = $(window).scrollTop() + $(window).height() - um.offset().top - um.height();

        if (a > scroll && botOffset > 20) {
            //alert('down');
            um.css({
                left: $('#content').offset().left + 612 + 'px',
                marginTop: '0'
            });
            um.addClass('fixed');
            um.removeClass('top70');
            um.addClass('bottom20');
        }
        else if (a < scroll && topOffset > 70) {
            
            um.css({
                left: $('#content').offset().left + 612 + 'px',
                marginTop: '0'
            });
            um.addClass('fixed');
            um.removeClass('bottom20');
            um.addClass('top70');
        }
        else {
            //alert('rf');
            um.css({
                marginTop: um.offset().top - 50 + 'px',
            });
            um.removeClass('fixed');
            //um.removeClass('bottom20');
            //um.removeClass('top70');
        }

        scroll = $(window).scrollTop();

    }
    else {
        um.css({
            left: $('#content').offset().left + 612 + 'px',
            marginTop: '0',
        });
        um.addClass('fixed');
        um.addClass('top70');
        um.removeClass('bottom20');
    }
});

$(window).resize(function () {
    um.css({
        left: $('#content').offset().left + 612 + 'px'
    });

    if ($(window).height() < um.height() + 90) {
        um.css({
            marginTop: '0'
        });
        um.addClass('fixed');
        um.removeClass('bottom20');
        um.addClass('top70');
    }
    else {
        um.css({
            left: $('#content').offset().left + 612 + 'px',
            marginTop: '0',
        });
        um.addClass('fixed');
        um.addClass('top70');
        um.removeClass('bottom20');
    }
});

$('#user-info-long-button').click(function () {
    $('#user-info-long').toggleClass('open');

});

bodyFrame.onresize = function () {
    um.css({
        left: $('#content').offset().left + 612 + 'px'
    });

    if ($(window).height() < um.height() + 90) {
        um.css({
            marginTop: '0'
        });
        um.addClass('fixed');
        um.removeClass('bottom20');
        um.addClass('top70');
    }
    else {
        um.css({
            left: $('#content').offset().left + 612 + 'px',
            marginTop: '0',
        });
        um.addClass('fixed');
        um.addClass('top70');
        um.removeClass('bottom20');
    }
};