using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionesService _notificacionesService;

        public NotificacionesController(INotificacionesService notificacionesService)
        {
            _notificacionesService = notificacionesService;
        }

        /// <summary>
        /// Obtener todas las notificaciones
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notificaciones = await _notificacionesService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Notificaciones obtenidas correctamente",
                    data = notificaciones
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
        /// Obtener una notificación por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var notificacion = await _notificacionesService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Notificación obtenida correctamente",
                    data = notificacion
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Notificación con ID {id} no encontrada"
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
        /// Obtener notificaciones por usuario
        /// </summary>
        [HttpGet("por-usuario/{idUsuario}")]
        public async Task<IActionResult> GetByIdUsuario(int idUsuario)
        {
            try
            {
                var notificaciones = await _notificacionesService.GetByIdUsuarioAsync(idUsuario);
                return Ok(new
                {
                    success = true,
                    message = "Notificaciones obtenidas correctamente",
                    data = notificaciones
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
        /// Obtener todas las notificaciones no leídas
        /// </summary>
        [HttpGet("no-leidas")]
        public async Task<IActionResult> GetNoLeidas()
        {
            try
            {
                var notificaciones = await _notificacionesService.GetNoLeidasAsync();
                return Ok(new
                {
                    success = true,
                    message = "Notificaciones no leídas obtenidas correctamente",
                    data = notificaciones
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
        /// Obtener notificaciones no leídas de un usuario
        /// </summary>
        [HttpGet("no-leidas-usuario/{idUsuario}")]
        public async Task<IActionResult> GetNoLeidasByUsuario(int idUsuario)
        {
            try
            {
                var notificaciones = await _notificacionesService.GetNoLeidasByUsuarioAsync(idUsuario);
                return Ok(new
                {
                    success = true,
                    message = "Notificaciones no leídas obtenidas correctamente",
                    count = notificaciones.Count(),
                    data = notificaciones
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
        /// Crear una nueva notificación
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearNotificacionesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _notificacionesService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdNotificacion },
                    new
                    {
                        success = true,
                        message = "Notificación creada correctamente",
                        data = resultado
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
        /// Actualizar una notificación
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarNotificacionesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdNotificacion)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _notificacionesService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Notificación actualizada correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Notificación con ID {id} no encontrada"
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
        /// Marcar una notificación como leída
        /// </summary>
        [HttpPut("{id}/marcar-leida")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            try
            {
                await _notificacionesService.MarkAsReadAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Notificación marcada como leída"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Notificación con ID {id} no encontrada"
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
        /// Marcar todas las notificaciones de un usuario como leídas
        /// </summary>
        [HttpPut("marcar-todas-leidas/{idUsuario}")]
        public async Task<IActionResult> MarkAllAsRead(int idUsuario)
        {
            try
            {
                await _notificacionesService.MarkAllAsReadByUsuarioAsync(idUsuario);
                return Ok(new
                {
                    success = true,
                    message = "Todas las notificaciones del usuario marcadas como leídas"
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
        /// Eliminar una notificación
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _notificacionesService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Notificación con ID {id} no encontrada"
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
