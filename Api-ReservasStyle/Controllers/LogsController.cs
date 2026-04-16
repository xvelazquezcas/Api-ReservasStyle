using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Obtener todos los logs (solo Admin)
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _logService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Logs obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener un log por ID
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var log = await _logService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Log obtenido correctamente",
                    data = log
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Log con ID {id} no encontrado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener logs de un usuario
        /// </summary>
        [HttpGet("usuario/{idUsuario}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByIdUsuario(int idUsuario)
        {
            try
            {
                var logs = await _logService.GetByIdUsuarioAsync(idUsuario);
                return Ok(new
                {
                    success = true,
                    message = "Logs del usuario obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener logs por acción
        /// </summary>
        [HttpGet("accion/{accion}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByAccion(string accion)
        {
            try
            {
                var logs = await _logService.GetByAccionAsync(accion);
                return Ok(new
                {
                    success = true,
                    message = $"Logs de la acción '{accion}' obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener logs por entidad
        /// </summary>
        [HttpGet("entidad/{entidad}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByEntidad(string entidad)
        {
            try
            {
                var logs = await _logService.GetByEntidadAsync(entidad);
                return Ok(new
                {
                    success = true,
                    message = $"Logs de la entidad '{entidad}' obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener historial de cambios de un registro
        /// </summary>
        [HttpGet("historial/{entidad}/{idEntidad}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetHistorial(string entidad, int idEntidad)
        {
            try
            {
                var logs = await _logService.GetByEntidadYIdAsync(entidad, idEntidad);
                return Ok(new
                {
                    success = true,
                    message = $"Historial de cambios de {entidad} ID {idEntidad} obtenido correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener logs dentro de un rango de fechas
        /// </summary>
        [HttpGet("fecha/{fechaInicio}/{fechaFin}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var logs = await _logService.GetPorFechaAsync(fechaInicio, fechaFin);
                return Ok(new
                {
                    success = true,
                    message = $"Logs entre {fechaInicio:dd/MM/yyyy} y {fechaFin:dd/MM/yyyy} obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener logs con filtros avanzados
        /// </summary>
        [HttpPost("filtrar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filtrar([FromBody] FiltroLogsDto filtro)
        {
            try
            {
                var logs = await _logService.FiltrarLogsAsync(filtro);
                return Ok(new
                {
                    success = true,
                    message = "Logs filtrados obtenidos correctamente",
                    count = logs.Count(),
                    pageNumber = filtro.PageNumber,
                    pageSize = filtro.PageSize,
                    data = logs
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener los últimos N logs
        /// </summary>
        [HttpGet("ultimos/{cantidad}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUltimos(int cantidad = 100)
        {
            try
            {
                var logs = await _logService.GetUltimosLogsAsync(cantidad);
                return Ok(new
                {
                    success = true,
                    message = $"Últimos {cantidad} logs obtenidos correctamente",
                    count = logs.Count(),
                    data = logs
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener estadísticas de logs
        /// </summary>
        [HttpGet("estadisticas")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEstadisticas()
        {
            try
            {
                var estadisticas = await _logService.GetEstadisticasAsync();
                return Ok(new
                {
                    success = true,
                    message = "Estadísticas de logs obtenidas correctamente",
                    data = estadisticas
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtener recuento total de logs
        /// </summary>
        [HttpGet("count")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCount()
        {
            try
            {
                var count = await _logService.GetCountAsync();
                return Ok(new
                {
                    success = true,
                    message = "Recuento de logs obtenido correctamente",
                    data = new { totalLogs = count }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Eliminar un log antiguo (limpieza)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _logService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Log con ID {id} no encontrado"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
