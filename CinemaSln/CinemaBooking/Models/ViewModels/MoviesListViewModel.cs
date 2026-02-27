using System.Collections.Generic;
using System.Linq;

namespace CinemaBooking.Models.ViewModels
{
    public class MoviesListViewModel
    {
        public IEnumerable<Movie> Movies { get; set; } = Enumerable.Empty<Movie>();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}