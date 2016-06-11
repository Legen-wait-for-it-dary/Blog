var SignUpPage = function () {

    var that = this;
	
    this.initPage = function () {
        $("#sign-up-link").on("click", this.signUpLinkClick);
		$(".sign-in input").on("change", 
			function(){
				that.signInInputChange($(this));
			}
		);
		$(".sign-up input").on("change", 
			function(){
				that.signInInputChange($(this));
			}
		);
		$("form.sign-in").on("submit", this.signInFormSubmited);
        $("form.sign-up").on("submit", this.signUpFormSubmited);
    };
	
	this.signUpLinkClick = function () {
        $("#sign-in").hide();
        $("#sign-up").show();
    };
	
	this.signInInputChange = function(element){
		if ($(element).attr('data-val') == "true") {
			if ($(element).attr('data-val-regex-pattern') != undefined) {
                that.testPattern($(element));
            }
        } 
    };
	
    this.testPattern = function (element) {
		var idOfTestedInput = "#" + $(element).attr('id');
		var pattern = new RegExp($(element).attr('data-val-regex-pattern'));
        console.log(pattern);
		var data = $(element).val();
        var errorMessage = "Incorrect data";

        if (pattern.test(data)) {
			console.log("true");
			$(idOfTestedInput+"-error").hide()
										   .text("");
        } else {
			console.log("error");
			$(idOfTestedInput+"-error").show()
										   .text(errorMessage);
        }
    };
	
    this.signUpFormSubmited = function () {
        if(that.isUserDataValid("#sign-up")){
			console.log("User data valid");
		} else {
			console.log("User data invalid");
		}
    };
	
	this.signInFormSubmited = function () {
        if(that.isUserDataValid("#sign-up")){
			console.log("User data valid");
		} else {
			console.log("User data invalid");
		}
    };
	
	this.isUserDataValid = function(formId){
		var errorDivs = $(formId + " .alert-danger");
		for(var i=0; i < errorDivs.length; i++){
			if($(errorDivs[i]).css("display") != "none"){
				return false;
			}
		}
		return true;
	}	

    


};

$(function () {
    var signUpPage = new SignUpPage();
    signUpPage.initPage();

});
