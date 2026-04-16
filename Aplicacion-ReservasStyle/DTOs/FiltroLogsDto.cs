namespace Aplicacion_ReservasStyle.DTOs
{
    public class FiltroLogsDto
    {
        public int? IdUsuario { get; set; }
        public string? Accion { get; set; }
        public string? Entidad { get; set; }
        public bool? Exitoso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }
}
