using Aplicacion_ReservasStyle.Interfaces;
using Aplicacion_ReservasStyle.Mappings;
using Aplicacion_ReservasStyle.Servicios;
//using Aplicacion_ReservasStyle.Servicios.Aplicacion_ReservasStyle.Servicios;
using Microsoft.Extensions.DependencyInjection;

namespace Aplicacion_ReservasStyle;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(SucursalMappingProfile));
        services.AddAutoMapper(typeof(HorarioLocalMappingProfile));
        services.AddAutoMapper(typeof(ComprobantesMappingProfile));
        services.AddAutoMapper(typeof(HorariosDisponiblesMappingProfile));
        services.AddAutoMapper(typeof(NotificacionesMappingProfile));
        services.AddAutoMapper(typeof(PagoMappingProfile));
        services.AddAutoMapper(typeof(PromocionesMappingProfile));
        services.AddAutoMapper(typeof(ServicioPromocionMappingProfile));
        services.AddAutoMapper(typeof(ServicioSucursalMappingProfile));
        services.AddAutoMapper(typeof(LogMappingProfile));

        // Servicios de Aplicación
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ICitaService, CitaService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ISucursalService, SucursalService>();
        services.AddScoped<IEmpleadoService, EmpleadoService>();
        services.AddScoped<IServicioService, ServicioService>();
        services.AddScoped<IHorarioLocalService, HorarioLocalService>();
        services.AddScoped<IComprobantesService, ComprobantesService>();
        services.AddScoped<IHorariosDisponiblesService, HorariosDisponiblesService>();
        services.AddScoped<INotificacionesService, NotificacionesService>();
        services.AddScoped<IPagoService, PagoService>();
        services.AddScoped<IPromocionesService, PromocionesService>();
        services.AddScoped<IServicioPromocionService, ServicioPromocionService>();
        services.AddScoped<IServicioSucursalService, ServicioSucursalService>();
        services.AddScoped<ILogService, LogService>();

        return services;
    }
}
