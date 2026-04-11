namespace Dominio_ReservasStyle.Entities
{
    public interface INotificacionRepository
    {
        Task<Notificaciones> CreateAsync(Notificaciones notificacion);
        Task<IEnumerable<Notificaciones>> GetByUsuarioIdAsync(int usuarioId);
    }
}