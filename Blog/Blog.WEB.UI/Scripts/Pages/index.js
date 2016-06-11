var IndexPage = function () {

    var that = this;

    this.initPage = function () {
        $("#sign-up-link").on("click", this.signUpLinkClick);
        $("#submit-sign-in").on("click", this.submitSignInClick);
        $("#submit-sign-up").on("click", this.submitSignUpClick);
    };

    this.signUpLinkClick = function () {
        $(".sign-in").css("display", "none");
        $(".sign-up").css("display", "block");
    };

    this.submitSignInClick = function () {
        var email = $("#sign-in-email").val();
        var password = $("#sign-in-password").val();

        var xhr = $.ajax({
            url: "/Home/SignIn",
            dataType: "json",
            type: "POST",
            data: {
                email: email,
                password: password
            },
            success: function () {
                window.location = "/Articles/Index";
            },
            error: function () {
                alert('Incorrect login or password');
            }
        });

        return false;
    };

    this.submitSignUpClick = function () {
        
    };
};

$(function () {
    var indexPage = new IndexPage();
    indexPage.initPage();

});
