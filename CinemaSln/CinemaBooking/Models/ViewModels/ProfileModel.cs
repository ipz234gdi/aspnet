using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models.ViewModels
{
    public class ProfileModel
    {
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть email")]
        [EmailAddress(ErrorMessage = "Невірний формат email")]
        public string Email { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }

        public IList<string> Roles { get; set; } = new List<string>();
    }
}
