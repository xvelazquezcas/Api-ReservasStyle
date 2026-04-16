using Aplicacion_ReservasStyle.DTOs;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api_ReservasStyle.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public UsuariosController(IUsuarioService usuarioService, IAuthService authService)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        // REGISTRO
        [HttpPost("register")]
        public async Task<IActionResult> Registro([FromBody] RegistroDto dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Email = dto.Email,
                    Telefono = dto.Telefono,
                    ContrasenaHash = dto.Password, // PRUEBA
                    FechaRegistro = DateTime.UtcNow,
                    Estado = true,
                    IdRol = 1
                };

                var result = await _usuarioService.RegisterAsync(usuario, dto.Password);

                return Ok(new
                {
                    message = "Usuario registrado correctamente",
                    usuario = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var usuario = await _authService.LoginAsync(dto.Email, dto.Password);

                return Ok(new
                {
                    message = "Login exitoso",
                    usuario = usuario
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
        }
    }
}