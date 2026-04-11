using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Interfaces;
using Infraestructura_ReservasStyle.Repositories;
using Infraestructura_ReservasStyle.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infraestructura_ReservasStyle;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // DbContext
        services.AddDbContext<AplicationDbContext>(options =>
            options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly("Infraestructura-ReservasStyle")));

        // Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ICitaRepository, CitaRepository>();
        services.AddScoped<IServicioRepository, ServicioRepository>();
        services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
        services.AddScoped<IPagoRepository, PagoRepository>();
        services.AddScoped<IHorariosRepository, HorariosRepository>();

        // Security
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? "your-secret-key-min-256bits"))
                };
            });

        services.AddAuthorization();

        return services;
    }
}
