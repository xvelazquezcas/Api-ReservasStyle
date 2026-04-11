using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly ICitaService _citaService;

    public CitasController(ICitaService citaService)
    {
        _citaService = citaService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var citas = await _citaService.GetAllAsync();
        return Ok(citas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cita = await _citaService.GetByIdAsync(id);
        return Ok(cita);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Citas cita)
    {
        var result = await _citaService.CreateAsync(cita);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Citas cita)
    {
        await _citaService.UpdateAsync(cita);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _citaService.DeleteAsync(id);
        return Ok();
    }
}
