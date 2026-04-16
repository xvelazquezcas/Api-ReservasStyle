namespace Aplicacion_ReservasStyle.DTOs
{
    public class NotificacionesResponseDto
    {
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leida { get; set; }
        public string FechaEnvioFormato => FechaEnvio.ToString("dd/MM/yyyy HH:mm:ss");
    }
}
