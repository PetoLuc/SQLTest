using HR.Dal.Repos;
using HR.Dal.Repos.Contracts;
using HR.Dal.Services;
using HR.Dal.Services.Contracts;
using HR.Web.Blazor.Components;
using HR.Web.Blazor.ViewModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IConnectionStringProviderService, ConnectionStringProviderService>();
builder.Services.AddScoped<ICestovnyPrikazRepository, CestovnyPrikazRepository>();
builder.Services.AddScoped<IDopravaRepository, DopravaTypeRepository>(); 

builder.Services.AddScoped<HomeViewModel>();
builder.Services.AddTransient<AddCestovnyPrikazViewModel>();


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
