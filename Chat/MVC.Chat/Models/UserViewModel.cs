using MVC.Chat.Entities;
using MVC.Chat.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace MVC.Chat.Models
{
    public class UpdateImageOfUserViewModel
    {
        [Required]
        public IFormFile Image { get; set; }
    }
    public class UpdateNameDataViewModel
    { 
        [Required, StringLength(Constants.NameMaxLength, MinimumLength = Constants.NameMinLength)]
        public string FName { get; set; }

        [Required, StringLength(Constants.NameMaxLength, MinimumLength = Constants.NameMinLength)]
        public string LName { get; set; }
    }
    public class UserViewModel
    {
        public string FName { get; set; }

        public string LName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public string? PictureUrl { get; set; }

        public static explicit operator UserViewModel(User user) => 
            new UserViewModel() 
            {
                Email = user.Email,
                FName = user.FName,
                LName = user.LName,
                Phone = user.PhoneNumber,
                PictureUrl = user.Picture?.HasValue()?? false ? user.Picture : "~/Images/Default-Image.jpg"
            };
    }
}
