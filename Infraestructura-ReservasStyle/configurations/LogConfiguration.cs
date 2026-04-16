using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(l => l.IdLog);
            
            builder.Property(l => l.NombreUsuario)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(l => l.Accion)
                .IsRequired()
                .HasMaxLength(50);
            
            builder.Property(l => l.Entidad)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(l => l.DetalleAntes)
                .HasMaxLength(2000);
            
            builder.Property(l => l.DetalleDepues)
                .HasMaxLength(2000);
            
            builder.Property(l => l.FechaRegistro)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
            builder.Property(l => l.DireccionIP)
                .HasMaxLength(45);
            
            builder.Property(l => l.UserAgent)
                .HasMaxLength(500);
            
            builder.Property(l => l.MensajeError)
                .HasMaxLength(500);
            
            // Índices para búsquedas rápidas
            builder.HasIndex(l => l.IdUsuario);
            builder.HasIndex(l => l.Accion);
            builder.HasIndex(l => l.Entidad);
            builder.HasIndex(l => l.FechaRegistro).IsDescending();
            builder.HasIndex(l => new { l.IdUsuario, l.FechaRegistro }).IsDescending();
            builder.HasIndex(l => new { l.Entidad, l.IdEntidad });
        }
    }
}
