namespace Dominio_ReservasStyle.Entities
{
    public class ServicioSucursal
    {
        public int IdServicioSucursal { get; set; }
        public int IdServicio { get; set; }
        public int IdSucursal { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }

        // Navegaciones
        public Servicio? Servicio { get; set; }
        public Sucursal? Sucursal { get; set; }
        public ICollection<ServicioPromocion> Promociones { get; set; } = new List<ServicioPromocion>();
    }
}