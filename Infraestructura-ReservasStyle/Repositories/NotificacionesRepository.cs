using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class NotificacionesRepository : INotificacionesRepository
    {
        private readonly AplicationDbContext _context;

        public NotificacionesRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notificaciones> CreateAsync(Notificaciones notificacion)
        {
            _context.Notificaciones.Add(notificacion);
            await _context.SaveChangesAsync();
            return notificacion;
        }

        public async Task DeleteAsync(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion != null)
            {
                _context.Notificaciones.Remove(notificacion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Notificaciones>> GetAllAsync()
        {
            return await _context.Notificaciones
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();
        }

        public async Task<Notificaciones?> GetByIdAsync(int id)
        {
            return await _context.Notificaciones
                .FirstOrDefaultAsync(n => n.IdNotificacion == id);
        }

        public async Task UpdateAsync(Notificaciones notificacion)
        {
            _context.Notificaciones.Update(notificacion);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notificaciones>> GetByIdUsuarioAsync(int idUsuario)
        {
            return await _context.Notificaciones
                .Where(n => n.IdUsuario == idUsuario)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notificaciones>> GetNoLeidasAsync()
        {
            return await _context.Notificaciones
                .Where(n => !n.Leida)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notificaciones>> GetNoLeidasByUsuarioAsync(int idUsuario)
        {
            return await _context.Notificaciones
                .Where(n => n.IdUsuario == idUsuario && !n.Leida)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notificacion = await _context.Notificaciones.FindAsync(id);
            if (notificacion != null)
            {
                notificacion.Leida = true;
                _context.Notificaciones.Update(notificacion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAsReadByUsuarioAsync(int idUsuario)
        {
            var notificaciones = await _context.Notificaciones
                .Where(n => n.IdUsuario == idUsuario && !n.Leida)
                .ToListAsync();

            foreach (var notificacion in notificaciones)
            {
                notificacion.Leida = true;
            }

            _context.Notificaciones.UpdateRange(notificaciones);
            await _context.SaveChangesAsync();
        }
    }
}
