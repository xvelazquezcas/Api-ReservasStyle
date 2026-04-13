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
            builder.Property(s => s.Duracion).IsRequired(); // Minutos
            builder.Property(s => s.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
            // builder.Property(s => s.Imagen). HasMaxLength(4000); // URL o ruta de la imagen
            builder.Property(s => s.Estado).HasDefaultValue(true);
        }
    }

}