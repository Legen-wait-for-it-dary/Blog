var CommonPage = function () {

    var that = this;

    this.initPage = function () {
        //$("#menu-btn").on("click", this.menuBtnClick);
        //$(".user-block div").on("click", this.userBlockClick);
    };

    //this.userBlockClick = function () {
    //    if ($(".user-block ul").css("display") == "block") {
    //        $(".user-block ul").css("display", "none");
    //        $(".user-block").css("border-radius", "5px");
    //    } else {
    //        $(".user-block ul").css("display", "block");
    //        $(".user-block").css("border-radius", "5px 5px 0 0");
    //    }
    //};

    //this.menuBtnClick = function () {
    //    $(".site-header ul").css("display", "block");
    //    $("#site-nav").css("width", "100%");
    //    $("#site-nav .col-3").css("width", "100%");
    //};

};

$(function () {
    var commonPage = new CommonPage();
    commonPage.initPage();

    

    $(window).scroll(function () {
        var sT = $(this).scrollTop();
        //alert(sT);
        if (sT >= 3) {
            $('.navbar-brand').addClass('small-nav');
            $('ul.navbar-nav').addClass('small-nav');
            $('.navbar').css('border-bottom', '1px solid lightgray');
            $('.navbar-toggle').css('margin-top', '10px');
        } else {
            $('.navbar-brand').removeClass('small-nav');
            $('ul.navbar-nav').removeClass('small-nav');
            $('.navbar-toggle').css('margin-top', '40px');
        }
    })
});
