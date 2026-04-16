using Aplicacion_ReservasStyle.DTOs;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface ILogService
    {
        // CRUD
        Task<IEnumerable<LogResponseDto>> GetAllAsync();
        Task<LogResponseDto> GetByIdAsync(int idLog);
        Task<LogResponseDto> CreateAsync(CrearLogDto dto);
        Task<bool> DeleteAsync(int idLog);
        
        // Consultas especializadas
        Task<IEnumerable<LogResponseDto>> GetByIdUsuarioAsync(int idUsuario);
        Task<IEnumerable<LogResponseDto>> GetByAccionAsync(string accion);
        Task<IEnumerable<LogResponseDto>> GetByEntidadAsync(string entidad);
        Task<IEnumerable<LogResponseDto>> GetByEntidadYIdAsync(string entidad, int idEntidad);
        Task<IEnumerable<LogResponseDto>> GetPorFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<LogResponseDto>> FiltrarLogsAsync(FiltroLogsDto filtro);
        Task<IEnumerable<LogResponseDto>> GetUltimosLogsAsync(int cantidad = 100);
        Task<int> GetCountAsync();
        Task<LogEstadisticasDto> GetEstadisticasAsync();
        
        // Métodos auxiliares para registro automático
        Task RegistrarAccionAsync(
            string accion,
            string entidad,
            int? idEntidad = null,
            int? idUsuario = null,
            string? detalleAntes = null,
            string? detalleDepues = null,
            string? direccionIP = null,
            string? userAgent = null,
            bool exitoso = true,
            string? mensajeError = null);
    }
}
