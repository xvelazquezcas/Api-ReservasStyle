namespace Aplicacion_ReservasStyle.DTOs
{
    public class ComprobantesResponseDto
    {
        public int IdComprobante { get; set; }
        public int IdPago { get; set; }
        public string Folio { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
