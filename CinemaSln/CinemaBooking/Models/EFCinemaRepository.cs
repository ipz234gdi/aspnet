namespace CinemaBooking.Models
{
    public class EFCinemaRepository : ICinemaRepository
    {
        private CinemaDbContext context;

        public EFCinemaRepository(CinemaDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Movie> Movies => context.Movies;
    }
}