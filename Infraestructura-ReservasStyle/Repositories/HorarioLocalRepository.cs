using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class HorarioLocalRepository : IHorarioLocalRepository
    {
        private readonly AplicationDbContext _context;

        public HorarioLocalRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HorarioLocal> CreateAsync(HorarioLocal horarioLocal)
        {
            _context.HorariosLocales.Add(horarioLocal);
            await _context.SaveChangesAsync();
            return horarioLocal;
        }

        public async Task DeleteAsync(int id)
        {
            var horarioLocal = await _context.HorariosLocales.FindAsync(id);
            if (horarioLocal != null)
            {
                _context.HorariosLocales.Remove(horarioLocal);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HorarioLocal>> GetAllAsync()
        {
            return await _context.HorariosLocales
                .OrderBy(h => h.IdSucursal)
                .ThenBy(h => h.DiaSemana)
                .ToListAsync();
        }

        public async Task<HorarioLocal?> GetByIdAsync(int id)
        {
            return await _context.HorariosLocales
                .FirstOrDefaultAsync(h => h.IdHorarioLocal == id);
        }

        public async Task UpdateAsync(HorarioLocal horarioLocal)
        {
            _context.HorariosLocales.Update(horarioLocal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HorarioLocal>> GetByIdSucursalAsync(int idSucursal)
        {
            return await _context.HorariosLocales
                .Where(h => h.IdSucursal == idSucursal)
                .OrderBy(h => h.DiaSemana)
                .ToListAsync();
        }

        public async Task<HorarioLocal?> GetByIdSucursalAndDiaAsync(int idSucursal, string dia)
        {
            return await _context.HorariosLocales
                .FirstOrDefaultAsync(h => h.IdSucursal == idSucursal && h.DiaSemana == dia);
        }
    }
}
