using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly AplicationDbContext _context;

        public PagoRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Pago?> GetByIdAsync(int id)
        {
            return await _context.Pagos.FindAsync(id);
        }

        public async Task<IEnumerable<Pago>> GetAllAsync()
        {
            return await _context.Pagos.ToListAsync();
        }

        public async Task<Pago> CreateAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task UpdateAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }
}
