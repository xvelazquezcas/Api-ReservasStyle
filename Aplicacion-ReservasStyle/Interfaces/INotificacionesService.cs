using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface INotificacionesService
    {
        Task<IEnumerable<NotificacionesResponseDto>> GetAllAsync();
        Task<NotificacionesResponseDto> GetByIdAsync(int id);
        Task<NotificacionesResponseDto> CreateAsync(CrearNotificacionesDto dto);
        Task<NotificacionesResponseDto> UpdateAsync(int id, ActualizarNotificacionesDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<NotificacionesResponseDto>> GetByIdUsuarioAsync(int idUsuario);
        Task<IEnumerable<NotificacionesResponseDto>> GetNoLeidasAsync();
        Task<IEnumerable<NotificacionesResponseDto>> GetNoLeidasByUsuarioAsync(int idUsuario);
        Task<bool> MarkAsReadAsync(int id);
        Task<bool> MarkAllAsReadByUsuarioAsync(int idUsuario);
    }
}
