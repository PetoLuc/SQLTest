using HR.Dal.Repos;
using HR.Dal.Repos.Contracts;
using HR.Dal.Services;
using HR.Dal.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IConnectionStringProviderService, ConnectionStringProviderService>();
builder.Services.AddScoped<ICestovnyPrikazRepository, CestovnyPrikazRepository>();

builder.Services.AddRazorPages();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
