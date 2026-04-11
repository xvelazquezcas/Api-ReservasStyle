using Aplicacion_ReservasStyle.Interfaces;
using BCrypt.Net;
using Dominio_ReservasStyle.Interfaces;

namespace Aplicacion_ReservasStyle.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUsuarioRepository usuarioRepository, IJwtProvider jwtProvider)
        {
            _usuarioRepository = usuarioRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            // 1. Buscar usuario e incluir roles
            var usuario = await _usuarioRepository.GetByEmailAsync(email);

            if (usuario == null)
                throw new Exception("Credenciales inválidas");

            // 2. Verificar contraseña
            bool passwordValido = BCrypt.Net.BCrypt.Verify(password, usuario.ContrasenaHash);
            
            if (!passwordValido)
                throw new Exception("Credenciales inválidas");

            // 3. Generar Token
            return _jwtProvider.Generate(usuario);
        }
    }
}
