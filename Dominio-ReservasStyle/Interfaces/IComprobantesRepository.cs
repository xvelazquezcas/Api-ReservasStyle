using Dominio_ReservasStyle.Entities;

namespace Dominio_ReservasStyle.Interfaces
{
    public interface IComprobantesRepository
    {
        Task<IEnumerable<Comprobantes>> GetAllAsync();
        Task<Comprobantes?> GetByIdAsync(int id);
        Task<Comprobantes> CreateAsync(Comprobantes comprobante);
        Task UpdateAsync(Comprobantes comprobante);
        Task DeleteAsync(int id);
        Task<bool> ExisteFolioAsync(string folio);
        Task<IEnumerable<Comprobantes>> GetByIdPagoAsync(int idPago);
    }
}
