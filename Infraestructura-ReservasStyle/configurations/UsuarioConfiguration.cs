using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio_ReservasStyle.Entities; // Asegúrate de que este sea el namespace de tu clase Usuario

namespace Infraestructura_ReservasStyle.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.Telefono)
                .HasMaxLength(20);

            builder.Property(u => u.ContrasenaHash)
                .IsRequired();
            builder.Property(u => u.FotoPerfil)
                .IsRequired();
            builder.Property(u => u.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(u => u.Estado)
                .HasDefaultValue(true);

            builder.Property(u => u.IdRol)
                .IsRequired();
            builder.Property(u => u.IdSucursal)
                .IsRequired();
        }
    }
}