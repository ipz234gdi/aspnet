using Microsoft.EntityFrameworkCore;

namespace CinemaBooking.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            CinemaDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<CinemaDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Movies.Any())
            {
                context.Movies.AddRange(
                    new Movie { Title = "Дюна: Частина друга", Description = "Продовження епічної саги.", Genre = "Фантастика", TicketPrice = 250m },
                    new Movie { Title = "Дедпул і Росомаха", Description = "Нові пригоди улюблених героїв.", Genre = "Бойовик", TicketPrice = 200m },
                    new Movie { Title = "Думками навиворіт 2", Description = "Нові емоції в голові Райлі.", Genre = "Мультфільм", TicketPrice = 180m },
                    new Movie { Title = "Оппенгеймер", Description = "Історія створення атомної бомби.", Genre = "Біографія", TicketPrice = 220m },
                    new Movie { Title = "Аватар: Шлях води", Description = "Повернення на Пандору.", Genre = "Фантастика", TicketPrice = 270m },
                    new Movie { Title = "Джон Вік 4", Description = "Фінальний розділ легендарного кілера.", Genre = "Бойовик", TicketPrice = 210m },
                    new Movie { Title = "Елементарно", Description = "Історія дружби вогню та води.", Genre = "Мультфільм", TicketPrice = 160m },
                    new Movie { Title = "Наполеон", Description = "Епічна історія французького імператора.", Genre = "Біографія", TicketPrice = 230m }
                );
                context.SaveChanges();
            }
        }
    }
}