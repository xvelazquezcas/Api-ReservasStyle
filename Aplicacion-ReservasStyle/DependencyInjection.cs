using Aplicacion_ReservasStyle.Interfaces;
using Aplicacion_ReservasStyle.Mappings;
using Aplicacion_ReservasStyle.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacion_ReservasStyle;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(SucursalMappingProfile));

        // Servicios de Aplicación
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISucursalService, SucursalService>();
        services.AddScoped<IEmpleadoService, EmpleadoService>();

        return services;
    }
}
