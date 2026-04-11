using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class ServicioLocalConfiguration : IEntityTypeConfiguration<ServicioLocal>
{
    public void Configure(EntityTypeBuilder<ServicioLocal> builder)
    {
        builder.ToTable("ServicioLocal");
        builder.HasKey(sl => sl.IdServicioLocal);
        builder.Property(sl => sl.Precio).HasPrecision(18, 2).IsRequired();
        builder.Property(sl => sl.Estado).HasDefaultValue(true);
        builder.HasIndex(sl => new { sl.IdServicio, sl.IdLocal }); // Índice compuesto para búsquedas
    }
}

}