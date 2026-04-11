namespace Dominio_ReservasStyle.Entities
{
    public class ServicioLocal
    {
        public int IdServicioLocal { get; set; }
        public int IdServicio { get; set; }
        public int IdLocal { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }
}