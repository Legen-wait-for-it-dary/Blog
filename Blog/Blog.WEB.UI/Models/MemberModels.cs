using System.ComponentModel.DataAnnotations;

namespace Blog.WEB.UI.Models
{

    public class LoginMemberModel
    {
        [Required]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Irregular email")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RegisterMemberModel
    {
        [Required]
        [RegularExpression(@"^(([^<>()\[\]\\.,;:\s@']+(\.[^<>()\[\]\\.,;:\s@']+)*)|('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Irregular email")]
        public string Email { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Passwords not equals")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword { get; set; }
    }

    public class MemberModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }        
    }
}