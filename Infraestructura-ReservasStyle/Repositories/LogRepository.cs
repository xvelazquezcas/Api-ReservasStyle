using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly AplicationDbContext _context;

        public LogRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {
            return await _context.Logs
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<Log?> GetByIdAsync(int idLog)
        {
            return await _context.Logs
                .FirstOrDefaultAsync(l => l.IdLog == idLog);
        }

        public async Task<Log> CreateAsync(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return log;
        }

        public async Task UpdateAsync(Log log)
        {
            _context.Logs.Update(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idLog)
        {
            var log = await _context.Logs.FindAsync(idLog);
            if (log != null)
            {
                _context.Logs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Log>> GetByIdUsuarioAsync(int idUsuario)
        {
            return await _context.Logs
                .Where(l => l.IdUsuario == idUsuario)
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetByAccionAsync(string accion)
        {
            return await _context.Logs
                .Where(l => l.Accion == accion)
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetByEntidadAsync(string entidad)
        {
            return await _context.Logs
                .Where(l => l.Entidad == entidad)
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetByEntidadYIdAsync(string entidad, int idEntidad)
        {
            return await _context.Logs
                .Where(l => l.Entidad == entidad && l.IdEntidad == idEntidad)
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Logs
                .Where(l => l.FechaRegistro >= fechaInicio && l.FechaRegistro <= fechaFin)
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetConFiltrosAsync(
            int? idUsuario = null,
            string? accion = null,
            string? entidad = null,
            bool? exitoso = null,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null)
        {
            var query = _context.Logs.AsQueryable();

            if (idUsuario.HasValue)
                query = query.Where(l => l.IdUsuario == idUsuario);

            if (!string.IsNullOrWhiteSpace(accion))
                query = query.Where(l => l.Accion == accion);

            if (!string.IsNullOrWhiteSpace(entidad))
                query = query.Where(l => l.Entidad == entidad);

            if (exitoso.HasValue)
                query = query.Where(l => l.Exitoso == exitoso);

            if (fechaInicio.HasValue)
                query = query.Where(l => l.FechaRegistro >= fechaInicio);

            if (fechaFin.HasValue)
                query = query.Where(l => l.FechaRegistro <= fechaFin);

            return await query
                .OrderByDescending(l => l.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<Log>> GetUltimosLogsAsync(int cantidad = 100)
        {
            return await _context.Logs
                .OrderByDescending(l => l.FechaRegistro)
                .Take(cantidad)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Logs.CountAsync();
        }

        public async Task<decimal> GetAccionesExitosasAsync()
        {
            var total = await _context.Logs.CountAsync();
            if (total == 0) return 0;

            var exitosas = await _context.Logs
                .Where(l => l.Exitoso)
                .CountAsync();

            return (exitosas * 100m) / total;
        }

        public async Task<string> GetAccionMasFrecuenteAsync()
        {
            return await _context.Logs
                .GroupBy(l => l.Accion)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync() ?? "N/A";
        }

        public async Task<int> GetCountByFechaAsync(DateTime fecha)
        {
            var fechaSiguiente = fecha.AddDays(1);
            return await _context.Logs
                .Where(l => l.FechaRegistro >= fecha && l.FechaRegistro < fechaSiguiente)
                .CountAsync();
        }
    }
}
