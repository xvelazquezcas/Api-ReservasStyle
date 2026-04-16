using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IServicioSucursalService
    {
        // CRUD
        Task<IEnumerable<ServicioSucursalResponseDto>> GetAllAsync();
        Task<ServicioSucursalResponseDto> GetByIdAsync(int idServicioSucursal);
        Task<ServicioSucursalResponseDto> CreateAsync(CrearServicioSucursalDto dto);
        Task<ServicioSucursalResponseDto> UpdateAsync(int idServicioSucursal, ActualizarServicioSucursalDto dto);
        Task<bool> DeleteAsync(int idServicioSucursal);
        
        // Consultas especializadas
        Task<ServicioSucursalDetalleDto> GetDetalleAsync(int idServicio, int idSucursal);
        Task<IEnumerable<ServicioSucursalDetalleDto>> GetServiciosDeSucursalAsync(int idSucursal);
        Task<IEnumerable<ServicioSucursalResponseDto>> GetSucursalesDelServicioAsync(int idServicio);
        Task<IEnumerable<ServicioSucursalResponseDto>> GetActivosAsync();
        Task<IEnumerable<ServicioSucursalResponseDto>> GetActivosBySucursalAsync(int idSucursal);
        Task<decimal> GetPrecioAsync(int idServicio, int idSucursal);
        Task<ServicioSucursalResponseDto> UpdatePrecioAsync(int idServicio, int idSucursal, decimal nuevoPrecio);
        Task<IEnumerable<ServicioSucursalResponseDto>> GetByRangoPrecioAsync(decimal precioMin, decimal precioMax);
        Task<decimal> GetPrecioPromedioPorServicioAsync(int idServicio);
        Task<IEnumerable<ServicioSucursalResponseDto>> ToggleEstadoAsync(int idServicio, int idSucursal);
    }
}
