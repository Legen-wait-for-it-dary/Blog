var categories = [];

var AccountPage = function () {

    var that = this;

    this.initPage = function () {
        $("#add-new-post").on("click", { idOfClickedElement: "add-new-post" }, this.showMyAccountBlock);
        $("#my-posts").on("click", { idOfClickedElement: "my-posts" }, this.showMyAccountBlock);
        $("#rough-copies").on("click", { idOfClickedElement: "rough-copies" }, this.showMyAccountBlock);
        $("#change-email").on("click", { idOfClickedElement: "change-email" }, this.showMyAccountBlock);
        $("#change-password").on("click", { idOfClickedElement: "change-password" }, this.showMyAccountBlock);
        $("#change-user-photo").on("click", { idOfClickedElement: "change-user-photo" }, this.showMyAccountBlock);
        $("#entry-image-input").on("change", this.previewFile);
        $("#delete-entry-image").on("click", this.deleteEntryImage);
        $("#add-new-categ").on("click", function () {
            source.push($("#auto").val());
            $(this).hide();
        });
		
		
		
		
        $("#auto").autocomplete({
            source: function (request, response) {
                var result = $.ui.autocomplete.filter(categories, request.term);

                console.log($.inArray(request.term, result) < 0);
                $("#add-new-categ").toggle($.inArray(request.term, result) < 0);

                response(result);
            }
        });

        $(".form-inline").on("submit", function () { return false; });
		
		$("#delete-entry-image").on("click", this.deleteEntryImage);
    };
	
    this.showMyAccountBlock = function (event) {
        $("#" + event.data.idOfClickedElement).addClass("selected");
        var divs = $(".my-account > div");
        for (var i = 0; i < divs.length; i++) {
            if (!$(divs[i]).hasClass(event.data.idOfClickedElement)) {
                $(divs[i]).css("display", "none");
                $("#" + $(divs[i]).attr("class")).removeClass("selected");
            } else {
                $(divs[i]).css("display", "block");
            }
        }
    };

    this.previewFile = function () {
        var preview = $("#entry-img");
        var file = document.querySelector('input[type=file]').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            $(preview).attr("src", reader.result);
        }

        if (file) {
            reader.readAsDataURL(file);
        } else {
            $(preview).attr("src", "");
        }

        $("#entry-img").css("display", "block");
    };

    this.deleteEntryImage = function () {
        $("#entry-img").attr("src", "");
        $(".add-post-big-image img").css("display", "none");
    };

};

$(function () {
    var myAccountPage = new AccountPage();
    myAccountPage.initPage();

    tinymce.init({ selector: 'textarea' });

    

});
