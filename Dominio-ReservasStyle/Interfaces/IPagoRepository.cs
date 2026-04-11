using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IPagoRepository
    {
        Task<Pago?> GetByIdAsync(int id);
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago> CreateAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
    }
}