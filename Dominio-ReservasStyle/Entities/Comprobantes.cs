namespace Dominio_ReservasStyle.Entities
{
    public class Comprobantes
    {
        public int IdComprobante { get; set; }
        public int IdPago { get; set; }
        public string? Folio { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}