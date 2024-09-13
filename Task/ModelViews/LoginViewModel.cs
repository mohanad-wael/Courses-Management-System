using System.ComponentModel.DataAnnotations;

namespace Task.ModelViews
{
	public class LoginViewModel
	{
		[Display(Name = "Email Address")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; } = string.Empty;
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
		public bool RememberMe { get; set; }
	}
}
