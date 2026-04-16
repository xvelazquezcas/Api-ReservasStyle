using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class ActualizarPagoDto
    {
        [Required(ErrorMessage = "El IdPago es requerido")]
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El IdCita es requerido")]
        public int IdCita { get; set; }

        [Required(ErrorMessage = "El Monto es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El Monto debe estar entre 0.01 y 999999.99")]
        public decimal Monto { get; set; }

        [StringLength(50, ErrorMessage = "El MetodoPago no puede exceder 50 caracteres")]
        public string? MetodoPago { get; set; }

        [Required(ErrorMessage = "El EstadoPago es requerido")]
        [StringLength(20, ErrorMessage = "El EstadoPago no puede exceder 20 caracteres")]
        public string EstadoPago { get; set; }

        [StringLength(100, ErrorMessage = "La ReferenciaTransaccion no puede exceder 100 caracteres")]
        public string? ReferenciaTransaccion { get; set; }
    }
}
