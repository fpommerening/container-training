using FP.ContainerTraining.Healthcheck.Business;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHealthRepo, HealthRepo>();

var app = builder.Build();

app.MapGet("/", (IHealthRepo healthRepo) =>
        $"The Application is {(healthRepo.IsHealthy ? "healthy" : "sick")} - last update {healthRepo.LastChangeUtc:G}");

app.MapPost("/sick", (IHealthRepo healthRepo) =>
{
    healthRepo.IsHealthy = false;
    return Results.Accepted();
});

app.MapPost("/healthy", (IHealthRepo healthRepo) =>
{
    healthRepo.IsHealthy = true;
    return Results.Accepted();
});

app.Map("/api/healthcheck", (IHealthRepo healthRepo) =>
    healthRepo.IsHealthy ? Results.Ok() : Results.Problem(statusCode:500));
app.Run();
