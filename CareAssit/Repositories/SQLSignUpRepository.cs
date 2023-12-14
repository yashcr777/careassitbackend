using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLSignUpRepository : ISignUpRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLSignUpRepository(CareAssitDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<SignUp> CreateAsync(SignUp signUp)
        {
            await dbContext.SignUps.AddAsync(signUp);
            await dbContext.SaveChangesAsync();
            return signUp;
        }

        public async Task<SignUp?> DeleteAsync(Guid id)
        {
            var existingSignUp = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Id == id);
            if (existingSignUp == null)
            {
                return null;
            }
            dbContext.SignUps.Remove(existingSignUp);
            await dbContext.SaveChangesAsync();
            return existingSignUp;
        }

        public async Task<List<SignUp>> GetAllAsync()
        {
            return await dbContext.SignUps.ToListAsync();
        }

        public async Task<SignUp?> GetByIdAsync(Guid id)
        {
            return await dbContext.SignUps.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<SignUp?> UpdateAsync(Guid id, SignUp signUp)
        {
            var existingSignUp = await dbContext.SignUps.FirstOrDefaultAsync(x => x.Id == id);
            if (existingSignUp == null)
            {
                return null;
            }
            existingSignUp.Email = signUp.Email;
            existingSignUp.Password = signUp.Password;
            existingSignUp.User_Name = signUp.User_Name;

            await dbContext.SaveChangesAsync();
            return existingSignUp;
        }
    }
}
