namespace Dominio_ReservasStyle.Entities
{
    public class ServicioPromocion ///(TABLA INTERMEDIA)
    {
        public int IdServicioSucursal { get; set; }
        public int IdPromocion { get; set; }
        public DateTime FechaAsociacion { get; set; } = DateTime.Now;

        // Navegaciones
        public ServicioSucursal? ServicioSucursal { get; set; }
        public Promociones? Promocion { get; set; }
    }
}