var IndexPage = function () {

    var that = this;

    this.initPage = function () {
        $("#sign-up-link").on("click", this.signUpLinkClick);
        $("#sign-in-link").on("click", this.signInLinkClick);
        $(".sign-in input").on("change",
			function () {
			    that.signInInputChange($(this));
			}
		);
    };

    this.signUpLinkClick = function () {
        $("#sign-in").hide();
        $("#sign-up").show();
    };

    this.signInLinkClick = function () {
        $("#sign-up").hide();
        $("#sign-in").show();
    };
};

$(function () {
    var indexPage = new IndexPage();
    indexPage.initPage();

});
