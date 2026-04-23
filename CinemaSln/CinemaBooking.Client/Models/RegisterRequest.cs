using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Client.Models
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Введіть ім'я користувача")]
        [StringLength(50, ErrorMessage = "Ім'я не може перевищувати 50 символів")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть електронну пошту")]
        [EmailAddress(ErrorMessage = "Невірний формат електронної пошти")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть пароль")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Пароль має містити від 8 до 100 символів")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
