using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Dominio_ReservasStyle.Entities;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class UsuarioRolConfiguration : IEntityTypeConfiguration<UsuarioRol>
    {
        public void Configure(EntityTypeBuilder<UsuarioRol> builder)
        {
            builder.ToTable("usuario_roles");
            builder.HasKey(ur => new { ur.UsuarioId, ur.RolId }); // Llave compuesta

            builder.HasOne(ur => ur.Usuario)
                   .WithMany(u => u.UsuarioRoles)
                   .HasForeignKey(ur => ur.UsuarioId);

            builder.HasOne(ur => ur.Rol)
                   .WithMany(r => r.UsuarioRoles)
                   .HasForeignKey(ur => ur.RolId);
        }
    }
}
