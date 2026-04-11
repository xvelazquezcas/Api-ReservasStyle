namespace Dominio_ReservasStyle.Entities
{
    public class Rol
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty; // Ej: "Admin", "Empleado"
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
