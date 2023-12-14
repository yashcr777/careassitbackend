using CareAssit.Data;
using CareAssit.Models.Domain;
using CareAssit.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CareAssit.Repositories
{
    public class SQLClaimRepository : IClaimRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLClaimRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Claim> CreateAsync(Claim claim)
        {
            await dbContext.Claims.AddAsync(claim);
            await dbContext.SaveChangesAsync();
            return claim;
        }

        public async Task<Claim?> DeleteAsync(Guid id)
        {
            var existingClaim = await dbContext.Claims.FirstOrDefaultAsync(x => x.Claim_Id == id);
            if (existingClaim == null)
            {
                return null;
            }
            dbContext.Claims.Remove(existingClaim);
            await dbContext.SaveChangesAsync();
            return existingClaim;
        }

        public async Task<List<Claim>> GetAllAsync()
        {
            return await dbContext.Claims.ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(Guid id)
        {
            return await dbContext.Claims.FirstOrDefaultAsync(x => x.Claim_Id == id);
        }

        /*public Task<Claim?> UpdateAsync(Guid id, Claim claim)
        {
            throw new NotImplementedException();
        }*/

        public async Task<Claim?> UpdateAsync(Guid id, Claim claim)
        {
            var existingClaim = await dbContext.Claims.FirstOrDefaultAsync(x => x.Claim_Id == id);
            if (existingClaim == null)
            {
                return null;
            }
            /*existingClaim.Claim_Id = claim.Claim_Id;
            existingClaim.User_Id = claim.User_Id;
            existingClaim.Invoice_Id = claim.Invoice_Id;
            existingClaim.InsuranceCompant_Id = claim.InsuranceCompant_Id;
            existingClaim.User_Name = claim.User_Name;
            existingClaim.Dob = claim.Dob;
            existingClaim.Address = claim.Address;
            existingClaim.DateOfService = claim.DateOfService;
            existingClaim.Treatment = claim.Treatment;
            existingClaim.Diagnosis = claim.Diagnosis;
            existingClaim.Claim_Amount = claim.Claim_Amount;
            existingClaim.Invoice_Amount = claim.Invoice_Amount;*/
            existingClaim.status2 = 0;
            await dbContext.SaveChangesAsync();
            return existingClaim;
        }
    }
}
