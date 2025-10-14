using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task SaveAsync();
    }
}
