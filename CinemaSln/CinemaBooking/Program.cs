using Microsoft.EntityFrameworkCore;
using CinemaBooking.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CinemaDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:CinemaConnection"]);
});

builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
