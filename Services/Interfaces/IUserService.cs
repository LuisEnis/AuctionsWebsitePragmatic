using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
    }
}
