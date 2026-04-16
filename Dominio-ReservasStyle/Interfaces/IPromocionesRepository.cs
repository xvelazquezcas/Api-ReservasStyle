using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IPromocionesRepository
    {
        Task<IEnumerable<Promociones>> GetAllAsync();
        Task<Promociones?> GetByIdAsync(int id);
        Task<Promociones> CreateAsync(Promociones promocion);
        Task UpdateAsync(Promociones promocion);
        Task DeleteAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre);
        Task<IEnumerable<Promociones>> GetActivasAsync();
        Task<IEnumerable<Promociones>> GetVigentesAsync();
        Task<IEnumerable<Promociones>> GetProximasAsync(int dias = 7);
        Task<bool> IsPromocionVigenteAsync(int idPromocion);
    }
}
