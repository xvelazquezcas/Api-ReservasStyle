namespace Aplicacion_ReservasStyle.DTOs
{
    public class EmpleadoResponseDto
    {
        public int IdEmpleado { get; set; }
        public int IdUsuario { get; set; }
        public int IdLocal { get; set; }
        public string? Especialidad { get; set; }
        public bool Estado { get; set; }
    }
}