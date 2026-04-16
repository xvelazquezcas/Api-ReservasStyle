using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IServicioPromocionRepository
    {
        // CRUD básico
        Task<ServicioPromocion?> GetAsync(int idServicioSucursal, int idPromocion);
        Task<IEnumerable<ServicioPromocion>> GetAllAsync();
        Task<ServicioPromocion> CreateAsync(ServicioPromocion servicioPromocion);
        Task UpdateAsync(ServicioPromocion servicioPromocion);
        Task DeleteAsync(int idServicioSucursal, int idPromocion);
        
        // Consultas especializadas
        Task<IEnumerable<ServicioPromocion>> GetByIdServicioAsync(int idServicio);
        Task<IEnumerable<ServicioPromocion>> GetByIdPromocionAsync(int idPromocion);
        Task<bool> ExisteAsync(int idServicioSucursal, int idPromocion);
        Task<int> GetCountByServicioAsync(int idServicio);
        Task<int> GetCountByPromocionAsync(int idPromocion);
    }
}
