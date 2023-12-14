using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IClaimRepository
    {
        Task<Claim> CreateAsync(Claim claim);
        Task<List<Claim>> GetAllAsync();
        Task<Claim?> GetByIdAsync(Guid id);
        Task<Claim?> UpdateAsync(Guid id, Claim claim);

        Task<Claim?> DeleteAsync(Guid id);

    }
}
