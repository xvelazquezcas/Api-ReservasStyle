using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var empleados = await _empleadoService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Empleados obtenidos correctamente",
                    data = empleados
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
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Empleado")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var empleado = await _empleadoService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Empleado obtenido correctamente",
                    data = empleado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Sucursal con ID {id} no encontrada"
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
        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CrearEmpleadoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _empleadoService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdEmpleado },
                    new
                    {
                        success = true,
                        message = "Empleado creado correctamente",
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
        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarEmpleadoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdEmpleado)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _empleadoService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Empleado actualizado correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Empleado con ID {id} no encontrado"
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
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _empleadoService.DeleteAsync(id);

                return NoContent(); // 204 No Content es el estándar para DELETE exitoso
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Empleado con ID {id} no encontrado"
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