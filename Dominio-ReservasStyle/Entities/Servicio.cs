namespace Dominio_ReservasStyle.Entities
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Duracion { get; set; } // Duración en minutos
        public DateTime FechaCreacion { get; set; } ///En duda
        public bool Estado { get; set; }
    }
}