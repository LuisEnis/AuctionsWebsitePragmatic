using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using AuctionsWebsitePragmatic.Services.Interfaces;

namespace AuctionsWebsitePragmatic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _userRepo.GetByUsernameAsync(username);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _userRepo.Update(user);
            await _userRepo.SaveAsync();
        }
    }
}
