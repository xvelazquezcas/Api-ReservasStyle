using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class ServicioPromocionRepository : IServicioPromocionRepository
    {
        private readonly AplicationDbContext _context;

        public ServicioPromocionRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServicioPromocion?> GetAsync(int idServicioSucursal, int idPromocion)
        {
            return await _context.Set<ServicioPromocion>()
                .Include(sp => sp.Promocion)
                .FirstOrDefaultAsync(sp => sp.IdServicioSucursal == idServicioSucursal 
                    && sp.IdPromocion == idPromocion);
        }

        public async Task<IEnumerable<ServicioPromocion>> GetAllAsync()
        {
            return await _context.Set<ServicioPromocion>()
                .Include(sp => sp.Promocion)
                .OrderByDescending(sp => sp.FechaAsociacion)
                .ToListAsync();
        }

        public async Task<ServicioPromocion> CreateAsync(ServicioPromocion servicioPromocion)
        {
            _context.Set<ServicioPromocion>().Add(servicioPromocion);
            await _context.SaveChangesAsync();
            return servicioPromocion;
        }

        public async Task UpdateAsync(ServicioPromocion servicioPromocion)
        {
            _context.Set<ServicioPromocion>().Update(servicioPromocion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idServicioSucursal, int idPromocion)
        {
            var servicioPromocion = await GetAsync(idServicioSucursal, idPromocion);
            if (servicioPromocion != null)
            {
                _context.Set<ServicioPromocion>().Remove(servicioPromocion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ServicioPromocion>> GetByIdServicioAsync(int idServicio)
        {
            return await _context.Set<ServicioPromocion>()
                .Where(sp => sp.IdServicioSucursal == idServicio)
                .Include(sp => sp.Promocion)
                .OrderByDescending(sp => sp.FechaAsociacion)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicioPromocion>> GetByIdPromocionAsync(int idPromocion)
        {
            return await _context.Set<ServicioPromocion>()
                .Where(sp => sp.IdPromocion == idPromocion)
                .OrderByDescending(sp => sp.FechaAsociacion)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(int idServicioSucursal, int idPromocion)
        {
            return await _context.Set<ServicioPromocion>()
                .AnyAsync(sp => sp.IdServicioSucursal == idServicioSucursal 
                    && sp.IdPromocion == idPromocion);
        }

        public async Task<int> GetCountByServicioAsync(int idServicio)
        {
            return await _context.Set<ServicioPromocion>()
                .CountAsync(sp => sp.IdServicioSucursal == idServicio);
        }

        public async Task<int> GetCountByPromocionAsync(int idPromocion)
        {
            return await _context.Set<ServicioPromocion>()
                .CountAsync(sp => sp.IdPromocion == idPromocion);
        }
    }
}
