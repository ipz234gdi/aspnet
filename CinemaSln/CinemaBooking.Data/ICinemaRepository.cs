using CinemaBooking.Data.Models;

namespace CinemaBooking.Data
{
    public interface ICinemaRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<CinemaHall> CinemaHalls { get; }

        void CreateMovie(Movie m);
        void SaveMovie(Movie m);
        void DeleteMovie(Movie m);

        void CreateCinemaHall(CinemaHall h);
        void SaveCinemaHall(CinemaHall h);
        void DeleteCinemaHall(CinemaHall h);
    }
}
