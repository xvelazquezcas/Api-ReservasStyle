namespace Aplicacion_ReservasStyle.DTOs
{
    public class SucursalResponseDto
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
