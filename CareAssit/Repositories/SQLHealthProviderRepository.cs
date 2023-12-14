using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CareAssit.Repositories
{
    public class SQLHealthProviderRepository : IHealthProviderRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLHealthProviderRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<HealthProvider> CreateAsync(HealthProvider healthProvider)
        {
            await dbContext.HealthProviders.AddAsync(healthProvider);
            await dbContext.SaveChangesAsync();
            return healthProvider;
        }

        public async Task<HealthProvider?> DeleteAsync(Guid id)
        {
            var existingHealthProvider = await dbContext.HealthProviders.FirstOrDefaultAsync(x => x.Health_Id == id);
            if (existingHealthProvider == null)
            {
                return null;
            }
            dbContext.HealthProviders.Remove(existingHealthProvider);
            await dbContext.SaveChangesAsync();
            return existingHealthProvider;
        }

        public async Task<List<HealthProvider>> GetAllAsync()
        {
            return await dbContext.HealthProviders.ToListAsync();
        }

        public async Task<HealthProvider?> GetByIdAsync(Guid id)
        {
            return await dbContext.HealthProviders.FirstOrDefaultAsync(x => x.Health_Id == id);
        }

        public Task<HealthProvider?> UpdateAsync(Guid id, HealthProvider healthProvider)
        {
            throw new NotImplementedException();
        }
    }
}
