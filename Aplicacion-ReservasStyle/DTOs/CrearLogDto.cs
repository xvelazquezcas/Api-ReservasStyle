using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearLogDto
    {
        [Required(ErrorMessage = "La Acción es requerida")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La Acción debe tener entre 3 y 50 caracteres")]
        public string Accion { get; set; }

        [Required(ErrorMessage = "La Entidad es requerida")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La Entidad debe tener entre 1 y 100 caracteres")]
        public string Entidad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El IdEntidad debe ser mayor a 0")]
        public int? IdEntidad { get; set; }

        [StringLength(2000, ErrorMessage = "DetalleAntes no puede exceder 2000 caracteres")]
        public string? DetalleAntes { get; set; }

        [StringLength(2000, ErrorMessage = "DetalleDepues no puede exceder 2000 caracteres")]
        public string? DetalleDepues { get; set; }

        [StringLength(45, ErrorMessage = "DireccionIP no puede exceder 45 caracteres")]
        public string? DireccionIP { get; set; }

        [StringLength(500, ErrorMessage = "UserAgent no puede exceder 500 caracteres")]
        public string? UserAgent { get; set; }

        public bool Exitoso { get; set; } = true;

        [StringLength(500, ErrorMessage = "MensajeError no puede exceder 500 caracteres")]
        public string? MensajeError { get; set; }
    }
}
