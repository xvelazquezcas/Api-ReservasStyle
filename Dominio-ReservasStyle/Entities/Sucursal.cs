namespace Dominio_ReservasStyle.Entities
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public required string Estado { get; set; } 
        public required string Ciudad { get; set; } 
        public required string CodigoPostal { get; set; } 
        public required string Telefono { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
