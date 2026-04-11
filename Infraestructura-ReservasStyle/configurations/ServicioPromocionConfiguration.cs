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
        builder.HasKey(sp => new { sp.IdServicioLocal, sp.IdPromocion });
    }
}

}