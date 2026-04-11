using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requiere estar logueado
public class CitasController : ControllerBase
{
    private readonly ICitaService _citaService;

    public CitasController(ICitaService citaService)
    {
        _citaService = citaService;
    }

    [HttpGet]
    [AllowAnonymous] // Este endpoint es público
    public async Task<IActionResult> GetAll()
    {
        var citas = await _citaService.GetAllAsync();
        return Ok(citas);
    }

    [HttpGet("{id}")]
    [AllowAnonymous] // Este endpoint es público
    public async Task<IActionResult> GetById(int id)
    {
        var cita = await _citaService.GetByIdAsync(id);
        return Ok(cita);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Empleado")] // Solo estos roles pueden crear
    public async Task<IActionResult> Create([FromBody] Citas cita)
    {
        // Obtener claims desde el usuario autenticado
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var nombreCompleto = User.FindFirst("nombre_completo")?.Value;

        // Verificar que el usuario está autenticado
        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Usuario no autenticado");

        var result = await _citaService.CreateAsync(cita);
        return Ok(new
        {
            mensaje = "Cita creada exitosamente",
            citaId = result.IdCita,
            usuarioId = userId,
            email = email,
            empleado = nombreCompleto
        });
    }

    [HttpPut]
    [Authorize(Roles = "Admin,Empleado")]
    public async Task<IActionResult> Update([FromBody] Citas cita)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Usuario no autenticado");

        await _citaService.UpdateAsync(cita);
        return Ok(new { mensaje = "Cita actualizada exitosamente" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] // Solo admins pueden eliminar
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized("Usuario no autenticado");

        await _citaService.DeleteAsync(id);
        return Ok(new { mensaje = "Cita eliminada exitosamente" });
    }
}
