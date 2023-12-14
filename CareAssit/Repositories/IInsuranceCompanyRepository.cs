using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IInsuranceCompanyRepository
    {
        Task<InsuranceCompany?> GetByIdAsync(Guid id);
        Task<InsuranceCompany> CreateAsync(InsuranceCompany insuranceCompany);
        Task<InsuranceCompany?> UpdateAsync(Guid id, InsuranceCompany insuranceCompany);
        Task<InsuranceCompany?> DeleteAsync(Guid id);
        Task<List<InsuranceCompany>> GetAllAsync();
    }
}
