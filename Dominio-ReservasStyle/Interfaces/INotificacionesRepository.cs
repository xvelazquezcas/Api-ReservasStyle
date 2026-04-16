using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface INotificacionesRepository
    {
        Task<IEnumerable<Notificaciones>> GetAllAsync();
        Task<Notificaciones?> GetByIdAsync(int id);
        Task<Notificaciones> CreateAsync(Notificaciones notificacion);
        Task UpdateAsync(Notificaciones notificacion);
        Task DeleteAsync(int id);
        Task<IEnumerable<Notificaciones>> GetByIdUsuarioAsync(int idUsuario);
        Task<IEnumerable<Notificaciones>> GetNoLeidasAsync();
        Task<IEnumerable<Notificaciones>> GetNoLeidasByUsuarioAsync(int idUsuario);
        Task MarkAsReadAsync(int id);
        Task MarkAllAsReadByUsuarioAsync(int idUsuario);
    }
}
