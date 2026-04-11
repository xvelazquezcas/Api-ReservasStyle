namespace Aplicacion_ReservasStyle.DTOs
{
    public class CrearCitaDto
    {
        public int IdCliente { get; set; }

        public int IdEmpleado { get; set; }

        public int IdServicioLocal { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan HoraInicio { get; set; }
    }
}
