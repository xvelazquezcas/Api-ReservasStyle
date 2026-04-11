namespace Dominio_ReservasStyle.Entities
{
    public class Local
    {
        public int IdLocal { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Cuidad { get; set; }
        public string? Estado { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Telefono { get; set; }
        public bool EstadoActivo { get; set; }
    }
}