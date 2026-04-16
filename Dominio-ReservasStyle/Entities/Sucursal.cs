namespace Dominio_ReservasStyle.Entities
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }
        public required string Nombre { get; set; }
        public required string Direccion { get; set; }
        public string? Ubicacion { get; set; }
        public required string Estado { get; set; } 
        public required string Ciudad { get; set; } 
        public required string CodigoPostal { get; set; } 
        public required string Telefono { get; set; }
        public bool EstadoActivo { get; set; }

        // Navegaciones
        public ICollection<ServicioSucursal> Servicios { get; set; } = new List<ServicioSucursal>();
        public ICollection<ServicioPromocion> Promociones { get; set; } = new List<ServicioPromocion>();
    }
}

