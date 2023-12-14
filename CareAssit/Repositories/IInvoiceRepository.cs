using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice> CreateAsync(Invoice invoice);
        Task<List<Invoice>> GetAllAsync();
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<Invoice?> UpdateAsync(Guid id, Invoice invoice);

        Task<Invoice?> DeleteAsync(Guid id);
    }
}
