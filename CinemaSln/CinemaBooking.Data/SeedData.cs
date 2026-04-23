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
                var hall1 = new CinemaHall { Name = "Зал 1", Rows = 10, SeatsPerRow = 10, Capacity = 100, Location = "1 поверх, ліве крило" };
                var hall2 = new CinemaHall { Name = "Зал 2", Rows = 12, SeatsPerRow = 15, Capacity = 180, Location = "1 поверх, праве крило" };
                var hall3 = new CinemaHall { Name = "IMAX", Rows = 15, SeatsPerRow = 20, Capacity = 300, Location = "2 поверх" };

                context.CinemaHalls.AddRange(hall1, hall2, hall3);
                context.SaveChanges();

                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(
                        new Movie { Title = "Дюна: Частина друга", Description = "Продовження епічної саги Дені Вільньова.", Genre = "Фантастика", TicketPrice = 250m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8b8R8l88Qje9dn9OE8PY05Nez7Y.jpg", ShowDate = new DateTime(2026, 4, 24, 14, 0, 0) },
                        new Movie { Title = "Дедпул і Росомаха", Description = "Найочікуваніший кросовер Marvel.", Genre = "Бойовик", TicketPrice = 200m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8cdWjvZQUExUUTzyp4t6EDMubfO.jpg", ShowDate = new DateTime(2026, 4, 24, 17, 30, 0) },
                        new Movie { Title = "Думками навиворіт 2", Description = "Райлі стає підлітком і з'являються нові емоції.", Genre = "Мультфільм", TicketPrice = 180m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/vpnVM9B6NMmQpWeZvzLvDESb2QY.jpg", ShowDate = new DateTime(2026, 4, 24, 12, 0, 0) },
                        new Movie { Title = "Оппенгеймер", Description = "Епічна біографічна драма Крістофера Нолана.", Genre = "Біографія", TicketPrice = 220m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8Gxv8gSFCU0XGDykEGv7zR1n2ua.jpg", ShowDate = new DateTime(2026, 4, 24, 19, 0, 0) },
                        new Movie { Title = "Аватар: Шлях води", Description = "Джейк Саллі живе з новою сім'єю на Пандорі.", Genre = "Фантастика", TicketPrice = 270m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/t6HIqrRAclMCA60NsSmeqe9RmNV.jpg", ShowDate = new DateTime(2026, 4, 25, 18, 0, 0) },
                        new Movie { Title = "Джон Вік 4", Description = "Легендарний кілер Джон Вік знаходить шлях до перемоги.", Genre = "Бойовик", TicketPrice = 210m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/vZloFAK7NmvMGKE7LsyBGSME9Yd.jpg", ShowDate = new DateTime(2026, 4, 25, 21, 0, 0) },
                        new Movie { Title = "Елементарно", Description = "У місті, де мешкають елементи вогню, води, землі та повітря.", Genre = "Мультфільм", TicketPrice = 160m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/6oH3DpyGNjItEbqhIBj0hgjiCr.jpg", ShowDate = new DateTime(2026, 4, 25, 10, 0, 0) },
                        new Movie { Title = "Наполеон", Description = "Грандіозна історія французького імператора Наполеона Бонапарта.", Genre = "Біографія", TicketPrice = 230m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/jE5skGMI99hEJdFCdYkiWDBGJDH.jpg", ShowDate = new DateTime(2026, 4, 25, 16, 0, 0) },
                        new Movie { Title = "Людина-павук: Крізь Всесвіт", Description = "Майлз Моралес повертається у новому мультивсесвіті.", Genre = "Мультфільм", TicketPrice = 190m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/8Vt6mWEReuy4Of61Lnj5Xj704m8.jpg", ShowDate = new DateTime(2026, 4, 26, 12, 0, 0) },
                        new Movie { Title = "Вартові Галактики 3", Description = "Остання місія Пітера Квілла та його команди.", Genre = "Фантастика", TicketPrice = 210m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/r2J02Z2OpNTctfOSN1Ydgii51I3.jpg", ShowDate = new DateTime(2026, 4, 26, 15, 0, 0) },
                        new Movie { Title = "Форсаж 10", Description = "Дім Торетто стикається з найнебезпечнішим ворогом.", Genre = "Бойовик", TicketPrice = 200m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/fiVW06jE7z9YnO4trhaMEdclRVc.jpg", ShowDate = new DateTime(2026, 4, 26, 18, 30, 0) },
                        new Movie { Title = "Місія нездійсненна 7", Description = "Ітан Хант вирушає на нову місію з порятунку світу.", Genre = "Бойовик", TicketPrice = 230m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/NNxYkU70HPurnNCSiCjYAmacwm.jpg", ShowDate = new DateTime(2026, 4, 26, 21, 0, 0) },
                        new Movie { Title = "Барбі", Description = "Барбі залишає свій ідеальний світ і вирушає у реальний.", Genre = "Комедія", TicketPrice = 180m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/iuFNMS8U5cb6xfzi51Dbkovj7vM.jpg", ShowDate = new DateTime(2026, 4, 27, 13, 0, 0) },
                        new Movie { Title = "П'ять ночей у Фредді", Description = "Охоронець починає працювати в піцерії, де оживають аніматроніки.", Genre = "Жахи", TicketPrice = 170m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/A4j8S6moJS2zNtRR8oWF08gRnL5.jpg", ShowDate = new DateTime(2026, 4, 27, 20, 0, 0) },
                        new Movie { Title = "Супер Маріо Броз. У кіно", Description = "Пригоди братів Маріо та Луїджі у Грибному королівстві.", Genre = "Мультфільм", TicketPrice = 160m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/qNBAXBIQlnOPEes1tVvH94AWL33.jpg", ShowDate = new DateTime(2026, 4, 28, 11, 0, 0) },
                        new Movie { Title = "Вбивці квіткової повні", Description = "Драма Мартіна Скорсезе про розслідування ФБР в 1920-х.", Genre = "Драма", TicketPrice = 240m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/dB6Krk806zeie0Z1rvlU2bXvX4Z.jpg", ShowDate = new DateTime(2026, 4, 28, 19, 0, 0) },
                        new Movie { Title = "Русалонька", Description = "Ігрова адаптація класичного мультфільму Дісней.", Genre = "Фентезі", TicketPrice = 190m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/ym1dxyOk4jFcSl4Q2zmMBzx00YQ.jpg", ShowDate = new DateTime(2026, 4, 29, 14, 0, 0) },
                        new Movie { Title = "Трансформери: Час звіроботів", Description = "Нова епоха битв між автоботами та максималами.", Genre = "Фантастика", TicketPrice = 210m, CinemaHallID = hall3.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/gPbM0m8C8z43A1b7Q5nB5X4aB5v.jpg", ShowDate = new DateTime(2026, 4, 29, 17, 30, 0) },
                        new Movie { Title = "Індіана Джонс і реліквія долі", Description = "Остання пригода легендарного археолога.", Genre = "Пригоди", TicketPrice = 220m, CinemaHallID = hall1.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/p4T7bVj3F12N0g9jN8R3HGBB6k1.jpg", ShowDate = new DateTime(2026, 4, 30, 16, 0, 0) },
                        new Movie { Title = "Крід III", Description = "Адоніс Крід зустрічається з другом дитинства на рингу.", Genre = "Драма", TicketPrice = 180m, CinemaHallID = hall2.CinemaHallID, ImageUrl = "https://image.tmdb.org/t/p/w500/cvsXj3I9Q2iyyIOU1Q5YI4p1bX5.jpg", ShowDate = new DateTime(2026, 4, 30, 20, 30, 0) }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
