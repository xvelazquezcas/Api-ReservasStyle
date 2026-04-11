using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class HorarioLocalConfiguration : IEntityTypeConfiguration<HorarioLocal>
{
    public void Configure(EntityTypeBuilder<HorarioLocal> builder)
    {
        builder.ToTable("HorarioLocal");
        builder.HasKey(hl => hl.IdHorarioLocal);
        builder.Property(hl => hl.DiaSemana).IsRequired().HasMaxLength(20);
        builder.Property(hl => hl.HoraAbierto).IsRequired(); // Se mapea a 'time' en Postgres
        builder.Property(hl => hl.HoraCerrado).IsRequired();
        builder.Property(hl => hl.Estado).HasDefaultValue(true);
        builder.HasIndex(hl => hl.IdLocal);
    }
}

}