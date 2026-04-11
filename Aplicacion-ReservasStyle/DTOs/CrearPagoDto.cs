namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearPagoDto
    {
        public int IdCita { get; set; }

        public decimal Precio { get; set; }

        public string? MetodoPago { get; set; }
    }
}
