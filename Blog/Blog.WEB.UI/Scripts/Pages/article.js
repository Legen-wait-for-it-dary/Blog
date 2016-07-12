var ArticlePage = function () {

    var that = this;

    this.initPage = function () {
        $("#add-comment").on("click", this.addCommentClick);
        $('#show-menu-btn').on('click', function (event) {
            if ($('.site-nav > ul').css('display') == 'block') {
                $('.site-nav > ul').hide();
            } else {
                $('.site-nav > ul').show();
            }
        });
        $(window).on('scroll', this.windowScrolled);
    };

    this.windowScrolled = function () {
        if ($(window).scrollTop() > 30) {
            $('nav.site-nav').removeClass('higgerNarrower');
            $('nav.site-nav').addClass('shorterWidder');
        } else {
            $('nav.site-nav').removeClass('shorterWidder');
            $('nav.site-nav').addClass('higgerNarrower');
        }
    };

    this.addCommentClick = function () {
        var content = $("#add-comment-content").val();
        var articles = $("article");
        var articleId = $(articles[0]).attr("art-mediaFileId");

        if (!content) {
            alert("Can't post empty comment");
            return;
        }

        var xhr = $.ajax({
            url: "/Article/AddComment",
            dataType: "json",
            type: "POST",
            data: {
                content: content,
                articleId: articleId
            },
            success: function (data) {
                that.createCommentBlock(data.srcOfMemberAvatar, data.memberEmail, data.publishDate, content);
                $("#add-comment-content").val("");
                var numOfComments = parseInt($(".flag span").text()[0]);
                $(".flag span").empty();
                $(".flag span").text((numOfComments + 1)+" Comments");
            },
            error: function (data) {
                alert(data.error);
            }
        });
    }

    that.createCommentBlock = function (srcOfMemberAvatar, memberEmail, publishDate, content) {
        var li = document.createElement("li");
        $(li).addClass("media");

        var a = document.createElement("a");
        $(a).addClass("pull-left");
        var img = document.createElement("img");
        $(img).addClass("media-object");
        $(img).attr("src", srcOfMemberAvatar);
        $(a).append(img);

        var div = document.createElement("div");
        $(div).addClass("media-body");

        var h4 = document.createElement("h4");
        $(h4).addClass("media-heading");

        var spanForMemberEmail = document.createElement("span");
        $(spanForMemberEmail).addClass("comment-user-name");
        $(spanForMemberEmail).text(memberEmail);

        var spanForDate = document.createElement("span");
        $(spanForDate).addClass("comment-date");
        $(spanForDate).text(publishDate);

        $(h4).append(spanForMemberEmail);
        $(h4).append(spanForDate);

        var p = document.createElement("p");
        $(p).text(content);

        $(div).append(h4);
        $(div).append(p);

        $(".media-list").append(li);

        $(li).append(a);
        $(li).append(div);
    }


};

$(function () {
    var articlePage = new ArticlePage();
    articlePage.initPage();
});
