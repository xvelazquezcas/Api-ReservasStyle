using AutoMapper;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMapper _mapper;

        public LogService(ILogRepository logRepository, IMapper mapper)
        {
            _logRepository = logRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los logs
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetAllAsync()
        {
            var logs = await _logRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene un log por ID
        /// </summary>
        public async Task<LogResponseDto> GetByIdAsync(int idLog)
        {
            var log = await _logRepository.GetByIdAsync(idLog);
            if (log == null)
                throw new KeyNotFoundException($"Log con ID {idLog} no encontrado");

            return _mapper.Map<LogResponseDto>(log);
        }

        /// <summary>
        /// Crea un nuevo log
        /// </summary>
        public async Task<LogResponseDto> CreateAsync(CrearLogDto dto)
        {
            // ✅ VALIDACIONES
            if (string.IsNullOrWhiteSpace(dto.Accion))
                throw new InvalidOperationException("La acción es requerida");

            if (string.IsNullOrWhiteSpace(dto.Entidad))
                throw new InvalidOperationException("La entidad es requerida");

            // ✅ MAPEO DTO → ENTIDAD
            var log = _mapper.Map<Log>(dto);

            // ✅ PERSISTENCIA
            await _logRepository.CreateAsync(log);

            // ✅ MAPEO ENTIDAD → DTO RESPUESTA
            return _mapper.Map<LogResponseDto>(log);
        }

        /// <summary>
        /// Elimina un log (para limpiar logs antiguos)
        /// </summary>
        public async Task<bool> DeleteAsync(int idLog)
        {
            var log = await _logRepository.GetByIdAsync(idLog);
            if (log == null)
                throw new KeyNotFoundException($"Log con ID {idLog} no encontrado");

            await _logRepository.DeleteAsync(idLog);
            return true;
        }

        /// <summary>
        /// Obtiene todos los logs de un usuario
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetByIdUsuarioAsync(int idUsuario)
        {
            var logs = await _logRepository.GetByIdUsuarioAsync(idUsuario);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene todos los logs de una acción específica
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetByAccionAsync(string accion)
        {
            if (string.IsNullOrWhiteSpace(accion))
                throw new InvalidOperationException("La acción es requerida");

            var logs = await _logRepository.GetByAccionAsync(accion);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene todos los logs de una entidad
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetByEntidadAsync(string entidad)
        {
            if (string.IsNullOrWhiteSpace(entidad))
                throw new InvalidOperationException("La entidad es requerida");

            var logs = await _logRepository.GetByEntidadAsync(entidad);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene el historial de cambios de un registro específico
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetByEntidadYIdAsync(string entidad, int idEntidad)
        {
            if (string.IsNullOrWhiteSpace(entidad))
                throw new InvalidOperationException("La entidad es requerida");

            var logs = await _logRepository.GetByEntidadYIdAsync(entidad, idEntidad);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene logs dentro de un rango de fechas
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetPorFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
                throw new InvalidOperationException("La fecha inicio no puede ser mayor que la fecha fin");

            var logs = await _logRepository.GetPorFechaAsync(fechaInicio, fechaFin);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene logs con filtros complejos
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> FiltrarLogsAsync(FiltroLogsDto filtro)
        {
            if (filtro.PageNumber < 1 || filtro.PageSize < 1 || filtro.PageSize > 1000)
                throw new InvalidOperationException("Parámetros de paginación inválidos");

            var logs = await _logRepository.GetConFiltrosAsync(
                filtro.IdUsuario,
                filtro.Accion,
                filtro.Entidad,
                filtro.Exitoso,
                filtro.FechaInicio,
                filtro.FechaFin);

            var result = logs
                .Skip((filtro.PageNumber - 1) * filtro.PageSize)
                .Take(filtro.PageSize);

            return _mapper.Map<IEnumerable<LogResponseDto>>(result);
        }

        /// <summary>
        /// Obtiene los últimos N logs
        /// </summary>
        public async Task<IEnumerable<LogResponseDto>> GetUltimosLogsAsync(int cantidad = 100)
        {
            if (cantidad < 1 || cantidad > 10000)
                throw new InvalidOperationException("La cantidad debe estar entre 1 y 10000");

            var logs = await _logRepository.GetUltimosLogsAsync(cantidad);
            return _mapper.Map<IEnumerable<LogResponseDto>>(logs);
        }

        /// <summary>
        /// Obtiene el total de logs
        /// </summary>
        public async Task<int> GetCountAsync()
        {
            return await _logRepository.GetCountAsync();
        }

        /// <summary>
        /// Obtiene estadísticas generales de logs
        /// </summary>
        public async Task<LogEstadisticasDto> GetEstadisticasAsync()
        {
            var totalLogs = await _logRepository.GetCountAsync();
            var logsExitosos = (await _logRepository.GetConFiltrosAsync(exitoso: true)).Count();
            var acciones = await _logRepository.GetAllAsync();
            
            var logsPorAccion = acciones
                .GroupBy(l => l.Accion)
                .ToDictionary(g => g.Key, g => g.Count());

            var logsPorEntidad = acciones
                .GroupBy(l => l.Entidad)
                .ToDictionary(g => g.Key, g => g.Count());

            var ahora = DateTime.Now;
            var hoy = await _logRepository.GetCountByFechaAsync(ahora.Date);
            var estaSemana = (await _logRepository.GetPorFechaAsync(
                ahora.Date.AddDays(-(int)ahora.DayOfWeek),
                ahora.Date.AddDays(1))).Count();
            var esteMes = (await _logRepository.GetPorFechaAsync(
                new DateTime(ahora.Year, ahora.Month, 1),
                ahora.Date.AddDays(1))).Count();

            return new LogEstadisticasDto
            {
                TotalLogs = totalLogs,
                PorcentajeExitosos = totalLogs > 0 ? (logsExitosos * 100m / totalLogs) : 0,
                AccionMasFrecuente = logsPorAccion.OrderByDescending(x => x.Value).FirstOrDefault().Key ?? "N/A",
                LogsHoy = hoy,
                LogsEstaSemanä = estaSemana,
                LogsEsteMes = esteMes,
                LogsPorAccion = logsPorAccion,
                LogsPorEntidad = logsPorEntidad
            };
        }

        /// <summary>
        /// Registra una acción (método auxiliar para usar en toda la aplicación)
        /// </summary>
        public async Task RegistrarAccionAsync(
            string accion,
            string entidad,
            int? idEntidad = null,
            int? idUsuario = null,
            string? detalleAntes = null,
            string? detalleDepues = null,
            string? direccionIP = null,
            string? userAgent = null,
            bool exitoso = true,
            string? mensajeError = null)
        {
            var log = new Log
            {
                Accion = accion,
                Entidad = entidad,
                IdEntidad = idEntidad,
                IdUsuario = idUsuario,
                DetalleAntes = detalleAntes,
                DetalleDepues = detalleDepues,
                DireccionIP = direccionIP,
                UserAgent = userAgent,
                Exitoso = exitoso,
                MensajeError = mensajeError,
                FechaRegistro = DateTime.Now
            };

            await _logRepository.CreateAsync(log);
        }
    }
}
