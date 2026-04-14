using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class ActualizarEmpleadoDto
    {
        [Required]
        public int IdEmpleado { get; set; }
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdLocal { get; set; }
        [Required]
        public string? Especialidad { get; set; }
        [Required]
        public bool Estado { get; set; }
    }
}