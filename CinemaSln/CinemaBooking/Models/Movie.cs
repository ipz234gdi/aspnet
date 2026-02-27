using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaBooking.Models
{
    public class Movie
    {
        public long? MovieID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        [Column(TypeName = "decimal(8, 2)")]
        public decimal TicketPrice { get; set; }
    }
}