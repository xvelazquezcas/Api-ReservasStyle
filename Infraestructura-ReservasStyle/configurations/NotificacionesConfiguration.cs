using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class NotificacionesConfiguration : IEntityTypeConfiguration<Notificaciones>
    {
        public void Configure(EntityTypeBuilder<Notificaciones> builder)
        {
            builder.ToTable("Notificaciones");
            builder.HasKey(n => n.IdNotificacion);
            builder.Property(n => n.Mensaje).IsRequired().HasMaxLength(1000);
            builder.Property(n => n.FechaEnvio).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(n => n.Leida).HasDefaultValue(false);
            builder.HasIndex(n => n.IdUsuario);
        }
    }

}