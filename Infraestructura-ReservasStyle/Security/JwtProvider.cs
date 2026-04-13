using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplicacion_ReservasStyle.Interfaces;
using Dominio_ReservasStyle.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructura_ReservasStyle.Security
{
    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _config;

        public JwtProvider(IConfiguration config) => _config = config;

        public string Generate(Usuario usuario)
        {
            // 1. Definir Claims (Aquí agregas tus claims personalizados)
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email ?? string.Empty),
                new Claim("nombre_completo", $"{usuario.Nombre} {usuario.Apellido}"), // Claim personalizado
                // new Claim(JwtRegisteredClaimNames.UniqueName, usuario.IdRol.ToString())
            };

            // 2. Agregar Roles como Claims
            if (usuario.UsuarioRoles != null)
            {
                foreach (var usuarioRol in usuario.UsuarioRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, usuarioRol.Rol.Nombre));
                }
            }

            // 3. Crear Firma
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"] ?? "Dx1rkyuenkxluSX0spSXBEjlrLnZIMfa7xKXG0pXZRN"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. Crear el Token
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"] ?? "ReservasStyle",
                audience: _config["Jwt:Audience"] ?? "ReservasStyleAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
///jsjsjsjsjsjsjsjs
// JSJSJSJSJSJS