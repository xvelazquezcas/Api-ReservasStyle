namespace Dominio_ReservasStyle.Entities
{
    public class Promociones
    {
        public int IdPromocion { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }

        public ICollection<ServicioPromocion> Servicios { get; set; } = new List<ServicioPromocion>();
    }
}