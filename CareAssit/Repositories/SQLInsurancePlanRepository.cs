using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLInsurancePlanRepository : IInsurancePlanRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLInsurancePlanRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<InsurancePlan> CreateAsync(InsurancePlan insurancePlan)
        {
            await dbContext.InsurancePlans.AddAsync(insurancePlan);
            await dbContext.SaveChangesAsync();
            return insurancePlan;
        }

        public async Task<InsurancePlan?> DeleteAsync(Guid id)
        {
            var existingInsurancePlan= dbContext.InsurancePlans.FirstOrDefault(x=>x.InsurancePlan_Id==id);
            if(existingInsurancePlan==null) 
            {
                return null;
            }
            dbContext.InsurancePlans.Remove(existingInsurancePlan);
            await dbContext.SaveChangesAsync();
            return existingInsurancePlan;
        }

        public async Task<List<InsurancePlan>> GetAllAsync()
        {
            return await dbContext.InsurancePlans.ToListAsync();
        }

        public async Task<InsurancePlan?> GetByIdAsync(Guid id)
        {
            return await dbContext.InsurancePlans.FirstOrDefaultAsync(x=>x.InsurancePlan_Id==id);
        }

        public async Task<InsurancePlan?> UpdateAsync(Guid id, InsurancePlan insurancePlan)
        {
            var existingInsurancePlan=await dbContext.InsurancePlans.FirstOrDefaultAsync(x=>x.InsurancePlan_Id==id);
            if(existingInsurancePlan==null)
            {
                return null;
            }
            existingInsurancePlan.Insurance_Name = insurancePlan.Insurance_Name;
            existingInsurancePlan.Insurance_Description = insurancePlan.Insurance_Duration;
            existingInsurancePlan.Insurance_Price = insurancePlan.Insurance_Price;
            existingInsurancePlan.Insurance_Duration = insurancePlan.Insurance_Duration;
            await dbContext.SaveChangesAsync();
            return existingInsurancePlan;
        }
    }
}
