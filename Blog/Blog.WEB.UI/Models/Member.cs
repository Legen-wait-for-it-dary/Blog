using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.UI.Models
{
    public class Member
    {
        [Required(ErrorMessage = "Email не може бути пустим")]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Irregular email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password cannot be empty")]
        [StringLength(32, MinimumLength = 6)]
        [Compare("ConfirmedPassword", ErrorMessage = "Passwords not equals")]
        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string Avatar { get; set; }        
    }
}