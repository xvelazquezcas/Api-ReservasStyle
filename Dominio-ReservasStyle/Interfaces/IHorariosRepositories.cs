using Dominio_ReservasStyle.Entities;
using Dominio_ReservasStyle.Enums;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IHorariosRepository
    {
        Task<HorariosDisponibles?> GetHorarioAsync(int empleadoId, DiaSemana dia);
    }
}