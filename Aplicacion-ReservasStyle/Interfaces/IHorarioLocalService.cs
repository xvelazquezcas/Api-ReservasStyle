using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IHorarioLocalService
    {
        Task<IEnumerable<HorarioLocalResponseDto>> GetAllAsync();
        Task<HorarioLocalResponseDto> GetByIdAsync(int id);
        Task<HorarioLocalResponseDto> CreateAsync(CrearHorarioLocalDto dto);
        Task<HorarioLocalResponseDto> UpdateAsync(int id, ActualizarHorarioLocalDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<HorarioLocalResponseDto>> GetByIdSucursalAsync(int idSucursal);
        Task<HorarioLocalResponseDto?> GetByIdSucursalAndDiaAsync(int idSucursal, string dia);
    }
}
