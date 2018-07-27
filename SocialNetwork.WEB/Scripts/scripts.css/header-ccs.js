function afterLoad() {
    //$('#afterLoad').addClass('hidden-m');
    $('#container').removeClass('hidden-m');
}

$('#header-user-control').click(function () {
    $('#header-user-div').toggleClass('hidden');
    $('#header-user-arrow').toggleClass('arrow-active');
    $('#header-user').toggleClass('header-user-active');
});
$(document).mouseup(function (e) {
    if ($('#header-user-div').has(e.target).length === 0 && $('#header-user-control').has(e.target).length === 0) {
        $('#header-user-div').addClass('hidden');
        $('#header-user-arrow').removeClass('arrow-active');
        $('#header-user').removeClass('header-user-active');
    }
});