using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IServicioSucursalRepository
    {
        // CRUD básico
        Task<IEnumerable<ServicioSucursal>> GetAllAsync();
        Task<ServicioSucursal?> GetByIdAsync(int idServicioSucursal);
        Task<ServicioSucursal> CreateAsync(ServicioSucursal servicioSucursal);
        Task UpdateAsync(ServicioSucursal servicioSucursal);
        Task DeleteAsync(int idServicioSucursal);
        
        // Consultas especializadas
        Task<ServicioSucursal?> GetByServicioAndSucursalAsync(int idServicio, int idSucursal);
        Task<IEnumerable<ServicioSucursal>> GetByServicioAsync(int idServicio);
        Task<IEnumerable<ServicioSucursal>> GetBySucursalAsync(int idSucursal);
        Task<IEnumerable<ServicioSucursal>> GetActivosAsync();
        Task<IEnumerable<ServicioSucursal>> GetActivosBySucursalAsync(int idSucursal);
        Task<bool> ExisteAsync(int idServicio, int idSucursal);
        Task<decimal?> GetPrecioAsync(int idServicio, int idSucursal);
        Task<IEnumerable<ServicioSucursal>> GetByRangoPrecioAsync(decimal precioMin, decimal precioMax);
        Task<decimal> GetPrecioPromedioPorServicioAsync(int idServicio);
        Task<int> GetCountBySucursalAsync(int idSucursal);
    }
}
