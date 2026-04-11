namespace Dominio_ReservasStyle.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? ContrasenaHash { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Estado { get; set; }
        public int IdRol { get; set; }
    }
}