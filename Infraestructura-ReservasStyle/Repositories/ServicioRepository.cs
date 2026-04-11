using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly AplicationDbContext _context;

        public ServicioRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Servicio?> GetByIdAsync(int id)
        {
            return await _context.Servicios.FindAsync(id);
        }

        public async Task<IEnumerable<Servicio>> GetAllAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio> CreateAsync(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
            return servicio;
        }

        public async Task UpdateAsync(Servicio servicio)
        {
            _context.Servicios.Update(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
