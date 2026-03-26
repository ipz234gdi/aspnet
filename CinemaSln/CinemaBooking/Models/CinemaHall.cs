using System.ComponentModel.DataAnnotations;

namespace CinemaBooking.Models
{
    public class CinemaHall
    {
        public long? CinemaHallID { get; set; }

        [Required(ErrorMessage = "Введіть назву залу")]
        [StringLength(100, ErrorMessage = "Назва не може перевищувати 100 символів")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введіть місткість залу")]
        [Range(1, 1000, ErrorMessage = "Місткість має бути від 1 до 1000")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Введіть розташування залу")]
        [StringLength(200, ErrorMessage = "Розташування не може перевищувати 200 символів")]
        public string Location { get; set; } = string.Empty;

        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}
