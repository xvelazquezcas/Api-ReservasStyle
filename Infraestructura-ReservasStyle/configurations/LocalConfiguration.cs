using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class LocalConfiguration : IEntityTypeConfiguration<Local>
{
    public void Configure(EntityTypeBuilder<Local> builder)
    {
        builder.ToTable("Locales");
        builder.HasKey(l => l.IdLocal);
        builder.Property(l => l.Nombre).IsRequired().HasMaxLength(150);
        builder.Property(l => l.Direccion).IsRequired().HasMaxLength(250);
        builder.Property(l => l.Cuidad).HasMaxLength(100); // "Cuidad" (mantengo tu typo del modelo)
        builder.Property(l => l.Estado).HasMaxLength(100);
        builder.Property(l => l.Telefono).HasMaxLength(20);
        builder.Property(l => l.EstadoActivo).HasDefaultValue(true);
    }
}

}