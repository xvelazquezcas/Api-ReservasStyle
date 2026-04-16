using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IServicioPromocionService
    {
        // CRUD
        Task<ServicioPromocionResponseDto> GetAsync(int idServicioSucursal, int idPromocion);
        Task<IEnumerable<ServicioPromocionResponseDto>> GetAllAsync();
        Task<ServicioPromocionResponseDto> CreateAsync(CrearServicioPromocionDto dto);
        Task<ServicioPromocionResponseDto> UpdateAsync(int idServicioSucursal, int idPromocionAnterior, ActualizarServicioPromocionDto dto);
        Task<bool> DeleteAsync(int idServicioSucursal, int idPromocion);
        
        // Consultas especializadas
        Task<IEnumerable<ServicioPromocionDetalleDto>> GetPromocionesDeServicioAsync(int idServicio);
        Task<IEnumerable<ServicioPromocionResponseDto>> GetServiciosDePromocionAsync(int idPromocion);
        Task<int> GetCountPromocionesDeServicioAsync(int idServicio);
        Task<int> GetCountServiciosDePromocionAsync(int idPromocion);
    }
}
