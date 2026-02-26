var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/who", () => "Денис Грушевицький");

app.MapGet("/time", () => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

app.Run();