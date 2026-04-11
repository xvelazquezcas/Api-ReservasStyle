using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface ICitaRepository
        {
            //Esto va ser util para ver citas de un cliente y ver agenda del barbero
            Task<Citas?> GetByIdAsync(int id);
            Task<IEnumerable<Citas>> GetAllAsync();
            Task<IEnumerable<Citas>> GetCitasByClienteAsync(int clienteId);
            Task<IEnumerable<Citas>> GetCitasByEmpleadoAsync(int empleadoId);
            Task<Citas> CreateAsync(Citas cita);
            Task<bool> ExisteCitaTraslapadaAsync(int empleadoId, DateTime fecha, TimeSpan horaInicio, TimeSpan horaFin);
            Task UpdateAsync(Citas cita);
            Task DeleteAsync(int id);
        }
}