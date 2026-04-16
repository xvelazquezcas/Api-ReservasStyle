using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearServicioPromocionDto
    {
        [Required(ErrorMessage = "El IdServicioSucursal es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El IdServicioSucursal debe ser mayor a 0")]
        public int IdServicioSucursal { get; set; }

        [Required(ErrorMessage = "El IdPromocion es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El IdPromocion debe ser mayor a 0")]
        public int IdPromocion { get; set; }
    }
}
