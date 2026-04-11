using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IServicioRepository
    {
        Task<Servicio?> GetByIdAsync(int id);
        Task<IEnumerable<Servicio>> GetAllAsync();
        Task<Servicio> CreateAsync(Servicio servicio);
        Task UpdateAsync(Servicio servicio);
        Task DeleteAsync(int id);
    }
}