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
                        new Movie { Title = "Дюна: Частина друга", Description = "Продовження епічної саги Дені Вільньова. Пол Атрейдес об'єднується з фрименами для помсти тим, хто знищив його родину.", Genre = "Фантастика", TicketPrice = 250m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8b8R8l88Qje9dn9OE8PY05Nez7Y.jpg" },
                        new Movie { Title = "Дедпул і Росомаха", Description = "Найочікуваніший кросовер Marvel. Уейд Вілсон повертається з новим партнером — Логаном.", Genre = "Бойовик", TicketPrice = 200m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8cdWjvZQUExUUTzyp4t6EDMubfO.jpg" },
                        new Movie { Title = "Думками навиворіт 2", Description = "Райлі стає підлітком і в її голові з'являються нові емоції: Тривога, Заздрість, Нудьга та Сором.", Genre = "Мультфільм", TicketPrice = 180m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/vpnVM9B6NMmQpWeZvzLvDESb2QY.jpg" },
                        new Movie { Title = "Оппенгеймер", Description = "Епічна біографічна драма Крістофера Нолана про фізика Роберта Оппенгеймера та створення атомної бомби.", Genre = "Біографія", TicketPrice = 220m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8Gxv8gSFCU0XGDykEGv7zR1n2ua.jpg" },
                        new Movie { Title = "Аватар: Шлях води", Description = "Джейк Саллі живе з новою сім'єю на Пандорі. Коли знайома загроза повертається, він змушений битися з людьми.", Genre = "Фантастика", TicketPrice = 270m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/t6HIqrRAclMCA60NsSmeqe9RmNV.jpg" },
                        new Movie { Title = "Джон Вік 4", Description = "Легендарний кілер Джон Вік знаходить шлях до перемоги над Високим Столом, але ціна може бути надто високою.", Genre = "Бойовик", TicketPrice = 210m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/vZloFAK7NmvMGKE7LsyBGSME9Yd.jpg" },
                        new Movie { Title = "Елементарно", Description = "У місті, де мешкають елементи вогню, води, землі та повітря, палка Ембер і спокійний Вейд відкривають, що вони мають більше спільного, ніж здається.", Genre = "Мультфільм", TicketPrice = 160m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/6oH3DpyGNjItEbqhIBj0hgjiCr.jpg" },
                        new Movie { Title = "Наполеон", Description = "Грандіозна історія французького імператора Наполеона Бонапарта — від сходження до влади до поразки при Ватерлоо.", Genre = "Біографія", TicketPrice = 230m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/jE5skGMI99hEJdFCdYkiWDBGJDH.jpg" }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
