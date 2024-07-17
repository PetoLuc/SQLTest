using HR.Dal.Contracts;
using HR.Dal.Repos;
using HR.Dal.Services;
using HR.Web.Blazor.WASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HR.Web.Blazor.WASM.Model.ViewModel;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<IConnectionStringProviderService, ConnectionStringProviderService>();
builder.Services.AddScoped<ICestovnyPrikazRepository, CestovnyPrikazRepository>();
builder.Services.AddScoped<HomeViewModel>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
