using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Client.Models
{
    public class Movie
    {
        public long? MovieID { get; set; }

        [Required(ErrorMessage = "Введіть назву фільму")]
        [StringLength(200, ErrorMessage = "Назва не може перевищувати 200 символів")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть опис фільму")]
        [StringLength(1000, ErrorMessage = "Опис не може перевищувати 1000 символів")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть жанр")]
        [StringLength(50, ErrorMessage = "Жанр не може перевищувати 50 символів")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть ціну квитка")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Ціна має бути додатною")]
        public decimal TicketPrice { get; set; }

        public long? CinemaHallID { get; set; }
        public CinemaHall? CinemaHall { get; set; }
    }
}
