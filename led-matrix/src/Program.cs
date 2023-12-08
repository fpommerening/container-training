using FP.ContainerTraining.RaspiLedMatrix;
using FP.ContainerTraining.RaspiLedMatrix.Business;
using FP.ContainerTraining.RaspiLedMatrix.Services;
using Iot.Device.Max7219;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MatrixRepository>();
builder.Services.AddSingleton<MatrixRunner>();
builder.Services.AddHostedService<TextWriterService>();

var app = builder.Build();

app.MapPut("/text", (
    [FromBody] TextRequest request,
    [FromServices] MatrixRepository repository) =>
{
    repository.Graphic = string.Empty;
    repository.Text = request.Text;
    repository.Rotation = RotationType.Left;
    return Task.FromResult(Results.Accepted());
});

app.MapPut("/graphics/{val}", (
    [FromRoute] string val,
    [FromServices] MatrixRepository repository) =>
{
    repository.Text = string.Empty;
    repository.Graphic = val;
    repository.Rotation = RotationType.Left;
    return Task.FromResult(Results.Accepted());
});

app.MapGet("/color", ([FromServices] MatrixRepository repository) =>
{
    return Task.FromResult(repository.Color.ToString());
});
    
app.MapPut("/color/{val}", (
    [FromRoute] string val,
    [FromServices] MatrixRepository repository) =>
{
    if (!Enum.TryParse(typeof(Color), val, true, out var color))
    {
        return Results.BadRequest($"Color {val} is not supported");
    }
    repository.Color = (Color)color;
    return Results.Accepted();
});

app.MapPut("/rotation/{val}", (
    [FromRoute] string val,
    [FromServices] MatrixRepository repository) =>
{
    if (!Enum.TryParse(typeof(RotationType), val, true, out var rotation))
    {
        return Results.BadRequest($"RotationType {val} is not supported");
    }
    repository.Rotation = (RotationType)rotation;
    return Results.Accepted();
});

var runner = app.Services.GetRequiredService<MatrixRunner>();
runner.Run();

app.Run();

