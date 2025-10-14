using AuctionsWebsitePragmatic.Models;

namespace AuctionsWebsitePragmatic.Repositories.Interfaces
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAllAsync();
        Task<IEnumerable<Auction>> GetActiveAuctionsAsync();
        Task<Auction?> GetByIdAsync(int id);
        Task AddAsync(Auction auction);
        void Update(Auction auction);
        void Delete(Auction auction);
        Task SaveAsync();
    }
}
