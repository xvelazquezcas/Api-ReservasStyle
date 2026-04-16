using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago?> GetByIdAsync(int id);
        Task<Pago> CreateAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
        Task<IEnumerable<Pago>> GetByCitaAsync(int idCita);
        Task<IEnumerable<Pago>> GetByEstadoPagoAsync(string estadoPago);
        Task<decimal> CalcularTotalPorCitaAsync(int idCita);
        Task<IEnumerable<Pago>> GetPagosPendientesAsync();
        Task<Pago?> GetByReferenciaTransaccionAsync(string referencia);
    }
}