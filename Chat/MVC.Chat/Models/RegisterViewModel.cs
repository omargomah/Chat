using System.ComponentModel.DataAnnotations;

namespace MVC.Chat.Models
{
    public class RegisterViewModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required,StringLength(Constants.NameMaxLength,MinimumLength =Constants.NameMinLength)]
        public string FName { get; set; }
        [Required,StringLength(Constants.NameMaxLength,MinimumLength =Constants.NameMinLength)]
        public string LName { get; set; }
        [Required,StringLength(30)]
        public string Password { get; set; }
        [Required,StringLength(30),Compare(nameof(Password),ErrorMessage ="Two Passwords are Not Matched")]
        public string ConfirmPassword { get; set; }
        [Required,Phone]
        public string PhoneNumber { get; set; }
    }
}
