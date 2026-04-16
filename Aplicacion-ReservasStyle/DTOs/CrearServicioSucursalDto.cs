using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearServicioSucursalDto
    {
        [Required(ErrorMessage = "El IdServicio es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El IdServicio debe ser mayor a 0")]
        public int IdServicio { get; set; }

        [Required(ErrorMessage = "El IdSucursal es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El IdSucursal debe ser mayor a 0")]
        public int IdSucursal { get; set; }

        [Required(ErrorMessage = "El Precio es requerido")]
        [Range(0.01, 999999.99, ErrorMessage = "El Precio debe estar entre 0.01 y 999999.99")]
        public decimal Precio { get; set; }
    }
}
