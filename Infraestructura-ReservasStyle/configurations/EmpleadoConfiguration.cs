using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("Empleados");

            // 2. Llave Primaria (Obligatorio porque usas IdEmpleado)
            builder.HasKey(e => e.IdEmpleado);

            // 3. Configuración de Propiedades

            // IdUsuario e IdLocal suelen ser llaves foráneas (Foreign Keys)
            builder.Property(e => e.IdUsuario)
                .IsRequired();

            builder.Property(e => e.IdSucursal)
                .IsRequired();

            builder.Property(e => e.Especialidad)
                .HasMaxLength(150) // varchar(150)
                .IsRequired(false); // Permite nulos (string?)

            builder.Property(e => e.Estado)
                .IsRequired()
                .HasDefaultValue(true); // Por defecto, el empleado está activo

            // 4. Índices (Opcional pero recomendado)
            // Es buena idea indexar las llaves foráneas para mejorar la velocidad de búsqueda
            builder.HasIndex(e => e.IdUsuario);
            builder.HasIndex(e => e.IdSucursal);
        }
    }
}