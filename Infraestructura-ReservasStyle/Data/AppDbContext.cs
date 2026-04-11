// using Dominio_ReservasStyle.Entities;
// using Microsoft.EntityFrameworkCore;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Infraestructura_ReservasStyle
// {
//     public class AplicationDbContext : DbContext
//     {
//         public AplicationDbContext(DbContextOptions<AplicationDbContext> options)
//             : base(options)
//         {
//         }

//         public DbSet<Usuario> Usuarios { get; set; }

//         public DbSet<Citas> Citas { get; set; }

//         public DbSet<Servicio> Servicios { get; set; }

//         public DbSet<Pago> Pagos { get; set; }

//         public DbSet<Empleado> Empleados { get; set; }
//         public DbSet<HorariosDisponibles> HorariosDisponibles {  get; set; }
//     }
// }