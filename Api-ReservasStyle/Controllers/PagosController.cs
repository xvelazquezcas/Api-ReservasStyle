using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        /// <summary>
        /// Obtener todos los pagos
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pagos = await _pagoService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Pagos obtenidos correctamente",
                    data = pagos
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
        /// Obtener un pago por ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pago = await _pagoService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Pago obtenido correctamente",
                    data = pago
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Pago con ID {id} no encontrado"
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
        /// Obtener pagos por cita
        /// </summary>
        [HttpGet("por-cita/{idCita}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCita(int idCita)
        {
            try
            {
                var pagos = await _pagoService.GetByCitaAsync(idCita);
                return Ok(new
                {
                    success = true,
                    message = "Pagos obtenidos correctamente",
                    data = pagos
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
        /// Obtener pagos por estado (Pendiente, Completado, Fallido)
        /// </summary>
        [HttpGet("por-estado/{estado}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByEstado(string estado)
        {
            try
            {
                var pagos = await _pagoService.GetByEstadoPagoAsync(estado);
                return Ok(new
                {
                    success = true,
                    message = "Pagos obtenidos correctamente",
                    data = pagos
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
        /// Obtener todos los pagos pendientes
        /// </summary>
        [HttpGet("pendientes")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPendientes()
        {
            try
            {
                var pagos = await _pagoService.GetPagosPendientesAsync();
                return Ok(new
                {
                    success = true,
                    message = "Pagos pendientes obtenidos correctamente",
                    count = pagos.Count(),
                    data = pagos
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
        /// Obtener total pagado por una cita
        /// </summary>
        [HttpGet("total-cita/{idCita}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTotalPorCita(int idCita)
        {
            try
            {
                var total = await _pagoService.CalcularTotalPorCitaAsync(idCita);
                return Ok(new
                {
                    success = true,
                    message = "Total calculado correctamente",
                    data = new { idCita, total }
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
        /// Crear un nuevo pago
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearPagoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _pagoService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdPago },
                    new
                    {
                        success = true,
                        message = "Pago creado correctamente",
                        data = resultado
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
        /// Actualizar un pago
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarPagoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdPago)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _pagoService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Pago actualizado correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Pago con ID {id} no encontrado"
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
        /// Eliminar un pago
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pagoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Pago con ID {id} no encontrado"
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
