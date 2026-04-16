using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class ServicioSucursalRepository : IServicioSucursalRepository
    {
        private readonly AplicationDbContext _context;

        public ServicioSucursalRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicioSucursal>> GetAllAsync()
        {
            return await _context.ServicioSucursales
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.IdSucursal)
                .ThenBy(ss => ss.IdServicio)
                .ToListAsync();
        }

        public async Task<ServicioSucursal?> GetByIdAsync(int idServicioSucursal)
        {
            return await _context.ServicioSucursales
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .FirstOrDefaultAsync(ss => ss.IdServicioSucursal == idServicioSucursal);
        }

        public async Task<ServicioSucursal> CreateAsync(ServicioSucursal servicioSucursal)
        {
            _context.ServicioSucursales.Add(servicioSucursal);
            await _context.SaveChangesAsync();
            return servicioSucursal;
        }

        public async Task UpdateAsync(ServicioSucursal servicioSucursal)
        {
            _context.ServicioSucursales.Update(servicioSucursal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int idServicioSucursal)
        {
            var servicioSucursal = await _context.ServicioSucursales.FindAsync(idServicioSucursal);
            if (servicioSucursal != null)
            {
                _context.ServicioSucursales.Remove(servicioSucursal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ServicioSucursal?> GetByServicioAndSucursalAsync(int idServicio, int idSucursal)
        {
            return await _context.ServicioSucursales
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .FirstOrDefaultAsync(ss => ss.IdServicio == idServicio && ss.IdSucursal == idSucursal);
        }

        public async Task<IEnumerable<ServicioSucursal>> GetByServicioAsync(int idServicio)
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.IdServicio == idServicio)
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.IdSucursal)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicioSucursal>> GetBySucursalAsync(int idSucursal)
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.IdSucursal == idSucursal)
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.IdServicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicioSucursal>> GetActivosAsync()
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.Estado)
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.IdSucursal)
                .ThenBy(ss => ss.IdServicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicioSucursal>> GetActivosBySucursalAsync(int idSucursal)
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.IdSucursal == idSucursal && ss.Estado)
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.IdServicio)
                .ToListAsync();
        }

        public async Task<bool> ExisteAsync(int idServicio, int idSucursal)
        {
            return await _context.ServicioSucursales
                .AnyAsync(ss => ss.IdServicio == idServicio && ss.IdSucursal == idSucursal);
        }

        public async Task<decimal?> GetPrecioAsync(int idServicio, int idSucursal)
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.IdServicio == idServicio && ss.IdSucursal == idSucursal)
                .Select(ss => ss.Precio)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ServicioSucursal>> GetByRangoPrecioAsync(decimal precioMin, decimal precioMax)
        {
            return await _context.ServicioSucursales
                .Where(ss => ss.Precio >= precioMin && ss.Precio <= precioMax)
                .Include(ss => ss.Servicio)
                .Include(ss => ss.Sucursal)
                .OrderBy(ss => ss.Precio)
                .ToListAsync();
        }

        public async Task<decimal> GetPrecioPromedioPorServicioAsync(int idServicio)
        {
            var promedio = await _context.ServicioSucursales
                .Where(ss => ss.IdServicio == idServicio)
                .AverageAsync(ss => ss.Precio);

            return promedio;
        }

        public async Task<int> GetCountBySucursalAsync(int idSucursal)
        {
            return await _context.ServicioSucursales
                .CountAsync(ss => ss.IdSucursal == idSucursal);
        }
    }
}
