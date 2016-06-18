var AccountPage = function() {

    var that = this;

    this.initPage = function() {
        $("#add-new-post,#my-posts,#rough-copies,#change-email,#change-password,#change-user-photo")
            .on("click",
                function() {
                    $(".my-account-mng li").removeClass("selected");
                    $(this).addClass("selected");
                    that.showMyAccountBlock($(this).attr("id"));
                });

        $("#publish-article-btn").on("click", this.publishBtnClick);

        $("#entry-image-input").on("change", this.previewFile);

        $("#title").on('click', '.selector', function(event) {
            event.preventDefault();
            /* Act on the event */
        });

        $(".form-inline").on("submit", function() {
            return false;
        });

        $("#delete-entry-image").on("click", this.deleteEntryImage);

        $("#title, textarea")
            .on("focus",
                function() {
                    if ($(this).val() === "") {
                        console.log($(this).val());
                    }
                });
    };

    this.isArticleValid = function() {
        if ($("#title").val() === "") {
            if (!$("#empty-title-error-msg").length) {
                var div = document.createElement("div");
                $(div).addClass("alert alert-danger");
                $(div).attr("id", "empty-title-error-msg");
                $(div).text("Title cannot be empty");
                $(".title-panel .panel-body").append(div);
            }
            return false;
        }

        if($("#empty-title-error-msg").length){
            $("#empty-title-error-msg").remove();
        }

        if (tinyMCE.get('article').getContent() === "") {
            if (!$("#empty-content-error-msg").length) {
                var div = document.createElement("div");
                $(div).addClass("alert alert-danger");
                $(div).attr("id", "empty-content-error-msg");
                $(div).text("Article cannot be empty");
                $(".content-panel .panel-body").append(div);
            }
            return false;
        }

        if($("#empty-content-error-msg").length){
            $("#empty-content-error-msg").remove();
        }


        return true;
    }

    this.publishBtnClick = function() {
        if (that.isArticleValid()) {
            if ($("#article-cover-img").attr("src") !== "") {
                console.log("Article is valid");    
            }
        }
    }

    this.showMyAccountBlock = function(idOfClickedElement) {
        var divs = $(".my-account > div");
        for (var i = 0; i < divs.length; i++) {
            if ($(divs[i]).hasClass(idOfClickedElement)) {
                $(divs[i]).show();
            } else {
                $(divs[i]).hide();
            }
        }
    };

    this.previewFile = function() {
        var preview = $("#article-cover-img");
        var file = document.querySelector('input[type=file]').files[0];
        var reader = new FileReader();

        reader.onloadend = function() {
            $(preview).attr("src", reader.result);
        }

        if (file) {
            reader.readAsDataURL(file);
        } else {
            $(preview).attr("src", "");
        }

        $("#article-cover-img").show();
    };

    this.deleteEntryImage = function() {
        $("#article-cover-img").attr("src", "");
        $(".add-post-big-image img").css("display", "none");
    };

}

$(function() {
    var myAccountPage = new AccountPage();
    myAccountPage.initPage();

    tinymce.init({ selector: 'textarea' , plugins: "code image textcolor advlist"});
});
