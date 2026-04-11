using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class ComprobantesConfiguration : IEntityTypeConfiguration<Comprobantes>
{
    public void Configure(EntityTypeBuilder<Comprobantes> builder)
    {
        builder.ToTable("Comprobantes");
        builder.HasKey(c => c.IdComprobante);
        builder.Property(c => c.Folio).IsRequired().HasMaxLength(50);
        builder.Property(c => c.FechaEmision).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasIndex(c => c.IdPago);
        builder.HasIndex(c => c.Folio).IsUnique(); // Folios únicos
    }
}

}