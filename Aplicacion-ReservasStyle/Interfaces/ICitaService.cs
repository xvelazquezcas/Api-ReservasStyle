using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface ICitaService
    {
        Task<IEnumerable<Citas>> GetAllAsync();
        Task<Citas?> GetByIdAsync(int id);
        Task<Citas> CreateAsync(Citas cita);
        Task UpdateAsync(Citas cita);
        Task DeleteAsync(int id);
    }
}
