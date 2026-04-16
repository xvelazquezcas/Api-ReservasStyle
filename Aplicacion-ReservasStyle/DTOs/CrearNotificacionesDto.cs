using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearNotificacionesDto
    {
        [Required(ErrorMessage = "El IdUsuario es requerido")]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El Mensaje es requerido")]
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "El Mensaje debe tener entre 5 y 1000 caracteres")]
        public string Mensaje { get; set; }
    }
}
