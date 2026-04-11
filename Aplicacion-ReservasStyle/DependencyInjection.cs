using Aplicacion_ReservasStyle.Interfaces;
using Aplicacion_ReservasStyle.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacion_ReservasStyle;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Servicios de Aplicación
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
