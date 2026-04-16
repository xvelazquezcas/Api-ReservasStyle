using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class ServicioConfiguration : IEntityTypeConfiguration<Servicio>
    {
        public void Configure(EntityTypeBuilder<Servicio> builder)
        {
            builder.ToTable("Servicios");
            builder.HasKey(s => s.IdServicio);
            builder.Property(s => s.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Descripcion).HasMaxLength(500);
            builder.Property(s => s.DuracionMinutos).IsRequired();
            builder.Property(s => s.Imagen).HasMaxLength(1000);
            builder.Property(s => s.Estado).HasDefaultValue(true);
        }
    }
}