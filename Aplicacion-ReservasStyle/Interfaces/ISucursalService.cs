using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface ISucursalService
    {
        Task<IEnumerable<SucursalResponseDto>> GetAllAsync();
        Task<SucursalResponseDto> GetByIdAsync(int id);
        Task<SucursalResponseDto> CreateAsync(CrearSucursalDto dto);
        Task<SucursalResponseDto> UpdateAsync(int id, ActualizarSucursalDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
