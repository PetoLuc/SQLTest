using HR.Dal.Contracts;
using HR.Dal.Repos;
using HR.Dal.Services;
using HR.Web.Blazor.Components;
using HR.Web.Blazor.ViewModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConnectionStringProviderService, ConnectionStringProviderService>();
builder.Services.AddScoped<ICestovnyPrikazRepository, CestovnyPrikazRepository>();
builder.Services.AddScoped<HomeViewModel>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
