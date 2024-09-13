using System.ComponentModel.DataAnnotations;

namespace Task.ModelViews
{
    public class RegisterViewModel
    {
        [MinLength(3,ErrorMessage = "The minimum length of First Name is 3")]
        [MaxLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;
        [MinLength(3, ErrorMessage = "The minimum length of Last Name is 3")]
		[MaxLength(10)]
		[Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;
        [RegularExpression(@"^\S*$", ErrorMessage = "Username must not contain spaces.")]
		[MinLength(5, ErrorMessage = "The minimum length of Username is 5")]
        [MaxLength(15)]
		public string UserName { get; set; } = string.Empty;
        [Display(Name ="Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
