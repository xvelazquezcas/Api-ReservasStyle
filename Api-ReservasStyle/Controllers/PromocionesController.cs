using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PromocionesController : ControllerBase
    {
        private readonly IPromocionesService _promocionesService;

        public PromocionesController(IPromocionesService promocionesService)
        {
            _promocionesService = promocionesService;
        }

        /// <summary>
        /// Obtener todas las promociones
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var promociones = await _promocionesService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Promociones obtenidas correctamente",
                    data = promociones
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
        /// Obtener una promoción por ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var promocion = await _promocionesService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Promoción obtenida correctamente",
                    data = promocion
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Promoción con ID {id} no encontrada"
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
        /// Obtener promociones activas
        /// </summary>
        [HttpGet("activas")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivas()
        {
            try
            {
                var promociones = await _promocionesService.GetActivasAsync();
                return Ok(new
                {
                    success = true,
                    message = "Promociones activas obtenidas correctamente",
                    count = promociones.Count(),
                    data = promociones
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
        /// Obtener promociones vigentes (en curso)
        /// </summary>
        [HttpGet("vigentes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVigentes()
        {
            try
            {
                var promociones = await _promocionesService.GetVigentesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Promociones vigentes obtenidas correctamente",
                    count = promociones.Count(),
                    data = promociones
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
        /// Obtener promociones próximas (que comienzan pronto)
        /// </summary>
        [HttpGet("proximas/{dias:int?}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProximas(int dias = 7)
        {
            try
            {
                var promociones = await _promocionesService.GetProximasAsync(dias);
                return Ok(new
                {
                    success = true,
                    message = $"Promociones próximas en los próximos {dias} días obtenidas correctamente",
                    count = promociones.Count(),
                    data = promociones
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
        /// Verificar si una promoción está vigente
        /// </summary>
        [HttpGet("{id}/vigente")]
        [AllowAnonymous]
        public async Task<IActionResult> IsVigente(int id)
        {
            try
            {
                var esVigente = await _promocionesService.IsPromocionVigenteAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Verificación completada",
                    data = new { idPromocion = id, esVigente }
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
        /// Crear una nueva promoción
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearPromocionesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _promocionesService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdPromocion },
                    new
                    {
                        success = true,
                        message = "Promoción creada correctamente",
                        data = resultado
                    });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
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
        /// Actualizar una promoción
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarPromocionesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdPromocion)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _promocionesService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Promoción actualizada correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Promoción con ID {id} no encontrada"
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
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
        /// Eliminar una promoción
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _promocionesService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Promoción con ID {id} no encontrada"
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
