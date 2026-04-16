using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearPromocionesDto
    {
        [Required(ErrorMessage = "El Nombre es requerido")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El Nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [StringLength(200, ErrorMessage = "La Descripcion no puede exceder 200 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El PorcentajeDescuento es requerido")]
        [Range(0.01, 100, ErrorMessage = "El PorcentajeDescuento debe estar entre 0.01 y 100")]
        public decimal PorcentajeDescuento { get; set; }

        [Required(ErrorMessage = "La FechaInicio es requerida")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La FechaFin es requerida")]
        public DateTime FechaFin { get; set; }

        [CustomValidation(typeof(CrearPromocionesDto), nameof(ValidarFechas))]
        public static ValidationResult? ValidarFechas(CrearPromocionesDto dto, ValidationContext context)
        {
            if (dto.FechaFin <= dto.FechaInicio)
                return new ValidationResult("La FechaFin debe ser mayor que la FechaInicio");
            return ValidationResult.Success;
        }
    }
}
