using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IHorarioLocalRepository
    {
        Task<IEnumerable<HorarioLocal>> GetAllAsync();
        Task<HorarioLocal?> GetByIdAsync(int id);
        Task<HorarioLocal> CreateAsync(HorarioLocal horarioLocal);
        Task UpdateAsync(HorarioLocal horarioLocal);
        Task DeleteAsync(int id);
        Task<IEnumerable<HorarioLocal>> GetByIdSucursalAsync(int idSucursal);
        Task<HorarioLocal?> GetByIdSucursalAndDiaAsync(int idSucursal, string dia);
    }
}
