using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface ISucursalRepository
    {
        Task<IEnumerable<Sucursal>> GetAllAsync();
        Task<Sucursal?> GetByIdAsync(int id);
        Task<Sucursal> CreateAsync(Sucursal sucursal);
        Task UpdateAsync(Sucursal sucursal);
        Task DeleteAsync(int id);
        
        // Métodos adicionales para validaciones
        Task<bool> ExisteNombreAsync(string nombre);
        Task<IEnumerable<Sucursal>> GetByEstadoAsync(string estado);
    }
}
