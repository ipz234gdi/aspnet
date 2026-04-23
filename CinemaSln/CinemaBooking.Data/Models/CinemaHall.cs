using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Data.Models
{
    public class CinemaHall
    {
        public long? CinemaHallID { get; set; }

        [Required(ErrorMessage = "Введіть назву залу")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть кількість рядів")]
        [Range(1, 50, ErrorMessage = "Кількість рядів має бути від 1 до 50")]
        public int Rows { get; set; } = 10;

        [Required(ErrorMessage = "Введіть кількість місць у ряді")]
        [Range(1, 100, ErrorMessage = "Кількість місць у ряді має бути від 1 до 100")]
        public int SeatsPerRow { get; set; } = 10;

        // Custom validation to limit total capacity to 300
        [Range(1, 300, ErrorMessage = "Загальна кількість місць (Ряди × Місця в ряді) не може перевищувати 300")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Введіть розташування залу")]
        [StringLength(200, ErrorMessage = "Розташування не може перевищувати 200 символів")]
        public string Location { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
