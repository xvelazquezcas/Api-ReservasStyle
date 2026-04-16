namespace Aplicacion_ReservasStyle.DTOs
{
    public class ServicioPromocionDetalleDto
    {
        public int IdServicioSucursal { get; set; }
        public int IdPromocion { get; set; }
        public DateTime FechaAsociacion { get; set; }
        
        // Información adicional de la Promoción
        public string NombrePromocion { get; set; }
        public string? DescripcionPromocion { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public DateTime FechaInicioPromocion { get; set; }
        public DateTime FechaFinPromocion { get; set; }
        public bool EstadoPromocion { get; set; }
    }
}
