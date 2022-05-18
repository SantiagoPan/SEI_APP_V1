using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SEI_APP.DTOs
{
    public class UserDto : IdentityUser
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
