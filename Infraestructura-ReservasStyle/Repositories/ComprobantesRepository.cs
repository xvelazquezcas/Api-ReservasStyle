using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class ComprobantesRepository : IComprobantesRepository
    {
        private readonly AplicationDbContext _context;

        public ComprobantesRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comprobantes> CreateAsync(Comprobantes comprobante)
        {
            _context.Comprobantes.Add(comprobante);
            await _context.SaveChangesAsync();
            return comprobante;
        }

        public async Task DeleteAsync(int id)
        {
            var comprobante = await _context.Comprobantes.FindAsync(id);
            if (comprobante != null)
            {
                _context.Comprobantes.Remove(comprobante);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Comprobantes>> GetAllAsync()
        {
            return await _context.Comprobantes
                .OrderByDescending(c => c.FechaEmision)
                .ToListAsync();
        }

        public async Task<Comprobantes?> GetByIdAsync(int id)
        {
            return await _context.Comprobantes
                .FirstOrDefaultAsync(c => c.IdComprobante == id);
        }

        public async Task UpdateAsync(Comprobantes comprobante)
        {
            _context.Comprobantes.Update(comprobante);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteFolioAsync(string folio)
        {
            return await _context.Comprobantes
                .AnyAsync(c => c.Folio == folio);
        }

        public async Task<IEnumerable<Comprobantes>> GetByIdPagoAsync(int idPago)
        {
            return await _context.Comprobantes
                .Where(c => c.IdPago == idPago)
                .OrderByDescending(c => c.FechaEmision)
                .ToListAsync();
        }
    }
}
