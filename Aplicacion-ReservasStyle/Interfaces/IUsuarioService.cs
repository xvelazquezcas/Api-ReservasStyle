using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> RegisterAsync(Usuario usuario, string password);
        Task<Usuario> LoginAsync(string email, string password);
    }
}
