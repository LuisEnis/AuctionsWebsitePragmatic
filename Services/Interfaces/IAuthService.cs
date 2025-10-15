using AuctionsWebsitePragmatic.Models;
using AuctionsWebsitePragmatic.ViewModels.Account;

namespace AuctionsWebsitePragmatic.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string? Error)> RegisterAsync(RegisterViewModel model);
        Task<(bool Success, string? Error, User? User)> ValidateLoginAsync(LoginViewModel model);
    }
}
