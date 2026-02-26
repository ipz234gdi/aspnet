var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

int requestCount = 0;

app.Use(async (context, next) =>
{
    Console.WriteLine($"[Логер] Метод: {context.Request.Method} | Шлях: {context.Request.Path}");
    
    await next.Invoke(); 
});

app.Use(async (context, next) =>
{
    const string VALID_API_KEY = "SecretLabKey2026"; 

    if (!context.Request.Headers.TryGetValue("X-API-KEY", out var providedKey) || providedKey != VALID_API_KEY)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsync("403 Forbidden: Invalid or missing API Key.");
        return;
    }

    await next.Invoke();
});

app.Use(async (context, next) =>
{
    if (context.Request.Query.ContainsKey("custom") && context.Request.Query["custom"] == "true")
    {
        await context.Response.WriteAsync("You've hit a custom middleware!");
        return;
    }

    await next.Invoke();
});

app.Run(async context =>
{
    requestCount++;
    await context.Response.WriteAsync($"The amount of processed requests is {requestCount}.");
});

app.Run();