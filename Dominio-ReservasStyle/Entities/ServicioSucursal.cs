namespace Dominio_ReservasStyle.Entities
{
    public class ServicioSucursal
    {
        public int IdServicioSucursal { get; set; }
        public int IdServicio { get; set; }
        public int IdSucursal { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
    }
}