namespace Aplicacion_ReservasStyle.DTOs
{
    public class PromocionesResponseDto
    {
        public int IdPromocion { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Estado { get; set; }
        public string FechaInicioFormato => FechaInicio.ToString("dd/MM/yyyy");
        public string FechaFinFormato => FechaFin.ToString("dd/MM/yyyy");
        public bool EsVigente => DateTime.Now >= FechaInicio && DateTime.Now <= FechaFin && Estado;
    }
}
