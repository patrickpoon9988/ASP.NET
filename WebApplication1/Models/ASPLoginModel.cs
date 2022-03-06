using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ASPLoginModel
    {
        [Required]
        [Display(Name = "Account")]
        public string userName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string password { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "This is not a valid email address")]
        public string email { get; set; }

        //[Required]
        //[Display(Name ="Phone Number")]
        //public string phoneNumber { get; set; }
    }
}