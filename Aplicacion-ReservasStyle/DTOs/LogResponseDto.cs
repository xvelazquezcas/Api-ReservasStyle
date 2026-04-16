namespace Aplicacion_ReservasStyle.DTOs
{
    public class LogResponseDto
    {
        public int IdLog { get; set; }
        public int? IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Accion { get; set; }
        public string Entidad { get; set; }
        public int? IdEntidad { get; set; }
        public string? DetalleAntes { get; set; }
        public string? DetalleDepues { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaRegistroFormato => FechaRegistro.ToString("dd/MM/yyyy HH:mm:ss");
        public string? DireccionIP { get; set; }
        public string? UserAgent { get; set; }
        public bool Exitoso { get; set; }
        public string? MensajeError { get; set; }
        public string EstadoTexto => Exitoso ? "Éxito" : "Error";
    }
}
