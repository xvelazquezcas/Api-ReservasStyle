using System.Reflection;
using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<ServicioSucursal> ServicioSucursales { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<HorariosDisponibles> HorariosDisponibles { get; set; }
        public DbSet<HorarioLocal> HorariosLocales { get; set; }
        public DbSet<Comprobantes> Comprobantes { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Promociones> Promociones { get; set; }
        public DbSet<ServicioPromocion> ServicioPromociones { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<Empleado>().HasKey(e => e.IdEmpleado);
            // modelBuilder.Entity<HorariosDisponibles>().HasKey(h => h.IdHorario); // Revisa si se llama IdHorario en tu entidad
            // modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            // modelBuilder.Entity<Servicio>().HasKey(s => s.IdServicio);
            // modelBuilder.Entity<Pago>().HasKey(p => p.IdPago);
            // modelBuilder.Entity<ServicioLocal>().HasKey(sl => sl.IdServicioLocal);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
