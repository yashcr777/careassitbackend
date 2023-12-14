using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IInsurancePlanRepository
    {
        Task<InsurancePlan> CreateAsync(InsurancePlan insurancePlan);
        Task<List<InsurancePlan>> GetAllAsync();
        Task<InsurancePlan?> GetByIdAsync(Guid id);
        Task<InsurancePlan?> UpdateAsync(Guid id, InsurancePlan insurancePlan);

        Task<InsurancePlan?> DeleteAsync(Guid id);
    }
}
