using CinemaBooking.Data.Models;

namespace CinemaBooking.Data.ViewModels
{
    public class MoviesListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } = Enumerable.Empty<Movie>();
        public PagingInfo PagingInfo { get; set; } = new();
        public string? CurrentGenre { get; set; }
    }
}
