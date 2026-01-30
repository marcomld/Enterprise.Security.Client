using Enterprise.Security.Client;
using Enterprise.Security.Client.Core.Common;
using Enterprise.Security.Client.Core.Interfaces;
using Enterprise.Security.Client.Infrastructure.Auth;
using Enterprise.Security.Client.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. Configurar HttpClient para apuntar a TU API (ajusta el puerto según tu launchSettings del backend)
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7213") });

// 2. Registrar nuestro LocalStorage Nativo
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();

// 3. Registrar el Sistema de Autenticación y Políticas
builder.Services.AddAuthorizationCore(options =>
{
    // Registramos dinámicamente una Policy por cada Permiso que definimos
    foreach (var permission in Permissions.GetAll())
    {
        options.AddPolicy(permission, policy =>
            policy.RequireClaim("permission", permission));
    }
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Tip: Hacemos accesible la clase concreta también para poder llamar a LoginAsync/LogoutAsync
builder.Services.AddScoped<CustomAuthenticationStateProvider>(sp =>
    (CustomAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IAuditLogService, AuditLogService>();

await builder.Build().RunAsync();
