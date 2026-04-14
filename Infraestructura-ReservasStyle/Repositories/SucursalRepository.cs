using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class SucursalRepository : ISucursalRepository
    {
        private readonly AplicationDbContext _context;

        public SucursalRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Sucursal> CreateAsync(Sucursal sucursal)
        {
            _context.Sucursales.Add(sucursal);
            await _context.SaveChangesAsync();
            return sucursal;
        }

        public async Task DeleteAsync(int id)
        {
            var sucursal = await _context.Sucursales.FindAsync(id);
            if (sucursal != null)
            {
                _context.Sucursales.Remove(sucursal);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Sucursal>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Sucursal?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Sucursal sucursal)
        {
            _context.Sucursales.Update(sucursal);
            await _context.SaveChangesAsync();
        }

        // ✅ Métodos adicionales para validaciones
        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Sucursales
                .AnyAsync(s => s.Nombre == nombre);
        }

        public async Task<IEnumerable<Sucursal>> GetByEstadoAsync(string estado)
        {
            return await _context.Sucursales
                .Where(s => s.Estado == estado && s.EstadoActivo)
                .OrderBy(s => s.Nombre)
                .ToListAsync();
        }
    }
}