using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
//using Aplicacion_ReservasStyle.Servicios.Aplicacion_ReservasStyle.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace API_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioService _servicioService;

        public ServicioController(IServicioService servicioService)
        {
            _servicioService = servicioService;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _servicioService.ObtenerTodos();
            return Ok(lista);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servicio = await _servicioService.ObtenerPorId(id);

            if (servicio == null)
                return NotFound();

            return Ok(servicio);
        }

        //CREATE
        [HttpPost]
        public async Task<IActionResult> Crear(ServicioCreateDto dto)
        {
            var servicio = await _servicioService.Crear(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = servicio.IdServicio },
                servicio);
        }

        // UPDATE
        [HttpPut]
        public async Task<IActionResult> Actualizar(ServicioUpdateDto dto)
        {
            await _servicioService.Actualizar(dto);
            return NoContent();
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var eliminado = await _servicioService.Eliminar(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}