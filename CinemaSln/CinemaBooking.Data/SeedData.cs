using CinemaBooking.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.Data
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

            if (!context.CinemaHalls.Any())
            {
                var hall1 = new CinemaHall { Name = "Зал 1", Capacity = 100, Location = "1 поверх, ліве крило" };
                var hall2 = new CinemaHall { Name = "Зал 2", Capacity = 150, Location = "1 поверх, праве крило" };
                var hall3 = new CinemaHall { Name = "IMAX", Capacity = 200, Location = "2 поверх" };

                context.CinemaHalls.AddRange(hall1, hall2, hall3);
                context.SaveChanges();

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new Movie { Title = "Дюна: Частина друга", Description = "Продовження епічної саги.", Genre = "Фантастика", TicketPrice = 250m, CinemaHallID = hall1.CinemaHallID },
                        new Movie { Title = "Дедпул і Росомаха", Description = "Нові пригоди улюблених героїв.", Genre = "Бойовик", TicketPrice = 200m, CinemaHallID = hall1.CinemaHallID },
                        new Movie { Title = "Думками навиворіт 2", Description = "Нові емоції в голові Райлі.", Genre = "Мультфільм", TicketPrice = 180m, CinemaHallID = hall2.CinemaHallID },
                        new Movie { Title = "Оппенгеймер", Description = "Історія створення атомної бомби.", Genre = "Біографія", TicketPrice = 220m, CinemaHallID = hall2.CinemaHallID },
                        new Movie { Title = "Аватар: Шлях води", Description = "Повернення на Пандору.", Genre = "Фантастика", TicketPrice = 270m, CinemaHallID = hall3.CinemaHallID },
                        new Movie { Title = "Джон Вік 4", Description = "Фінальний розділ легендарного кілера.", Genre = "Бойовик", TicketPrice = 210m, CinemaHallID = hall3.CinemaHallID },
                        new Movie { Title = "Елементарно", Description = "Історія дружби вогню та води.", Genre = "Мультфільм", TicketPrice = 160m, CinemaHallID = hall1.CinemaHallID },
                        new Movie { Title = "Наполеон", Description = "Епічна історія французького імператора.", Genre = "Біографія", TicketPrice = 230m, CinemaHallID = hall2.CinemaHallID }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
