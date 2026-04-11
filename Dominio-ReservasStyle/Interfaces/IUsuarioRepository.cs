using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        // Importante para login
        Task<Usuario?> GetByEmailAsync(string email);
    }
}