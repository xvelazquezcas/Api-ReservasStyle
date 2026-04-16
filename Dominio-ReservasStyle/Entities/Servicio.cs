
namespace Dominio_ReservasStyle.Entities
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int DuracionMinutos { get; set; }
        public string? Imagen {get ; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Navegaciones
        public ICollection<ServicioSucursal> ServcioSucursales { get; set; } = new List<ServicioSucursal>();
        public ICollection<ServicioPromocion> Promociones { get; set; } = new List<ServicioPromocion>();
    }
}