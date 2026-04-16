using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Servicios.Aplicacion_ReservasStyle.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace API_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly ServicioService _service;

        public ServicioController(ServicioService service)
        {
            _service = service;
        }

        //GET: api/servicio
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicios = await _service.ObtenerTodos();
            return Ok(servicios);
        }

        //GET: api/servicio/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servicio = await _service.ObtenerPorId(id);

            if (servicio == null)
                return NotFound("Servicio no encontrado");

            return Ok(servicio);
        }

        // POST: api/servicio
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicioCreateDto dto)
        {
            try
            {
                await _service.Crear(dto);
                return Ok("Servicio creado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //PUT api/servicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServicioUpdateDto dto)
        {
            if (id != dto.IdServicio)
                return BadRequest("El ID no coincide");

            try
            {
                await _service.Actualizar(dto);
                return Ok("Servicio actualizado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE: api/servicio/5 (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Eliminar(id);
                return Ok("Servicio eliminado");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}