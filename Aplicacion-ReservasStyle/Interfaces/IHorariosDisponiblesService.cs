using Aplicacion_ReservasStyle.DTOs;
using Dominio_ReservasStyle.Enums;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IHorariosDisponiblesService
    {
        Task<IEnumerable<HorariosDisponiblesResponseDto>> GetAllAsync();
        Task<HorariosDisponiblesResponseDto> GetByIdAsync(int id);
        Task<HorariosDisponiblesResponseDto> CreateAsync(CrearHorariosDisponiblesDto dto);
        Task<HorariosDisponiblesResponseDto> UpdateAsync(int id, ActualizarHorariosDisponiblesDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<HorariosDisponiblesResponseDto>> GetByEmpleadoAsync(int idEmpleado);
        Task<IEnumerable<HorariosDisponiblesResponseDto>> GetByDiaAsync(DiaSemana dia);
    }
}
