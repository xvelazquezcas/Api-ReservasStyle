namespace Dominio_ReservasStyle.Entities
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdCita { get; set; }
        public decimal Precio { get; set; }
        public string? MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string? EstadoPago { get; set; }
        public string? ReferenciaTransaccion { get; set; }

    }
}