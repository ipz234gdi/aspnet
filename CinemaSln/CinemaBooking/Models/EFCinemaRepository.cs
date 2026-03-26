using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Models
{
    public class EFCinemaRepository : ICinemaRepository
    {
        private CinemaDbContext context;

        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Movie> Movies => context.Movies.Include(m => m.CinemaHall);
        public IQueryable<CinemaHall> CinemaHalls => context.CinemaHalls;

        public void CreateMovie(Movie m)
        {
            context.Add(m);
            context.SaveChanges();
        }

        public void SaveMovie(Movie m)
        {
            context.SaveChanges();
        }

        public void DeleteMovie(Movie m)
        {
            context.Remove(m);
            context.SaveChanges();
        }

        public void CreateCinemaHall(CinemaHall h)
        {
            context.Add(h);
            context.SaveChanges();
        }

        public void SaveCinemaHall(CinemaHall h)
        {
            context.SaveChanges();
        }

        public void DeleteCinemaHall(CinemaHall h)
        {
            context.Remove(h);
            context.SaveChanges();
        }
    }
}