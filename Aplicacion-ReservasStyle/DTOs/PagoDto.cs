namespace Aplicacion_ReservasStyle.DTOs
{
    public class PagoDto
    {
        public int IdPago { get; set; }

        public int IdCita { get; set; }

        public decimal Precio { get; set; }

        public string? MetodoPago { get; set; }

        public DateTime FechaPago { get; set; }
    }
}
