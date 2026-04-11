namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string email, string password);
    }
}
