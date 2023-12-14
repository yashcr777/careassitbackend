using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task <User> CreateAsync(User user);
        Task<User?> UpdateAsync(Guid id, User user);
        Task <User?> DeleteAsync(Guid id);
    }
}
