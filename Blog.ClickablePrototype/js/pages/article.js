var ArticlePage = function () {
    var that = this;

    this.initPage = function () {
        $("#add-comment").on("click", this.addCommentClick);
    };

    this.addCommentClick = function () {
        var content = $("#add-comment-content").val();
        alert(window.location[0]);
        //createCommentBlock("src","avav@gmail.com","12-01-2016",content);
	};
	
	

};

$(function () {
    var articlePage = new ArticlePage();
    articlePage.initPage();

});


function createCommentBlock(srcOfMemberAvatar,memberEmail,publishDate,content){
	var li = document.createElement("li");
        $(li).addClass("media");
        
		var a = document.createElement("a");
		$(a).addClass("pull-left");
		var img = document.createElement("img");
		$(img).addClass("media-object");
		$(img).attr("class",srcOfMemberAvatar);
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
