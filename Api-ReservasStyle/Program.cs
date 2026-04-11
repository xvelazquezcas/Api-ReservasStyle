using Aplicacion_ReservasStyle.Interfaces;
using Aplicacion_ReservasStyle.Servicios;
using Dominio_ReservasStyle.Interfaces;
using Infraestructura_ReservasStyle;
using Infraestructura_ReservasStyle.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; 

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// INFRAESTRUCTURA-USUARIO-REPOSITORIES
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// INFRAESTRUCTURA-CITA-REPOSITORIES
builder.Services.AddScoped<ICitaRepository, CitaRepository>();

// INFRAESTRUCTURA-SERVICIOS-REPOSITORIES
builder.Services.AddScoped<IServicioRepository, ServicioRepository>();

// INFRAESTRUCTURA-EMPLEADO-REPOSITORIES
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();

// INFRAESTRUCTURA-PAGO-REPOSITORIES
builder.Services.AddScoped<IPagoRepository, PagoRepository>();

// APLICACION-CITA-SERVICIOS
builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();

// APLICACION-USUARIO-SERVICIOS
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// APLICACION-CITA-SERVICIOS
builder.Services.AddScoped<ICitaService, CitaService>();

builder.Services.AddDbContext<AplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();