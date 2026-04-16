namespace Dominio_ReservasStyle.Entities
{
    public class Log
    {
        public int IdLog { get; set; }
        public int? IdUsuario { get; set; }  // Nullable para logs del sistema
        public string NombreUsuario { get; set; } = "Sistema";
        public string Accion { get; set; }  // CREATE, UPDATE, DELETE, LOGIN, LOGOUT, GET
        public string Entidad { get; set; }  // Nombre de la tabla/entidad afectada
        public int? IdEntidad { get; set; }  // ID del registro afectado
        public string? DetalleAntes { get; set; }  // Valor anterior (para UPDATE)
        public string? DetalleDepues { get; set; }  // Valor nuevo (para UPDATE)
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string? DireccionIP { get; set; }
        public string? UserAgent { get; set; }
        public bool Exitoso { get; set; } = true;
        public string? MensajeError { get; set; }
    }
}
