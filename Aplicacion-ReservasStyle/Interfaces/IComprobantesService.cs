using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IComprobantesService
    {
        Task<IEnumerable<ComprobantesResponseDto>> GetAllAsync();
        Task<ComprobantesResponseDto> GetByIdAsync(int id);
        Task<ComprobantesResponseDto> CreateAsync(CrearComprobantesDto dto);
        Task<ComprobantesResponseDto> UpdateAsync(int id, ActualizarComprobantesDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ComprobantesResponseDto>> GetByIdPagoAsync(int idPago);
    }
}
