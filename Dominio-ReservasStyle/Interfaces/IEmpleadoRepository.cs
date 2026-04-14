using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<Empleado?> GetByIdAsync(int id);
        Task<IEnumerable<Empleado>> GetAllAsync();
        Task<Empleado> CreateAsync(Empleado empleado);
        Task UpdateAsync(Empleado empleado);
        Task DeleteAsync(int id);
        Task<bool> ExisteEmpleadoIdAsync(int id);
    }
}