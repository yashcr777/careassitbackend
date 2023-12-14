using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IHealthProviderRepository
    {
        Task<HealthProvider> CreateAsync(HealthProvider healthProvider);
        Task<List<HealthProvider>> GetAllAsync();
        Task<HealthProvider?> GetByIdAsync(Guid id);
        Task<HealthProvider?> UpdateAsync(Guid id, HealthProvider healthProvider);

        Task<HealthProvider?> DeleteAsync(Guid id);
    }
}
