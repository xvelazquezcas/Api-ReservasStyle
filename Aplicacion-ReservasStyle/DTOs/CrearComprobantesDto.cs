using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearComprobantesDto
    {
        [Required(ErrorMessage = "El IdPago es requerido")]
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El Folio es requerido")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "El Folio debe tener entre 1 y 50 caracteres")]
        public string Folio { get; set; }
    }
}
