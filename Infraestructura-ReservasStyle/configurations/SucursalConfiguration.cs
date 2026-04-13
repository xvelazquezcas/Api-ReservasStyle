using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations;

public class SucursalConfiguration : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> builder)
    {
        builder.ToTable("sucursales");

        builder.HasKey(s => s.IdSucursal);

        builder.Property(s => s.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Direccion)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Estado)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Ciudad)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.CodigoPostal)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.Telefono)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(s => s.EstadoActivo)
            .HasDefaultValue(true);

        builder.HasIndex(s => s.Nombre)
            .IsUnique();
    }
}