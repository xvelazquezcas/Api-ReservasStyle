using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> RegisterAsync(Usuario usuario, string password)
        {
            // Encriptar contraseña
            usuario.ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Fecha
            usuario.FechaRegistro = DateTime.UtcNow;

            // Estado
            usuario.Estado = true;

            //ROL (ESTO TE FALTABA)
            usuario.IdRol = 1; // cliente por defecto

            return await _usuarioRepository.CreateAsync(usuario);
        }

        public async Task<Usuario> LoginAsync(string email, string password)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.ContrasenaHash);

            if (!passwordValido)
                throw new Exception("Contraseña incorrecta");

            return usuario;
        }
    }
}