namespace Dominio_ReservasStyle.Entities
{
    public class UsuarioRol
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public Guid RolId { get; set; }
        public Rol Rol { get; set; } = null!;
    }
}
