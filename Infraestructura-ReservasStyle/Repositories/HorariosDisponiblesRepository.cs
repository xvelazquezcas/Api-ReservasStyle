using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class HorariosDisponiblesRepository : IHorariosDisponiblesRepository
    {
        private readonly AplicationDbContext _context;

        public HorariosDisponiblesRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HorariosDisponibles> CreateAsync(HorariosDisponibles horario)
        {
            _context.HorariosDisponibles.Add(horario);
            await _context.SaveChangesAsync();
            return horario;
        }

        public async Task DeleteAsync(int id)
        {
            var horario = await _context.HorariosDisponibles.FindAsync(id);
            if (horario != null)
            {
                _context.HorariosDisponibles.Remove(horario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HorariosDisponibles>> GetAllAsync()
        {
            return await _context.HorariosDisponibles
                .OrderBy(h => h.IdEmpleado)
                .ThenBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .ToListAsync();
        }

        public async Task<HorariosDisponibles?> GetByIdAsync(int id)
        {
            return await _context.HorariosDisponibles
                .FirstOrDefaultAsync(h => h.IdHorario == id);
        }

        public async Task UpdateAsync(HorariosDisponibles horario)
        {
            _context.HorariosDisponibles.Update(horario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteHorarioAsync(int idEmpleado, DiaSemana diaSemana)
        {
            return await _context.HorariosDisponibles
                .AnyAsync(h => h.IdEmpleado == idEmpleado && h.DiaSemana == diaSemana);
        }

        public async Task<IEnumerable<HorariosDisponibles>> GetByEmpleadoAsync(int idEmpleado)
        {
            return await _context.HorariosDisponibles
                .Where(h => h.IdEmpleado == idEmpleado)
                .OrderBy(h => h.DiaSemana)
                .ThenBy(h => h.HoraInicio)
                .ToListAsync();
        }

        public async Task<IEnumerable<HorariosDisponibles>> GetByDiaAsync(DiaSemana dia)
        {
            return await _context.HorariosDisponibles
                .Where(h => h.DiaSemana == dia)
                .OrderBy(h => h.IdEmpleado)
                .ThenBy(h => h.HoraInicio)
                .ToListAsync();
        }
    }
}
