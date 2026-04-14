using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoResponseDto>> GetAllAsync();
        Task<EmpleadoResponseDto> GetByIdAsync(int id);
        Task<EmpleadoResponseDto> CreateAsync(CrearEmpleadoDto dto);
        Task<EmpleadoResponseDto> UpdateAsync(int id, ActualizarEmpleadoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}