using CareAssit.Data;
using CareAssit.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CareAssit.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly CareAssitDbContext dbContext;

        public SQLUserRepository(CareAssitDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAsync(Guid id)
        {
            var existingUser=await dbContext.Users.FirstOrDefaultAsync(x=>x.User_Id==id);
            if (existingUser==null)
            {
                return null;
            }
            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;

        }

        public async Task<List<User>> GetAllAsync()
        {
            return await dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.User_Id == id);
        }

        public async Task<User?> UpdateAsync(Guid id, User user)
        {
            var existingUser=await dbContext.Users.FirstOrDefaultAsync(x=>x.User_Id == id);
            if(existingUser == null) 
            {
                return null;
            }
            existingUser.Name=user.Name;
            existingUser.ContactNumber=user.ContactNumber;
            existingUser.Dob=user.Dob;
            existingUser.Gender=user.Gender;
            existingUser.Description=user.Description;
            existingUser.Address=user.Address;
            existingUser.Blood_Group=user.Blood_Group;
            await dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
