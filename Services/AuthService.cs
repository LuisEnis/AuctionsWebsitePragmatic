using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.Repositories.Interfaces;
using AuctionsWebsitePragmatic.Services.Interfaces;
using AuctionsWebsitePragmatic.ViewModels.Account;
using BCrypt.Net;

namespace AuctionsWebsitePragmatic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(RegisterViewModel model)
        {
            var existsByUsername = await _userRepo.GetByUsernameAsync(model.Username);
            if (existsByUsername != null) return (false, "Username already taken.");

            var existsByEmail = (await _userRepo.GetAllAsync()).FirstOrDefault(u => u.Email == model.Email);
            if (existsByEmail != null) return (false, "Email already registered.");

            var user = new User
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Wallet = 1000.00m
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? Error, User? User)> ValidateLoginAsync(LoginViewModel model)
        {
            var user = (await _userRepo.GetAllAsync()).FirstOrDefault(u => u.Email == model.Email);
            if (user == null) return (false, "Invalid credentials.", null);

            var valid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);
            if (!valid) return (false, "Invalid credentials.", null);

            return (true, null, user);
        }
    }
}
