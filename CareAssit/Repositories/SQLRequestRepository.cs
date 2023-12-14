using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLRequestRepository : IRequestRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLRequestRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Request> CreateAsync(Request request)
        {
            await dbContext.Requests.AddAsync(request);
            await dbContext.SaveChangesAsync();
            return request;
        }

        public async Task<Request?> DeleteAsync(Guid id)
        {
            var existingRequest = await dbContext.Requests.FirstOrDefaultAsync(x => x.Request_Id == id);
            if (existingRequest == null)
            {
                return null;
            }
            dbContext.Requests.Remove(existingRequest);
            await dbContext.SaveChangesAsync();
            return existingRequest;
        }

        public async Task<List<Request>> GetAllAsync()
        {
            return await dbContext.Requests.ToListAsync();
        }

        public async Task<Request?> GetByIdAsync(Guid id)
        {
            return await dbContext.Requests.FirstOrDefaultAsync(x => x.Request_Id == id);
        }

        public async Task<Request?> UpdateAsync(Guid id, Request request)
        {
            var existingRequest = await dbContext.Requests.FirstOrDefaultAsync(x => x.Request_Id == id);
            if (existingRequest == null)
            {
                return null;
            }
            existingRequest.InsurancePlan_Id = request.InsurancePlan_Id;
            existingRequest.User_Id = request.User_Id;
            existingRequest.Health_Id = request.Health_Id;
            existingRequest.DocUrl=request.DocUrl;
            await dbContext.SaveChangesAsync();
            return existingRequest;
        }
    }
}
