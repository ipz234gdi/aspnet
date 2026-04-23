using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Client.Models
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; } = string.Empty;
    }
}
