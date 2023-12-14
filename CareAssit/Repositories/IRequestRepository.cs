using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IRequestRepository
    {
        Task<Request?> GetByIdAsync(Guid id);
        Task<List<Request>> GetAllAsync();
        Task<Request> CreateAsync(Request request);
        Task<Request?> UpdateAsync(Guid id, Request request);
        Task<Request?> DeleteAsync(Guid id);
    }
}
