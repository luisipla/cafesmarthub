using CafeSmartHub.Client;
using CafeSmartHub.Client.Auth;
using CafeSmartHub.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//  Base URL de la API 
var apiBaseUrl = builder.Configuration["Api:BaseUrl"];
if (string.IsNullOrWhiteSpace(apiBaseUrl))
{
    apiBaseUrl = builder.HostEnvironment.BaseAddress;
}

if (!apiBaseUrl.EndsWith("/"))
    apiBaseUrl += "/";

//  Auth 
builder.Services.AddScoped<TokenStorage>();
builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());
builder.Services.AddAuthorizationCore();

// Handler que agrega Authorization: Bearer 
builder.Services.AddTransient<AuthHeaderHandler>();

// HttpClient con handler 
// Este HttpClient será el que usan CategoriasApi  ProductosApi AlertasApi
builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<AuthHeaderHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(apiBaseUrl)
    };
});

// APIs existentes
builder.Services.AddScoped<CategoriasApi>();
builder.Services.AddScoped<ProductosApi>();
builder.Services.AddScoped<AlertasApi>();

// AuthApi (login)
builder.Services.AddScoped<AuthApi>();

await builder.Build().RunAsync();
