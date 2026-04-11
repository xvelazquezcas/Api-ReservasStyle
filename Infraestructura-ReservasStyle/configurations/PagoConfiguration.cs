using Dominio_ReservasStyle.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructura_ReservasStyle.Configurations
{
    public class PagoConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            // 1. Nombre de la tabla
            builder.ToTable("Pagos");

            // 2. Llave Primaria
            builder.HasKey(p => p.IdPago);

            // 3. Configuración de Propiedades

            builder.Property(p => p.IdCita)
                .IsRequired();

            // IMPORTANTE: En PostgreSQL el tipo decimal debe tener precisión
            builder.Property(p => p.Precio)
                .HasPrecision(18, 2) // 18 dígitos en total, 2 decimales (ej: 9999.99)
                .IsRequired();

            builder.Property(p => p.MetodoPago)
                .HasMaxLength(50) // Efectivo, Tarjeta, Transferencia, etc.
                .IsRequired(false);

            // Configuración de Fecha (Recuerda usar UTC en el código C#)
            builder.Property(p => p.FechaPago)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(p => p.EstadoPago)
                .HasMaxLength(20) // Pendiente, Completado, Fallido
                .HasDefaultValue("Pendiente");

            builder.Property(p => p.ReferenciaTransaccion)
                .HasMaxLength(100) // Código de comprobante o ID de pasarela
                .IsRequired(false);

            // 4. Índice para la Cita (Para buscar pagos de una cita rápidamente)
            builder.HasIndex(p => p.IdCita);
        }
    }
}