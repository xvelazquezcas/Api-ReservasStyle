namespace Aplicacion_ReservasStyle.DTOs
{
    public class ServicioSucursalResponseDto
    {
        public int IdServicioSucursal { get; set; }
        public int IdServicio { get; set; }
        public int IdSucursal { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public string PrecioFormato => $"${Precio:F2}";
        public string EstadoTexto => Estado ? "Activo" : "Inactivo";
    }
}
