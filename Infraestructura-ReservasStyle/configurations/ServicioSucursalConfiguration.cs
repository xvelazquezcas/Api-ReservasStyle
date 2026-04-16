using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    namespace Infraestructura_ReservasStyle.Configurations
    {
        public class ServicioSucursalConfiguration : IEntityTypeConfiguration<ServicioSucursal>
        {
            public void Configure(EntityTypeBuilder<ServicioSucursal> builder)
            {
                builder.ToTable("ServicioSucursal");
                builder.HasKey(sl => sl.IdServicioSucursal);
                builder.Property(sl => sl.Precio).HasPrecision(18, 2).IsRequired();
                builder.Property(sl => sl.Estado).HasDefaultValue(true);
                builder.HasIndex(sl => new { sl.IdServicio, sl.IdSucursal }); // Índice compuesto para búsquedas
            }
        }

    }
}