namespace Dominio_ReservasStyle.Entities
{
    public class Notificaciones
    {
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public string? Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Leida { get; set; }
    }
}