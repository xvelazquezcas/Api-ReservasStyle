using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.configurations
{
    internal sealed class CitaConfigurations : IEntityTypeConfiguration<Citas>
    {
        public void Configure(EntityTypeBuilder<Citas> builder)
        {
            builder.ToTable("citas");
            builder.HasKey(c => c.IdCita);
            builder.HasOne<Usuario>().WithMany().HasForeignKey(c => c.IdCliente);
            builder.HasOne<Empleado>().WithMany().HasForeignKey(c => c.IdEmpleado);
            builder.HasOne<ServicioLocal>().WithMany().HasForeignKey(c => c.IdServicioLocal);
            builder.Property(c => c.Fecha).IsRequired().HasColumnType("timestamp with time zone")
                .HasColumnName("fecha");
            builder.Property(c => c.HoraInicio).IsRequired().HasColumnType("interval").HasColumnName("hora_inicio");
            builder.Property(c => c.HoraFin).IsRequired().HasColumnType("interval").HasColumnName("hora_fin");
            builder.Property(c => c.Estado).HasColumnName("estado");
            builder.Property(c => c.FechaCreacion).IsRequired().HasDefaultValueSql("now()")
                .HasColumnName("fecha_creacion");
        }
    }
}
