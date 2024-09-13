using Microsoft.AspNetCore.Identity;

namespace Task.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
