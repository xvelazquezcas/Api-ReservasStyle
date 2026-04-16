using System.ComponentModel.DataAnnotations;
using Dominio_ReservasStyle.Enums;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class ActualizarHorariosDisponiblesDto
    {
        [Required(ErrorMessage = "El IdHorario es requerido")]
        public int IdHorario { get; set; }

        [Required(ErrorMessage = "El IdEmpleado es requerido")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El DiaSemana es requerido")]
        public DiaSemana DiaSemana { get; set; }

        [Required(ErrorMessage = "La hora de inicio es requerida")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es requerida")]
        public TimeSpan HoraFin { get; set; }

        [CustomValidation(typeof(ActualizarHorariosDisponiblesDto), nameof(ValidarHoras))]
        public static ValidationResult? ValidarHoras(ActualizarHorariosDisponiblesDto dto, ValidationContext context)
        {
            if (dto.HoraFin <= dto.HoraInicio)
                return new ValidationResult("La hora de fin debe ser mayor que la hora de inicio");
            return ValidationResult.Success;
        }
    }
}
