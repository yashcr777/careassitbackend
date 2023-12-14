using CareAssit.Models.Domain;

namespace CareAssit.Repositories
{
    public interface ISignUpRepository
    {
        Task<SignUp> CreateAsync(SignUp signUp);
        Task<List<SignUp>> GetAllAsync();
        Task<SignUp?> GetByIdAsync(Guid id);
        Task<SignUp?> UpdateAsync(Guid id, SignUp signUp);

        Task<SignUp?> DeleteAsync(Guid id);
    }
}
