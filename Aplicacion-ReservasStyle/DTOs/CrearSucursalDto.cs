using System.ComponentModel.DataAnnotations;

namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearSucursalDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, 
            ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200, MinimumLength = 5,
            ErrorMessage = "La dirección debe tener entre 5 y 200 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(50)]
        public string Estado { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "La ciudad debe tener entre 2 y 100 caracteres")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [StringLength(20)]
        [RegularExpression(@"^\d{5}(-\d{4})?$", 
            ErrorMessage = "Formato de código postal inválido")]
        public string CodigoPostal { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20)]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string Telefono { get; set; }
    }
}
