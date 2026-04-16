using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HorariosLocalesController : ControllerBase
    {
        private readonly IHorarioLocalService _horarioLocalService;

        public HorariosLocalesController(IHorarioLocalService horarioLocalService)
        {
            _horarioLocalService = horarioLocalService;
        }

        /// <summary>
        /// Obtener todos los horarios locales
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var horariosLocales = await _horarioLocalService.GetAllAsync();
                return Ok(new
                {
                    success = true,
                    message = "Horarios locales obtenidos correctamente",
                    data = horariosLocales
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
        /// Obtener un horario local por ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var horarioLocal = await _horarioLocalService.GetByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Horario local obtenido correctamente",
                    data = horarioLocal
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario local con ID {id} no encontrado"
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
        /// Obtener horarios locales por IdSucursal
        /// </summary>
        [HttpGet("por-sucursal/{idSucursal}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdSucursal(int idSucursal)
        {
            try
            {
                var horariosLocales = await _horarioLocalService.GetByIdSucursalAsync(idSucursal);
                return Ok(new
                {
                    success = true,
                    message = "Horarios locales obtenidos correctamente",
                    data = horariosLocales
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
        /// Obtener horario local por IdSucursal y DiaSemana
        /// </summary>
        [HttpGet("por-sucursal/{idSucursal}/dia/{dia}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdSucursalAndDia(int idSucursal, string dia)
        {
            try
            {
                var horarioLocal = await _horarioLocalService.GetByIdSucursalAndDiaAsync(idSucursal, dia);
                if (horarioLocal == null)
                    return NotFound(new
                    {
                        success = false,
                        message = $"No existe horario para la sucursal {idSucursal} en el día '{dia}'"
                    });

                return Ok(new
                {
                    success = true,
                    message = "Horario local obtenido correctamente",
                    data = horarioLocal
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
        /// Crear un nuevo horario local
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CrearHorarioLocalDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var resultado = await _horarioLocalService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = resultado.IdHorarioLocal },
                    new
                    {
                        success = true,
                        message = "Horario local creado correctamente",
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
        /// Actualizar un horario local
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarHorarioLocalDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != dto.IdHorarioLocal)
                    return BadRequest(new
                    {
                        success = false,
                        message = "El ID de la URL no coincide con el ID del DTO"
                    });

                var resultado = await _horarioLocalService.UpdateAsync(id, dto);

                return Ok(new
                {
                    success = true,
                    message = "Horario local actualizado correctamente",
                    data = resultado
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario local con ID {id} no encontrado"
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
        /// Eliminar un horario local
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _horarioLocalService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"Horario local con ID {id} no encontrado"
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
