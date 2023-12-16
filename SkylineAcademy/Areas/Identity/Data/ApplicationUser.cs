using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace SkylineAcademy.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]

        public string? FName { get; set; }

        public string? Lname { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
