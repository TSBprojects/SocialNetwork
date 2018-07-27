$.ajax({
    type: "POST",
    url: "/Account/IsAuth",
    success: function (data) {
        if (data == "True") location.href = "/Home/Index";
        //else 
        //{
        //    //$('#login-p').css('display', 'none');
        //    $('#login-p').removeAttr('style');
        //}
    }
});

//window.onbeforeunload = function () {
//    if (location.href.search(/Index/i) != -1) {
//        location.href = "/Home/Index";
//    }
//};