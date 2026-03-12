using Microsoft.EntityFrameworkCore;
using CinemaBooking.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<CinemaDbContext>(opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:CinemaConnection"]);
});

builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
