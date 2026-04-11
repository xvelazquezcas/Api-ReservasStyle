using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class CitaRepository : ICitaRepository
    {
        private readonly AplicationDbContext _context;

        public CitaRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Citas> CreateAsync(Citas cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return cita;
        }

        public async Task DeleteAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);

            if (cita != null)
            {
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteCitaTraslapadaAsync(int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin)
        {
            return await _context.Citas
                .AnyAsync(c => c.IdEmpleado == empleadoId
                            && c.Fecha == fecha
                            && (
                                horaInicio < c.HoraFin &&
                                horaFin > c.HoraInicio
                            ));
        }

        public async Task<IEnumerable<Citas>> GetAllAsync()
        {
            return await _context.Citas.ToListAsync();
        }

        public async Task<Citas?> GetByIdAsync(int id)
        {
            return await _context.Citas.FindAsync(id);
        }

        public async Task<IEnumerable<Citas>> GetCitasByClienteAsync(int clienteId)
        {
            return await _context.Citas
                .Where(c => c.IdCliente == clienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Citas>> GetCitasByEmpleadoAsync(int empleadoId)
        {
            return await _context.Citas
                .Where(c => c.IdEmpleado == empleadoId)
                .ToListAsync();
        }

        public async Task UpdateAsync(Citas cita)
        {
            _context.Citas.Update(cita);
            await _context.SaveChangesAsync();
        }
    }
}
