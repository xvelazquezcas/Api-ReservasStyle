namespace Dominio_ReservasStyle.Entities
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public int IdUsuario { get; set; }
        public int IdSucursal { get; set; }
        public string? Especialidad { get; set; }
        public bool Estado { get; set; }
    }
}