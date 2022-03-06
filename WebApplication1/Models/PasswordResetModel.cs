using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PasswordResetModel
    {
        [Required]
        public string Account { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password Reset")]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}