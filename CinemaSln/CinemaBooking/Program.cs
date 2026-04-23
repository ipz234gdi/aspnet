using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CinemaBooking.Data;
using CinemaBooking.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddDbContext<CinemaDbContext>(opts => {
    opts.UseSqlite(builder.Configuration["ConnectionStrings:CinemaConnection"]);
});

builder.Services.AddDbContext<AppIdentityDbContext>(opts => {
    opts.UseSqlite(builder.Configuration["ConnectionStrings:IdentityConnection"]);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {
    opts.Password.RequiredLength = 8;
    opts.Password.RequireDigit = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireNonAlphanumeric = false;
    opts.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSignalR();

builder.Services.ConfigureApplicationCookie(opts => {
    opts.LoginPath = "/Account/Login";
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.MapHub<BookingHub>("/bookinghub");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app).Wait();

app.Run();
