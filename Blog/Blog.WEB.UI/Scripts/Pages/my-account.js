﻿tinyMCE.init({ selector: 'textarea' });

var AccountPage = function () {
    var that = this;

    this.initPage = function () {
        $("#add-new-post,#my-posts,#rough-copies,#change-email,#change-password,#change-user-photo")
            .on("click",
                function () {
                    $(".my-account-mng li").removeClass("selected");
                    $(this).addClass("selected");
                    that.showMyAccountBlock($(this).attr("id"));
                });
        $("#my-posts").on("click", this.myPostsClick);
        $("#rough-copies").on("click", this.myRoughCopiesClick);
        $("#entry-image-input").on("change", this.previewFile);
        $("#delete-entry-image").on("click", this.deleteArticleCoverImage);
        $("#publish-btn").on("click", this.publishBtnClick);
        $(".form-inline").on("submit", function () { return false; });
    };

    this.deleteArticle = function() {
        console.log($(this).attr("art-id"));
        var xhr = $.ajax({
            url: "/MyAccount/DeleteArticle",
            dataType: "json",
            type: "POST",
            data: {
                articleId: $(this).attr("art-id")
            },
            success: function() {
                location.reload();
            }
        });
    };

    this.myRoughCopiesClick = function () {
        $.ajax({
            url: "/MyAccount/GetMyRoughCopies",
            dataType: "json",
            type: "GET",
            success: function (data) {
                var responseArr = jQuery.parseJSON(data.data);
                var allPropertyNames = Object.keys(responseArr);
                $(".rough-copies > * ").remove();
                for (var i = 0; i < allPropertyNames.length; i++) {
                    that.createPostsList($(".rough-copies"), allPropertyNames[i], responseArr[allPropertyNames[i]]);
                };
            },
            error: function () {
                console.log("my post error");
            }
        });
    };

    this.myPostsClick = function () {
        $.ajax({
            url: "/MyAccount/GetMyPublishedPosts",
            dataType: "json",
            type: "GET",
            success: function (data) {
                var responseArr = jQuery.parseJSON(data.data);
                var allPropertyNames = Object.keys(responseArr);
                $(".my-posts > * ").remove();
                for (var i = 0; i < allPropertyNames.length; i++) {
                    that.createPostsList($(".my-posts"),allPropertyNames[i], responseArr[allPropertyNames[i]]);
                };
            },
            error: function () {
                console.log("my post error");
            }
        });
    };

    this.createPostsList = function (root,articleId, articleTitle) {
        var inputGroupDiv = document.createElement("div");
        $(inputGroupDiv).addClass("input-group");

        var a = document.createElement("a");
        $(a).addClass("list-group-item");
        $(a).text(articleTitle);
        $(a).attr("href", "/Article/Index/" + articleId);
        $(inputGroupDiv).append(a);

        var inputGroupBtn = document.createElement("div");
        $(inputGroupBtn).addClass("input-group-btn");

        var updateBtnGroup = document.createElement("div");
        $(updateBtnGroup).addClass("btn-group");
        var deleteBtnGroup = document.createElement("div");
        $(deleteBtnGroup).addClass("btn-group");

        var updateBtn = document.createElement("button");
        $(updateBtn).addClass("btn btn-default update-btn")
                    .text("Update")
                    .attr("art-id", articleId);

        var deleteBtn = document.createElement("button");
        $(deleteBtn).addClass("btn btn-default delete-btn")
                    .text("Delete")
                    .attr("art-id", articleId);

        $(deleteBtn).on('click', that.deleteArticle);

        $(updateBtnGroup).append(updateBtn)
        $(deleteBtnGroup).append(deleteBtn);

        $(inputGroupBtn)
                        .append(updateBtnGroup)
                        .append(deleteBtnGroup);

        $(inputGroupDiv).append(inputGroupBtn);

        $(root).append(inputGroupDiv);
    }

    this.isArticleValid = function () {
        var div;
        if ($("#title").val() === "") {
            if (!$("#empty-title-error-msg").length) {
                div = document.createElement("div");
                $(div).addClass("alert alert-danger");
                $(div).attr("id", "empty-title-error-msg");
                $(div).text("Title cannot be empty");
                $(".title-panel .panel-body").append(div);
            }
            return false;
        }

        if ($("#empty-title-error-msg").length) {
            $("#empty-title-error-msg").remove();
        }

        if (tinyMCE.get('article').getContent() === "") {
            if (!$("#empty-content-error-msg").length) {
                div = document.createElement("div");
                $(div).addClass("alert alert-danger");
                $(div).attr("id", "empty-content-error-msg");
                $(div).text("Article cannot be empty");
                $(".content-panel .panel-body").append(div);
            }
            return false;
        }

        if ($("#empty-content-error-msg").length) {
            $("#empty-content-error-msg").remove();
        }

        return true;
    }

    this.publishBtnClick = function () {
        if (that.isArticleValid()) {
            if ($("#article-cover-img").attr("src") !== "") {
                that.uploadImageToServer();
            } else {
                that.uploadArticle(null);
            }
            
        }
    };

    this.uploadArticle = function (mediaFileId) {
        var formattedText = tinyMCE.get('article').getContent();
        var title = $("#title").val();
        var category = $('#category').find(":selected").text();

        $.ajax({
            url: "/MyAccount/UploadArticle",
            dataType: "json",
            type: "POST",
            data: {
                formattedText: escape(formattedText),
                mediaFileId: mediaFileId,
                title: title,
                category: category
            },
            success: function () {
                location.reload();
            },
            error: function () {
                console.log("uploadArticle error");
            }
        });
    };

    this.uploadImageToServer = function () {
        var data = new FormData();
        var files = $('input[type=file]').get(0).files;
        if (files.length > 0) {
            data.append("ArticleCover", files[0]);
        }

        var xhr = $.ajax({
            url: "/MyAccount/UploadArticleCoverImage",
            type: "POST",
            processData: false,
            contentType: false,
            data: data,
            error: function () {
                console.log("Image upload error");
            }
        });

        xhr.done(function (data) {
            that.uploadArticle(data.mediaFileId);
        });
    };

    this.showMyAccountBlock = function (idOfClickedElement) {
        var divs = $(".my-account > div");
        for (var i = 0; i < divs.length; i++) {
            if ($(divs[i]).hasClass(idOfClickedElement)) {
                $(divs[i]).show();
            } else {
                $(divs[i]).hide();
            }
        }
    };

    this.previewFile = function () {
        var preview = $("#article-cover-img");
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

        $("#article-cover-img").show();
    };

    this.deleteArticleCoverImage = function () {
        $("#article-cover-img").attr("src", "");
        $(".add-post-big-image img").hide();
    };

};

$(function () {
    var myAccountPage = new AccountPage();
    myAccountPage.initPage();
});

