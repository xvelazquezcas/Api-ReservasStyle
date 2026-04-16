using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<PagoResponseDto>> GetAllAsync();
        Task<PagoResponseDto> GetByIdAsync(int id);
        Task<PagoResponseDto> CreateAsync(CrearPagoDto dto);
        Task<PagoResponseDto> UpdateAsync(int id, ActualizarPagoDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PagoResponseDto>> GetByCitaAsync(int idCita);
        Task<IEnumerable<PagoResponseDto>> GetByEstadoPagoAsync(string estadoPago);
        Task<decimal> CalcularTotalPorCitaAsync(int idCita);
        Task<IEnumerable<PagoResponseDto>> GetPagosPendientesAsync();
    }
}
