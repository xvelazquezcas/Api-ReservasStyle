using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class PromocionesConfiguration : IEntityTypeConfiguration<Promociones>
    {
        public void Configure(EntityTypeBuilder<Promociones> builder)
        {
            builder.ToTable("Promociones");
            builder.HasKey(p => p.IdPromocion);
            builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Descripcion).HasMaxLength(200);
            builder.Property(p => p.PorcentajeDescuento).HasPrecision(5, 2).IsRequired(); // Ej: 15.50
            builder.Property(c => c.FechaInicio).IsRequired().HasColumnType("timestamp").HasColumnName("fecha_inicio");
            builder.Property(c => c.FechaFin).IsRequired().HasColumnType("timestamp").HasColumnName("fecha_fin");
            builder.Property(p => p.Estado).HasDefaultValue(true);
        }
    }

}