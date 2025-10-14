using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int userId);
        Task<Transaction?> GetByIdAsync(int id);
        Task AddAsync(Transaction transaction);
        Task SaveAsync();
    }
}
