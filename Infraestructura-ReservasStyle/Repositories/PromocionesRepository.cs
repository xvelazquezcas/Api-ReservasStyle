using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class PromocionesRepository : IPromocionesRepository
    {
        private readonly AplicationDbContext _context;

        public PromocionesRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Promociones> CreateAsync(Promociones promocion)
        {
            _context.Promociones.Add(promocion);
            await _context.SaveChangesAsync();
            return promocion;
        }

        public async Task DeleteAsync(int id)
        {
            var promocion = await _context.Promociones.FindAsync(id);
            if (promocion != null)
            {
                _context.Promociones.Remove(promocion);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Promociones>> GetAllAsync()
        {
            return await _context.Promociones
                .OrderBy(p => p.FechaInicio)
                .ToListAsync();
        }

        public async Task<Promociones?> GetByIdAsync(int id)
        {
            return await _context.Promociones
                .FirstOrDefaultAsync(p => p.IdPromocion == id);
        }

        public async Task UpdateAsync(Promociones promocion)
        {
            _context.Promociones.Update(promocion);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _context.Promociones
                .AnyAsync(p => p.Nombre == nombre);
        }

        public async Task<IEnumerable<Promociones>> GetActivasAsync()
        {
            return await _context.Promociones
                .Where(p => p.Estado)
                .OrderBy(p => p.FechaInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<Promociones>> GetVigentesAsync()
        {
            var ahora = DateTime.Now;
            return await _context.Promociones
                .Where(p => p.Estado && p.FechaInicio <= ahora && ahora <= p.FechaFin)
                .OrderBy(p => p.FechaFin)
                .ToListAsync();
        }

        public async Task<IEnumerable<Promociones>> GetProximasAsync(int dias = 7)
        {
            var ahora = DateTime.Now;
            var fecha = ahora.AddDays(dias);
            
            return await _context.Promociones
                .Where(p => p.Estado && p.FechaInicio > ahora && p.FechaInicio <= fecha)
                .OrderBy(p => p.FechaInicio)
                .ToListAsync();
        }

        public async Task<bool> IsPromocionVigenteAsync(int idPromocion)
        {
            var ahora = DateTime.Now;
            return await _context.Promociones
                .AnyAsync(p => p.IdPromocion == idPromocion 
                    && p.Estado 
                    && p.FechaInicio <= ahora 
                    && ahora <= p.FechaFin);
        }
    }
}
