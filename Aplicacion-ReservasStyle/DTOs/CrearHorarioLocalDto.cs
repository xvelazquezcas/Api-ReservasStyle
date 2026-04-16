using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearHorarioLocalDto
    {
        [Required(ErrorMessage = "El IdSucursal es requerido")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El DiaSemana es requerido")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "El DiaSemana debe tener entre 1 y 20 caracteres")]
        public string DiaSemana { get; set; }

        [Required(ErrorMessage = "La HoraAbierto es requerida")]
        public TimeSpan HoraAbierto { get; set; }

        [Required(ErrorMessage = "La HoraCerrado es requerida")]
        public TimeSpan HoraCerrado { get; set; }

        public bool Estado { get; set; } = true;
    }
}
