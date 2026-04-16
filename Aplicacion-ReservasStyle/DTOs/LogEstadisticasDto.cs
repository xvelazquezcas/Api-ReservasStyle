namespace Aplicacion_ReservasStyle.DTOs
{
    public class LogEstadisticasDto
    {
        public int TotalLogs { get; set; }
        public decimal PorcentajeExitosos { get; set; }
        public string AccionMasFrecuente { get; set; }
        public int LogsHoy { get; set; }
        public int LogsEstaSemanä { get; set; }
        public int LogsEsteMes { get; set; }
        public Dictionary<string, int> LogsPorAccion { get; set; } = new();
        public Dictionary<string, int> LogsPorEntidad { get; set; } = new();
    }
}
