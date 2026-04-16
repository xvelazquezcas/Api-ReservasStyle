using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServicioSucursalController : ControllerBase
    {
        private readonly IServicioSucursalService _servicioSucursalService;

        public ServicioSucursalController(IServicioSucursalService servicioSucursalService)
        {
            _servicioSucursalService = servicioSucursalService;
        }

        /// <summary>
        /// Obtener todos los servicios-sucursales
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var servicios = await _servicioSucursalService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Servicios-sucursales obtenidos correctamente",
                    count = servicios.Count(),
                    data = servicios
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
        /// Obtener un servicio-sucursal por ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var servicio = await _servicioSucursalService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Servicio-sucursal obtenido correctamente",
                    data = servicio
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"ServicioSucursal con ID {id} no encontrado"
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
        /// Obtener detalle de un servicio en una sucursal específica
        /// </summary>
        [HttpGet("detalle/{idServicio}/{idSucursal}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDetalle(int idServicio, int idSucursal)
        {
            try
            {
                var detalle = await _servicioSucursalService.GetDetalleAsync(idServicio, idSucursal);
                return Ok(new
                {
                    success = true,
                    message = "Detalle obtenido correctamente",
                    data = detalle
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
        /// Obtener todos los servicios de una sucursal
        /// </summary>
        [HttpGet("sucursal/{idSucursal}/servicios")]
        [AllowAnonymous]
        public async Task<IActionResult> GetServiciosDeSucursal(int idSucursal)
        {
            try
            {
                var servicios = await _servicioSucursalService.GetServiciosDeSucursalAsync(idSucursal);
                return Ok(new
                {
                    success = true,
                    message = "Servicios de la sucursal obtenidos correctamente",
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
        /// Obtener todos los servicios activos de una sucursal
        /// </summary>
        [HttpGet("sucursal/{idSucursal}/activos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivosBySucursal(int idSucursal)
        {
            try
            {
                var servicios = await _servicioSucursalService.GetActivosBySucursalAsync(idSucursal);
                return Ok(new
                {
                    success = true,
                    message = "Servicios activos de la sucursal obtenidos correctamente",
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
        /// Obtener todas las sucursales donde está un servicio
        /// </summary>
        [HttpGet("servicio/{idServicio}/sucursales")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSucursalesDelServicio(int idServicio)
        {
            try
            {
                var sucursales = await _servicioSucursalService.GetSucursalesDelServicioAsync(idServicio);
                return Ok(new
                {
                    success = true,
                    message = "Sucursales del servicio obtenidas correctamente",
                    count = sucursales.Count(),
                    data = sucursales
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
        /// Obtener todos los servicios-sucursales activos
        /// </summary>
        [HttpGet("activos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetActivos()
        {
            try
            {
                var servicios = await _servicioSucursalService.GetActivosAsync();
                return Ok(new
                {
                    success = true,
                    message = "Servicios-sucursales activos obtenidos correctamente",
                    count = servicios.Count(),
                    data = servicios
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
        /// Obtener el precio de un servicio en una sucursal
        /// </summary>
        [HttpGet("precio/{idServicio}/{idSucursal}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPrecio(int idServicio, int idSucursal)
        {
            try
            {
                var precio = await _servicioSucursalService.GetPrecioAsync(idServicio, idSucursal);
                return Ok(new
                {
                    success = true,
                    message = "Precio obtenido correctamente",
                    data = new { idServicio, idSucursal, precio, precioFormato = $"${precio:F2}" }
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
        /// Obtener precio promedio de un servicio en todas las sucursales
        /// </summary>
        [HttpGet("precio-promedio/{idServicio}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPrecioPromedio(int idServicio)
        {
            try
            {
                var promedio = await _servicioSucursalService.GetPrecioPromedioPorServicioAsync(idServicio);
                return Ok(new
                {
                    success = true,
                    message = "Precio promedio obtenido correctamente",
                    data = new { idServicio, precioPromedio = promedio, precioPromedioFormato = $"${promedio:F2}" }
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
        /// Obtener servicios-sucursales dentro de un rango de precio
        /// </summary>
        [HttpGet("rango-precio/{precioMin}/{precioMax}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByRangoPrecio(decimal precioMin, decimal precioMax)
        {
            try
            {
                var servicios = await _servicioSucursalService.GetByRangoPrecioAsync(precioMin, precioMax);
                return Ok(new
                {
                    success = true,
                    message = $"Servicios con precio entre ${precioMin:F2} y ${precioMax:F2} obtenidos correctamente",
                    count = servicios.Count(),
                    data = servicios
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
        /// Crear un nuevo servicio-sucursal
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearServicioSucursalDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _servicioSucursalService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdServicioSucursal },
                    new
                    {
                        success = true,
                        message = "Servicio-sucursal creado correctamente",
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
        /// Actualizar un servicio-sucursal
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarServicioSucursalDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdServicioSucursal)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _servicioSucursalService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Servicio-sucursal actualizado correctamente",
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
        /// Actualizar solo el precio de un servicio en una sucursal
        /// </summary>
        [HttpPatch("precio/{idServicio}/{idSucursal}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePrecio(int idServicio, int idSucursal, [FromBody] decimal nuevoPrecio)
        {
            try
            {
                var resultado = await _servicioSucursalService.UpdatePrecioAsync(idServicio, idSucursal, nuevoPrecio);

                return Ok(new
                {
                    success = true,
                    message = "Precio actualizado correctamente",
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
        /// Alternar el estado (activo/inactivo) de un servicio en una sucursal
        /// </summary>
        [HttpPatch("toggle-estado/{idServicio}/{idSucursal}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleEstado(int idServicio, int idSucursal)
        {
            try
            {
                var resultado = await _servicioSucursalService.ToggleEstadoAsync(idServicio, idSucursal);

                return Ok(new
                {
                    success = true,
                    message = "Estado alternado correctamente",
                    count = resultado.Count(),
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
        /// Eliminar un servicio-sucursal
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicioSucursalService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"ServicioSucursal con ID {id} no encontrado"
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
