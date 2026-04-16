using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Enums;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HorariosDisponiblesController : ControllerBase
    {
        private readonly IHorariosDisponiblesService _horariosDisponiblesService;

        public HorariosDisponiblesController(IHorariosDisponiblesService horariosDisponiblesService)
        {
            _horariosDisponiblesService = horariosDisponiblesService;
        }

        /// <summary>
        /// Obtener todos los horarios disponibles
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var horarios = await _horariosDisponiblesService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Horarios disponibles obtenidos correctamente",
                    data = horarios
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
        /// Obtener un horario disponible por ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var horario = await _horariosDisponiblesService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Horario disponible obtenido correctamente",
                    data = horario
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario disponible con ID {id} no encontrado"
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
        /// Obtener horarios disponibles por empleado
        /// </summary>
        [HttpGet("por-empleado/{idEmpleado}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByEmpleado(int idEmpleado)
        {
            try
            {
                var horarios = await _horariosDisponiblesService.GetByEmpleadoAsync(idEmpleado);
                return Ok(new
                {
                    success = true,
                    message = "Horarios obtenidos correctamente",
                    data = horarios
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
        /// Obtener horarios disponibles por día de la semana
        /// </summary>
        [HttpGet("por-dia/{dia}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByDia(string dia)
        {
            try
            {
                if (!Enum.TryParse<DiaSemana>(dia, true, out var diaSemana))
                    return BadRequest(new
                    {
                        success = false,
                        message = $"Día inválido. Use: Lunes, Martes, Miércoles, Jueves, Viernes, Sábado, Domingo"
                    });

                var horarios = await _horariosDisponiblesService.GetByDiaAsync(diaSemana);
                return Ok(new
                {
                    success = true,
                    message = "Horarios obtenidos correctamente",
                    data = horarios
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
        /// Crear un nuevo horario disponible
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearHorariosDisponiblesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _horariosDisponiblesService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdHorario },
                    new
                    {
                        success = true,
                        message = "Horario disponible creado correctamente",
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
        /// Actualizar un horario disponible
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarHorariosDisponiblesDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdHorario)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _horariosDisponiblesService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Horario disponible actualizado correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario disponible con ID {id} no encontrado"
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
        /// Eliminar un horario disponible
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _horariosDisponiblesService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario disponible con ID {id} no encontrado"
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
