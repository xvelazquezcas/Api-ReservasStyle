
using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearEmpleadoDto
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public int IdLocal { get; set; }
        [Required]
        public string? Especialidad { get; set; }
    }
}