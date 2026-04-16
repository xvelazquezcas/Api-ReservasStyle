using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IHorariosDisponiblesRepository
    {
        Task<IEnumerable<HorariosDisponibles>> GetAllAsync();
        Task<HorariosDisponibles?> GetByIdAsync(int id);
        Task<HorariosDisponibles> CreateAsync(HorariosDisponibles horario);
        Task UpdateAsync(HorariosDisponibles horario);
        Task DeleteAsync(int id);
        Task<bool> ExisteHorarioAsync(int idEmpleado, DiaSemana diaSemana);
        Task<IEnumerable<HorariosDisponibles>> GetByEmpleadoAsync(int idEmpleado);
        Task<IEnumerable<HorariosDisponibles>> GetByDiaAsync(DiaSemana dia);
    }
}
