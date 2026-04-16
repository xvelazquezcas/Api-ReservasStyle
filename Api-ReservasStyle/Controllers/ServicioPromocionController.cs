using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicioPromocionController : ControllerBase
    {
        private readonly IServicioPromocionService _servicioPromocionService;

        public ServicioPromocionController(IServicioPromocionService servicioPromocionService)
        {
            _servicioPromocionService = servicioPromocionService;
        }

        /// <summary>
        /// Obtener todas las asociaciones servicio-promoción
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var asociaciones = await _servicioPromocionService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Asociaciones servicio-promoción obtenidas correctamente",
                    count = asociaciones.Count(),
                    data = asociaciones
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
        /// Obtener una asociación específica por su clave compuesta
        /// </summary>
        [HttpGet("{idServicioSucursal}/{idPromocion}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int idServicioSucursal, int idPromocion)
        {
            try
            {
                var asociacion = await _servicioPromocionService.GetAsync(idServicioSucursal, idPromocion);
                return Ok(new
                {
                    success = true,
                    message = "Asociación obtenida correctamente",
                    data = asociacion
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Asociación entre ServicioSucursal {idServicioSucursal} " +
                             $"y Promoción {idPromocion} no encontrada"
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
        /// Obtener todas las promociones de un servicio (con detalles)
        /// </summary>
        [HttpGet("servicio/{idServicio}/promociones")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPromocionesDeServicio(int idServicio)
        {
            try
            {
                var promociones = await _servicioPromocionService.GetPromocionesDeServicioAsync(idServicio);
                return Ok(new
                {
                    success = true,
                    message = "Promociones del servicio obtenidas correctamente",
                    count = promociones.Count(),
                    data = promociones
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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
        /// Obtener conteo de promociones de un servicio
        /// </summary>
        [HttpGet("servicio/{idServicio}/promociones/count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountPromocionesDeServicio(int idServicio)
        {
            try
            {
                var count = await _servicioPromocionService.GetCountPromocionesDeServicioAsync(idServicio);
                return Ok(new
                {
                    success = true,
                    message = "Conteo obtenido correctamente",
                    data = new { idServicio, count }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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
        /// Obtener todos los servicios de una promoción
        /// </summary>
        [HttpGet("promocion/{idPromocion}/servicios")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiciosDePromocion(int idPromocion)
        {
            try
            {
                var servicios = await _servicioPromocionService.GetServiciosDePromocionAsync(idPromocion);
                return Ok(new
                {
                    success = true,
                    message = "Servicios de la promoción obtenidos correctamente",
                    count = servicios.Count(),
                    data = servicios
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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
        /// Obtener conteo de servicios de una promoción
        /// </summary>
        [HttpGet("promocion/{idPromocion}/servicios/count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCountServiciosDePromocion(int idPromocion)
        {
            try
            {
                var count = await _servicioPromocionService.GetCountServiciosDePromocionAsync(idPromocion);
                return Ok(new
                {
                    success = true,
                    message = "Conteo obtenido correctamente",
                    data = new { idPromocion, count }
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
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
        /// Crear una nueva asociación servicio-promoción
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearServicioPromocionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _servicioPromocionService.CreateAsync(dto);

                return CreatedAtAction(nameof(Get),
                    new { idServicioSucursal = resultado.IdServicioSucursal, idPromocion = resultado.IdPromocion },
                    new
                    {
                        success = true,
                        message = "Asociación creada correctamente",
                        data = resultado
                    });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
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
        /// Actualizar una asociación servicio-promoción
        /// </summary>
        [HttpPut("{idServicioSucursal}/{idPromocionAnterior}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int idServicioSucursal, int idPromocionAnterior,
            [FromBody] ActualizarServicioPromocionDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _servicioPromocionService.UpdateAsync(
                    idServicioSucursal, idPromocionAnterior, dto);

                return Ok(new
                {
                    success = true,
                    message = "Asociación actualizada correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
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
        /// Eliminar una asociación servicio-promoción
        /// </summary>
        [HttpDelete("{idServicioSucursal}/{idPromocion}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int idServicioSucursal, int idPromocion)
        {
            try
            {
                await _servicioPromocionService.DeleteAsync(idServicioSucursal, idPromocion);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Asociación entre ServicioSucursal {idServicioSucursal} " +
                             $"y Promoción {idPromocion} no encontrada"
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
