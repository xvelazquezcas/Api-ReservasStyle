using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura_ReservasStyle.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AplicationDbContext _context;

        public EmpleadoRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Empleado> CreateAsync(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.Empleados.Remove(empleado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteEmpleadoIdAsync(int id)
        {
            return await _context.Empleados
                .AnyAsync(e => e.IdUsuario == id);
        }

        public Task<IEnumerable<Empleado>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Empleado?> GetByIdAsync(int id)
        {
            return await _context.Empleados
                .Where(s => s.IdEmpleado == id)
                .FirstOrDefaultAsync();
        // estado && s.EstadoActivo)
        //         .OrderBy(s => s.Nombre)
        //         .ToListAsync();
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            _context.Empleados.Update(empleado);
            await _context.SaveChangesAsync();
        }
    }
}
