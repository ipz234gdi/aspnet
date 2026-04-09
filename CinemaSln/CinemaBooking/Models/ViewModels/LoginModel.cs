using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = "/";
    }
}
