using buibaquang_aspcoreblazor.Wasm;
using buibaquang_aspcoreblazor.Wasm.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
new HttpClient
{
    BaseAddress = new Uri("https://localhost:7002")
});

builder.Services.AddScoped<IProductApiClient, ProductApiClient>();
builder.Services.AddScoped<ICategoryApiClient, CategoryApiClient>();


await builder.Build().RunAsync();
