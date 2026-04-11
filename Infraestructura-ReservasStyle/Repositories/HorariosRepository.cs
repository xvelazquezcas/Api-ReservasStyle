using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{

    public class HorariosRepository : IHorariosRepository
    {
        private readonly AplicationDbContext _context;

        public HorariosRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HorariosDisponibles?> GetHorarioAsync(int empleadoId, DiaSemana dia)
        {
            return await _context.HorariosDisponibles
                .FirstOrDefaultAsync(h => h.IdEmpleado == empleadoId && h.DiaSemana == dia);
        }
    }
}
