using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class ServicioPromocionConfiguration : IEntityTypeConfiguration<ServicioPromocion>
    {
        public void Configure(EntityTypeBuilder<ServicioPromocion> builder)
        {
            builder.ToTable("ServicioPromocion");

            // Llave primaria compuesta
            builder.HasKey(sp => new { sp.IdServicioSucursal, sp.IdPromocion });

            // Índices para mejor rendimiento
            builder.HasIndex(sp => sp.IdServicioSucursal);
            builder.HasIndex(sp => sp.IdPromocion);

            // Relaciones con cascada
            builder.HasOne(sp => sp.ServicioSucursal)
                .WithMany(s => s.Promociones)
                .HasForeignKey(sp => sp.IdServicioSucursal)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.Promocion)
                .WithMany(p => p.Servicios)
                .HasForeignKey(sp => sp.IdPromocion)
                .OnDelete(DeleteBehavior.Cascade);

            // Campo de auditoría
            builder.Property(sp => sp.FechaAsociacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}