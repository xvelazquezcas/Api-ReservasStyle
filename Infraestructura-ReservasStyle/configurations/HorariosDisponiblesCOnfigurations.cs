using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class HorariosDisponiblesConfiguration : IEntityTypeConfiguration<HorariosDisponibles>
    {
        public void Configure(EntityTypeBuilder<HorariosDisponibles> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("HorariosDisponibles");

            // 2. Llave Primaria
            builder.HasKey(h => h.IdHorario);

            // 3. Configuración de Propiedades

            builder.Property(h => h.IdEmpleado)
                .IsRequired();

            // Configuración del Enum (DiaSemana)
            // Es recomendable guardarlo como string para que en la BD sea legible (Lunes, Martes...)
            // Si prefieres números (0, 1, 2...), quita el .HasConversion<string>()
            builder.Property(h => h.DiaSemana)
                .HasConversion<string>() 
                .HasMaxLength(20)
                .IsRequired();

            // TimeSpan se mapea automáticamente a "time" en PostgreSQL
            builder.Property(h => h.HoraInicio)
                .IsRequired();

            builder.Property(h => h.HoraFin)
                .IsRequired();

            // 4. Índices (Para búsquedas rápidas por empleado)
            builder.HasIndex(h => h.IdEmpleado);

            // 5. Relación (Opcional, si tienes la entidad Empleado configurada)
            // builder.HasOne<Empleado>()
            //        .WithMany()
            //        .HasForeignKey(h => h.IdEmpleado)
            //        .OnDelete(DeleteBehavior.Cascade); // Si se borra el empleado, se borran sus horarios
        }
    }
}