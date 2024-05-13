using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
new HttpClient
{
    BaseAddress = new Uri("https://localhost:7002;http://localhost:5178")
});

await builder.Build().RunAsync();
