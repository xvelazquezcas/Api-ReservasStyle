using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio_ReservasStyle.Entities;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class RolConfiguration : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(r => r.Nombre)
                .IsUnique();
        }
    }
}
