namespace Aplicacion_ReservasStyle.DTOs
{
    public class PagoResponseDto
    {
        public int IdPago { get; set; }
        public int IdCita { get; set; }
        public decimal Monto { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string? EstadoPago { get; set; }
        public string? ReferenciaTransaccion { get; set; }
        public string FechaPagoFormato => FechaPago.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
