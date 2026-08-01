using System.ComponentModel.DataAnnotations;

namespace MVC.Chat.Models
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }
    }
}
