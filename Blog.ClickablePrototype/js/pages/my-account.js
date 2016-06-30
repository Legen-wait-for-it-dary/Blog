var AccountPage = function() {

    var that = this;

    this.initPage = function() {
        $('#show-menu-btn').on('click', function(event) {
            if ($('.site-nav > ul').css('display') == 'block') {
                $('.site-nav > ul').hide();
            } else {
                $('.site-nav > ul').show();
            }
        });
        $(window).on('scroll', this.windowScrolled);

        $("#add-new-post,#my-posts,#rough-copies,#change-email,#change-password,#change-user-photo")
            .on("click",
                function() {
                    $(".my-account-mng li").removeClass("selected");
                    $(this).addClass("selected");
                    that.showMyAccountBlock($(this).attr("id"));
                });

        $("#publish-article-btn").on("click", this.publishBtnClick);

        $("#entry-image-input").on("change", this.previewFile);


        $("#my-posts").on("click", this.myPostsClick);
        $("#rough-copies").on("click", function(){

        });
        $(".btn-group").on("click",".update-btn" ,function(){
            console.log("here");
            $(".my-posts > * ").remove();    
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

    this.windowScrolled = function() {
        if ($(window).scrollTop() > 30) {
            $('nav.site-nav').removeClass('higgerNarrower');
            $('nav.site-nav').addClass('shorterWidder');
        } else {
            $('nav.site-nav').removeClass('shorterWidder');
            $('nav.site-nav').addClass('higgerNarrower');
        }
    };

    this.myPostsClick = function() {
        var data = {"6":"Title","7":"fewfew","8":"ewefwwe","9":"3333","10":"vevwwev"};

        $(".my-posts > * ").remove();  
        var allPropertyNames = Object.keys(data);
        console.log(allPropertyNames.length);
        for (var i = 0; i < allPropertyNames.length; i++) {
            that.createDOMElement(allPropertyNames[i],data[allPropertyNames[i]]);
        };

        
    };

    this.createDOMElement = function (articleId,articleTitle){
        var inputGroupDiv = document.createElement("div");
        $(inputGroupDiv).addClass("input-group");
        
        var a = document.createElement("a");
        $(a).addClass("list-group-item");
        $(a).text(articleTitle);
        $(a).attr("href","/Article/Index/"+articleId);
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
                    .attr("art-id",articleId);

        var deleteBtn = document.createElement("button");
        $(deleteBtn).addClass("btn btn-default delete-btn")
                    .text("Delete")
                    .attr("art-id",articleId);

        $(deleteBtn).on('click', function(event) {
            console.log("effefe");
        });

        $(updateBtnGroup).append(updateBtn)
        $(deleteBtnGroup).append(deleteBtn);

        $(inputGroupBtn)
                        .append(updateBtnGroup)
                        .append(deleteBtnGroup);

        $(inputGroupDiv).append(inputGroupBtn);

        $(".my-posts").append(inputGroupDiv);
    }



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

        if ($("#empty-title-error-msg").length) {
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

        if ($("#empty-content-error-msg").length) {
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

    tinymce.init({ selector: 'textarea', plugins: "code image textcolor advlist" , height : "480"});
});
