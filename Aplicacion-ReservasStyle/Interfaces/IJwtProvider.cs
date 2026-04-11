using Dominio_ReservasStyle.Entities;

namespace Aplicacion_ReservasStyle.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(Usuario usuario);
    }
}
