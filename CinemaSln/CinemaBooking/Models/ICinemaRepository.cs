namespace CinemaBooking.Models
{
    public interface ICinemaRepository
    {
        IQueryable<Movie> Movies { get; }
    }
}