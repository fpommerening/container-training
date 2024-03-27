using FP.ContainerTraining.Messages.Data;
using FP.ContainerTraining.Messages.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Configuration.AddEnvironmentVariables();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseAntiforgery();
app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
