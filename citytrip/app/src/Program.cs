using FP.ContainerTraining.CityTrip.App;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async context =>
{
    var content = ResourceLink.ReadEmbeddedTextFile("index.html");
    content = content.Replace("BASEURL", builder.Configuration["BaseUrl"]);
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(content);
});
app.Run();
