using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IPromocionesService
    {
        Task<IEnumerable<PromocionesResponseDto>> GetAllAsync();
        Task<PromocionesResponseDto> GetByIdAsync(int id);
        Task<PromocionesResponseDto> CreateAsync(CrearPromocionesDto dto);
        Task<PromocionesResponseDto> UpdateAsync(int id, ActualizarPromocionesDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PromocionesResponseDto>> GetActivasAsync();
        Task<IEnumerable<PromocionesResponseDto>> GetVigentesAsync();
        Task<IEnumerable<PromocionesResponseDto>> GetProximasAsync(int dias = 7);
        Task<bool> IsPromocionVigenteAsync(int idPromocion);
    }
}
