using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface ILogRepository
    {
        // CRUD básico
        Task<IEnumerable<Log>> GetAllAsync();
        Task<Log?> GetByIdAsync(int idLog);
        Task<Log> CreateAsync(Log log);
        Task UpdateAsync(Log log);
        Task DeleteAsync(int idLog);
        
        // Consultas especializadas
        Task<IEnumerable<Log>> GetByIdUsuarioAsync(int idUsuario);
        Task<IEnumerable<Log>> GetByAccionAsync(string accion);
        Task<IEnumerable<Log>> GetByEntidadAsync(string entidad);
        Task<IEnumerable<Log>> GetByEntidadYIdAsync(string entidad, int idEntidad);
        Task<IEnumerable<Log>> GetPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Log>> GetConFiltrosAsync(
            int? idUsuario = null,
            string? accion = null,
            string? entidad = null,
            bool? exitoso = null,
            DateTime? fechaInicio = null,
            DateTime? fechaFin = null);
        Task<IEnumerable<Log>> GetUltimosLogsAsync(int cantidad = 100);
        Task<int> GetCountAsync();
        Task<decimal> GetAccionesExitosasAsync();
        Task<string> GetAccionMasFrecuenteAsync();
        Task<int> GetCountByFechaAsync(DateTime fecha);
    }
}
