using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLInsuranceCompanyRepository : IInsuranceCompanyRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLInsuranceCompanyRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<InsuranceCompany> CreateAsync(InsuranceCompany insuranceCompany)
        {
            await dbContext.InsuranceCompanies.AddAsync(insuranceCompany);
            await dbContext.SaveChangesAsync();
            return insuranceCompany;
        }

        public async Task<InsuranceCompany?> DeleteAsync(Guid id)
        {
            var existingInsuranceCom = await dbContext.InsuranceCompanies.FirstOrDefaultAsync(x => x.InsuranceCompany_Id == id);
            if (existingInsuranceCom == null)
            {
                return null;
            }
            dbContext.InsuranceCompanies.Remove(existingInsuranceCom);
            await dbContext.SaveChangesAsync();
            return existingInsuranceCom;
        }

        public async Task<List<InsuranceCompany>> GetAllAsync()
        {
            return await dbContext.InsuranceCompanies.ToListAsync();
        }

        public async Task<InsuranceCompany?> GetByIdAsync(Guid id)
        {
            return await dbContext.InsuranceCompanies.FirstOrDefaultAsync(x => x.InsuranceCompany_Id == id);
        }

        public Task<InsuranceCompany?> UpdateAsync(Guid id, InsuranceCompany insuranceCompany)
        {
            throw new NotImplementedException();
        }
    }
}
